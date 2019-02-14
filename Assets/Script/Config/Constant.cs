namespace Config
{
    public class Constant
    {
        public const float HEART_BEAT = 10f;
        public const string ExcelPath = "/sdcard/excel";
#if UNITY_EDITOR
        public const string UsbPath = "/Users/janady";
        public const string VedioPath = "/Users/janady";
        public const string ImagePath = "/Users/janady";
#else
        public const string UsbPath = "/storage/usbhost1";
        public const string VedioPath = "/sdcard/vedioXuanma";
        public const string ImagePath = "/sdcard/Image";
#endif
        public const string Version = "1.0.0";
    }
}
