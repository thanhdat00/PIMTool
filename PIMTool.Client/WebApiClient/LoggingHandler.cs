using log4net;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PIMTool.Client.WebApiClient
{
    public class LoggingHandler : DelegatingHandler
    {
        private static readonly ILog ClassTracer = LogManager.GetLogger("{PIMToolClient");
 
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            ClassTracer.Debug($"Request: {request}");
            if (request.Content != null)
            {
                ClassTracer.Debug($"Request-Content: {await request.Content.ReadAsStringAsync()}");
            }
            try
            {
                // base.SendAsync calls the inner handler
                var response = await base.SendAsync(request, cancellationToken);
                ClassTracer.Debug($"Response: {response}");
                if (response.Content != null)
                {
                    ClassTracer.Debug($"Response-Content: {await response.Content.ReadAsStringAsync()}");
                }
                return response;
            }
            catch (Exception ex)
            {
                ClassTracer.Error($"Failed to get response: {ex}", ex);
                throw;
            }
        }
    }
}
