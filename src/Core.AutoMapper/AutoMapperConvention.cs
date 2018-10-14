using Rocket.Surgery.Core.AutoMapper;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Extensions.DependencyInjection;

[assembly: Convention(typeof(AutoMapperConvention))]

namespace Rocket.Surgery.Core.AutoMapper
{
    /// <summary>
    /// Class AutoMapperConvention.
    /// </summary>
    /// <seealso cref="IServiceConvention" />
    /// TODO Edit XML Comment Template for AutoMapperConvention
    public class AutoMapperConvention : IServiceConvention
    {
        /// <summary>
        /// Registers the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for Register
        public void Register(IServiceConventionContext context)
        {
            context.WithAutoMapper();
        }
    }
}
