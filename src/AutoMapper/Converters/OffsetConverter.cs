using System;
using AutoMapper;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
    /// <summary>
    /// OffsetConverter.
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.Offset, System.TimeSpan}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{NodaTime.Offset?, System.TimeSpan?}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.TimeSpan, NodaTime.Offset}" />
    /// Implements the <see cref="AutoMapper.ITypeConverter{System.TimeSpan?, NodaTime.Offset?}" />
    /// </summary>
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.Offset, System.TimeSpan}" />
    /// <seealso cref="AutoMapper.ITypeConverter{NodaTime.Offset?, System.TimeSpan?}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.TimeSpan, NodaTime.Offset}" />
    /// <seealso cref="AutoMapper.ITypeConverter{System.TimeSpan?, NodaTime.Offset?}" />
    public class OffsetConverter :
        ITypeConverter<Offset, TimeSpan>,
        ITypeConverter<Offset?, TimeSpan?>,
        ITypeConverter<TimeSpan, Offset>,
        ITypeConverter<TimeSpan?, Offset?>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public TimeSpan Convert(Offset source, TimeSpan destination, ResolutionContext context)
        {
            return source.ToTimeSpan();
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public TimeSpan? Convert(Offset? source, TimeSpan? destination, ResolutionContext context)
        {
            return source?.ToTimeSpan();
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Offset Convert(TimeSpan source, Offset destination, ResolutionContext context)
        {
            return Offset.FromTicks(source.Ticks);
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.Nullable&lt;Offset&gt;.</returns>
        public Offset? Convert(TimeSpan? source, Offset? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return Offset.FromTicks(source.Value.Ticks);
        }
    }
}
