using UnityEngine;
using System.Collections;
using System.IO;

public class AdView : MonoBehaviour
{
    private NavView nav;
    private VedioListView list;
    // Use this for initialization
    void Start()
    {
        nav = transform.FindChild("nav").GetComponent<NavView>();
        nav.setBtn1("导入视频", () =>
        {
            UI.Widget.FileManager.openf(Config.Constant.UsbPath, onLoadFile, "*.flv", "*.mp4", "*.mov", "*.qt", "*.avi");
        });
        list = transform.FindChild("Content").FindChild("VedioList").GetComponent<VedioListView>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void onLoadFile(string filename)
    {
        FileUtil.storeFile(filename, Config.Constant.VedioPath);
        Refresh();
    }
    // Update is called once per frame
    private void Refresh()
    {
        if (list != null) list.Refresh();
    }
}
