namespace Config
{
    public class Constant
    {
        public const float HEART_BEAT = 10f;
        public const string VedioPath = "/sdcard/vedioXuanma";
        public const string ImagePath = "/sdcard/Image";
        public const string ExcelPath = "/sdcard/excel";
#if UNITY_EDITOR
        public const string UsbPath = "/";
#else
        public const string UsbPath = "/storage/usbhost1";
#endif
        public const string Version = "1.0.0";
    }
}
