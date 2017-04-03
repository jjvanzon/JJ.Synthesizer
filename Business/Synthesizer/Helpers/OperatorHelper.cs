using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class OperatorHelper
    {
        // Sorting

        public static IList<Inlet> GetSortedInlets(Operator op)
        {
            IList<Inlet> sortedInlets = EnumerateSortedInlets(op).ToArray();
            return sortedInlets;
        }

        public static IEnumerable<Inlet> EnumerateSortedInlets(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            IEnumerable<Inlet> enumerable = op.Inlets.OrderBy(x => x.ListIndex);
            return enumerable;
        }

        public static IList<Outlet> GetSortedOutlets(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();
            return sortedOutlets;
        }

        public static IList<Outlet> GetSortedInputOutlets(Operator op)
        {
            IList<Outlet> outlets = EnumerateSortedInputOutlets(op).ToArray();
            return outlets;
        }

        public static IEnumerable<Outlet> EnumerateSortedInputOutlets(Operator op)
        {
            IEnumerable<Outlet> enumerable = GetSortedInlets(op).Select(x => x.InputOutlet);
            return enumerable;
        }

        // Get Inlet

        /// <param name="listIndex">List indices are not necessarily consecutive.</param>
        public static Inlet GetInlet(Operator op, int listIndex)
        {
            Inlet inlet = TryGetInlet(op, listIndex);
            if (inlet == null)
            {
                throw new NotFoundException<Inlet>(new { listIndex });
            }
            return inlet;
        }

        /// <param name="listIndex">List indices are not necessarily consecutive.</param>
        public static Inlet TryGetInlet(Operator op, int listIndex)
        {
            if (op == null) throw new NullException(() => op);

            IList<Inlet> inlets = op.Inlets.Where(x => x.ListIndex == listIndex).ToArray();
            switch (inlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return inlets[0];

                default:
                    throw new NotUniqueException<Operator>(new { listIndex });
            }
        }

        public static Inlet GetInlet(Operator op, string name)
        {
            Inlet inlet = TryGetInlet(op, name);
            if (inlet == null)
            {
                throw new Exception($"Inlet '{name}' not found.");
            }
            return inlet;
        }

        public static Inlet TryGetInlet(Operator op, string name)
        {
            if (op == null) throw new NullException(() => op);

            IList<Inlet> inlets = op.Inlets.Where(x => string.Equals(x.Name, name)).ToArray();
            switch (inlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return inlets[0];

                default:
                    throw new NotUniqueException<Inlet>(new { name });
            }
        }

        public static Inlet GetInlet(Operator op, DimensionEnum dimensionEnum)
        {
            Inlet inlet = TryGetInlet(op, dimensionEnum);
            if (inlet == null)
            {
                throw new NotFoundException<Inlet>(new { dimensionEnum });
            }
            return inlet;
        }

        public static Inlet TryGetInlet(Operator op, DimensionEnum dimensionEnum)
        {
            if (op == null) throw new NullException(() => op);

            IList<Inlet> inlets = GetInlets(op, dimensionEnum);
            switch (inlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return inlets[0];

                default:
                    throw new NotUniqueException<Inlet>(new { dimensionEnum });
            }
        }

        public static IList<Inlet> GetInlets(Operator op, DimensionEnum dimensionEnum)
        {
            if (op == null) throw new NullException(() => op);

            IList<Inlet> inlets = op.Inlets.Where(x => x.GetDimensionEnum() == dimensionEnum).ToArray();
            return inlets;
        }

        // Get Outlet

        /// <param name="listIndex">List indices are not necessarily consecutive.</param>
        public static Outlet GetOutlet(Operator op, int listIndex)
        {
            Outlet inlet = TryGetOutlet(op, listIndex);
            if (inlet == null)
            {
                throw new NotFoundException<Outlet>(new { listIndex });
            }
            return inlet;
        }

        /// <param name="listIndex">List indices are not necessarily consecutive.</param>
        public static Outlet TryGetOutlet(Operator op, int listIndex)
        {
            if (op == null) throw new NullException(() => op);

            IList<Outlet> inlets = op.Outlets.Where(x => x.ListIndex == listIndex).ToArray();
            switch (inlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return inlets[0];

                default:
                    throw new NotUniqueException<Operator>(new { listIndex });
            }
        }

        public static Outlet GetOutlet(Operator op, string name)
        {
            Outlet outlet = TryGetOutlet(op, name);
            if (outlet == null)
            {
                throw new NotFoundException<Outlet>(new { name });
            }
            return outlet;
        }

        public static Outlet TryGetOutlet(Operator op, string name)
        {
            if (op == null) throw new NullException(() => op);

            IList<Outlet> outlets = op.Outlets.Where(x => string.Equals(x.Name, name)).ToArray();
            switch (outlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return outlets[0];

                default:
                    throw new NotUniqueException<Outlet>(new { name });
            }
        }

        public static Outlet GetOutlet(Operator op, DimensionEnum dimensionEnum)
        {
            Outlet outlet = TryGetOutlet(op, dimensionEnum);
            if (outlet == null)
            {
                throw new NotFoundException<Outlet>(new { dimensionEnum });
            }
            return outlet;
        }

        public static Outlet TryGetOutlet(Operator op, DimensionEnum dimensionEnum)
        {
            if (op == null) throw new NullException(() => op);

            IList<Outlet> outlets = GetOutlets(op, dimensionEnum);
            switch (outlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return outlets[0];

                default:
                    throw new NotUniqueException<Outlet>(new { dimensionEnum });
            }
        }

        public static IList<Outlet> GetOutlets(Operator op, DimensionEnum dimensionEnum)
        {
            if (op == null) throw new NullException(() => op);

            IList<Outlet> outlets = op.Outlets.Where(x => x.GetDimensionEnum() == dimensionEnum).ToArray();

            return outlets;
        }

        // Get InputOutlet

        public static Outlet GetInputOutlet(Operator op, int index)
        {
            Inlet inlet = GetInlet(op, index);
            return inlet.InputOutlet;
        }

        public static Outlet GetInputOutlet(Operator op, string name)
        {
            Inlet inlet = GetInlet(op, name);
            return inlet.InputOutlet;
        }

        public static Outlet GetInputOutlet(Operator op, DimensionEnum dimensionEnum)
        {
            Inlet inlet = GetInlet(op, dimensionEnum);
            return inlet.InputOutlet;
        }

        /// <summary>
        /// Gets the input outlet of a specific inlet.
        /// If it is null, and the inlet has a default value, a fake Number Operator is created and its outlet returned.
        /// If the inlet has no default value either, null is returned.
        /// </summary>
        public static Outlet GetInputOutletOrDefault(Operator op, string name)
        {
            Inlet inlet = GetInlet(op, name);
            return GetInputOutletOrDefault(inlet);
        }

        /// <summary>
        /// Gets the input outlet of a specific inlet.
        /// If it is null, and the inlet has a default value, a fake Number Operator is created and its outlet returned.
        /// If the inlet has no default value either, null is returned.
        /// </summary>
        public static Outlet GetInputOutletOrDefault(Operator op, int index)
        {
            Inlet inlet = GetInlet(op, index);
            return GetInputOutletOrDefault(inlet);
        }

        /// <summary>
        /// Gets the input outlet of a specific inlet.
        /// If it is null, and the inlet has a default value, a fake Number Operator is created and its outlet returned.
        /// If the inlet has no default value either, null is returned.
        /// </summary>
        public static Outlet GetInputOutletOrDefault(Operator op, DimensionEnum dimensionEnum)
        {
            Inlet inlet = GetInlet(op, dimensionEnum);
            return GetInputOutletOrDefault(inlet);
        }

        /// <summary>
        /// Gets the input outlet of a specific inlet.
        /// If it is null, and the inlet has a default value, a fake Number Operator is created and its outlet returned.
        /// If the inlet has no default value either, null is returned.
        /// </summary>
        public static Outlet GetInputOutletOrDefault(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.InputOutlet != null)
            {
                return inlet.InputOutlet;
            }

            // ReSharper disable once InvertIf
            if (inlet.DefaultValue.HasValue)
            {
                Number_OperatorWrapper dummyNumberOperator = CreateDummyNumberOperator(inlet.DefaultValue.Value);
                return dummyNumberOperator.Result;
            }

            return null;
        }

        // Private Methods

        private static Number_OperatorWrapper CreateDummyNumberOperator(double number)
        {
            var operatorType = new OperatorType
            {
                ID = (int)OperatorTypeEnum.Number,
                Name = OperatorTypeEnum.Number.ToString()
            };

            var op = new Operator();
            op.LinkTo(operatorType);

            var outlet = new Outlet();
            outlet.LinkTo(op);

            var wrapper = new Number_OperatorWrapper(op) { Number = number };

            return wrapper;
        }
    }
}