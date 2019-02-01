﻿using Insql.Providers;
using Insql.Providers.Embedded;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Insql
{
    public static class EmbeddedInsqlBuilderExtensions
    {
        public static IInsqlBuilder AddEmbeddedXml(this IInsqlBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IInsqlDescriptorProvider, EmbeddedDescriptorProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<EmbeddedDescriptorOptions>, EmbeddedDescriptorOptionsSetup>());

            return builder;
        }

        public static IInsqlBuilder AddEmbeddedXml(this IInsqlBuilder builder, Action<EmbeddedDescriptorOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddEmbeddedXml();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
