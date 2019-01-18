using UnityEngine;
using UnityEditor;

namespace Libs.Frame
{
    class Frame
    {
        private const byte HEADER = 0xEA;
        private const byte TAILER = 0xFA;
        public static byte[] packet(byte len, byte func, byte cmd, byte[] val)
        {
            byte[] _buf = new byte[len];
            int i = 0;
            _buf[i++] = TAILER;
            _buf[i++] = len;
            _buf[i++] = func;
            _buf[i++] = cmd;
            if (val != null) val.CopyTo(_buf, i);
            _buf[len - 1] = TAILER;
            return _buf;
        }
        public static byte[] packet(byte len, byte func, byte cmd, byte val)
        {
            byte[] _buf = new byte[len];
            int i = 0;
            _buf[i++] = TAILER;
            _buf[i++] = len;
            _buf[i++] = func;
            _buf[i++] = cmd;
            _buf[i++] = val;
            _buf[len - 1] = TAILER;
            return _buf;
        }
        public static byte[] packet(byte func, byte cmd, byte val = 0)
        {
            return packet((byte)0x06, func, cmd, val);
        }
    }
}