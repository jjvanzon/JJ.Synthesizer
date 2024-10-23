using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        [Obsolete("Use _[123] instead."), UsedImplicitly]
        public ValueOperatorWrapper Value(double value = 0) => _[value];

        [Obsolete("Use Add instead.", true), UsedImplicitly]
        public Adder Adder(params Outlet[] operands) => throw new NotSupportedException();

        [Obsolete("Use Add instead.", true), UsedImplicitly]
        public Adder Adder(IList<Outlet> operands) => throw new NotSupportedException();
        
        [Obsolete("Typo. Use Subtract instead.", true), UsedImplicitly]
        public Substract Substract(Outlet operandA = null, Outlet operandB = null) => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public Outlet Multiply(Outlet operandA, Outlet operandB, Outlet origin) => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public Divide Divide(Outlet numerator, Outlet denominator, Outlet origin) => throw new NotSupportedException();

        [Obsolete("Use Multiply(Sine(pitch), volume) instead of Sine(volume, pitch).", true), UsedImplicitly]
        public Sine Sine(Outlet volume, Outlet pitch) => throw new NotSupportedException();

        [Obsolete("Use Add(Multiply(Sine(pitch), volume), level) instead of Sine(volume, pitch, level).", true), UsedImplicitly]
        public Sine Sine(Outlet volume, Outlet pitch, Outlet level) => throw new NotSupportedException();

        [Obsolete("Use Delay(Add(Multiply(Sine(pitch), volume), level), phaseStart) instead of Sine(volume, pitch, level, phaseStart).", true), UsedImplicitly]
        public Sine Sine(Outlet volume, Outlet pitch, Outlet level, Outlet phaseStart) => throw new NotSupportedException();

        [Obsolete("Use Delay instead.", true), UsedImplicitly]
        public TimeAdd TimeAdd(Outlet signal = null, Outlet timeDifference = null) => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public Outlet Stretch(Outlet signal, Outlet timeFactor, Outlet origin) => throw new NotSupportedException();
        
        [Obsolete("Use Stretch instead.", true), UsedImplicitly]
        public TimeMultiply TimeMultiply(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null) => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public TimeDivide Squash(Outlet signal, Outlet timeDivider, Outlet origin) => throw new NotSupportedException();

        [Obsolete("Use Squash instead.", true), UsedImplicitly]
        public TimeDivide TimeDivide(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null) => throw new NotSupportedException();

        [Obsolete("Typo. Use TimeSubtract instead.", true), UsedImplicitly]
        public TimeSubstract TimeSubstract(Outlet signal = null, Outlet timeDifference = null) => throw new NotSupportedException();

        [Obsolete("Origin parameter obsolete.", true), UsedImplicitly]
        public TimePower TimePower(Outlet signal, Outlet exponent, Outlet origin) => throw new NotSupportedException();
    }
}
