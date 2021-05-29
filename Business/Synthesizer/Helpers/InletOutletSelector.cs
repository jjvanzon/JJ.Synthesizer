using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Aggregates;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class InletOutletSelector
    {
        // TODO: Composite keys Name-Position and DimensionEnum-Position have also become supported.

        // Sort

        // ReSharper disable once UnusedMember.Global
        public static IList<Outlet> GetSortedInputOutlets(Operator op)
        {
            IList<Outlet> outlets = EnumerateSortedInputOutlets(op).ToArray();
            return outlets;
        }

        // ReSharper disable once UnusedMember.Global
        public static IEnumerable<Outlet> EnumerateSortedInputOutlets(Operator op)
        {
            IEnumerable<Outlet> enumerable = op.Inlets.Sort().Select(x => x.InputOutlet);
            return enumerable;
        }

        // Get Inlet

        /// <param name="position">List indices are not necessarily consecutive.</param>
        public static Inlet GetInlet(Operator op, int position)
        {
            Inlet inlet = TryGetInlet(op, position);
            if (inlet == null)
            {
                throw new NotFoundException<Inlet>(new { position });
            }
            return inlet;
        }

        /// <param name="position">List indices are not necessarily consecutive.</param>
        public static Inlet TryGetInlet(Operator op, int position)
        {
            if (op == null) throw new NullException(() => op);

            IList<Inlet> inlets = GetInlets(op, position);
            switch (inlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return inlets[0];

                default:
                    throw new NotUniqueException<Operator>(new { position });
            }
        }

        public static IList<Inlet> GetInlets(Operator op, int position)
        {
            if (op == null) throw new NullException(() => op);

            IList<Inlet> inlets = op.Inlets.Where(x => x.Position == position).Sort().ToArray();

            return inlets;
        }

        public static Inlet GetInlet(Operator op, string name)
        {
            Inlet inlet = TryGetInlet(op, name);
            if (inlet == null)
            {
                throw new NotFoundException<Inlet>(new { name });
            }
            return inlet;
        }

        public static Inlet TryGetInlet(Operator op, string name)
        {
            IList<Inlet> inlets = GetInlets(op, name);

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

        public static IList<Inlet> GetInlets(Operator op, string name)
        {
            if (op == null) throw new NullException(() => op);

            IList<Inlet> inlets = op.Inlets.Where(x => NameHelper.AreEqual(x.Name, name)).Sort().ToArray();

            return inlets;
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

            IList<Inlet> inlets = op.Inlets.Where(x => x.GetDimensionEnum() == dimensionEnum).Sort().ToArray();
            return inlets;
        }

        // Get Outlet

        /// <param name="position">List indices are not necessarily consecutive.</param>
        public static Outlet GetOutlet(Operator op, int position)
        {
            Outlet outlet = TryGetOutlet(op, position);
            if (outlet == null)
            {
                throw new NotFoundException<Outlet>(new { position });
            }
            return outlet;
        }

        /// <param name="position">List indices are not necessarily consecutive.</param>
        public static Outlet TryGetOutlet(Operator op, int position)
        {
            IList<Outlet> outlets = GetOutlets(op, position);

            switch (outlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return outlets[0];

                default:
                    throw new NotUniqueException<Operator>(new { position });
            }
        }

        public static IList<Outlet> GetOutlets(Operator op, int position)
        {
            if (op == null) throw new NullException(() => op);

            IList<Outlet> outlets = op.Outlets.Where(x => x.Position == position).Sort().ToArray();

            return outlets;
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
            IList<Outlet> outlets = GetOutlets(op, name);

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

        public static IList<Outlet> GetOutlets(Operator op, string name)
        {
            if (op == null) throw new NullException(() => op);

            IList<Outlet> outlets = op.Outlets.Where(x => NameHelper.AreEqual(x.Name, name)).Sort().ToArray();

            return outlets;
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

            IList<Outlet> outlets = op.Outlets.Where(x => x.GetDimensionEnum() == dimensionEnum).Sort().ToArray();

            return outlets;
        }

        // Get InputOutlet

        public static Outlet GetInputOutlet(Operator op, int position)
        {
            Inlet inlet = GetInlet(op, position);
            return inlet.InputOutlet;
        }

        public static Outlet TryGetInputOutlet(Operator op, int position)
        {
            Inlet inlet = TryGetInlet(op, position);
            return inlet?.InputOutlet;
        }

        public static IList<Outlet> GetInputOutlets(Operator op, int position)
        {
            IList<Inlet> inlets = GetInlets(op, position);
            IList<Outlet> outlets = inlets.Select(x => x.InputOutlet).Sort().ToArray();
            return outlets;
        }

        public static Outlet GetInputOutlet(Operator op, string name)
        {
            Inlet inlet = GetInlet(op, name);
            return inlet.InputOutlet;
        }

        public static Outlet TryGetInputOutlet(Operator op, string name)
        {
            Inlet inlet = TryGetInlet(op, name);
            return inlet?.InputOutlet;
        }

        public static IList<Outlet> GetInputOutlets(Operator op, string name)
        {
            IList<Inlet> inlets = GetInlets(op, name);
            IList<Outlet> outlets = inlets.Select(x => x.InputOutlet).Sort().ToArray();
            return outlets;
        }

        public static Outlet GetInputOutlet(Operator op, DimensionEnum dimensionEnum)
        {
            Inlet inlet = GetInlet(op, dimensionEnum);
            return inlet.InputOutlet;
        }

        public static Outlet TryGetInputOutlet(Operator op, DimensionEnum dimensionEnum)
        {
            Inlet inlet = TryGetInlet(op, dimensionEnum);
            return inlet?.InputOutlet;
        }

        public static IList<Outlet> GetInputOutlets(Operator op, DimensionEnum dimensionEnum)
        {
            IList<Inlet> inlets = GetInlets(op, dimensionEnum);
            IList<Outlet> outlets = inlets.Select(x => x.InputOutlet).Sort().ToArray();
            return outlets;
        }
    }
}