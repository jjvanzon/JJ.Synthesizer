using System.Collections;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Inlets : IEnumerable<Inlet>
    {
        private readonly Operator _operator;

        internal OperatorWrapper_Inlets(Operator op)
        {
            _operator = op ?? throw new NullException(() => op);
        }

        // TODO: Composite keys Name-Position and DimensionEnum-Position have also become supported.

        public Inlet this[string name] => InletOutletSelector.GetInlet(_operator, name);
        public Inlet TryGet(string name) => InletOutletSelector.TryGetInlet(_operator, name);
        public IList<Inlet> GetMany(string name) => InletOutletSelector.GetInlets(_operator, name);

        /// <summary> not fast </summary>
        public Inlet this[int index] => InletOutletSelector.GetInlet(_operator, index);
        public Inlet TryGet(int index) => InletOutletSelector.TryGetInlet(_operator, index);
        public IList<Inlet> GetMany(int index) => InletOutletSelector.GetInlets(_operator, index);

        public Inlet this[DimensionEnum dimensionEnum] => InletOutletSelector.GetInlet(_operator, dimensionEnum);
        public Inlet TryGet(DimensionEnum dimensionEnum) => InletOutletSelector.TryGetInlet(_operator, dimensionEnum);
        public IList<Inlet> GetMany(DimensionEnum dimensionEnum) => InletOutletSelector.GetInlets(_operator, dimensionEnum);

        public int Count => _operator.Inlets.Count;
        public IEnumerator<Inlet> GetEnumerator() => _operator.Inlets.Sort().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _operator.Inlets.Sort().GetEnumerator();
    }
}
