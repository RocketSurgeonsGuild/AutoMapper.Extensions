using AutoMapper;
using AutoMapper.Features;
using AutoMapper.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Rocket.Surgery.Conventions.AutoMapper;

/// <summary>
/// Class AutoMapperOptions.
/// </summary>
public class AutoMapperOptions
{
    /// <summary>
    /// Gets or sets the service lifetime.
    /// </summary>
    /// <value>The service lifetime.</value>
    public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
}
