using System.Net.Http;

namespace PIMTool.Client.WebApiClient
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClientFactory()
        {
        }

        public HttpClient GetClient()
        {
            HttpMessageHandler pipeline = new HttpClientHandler();

            // Add logging handler to the beginning of the pipeline.
            return new HttpClient(new LoggingHandler
            {
                InnerHandler = pipeline
            });
        }
    }
}
