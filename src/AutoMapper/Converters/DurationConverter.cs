using System;
using AutoMapper;
using NodaTime;

namespace Rocket.Surgery.Extensions.AutoMapper.Converters
{
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
        public TimeSpan Convert(Duration source, TimeSpan destination, ResolutionContext context)
        {
            return source.ToTimeSpan();
        }

        public TimeSpan? Convert(Duration? source, TimeSpan? destination, ResolutionContext context)
        {
            return source?.ToTimeSpan();
        }

        public Duration Convert(TimeSpan source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTimeSpan(source);
        }

        public Duration? Convert(TimeSpan? source, Duration? destination, ResolutionContext context)
        {
            return source != null ? (Duration?)Duration.FromTimeSpan(source.Value) : null;
        }

        public long Convert(Duration source, long destination, ResolutionContext context)
        {
            return source.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        public long? Convert(Duration? source, long? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return source.Value.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        public Duration Convert(long source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTicks(source * NodaConstants.TicksPerMillisecond);
        }

        public Duration? Convert(long? source, Duration? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return Duration.FromTicks(source.Value * NodaConstants.TicksPerMillisecond);
        }

        public int Convert(Duration source, int destination, ResolutionContext context)
        {
            return (int)(source.BclCompatibleTicks / NodaConstants.TicksPerSecond);
        }

        public int? Convert(Duration? source, int? destination, ResolutionContext context)
        {
            if (source == null)
                return null;

            return (int)(source.Value.BclCompatibleTicks / NodaConstants.TicksPerSecond);
        }

        public Duration Convert(int source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTicks(source * NodaConstants.TicksPerSecond);
        }

        public Duration? Convert(int? source, Duration? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return Duration.FromTicks(source.Value * NodaConstants.TicksPerSecond);
        }

        public double Convert(Duration source, double destination, ResolutionContext context)
        {
            return (double)source.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        public double? Convert(Duration? source, double? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return (double)source.Value.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        public Duration Convert(double source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTicks((long)(source * NodaConstants.TicksPerMillisecond));
        }

        public Duration? Convert(double? source, Duration? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return Duration.FromTicks((long)(source.Value * NodaConstants.TicksPerMillisecond));
        }

        public decimal Convert(Duration source, decimal destination, ResolutionContext context)
        {
            return (decimal)source.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        public decimal? Convert(Duration? source, decimal? destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            return (decimal)source.Value.BclCompatibleTicks / NodaConstants.TicksPerMillisecond;
        }

        public Duration Convert(decimal source, Duration destination, ResolutionContext context)
        {
            return Duration.FromTicks((long)(source * NodaConstants.TicksPerMillisecond));
        }

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
