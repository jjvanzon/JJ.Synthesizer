using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase
    {
        protected Operator _operator;

        public OperatorWrapperBase(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            _operator = op;
        }

        /// <summary> Wrapped object summary>
        public Operator Operator { get { return _operator; } }

        public string Name
        {
            get { return _operator.Name; }
            set { _operator.Name = value; }
        }

        /// <summary> Gets an item out of the sorted _operator.Inlets and verifies that the index is valid in the list. </summary>
        protected Inlet GetInlet(int index)
        {
            IList<Inlet> sortedInlets = GetSortedInlets();
            if (index >= sortedInlets.Count)
            {
                throw new Exception(String.Format("Sorted inlets does not have index [{0}].", index));
            }
            return sortedInlets[index];
        }

        /// <summary> Gets an item out of the sorted _operator.Outlets and verifies that the index is valid in the list. </summary>
        protected Outlet GetOutlet(int index)
        {
            IList<Outlet> sortedOutlets = GetSortedOutlets();
            if (index >= sortedOutlets.Count)
            {
                throw new Exception(String.Format("Sorted outlets does not have index [{0}].", index));
            }
            return sortedOutlets[index];
        }

        /// <summary>
        /// Gets the input outlet of a specific inlet.
        /// If it is null, and the inlet has a default value, a fake Number Operator is created and its outlet returned.
        /// If the inlet has no default value either, null is returned.
        /// </summary>
        protected Outlet GetInputOutletOrDefault(int index)
        {
            Inlet inlet = GetInlet(index);

            if (inlet.InputOutlet != null)
            {
                return inlet.InputOutlet;
            }

            if (inlet.DefaultValue.HasValue)
            {
                Number_OperatorWrapper dummyNumberOperator = CreateDummyNumberOperator(inlet.DefaultValue.Value);
                return dummyNumberOperator.Result;
            }

            return null;
        }

        public static implicit operator Operator(OperatorWrapperBase wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Operator;
        }

        // Helpers

        private IList<Inlet> GetSortedInlets()
        {
            IList<Inlet> sortedInlets = _operator.Inlets.OrderBy(x => x.ListIndex).ToArray();
            return sortedInlets;
        }

        private IList<Outlet> GetSortedOutlets()
        {
            IList<Outlet> sortedOutlets = _operator.Outlets.OrderBy(x => x.ListIndex).ToArray();
            return sortedOutlets;
        }

        private Number_OperatorWrapper CreateDummyNumberOperator(double number)
        {
            var operatorType = new OperatorType();
            operatorType.ID = (int)OperatorTypeEnum.Number;
            operatorType.Name = OperatorTypeEnum.Number.ToString();

            var op = new Operator();
            op.LinkTo(operatorType);

            var outlet = new Outlet();
            outlet.LinkTo(op);

            var wrapper = new Number_OperatorWrapper(op);
            wrapper.Number = number;

            return wrapper;
        }
    }
}
