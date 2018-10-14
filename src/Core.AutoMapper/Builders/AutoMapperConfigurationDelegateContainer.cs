using System;

namespace Rocket.Surgery.Core.AutoMapper.Builders
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
