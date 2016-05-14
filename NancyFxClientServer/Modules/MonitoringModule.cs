using System.IO;
using System.Text;
using Constants;
using Data;
using Nancy;
using Nancy.Extensions;
using NLog;

namespace Server.Modules
{
    public class MonitoringModule : NancyModule
    {
        private static readonly Logger _logger = LogManager.GetLogger("MonitoringModule");

        public MonitoringModule()
            : base(ApiPath.LocationStatesV1)
        {
            Before += LogRequest;
            After += LogResponse;

            Get["/"] = GetLocationStateContainer;
        }

        private object GetLocationStateContainer(object o)
        {
            var message = new LocationStateContainer
            {
                States = new []
                {
                    new LocationState
                    {
                        Id = 0,
                        MainEntranceClosed = true,
                        WindowClosed = true
                    }, 
                }
            };

            return Response.AsJson(message);
        }

        private Response LogRequest(NancyContext ctx)
        {
            var request = ctx.Request;
            var message = request.Body.AsString();

            _logger.Debug("{0} received : {1}", request.Path, message);
            return null;
        }

        private void LogResponse(NancyContext ctx)
        {
            using (var ms = new MemoryStream())
            {
                ctx.Response.Contents.Invoke(ms);

                var message = Encoding.UTF8.GetString(ms.ToArray());
                var status = (int)ctx.Response.StatusCode + "-" + ctx.Response.StatusCode;

                _logger.Debug("Response sent [{0}] {1}", status, message);
            };
        }
    }
}
