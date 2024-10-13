using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;
using static JJ.Business.Synthesizer.Tests.Wishes.Notes;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    /// <summary>
    /// Extensions that are wishes for the back-end related to the Operator entity.
    /// </summary>
    public static class OperatorExtensionsWishes
    {
        /// <inheritdoc cref="docs._asconst"/>
        public static double? AsConst(this Outlet outlet) =>  outlet?.Operator?.AsConst();

        /// <inheritdoc cref="docs._asconst"/>
        public static double? AsConst(this Operator op) => op?.AsValueOperator?.Value;
        
        /// <inheritdoc cref="docs._default" />
        public static Outlet Stretch(this OperatorFactory operatorFactory, Outlet signal, Outlet timeFactor)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;
            
            return x.TimeMultiply(signal, timeFactor ?? x.Value(1));
        }

        /// <inheritdoc cref="docs._sine" />
        public static Outlet Sine(this OperatorFactory operatorFactory, Outlet pitch)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;
            
            return x.Sine(x.Value(1), pitch);
        }

        /// <inheritdoc cref="docs._default" />
        public static Outlet StrikeNote(
            this OperatorFactory operatorFactory, Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;

            // A little optimization, because so slow...
            bool delayFilledIn = delay != null && delay.AsConst() != 0;
            bool volumeFilledIn = volume != null && volume.AsConst() != 1;
            
            if (delayFilledIn) sound = x.TimeAdd(sound, delay);
            if (volumeFilledIn) sound = x.Multiply(sound, volume);

            return sound;
        }

        /// <inheritdoc cref="docs._vibrato" />
        public static Outlet VibratoOverPitch(
            this OperatorFactory operatorFactory, Outlet freq, (Outlet speed, Outlet depth) vibrato = default)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;

            vibrato.speed = vibrato.speed ?? x.Value(5.5);
            vibrato.depth = vibrato.depth ?? x.Value(0.0005);

            return x.Multiply(freq, x.Add(x.Value(1), x.Sine(vibrato.depth, vibrato.speed)));
        }

        /// <inheritdoc cref="docs._tremolo" />
        public static Outlet Tremolo(
            this OperatorFactory operatorFactory, Outlet sound, (Outlet speed, Outlet depth) tremolo = default)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;
            
            tremolo.speed = tremolo.speed ?? x.Value(8);
            tremolo.depth = tremolo.depth ?? x.Value(0.33);

            return x.Multiply(sound, x.Add(x.Sine(tremolo.depth, tremolo.speed), x.Value(1)));
        }

        // Panning
        
        /// <inheritdoc cref="docs._panning" />
        public static Outlet Panning(
            this OperatorFactory operatorFactory, Outlet sound, Outlet panning, ChannelEnum channel)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;

            // Some optimization in case of a constant value
            {
                double? constPanning = panning.AsConst();
                if (constPanning != null)
                {
                    return x.Panning(sound, constPanning.Value, channel);
                }
            }

            switch (channel)
            {
                case Left:  return x.Multiply(sound, x.Substract(x.Value(1), panning));
                case Right: return x.Multiply(sound, panning);
                default:    return sound;
            }
        }

        /// <inheritdoc cref="docs._panning" />
        public static Outlet Panning(
            this OperatorFactory operatorFactory, Outlet sound, double panning, ChannelEnum channel)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;
            
            if (panning < 0) panning = 0;
            if (panning > 1) panning = 1;

            switch (channel)
            {
                case Left:  return x.Multiply(sound, x.Value(1 - panning));
                case Right: return x.Multiply(sound, x.Value(panning));
                default:    return sound;
            }
        }

        // Panbrello
        
        /// <inheritdoc cref="docs._panbrello" />
        public static Outlet Panbrello(
            this OperatorFactory operatorFactory, 
            Outlet sound, (Outlet speed, Outlet depth) panbrello, ChannelEnum channel)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;
            
            panbrello.speed = panbrello.speed ?? x.Value(1);
            panbrello.depth = panbrello.depth ?? x.Value(1);

            // Some optimization for constant values
            {
                double? constSpeed = panbrello.speed?.AsConst();
                double? constDepth = panbrello.depth?.AsConst();
                if (constSpeed != null && constDepth != null)
                {
                    return x.Panbrello(sound, (constSpeed.Value, constDepth.Value), channel);
                }
            }

            // 0.5 is in the middle. 0 is left, 1 is right.
            var sine      = x.Sine(panbrello.depth, panbrello.speed); // [-1,+1]
            var halfSine  = x.Multiply(x.Value(0.5), sine); // [-0.5,+0.5]
            var zeroToOne = x.Add(x.Value(0.5), halfSine); // [0,1]

            return x.Panning(sound, zeroToOne, channel);
        }
        
        /// <inheritdoc cref="docs._panbrello" />
        public static Outlet Panbrello(
            this OperatorFactory operatorFactory,
            Outlet sound, (double speed, double depth) panbrello = default, ChannelEnum channel = default)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;

            if (panbrello.speed == default) panbrello.speed = 1;
            if (panbrello.depth == default) panbrello.depth = 1;

            // 0.5 is in the middle. 0 is left, 1 is right.
            var halfSine  = x.Multiply(x.Sine(x.Value(panbrello.speed), x.Value(panbrello.depth / 2))); // [-0.5,+0.5]
            var zeroToOne = x.Add(x.Value(0.5), halfSine); // [0,1]

            return x.Panning(sound, zeroToOne, channel);
        }

        // PitchPan
        
        /// <inheritdoc cref="docs._pitchpan" />
        public static Outlet PitchPan(
            this OperatorFactory operatorFactory,
            Outlet actualFrequency, Outlet centerFrequency,
            Outlet referenceFrequency, Outlet referencePanning)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);
            var x = operatorFactory;

            // Some optimization in case of constants, because things are currently so slow.
            {
                double? constActualFrequency    = actualFrequency?.AsConst();
                double? constCenterFrequency    = centerFrequency?.AsConst();
                double? constReferenceFrequency = referenceFrequency?.AsConst();
                double? constReferencePanning   = referencePanning?.AsConst();

                if (constActualFrequency != null &&
                    constCenterFrequency != null &&
                    constReferenceFrequency != null &&
                    constReferencePanning != null)
                {
                    double pitchPan =
                        PitchPan(operatorFactory,
                                 constActualFrequency.Value,    constCenterFrequency.Value,
                                 constReferenceFrequency.Value, constReferencePanning.Value);
                    return x.Value(pitchPan);
                }
            }
            
            // Defaults
            centerFrequency    = centerFrequency ?? x.Value(A4);
            referenceFrequency = referenceFrequency ?? x.Value(E4);
            referencePanning   = referencePanning ?? x.Value(0.6);
            
            var centerPanning = x.Value(0.5);

            // Calculate intervals relative to the center frequency
            var referenceInterval = x.Divide(referenceFrequency, centerFrequency);
            var actualInterval    = x.Divide(actualFrequency,    centerFrequency);

            var factor = x.Multiply(actualInterval, referenceInterval);

            // Calculate panning deviation
            //var newPanningDeviation = x.Multiply(x.Substract(referencePanning, centerPanning), factor);
            // AI's correction:
            var newPanningDeviation = x.Multiply(x.Substract(referencePanning, centerPanning), x.Substract(factor, x.Value(1)));
            var newPanning          = x.Add(centerPanning, newPanningDeviation);

            return newPanning;
        }

        /// <inheritdoc cref="docs._pitchpan" />
        public static double PitchPan(
            this OperatorFactory operatorFactory,
            double actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
        {
            if (operatorFactory == null) throw new NullException(() => operatorFactory);

            // Defaults
            if (centerFrequency == default) centerFrequency       = A4;
            if (referenceFrequency == default) referenceFrequency = E4;
            if (referencePanning == default) referencePanning     = 0.6;

            double centerPanning = 0.5;

            // Calculate intervals relative to the center frequency
            double referenceInterval = referenceFrequency / centerFrequency;
            double actualInterval    = actualFrequency / centerFrequency;

            double factor = actualInterval * referenceInterval;

            // Calculate panning deviation
            //double newPanningDeviation = (referencePanning - centerPanning) * factor;
            // AI's correction:
            double newPanningDeviation = (referencePanning - centerPanning) * (factor - 1);
            double newPanning          = centerPanning + newPanningDeviation;

            return newPanning;
        }
    }
}