using static Jwt.Infrastructure.Configuations.AppSettingDetail;

namespace Jwt.Infrastructure.Configuations
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public BasicAuth BasicAuth { get; set; }
        public JwtAuth JwtAuth { get; set; }
        public int TokenLife { get; set; }
    }

    public class AppSettingDetail
    {
        public class BasicAuth
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public class JwtAuth
        {
            public string SecretKey { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string Subject { get; set; }

        }
      
    }
}
