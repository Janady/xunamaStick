using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Libs.Coroutine;

namespace UI.Widget
{
    public delegate void OnTimeoutCallBack();
    public class CountDown
    {
        public static void countDown(int seconds, Text text, OnTimeoutCallBack callback = null)
        {
            if (text == null)
            {
                CoroutineHandler.Instance().MultiDoCoroutine(run(seconds, text, callback));
            }
            else
            {
                string tag = text.GetHashCode().ToString();
                CoroutineHandler.Instance().DoCoroutine(run(seconds, text, callback), "CountDown", tag);
            }
        }
        private static IEnumerator run(int seconds, Text text, OnTimeoutCallBack callback)
        {
            for (int i=seconds-1; i>=0; i--)
            {
                if (text != null) text.text = i%100/10 + "" + i%10;
                yield return new WaitForSeconds(1f);
            }
            if (null != callback) callback();
        }
        public static void cancel(Text text = null)
        {
            if (text == null) return;
            text.text = "00";
            string tag = text.GetHashCode().ToString();
            CoroutineHandler.Instance().CancelCoroutine("CountDown", tag);
        }
    }
}