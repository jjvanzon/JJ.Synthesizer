using System;
using System.Text;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Data.Synthesizer.Helpers
{
    public static class DebugHelper
    {
        public static string GetDebuggerDisplay(EntityPosition entityPosition)
        {
            if (entityPosition == null) throw new NullException(() => entityPosition);

            string debuggerDisplay = String.Format(
                "{{{0}}} {1} {2}: X={3}, Y={4}",
                entityPosition.GetType().Name,
                entityPosition.EntityTypeName,
                entityPosition.EntityID,
                entityPosition.X,
                entityPosition.Y);

            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}} ", entity.GetType().Name);

            sb.AppendFormat("[{0}] ", entity.ListIndex);

            if (!String.IsNullOrEmpty(entity.Name))
            {
                sb.AppendFormat("'{0}' ", entity.Name);
            }

            if (entity.Dimension != null)
            {
                sb.AppendFormat("Dimension={0} ", entity.Dimension.Name);
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

            sb.AppendFormat("x={0} y={1} ", entity.X, entity.Y);

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

        public static string GetDebuggerDisplay(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}} ", op.GetType().Name);

            if (op.OperatorType != null)
            {
                if (!String.IsNullOrEmpty(op.OperatorType.Name))
                {
                    sb.Append(op.OperatorType.Name);
                    sb.Append(' ');
                }
            }

            bool isValidPatchInlet = op.OperatorType != null &&
                                     String.Equals(op.OperatorType.Name, "PatchInlet") &&
                                     op.Inlets.Count == 1 &&
                                     op.Inlets[0] != null;
            if (isValidPatchInlet)
            {
                sb.AppendFormat("[{0}] ", op.Data);

                Inlet inlet = op.Inlets[0];

                if (inlet.Dimension != null)
                {
                    sb.AppendFormat("Dimension={0} ", inlet.Dimension.Name);
                }
            }

            if (!String.IsNullOrEmpty(op.Name))
            {
                sb.AppendFormat("'{0}' ", op.Name);
            }

            sb.AppendFormat("({0})", op.ID);

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
    }
}
