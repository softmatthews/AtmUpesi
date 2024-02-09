using Application.Core.License;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Behaviors
{
    public class LicenseCheckBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            LicenseInfo? info = LicenseCache.Instance.GetLicenseInfo() ?? throw new Exception("Missing license information");
            //if (request is CreateISO.Command cmd)
            //{
            //    IFormFile file = cmd.File;
            //}
            return await next();
        }
    }
}
