using System;
using AutoMapper;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
    /// <summary>
    /// LocalDateTimeConverter.
    /// Implements the <see cref="ITypeConverter{LocalDateTime, DateTime}" />
    /// Implements the <see cref="ITypeConverter{DateTime, LocalDateTime}" />
    /// </summary>
    /// <seealso cref="ITypeConverter{LocalDateTime, DateTime}" />
    /// <seealso cref="ITypeConverter{DateTime, LocalDateTime}" />
    /// [PublicAPI]
    public class LocalDateTimeConverter :
        ITypeConverter<LocalDateTime, DateTime>,
        ITypeConverter<LocalDateTime?, DateTime?>,
        ITypeConverter<DateTime, LocalDateTime>,
        ITypeConverter<DateTime?, LocalDateTime?>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public DateTime Convert(LocalDateTime source, DateTime destination, ResolutionContext context)
        {
            return source.ToDateTimeUnspecified();
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public LocalDateTime Convert(DateTime source, LocalDateTime destination, ResolutionContext context)
        {
            return LocalDateTime.FromDateTime(source);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public DateTime? Convert(LocalDateTime? source, DateTime? destination, ResolutionContext context)
        {
            return source?.ToDateTimeUnspecified() ?? destination;
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public LocalDateTime? Convert(DateTime? source, LocalDateTime? destination, ResolutionContext context)
        {
            return source.HasValue ? LocalDateTime.FromDateTime(source.Value) : destination;
        }
    }
}
