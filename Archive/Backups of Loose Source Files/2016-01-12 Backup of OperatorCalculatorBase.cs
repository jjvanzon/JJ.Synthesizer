//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    // Dispatch through a base class is faster than using an interface.

//    /// <summary>
//    /// If you have child OperatorCalculators or your calculator needs phase tracking, use one of the other base classes:
//    /// OperatorCalculatorBase_WithPhaseTracking /
//    /// OperatorCalculatorBase_WithChildCalculators /
//    /// OperatorCalculatorBase_WithChildCalculators_WithPhaseTracking.
//    /// </summary>
//    internal abstract class OperatorCalculatorBase
//    {
//        public abstract double Calculate(double time, int channelIndex);
//        public virtual void ResetPhase(double time) { }
//    }
//}
