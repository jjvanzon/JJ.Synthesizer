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
                if (inlet.Operator.Inlets.Contains(inlet))
                {
                    inlet.Operator.Inlets.Remove(inlet);
                }
            }

            inlet.Operator = op;

            if (inlet.Operator != null)
            {
                if (!inlet.Operator.Inlets.Contains(inlet))
                {
                    inlet.Operator.Inlets.Add(inlet);
                }
            }
        }

        public static void LinkTo(this Outlet outlet, Operator op)
        {
            if (outlet == null) throw new NullException(() => outlet);

            if (outlet.Operator != null)
            {
                if (outlet.Operator.Outlets.Contains(outlet))
                {
                    outlet.Operator.Outlets.Remove(outlet);
                }
            }

            outlet.Operator = op;

            if (outlet.Operator != null)
            {
                if (!outlet.Operator.Outlets.Contains(outlet))
                {
                    outlet.Operator.Outlets.Add(outlet);
                }
            }
        }

        public static void LinkTo(this Inlet inlet, Outlet outlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.Input != null)
            {
                if (inlet.Input.ConnectedInlets.Contains(inlet))
                {
                    inlet.Input.ConnectedInlets.Remove(inlet);
                }
            }

            inlet.Input = outlet;

            if (inlet.Input != null)
            {
                if (!inlet.Input.ConnectedInlets.Contains(inlet))
                {
                    inlet.Input.ConnectedInlets.Add(inlet);
                }
            }
        }
    }
}
