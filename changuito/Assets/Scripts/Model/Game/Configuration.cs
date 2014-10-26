public class Configuration : SharedObject
{
	//public static string ServerURL = "http://acomprarconchanguito.appspot.com/ChanguitoServices";
	public static string ServerURL = "http://localhost:80/ChanguitoServices";
	public static string GONDOLAS_PATH = "Prefabs/Gondolas/";
	public static string PRODUCTOS_PATH = "Prefabs/Productos/";
	public static string BOTONESVUELTO_PATH = "Prefabs/BotonesVuelto/";
	public static string BILLETES_PATH = "Prefabs/Billetes/";
	private const string CONFIGURATION_FILE = "configuration.data";
	private static Configuration _current;

	public bool PurchaseModule {
		get{ return GetBool ("purm");}
		set{ Set ("purm", value);}
	}
	
	public bool ChangeControlModule {
		get { return GetBool ("chcom");}
		set{ Set ("chcom", value);}
	}
	
	public int GondolasCount {
		get{ return GetInt ("goncou");}
		set{ Set ("goncou", value);}
	}
	
	public int ModulesCount {
		get { 
			//REVIEW: No haga esto en su casa :P
			return 2 + (PurchaseModule ? 1 : 0) + (ChangeControlModule ? 1 : 0); 
		}
	}

	public Configuration ()
	{
		PurchaseModule = false;
		ChangeControlModule = false;
		GondolasCount = 2;
	}

	public static Configuration Current {
		get {
			if (_current == null) {
				_current = LocalDatabase.LoadFile<Configuration> (CONFIGURATION_FILE);
				if (_current == null) {
					_current = new Configuration ();
				}
			}
			return _current;
		}
	}
	
	public void SaveAsCurrent ()
	{
		if(User.Current != null){
			User.Current.Configuration = this;
		}

		_current = this;
		LocalDatabase.SaveFile (CONFIGURATION_FILE, this);
	}
}