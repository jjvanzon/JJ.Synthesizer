using JJ.Persistence.Synthesizer;
using System;
using System.Linq;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Wishes.Helpers;
 
namespace JJ.Business.Synthesizer.Wishes
{
    // Operands in FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet A 
        { 
            get => _[_wrappedOutlet.A()]; 
            set => _wrappedOutlet.SetA(value); 
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet B
        {
            get => _[_wrappedOutlet.B()];
            set => _wrappedOutlet.SetB(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet Frequency
        {
            get => _[_wrappedOutlet.Frequency()];
            set => _wrappedOutlet.SetFrequency(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet Pitch
        {
            get => _[_wrappedOutlet.Pitch()];
            set => _wrappedOutlet.SetPitch(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet Signal
        {
            get => _[_wrappedOutlet.Signal()];
            set => _wrappedOutlet.SetSignal(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet Base
        {
            get => _[_wrappedOutlet.Base()];
            set => _wrappedOutlet.SetBase(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet Exponent
        {
            get => _[_wrappedOutlet.Exponent()];
            set => _wrappedOutlet.SetExponent(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet TimeDifference
        {
            get => _[_wrappedOutlet.TimeDifference()];
            set => _wrappedOutlet.SetTimeDifference(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet TimeScale
        {
            get => _[_wrappedOutlet.TimeScale()];
            set => _wrappedOutlet.SetTimeScale(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FluentOutlet SpeedFactor
        {
            get => _[_wrappedOutlet.SpeedFactor()];
            set => _wrappedOutlet.SetSpeedFactor(value);
        }

        public FluentOperandList Operands { get; }
    }

    // Operands on Entity Objects
    
    /// <inheritdoc cref="docs._operand"/>
    public static partial class OperandExtensionWishes
    { 
        // A
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet A(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Inlets.Count == 0) throw new ArgumentException("entity.Inlets.Count == 0");
            return entity.Inlets[0].Input;
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetA(this Operator entity, Outlet a)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Inlets.Count == 0) throw new ArgumentException("entity.Inlets.Count == 0");
            entity.Inlets[0].LinkTo(a);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet A(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return A(entity.Operator);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetA(this Outlet entity, Outlet a)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            SetA(entity.Operator, a);
        }

        // B

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet B(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Inlets.Count < 2) throw new ArgumentException("entity.Inlets.Count < 2");
            return entity.Inlets[1].Input;
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetB(this Operator entity, Outlet b)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Inlets.Count < 2) throw new ArgumentException("entity.Inlets.Count < 2");
            entity.Inlets[1].LinkTo(b);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet B(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return B(entity.Operator);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetB(this Outlet entity, Outlet b)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            SetB(entity.Operator, b);
        }

        // Pitch
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Pitch(this Outlet entity)
        {
            AssertHasPitch(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetPitch(this Outlet entity, Outlet b)
        {
            AssertHasPitch(entity);
            SetB(entity, b);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Pitch(this Operator entity)
        {
            AssertHasPitch(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetPitch(this Operator entity, Outlet b)
        {
            AssertHasPitch(entity);
            SetB(entity, b);
        }
        
        private static void AssertHasPitch(Outlet entity)
        {
            if (!HasPitch(entity)) throw new Exception("The Operator does not have a Pitch Inlet.");
        }

        private static void AssertHasPitch(Operator entity)
        {
            if (!HasPitch(entity)) throw new Exception("The Operator does not have a Pitch Inlet.");
        }

        public static bool HasPitch(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return HasPitch(entity.Operator);
        }

        public static bool HasPitch(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsSine()) return true;
            return false;
        }

        // Frequency

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Frequency(this Outlet entity) => Pitch(entity);
        /// <inheritdoc cref="docs._operand"/>
        public static void SetFrequency(this Outlet entity, Outlet b) => SetPitch(entity, b);
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Frequency(this Operator entity) => Pitch(entity);
        /// <inheritdoc cref="docs._operand"/>
        public static void SetFrequency(this Operator entity, Outlet b) => SetPitch(entity, b);

        // Signal

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Signal(this Outlet entity)
        {
            AssertHasSignal(entity);
            return A(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetSignal(this Outlet entity, Outlet a)
        {
            AssertHasSignal(entity);
            SetA(entity, a);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Signal(this Operator entity)
        {
            AssertHasSignal(entity);
            return A(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetSignal(this Operator entity, Outlet a)
        {
            AssertHasSignal(entity);
            SetA(entity, a);
        }

        private static void AssertHasSignal(Outlet entity)
        {
            if (!HasSignal(entity)) throw new Exception("The Operator does not have a Signal Inlet.");
        }

        private static void AssertHasSignal(Operator entity)
        {
            if (!HasSignal(entity)) throw new Exception("The Operator does not have a Signal Inlet.");
        }

        public static bool HasSignal(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return HasSignal(entity.Operator);
        }

        public static bool HasSignal(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsDelay()) return true;
            if (entity.IsSkip()) return true;
            if (entity.IsStretch()) return true;
            if (entity.IsSpeedUp()) return true;
            if (entity.IsTimePower()) return true;
            return false;
        }

        // Base

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Base(this Outlet entity)
        {
            AssertHasBase(entity);
            return A(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetBase(this Outlet entity, Outlet a)
        {
            AssertHasBase(entity);
            SetA(entity, a);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Base(this Operator entity)
        {
            AssertHasBase(entity);
            return A(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetBase(this Operator entity, Outlet a)
        {
            AssertHasBase(entity);
            SetA(entity, a);
        }

        private static void AssertHasBase(Outlet entity)
        {
            if (!HasBase(entity)) throw new Exception("The Operator does not have a Base Inlet.");
        }

        private static void AssertHasBase(Operator entity)
        {
            if (!HasBase(entity)) throw new Exception("The Operator does not have a Base Inlet.");
        }

        public static bool HasBase(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return HasBase(entity.Operator);
        }

        public static bool HasBase(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsPower()) return true;
            return false;
        }

        // Exponent

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Exponent(this Outlet entity)
        {
            AssertHasExponent(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetExponent(this Outlet entity, Outlet b)
        {
            AssertHasExponent(entity);
            SetB(entity, b);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Exponent(this Operator entity)
        {
            AssertHasExponent(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetExponent(this Operator entity, Outlet b)
        {
            AssertHasExponent(entity);
            SetB(entity, b);
        }

        private static void AssertHasExponent(Outlet entity)
        {
            if (!HasExponent(entity)) throw new Exception("The Operator does not have a Exponent Inlet.");
        }

        private static void AssertHasExponent(Operator entity)
        {
            if (!HasExponent(entity)) throw new Exception("The Operator does not have a Exponent Inlet.");
        }

        public static bool HasExponent(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return HasExponent(entity.Operator);
        }

        public static bool HasExponent(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsPower()) return true;
            if (entity.IsTimePower()) return true;
            return false;
        }

        // TimeDifference

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet TimeDifference(this Outlet entity)
        {
            AssertHasTimeDifference(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetTimeDifference(this Outlet entity, Outlet b)
        {
            AssertHasTimeDifference(entity);
            SetB(entity, b);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet TimeDifference(this Operator entity)
        {
            AssertHasTimeDifference(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetTimeDifference(this Operator entity, Outlet b)
        {
            AssertHasTimeDifference(entity);
            SetB(entity, b);
        }

        private static void AssertHasTimeDifference(Outlet entity)
        {
            if (!HasTimeDifference(entity)) throw new Exception("The Operator does not have a TimeDifference Inlet.");
        }

        private static void AssertHasTimeDifference(Operator entity)
        {
            if (!HasTimeDifference(entity)) throw new Exception("The Operator does not have a TimeDifference Inlet.");
        }

        public static bool HasTimeDifference(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return HasTimeDifference(entity.Operator);
        }

        public static bool HasTimeDifference(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsDelay()) return true;
            if (entity.IsSkip()) return true;
            return false;
        }

        // TimeScale

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet TimeScale(this Outlet entity)
        {
            AssertHasTimeScale(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetTimeScale(this Outlet entity, Outlet b)
        {
            AssertHasTimeScale(entity);
            SetB(entity, b);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet TimeScale(this Operator entity)
        {
            AssertHasTimeScale(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetTimeScale(this Operator entity, Outlet b)
        {
            AssertHasTimeScale(entity);
            SetB(entity, b);
        }

        private static void AssertHasTimeScale(Outlet entity)
        {
            if (!HasTimeScale(entity)) throw new Exception("The Operator does not have a TimeScale Inlet.");
        }

        private static void AssertHasTimeScale(Operator entity)
        {
            if (!HasTimeScale(entity)) throw new Exception("The Operator does not have a TimeScale Inlet.");
        }

        public static bool HasTimeScale(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return HasTimeScale(entity.Operator);
        }

        public static bool HasTimeScale(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsStretch()) return true;
            return false;
        }

        // SpeedFactor

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet SpeedFactor(this Outlet entity)
        {
            AssertHasSpeedFactor(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetSpeedFactor(this Outlet entity, Outlet b)
        {
            AssertHasSpeedFactor(entity);
            SetB(entity, b);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet SpeedFactor(this Operator entity)
        {
            AssertHasSpeedFactor(entity);
            return B(entity);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static void SetSpeedFactor(this Operator entity, Outlet b)
        {
            AssertHasSpeedFactor(entity);
            SetB(entity, b);
        }

        private static void AssertHasSpeedFactor(Outlet entity)
        {
            if (!HasSpeedFactor(entity)) throw new Exception("The Operator does not have a SpeedFactor Inlet.");
        }

        private static void AssertHasSpeedFactor(Operator entity)
        {
            if (!HasSpeedFactor(entity)) throw new Exception("The Operator does not have a SpeedFactor Inlet.");
        }

        public static bool HasSpeedFactor(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return HasSpeedFactor(entity.Operator);
        }

        public static bool HasSpeedFactor(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsSpeedUp()) return true;
            return false;
        }

        // Result

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Result(this Operator entity) => entity.Outlets[0];

        // OperandsList

        /// <inheritdoc cref="docs._operand"/>
        public static OperandList Operands(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Operands(entity.Operator);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static OperandList Operands(this Operator entity) => new OperandList(entity);

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Operands(this Outlet entity, int index)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Operands(entity.Operator, index);
        }

        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Operands(this Operator entity, int index)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Inlets[index].Input;
        }
    }

    // Operand Origin

    public partial class FluentOutlet
    {
        [Obsolete("Rarely used because default origin 0 usually works. " +
                  "Otherwise consider use separate operators like Shift and Stretch instead.")]
        public FluentOutlet Origin => _[_wrappedOutlet.Operator?.Inlets.ElementAt(2)?.Input];
    }

    /// <inheritdoc cref="docs._operand"/>
    public static partial class OperandExtensionWishes
    { 
        [Obsolete("Rarely used because default origin 0 usually works. " +
                  "Otherwise consider use separate operators like Shift and Stretch instead.")]
        public static Outlet Origin(this Operator entity) => entity.Inlets.ElementAt(2)?.Input;
    }
}
