using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Dispatch through a base class is faster than using an interface.

    /// <summary>
    /// If you have child OperatorCalculators use OperatorCalculatorBase_WithChildCalculators instead.
    /// </summary>
    internal abstract class OperatorCalculatorBase
    {
        //public OperatorCalculatorBase()
        //{
        //    #if DEBUG
        //        Type type = GetType();
        //        FieldInfo dimensionStackField = type.GetField("_dimensionStack", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (dimensionStackField != null)
        //        {
        //            DimensionStack dimensionStack = (DimensionStack)dimensionStackField.GetValue(this);
        //            int dimensionStackPosition = dimensionStack.Count - 1;
        //            Debug.WriteLine($"DimensionStack position: {type.Name} - {dimensionStackPosition}", "DimensionStacks");
        //        }
        //    #endif
        //}

        public abstract double Calculate();

        /// <summary> Does nothing </summary>
        public virtual void Reset() { }
    }
}
