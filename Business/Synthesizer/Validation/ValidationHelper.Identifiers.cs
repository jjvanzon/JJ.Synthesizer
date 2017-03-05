using System;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal static partial class ValidationHelper
    {
        // Identifiers in this case are user-friendly identifications of an entity.
        // They are used in places where the data might not be valid yet,
        // so contains fallbacks to other means of identification.
        // It can happen that the identification is non-unique.
        // But they attempt to be the clearest identification to the user.
        // Quotes are added around names, so are already part of the identifier.

        [NotNull]
        public static string GetIdentifier([NotNull] AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetIdentifierWithName(entity.Name);
        }

        [CanBeNull]
        public static string GetIdentifier([NotNull] AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // Ouch. Nothing to identify it with.
            return null;
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetIdentifierWithName(entity.Name);
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Document document) => GetIdentifierWithName(document?.Name);

        /// <summary>
        /// Returns a string formatted like one of the following:
        /// '{0}' /
        /// 'no Name' /
        /// '{0}' (Alias '{1}') /
        /// 'no Name' (Alias '{1}')
        /// </summary>
        [NotNull]
        public static string GetIdentifier_ForLowerDocumentReference([NotNull] DocumentReference lowerDocumentReference)
        {
            if (lowerDocumentReference == null) throw new NullException(() => lowerDocumentReference);

            var sb = new StringBuilder();

            if (lowerDocumentReference.LowerDocument != null)
            {
                string lowerDocumentIdentifier = GetIdentifier(lowerDocumentReference.LowerDocument);
                sb.Append(lowerDocumentIdentifier);
            }

            if (string.IsNullOrWhiteSpace(lowerDocumentReference.Alias))
            {
                sb.Append($" ({ResourceFormatter.Alias} '{lowerDocumentReference.Alias}')");
            }

            return sb.ToString();
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            return GetIdentifier_WithName_DimensionEnum_AndListIndex(inlet.Name, inlet.GetDimensionEnum(), inlet.ListIndex);
        }

        /// <param name="number">1-based</param>
        [NotNull]
        public static string GetIdentifier([NotNull] Node entity, int number)
        {
            if (entity == null) throw new NullException(() => entity);

            return number.ToString();
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Operator op)
        {
            if (op == null) throw new NullException(() => op);

            if (!string.IsNullOrWhiteSpace(op.Name))
            {
                return $"'{op.Name}'";
            }

            // ReSharper disable once InvertIf
            if (op.OperatorType != null)
            {
                string operatorTypeDisplayName = ResourceFormatter.GetText(op.OperatorType);
                return $"'{operatorTypeDisplayName}'";
            }

            return op.ID.ToString();
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            return GetIdentifier_WithName_DimensionEnum_AndListIndex(outlet.Name, outlet.GetDimensionEnum(), outlet.ListIndex);
        }

        // Private Methods

        [NotNull]
        private static string GetIdentifierWithName([CanBeNull] string name)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!string.IsNullOrWhiteSpace(name))
            {
                return $"'{name}'";
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                return $"'{CommonResourceFormatter.NoObject_WithName(CommonResourceFormatter.Name)}'";
            }
        }

        [NotNull]
        private static string GetIdentifier_WithName_DimensionEnum_AndListIndex([CanBeNull] string name, DimensionEnum dimensionEnum, int listIndex)
        {
            // Use Name
            if (!string.IsNullOrWhiteSpace(name))
            {
                return $"'{name}'";
            }

            // Use Dimension
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                // Downside: Dimension might not be unique among an operator's inlets or outlets.
                string dimensionDisplayName = ResourceFormatter.GetText(dimensionEnum);
                return $"'{dimensionDisplayName}'";
            }

            // Use ListIndex
            string identifier = $"({ResourceFormatter.Number} = {listIndex + 1})";
            return identifier;
        }
    }
}
