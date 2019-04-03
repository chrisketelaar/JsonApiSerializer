using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Buffers;

namespace JsonApiSerializer.AspNetCore.Mvc.Formatters.JsonApi.DependencyInjection
{
    internal class MvcJsonApiMvcOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly MvcJsonApiOptions _jsonApiOptions;
        private readonly ArrayPool<char> _charPool;
        private readonly ObjectPoolProvider _objectPoolProvider;

        public MvcJsonApiMvcOptionsSetup(
            ILoggerFactory loggerFactory,
            IOptions<MvcJsonApiOptions> jsonApiOptions,
            ArrayPool<char> charPool,
            ObjectPoolProvider objectPoolProvider)
        {
            _loggerFactory = loggerFactory;
            _jsonApiOptions = jsonApiOptions.Value;
            _charPool = charPool;
            _objectPoolProvider = objectPoolProvider;
        }

        public void Configure(MvcOptions options)
        {
            var jsonApiOutputFormatter =
                new JsonApiOutputFormatter(_jsonApiOptions.SerializerSettings, _charPool);

            var jsonApiInputFormatter = new JsonApiInputFormatter(
                _loggerFactory.CreateLogger<JsonApiInputFormatter>(),
                _jsonApiOptions.SerializerSettings,
                _charPool,
                _objectPoolProvider,
                options,
                new MvcJsonOptions()
                {
                    AllowInputFormatterExceptionMessages =
                    _jsonApiOptions.AllowInputFormatterExceptionMessages
                }
            );

            options.InputFormatters.Add(jsonApiInputFormatter);
            options.OutputFormatters.Add(jsonApiOutputFormatter);

            options.FormatterMappings.SetMediaTypeMappingForFormat("vnd.api+json", MediaTypeHeaderValue.Parse("application/vnd.api+json"));
        }
    }
}
