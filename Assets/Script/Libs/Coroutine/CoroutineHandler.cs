using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace Libs.Coroutine
{

    public class CoroutineHandler : MonoBehaviour
    {
        private static CoroutineHandler _instance = null;

        public delegate IEnumerator VoidDelegate();
        VoidDelegate processOpr;
        private Hashtable coroutineTb = new Hashtable();

        private IEnumerator curCoroutine;
        void Awake()
        {
            _instance = this;
        }

        public static CoroutineHandler Instance()
        {
            return _instance;
        }


        void Start()
        {

        }
        /*
        *开启单例协程，支持关闭，只支持当前情况下只存在一个同名协程
        */
        public void DoCoroutine(IEnumerator proOpr, string className, string proName)
        {
            //coroutine = ProcessOpr;
            string Keyname = className + proName;

            CancelCoroutine(className, proName);//每次先关闭上次的同名计时器，保证同名计时器当前情况下只有一个在运行

            coroutineTb.Add(Keyname, proOpr);
            StartCoroutine(proOpr);
        }

        /*
        *开启单例协程，支持当前情况下存在多个同名协程
        */
        public void MultiDoCoroutine(IEnumerator proOpr)
        {
            StartCoroutine(proOpr);
        }

        /*
        *关闭单例协程
        */
        public void CancelCoroutine(string className, string proName)
        {
            //coroutine = ProcessOpr;
            string Keyname = className + proName;
            if (coroutineTb.Contains(Keyname))
            {
                // Debuger.LogSkill("close last samename timer");
                curCoroutine = (IEnumerator)coroutineTb[Keyname];
                StopCoroutine(curCoroutine);
                coroutineTb.Remove(Keyname);
            }
        }
    }
}
