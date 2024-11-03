using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Persistence.Synthesizer;
using System;
using JJ.Framework.Reflection;
using System.Linq;
using JJ.Business.Synthesizer.Wishes.Helpers;
using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes
{
    // Related Object Wishes
    
    // Operator Extension Method

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_operatorextensionwishes"/>
        public Operator Operator => _this.Operator;
    }

    /// <inheritdoc cref="_operatorextensionwishes"/>
    public static partial class RelatedObjectsExtensionWishes
    {
        /// <inheritdoc cref="_operatorextensionwishes"/>
        public static Operator Operator(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }
        
        /// <inheritdoc cref="_operatorextensionwishes"/>
        public static Operator Operator(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }
    }

    // Related Objects in FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_getsample" />
        public Sample Sample() => _this.Sample();

        /// <inheritdoc cref="_getsamplewrapper" />
        public SampleOperatorWrapper GetSampleWrapper() => _this.GetSampleWrapper();

        /// <inheritdoc cref="_getcurve" />"/>
        public Curve Curve() => _this.Curve();

        /// <inheritdoc cref="_getcurvewrapper"/>
        public CurveInWrapper GetCurveWrapper() => _this.GetCurveWrapper();
    }

    // Operands

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_operand"/>
        public FluentOutlet A 
        { 
            get => _[Outlet.A()]; 
            set => Outlet.SetA(value); 
        }
        
        /// <inheritdoc cref="_operand"/>
        public FluentOutlet B
        {
            get => _[Outlet.B()];
            set => Outlet.SetB(value);
        }

        /// <inheritdoc cref="_operand"/>
        public FluentOutlet Frequency
        {
            get => _[Outlet.Frequency()];
            set => Outlet.SetFrequency(value);
        }

        /// <inheritdoc cref="_operand"/>
        public FluentOutlet Pitch
        {
            get => _[Outlet.Pitch()];
            set => Outlet.SetPitch(value);
        }

        /// <inheritdoc cref="_operand"/>
        public FluentOutlet Signal
        {
            get => _[Outlet.Signal()];
            set => Outlet.SetSignal(value);
        }

        /// <inheritdoc cref="_operand"/>
        public FluentOutlet Base
        {
            get => _[Outlet.Base()];
            set => Outlet.SetBase(value);
        }

        /// <inheritdoc cref="_operand"/>
        public FluentOutlet Exponent
        {
            get => _[Outlet.Exponent()];
            set => Outlet.SetExponent(value);
        }

        /// <inheritdoc cref="_operand"/>
        public FluentOutlet TimeDifference
        {
            get => _[Outlet.TimeDifference()];
            set => Outlet.SetTimeDifference(value);
        }

        /// <inheritdoc cref="_operand"/>
        public FluentOutlet TimeScale
        {
            get => _[Outlet.TimeScale()];
            set => Outlet.SetTimeScale(value);
        }

        /// <inheritdoc cref="_operand"/>
        public FluentOutlet SpeedFactor
        {
            get => _[Outlet.SpeedFactor()];
            set => Outlet.SetSpeedFactor(value);
        }

        public FluentOperandList Operands { get; }
    }


    public static partial class RelatedObjectsExtensionWishes
    { 
        // TODO: Make conditional and throw?
        // TODO: Bounds checks?

        /// <inheritdoc cref="_operand"/>
        public static Outlet A(this Outlet entity) => entity.Operator.A();
        /// <inheritdoc cref="_operand"/>
        public static void SetA(this Outlet entity, Outlet a) => entity.Operator.SetA(a);
        /// <inheritdoc cref="_operand"/>
        public static Outlet A(this Operator entity) => entity.Inlets.ElementAt(0)?.Input;
        /// <inheritdoc cref="_operand"/>
        public static void SetA(this Operator entity, Outlet a) => entity.Inlets.ElementAt(0).Input = a;
        
        /// <inheritdoc cref="_operand"/>
        public static Outlet B(this Outlet entity) => entity.Operator.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetB(this Outlet entity, Outlet b) => entity.Operator.SetB(b);
        /// <inheritdoc cref="_operand"/>
        public static Outlet B(this Operator entity) => entity.Inlets.ElementAt(1)?.Input;
        /// <inheritdoc cref="_operand"/>
        public static void SetB(this Operator entity, Outlet b) => entity.Inlets.ElementAt(1).Input = b;
        
        /// <inheritdoc cref="_operand"/>
        public static Outlet Pitch(this Outlet entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetPitch(this Outlet entity, Outlet b) => entity.SetB(b);
        /// <inheritdoc cref="_operand"/>
        public static Outlet Pitch(this Operator entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetPitch(this Operator entity, Outlet b) => entity.SetB(b);
        
        /// <inheritdoc cref="_operand"/>
        public static Outlet Frequency(this Outlet entity) => entity.Pitch();
        /// <inheritdoc cref="_operand"/>
        public static void SetFrequency(this Outlet entity, Outlet b) => entity.SetPitch(b);
        /// <inheritdoc cref="_operand"/>
        public static Outlet Frequency(this Operator entity) => entity.Pitch();
        /// <inheritdoc cref="_operand"/>
        public static void SetFrequency(this Operator entity, Outlet b) => entity.SetPitch(b);
        
        /// <inheritdoc cref="_operand"/>
        public static Outlet Signal(this Outlet entity) => entity.A();
        /// <inheritdoc cref="_operand"/>
        public static void SetSignal(this Outlet entity, Outlet a) => entity.SetA(a);
        /// <inheritdoc cref="_operand"/>
        public static Outlet Signal(this Operator entity) => entity.A();
        /// <inheritdoc cref="_operand"/>
        public static void SetSignal(this Operator entity, Outlet a) => entity.SetA(a);
        
        /// <inheritdoc cref="_operand"/>
        public static Outlet Base(this Outlet entity) => entity.A();
        /// <inheritdoc cref="_operand"/>
        public static void SetBase(this Outlet entity, Outlet a) => entity.SetA(a);
        /// <inheritdoc cref="_operand"/>
        public static Outlet Base(this Operator entity) => entity.A();
        /// <inheritdoc cref="_operand"/>
        public static void SetBase(this Operator entity, Outlet a) => entity.SetA(a);
        
        /// <inheritdoc cref="_operand"/>
        public static Outlet Exponent(this Outlet entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetExponent(this Outlet entity, Outlet b) => entity.SetB(b);
        /// <inheritdoc cref="_operand"/>
        public static Outlet Exponent(this Operator entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetExponent(this Operator entity, Outlet b) => entity.SetB(b);
        
        /// <inheritdoc cref="_operand"/>
        public static Outlet TimeDifference(this Outlet entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetTimeDifference(this Outlet entity, Outlet b) => entity.SetB(b);
        /// <inheritdoc cref="_operand"/>
        public static Outlet TimeDifference(this Operator entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetTimeDifference(this Operator entity, Outlet b) => entity.SetB(b);
        
        /// <inheritdoc cref="_operand"/>
        public static Outlet TimeScale(this Outlet entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetTimeScale(this Outlet entity, Outlet b) => entity.SetB(b);
        /// <inheritdoc cref="_operand"/>
        public static Outlet TimeScale(this Operator entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetTimeScale(this Operator entity, Outlet b) => entity.SetB(b);
        
        /// <inheritdoc cref="_operand"/>
        public static Outlet SpeedFactor(this Outlet entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetSpeedFactor(this Outlet entity, Outlet b) => entity.SetB(b);
        /// <inheritdoc cref="_operand"/>
        public static Outlet SpeedFactor(this Operator entity) => entity.B();
        /// <inheritdoc cref="_operand"/>
        public static void SetSpeedFactor(this Operator entity, Outlet b) => entity.SetB(b);
        
        ///// <inheritdoc cref="_operand"/>
        public static OperandList Operands(this Outlet entity) => entity.Operator.Operands();
        ///// <inheritdoc cref="_operand"/>
        public static OperandList Operands(this Operator entity) => new OperandList(entity);
        
        ///// <inheritdoc cref="_operand"/>
        public static Outlet Operands(this Outlet entity, int index) => entity.Operator.Operands(index);
        ///// <inheritdoc cref="_operand"/>
        public static Outlet Operands(this Operator entity, int index) => entity.Inlets[index].Input;

        /// <inheritdoc cref="_operand"/>
        public static Outlet Result(this Operator entity) => entity.Outlets[0];
    }

    // Operand Origin

    public partial class FluentOutlet
    {
        [Obsolete("Rarely used because default origin 0 usually works. " +
                  "Otherwise consider use separate operators like Shift and Stretch instead.")]
        public FluentOutlet Origin => _[Outlet.Operator?.Inlets.ElementAt(2)?.Input];

    }

    public static partial class RelatedObjectsExtensionWishes
    { 
        [Obsolete("Rarely used because default origin 0 usually works. " +
                  "Otherwise consider use separate operators like Shift and Stretch instead.")]
        public static Outlet Origin(this Operator entity) => entity.Inlets.ElementAt(2)?.Input;
    }
}
