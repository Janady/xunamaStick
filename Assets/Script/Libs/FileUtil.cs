using System.Collections.Generic;
using System.IO;

public class FileUtil
{
    public static FileInfo[] getVedios(string directory)
    {
        List<FileInfo> list = new List<FileInfo>();
        if (Directory.Exists(directory))
        {
            string[] vedioPattern = { "*.flv", "*.mp4", "*.mov", "*.qt", "*.avi" };
            DirectoryInfo direction = new DirectoryInfo(directory);
            foreach (string pattern in vedioPattern)
            {
                FileInfo[] files = direction.GetFiles(pattern, SearchOption.AllDirectories);
                foreach (FileInfo fi in files)
                {
                    list.Add(fi);
                }
            }
        }
        return list.ToArray();
    }
}