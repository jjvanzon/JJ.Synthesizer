using System;
using System.Text;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Logging;

namespace JJ.Data.Synthesizer.Helpers
{
	public static class DebuggerDisplayFormatter
	{
		internal static string GetDebuggerDisplay(AudioFileFormat entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<AudioFileFormat>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(AudioFileOutput entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<AudioFileOutput>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(Channel entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<Channel>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(Curve entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<Curve>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(Document entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<Document>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string TryGetDebuggerDisplay(Dimension entity) => entity == null ? null : GetDebuggerDisplay(entity);

		internal static string GetDebuggerDisplay(Dimension entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<Dimension>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(EntityPosition entityPosition)
		{
			if (entityPosition == null) throw new NullException(() => entityPosition);

			string debuggerDisplay = $"{{{nameof(EntityPosition)}}} {new { entityPosition.X, entityPosition.Y }}";

			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(IInletOrOutlet entity)
		{
			if (entity == null) throw new NullException(() => entity);

			var sb = new StringBuilder();

			sb.Append($"{{{entity.GetType().Name}}} ");

			if (entity.Dimension != null)
			{
				sb.Append($"{nameof(entity.Dimension)}={entity.Dimension.Name} ");
			}

			if (!string.IsNullOrEmpty(entity.Name))
			{
				sb.Append($"{nameof(entity.Name)}='{entity.Name}' ");
			}

			sb.Append($"{nameof(entity.Position)}={entity.Position} ");

			if (entity.RepetitionPosition.HasValue)
			{
				sb.Append($"{nameof(entity.RepetitionPosition)}={entity.RepetitionPosition} ");
			}

			sb.Append($"({entity.ID})");

			if (entity.IsObsolete)
			{
				sb.Append(" (obsolete)");
			}

			if (entity.Operator != null)
			{
				sb.Append(" for ");
				string operatorDebuggerDisplay = GetDebuggerDisplay(entity.Operator);
				sb.Append(operatorDebuggerDisplay);
			}

			return sb.ToString();
		}

		internal static string GetDebuggerDisplay(InterpolationType entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<InterpolationType>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(MidiMappingGroup entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<MidiMappingGroup>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			string debuggerDisplay =
				$"{{{nameof(MidiMapping)}}} " +
				$"{new { Dimension = entity.Dimension?.Name, entity.Name, entity.Position, entity.FromDimensionValue, entity.TillDimensionValue }} " +
				$"{new { MidiMappingType = entity.MidiMappingType?.Name, entity.FromMidiValue, entity.TillMidiValue, entity.MidiControllerCode }}";

			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(Node entity)
		{
			if (entity == null) throw new NullException(() => entity);

			var sb = new StringBuilder();

			sb.Append($"{{{nameof(Node)}}} ");

			sb.Append($"x={entity.X} y={entity.Y} ");

			if (!string.IsNullOrEmpty(entity.NodeType?.Name))
			{
				sb.Append($"({entity.NodeType.Name}) ");
			}

			sb.Append($"({entity.ID})");

			return sb.ToString();
		}

		internal static string GetDebuggerDisplay(NodeType entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<NodeType>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		public static string GetDebuggerDisplay(Operator op)
		{
			if (op == null) throw new NullException(() => op);

			var sb = new StringBuilder();

			sb.Append($"{{{typeof(Operator)}}} ");

			if (op.UnderlyingPatch != null)
			{
				sb.Append($"{nameof(op.UnderlyingPatch)}={op.UnderlyingPatch.Name} ");
			}

			if (!string.IsNullOrEmpty(op.Name))
			{
				sb.Append($"'{op.Name}' ");
			}

			bool isValidPatchInlet = op.UnderlyingPatch != null &&
			                         string.Equals(op.UnderlyingPatch.Document.Name, "System") &&
			                         string.Equals(op.UnderlyingPatch.Name, "PatchInlet") &&
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

			bool isValidPatchOutlet = op.UnderlyingPatch != null &&
			                          string.Equals(op.UnderlyingPatch.Document.Name, "System") &&
			                          string.Equals(op.UnderlyingPatch.Name, "PatchOutlet") &&
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

		internal static string GetDebuggerDisplay(Patch entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<Patch>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(Sample entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<Sample>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(SampleDataType entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<SampleDataType>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(Scale entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<Scale>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(ScaleType entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<ScaleType>(entity.ID, entity.Name);
			return debuggerDisplay;
		}

		internal static string GetDebuggerDisplay(SpeakerSetup entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string debuggerDisplay = CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName<SpeakerSetup>(entity.ID, entity.Name);
			return debuggerDisplay;
		}
	}
}