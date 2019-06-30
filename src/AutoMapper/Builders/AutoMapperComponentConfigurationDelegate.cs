using System;
using AutoMapper;

namespace Rocket.Surgery.Extensions.AutoMapper.Builders
{
    /// <summary>
    /// Delegate MartenComponentConfigurationDelegate
    /// </summary>
    /// <param name="serviceProvider">The serviceProvider.</param>
    /// <param name="configuration">The options.</param>
    public delegate void AutoMapperComponentConfigurationDelegate(IServiceProvider serviceProvider, IMapperConfigurationExpression configuration);
}
