using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {

        private Stopwatch _stopwatch = new Stopwatch();
        private readonly ILogger<RequestTimeMiddleware> _logger;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var elapsed = _stopwatch.ElapsedMilliseconds;

            if(elapsed / 1000 > 4)
            {
                var message = $"Request[{context.Request.Method} at {context.Request.Path} took {elapsed} ms";
                _logger.LogInformation(message);
            }

        }
    }
}
