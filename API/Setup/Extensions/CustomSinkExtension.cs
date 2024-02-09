using Serilog.Configuration;
using Serilog;
using MediatR;
using Esprima.Ast;

namespace API.Setup.Extensions
{
    public static class CustomSinkExtension 
    {
      // private readonly IMediator _mediator;

        public static LoggerConfiguration CustomSink(this LoggerSinkConfiguration loggerConfiguration,IFormatProvider fmtProvider = null )
            {
                return loggerConfiguration.Sink(new CustomSink(fmtProvider ));
            }        
    }
}
