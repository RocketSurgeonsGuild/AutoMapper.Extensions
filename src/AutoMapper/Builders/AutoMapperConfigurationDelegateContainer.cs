using System;

namespace Rocket.Surgery.Extensions.AutoMapper.Builders
{
    /// <summary>
    /// Class AutoMapperConfigurationDelegateContainer.
    /// </summary>
    class AutoMapperConfigurationDelegateContainer
    {
        /// <summary>
        /// Gets the delegate.
        /// </summary>
        /// <value>The delegate.</value>
        public Delegate Delegate { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperConfigurationDelegateContainer"/> class.
        /// </summary>
        /// <param name="delegate">The delegate.</param>
        public AutoMapperConfigurationDelegateContainer(Delegate @delegate)
        {
            Delegate = @delegate;
        }
    }
}
