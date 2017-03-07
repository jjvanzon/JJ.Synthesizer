using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
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
            return GetIdentifier_WithName(entity.Name);
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
            return GetIdentifier_WithName(entity.Name);
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Document entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return GetIdentifier_WithName(entity.Name);
        }

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
        public static string GetIdentifier(
            [NotNull] Operator entity,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] IPatchRepository patchRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            OperatorTypeEnum operatorTypeEnum = entity.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Curve:
                    return GetIdentifier_ForCurveOperator(entity, curveRepository);

                case OperatorTypeEnum.CustomOperator:
                    return GetIdentifier_ForCustomOperator(entity, patchRepository);

                case OperatorTypeEnum.Number:
                    return GetIdentifier_ForNumberOperator(entity);

                case OperatorTypeEnum.PatchInlet:
                    return GetIdentifier_ForPatchInlet(entity);

                case OperatorTypeEnum.PatchOutlet:
                    return GetIdentifier_ForPatchOutlet(entity);

                case OperatorTypeEnum.Sample:
                    return GetIdentifier_ForSampleOperator(entity, sampleRepository);

                case OperatorTypeEnum.Undefined:
                    return GetIdentifier_ForUndefinedOperatorType(entity);

                default:
                    return GetIdentifier_ForOtherOperartorType(entity);
            }
        }

        [NotNull]
        private static string GetIdentifier_ForCurveOperator([NotNull] Operator entity, [NotNull] ICurveRepository curveRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            // Use Operator Name
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            // Use Underlying Entity Name
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(entity))
            {
                int? underlyingEntityID = DataPropertyParser.TryParseInt32(entity, PropertyNames.CurveID);
                // ReSharper disable once InvertIf
                if (underlyingEntityID.HasValue)
                {
                    Curve underlyingEntity = curveRepository.TryGet(underlyingEntityID.Value);
                    if (underlyingEntity != null)
                    {
                        return GetIdentifier(underlyingEntity);
                    }
                }
            }

            // Mention 'no name' only
            return GetNoNameIdentifier();
        }

        [NotNull]
        private static string GetIdentifier_ForCustomOperator([NotNull] Operator entity, [NotNull] IPatchRepository patchRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            // Use Operator Name
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            // Use Underlying Entity Name
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(entity))
            {
                int? underlyingEntityID = DataPropertyParser.TryParseInt32(entity, PropertyNames.UnderlyingPatchID);
                // ReSharper disable once InvertIf
                if (underlyingEntityID.HasValue)
                {
                    Patch underlyingEntity = patchRepository.TryGet(underlyingEntityID.Value);
                    if (underlyingEntity != null)
                    {
                        return GetIdentifier(underlyingEntity);
                    }
                }
            }

            // Mention 'no name' only
            return GetNoNameIdentifier();
        }

        [NotNull]
        private static string GetIdentifier_ForNumberOperator([NotNull] Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // Use Operator Name
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            // Use Number
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(entity.Data))
            {
                double? number = DataPropertyParser.TryParseDouble(entity.Data, PropertyNames.Number);
                // ReSharper disable once InvertIf
                if (number.HasValue)
                {
                    return $"'{number.Value:0.######}'";
                }
            }

            // Mention 'no name' only
            return GetNoNameIdentifier();
        }

        [NotNull]
        public static string GetIdentifier_ForPatchInlet([NotNull] Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // Use Name
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            // Use Dimension
            // ReSharper disable once InvertIf
            if (entity.Inlets.Count == 1)
            {
                Inlet inlet = entity.Inlets[0];
                DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
                // ReSharper disable once InvertIf
                if (dimensionEnum != DimensionEnum.Undefined)
                {
                    string dimensionDisplayName = ResourceFormatter.GetDisplayName(dimensionEnum);
                    return $"'{dimensionDisplayName}'";
                }
            }

            // Mention 'no name' only
            return GetNoNameIdentifier();
        }

        [NotNull]
        public static string GetIdentifier_ForPatchOutlet([NotNull] Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // Use Name
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            // Use Dimension
            // ReSharper disable once InvertIf
            if (entity.Outlets.Count == 1)
            {
                Outlet outlet = entity.Outlets[0];
                DimensionEnum dimensionEnum = outlet.GetDimensionEnum();
                // ReSharper disable once InvertIf
                if (dimensionEnum != DimensionEnum.Undefined)
                {
                    string dimensionDisplayName = ResourceFormatter.GetDisplayName(dimensionEnum);
                    return $"'{dimensionDisplayName}'";
                }
            }

            // Mention 'no name' only
            return GetNoNameIdentifier();
        }

        [NotNull]
        private static string GetIdentifier_ForSampleOperator([NotNull] Operator entity, [NotNull] ISampleRepository sampleRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            // Use Operator Name
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            // Use Underlying Entity Name
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(entity))
            {
                int? underlyingEntityID = DataPropertyParser.TryParseInt32(entity, PropertyNames.SampleID);
                // ReSharper disable once InvertIf
                if (underlyingEntityID.HasValue)
                {
                    Sample underlyingEntity = sampleRepository.TryGet(underlyingEntityID.Value);
                    if (underlyingEntity != null)
                    {
                        return GetIdentifier(underlyingEntity);
                    }
                }
            }

            // Mention 'no name' only
            return GetNoNameIdentifier();
        }

        [NotNull]
        private static string GetIdentifier_ForUndefinedOperatorType([NotNull] Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return GetIdentifier_WithName(entity.Name);
        }

        [NotNull]
        private static string GetIdentifier_ForOtherOperartorType([NotNull] Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return GetIdentifier_WithName(entity.Name);
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);
            return GetIdentifier_WithName_DimensionEnum_AndListIndex(outlet.Name, outlet.GetDimensionEnum(), outlet.ListIndex);
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return GetIdentifier_WithName(entity.Name);
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // TODO: You could fall back to OriginalFileName.
            return GetIdentifier_WithName(entity.Name);
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // TODO: You could fall back to for instance ScaleType.

            return GetIdentifier_WithName(entity.Name);
        }

        [NotNull]
        public static string GetIdentifier([NotNull] Tone entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // TODO: Make a better identifier.
            // TODO: You could fall back to for instance ScaleType.

            return GetIdentifier_WithName(null);
        }

        // Helpers

        [NotNull]
        private static string GetIdentifier_WithName([CanBeNull] string name)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!string.IsNullOrWhiteSpace(name))
            {
                return $"'{name}'";
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                return GetNoNameIdentifier();
            }
        }

        [NotNull]
        private static string GetNoNameIdentifier()
        {
            return $"'{CommonResourceFormatter.NoObject_WithName(CommonResourceFormatter.Name)}'";
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
                string dimensionDisplayName = ResourceFormatter.GetDisplayName(dimensionEnum);
                return $"'{dimensionDisplayName}'";
            }

            // Use ListIndex
            string identifier = $"({ResourceFormatter.Number} = {listIndex + 1})";
            return identifier;
        }
    }
}
