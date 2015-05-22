using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Extensions
{
    internal static class RecursionExtensions
    {
        public static bool IsCircular(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var alreadyDone = new HashSet<Operator>();

            return IsCircular(op, alreadyDone);
        }

        private static bool IsCircular(this Operator op, HashSet<Operator> alreadyDone)
        {
            if (op != null) // Be null-tollerant, because you might call it in places where the entities are not valid.
            {
                if (alreadyDone.Contains(op))
                {
                    return true;
                }
                alreadyDone.Add(op);

                foreach (Inlet inlet in op.Inlets)
                {
                    if (inlet.InputOutlet != null)
                    {
                        if (IsCircular(inlet.InputOutlet.Operator, alreadyDone))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
