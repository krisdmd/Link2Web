namespace Link2Web.Helpers
{
    public static class GlobalSettings
    {
        public static bool HideCreateProjectDialog { get; set; }
        public static int ProjectId { get; set; }
        public static string ProjectViewId { get; set; }
        public static string GoogleClientId { get; set; }
        public static string GoogleClientSecret { get; set; }
        public static string FacebookClientId { get; set; }
        public static string FacebookSecret { get; set; }

        //Set active when the settings are initialised.
        public static bool Active { get; set; }
    }
}