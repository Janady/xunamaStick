using SQLite4Unity3d;
using System.Collections;
using System;

public class Model
{
    private SQLiteConnection connection
    {
        get
        {
            return DataService.GetInstance().sQLiteConnection();
        }
    }
    public void save()
    {
        connection.InsertOrReplace(this.GetType());
    }
}