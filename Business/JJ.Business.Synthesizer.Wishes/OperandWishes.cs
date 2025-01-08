using JJ.Persistence.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes
{
    // Operands in FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._operand"/>
        public FlowNode A 
        { 
            get => _[_underlyingOutlet.A()]; 
            set => _underlyingOutlet.SetA(value); 
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public FlowNode B
        {
            get => _[_underlyingOutlet.B()];
            set => _underlyingOutlet.SetB(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Frequency
        {
            get => _[_underlyingOutlet.Frequency()];
            set => _underlyingOutlet.SetFrequency(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsFrequency => _underlyingOutlet.FrequencyIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Pitch
        {
            get => _[_underlyingOutlet.Pitch()];
            set => _underlyingOutlet.SetPitch(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsPitch => _underlyingOutlet.PitchIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Signal
        {
            get => _[_underlyingOutlet.Signal()];
            set => _underlyingOutlet.SetSignal(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsSignal => _underlyingOutlet.SignalIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Base
        {
            get => _[_underlyingOutlet.Base()];
            set => _underlyingOutlet.SetBase(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsBase => _underlyingOutlet.BaseIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Exponent
        {
            get => _[_underlyingOutlet.Exponent()];
            set => _underlyingOutlet.SetExponent(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsExponent => _underlyingOutlet.ExponentIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode TimeDifference
        {
            get => _[_underlyingOutlet.TimeDifference()];
            set => _underlyingOutlet.SetTimeDifference(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsTimeDifference => _underlyingOutlet.TimeDifferenceIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode TimeScale
        {
            get => _[_underlyingOutlet.TimeScale()];
            set => _underlyingOutlet.SetTimeScale(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsTimeScale => _underlyingOutlet.TimeScaleIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode SpeedFactor
        {
            get => _[_underlyingOutlet.SpeedFactor()];
            set => _underlyingOutlet.SetSpeedFactor(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsSpeedFactor => _underlyingOutlet.SpeedFactorIsSupported();
    
        // Origin (Obsolete)
        
        [Obsolete("Rarely used because default origin 0 usually works. " +
          "Otherwise consider use separate operators like Shift and Stretch instead.")]
        public FlowNode Origin => _[_underlyingOutlet.Operator?.Inlets.ElementAtOrDefault(2)?.Input];

    }

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
            return entity.Inlets.ElementAtOrDefault(1)?.Input;
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
            
    // Specialized Lists

    public class OperandList : IList<Outlet> 
    {
        private IList<Inlet> _inlets;
        
        internal OperandList(Outlet outlet) => Initialize(outlet);
        internal OperandList(Operator op) => Initialize(op);
        
        public int Count => _inlets.Count;

        public Outlet this[int index]
        {
            get => _inlets[index].Input;
            set => _inlets[index].LinkTo(value);
        }
        
        public int IndexOf(Outlet item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (this[i] == item)
                {
                    return i;
                }
            }

            return -1;
        }
                
        public bool Contains(Outlet item) => _inlets.Any(x => x.Input == item);

        public void Add(Outlet item) => ThrowListCannotBeExtended();

        public void Insert(int index, Outlet item) => ThrowListCannotBeExtended();

        public bool Remove(Outlet item)
        {
            var inlet = _inlets.FirstOrDefault(x => x.Input == item);
            if (inlet != null)
            {
                // TODO: Add UnlinkOutlet to Wishes project. UnlinkWishes or something.
                inlet.LinkTo((Outlet)null);
                return true;
            }

            return false;
        }
        
        public void RemoveAt(int index) => _inlets[index].LinkTo((Outlet)null);

        public void Clear() => _inlets.ForEach(x => x.LinkTo((Outlet)null));

        public void CopyTo(Outlet[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < _inlets.Count) throw new ArgumentException("The number of elements in the source OperandList is greater than the available space from arrayIndex to the end of the destination array.");

            for (var i = 0; i < _inlets.Count; i++)
            {
                array[arrayIndex + i] = _inlets[i].Input;
            }
        }

        public bool IsReadOnly => false;

        public IEnumerator<Outlet> GetEnumerator()
        {
            foreach (var inlet in _inlets)
            {
                yield return inlet.Input;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // Helpers

        private void ThrowListCannotBeExtended()
        {
            throw new NotSupportedException(
                "List cannot be extended." +
                "For it would require internal entity creation " +
                "for which it is missing its internal services.");
        }

        // Field Assignment with Null Checks

        private void Initialize(Outlet outlet) 
        {
            if (outlet == null) throw new NullException(() => outlet);
            Initialize(outlet.Operator);
        }

        private void Initialize(Operator op) 
        {
            if (op == null) throw new NullException(() => op);
            _inlets = op.Inlets;
        }
    }
        
    public class FluentOperandList : IList<FlowNode>
    {
        private readonly FlowNode _parent;
        private readonly OperandList _underlyingList;
        
        public FluentOperandList(FlowNode parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _underlyingList = new OperandList(parent.UnderlyingOutlet);
        }
           
        public int Count => _underlyingList.Count;

        public FlowNode this[int index]
        {
            get
            {
                Outlet outlet = _underlyingList[index];
                return outlet == null ? null : _parent._[outlet];
            }
            set => _underlyingList[index] = value?.UnderlyingOutlet;
        }

        public int IndexOf(FlowNode item) => _underlyingList.IndexOf(item?.UnderlyingOutlet);
        public bool Contains(FlowNode item) => _underlyingList.Contains(item?.UnderlyingOutlet);
        public void Add(FlowNode item) => _underlyingList.Add(item?.UnderlyingOutlet);
        public void Insert(int index, FlowNode item) => _underlyingList.Insert(index, item?.UnderlyingOutlet);
        public bool Remove(FlowNode item) => _underlyingList.Remove(item?.UnderlyingOutlet);
        public void RemoveAt(int index) => _underlyingList.RemoveAt(index);
        public void Clear() => _underlyingList.Clear();
        public bool IsReadOnly => _underlyingList.IsReadOnly;
        
        public void CopyTo(FlowNode[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < _underlyingList.Count) throw new ArgumentException("The number of elements in the source OperandList is greater than the available space from arrayIndex to the end of the destination array.");

            for (int i = 0; i < _underlyingList.Count; i++)
            {
                Outlet outlet = _underlyingList[i];
                FlowNode flowNode = null;
                if (outlet != null)
                {
                    flowNode = _parent[outlet];
                }
                array[arrayIndex + i] = flowNode;
            }
        }

        public IEnumerator<FlowNode> GetEnumerator()
        {
            return _underlyingList
                   // Convert each Outlet to FlowNode
                   .Select(outlet => outlet == null ? null : _parent[outlet]) 
                   .GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
