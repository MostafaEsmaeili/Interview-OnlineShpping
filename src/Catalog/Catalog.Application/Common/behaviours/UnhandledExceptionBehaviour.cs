using Catalog.Application.Common.exceptions;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Catalog.Application.Common.behaviours
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
            catch (Exception ex) when (ex.GetType() != typeof(CatalogValidationException) && ex.GetType() != typeof(CatalogApiExceptions))
            {
                var requestName = typeof(TRequest).FullName;
                _logger.Error(" Error In :{CommandName} with Message : {Message} In Line {Line}", requestName, ex.Message, GetLineNumber(ex), ex.ToString());

                throw;
            }
        }
        public int GetLineNumber(Exception ex)
        {
            int line = Convert.ToInt32(ex.ToString()[ex.ToString()
                                                       .IndexOf("line")..].Substring(0, ex.ToString()[ex.ToString()
                                                       .IndexOf("line")..].ToString()
                                                       .IndexOf("\r\n")).Replace("line ", ""));

            return line;
        }
    }
}
