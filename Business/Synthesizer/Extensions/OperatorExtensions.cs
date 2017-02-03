using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class OperatorExtensions
    {
        public static IList<Operator> GetConnectedOperators(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            IList<Operator> connectedOperators = 
                // ReSharper disable once InvokeAsExtensionMethod
                Enumerable.Union(
                    op.Inlets.Where(x => x.InputOutlet != null).Select(x => x.InputOutlet.Operator),
                    op.Outlets.SelectMany(x => x.ConnectedInlets).Select(x => x.Operator))
                .ToArray();

            return connectedOperators;
        }
    }
}
