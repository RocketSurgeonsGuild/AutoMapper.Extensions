using System;
using AutoMapper;
using JetBrains.Annotations;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
    /// <summary>
    /// DurationConverter.
    /// Implements the <see cref="ITypeConverter{Duration, TimeSpan}" />
    /// Implements the <see cref="ITypeConverter{TimeSpan, Duration}" />
    /// Implements the <see cref="ITypeConverter{Duration, Int64}" />
    /// Implements the <see cref="ITypeConverter{Int64, Duration}" />
    /// Implements the <see cref="ITypeConverter{Duration, Int32}" />
    /// Implements the <see cref="ITypeConverter{Int32, Duration}" />
    /// Implements the <see cref="ITypeConverter{Duration, Double}" />
    /// Implements the <see cref="ITypeConverter{Double, Duration}" />
    /// Implements the <see cref="ITypeConverter{Duration, Decimal}" />
    /// Implements the <see cref="ITypeConverter{Decimal, Duration}" />
    /// </summary>
    /// <seealso cref="ITypeConverter{Duration, TimeSpan}" />
    /// <seealso cref="ITypeConverter{TimeSpan, Duration}" />
    /// <seealso cref="ITypeConverter{Duration, Int64}" />
    /// <seealso cref="ITypeConverter{Int64, Duration}" />
    /// <seealso cref="ITypeConverter{Duration, Int32}" />
    /// <seealso cref="ITypeConverter{Int32, Duration}" />
    /// <seealso cref="ITypeConverter{Duration, Double}" />
    /// <seealso cref="ITypeConverter{Double, Duration}" />
    /// <seealso cref="ITypeConverter{Duration, Decimal}" />
    /// <seealso cref="ITypeConverter{Decimal, Duration}" />
    [PublicAPI]
    public class DurationConverter :
        ITypeConverter<Duration, TimeSpan>,
        ITypeConverter<Duration?, TimeSpan?>,
        ITypeConverter<TimeSpan, Duration>,
        ITypeConverter<TimeSpan?, Duration?>,
        ITypeConverter<Duration, long>,
        ITypeConverter<Duration?, long?>,
        ITypeConverter<long, Duration>,
        ITypeConverter<long?, Duration?>,
        ITypeConverter<Duration, int>,
        ITypeConverter<Duration?, int?>,
        ITypeConverter<int, Duration>,
        ITypeConverter<int?, Duration?>,
        ITypeConverter<Duration, double>,
        ITypeConverter<Duration?, double?>,
        ITypeConverter<double, Duration>,
        ITypeConverter<double?, Duration?>,
        ITypeConverter<Duration, decimal>,
        ITypeConverter<Duration?, decimal?>,
        ITypeConverter<decimal, Duration>,
        ITypeConverter<decimal?, Duration?>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public TimeSpan Convert(Duration source, TimeSpan destination, ResolutionContext context)
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
        public TimeSpan? Convert(Duration? source, TimeSpan? destination, ResolutionContext context)
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
        public Duration Convert(TimeSpan source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTimeSpan(source);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Duration? Convert(TimeSpan? source, Duration? destination, ResolutionContext context)
        {
            return source != null ? (Duration?)Duration.FromTimeSpan(source.Value) : null;
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public long Convert(Duration source, long destination, ResolutionContext context)
        {
            return source.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public long? Convert(Duration? source, long? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return source.Value.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Duration Convert(long source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTicks(source * NodaConstants.TicksPerMillisecond);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Duration? Convert(long? source, Duration? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return Duration.FromTicks(source.Value * NodaConstants.TicksPerMillisecond);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public int Convert(Duration source, int destination, ResolutionContext context)
        {
            return (int)(source.BclCompatibleTicks / NodaConstants.TicksPerSecond);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public int? Convert(Duration? source, int? destination, ResolutionContext context)
        {
            if (source == null)
                return null;

            return (int)(source.Value.BclCompatibleTicks / NodaConstants.TicksPerSecond);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Duration Convert(int source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTicks(source * NodaConstants.TicksPerSecond);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Duration? Convert(int? source, Duration? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return Duration.FromTicks(source.Value * NodaConstants.TicksPerSecond);
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public double Convert(Duration source, double destination, ResolutionContext context)
        {
            return (double)source.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public double? Convert(Duration? source, double? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return (double)source.Value.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Duration Convert(double source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTicks((long)(source * NodaConstants.TicksPerMillisecond));
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Duration? Convert(double? source, Duration? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return Duration.FromTicks((long)(source.Value * NodaConstants.TicksPerMillisecond));
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public decimal Convert(Duration source, decimal destination, ResolutionContext context)
        {
            return (decimal)source.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public decimal? Convert(Duration? source, decimal? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return (decimal)source.Value.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public Duration Convert(decimal source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTicks((long)(source * NodaConstants.TicksPerMillisecond));
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.Nullable{Duration}.</returns>
        public Duration? Convert(decimal? source, Duration? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return Duration.FromTicks((long)(source.Value * NodaConstants.TicksPerMillisecond));
        }
    }
}
