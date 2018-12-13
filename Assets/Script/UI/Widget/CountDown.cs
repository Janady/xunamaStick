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
            CoroutineHandler.Instance().DoCoroutine(run(seconds, text, callback), "CountDown", text.GetHashCode().ToString());
        }
        private static IEnumerator run(int seconds, Text text, OnTimeoutCallBack callback)
        {
            for (int i=seconds-1; i>=0; i--)
            {
                text.text = i%100/10 + "" + i%10;
                yield return new WaitForSeconds(1f);
            }
            if (null != callback) callback();
        }
        public static void cancel(Text text)
        {
            text.text = "00";
            CoroutineHandler.Instance().CancelCoroutine("CountDown", text.GetHashCode().ToString());
        }
    }
}