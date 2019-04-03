using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using System.Buffers;

namespace JsonApiSerializer.AspNetCore.Mvc.Formatters.JsonApi
{
    public class JsonApiInputFormatter : JsonInputFormatter
    {
        public JsonApiInputFormatter(ILogger logger, JsonApiSerializerSettings serializerSettings, ArrayPool<char> charPool, ObjectPoolProvider objectPoolProvider, MvcOptions options, MvcJsonOptions jsonOptions)
            : base(logger, serializerSettings, charPool, objectPoolProvider, options, jsonOptions)
        {
            SupportedMediaTypes.Clear();
            SupportedEncodings.Clear();

            SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/vnd.api+json"));
            SupportedEncodings.Add(System.Text.Encoding.UTF8);
            SupportedEncodings.Add(System.Text.Encoding.Unicode);
        }
    }
}
