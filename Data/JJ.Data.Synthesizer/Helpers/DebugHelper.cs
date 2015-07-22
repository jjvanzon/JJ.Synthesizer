using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.Helpers
{
    internal static class DebugHelper
    {
        public static string GetDebuggerDisplay(Operator entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            if (entity.OperatorType == null)
            {
                return String.Format("'{1}' ({2})", entity.Name, entity.ID);
            }

            return String.Format("{0} '{1}' ({2})", entity.OperatorType.Name, entity.Name, entity.ID);
        }

        public static string GetDebuggerDisplay(Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            if (entity.Operator == null) return entity.Name;

            if (entity.Operator.OperatorType == null)
            {
                return String.Format("'{1}' ({2}) - {3}", entity.Operator.Name, entity.Operator.ID, entity.Name);
            }

            return String.Format("{0} '{1}' ({2}) - {3}", entity.Operator.OperatorType.Name, entity.Operator.Name, entity.Operator.ID, entity.Name);
        }

        public static string GetDebuggerDisplay(Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            if (entity.Operator == null) return entity.Name;

            if (entity.Operator.OperatorType == null)
            {
                return String.Format("'{1}' ({2}) - {3}", entity.Operator.Name, entity.Operator.ID, entity.Name);
            }

            return String.Format("{0} '{1}' ({2}) - {3}", entity.Operator.OperatorType.Name, entity.Operator.Name, entity.Operator.ID, entity.Name);
        }
    }
}
