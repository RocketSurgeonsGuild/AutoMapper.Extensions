using System;
using AutoMapper;

namespace Rocket.Surgery.Core.AutoMapper.Builders
{
    /// <summary>
    /// Delegate MartenComponentConfigurationDelegate
    /// </summary>
    /// <param name="serviceProvider">The serviceProvider.</param>
    /// <param name="configuration">The options.</param>
    /// TODO Edit XML Comment Template for MartenComponentConfigurationDelegate
    public delegate void AutoMapperComponentConfigurationDelegate(IServiceProvider serviceProvider, IMapperConfigurationExpression configuration);
}
