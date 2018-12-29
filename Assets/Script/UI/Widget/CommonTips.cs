using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UI;

namespace UI.Widget
{
    public delegate void OnButtonCallBack();

    public enum TipsType
    {
        OK,
        OK_CANCEL,
        AUTO_CLOSE
    };

    public class CommonTips : MonoBehaviour
    {
        public OnButtonCallBack m_okCallBack;
        public OnButtonCallBack m_cancelCallBack;
        public OnButtonCallBack m_closeCallBack = null;

        public float m_closeTime = 0;

        public Text tips_text;

        public Button btn_ok;
        public Button btn_cancel;
        public Button btn_close;

        void Awake()
        {
            EventTriggerListener.Get(btn_ok.gameObject).onClick = OnOKClick;
            EventTriggerListener.Get(btn_cancel.gameObject).onClick = OnCancelClick;
            //EventTriggerListener.Get(btn_close.gameObject).onClick = OnCloseClick;
        }
        public static void showDelete(string title, UnityEngine.Events.UnityAction callback)
        {
            GameObject go = Libs.Resource.UIManager.OpenUI(Config.UI.UIPath.DeletePanel);
            go.transform.FindChild("bg").FindChild("content").GetComponent<Text>().text = title;
            Button btn = go.transform.FindChild("bg").FindChild("confirm").GetComponent<Button>();
            btn.onClick.AddListener(()=>
            {
                callback();
                Destroy(go);
            });
        }
        public static GameObject OpenTips(TipsType type, string title, OnButtonCallBack okCallBack = null, OnButtonCallBack cancelCallBack = null, OnButtonCallBack closeCallBack = null, float delayTime = 0)
        {
            GameObject go = Libs.Resource.UIManager.OpenUI(Config.UI.UIPath.TipsPanel);
            CommonTips common_tips = go.GetComponent<CommonTips>();

            if (type == TipsType.OK_CANCEL)
            {
                common_tips.btn_ok.gameObject.SetActive(true);
                common_tips.btn_cancel.gameObject.SetActive(true);

                common_tips.btn_ok.GetComponent<RectTransform>().localPosition = new Vector3(150, -100, 0);
                common_tips.btn_cancel.GetComponent<RectTransform>().localPosition = new Vector3(-150, -100, 0);
            }
            else if (type == TipsType.OK)
            {
                common_tips.btn_cancel.gameObject.SetActive(false);
                common_tips.btn_ok.gameObject.SetActive(true);

                common_tips.btn_ok.GetComponent<RectTransform>().localPosition = new Vector3(0, -100, 0);
            }
            else if (type == TipsType.AUTO_CLOSE)
            {
                common_tips.btn_cancel.gameObject.SetActive(false);
                common_tips.btn_ok.gameObject.SetActive(false);

                if (delayTime > 0)
                {
                    common_tips.m_closeTime = Time.time + delayTime;
                }
            }

            common_tips.m_okCallBack = okCallBack;
            common_tips.m_cancelCallBack = cancelCallBack;
            common_tips.m_closeCallBack = closeCallBack;

            common_tips.tips_text.text = title;

            return go;
        }

        private void OnOKClick(GameObject go)
        {
            if (null != m_okCallBack)
            {
                m_okCallBack();
            }

            DestroyImmediate(gameObject);
        }

        private void OnCancelClick(GameObject go)
        {
            if (null != m_cancelCallBack)
            {
                m_cancelCallBack();
            }

            DestroyImmediate(gameObject);
        }

        private void OnCloseClick(GameObject go)
        {
            if (null != m_closeCallBack)
            {
                m_closeCallBack();
            }

            DestroyImmediate(gameObject);
        }

        void Update()
        {
            if (m_closeTime > 0 && Time.time >= m_closeTime)
            {
                OnCloseClick(gameObject);
            }
        }

        public void SetOkText(string str)
        {
            if (str != "")
            {
                btn_ok.transform.Find("Text").GetComponent<Text>().text = str;
            }
        }

        public void SetCancelText(string str)
        {
            if (str != "")
            {
                btn_cancel.transform.Find("Text").GetComponent<Text>().text = str;
            }
        }

        public void SetTipsText(string str)
        {
            if (str != "")
            {
                tips_text.text = str;
            }
        }
    }
}