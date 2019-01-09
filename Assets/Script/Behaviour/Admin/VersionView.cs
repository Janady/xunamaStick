using UnityEngine;
using UnityEngine.UI;

public class VersionView : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Text versionText = transform.FindChild("Content").FindChild("version").GetComponent<Text>();
        versionText.text = Config.Constant.Version;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
