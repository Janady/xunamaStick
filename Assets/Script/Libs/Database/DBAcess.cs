using UnityEngine;
using Libs.Coroutine;
using System;
using System.Collections;
#if true
using Mono.Data.Sqlite;
using System.IO;
public class DbAccess
{
    public static bool DbAccessState = true;
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader reader;
    public static bool databaseReady = false;

    private static DbAccess _instance = null;
    private static readonly object SynObject = new object(); // sync object

    public static string connectionString = "URI=file:" + Application.persistentDataPath + "/ModulesData.db";

    public static DbAccess Instance
    {
        get
        {
            if (null == _instance)
            {
                lock (SynObject)
                {
                    if (null == _instance)
                    {
                        //cpDbFile ();
                        _instance = new DbAccess(connectionString);
                    }
                }
            }
            return _instance;
        }
    }

    private IEnumerator cpDbFile()
    {
        //将第三方数据库拷贝至Android可找到的地方
        string appDBPath = Application.persistentDataPath + "/ModulesData.db";//（这个目录就是手机的沙盒）;

        if (!File.Exists(appDBPath))
        {
            WWW loadDb = new WWW(Application.streamingAssetsPath + "/ModulesData.db");
            yield return loadDb;
            //拷贝至规定的地方
            File.WriteAllBytes(appDBPath, loadDb.bytes);
            //File.WriteAllBytes(appDBPath.)
            databaseReady = true;
        }
        else
        {
            //如果在手机沙盒中以存在该文件，可以先删除，再写进去（这样的是保证手机沙盒中的文件不是0字符的空文件，反正不这样写就悲催了）
            databaseReady = true;
        }

        //在这里重新得到db对象。
        //DbAccess db = new DbAccess("URI=file:" + appDBPath);
    }

    public DbAccess(string connectionString)
    {
        CoroutineHandler.Instance().DoCoroutine(cpDbFile(), this.GetType().Name, "cpDbFile");
        OpenDB(connectionString);

    }
    public DbAccess()
    {

    }

    public void OpenDB(string connectionString)

    {
        try
        {
            dbConnection = new SqliteConnection(connectionString);

            dbConnection.Open();

            DbAccessState = true;
        }
        catch (Exception e)
        {
            DbAccessState = false;
            string temp1 = e.ToString();
        }

    }

    public void CloseSqlConnection()

    {

        if (dbCommand != null)
        {

            dbCommand.Dispose();

        }

        dbCommand = null;

        if (reader != null)
        {

            reader.Dispose();

        }

        reader = null;

        if (dbConnection != null)
        {

            dbConnection.Close();

        }

        dbConnection = null;

    }

    public SqliteDataReader ExecuteQuery(string sqlQuery)

    {

        dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = sqlQuery;

        reader = dbCommand.ExecuteReader();

        return reader;

    }

    public SqliteDataReader ReadFullTable(string tableName)

    {

        string query = "SELECT * FROM " + tableName;

        return ExecuteQuery(query);

    }

    public SqliteDataReader InsertInto(string tableName, string[] values)

    {

        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];

        for (int i = 1; i < values.Length; ++i)
        {

            query += ", " + values[i];

        }

        query += ")";

        return ExecuteQuery(query);

    }

    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {

        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += ", " + cols[i] + " =" + colsvalues[i];
        }

        query += " WHERE " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }

    public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += " or " + cols[i] + " = " + colsvalues[i];
        }
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)

    {

        if (cols.Length != values.Length)
        {

            throw new SqliteException("columns.Length != values.Length");

        }

        string query = "INSERT INTO " + tableName + "(" + cols[0];

        for (int i = 1; i < cols.Length; ++i)
        {

            query += ", " + cols[i];

        }

        query += ") VALUES (" + values[0];

        for (int i = 1; i < values.Length; ++i)
        {

            query += ", " + values[i];

        }

        query += ")";

        return ExecuteQuery(query);

    }

    public SqliteDataReader DeleteContents(string tableName)

    {

        string query = "DELETE FROM " + tableName;

        return ExecuteQuery(query);

    }

    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {

        if (col.Length != colType.Length)
        {

            throw new SqliteException("columns.Length != colType.Length");

        }
        //drop table if exist
        //string queryTableCheck = "drop table if exists "+name;
        //ExecuteQuery (queryTableCheck);

        //create table
        string query = "CREATE TABLE IF NOT EXISTS " + name + " (" + col[0] + " " + colType[0];
        for (int i = 1; i < col.Length; ++i)
        {

            query += ", " + col[i] + " " + colType[i];

        }
        query += ")";

        return ExecuteQuery(query);

    }

    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {

        if (col.Length != operation.Length || operation.Length != values.Length)
        {

            throw new SqliteException("col.Length != operation.Length != values.Length");

        }

        string query = "SELECT " + items[0];

        for (int i = 1; i < items.Length; ++i)
        {

            query += ", " + items[i];

        }

        query += " FROM " + tableName;
        if (col.Length > 0)
        {
            query += " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
        }
        for (int i = 1; i < col.Length; ++i)
        {

            query += " AND " + col[i] + operation[i] + "'" + values[i] + "' ";

        }
        Debug.Log("query:" + query);
        return ExecuteQuery(query);

    }

}
#endif