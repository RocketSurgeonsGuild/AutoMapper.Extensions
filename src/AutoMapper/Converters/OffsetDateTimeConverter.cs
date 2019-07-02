using System;
using AutoMapper;
using JetBrains.Annotations;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
    /// <summary>
    /// OffsetDateTimeConverter.
    /// Implements the <see cref="ITypeConverter{OffsetDateTime, DateTimeOffset}" />
    /// Implements the <see cref="ITypeConverter{DateTimeOffset, OffsetDateTime}" />
    /// </summary>
    /// <seealso cref="ITypeConverter{OffsetDateTime, DateTimeOffset}" />
    /// <seealso cref="ITypeConverter{DateTimeOffset, OffsetDateTime}" />
    [PublicAPI]
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
        /// <returns>System.Nullable{OffsetDateTime}.</returns>
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
