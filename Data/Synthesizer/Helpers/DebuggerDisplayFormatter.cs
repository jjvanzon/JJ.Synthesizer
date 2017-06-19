using System.Text;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Data.Synthesizer.Helpers
{
    public static class DebuggerDisplayFormatter
    {
        public static string GetDebuggerDisplay(AudioFileFormat entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<AudioFileFormat>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<AudioFileOutput>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Channel entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<Channel>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<Curve>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<Document>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Dimension entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<Dimension>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(EntityPosition entityPosition)
        {
            if (entityPosition == null) throw new NullException(() => entityPosition);

            // ReSharper disable once UseStringInterpolation
            string debuggerDisplay = string.Format(
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

            if (entity.Dimension != null)
            {
                sb.Append($"{nameof(entity.Dimension)}={entity.Dimension.Name} ");
            }

            if (!string.IsNullOrEmpty(entity.Name))
            {
                sb.Append($"{nameof(entity.Name)}='{entity.Name}' ");
            }

            sb.Append($"{nameof(entity.Position)}={entity.Position} ");

            sb.AppendFormat("({0})", entity.ID);

            if (entity.Operator != null)
            {
                sb.Append(" for ");
                string operatorDebuggerDisplay = GetDebuggerDisplay(entity.Operator);
                sb.Append(operatorDebuggerDisplay);
            }

            if (entity.IsObsolete)
            {
                sb.Append(" (obsolete)");
            }

            return sb.ToString();
        }

        public static string GetDebuggerDisplay(InterpolationType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<InterpolationType>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Node entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}} ", entity.GetType().Name);

            sb.AppendFormat("x={0} y={1} ", entity.X, entity.Y);

            if (entity.NodeType != null)
            {
                if (!string.IsNullOrEmpty(entity.NodeType.Name))
                {
                    sb.AppendFormat("({0}) ", entity.NodeType.Name);
                }
            }

            sb.AppendFormat("({0})", entity.ID);

            return sb.ToString();
        }

        public static string GetDebuggerDisplay(NodeType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<NodeType>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}} ", op.GetType().Name);

            if (!string.IsNullOrEmpty(op.OperatorType?.Name))
            {
                sb.Append($"{nameof(op.OperatorType)}={op.OperatorType.Name} ");
            }

            if (op.UnderlyingPatch != null)
            {
                sb.Append($"{nameof(op.UnderlyingPatch)}={op.UnderlyingPatch.Name} ");
            }

            if (!string.IsNullOrEmpty(op.Name))
            {
                sb.Append($"'{op.Name}' ");
            }

            bool isValidPatchInlet = op.OperatorType != null &&
                                     string.Equals(op.OperatorType.Name, "PatchInlet") &&
                                     op.Inlets.Count == 1 &&
                                     op.Inlets[0] != null;
            if (isValidPatchInlet)
            {
                Inlet inlet = op.Inlets[0];

                if (inlet.Dimension != null)
                {
                    sb.Append($"{nameof(inlet.Dimension)}={inlet.Dimension.Name} ");
                }

                if (!string.IsNullOrEmpty(inlet.Name))
                {
                    sb.Append($"{nameof(inlet.Name)}='{inlet.Name}' ");
                }

                sb.Append($"{nameof(inlet.Position)}={inlet.Position} ");

                if (inlet.IsObsolete)
                {
                    sb.Append(" (obsolete)");
                }
            }

            bool isValidPatchOutlet = op.OperatorType != null &&
                                     string.Equals(op.OperatorType.Name, "PatchOutlet") &&
                                     op.Outlets.Count == 1 &&
                                     op.Outlets[0] != null;
            if (isValidPatchOutlet)
            {
                Outlet outlet = op.Outlets[0];

                if (outlet.Dimension != null)
                {
                    sb.Append($"{nameof(outlet.Dimension)}={outlet.Dimension.Name} ");
                }

                if (!string.IsNullOrEmpty(outlet.Name))
                {
                    sb.Append($"{nameof(outlet.Name)}='{outlet.Name}' ");
                }

                sb.Append($"{nameof(outlet.Position)}={outlet.Position} ");

                if (outlet.IsObsolete)
                {
                    sb.Append(" (obsolete)");
                }
            }

            sb.Append($"({op.ID})");

            return sb.ToString();
        }

        public static string GetDebuggerDisplay(OperatorType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<OperatorType>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}} ", entity.GetType().Name);

            if (entity.Dimension != null)
            {
                sb.Append($"{nameof(entity.Dimension)}={entity.Dimension.Name} ");
            }

            if (!string.IsNullOrEmpty(entity.Name))
            {
                sb.Append($"{nameof(entity.Name)}='{entity.Name}' ");
            }

            sb.Append($"{nameof(entity.Position)}={entity.Position} ");

            sb.AppendFormat("({0})", entity.ID);

            if (entity.Operator != null)
            {
                sb.Append(" for ");
                string operatorDebuggerDisplay = GetDebuggerDisplay(entity.Operator);
                sb.Append(operatorDebuggerDisplay);
            }

            if (entity.IsObsolete)
            {
                sb.Append(" (obsolete)");
            }

            return sb.ToString();
        }

        public static string GetDebuggerDisplay(Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<Patch>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<Sample>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(SampleDataType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<SampleDataType>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<Scale>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(ScaleType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<ScaleType>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string debuggerDisplay = GetDebuggDisplayWithIDAndName<SpeakerSetup>(entity.ID, entity.Name);
            return debuggerDisplay;
        }

        private static string GetDebuggDisplayWithIDAndName<TEntity>(int id, string name)
        {
            string debuggerDisplay = $"{{{typeof(TEntity).Name}}} {name} (ID = {id})";
            return debuggerDisplay;
        }
    }
}
