using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mod;

public class DeviceView : MonoBehaviour
{
    private Slider slider1;
    private Slider slider2;
    private Text text1;
    private Text text2;
    private Text text3;
    private Text text4;
    private Button btn;
    private Button btn3;
    private Button btn4;
    private bool inited = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(initNav());
        initContent();
    }
    private void OnEnable()
    {
        loadView();
    }
    private void initContent()
    {
        Transform tr = transform.FindChild("Content").FindChild("ContainerContent").FindChild("ScrollPanel");
        slider1 = tr.FindChild("Slider1").GetComponent<Slider>();
        text1 = tr.FindChild("value1").GetComponent<Text>();
        slider1.onValueChanged.AddListener((float value) => {
            text1.text = (int)value + "";
            Game game = Game.get();
            game.price = (int)value;
            game.update();
        });
        slider2 = tr.FindChild("Slider2").GetComponent<Slider>();
        text2 = tr.FindChild("value2").GetComponent<Text>();
        slider2.onValueChanged.AddListener((float value) => {
            text2.text = (int)value + "%";
            Game game = Game.get();
            game.ratio = (int)value;
            game.update();
        });
        text3 = tr.FindChild("value3").GetComponent<Text>();
        btn3 = tr.FindChild("Button3").GetComponent<Button>();
        btn3.onClick.AddListener(()=> {
            text3.text = "0";
            Game game = Game.get();
            game.lucky = 0;
            game.update();
        });
        text4 = tr.FindChild("value4").GetComponent<Text>();
        btn4 = tr.FindChild("Button4").GetComponent<Button>();
        btn4.onClick.AddListener(() => {
            text4.text = "0";
            Game game = Game.get();
            game.offset = 0;
            game.update();
        });
        btn = tr.FindChild("Button").GetComponent<Button>();
        btn.onClick.AddListener(() => {
            Game.clear();
            loadView();
        });
        inited = true;
        loadView();
    }
    private void loadView()
    {
        if (!inited) return;
        Game game = Game.get();
        text1.text = game.price + "";
        slider1.value = game.price;
        slider2.value = game.ratio;
        text2.text = game.ratio + "%";
        text3.text = game.lucky.ToString();
        text4.text = game.offset.ToString();
    }

    IEnumerator initNav()
    {
        NavView nv = transform.FindChild("nav").GetComponent<NavView>();
        nv.Title = "设备管理";
        nv.setBtn1("一键开锁", () => {
            Service.LockingPlateService.Instance().OpenAll();
        });
        nv.setBtn2("一键补货", () => {
            Service.GoodsService.Instance().Replenishment();
        });
        yield return new WaitForEndOfFrame();
    }
}
