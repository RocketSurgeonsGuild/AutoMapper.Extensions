using System;
using AutoMapper;
using JetBrains.Annotations;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
    /// <summary>
    /// LocalTimeConverter.
    /// Implements the <see cref="ITypeConverter{LocalTime, TimeSpan}" />
    /// Implements the <see cref="ITypeConverter{TimeSpan, LocalTime}" />
    /// Implements the <see cref="ITypeConverter{LocalTime, DateTime}" />
    /// Implements the <see cref="ITypeConverter{DateTime, LocalTime}" />
    /// </summary>
    /// <seealso cref="ITypeConverter{LocalTime, TimeSpan}" />
    /// <seealso cref="ITypeConverter{TimeSpan, LocalTime}" />
    /// <seealso cref="ITypeConverter{LocalTime, DateTime}" />
    /// <seealso cref="ITypeConverter{DateTime, LocalTime}" />
    [PublicAPI]
    public class LocalTimeConverter :
        ITypeConverter<LocalTime, TimeSpan>,
        ITypeConverter<TimeSpan, LocalTime>,
        ITypeConverter<LocalTime, DateTime>,
        ITypeConverter<DateTime, LocalTime>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public TimeSpan Convert(LocalTime source, TimeSpan destination, ResolutionContext context)
        {
            return new TimeSpan(source.TickOfDay);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public LocalTime Convert(TimeSpan source, LocalTime destination, ResolutionContext context)
        {
            return LocalTime.FromTicksSinceMidnight(source.Ticks);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public DateTime Convert(LocalTime source, DateTime destination, ResolutionContext context)
        {
            return source.On(new LocalDate(1, 1, 1)).ToDateTimeUnspecified();
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public LocalTime Convert(DateTime source, LocalTime destination, ResolutionContext context)
        {
            return LocalDateTime.FromDateTime(source).TimeOfDay;
        }
    }
}
