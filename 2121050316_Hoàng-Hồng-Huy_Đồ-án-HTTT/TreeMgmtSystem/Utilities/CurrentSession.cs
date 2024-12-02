namespace TreeMgmtSystem.Utilities
{
    public static class CurrentSession
    {
        public static int UserId { get; set; }
        public static string UserName { get; set; }

        // Phương thức đặt lại phiên
        public static void ResetSession()
        {
            UserId = 0;
            UserName = string.Empty;
            
        }
    }
}
