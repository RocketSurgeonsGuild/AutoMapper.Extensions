using System;
using AutoMapper;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
    /// <summary>
    /// Class LocalDateConverter.
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.LocalDate, System.DateTime}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.LocalDate?, System.DateTime?}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.DateTime, NodaTime.LocalDate}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.DateTime?, NodaTime.LocalDate?}" />
    /// </summary>
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.LocalDate, System.DateTime}" />
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.LocalDate?, System.DateTime?}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.DateTime, NodaTime.LocalDate}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.DateTime?, NodaTime.LocalDate?}" />
    public class LocalDateConverter :
        ITypeConverter<LocalDate, DateTime>,
        ITypeConverter<LocalDate?, DateTime?>,
        ITypeConverter<DateTime, LocalDate>,
        ITypeConverter<DateTime?, LocalDate?>
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
        public DateTime? Convert(LocalDate? source, DateTime? destination, ResolutionContext context)
        {
            if (source == null)
                return null;

            return source.Value.AtMidnight().ToDateTimeUnspecified();
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

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.Nullable&lt;LocalDate&gt;.</returns>
        public LocalDate? Convert(DateTime? source, LocalDate? destination, ResolutionContext context)
        {
            if (source == null)
                return null;

            return LocalDateTime.FromDateTime(source.Value).Date;
        }
    }
}
