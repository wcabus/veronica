using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Veronica.Backend.Application.Infrastructure
{
    public class QueryPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
        private readonly ILogger _logger;

        public QueryPipeline(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(GetType());
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"{typeof(TRequest).Name} query triggered, request data: {JsonConvert.SerializeObject(request)}");

            return await next().ConfigureAwait(true);
        }
    }
}