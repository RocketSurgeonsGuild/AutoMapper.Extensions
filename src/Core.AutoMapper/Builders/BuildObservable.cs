using AutoMapper;
using System;

namespace Rocket.Surgery.Core.AutoMapper.Builders
{
    class BuildObservable : IObserver<IServiceProvider>
    {
        private IServiceProvider _serviceProvider;
        public void OnCompleted()
        {
            Mapper.Initialize(AutoMapperServicesExtensions.ConfigAction(_serviceProvider));
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IServiceProvider value)
        {
            _serviceProvider = value;
        }
    }
}

