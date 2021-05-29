using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_WithPositionOutput : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _positionCalculator;
        private readonly OperatorCalculatorBase _skipCalculator;
        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;
        private readonly OperatorCalculatorBase _releaseEndMarkerCalculator;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        private double _origin;

        public Loop_OperatorCalculator_WithPositionOutput(
            OperatorCalculatorBase positionCalculator,
            OperatorCalculatorBase skipCalculator,
            OperatorCalculatorBase loopStartMarkerCalculator,
            OperatorCalculatorBase loopEndMarkerCalculator,
            OperatorCalculatorBase releaseEndMarkerCalculator,
            OperatorCalculatorBase noteDurationCalculator)
            : base(new[]
            {
                positionCalculator,
                skipCalculator,
                loopStartMarkerCalculator,
                loopEndMarkerCalculator,
                releaseEndMarkerCalculator,
                noteDurationCalculator
            }.Where(x => x != null).ToArray())
        {
            _positionCalculator = positionCalculator;
            _skipCalculator = skipCalculator;
            _loopStartMarkerCalculator = loopStartMarkerCalculator;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
            _releaseEndMarkerCalculator = releaseEndMarkerCalculator;
            _noteDurationCalculator = noteDurationCalculator;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionCalculator.Calculate();

            double? transformedPosition = GetTransformedPosition(position);

            return transformedPosition ?? 0.0;
        }

        public override void Reset()
        {
            ResetNonRecursive();

            // Dimension Transformation
            double position = _positionCalculator.Calculate();

            double? transformedPosition = GetTransformedPosition(position);
            if (!transformedPosition.HasValue)
            {
                return;
            }

            base.Reset();
        }

        private void ResetNonRecursive()
        {
            double position = _positionCalculator.Calculate();

            _origin = position;
        }

        /// <summary>
        /// Returns null if before attack or after release.
        /// NOTE: The need nesting and avoiding early returns,
        /// is to make it easier to port to an inlined version,
        /// to the C# code generator.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double? GetTransformedPosition(double position)
        {
            double? nullableInputPosition;

            double outputPosition = position;
            double inputPosition = outputPosition;

            inputPosition -= _origin;

            // BeforeAttack
            double skip = GetSkip();
            inputPosition += skip;
            bool isBeforeAttack = inputPosition < skip;
            if (isBeforeAttack)
            {
                nullableInputPosition = null;
            }
            else
            {
                // InAttack
                double loopStartMarker = GetLoopStartMarker();
                bool isInAttack = inputPosition < loopStartMarker;
                if (isInAttack)
                {
                    nullableInputPosition = inputPosition;
                }
                else
                {
                    // InLoop
                    double loopEndMarker = GetLoopEndMarker();
                    double cycleLength = loopEndMarker - loopStartMarker;

                    // Round up end of loop to whole cycles.
                    double outputLoopStart = loopStartMarker - skip;
                    double noteDuration = GetNoteDuration();
                    double noteEndPhase = (noteDuration - outputLoopStart) / cycleLength;
                    double outputLoopEnd = outputLoopStart + Math.Ceiling(noteEndPhase) * cycleLength;

                    bool isInLoop = outputPosition < outputLoopEnd;
                    if (isInLoop)
                    {
                        double phase = (inputPosition - loopStartMarker) % cycleLength;
                        inputPosition = loopStartMarker + phase;
                        nullableInputPosition = inputPosition;
                    }
                    else
                    {
                        // InRelease
                        double releaseEndMarker = GetReleaseEndMarker();
                        double releaseLength = releaseEndMarker - loopEndMarker;
                        double outputReleaseEndPosition = outputLoopEnd + releaseLength;
                        bool isInRelease = outputPosition < outputReleaseEndPosition;
                        if (isInRelease)
                        {
                            double positionInRelease = outputPosition - outputLoopEnd;
                            inputPosition = loopEndMarker + positionInRelease;
                            nullableInputPosition = inputPosition;
                        }
                        else
                        {
                            // AfterRelease
                            nullableInputPosition = null;
                        }
                    }
                }
            }

            return nullableInputPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetSkip()
        {
            double value = 0;
            if (_skipCalculator != null)
            {
                value = _skipCalculator.Calculate();
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopStartMarker()
        {
            double value = 0;
            if (_loopStartMarkerCalculator != null)
            {
                value = _loopStartMarkerCalculator.Calculate();
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopEndMarker()
        {
            double value = 0;
            if (_loopEndMarkerCalculator != null)
            {
                value = _loopEndMarkerCalculator.Calculate();
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetReleaseEndMarker()
        {
            double value = 0;
            if (_releaseEndMarkerCalculator != null)
            {
                value = _releaseEndMarkerCalculator.Calculate();
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetNoteDuration()
        {
            double value = CalculationHelper.VERY_HIGH_VALUE;
            if (_noteDurationCalculator != null)
            {
                value = _noteDurationCalculator.Calculate();
            }

            return value;
        }
    }
}