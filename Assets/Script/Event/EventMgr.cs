using System;

namespace Libs.Event
{
    public class EventMgr : EventDispatcher
    {
        public EventMgr()
        {

        }

        private static EventMgr m_Instance = null;
        public static EventMgr Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new EventMgr();

                return m_Instance;
            }
        }
    }
}
