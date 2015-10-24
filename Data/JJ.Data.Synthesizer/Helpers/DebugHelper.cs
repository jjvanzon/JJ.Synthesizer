using System;
using System.Text;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Data.Synthesizer.Helpers
{
    internal static class DebugHelper
    {
        public static string GetDebuggerDisplay(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}} ", entity.GetType().Name);

            if (entity.OperatorType != null)
            {
                if (!String.IsNullOrEmpty(entity.OperatorType.Name))
                {
                    sb.Append(entity.OperatorType.Name);
                    sb.Append(' ');
                }
            }

            if (!String.IsNullOrEmpty(entity.Name))
            {
                sb.AppendFormat("'{0}' ", entity.Name);
            }
            
            sb.AppendFormat("({0})", entity.ID);

            return sb.ToString();
        }

        public static string GetDebuggerDisplay(Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}} ", entity.GetType().Name);

            if (!String.IsNullOrEmpty(entity.Name))
            {
                sb.AppendFormat("'{0}' ", entity.Name);
            }

            sb.AppendFormat("({0})", entity.ID);

            if (entity.Operator != null)
            {
                sb.Append(" - ");
                string operatorDebuggerDisplay = GetDebuggerDisplay(entity.Operator);
                sb.Append(operatorDebuggerDisplay);
            }

            return sb.ToString();
        }

        public static string GetDebuggerDisplay(Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}} ", entity.GetType().Name);

            if (!String.IsNullOrEmpty(entity.Name))
            {
                sb.AppendFormat("'{0}' ", entity.Name);
            }

            sb.AppendFormat("({0})", entity.ID);

            if (entity.Operator != null)
            {
                sb.Append(" - ");
                string operatorDebuggerDisplay = GetDebuggerDisplay(entity.Operator);
                sb.Append(operatorDebuggerDisplay);
            }

            return sb.ToString();
        }

        public static string GetDebuggerDisplay(Node entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}} ", entity.GetType().Name);

            sb.AppendFormat("time={0} value={1} ", entity.Time, entity.Value);

            if (entity.NodeType != null)
            {
                if (!String.IsNullOrEmpty(entity.NodeType.Name))
                {
                    sb.AppendFormat("({0}) ", entity.NodeType.Name);
                }
            }

            sb.AppendFormat("({0})", entity.ID);

            return sb.ToString();
        }

    }
}
