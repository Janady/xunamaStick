using System.Collections.Generic;
using System.IO;

public class FileUtil
{
    public static FileInfo[] getVedios(string directory)
    {
        return getFiles(directory, "*.flv", "*.mp4", "*.mov", "*.qt", "*.avi");
    }
    public static FileInfo[] getExcel(string directory)
    {
        return getFiles(directory, "*.json");
    }
    private static FileInfo[] getFiles(string directory, params string[] pattern)
    {
        List<FileInfo> list = new List<FileInfo>();
        if (Directory.Exists(directory))
        {
            DirectoryInfo direction = new DirectoryInfo(directory);
            foreach (string ptn in pattern)
            {
                FileInfo[] files = direction.GetFiles(ptn, SearchOption.AllDirectories);
                foreach (FileInfo fi in files)
                {
                    list.Add(fi);
                }
            }
        }
        return list.ToArray();
    }
}