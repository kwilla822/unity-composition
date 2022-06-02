using System;
using System.Net.Http;
using System.Text;
using System.Web.Http.ExceptionHandling;

using FiveWattGroup.Contracts.Composition.Common;
using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Entities.CodeFirst.Logging;
using FiveWattGroup.Entities.Enums.Diagnostics.Logging;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.Services.Common.ExceptionHandling
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var dependencyManager = (IUnityManager)context.RequestContext.Configuration.DependencyResolver;

            var logger = dependencyManager.GetService<ICommandOperations<Log>>("LogCommandRepository");

            logger.Create(new CommandRequest<Log>
            {
                Entity =
                        new Log
                        {
                            CreatedOnUtc = DateTimeOffset.UtcNow.UtcDateTime,
                            FullMessage = context.Exception.ToString(),
                            IpAddress = Environment.MachineName,
                            LogLevelId = (int)LogLevelTypeCode.Error,
                            ShortMessage = RequestToString(context.Request),
                        },
                SaveChanges = true
            });
        }

        private static string RequestToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();

            if (request.Method != null)
                message.Append(request.Method);

            if (request.RequestUri != null)
                message.Append(" ").Append(request.RequestUri);

            return message.ToString();
        }
    }
}
