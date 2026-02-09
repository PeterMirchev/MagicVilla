using Funq;
using MagicVillaAPI.Services.ServiceStack;
using MagicVillaAPI.Services.ServiceStack.User.Services;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Model;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Repository;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;
namespace MagicVillaAPI
{
    public class AppHost : AppHostBase
    {

        public AppHost() : base("Magic Villa App", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            this.Config.HandlerFactoryPath = "api/v2";

            JsConfig.DateHandler = DateHandler.ISO8601;

            var dbFactory = new OrmLiteConnectionFactory(
                "Host=localhost;Port=5433;Username=postgres;Password=root;Database=villa_lite",
                PostgreSqlDialect.Provider
            );

            container.Register<IDbConnectionFactory>(dbFactory);
            container.Register<IUserLiteRepository>(c => new UserLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<UserServiceLayer>(c => new UserServiceLayer(c.Resolve<IUserLiteRepository>()));

            // Optional: Create tables on startup
            using var db = dbFactory.Open();
            db.CreateTableIfNotExists<UserLite>();
        }
    }
}
