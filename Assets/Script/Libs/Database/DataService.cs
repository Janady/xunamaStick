using SQLite4Unity3d;
using Mod;
using UnityEngine;
using Libs.Singleton;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService
{
    public static DataService GetInstance()
    {
        return SingletonProvider<DataService>.Instance;
    }
    private SQLiteConnection _connection;
    public DataService()
    {
        init("xuanma.db");
    }
    public DataService(string DatabaseName)
    {
        init(DatabaseName);
    }
    private void init(string DatabaseName)
    {

#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);
    }

    public SQLiteConnection sQLiteConnection() { return _connection; }

    public void CreateDB()
    {
        _connection.DropTable<Goods>();
        _connection.CreateTable<Goods>();

        _connection.InsertAll(new[]{
            new Goods{
                Id = 1,
                Sku = "Tom",
                Title = "Perez",
                Price = 56
            }
        });
    }

    public IEnumerable<Goods> GetPersons()
    {
        return _connection.Table<Goods>();
    }

    public IEnumerable<Goods> GetPersonsNamedRoberto()
    {
        return _connection.Table<Goods>().Where(x => x.Title == "Roberto");
    }

    public Goods GetJohnny()
    {
        return _connection.Table<Goods>().Where(x => x.Title == "Johnny").FirstOrDefault();
    }

    public Goods CreatePerson()
    {
        var p = new Goods
        {
            Sku = "Johnny",
            Title = "Mnemonic",
            Price = 21
        };
        _connection.Insert(p);
        return p;
    }
}