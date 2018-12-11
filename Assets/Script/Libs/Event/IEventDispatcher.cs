using System;

namespace Libs.Event
{
    public delegate void EventDispatcherListener(object dispatcher, string eventName, object value);
    /// <summary>
    /// 事件接口
    /// </summary>
    public interface IEventDispatcher
    {
        /// <summary>
        /// 绑定事件
        /// </summary>
        /// <param name="name">事件类型(int类型)</param>
        /// <param name="listener">回调函数</param>
        /// <returns>绑定是否成功</returns>
        bool AddEvent(string eventName, EventDispatcherListener listener);
        /// <summary>
        /// 卸载绑定事件
        /// </summary>
        /// <param name="eventName">事件类型</param>
        /// <param name="listener">回调函数</param>
        /// <returns>卸载绑定是否成功</returns>
        bool RemoveEvent(string eventName, EventDispatcherListener listener);
        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="value">参数</param>
        /// <returns>是否派发成功</returns>
        bool DispatchEvent(string eventName, object value);
        /// <summary>
        /// 是否存在此事件名
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns>bool</returns>
        bool HasEvent(string eventName);
    }
}
