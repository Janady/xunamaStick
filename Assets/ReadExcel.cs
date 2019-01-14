using UnityEngine;
using System.IO;
using Mod;
using SimpleJSON;

public class ReadExcel : MonoBehaviour
{


    public string ExcelPathName;

    void Start()
    {
        GameReadExcel(ExcelPathName);
    }

    /// <summary>
    /// 只读Excel方法
    /// </summary>
    /// <param name="ExcelPath"></param>
    /// <returns></returns>
    public static void GameReadExcel(string ExcelPath)
    {
#if UNITY_ANDROID
        if (!Directory.Exists(Config.Constant.ExcelPath))
        {
            Debug.Log(Directory.CreateDirectory(Config.Constant.ExcelPath));
        }
        foreach (FileInfo fi in FileUtil.getExcel(Config.Constant.ExcelPath))
        {
            Debug.Log(fi.FullName);

            StreamReader stream = fi.OpenText();
            string jsonstr = stream.ReadToEnd();
            Debug.Log(jsonstr);
            JSONNode N = JSON.Parse(jsonstr);
            foreach (JSONNode goodsJson in N["goods"])
            {
                Debug.Log(goodsJson["name"].ToString());
                Debug.Log((int)goodsJson["price"]);
            }
            /*
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                string[] tmpDataStr = System.Text.RegularExpressions.Regex.Split(line, @"\s+");
                foreach (string str in tmpDataStr)
                {
                    Debug.Log(str);
                }
            }
            // IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            /*
            DataSet result = excelReader.AsDataSet();

            int columns = result.Tables[0].Columns.Count;//获取列数
            int rows = result.Tables[0].Rows.Count;//获取行数


            //从第二行开始读
            for (int i = 1; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    string nvalue = result.Tables[0].Rows[i][j].ToString();
                    Debug.Log(nvalue);
                }
            }
            */
            stream.Close();
            stream.Dispose();
        }
#else
        var path = string.Format(@"Assets/StreamingAssets/{0}", ExcelPath);
#endif
        // FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
        

    }
}