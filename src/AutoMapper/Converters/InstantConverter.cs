using System;
using AutoMapper;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
    /// <summary>
    /// InstantConverter.
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.Instant, System.DateTime}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.Instant?, System.DateTime?}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.Instant, System.DateTimeOffset}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.Instant?, System.DateTimeOffset?}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.DateTime, NodaTime.Instant}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.DateTime?, NodaTime.Instant?}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.DateTimeOffset, NodaTime.Instant}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.DateTimeOffset?, NodaTime.Instant?}" />
    /// </summary>
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.Instant, System.DateTime}" />
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.Instant?, System.DateTime?}" />
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.Instant, System.DateTimeOffset}" />
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.Instant?, System.DateTimeOffset?}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.DateTime, NodaTime.Instant}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.DateTime?, NodaTime.Instant?}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.DateTimeOffset, NodaTime.Instant}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.DateTimeOffset?, NodaTime.Instant?}" />
    public class InstantConverter :
        ITypeConverter<Instant, DateTime>,
        ITypeConverter<Instant?, DateTime?>,
        ITypeConverter<Instant, DateTimeOffset>,
        ITypeConverter<Instant?, DateTimeOffset?>,
        ITypeConverter<DateTime, Instant>,
        ITypeConverter<DateTime?, Instant?>,
        ITypeConverter<DateTimeOffset, Instant>,
        ITypeConverter<DateTimeOffset?, Instant?>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public DateTime Convert(Instant source, DateTime destination, ResolutionContext context)
        {
            return source.ToDateTimeUtc();
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public DateTime? Convert(Instant? source, DateTime? destination, ResolutionContext context)
        {
            return source?.ToDateTimeUtc();
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public DateTimeOffset Convert(Instant source, DateTimeOffset destination, ResolutionContext context)
        {
            return source.ToDateTimeOffset();
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public DateTimeOffset? Convert(Instant? source, DateTimeOffset? destination, ResolutionContext context)
        {
            return source?.ToDateTimeOffset();
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Instant Convert(DateTime source, Instant destination, ResolutionContext context)
        {
            var utcDateTime = source.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(source, DateTimeKind.Utc)
                : source.ToUniversalTime();

            return Instant.FromDateTimeUtc(utcDateTime);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Instant? Convert(DateTime? source, Instant? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            var dateTime = source.Value;
            var utcDateTime = dateTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
                : dateTime.ToUniversalTime();

            return Instant.FromDateTimeUtc(utcDateTime);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Instant Convert(DateTimeOffset source, Instant destination, ResolutionContext context)
        {
            return Instant.FromDateTimeOffset(source);
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.Nullable&lt;Instant&gt;.</returns>
        public Instant? Convert(DateTimeOffset? source, Instant? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return Instant.FromDateTimeOffset(source.Value);
        }
    }
}
