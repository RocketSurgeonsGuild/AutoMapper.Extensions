using AutoMapper;
using System;

namespace Rocket.Surgery.Extensions.AutoMapper.Builders
{
    /// <summary>
    /// Class BuildObservable.
    /// Implements the <see cref="System.IObserver{System.IServiceProvider}" />
    /// </summary>
    /// <seealso cref="System.IObserver{System.IServiceProvider}" />
    class BuildObservable : IObserver<IServiceProvider>
    {
        private IServiceProvider _serviceProvider;
        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted()
        {
            Mapper.Initialize(AutoMapperServicesExtensions.ConfigAction(_serviceProvider));
        }

        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
        }

        /// <summary>
        /// Called when [next].
        /// </summary>
        /// <param name="value">The value.</param>
        public void OnNext(IServiceProvider value)
        {
            _serviceProvider = value;
        }
    }
}

