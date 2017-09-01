//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Loop_OperatorCalculator_WithSignalOutput : Loop_OperatorCalculator_Base
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly DimensionStack _dimensionStack;

//        public Loop_OperatorCalculator_WithSignalOutput(
//            OperatorCalculatorBase signalCalculator,
//            OperatorCalculatorBase skipCalculator,
//            OperatorCalculatorBase loopStartMarkerCalculator,
//            OperatorCalculatorBase loopEndMarkerCalculator,
//            OperatorCalculatorBase releaseEndMarkerCalculator,
//            OperatorCalculatorBase noteDurationCalculator,
//            DimensionStack dimensionStack)
//            : base(
//                skipCalculator,
//                loopStartMarkerCalculator,
//                loopEndMarkerCalculator,
//                releaseEndMarkerCalculator,
//                noteDurationCalculator,
//                new[]
//                    {
//                        signalCalculator,
//                        skipCalculator,
//                        loopStartMarkerCalculator,
//                        loopEndMarkerCalculator,
//                        releaseEndMarkerCalculator,
//                        noteDurationCalculator
//                    }.Where(x => x != null)
//                     .ToArray())
//        {
//            _signalCalculator = signalCalculator ?? throw new ArgumentNullException(nameof(signalCalculator));
//            _dimensionStack = dimensionStack ?? throw new NullException(() => dimensionStack);

//            ResetNonRecursive();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double? transformedPosition = GetTransformedPosition(_dimensionStack.Get());
//            if (!transformedPosition.HasValue)
//            {
//                return 0.0;
//            }

//            _dimensionStack.Push(transformedPosition.Value);

//            double value = _signalCalculator.Calculate();

//            _dimensionStack.Pop();

//            return value;
//        }

//        public override void Reset()
//        {
//            // First reset parent, then children,
//            // because unlike some other operators,
//            // child state is dependent transformed position,
//            // which is dependent on parent state.
//            ResetNonRecursive();

//            // Dimension Transformation
//            double? transformedPosition = GetTransformedPosition(_dimensionStack.Get());
//            if (!transformedPosition.HasValue)
//            {
//                return;
//            }

//            _dimensionStack.Push(transformedPosition.Value);

//            base.Reset();

//            _dimensionStack.Pop();
//        }

//        protected void ResetNonRecursive()
//        {
//            double position = _dimensionStack.Get();

//            _origin = position;
//        }
//    }

//    internal class Loop_OperatorCalculator_WithPositionOutput : Loop_OperatorCalculator_Base
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;

//        public Loop_OperatorCalculator_WithPositionOutput(
//            OperatorCalculatorBase positionCalculator,
//            OperatorCalculatorBase skipCalculator,
//            OperatorCalculatorBase loopStartMarkerCalculator,
//            OperatorCalculatorBase loopEndMarkerCalculator,
//            OperatorCalculatorBase releaseEndMarkerCalculator,
//            OperatorCalculatorBase noteDurationCalculator)
//            : base(
//                skipCalculator,
//                loopStartMarkerCalculator,
//                loopEndMarkerCalculator,
//                releaseEndMarkerCalculator,
//                noteDurationCalculator,
//                new[]
//                    {
//                        positionCalculator,
//                        skipCalculator,
//                        loopStartMarkerCalculator,
//                        loopEndMarkerCalculator,
//                        releaseEndMarkerCalculator,
//                        noteDurationCalculator
//                    }.Where(x => x != null)
//                     .ToArray())
//        {
//            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));

//            ResetNonRecursive();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();

//            double? transformedPosition = GetTransformedPosition(position);

//            return transformedPosition ?? 0.0;
//        }

//        public override void Reset()
//        {
//            ResetNonRecursive();

//            // Dimension Transformation
//            double position = _positionCalculator.Calculate();

//            double? transformedPosition = GetTransformedPosition(position);
//            if (!transformedPosition.HasValue)
//            {
//                return;
//            }

//            base.Reset();
//        }

//        protected void ResetNonRecursive()
//        {
//            double position = _positionCalculator.Calculate();

//            _origin = position;
//        }
//    }

//    internal abstract class Loop_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _skipCalculator;
//        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
//        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;
//        private readonly OperatorCalculatorBase _releaseEndMarkerCalculator;
//        private readonly OperatorCalculatorBase _noteDurationCalculator;

//        protected double _origin;

//        public Loop_OperatorCalculator_Base(
//            OperatorCalculatorBase skipCalculator,
//            OperatorCalculatorBase loopStartMarkerCalculator,
//            OperatorCalculatorBase loopEndMarkerCalculator,
//            OperatorCalculatorBase releaseEndMarkerCalculator,
//            OperatorCalculatorBase noteDurationCalculator,
//            IList<OperatorCalculatorBase> childCalculators)
//            : base(childCalculators)
//        {
//            _skipCalculator = skipCalculator;
//            _loopStartMarkerCalculator = loopStartMarkerCalculator;
//            _loopEndMarkerCalculator = loopEndMarkerCalculator;
//            _releaseEndMarkerCalculator = releaseEndMarkerCalculator;
//            _noteDurationCalculator = noteDurationCalculator;
//        }

//        /// <summary> Returns null if before attack or after release. </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected double? GetTransformedPosition(double position)
//        {
//            double outputPosition = position;

//            outputPosition -= _origin;

//            // BeforeAttack
//            double skip = GetSkip();
//            double inputPosition = outputPosition + skip;
//            bool isBeforeAttack = inputPosition < skip;
//            if (isBeforeAttack)
//            {
//                return null;
//            }

//            // InAttack
//            double loopStartMarker = GetLoopStartMarker();
//            bool isInAttack = inputPosition < loopStartMarker;
//            if (isInAttack)
//            {
//                return inputPosition;
//            }

//            // InLoop
//            double noteDuration = GetNoteDuration();
//            double loopEndMarker = GetLoopEndMarker();
//            double cycleLength = loopEndMarker - loopStartMarker;

//            // Round up end of loop to whole cycles.
//            double outputLoopStart = loopStartMarker - skip;
//            double noteEndPhase = (noteDuration - outputLoopStart) / cycleLength;
//            double outputLoopEnd = outputLoopStart + Math.Ceiling(noteEndPhase) * cycleLength;

//            bool isInLoop = outputPosition < outputLoopEnd;
//            if (isInLoop)
//            {
//                double phase = (inputPosition - loopStartMarker) % cycleLength;
//                inputPosition = loopStartMarker + phase;
//                return inputPosition;
//            }

//            // InRelease
//            double releaseEndMarker = GetReleaseEndMarker();
//            double releaseLength = releaseEndMarker - loopEndMarker;
//            double outputReleaseEndPosition = outputLoopEnd + releaseLength;
//            bool isInRelease = outputPosition < outputReleaseEndPosition;
//            if (isInRelease)
//            {
//                double positionInRelease = outputPosition - outputLoopEnd;
//                inputPosition = loopEndMarker + positionInRelease;
//                return inputPosition;
//            }

//            // AfterRelease
//            return null;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetSkip()
//        {
//            double value = 0;
//            if (_skipCalculator != null)
//            {
//                value = _skipCalculator.Calculate();
//            }

//            return value;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetLoopStartMarker()
//        {
//            double value = 0;
//            if (_loopStartMarkerCalculator != null)
//            {
//                value = _loopStartMarkerCalculator.Calculate();
//            }

//            return value;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetLoopEndMarker()
//        {
//            double value = 0;
//            if (_loopEndMarkerCalculator != null)
//            {
//                value = _loopEndMarkerCalculator.Calculate();
//            }

//            return value;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetReleaseEndMarker()
//        {
//            double value = 0;
//            if (_releaseEndMarkerCalculator != null)
//            {
//                value = _releaseEndMarkerCalculator.Calculate();
//            }

//            return value;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetNoteDuration()
//        {
//            double value = CalculationHelper.VERY_HIGH_VALUE;
//            if (_noteDurationCalculator != null)
//            {
//                value = _noteDurationCalculator.Calculate();
//            }

//            return value;
//        }
//    }
//}