using System.Net;
using System.Runtime.Serialization;
using Funq;
using MagicVillaAPI.Services.ServiceStack.User.Services;
using MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.DiscountRequests;
using MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.RecommendationRequests;
using MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.SupportRequests;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Model;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Repository;
using MagicVillaAPI.ServiceStack.ServiceStack.Utils;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using ServiceStack.Validation;
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

            // GLOBAL EXCEPTION → HTTP STATUS + MESSAGE
            ServiceExceptionHandlers.Add((req, dto, ex) =>
            {
                if (ex is KeyNotFoundException)
                {
                    var httpError = new HttpError(HttpStatusCode.NotFound, ex.Message);
                    httpError.Response = new { message = ex.Message };
                    return httpError;
                }

                if (ex is ArgumentException)
                {
                    var httpError = new HttpError(HttpStatusCode.BadRequest, ex.Message);
                    httpError.Response = new { message = ex.Message };
                    return httpError;
                }

                if (ex is SerializationException && ex.Message.Contains("Unable to bind to request"))
                {
                    var httpError = new HttpError(HttpStatusCode.BadRequest, "Invalid request data")
                    {
                        Response = new { message = "Invalid request data" }
                    };
                    return httpError;
                }

                return null;
            });

            // GLOBAL EXCEPTION → HTTP STATUS + MESSAGE (binding-level)
            this.UncaughtExceptionHandlers.Add((req, res, operationName, ex) =>
            {
                if (ex is SerializationException && ex.Message.Contains("Unable to bind to request"))
                {
                    var httpError = new HttpError(HttpStatusCode.BadRequest, "Invalid request data")
                    {
                        Response = new
                        {
                            message = "Invalid request data",
                            request = req.GetRawBody() // get the original raw request body if available
                        }
                    };

                    res.StatusCode = (int)httpError.StatusCode;
                    res.EndRequest();
                }
            });

            JsConfig.DateHandler = DateHandler.ISO8601;

            var dbFactory = new OrmLiteConnectionFactory(
                "Host=localhost;Port=5433;Username=postgres;Password=root;Database=villa_lite",
                PostgreSqlDialect.Provider
            );

            Plugins.Add(new ValidationFeature());

            container.RegisterValidators(typeof(UsersStackGetByEmailValidator).Assembly);
            container.RegisterValidators(typeof(UserStackRequestValidator).Assembly);

            container.Register<IDbConnectionFactory>(dbFactory);

            container.Register<IUserLiteRepository>(c => new UserLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<UserServiceLayer>(c => new UserServiceLayer(c.Resolve<IUserLiteRepository>()));

            // Optional: Create tables on startup
            using var db = dbFactory.Open();
            db.CreateTableIfNotExists<UserLite>();
            db.CreateTableIfNotExists<DiscountRequest>();
            db.CreateTableIfNotExists<SupportRequest>();
            db.CreateTableIfNotExists<RecommendationRequest>();

            if (!db.ColumnExists<UserLite>(x => x.Role))
            {
                db.AddColumn<UserLite>(x => x.Role);
            }   
        }
    }
}
