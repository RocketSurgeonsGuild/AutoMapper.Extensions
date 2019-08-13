using System;
using AutoMapper;
using JetBrains.Annotations;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
    /// <summary>
    /// LocalDateConverter.
    /// Implements the <see cref="ITypeConverter{LocalDate, DateTime}" />
    /// Implements the <see cref="ITypeConverter{DateTime, LocalDate}" />
    /// </summary>
    /// <seealso cref="ITypeConverter{LocalDate, DateTime}" />
    /// <seealso cref="ITypeConverter{DateTime, LocalDate}" />
    [PublicAPI]
    public class LocalDateConverter :
        ITypeConverter<LocalDate, DateTime>,
        ITypeConverter<DateTime, LocalDate>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public DateTime Convert(LocalDate source, DateTime destination, ResolutionContext context)
        {
            return source.AtMidnight().ToDateTimeUnspecified();
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public LocalDate Convert(DateTime source, LocalDate destination, ResolutionContext context)
        {
            return LocalDateTime.FromDateTime(source).Date;
        }
    }
}
