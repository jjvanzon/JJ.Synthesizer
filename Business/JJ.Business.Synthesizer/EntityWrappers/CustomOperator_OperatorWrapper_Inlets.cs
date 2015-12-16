using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper_Inlets : IEnumerable<Inlet>
    {
        private Operator _operator;

        internal CustomOperator_OperatorWrapper_Inlets(Operator op)
        {
            _operator = op;
        }

        public Inlet this[string name]
        {
            get { return OperatorHelper.GetInlet(_operator, name); }
        }

        /// <summary> not fast </summary>
        public Inlet this[int index]
        {
            get { return OperatorHelper.GetInlet(_operator, index); }
        }

        public Inlet this[InletTypeEnum inletTypeEnum]
        {
            get { return OperatorHelper.GetInlet(_operator, inletTypeEnum); }
        }

        public IEnumerator<Inlet> GetEnumerator()
        {
            foreach (Inlet inlet in OperatorHelper.GetSortedInlets(_operator))
            {
                yield return inlet;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Inlet inlet in OperatorHelper.GetSortedInlets(_operator))
            {
                yield return inlet;
            }
        }
    }
}
