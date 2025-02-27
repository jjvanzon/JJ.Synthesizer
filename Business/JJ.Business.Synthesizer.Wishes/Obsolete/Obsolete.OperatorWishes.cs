using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    public static class OperatorObsoleteExtensionsWishes
    {
        [Obsolete("Use " + nameof(Add) + " instead.", true), UsedImplicitly]
        public static Adder Adder(this SynthWishes synthWishes, params Outlet[] operands) => throw new NotSupportedException();

        [Obsolete("Use " + nameof(Add) + " instead.", true), UsedImplicitly]
        public static Adder Adder(this SynthWishes synthWishes, IList<Outlet> operands) => throw new NotSupportedException();
        
        [Obsolete("Typo. Use Subtract instead.", true), UsedImplicitly]
        public static Substract Substract(this SynthWishes synthWishes, Outlet operandA = null, Outlet operandB = null) => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public static Outlet Multiply(this SynthWishes synthWishes, Outlet operandA, Outlet operandB, Outlet origin) => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public static Divide Divide(this SynthWishes synthWishes, Outlet numerator, Outlet denominator, Outlet origin) => throw new NotSupportedException();

        [Obsolete("Use Multiply(Sine(pitch), volume) instead of Sine(volume, pitch).", true), UsedImplicitly]
        public static Sine Sine(this SynthWishes synthWishes, Outlet volume, Outlet pitch) => throw new NotSupportedException();

        [Obsolete("Use Add(Multiply(Sine(pitch), volume), level) instead of Sine(volume, pitch, level).", true), UsedImplicitly]
        public static Sine Sine(this SynthWishes synthWishes, Outlet volume, Outlet pitch, Outlet level) => throw new NotSupportedException();

        [Obsolete("Use Delay(Add(Multiply(Sine(pitch), volume), level), phaseStart) instead of Sine(volume, pitch, level, phaseStart).", true), UsedImplicitly]
        public static Sine Sine(this SynthWishes synthWishes, Outlet volume, Outlet pitch, Outlet level, Outlet phaseStart) => throw new NotSupportedException();

        [Obsolete("Use Delay instead.", true), UsedImplicitly]
        public static TimeAdd TimeAdd(this SynthWishes synthWishes, Outlet signal = null, Outlet timeDifference = null) => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public static Outlet Stretch(this SynthWishes synthWishes, Outlet signal, Outlet timeFactor, Outlet origin) => throw new NotSupportedException();
        
        [Obsolete("Use " + nameof(Stretch) + " instead.", true), UsedImplicitly]
        public static TimeMultiply TimeMultiply(this SynthWishes synthWishes, Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null) => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public static TimeDivide SpeedUp(this SynthWishes synthWishes, Outlet signal, Outlet timeDivider, Outlet origin) => throw new NotSupportedException();

        [Obsolete("Use " + nameof(SpeedUp) + " instead.", true), UsedImplicitly]
        public static TimeDivide TimeDivide(this SynthWishes synthWishes, Outlet signal = null, Outlet timeDivider = null, Outlet origin = null) => throw new NotSupportedException();

        [Obsolete("Typo. Use Skip instead.", true), UsedImplicitly]
        public static TimeSubstract TimeSubstract(this SynthWishes synthWishes, Outlet signal = null, Outlet timeDifference = null) => throw new NotSupportedException();
        
        [Obsolete("Use Skip instead.", true), UsedImplicitly]
        public static TimeSubstract TimeSubtract(this SynthWishes synthWishes, Outlet signal = null, Outlet timeDifference = null)            => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public static TimePower TimePower(this SynthWishes synthWishes, Outlet signal, Outlet exponent, Outlet origin) => throw new NotSupportedException();

        [Obsolete("Prefer other parameters.", true), UsedImplicitly]
        public static SampleOperatorWrapper Sample(this SynthWishes synthWishes, Sample sample) => throw new NotSupportedException();
    }

    public static class FlowNodeObsoleteExtensions
    {
        /// <inheritdoc cref="_getcurvewrapper"/>
        [Obsolete(WrappersObsoleteMessages.ObsoleteMessage)]
        public static CurveInWrapper GetCurveWrapper(this FlowNode flowNode )
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return flowNode.UnderlyingOutlet.GetCurveWrapper();
        }

        /// <inheritdoc cref="_getsamplewrapper" />
        [Obsolete(WrappersObsoleteMessages.ObsoleteMessage)]
        public static SampleOperatorWrapper GetSampleWrapper(this FlowNode flowNode )
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return flowNode.UnderlyingOutlet.GetSampleWrapper();
        }
    }
}
