using System.Collections.Generic;

namespace Libs
{
    class HexString
    {
        public static byte Int2Byte(int val)
        {
            if (val >= 0)
            {
                return (byte)(val & 0xff);
            }
            else
            {
                return (byte)(0xff + val + 1);
            }
        }

        public static byte[] Int2Bytes(int val, byte size)
        {
            byte[] res = new byte[size];
            for (int i = size - 1; i > 0; i--)
            {
                res[i] = (byte)(val & 0xff);
                val >>= 8;
            }
            return res;
        }

        public static int IntnByteFromBuf(byte[] buf, int from, int size)
        {
            int res = 0;
            for (int i = 0; i < size; i++)
            {
                res = (res << 8) + buf[from + i];
            }
            return res;
        }

        public static string Bytes2hex(byte[] data)
        {
            string res = "";
            if (null == data)
                return res;

            foreach (byte b in data)
            {
                res += b.ToString("X2");
            }
            return res;
        }

        public static byte[] Hex2bytes(string hexStr)
        {
            int len = ((hexStr.Length + 1) >> 1);
            byte[] res = new byte[len];
            for (int i = 0; i < hexStr.Length; i++)
            {
                char c = hexStr[i];
                if ((i & 0x01) == 0)
                {
                    res[i >> 1] |= (byte)(hexChar2byte(c) << 4);
                }
                else
                {
                    res[i >> 1] |= hexChar2byte(c);
                }
            }
            return res;
        }

        private static byte hexChar2byte(char c)
        {
            byte res = 0x00;
            if (c >= '0' && c <= '9')
            {
                res = (byte)(c - '0');
            }
            else if (c >= 'A' && c <= 'F')
            {
                res = (byte)(10 + c - 'A');
            }
            else if (c >= 'a' && c <= 'f')
            {
                res = (byte)(10 + c - 'a');
            }
            return res;
        }

        public static uint BitCount(uint n)
        {
            uint c = 0; // 计数器
            while (n > 0)
            {
                // 当前位是1
                if ((n & 1) == 1)
                {
                    ++c; // 计数器加1
                }
                n >>= 1; // 移位
            }
            return c;
        }

        public static List<uint> BitIdx(uint n)
        {
            List<uint> index_list = new List<uint>();
            uint i = 0;
            while (n > 0)
            {
                if ((n & 1) == 1)
                {
                    index_list.Add(i + 1);
                }
                i++;
                n >>= 1;
            }
            return index_list;
        }

        public static int Num2Bit(List<uint> num_list)
        {
            int a = 0;
            for (int i = 0; i < num_list.Count; i++)
            {
                int b = 1 << ((int)num_list[i] - 1);
                a += b;
            }
            return a;
        }
    }
}
