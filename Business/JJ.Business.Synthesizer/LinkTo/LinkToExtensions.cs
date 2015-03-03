using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.LinkTo
{
    public static class LinkToExtensions
    {
        public static void LinkTo(this Inlet inlet, Operator op)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.Operator != null)
            {
                if (op.Inlets.Contains(inlet))
                {
                    op.Inlets.Remove(inlet);
                }
            }

            inlet.Operator = op;

            if (inlet.Operator != null)
            {
                if (!op.Inlets.Contains(inlet))
                {
                    op.Inlets.Add(inlet);
                }
            }
        }

        public static void LinkTo(this Outlet outlet, Operator op)
        {
            if (outlet == null) throw new NullException(() => outlet);

            if (outlet.Operator != null)
            {
                if (op.Outlets.Contains(outlet))
                {
                    op.Outlets.Remove(outlet);
                }
            }

            outlet.Operator = op;

            if (outlet.Operator != null)
            {
                if (!op.Outlets.Contains(outlet))
                {
                    op.Outlets.Add(outlet);
                }
            }
        }

        public static void LinkTo(this Inlet inlet, Outlet outlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.Input != null)
            {
                if (outlet.ConnectedInlets.Contains(inlet))
                {
                    outlet.ConnectedInlets.Remove(inlet);
                }
            }

            inlet.Input = outlet;

            if (inlet.Input != null)
            {
                if (!outlet.ConnectedInlets.Contains(inlet))
                {
                    outlet.ConnectedInlets.Add(inlet);
                }
            }
        }
    }
}
