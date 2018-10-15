using System;

namespace Rocket.Surgery.Extensions.AutoMapper.Builders
{
    class AutoMapperConfigurationDelegateContainer
    {
        public Delegate Delegate { get; }

        public AutoMapperConfigurationDelegateContainer(Delegate @delegate)
        {
            Delegate = @delegate;
        }
    }
}
