using UnityEngine;

namespace Libs.Api
{
    class AndroidApi
    {
        public static void CallAndroidFunc(string funcStr, params object[] args)
        {
            Debug.Log("CallAndroidFunc - "+funcStr);
#if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(funcStr, args);
                }
            }
#endif
        }
        public static string CallAndroidFuncString(string funcStr, params object[] args)
        {
            Debug.Log("CallAndroidFuncString");
#if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    return jo.Call<string>(funcStr, args);
                }
            }
#else
            return null;
#endif
        }
    }
}
