namespace EmployeeInformation
{
    /// <summary>
    /// Application settings configuration.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Client secret.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// File path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Name of file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Name of users file.
        /// </summary>
        public string UsersFile { get; set; }
    }
}
