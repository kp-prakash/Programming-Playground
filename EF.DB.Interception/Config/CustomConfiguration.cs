namespace EF.DB.Interception.Config
{
    using System.Data.Entity;
    using EF.DB.Interception.Logging;

    internal class CustomConfiguration : DbConfiguration
    {
        public CustomConfiguration()
        {
            AddInterceptor(new SqlSyntaxLogger());
        }
    }
}