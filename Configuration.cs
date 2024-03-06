namespace Blog;

public static class Configuration
{
    public static string JwtKey = "ZWMzOWRhNjYtYWE1Yy00NTRiLTk5ZWUtOWE1MzkyM2JkMzI1";

    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api_IltmsdD43da/sZ=";

    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    } // classe aninhada, classe de config nao tem mt problema
}