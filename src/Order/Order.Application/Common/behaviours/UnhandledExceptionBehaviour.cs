using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Order.Application.Common.behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;
        public UnhandledExceptionBehaviour()
        {
            _logger = Log.ForContext<TRequest>();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }
    }
}
