using System.Collections.Generic;
using UnityEngine;

namespace Libs.Event
{
    /// <summary>
    /// 事件调度者类
    /// </summary>
    public class EventDispatcher : IEventDispatcher
    {
        private Dictionary<string, List<EventDispatcherListener>> mDispatcher = null;
        /// <summary>
        /// 构造方法
        /// </summary>
        public EventDispatcher()
        {
            mDispatcher = new Dictionary<string, List<EventDispatcherListener>>();
        }
        /// <summary>
        /// 销毁对象
        /// </summary>
        public virtual void Dispose()
        {
            if (mDispatcher != null)
                RemoveAllEvent();
            mDispatcher = null;
        }
        /// <summary>
        /// 移除所有事件
        /// </summary>
        public void RemoveAllEvent()
        {
            mDispatcher.Clear();
        }
		/// <summary>
		/// 添加监听事件
		/// </summary>
		/// <param name="eventName">事件名称</param>
		/// <param name="listener">监听方法</param>
		/// <returns>绑定是否成功</returns>
		public virtual bool AddEvent(string eventName, EventDispatcherListener listener)
        {
            List<EventDispatcherListener> list = null;
            if (HasKey(eventName))
            {
                list = mDispatcher[eventName];
            }
            else
            {
                lock (mDispatcher)
                {
                    list = new List<EventDispatcherListener>();
                    mDispatcher.Add(eventName, list);
                }
            }
            if (!list.Contains(listener))
            {
                lock (list)
                {
                    list.Add(listener);
                }
                return true;
            }
            Debug.Log("Have Same listener eventName:" + eventName + " listener :" + listener);
            return false;
        }
        /// <summary>
        /// 移除监听事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="listener">回调函数</param>
        /// <returns>移除是否成功</returns>
        public virtual bool RemoveEvent(string eventName, EventDispatcherListener listener)
        {
            if (HasKey(eventName))
            {
                List<EventDispatcherListener> list = mDispatcher[eventName];
                int ind = list.IndexOf(listener);
                if (ind >= 0)
                {
                    lock (list)
                    {
                        list.RemoveAt(ind);
                        if (list.Count == 0)
                            mDispatcher.Remove(eventName);
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns>是否派发成功</returns>
        public virtual bool DispatchEvent(string eventName)
        {
            return Dispatch(this, eventName, null);
        }
        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="value">参数</param>
        /// <returns>是否派发成功</returns>
        public virtual bool DispatchEvent(string eventName, object value)
        {
            return Dispatch(this, eventName, value);
        }
        /// <summary>
        /// 是否存在此事件名
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns>bool是否存在</returns>
        public virtual bool HasEvent(string eventName)
        {
            return HasKey(eventName);
        }
        /// <summary>
        /// 是否存在此事件名
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="listener">事件回调函数</param>
        /// <returns>是否存在</returns>
        public virtual bool HasEvent(string eventName, EventDispatcherListener listener)
        {
            if (HasKey(eventName))
                return mDispatcher[eventName].Contains(listener);
            return false;
        }

        private bool HasKey(string eventName)
        {
            return mDispatcher.ContainsKey(eventName);
        }
        /// <summary>
        /// 调度事件
        /// </summary>
        /// <param name="dispatcher">调度者</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="value">传递参数值</param>
        /// <returns></returns>
        private bool Dispatch(object dispatcher, string eventName, object value)
        {
            if (HasKey(eventName))
            {
                EventDispatcherListener[] list = mDispatcher[eventName].ToArray();
                int len = list.Length;
                for (int i = 0; i < len; i++)
                    list[i].Invoke(dispatcher, eventName, value);
                list = null;

				if (_unlock_msg_event == eventName)
				{
					DelUnlockMsgEvent();
				}

				return true;
            }
            return false;
        }

		private string _unlock_msg_event = "";
		/// <summary>
		/// 添加监听协议事件
		/// </summary>
		public void AddUnlockMsgEvent(string even)
		{
			_unlock_msg_event = even;
            //UIMgr.instance.OpenUI(UIPath.WaitMsgPanel);
		}

		/// <summary>
		/// 移除监听协议事件
		/// </summary>
		public void DelUnlockMsgEvent()
		{
			_unlock_msg_event = "";
            //UIMgr.instance.CloseUI(UIPath.WaitMsgPanel);
		}

	}
}
