using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace alxbrn_api.HttpActionResults
{
    public class ErrorContentResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly string _content;
        private readonly string _mediaType;

        public ErrorContentResult(string content, string mediaType, HttpRequestMessage request)
        {
            _content = content;
            _request = request;
            _mediaType = mediaType;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(_content, Encoding.UTF8, _mediaType),
                RequestMessage = _request
            });
        }
    }
}