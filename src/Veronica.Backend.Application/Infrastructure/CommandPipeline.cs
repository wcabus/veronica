using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Veronica.Backend.Domain;

namespace Veronica.Backend.Application.Infrastructure
{
    public class CommandPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public CommandPipeline(IEnumerable<IValidator<TRequest>> validators, IUnitOfWork unitOfWork, ILoggerFactory logger)
        {
            _validators = validators;
            _unitOfWork = unitOfWork;
            _logger = logger.CreateLogger(GetType());
        }
        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"{typeof(TRequest).Name} command triggered, request data: {JsonConvert.SerializeObject(request)}");

            // Pre-execution: validate the request
            if (_validators != null)
            {
                var context = new ValidationContext(request);
                var failures = _validators
                    .Select(x => x.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(x => x != null)
                    .ToList();

                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
            }
            
            // Execute the request
            var response = await next().ConfigureAwait(false);

            _logger.LogDebug($"Command response: {JsonConvert.SerializeObject(response)}");

            // Post-execution: commit
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            _logger.LogInformation($"Command successfully committed.");

            return response;
        }
    }
}
