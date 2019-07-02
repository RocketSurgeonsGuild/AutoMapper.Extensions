using Microsoft.Extensions.DependencyInjection;

namespace Rocket.Surgery.Extensions.AutoMapper
{
    /// <summary>
    /// Class AutoMapperOptions.
    /// </summary>
    public class AutoMapperOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether [automatic load profiles].
        /// </summary>
        /// <value><c>true</c> if [automatic load profiles]; otherwise, <c>false</c>.</value>
        public bool AutoLoadFromAssemblies { get; set; } = true;

        /// <summary>
        /// Gets or sets the service lifetime.
        /// </summary>
        /// <value>The service lifetime.</value>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
    }
}
