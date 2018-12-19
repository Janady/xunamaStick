using SQLite4Unity3d;
using System.Collections;
using System;

public class Model
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    protected static SQLiteConnection connection
    {
        get
        {
            return DataService.GetInstance().sQLiteConnection();
        }
    }
    public int insert()
    {
        return connection.Insert(this);
    }
    public int update()
    {
        return connection.Update(this);
    }
    public int delete()
    {
        return connection.Delete(this);
    }
}