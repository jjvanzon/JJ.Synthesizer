using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.OperandWishes
{
    // Operands on Entity Objects

    /// <inheritdoc cref="docs._operand"/>
    public static class OperandExtensionWishes
    {
        // A
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet A(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Inlets.ElementAtOrDefault(0)?.Input;
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetA(this Operator entity, Outlet a)
        {
            if (entity              == null) throw new ArgumentNullException(nameof(entity));
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
            return entity.Inlets.ElementAtOrDefault(1)?.Input;
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetB(this Operator entity, Outlet b)
        {
            if (entity              == null) throw new ArgumentNullException(nameof(entity));
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
        public static Outlet Pitch(this Outlet entity) => PitchIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetPitch(this Outlet entity, Outlet b)
        {
            AssertPitchIsSupported(entity);
            SetB(entity, b);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Pitch(this Operator entity) => PitchIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetPitch(this Operator entity, Outlet b)
        {
            AssertPitchIsSupported(entity);
            SetB(entity, b);
        }
        
        private static void AssertPitchIsSupported(Outlet entity)
        {
            if (!PitchIsSupported(entity)) throw new Exception("The Operator does not have a Pitch Inlet.");
        }
        
        private static void AssertPitchIsSupported(Operator entity)
        {
            if (!PitchIsSupported(entity)) throw new Exception("The Operator does not have a Pitch Inlet.");
        }
        
        public static bool PitchIsSupported(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return PitchIsSupported(entity.Operator);
        }
        
        public static bool PitchIsSupported(this Operator entity)
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
        /// <inheritdoc cref="docs._operand"/>
        public static bool FrequencyIsSupported(this Outlet entity) => PitchIsSupported(entity);
        /// <inheritdoc cref="docs._operand"/>
        public static bool FrequencyIsSupported(this Operator entity) => PitchIsSupported(entity);
        
        // Signal
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Signal(this Outlet entity) => SignalIsSupported(entity) ? A(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetSignal(this Outlet entity, Outlet a)
        {
            AssertSignalIsSupported(entity);
            SetA(entity, a);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Signal(this Operator entity) => SignalIsSupported(entity) ? A(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetSignal(this Operator entity, Outlet a)
        {
            AssertSignalIsSupported(entity);
            SetA(entity, a);
        }
        
        private static void AssertSignalIsSupported(Outlet entity)
        {
            if (!SignalIsSupported(entity)) throw new Exception("The Operator does not have a Signal Inlet.");
        }
        
        private static void AssertSignalIsSupported(Operator entity)
        {
            if (!SignalIsSupported(entity)) throw new Exception("The Operator does not have a Signal Inlet.");
        }
        
        public static bool SignalIsSupported(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SignalIsSupported(entity.Operator);
        }
        
        public static bool SignalIsSupported(this Operator entity)
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
        public static Outlet Base(this Outlet entity) => BaseIsSupported(entity) ? A(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetBase(this Outlet entity, Outlet a)
        {
            AssertBaseIsSupported(entity);
            SetA(entity, a);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Base(this Operator entity) => BaseIsSupported(entity) ? A(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetBase(this Operator entity, Outlet a)
        {
            AssertBaseIsSupported(entity);
            SetA(entity, a);
        }
        
        private static void AssertBaseIsSupported(Outlet entity)
        {
            if (!BaseIsSupported(entity)) throw new Exception("The Operator does not have a Base Inlet.");
        }
        
        private static void AssertBaseIsSupported(Operator entity)
        {
            if (!BaseIsSupported(entity)) throw new Exception("The Operator does not have a Base Inlet.");
        }
        
        public static bool BaseIsSupported(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return BaseIsSupported(entity.Operator);
        }
        
        public static bool BaseIsSupported(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsPower()) return true;
            return false;
        }
        
        // Exponent
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Exponent(this Outlet entity) => ExponentIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetExponent(this Outlet entity, Outlet b)
        {
            AssertExponentIsSupported(entity);
            SetB(entity, b);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Exponent(this Operator entity) => ExponentIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetExponent(this Operator entity, Outlet b)
        {
            AssertExponentIsSupported(entity);
            SetB(entity, b);
        }
        
        private static void AssertExponentIsSupported(Outlet entity)
        {
            if (!ExponentIsSupported(entity)) throw new Exception("The Operator does not have a Exponent Inlet.");
        }
        
        private static void AssertExponentIsSupported(Operator entity)
        {
            if (!ExponentIsSupported(entity)) throw new Exception("The Operator does not have a Exponent Inlet.");
        }
        
        public static bool ExponentIsSupported(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return ExponentIsSupported(entity.Operator);
        }
        
        public static bool ExponentIsSupported(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsPower()) return true;
            if (entity.IsTimePower()) return true;
            return false;
        }
        
        // TimeDifference
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet TimeDifference(this Outlet entity) => TimeDifferenceIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetTimeDifference(this Outlet entity, Outlet b)
        {
            AssertTimeDifferenceIsSupported(entity);
            SetB(entity, b);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet TimeDifference(this Operator entity) => TimeDifferenceIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetTimeDifference(this Operator entity, Outlet b)
        {
            AssertTimeDifferenceIsSupported(entity);
            SetB(entity, b);
        }
        
        private static void AssertTimeDifferenceIsSupported(Outlet entity)
        {
            if (!TimeDifferenceIsSupported(entity)) throw new Exception("The Operator does not have a TimeDifference Inlet.");
        }
        
        private static void AssertTimeDifferenceIsSupported(Operator entity)
        {
            if (!TimeDifferenceIsSupported(entity)) throw new Exception("The Operator does not have a TimeDifference Inlet.");
        }
        
        public static bool TimeDifferenceIsSupported(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return TimeDifferenceIsSupported(entity.Operator);
        }
        
        public static bool TimeDifferenceIsSupported(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsDelay()) return true;
            if (entity.IsSkip()) return true;
            return false;
        }
        
        // TimeScale
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet TimeScale(this Outlet entity)
            => TimeScaleIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetTimeScale(this Outlet entity, Outlet b)
        {
            AssertTimeScaleIsSupported(entity);
            SetB(entity, b);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet TimeScale(this Operator entity) => TimeScaleIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetTimeScale(this Operator entity, Outlet b)
        {
            AssertTimeScaleIsSupported(entity);
            SetB(entity, b);
        }
        
        private static void AssertTimeScaleIsSupported(Outlet entity)
        {
            if (!TimeScaleIsSupported(entity)) throw new Exception("The Operator does not have a TimeScale Inlet.");
        }
        
        private static void AssertTimeScaleIsSupported(Operator entity)
        {
            if (!TimeScaleIsSupported(entity)) throw new Exception("The Operator does not have a TimeScale Inlet.");
        }
        
        public static bool TimeScaleIsSupported(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return TimeScaleIsSupported(entity.Operator);
        }
        
        public static bool TimeScaleIsSupported(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.IsStretch()) return true;
            return false;
        }
        
        // SpeedFactor
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet SpeedFactor(this Outlet entity) => SpeedFactorIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetSpeedFactor(this Outlet entity, Outlet b)
        {
            AssertSpeedFactorIsSupported(entity);
            SetB(entity, b);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet SpeedFactor(this Operator entity) => SpeedFactorIsSupported(entity) ? B(entity) : null;
        
        /// <inheritdoc cref="docs._operand"/>
        public static void SetSpeedFactor(this Operator entity, Outlet b)
        {
            AssertSpeedFactorIsSupported(entity);
            SetB(entity, b);
        }
        
        private static void AssertSpeedFactorIsSupported(Outlet entity)
        {
            if (!SpeedFactorIsSupported(entity)) throw new Exception("The Operator does not have a SpeedFactor Inlet.");
        }
        
        private static void AssertSpeedFactorIsSupported(Operator entity)
        {
            if (!SpeedFactorIsSupported(entity)) throw new Exception("The Operator does not have a SpeedFactor Inlet.");
        }
        
        public static bool SpeedFactorIsSupported(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SpeedFactorIsSupported(entity.Operator);
        }
        
        public static bool SpeedFactorIsSupported(this Operator entity)
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
        public static Outlet Operand(this Outlet entity, int index)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Operand(entity.Operator, index);
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public static Outlet Operand(this Operator entity, int index)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Inlets[index].Input;
        }
        
        // Origin (Obsolete)
        
        [Obsolete("Rarely used because default origin 0 usually works. " +
                  "Otherwise consider use separate operators like Shift and Stretch instead.")]
        public static Outlet Origin(this Operator entity) => entity.Inlets.ElementAtOrDefault(2)?.Input;
    }
}