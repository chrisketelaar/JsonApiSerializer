using Microsoft.AspNetCore.Mvc.Formatters;
using System.Buffers;

namespace JsonApiSerializer.AspNetCore.Mvc.Formatters.JsonApi
{
    public class JsonApiOutputFormatter : JsonOutputFormatter
    {
        public JsonApiOutputFormatter(JsonApiSerializerSettings serializerSettings, ArrayPool<char> charPool)
            : base(serializerSettings, charPool)
        {
            SupportedMediaTypes.Clear();
            SupportedEncodings.Clear();

            SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/vnd.api+json"));
            SupportedEncodings.Add(System.Text.Encoding.UTF8);
            SupportedEncodings.Add(System.Text.Encoding.Unicode);
        }
    }
}
