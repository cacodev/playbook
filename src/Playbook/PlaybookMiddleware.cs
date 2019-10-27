using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace Playbook
{
    public class PlaybookMiddleware
    {
        private const string _defaultPlaybookRoute = "/playbook";
        private readonly RequestDelegate _next;
        public PlaybookMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IPlaybookService playbookService)
        {
            if (!RequestingPlaybook(httpContext.Request))
            {
                await _next(httpContext);
                return;
            }

            var response = httpContext.Response;

            response.StatusCode = 200;
            response.ContentType = "application/json;charset=utf-8";

            var json = JsonConvert.SerializeObject(playbookService.GetPlaybook());

            await response.WriteAsync(json, new UTF8Encoding(false));
        }

        public bool RequestingPlaybook(HttpRequest request)
        {
            if (request.Method != "GET") return false;

            return request.Path.Value == _defaultPlaybookRoute;
        }
    }
}