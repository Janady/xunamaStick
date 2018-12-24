namespace Libs.Api
{
    class SerailApi
    {
        public static void check()
        {
            AndroidApi.CallAndroidFunc("check");
        }

		public static void serialTransfer(byte[] buf)
		{
			AndroidApi.CallAndroidFunc("serialTransfer", buf);
		}
    }
}
