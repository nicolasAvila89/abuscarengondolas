package ar.com.utn.changuito.architecture.net;

import ar.com.utn.changuito.model.Statistic;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;

public class SharedObject {

    private final Map<String, Object> data;

    public SharedObject() {
        data = new HashMap<String, Object>(20);
    }

    public SharedObject(final SharedObject sharedObject) {
        this(sharedObject.data);
    }

    private SharedObject(final Map<String, Object> data) {
        this.data = data;
    }

    public static SharedObject deserialize(final byte[] jsonCodeBytes) {

        return new SharedObject(JsonUtils.convertJsonBytesToMap(jsonCodeBytes));
    }

    public static SharedObject deserialize(final String jsonCode) {

        return new SharedObject(JsonUtils.convertJsonStringToMap(jsonCode));
    }

    public static void main(String[] args) {

        testSerialization();
        testMergeWith();
        testModelClassSharedObject();
    }

    private static void testSerialization() {
        final SharedObject child = new SharedObject();
        child.set("childName", "mateo");

        final SharedObject parent = new SharedObject();
        parent.set("parentName", "fernando");
        parent.set("child", child);

        SharedObject parentDeserialized = SharedObject.deserialize(parent.serialize());

        System.err.println(parentDeserialized);

        SharedObject childDeserialized = parentDeserialized.getSharedObject("child");

        System.err.println(childDeserialized.getString("childName"));
    }

    private static void testMergeWith() {
        SharedObject a = new SharedObject();
        SharedObject b = new SharedObject();
        SharedObject c = new SharedObject();
        SharedObject d = new SharedObject();

        b.set("id", "1");
        b.set("b", "1");

        d.set("id", "2");
        d.set("b", "2");
        d.set("d", "2");

        a.set("so", b);
        c.set("so", d);

        System.err.println("a: " + a);
        System.err.println("c: " + c);

        a.mergeWith(c);

        System.err.println("a.MergeWith(c): " + a);

        SharedObject e = new SharedObject();

        System.err.println("e: " + e);

        e.mergeWith(b);

        System.err.println("b: " + b);
        System.err.println("e.MergeWith(b): " + e);
    }

    private static void testModelClassSharedObject() {
        Statistic statistic = new Statistic();
        statistic.setId(123L);

        System.err.println("Statistic: " + statistic);

        SharedObject sharedObject = new SharedObject();
        sharedObject.set("child", statistic);

        System.err.println("SharedObject: " + sharedObject);

        Statistic statisticCopy = sharedObject.getSharedObject("child", Statistic.class);
        statisticCopy.set("copy", "true");

        System.err.println("Statistic: " + statistic);
        System.err.println("StatisticCopy: " + statisticCopy);
    }

    public Set<String> getKeys() {
        return data.keySet();
    }

    public Object get(final String key) {
        final Object value = data.get(key);
        if (value instanceof SharedObject) {
            return value.toString();
        }
        return value;
    }

    public double getDouble(final String key) {
        return (Double) data.get(key);
    }

    public int getInt(final String key) {
        return (Integer) data.get(key);
    }

    public long getLong(final String key) {
        final Object value = data.get(key);
        if (value instanceof Integer) {
            return (long) (Integer) value;
        }
        return (Long) value;
    }

    public String getString(final String key) {
        return (String) data.get(key);
    }

    public <T extends SharedObject> T getSharedObject(final String key, final Class<T> classType) {

        try {
            final T result = classType.newInstance();
            result.mergeWith(getSharedObject(key));
            return result;
        } catch (final Exception exception) {
            throw new RuntimeException(exception.getMessage(), exception);
        }
    }

    public SharedObject getSharedObject(final String key) {

        final Object value = data.get(key);

        SharedObject valueAsSharedObject = null;
        if (value instanceof SharedObject) {
            valueAsSharedObject = (SharedObject) value;
        } else if (valueAsSharedObject == null) {
            valueAsSharedObject = deserialize((String) value);
            // Preferible dejar el objeto ya deserializado, en vez de deserializarlo siempre
            set(key, valueAsSharedObject);
        }
        return valueAsSharedObject;
    }

    public void mergeWith(final SharedObject sharedObject) {
        for (final Map.Entry<String, Object> entry : sharedObject.data.entrySet()) {
            final String key = entry.getKey();
            final Object value = entry.getValue();

            if (value instanceof SharedObject) {
                final SharedObject valueAsSharedObject = (SharedObject) value;
                final Object oldValue = getSharedObject(key);

                if (oldValue instanceof SharedObject) {
                    ((SharedObject) oldValue).mergeWith(valueAsSharedObject);
                } else {
                    set(key, valueAsSharedObject);
                }
            } else {
                set(key, value);
            }
        }
    }

    public byte[] serialize() {

        return JsonUtils.convertMapToJsonBytes(data);
    }

    public <T> void set(final String key, final T value) {
        data.put(key, value);
    }

    @Override
    public String toString() {

        return JsonUtils.convertMapToJsonString(data);
    }
}