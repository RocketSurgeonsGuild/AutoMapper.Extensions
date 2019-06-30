using System;
using AutoMapper;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
    /// <summary>
    /// Class OffsetDateTimeConverter.
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.OffsetDateTime, System.DateTimeOffset}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.OffsetDateTime?, System.DateTimeOffset?}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.DateTimeOffset, NodaTime.OffsetDateTime}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.DateTimeOffset?, NodaTime.OffsetDateTime?}" />
    /// </summary>
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.OffsetDateTime, System.DateTimeOffset}" />
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.OffsetDateTime?, System.DateTimeOffset?}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.DateTimeOffset, NodaTime.OffsetDateTime}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.DateTimeOffset?, NodaTime.OffsetDateTime?}" />
    public class OffsetDateTimeConverter :
        ITypeConverter<OffsetDateTime, DateTimeOffset>,
        ITypeConverter<OffsetDateTime?, DateTimeOffset?>,
        ITypeConverter<DateTimeOffset, OffsetDateTime>,
        ITypeConverter<DateTimeOffset?, OffsetDateTime?>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public DateTimeOffset Convert(OffsetDateTime source, DateTimeOffset destination, ResolutionContext context)
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
        public DateTimeOffset? Convert(OffsetDateTime? source, DateTimeOffset? destination, ResolutionContext context)
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
        public OffsetDateTime Convert(DateTimeOffset source, OffsetDateTime destination, ResolutionContext context)
        {
            return OffsetDateTime.FromDateTimeOffset(source);
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.Nullable&lt;OffsetDateTime&gt;.</returns>
        public OffsetDateTime? Convert(DateTimeOffset? source, OffsetDateTime? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return OffsetDateTime.FromDateTimeOffset(source.Value);
        }
    }
}
