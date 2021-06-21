using System.Net.Http;

namespace PIMTool.Client.WebApiClient
{
    public interface IHttpClientFactory
    {
        HttpClient GetClient();
    }
}
