using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    internal static class ValidationHelper
    {
        public static string GetMessagePrefix(string entityTypeDisplayName, string name)
        {
            string messagePrefix;
            if (String.IsNullOrEmpty(name))
            {
                messagePrefix = String.Format("{0}: ", entityTypeDisplayName);
            }
            else
            {
                messagePrefix = String.Format("{0} '{1}': ", entityTypeDisplayName, name);
            }
            return messagePrefix;
        }

        public static string GetMessagePrefix(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            if (op.OperatorType != null)
            {
                string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(op);
                return GetMessagePrefix(operatorTypeDisplayName, op.Name);
            }
            else
            {
                return GetMessagePrefix(PropertyDisplayNames.Operator, op.Name);
            }
        }

        public static string GetMessagePrefix(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            return GetMessagePrefix(PropertyDisplayNames.Outlet, outlet.Name);
        }

        public static string GetMessagePrefix(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            return GetMessagePrefix(PropertyDisplayNames.Inlet, inlet.Name);
        }
    }
}
