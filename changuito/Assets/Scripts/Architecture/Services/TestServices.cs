﻿using UnityEngine;
using System.Collections;
using System;

public class TestServices : MonoBehaviour
{

	void Start ()
	{
//		TestServiceCall ();
//		TestServiceInvalidCall ();
//		TestSerialization ();
//		TestMergeWith ();
//		TestModelClassSharedObject();
		TestTrueServerCall ();
	}

	void TestTrueServerCall ()
	{
		Statistic statistic = new Statistic ();
		UploadStatisticsService.TryToCall (statistic);
	}
	
	private void ServiceResult (SharedObject result, Exception exception)
	{
		Debug.Log ("Resultado servicio: " + ((result == null) ? "Fallo con [" + exception + "]" : result.ToString ()));
	}

	void TestServiceCall ()
	{
		ServiceManager.Instance.NewService ("").Call<SharedObject> (ServiceResult);
	}

	void TestServiceInvalidCall ()
	{
		ServiceManager.Instance.NewService ("").WithURL ("Aasdadsasd//").Call<SharedObject> (ServiceResult);
	}

	void TestSerialization ()
	{
		SharedObject child = new SharedObject ();
		child.Set ("childName", "mateo");

		SharedObject parent = new SharedObject ();
		parent.Set ("parentName", "fernando");
		parent.Set ("child", child);

		var parentDeserialized = SharedObject.Deserialize (parent.Serialize ());

		Debug.Log (parentDeserialized);

		var childDeserialized = parentDeserialized.GetSharedObject ("child");

		Debug.Log (childDeserialized.GetString ("childName"));
	}

	void TestMergeWith ()
	{
		SharedObject a = new SharedObject ();
		SharedObject b = new SharedObject ();
		SharedObject c = new SharedObject ();
		SharedObject d = new SharedObject ();
				
		b.Set ("id", "1");
		b.Set ("b", "1");
		
		d.Set ("id", "2");
		d.Set ("b", "2");
		d.Set ("d", "2");

		a.Set ("so", b);
		c.Set ("so", d);
		
		Debug.Log ("a: " + a);
		Debug.Log ("c: " + c);
		
		a.MergeWith (c);
		
		Debug.Log ("a.MergeWith(c): " + a);

		SharedObject e = new SharedObject ();

		Debug.Log ("e: " + e);

		e.MergeWith (b);
		
		Debug.Log ("b: " + b);
		Debug.Log ("e.MergeWith(b): " + e);
	}

	void TestModelClassSharedObject()
	{
		Statistic statistic = new Statistic();

		Debug.Log("Statistic: " + statistic);

		SharedObject sharedObject = new SharedObject();
		sharedObject.Set("child", statistic);
		
		Debug.Log("SharedObject: " + sharedObject);

		Statistic statisticCopy = sharedObject.GetSharedObject<Statistic>("child");
		statisticCopy.Set("copy" , "true");
		
		Debug.Log("Statistic: " + statistic);
		Debug.Log("StatisticCopy: " + statisticCopy);
	}
}
