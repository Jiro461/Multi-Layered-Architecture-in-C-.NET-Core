namespace SOA_BaiTap.CommonLayer.Utilities
{
    public static class ErrorHandler
    {
        public static string GetErrorMessage(Exception ex)
        {
            return ex.Message;
        }
    }

}
