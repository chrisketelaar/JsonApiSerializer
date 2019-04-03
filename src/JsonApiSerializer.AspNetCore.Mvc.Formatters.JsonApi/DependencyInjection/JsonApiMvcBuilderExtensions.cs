using JsonApiSerializer.AspNetCore.Mvc.Formatters.JsonApi.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JsonApiMvcBuilderExtensions
    {
        public static IMvcBuilder AddJsonApiOptions(this IMvcBuilder builder, Action<MvcJsonApiOptions> setupAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            builder.Services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcJsonApiMvcOptionsSetup>());

            builder.Services.Configure(setupAction);

            return builder;
        }
    }
}
