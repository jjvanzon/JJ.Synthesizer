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
        /// <summary>
        /// Returns a string formatted like one of the following:
        /// '{0}' /
        /// 'no Name' /
        /// '{0}' (Alias '{1}') /
        /// 'no Name' (Alias '{1}')
        /// </summary>
        public static string GetIdentifier_ForLowerDocumentReference([NotNull] DocumentReference lowerDocumentReference)
        {
            if (lowerDocumentReference == null) throw new NullException(() => lowerDocumentReference);

            var sb = new StringBuilder();

            // TODO: Low priority: Delegate to Document's GetIdentifier.
            string lowerDocumentName = lowerDocumentReference.LowerDocument?.Name;

            if (!string.IsNullOrWhiteSpace(lowerDocumentName))
            {
                sb.Append($"'{lowerDocumentName}'");
            }
            else
            {
                sb.Append($"'{CommonResourceFormatter.NoObject_WithName(CommonResourceFormatter.Name)}'");
            }

            if (string.IsNullOrWhiteSpace(lowerDocumentReference.Alias))
            {
                sb.Append($" ({ResourceFormatter.Alias} '{lowerDocumentReference.Alias}')");
            }

            return sb.ToString();
        }

        public static string GetIdentifier([NotNull] Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            // Use Name
            if (!string.IsNullOrEmpty(inlet.Name))
            {
                return inlet.Name;
            }

            // Use Dimension
            DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                // Downside: Dimension might not be unique among an operator's inlets.
                string dimensionDisplayName = ResourceFormatter.GetText(dimensionEnum);
                return dimensionDisplayName;
            }

            // Use ListIndex
            string inletIdentifier = $"{ResourceFormatter.Inlet} {inlet.ListIndex}";
            return inletIdentifier;
        }

        public static string GetIdentifier([NotNull] Operator op)
        {
            if (op == null) throw new NullException(() => op);

            if (!string.IsNullOrEmpty(op.Name))
            {
                return op.Name;
            }

            // ReSharper disable once InvertIf
            if (op.OperatorType != null)
            {
                string operatorTypeDisplayName = ResourceFormatter.GetText(op.OperatorType);
                return operatorTypeDisplayName;
            }

            return op.ID.ToString();
        }

        public static string GetIdentifier([NotNull] Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            // Use Name
            if (!string.IsNullOrEmpty(outlet.Name))
            {
                return outlet.Name;
            }

            // Use Dimension
            DimensionEnum dimensionEnum = outlet.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                // Downside: Dimension might not be unique among an operator's outlets.
                string dimensionDisplayName = ResourceFormatter.GetText(dimensionEnum);
                return dimensionDisplayName;
            }

            // Use ListIndex
            string outletIdentifier = $"{ResourceFormatter.Outlet} {outlet.ListIndex}";
            return outletIdentifier;
        }
    }
}
