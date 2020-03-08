using AutoMapper;
using AutoMapper.Features;
using Microsoft.Extensions.DependencyInjection;

namespace Rocket.Surgery.Extensions.AutoMapper
{
    /// <summary>
    /// Class AutoMapperOptions.
    /// </summary>
    public class AutoMapperOptions : IRuntimeFeature, IGlobalFeature
    {
        /// <summary>
        /// Gets or sets the service lifetime.
        /// </summary>
        /// <value>The service lifetime.</value>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
        // /// <summary>
        // /// The default value for json operations
        // /// </summary>
        // public JsonDefaultValue JsonDefaultValue { get; set; } = JsonDefaultValue.NotNull;

        void IRuntimeFeature.Seal(IConfigurationProvider configurationProvider) { }

        void IGlobalFeature.Configure(IConfigurationProvider configurationProvider)
        {
            configurationProvider.Features.Set(this);
        }
    }
}