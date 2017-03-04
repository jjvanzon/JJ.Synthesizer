using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Helpers;
using JetBrains.Annotations;

namespace JJ.Business.Synthesizer.Validation
{
    internal static partial class ValidationHelper
    {
        [NotNull]
        public static string GetMessagePrefix([NotNull] AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(ResourceFormatter.AudioFileOutput, entity.Name);
        }

        [NotNull]
        public static string GetMessagePrefix([NotNull] AudioOutput audioOutput)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);

            return ResourceFormatter.AudioOutput;
        }

        [NotNull]
        public static string GetMessagePrefix([NotNull] Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(ResourceFormatter.Curve, entity.Name);
        }

        [NotNull]
        public static string GetMessagePrefix([NotNull] Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(ResourceFormatter.Document, entity.Name);
        }

        [NotNull]
        public static string GetMessagePrefix_ForLowerDocumentReference([NotNull] DocumentReference lowerDocumentReference)
        {
            // Returns something like "Library 'bla':"
            string identifier = GetIdentifier_ForLowerDocumentReference(lowerDocumentReference);
            return $"{ResourceFormatter.LowerDocument} {identifier}: ";
        }

        /// <param name="number">1-based</param>
        [NotNull]
        public static string GetMessagePrefix([NotNull] Inlet entity, int? number = null)
        {
            if (entity == null) throw new NullException(() => entity);

            if (!string.IsNullOrEmpty(entity.Name))
            {
                return $"{ResourceFormatter.Inlet} '{entity.Name}': ";
            }
            // ReSharper disable once RedundantIfElseBlock
            else if (number.HasValue)
            {
                return $"{ResourceFormatter.Inlet} {number}: ";
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                return ResourceFormatter.Inlet + ": ";
            }
        }

        /// <param name="number">1-based</param>
        [NotNull]
        public static string GetMessagePrefix([NotNull] Node entity, int number)
        {
            if (entity == null) throw new NullException(() => entity);

            return $"{ResourceFormatter.Node} {number}: ";
        }

        [NotNull]
        public static string GetMessagePrefix(
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
                    return GetMessagePrefix_ForCurveOperator(entity, curveRepository);

                case OperatorTypeEnum.CustomOperator:
                    return GetMessagePrefix_ForCustomOperator(entity, patchRepository);

                case OperatorTypeEnum.Number:
                    return GetMessagePrefix_ForNumberOperator(entity);

                case OperatorTypeEnum.PatchInlet:
                    return GetMessagePrefix_ForPatchInlet(entity);

                case OperatorTypeEnum.PatchOutlet:
                    return GetMessagePrefix_ForPatchOutlet(entity);

                case OperatorTypeEnum.Sample:
                    return GetMessagePrefix_ForSampleOperator(entity, sampleRepository);

                case OperatorTypeEnum.Undefined:
                    return GetMessagePrefix_ForUndefinedOperatorType(entity);

                default:
                    return GetMessagePrefix_ForOtherOperartorType(entity);
            }
        }

        [NotNull]
        private static string GetMessagePrefix_ForCurveOperator([NotNull] Operator entity, [NotNull] ICurveRepository curveRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);

            string operatorTypeDisplayName = ResourceFormatter.GetOperatorTypeText(entity);

            // Use Operator Name
            if (!string.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
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
                    if (!string.IsNullOrEmpty(underlyingEntity?.Name))
                    {
                        return GetMessagePrefix(operatorTypeDisplayName, underlyingEntity.Name);
                    }
                }
            }

            // Use OperatorTypeDisplayName only
            return operatorTypeDisplayName + ": ";
        }

        [NotNull]
        private static string GetMessagePrefix_ForCustomOperator([NotNull] Operator entity, [NotNull] IPatchRepository patchRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            string operatorTypeDisplayName = ResourceFormatter.GetOperatorTypeText(entity);

            // Use Operator Name
            if (!string.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
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
                    if (!string.IsNullOrEmpty(underlyingEntity?.Name))
                    {
                        return GetMessagePrefix(operatorTypeDisplayName, underlyingEntity.Name);
                    }
                }
            }

            // Use OperatorTypeDisplayName only
            return operatorTypeDisplayName + ": ";
        }

        [NotNull]
        private static string GetMessagePrefix_ForNumberOperator([NotNull] Operator entity)
        {
            string operatorTypeDisplayName = ResourceFormatter.GetOperatorTypeText(entity);

            // Use Operator Name
            if (!string.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
            }

            // Use Number
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(entity.Data))
            {
                double? number = DataPropertyParser.TryParseDouble(entity.Data, PropertyNames.Number);
                // ReSharper disable once InvertIf
                if (number.HasValue)
                {
                    return $"{operatorTypeDisplayName} '{number.Value:0.######}': ";
                }
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        [NotNull]
        private static string GetMessagePrefix_ForPatchInlet([NotNull] Operator entity)
        {
            string operatorTypeDisplayName = ResourceFormatter.GetOperatorTypeText(entity);

            // Use Name
            if (!string.IsNullOrEmpty(entity.Name))
            {
                string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, entity.Name);
                return messagePrefix;
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
                    string dimensionDisplayName = ResourceFormatter.GetText(dimensionEnum);
                    string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, dimensionDisplayName);
                    return messagePrefix;
                }
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        [NotNull]
        private static string GetMessagePrefix_ForPatchOutlet([NotNull] Operator entity)
        {
            string operatorTypeDisplayName = ResourceFormatter.GetOperatorTypeText(entity);

            // Use Name
            if (!string.IsNullOrEmpty(entity.Name))
            {
                string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, entity.Name);
                return messagePrefix;
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
                    string dimensionDisplayName = ResourceFormatter.GetText(dimensionEnum);
                    string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, dimensionDisplayName);
                    return messagePrefix;
                }
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        [NotNull]
        private static string GetMessagePrefix_ForSampleOperator([NotNull] Operator entity, [NotNull] ISampleRepository sampleRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            string operatorTypeDisplayName = ResourceFormatter.GetOperatorTypeText(entity);

            // Use Operator Name
            if (!string.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
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
                    if (!string.IsNullOrEmpty(underlyingEntity?.Name))
                    {
                        return GetMessagePrefix(operatorTypeDisplayName, underlyingEntity.Name);
                    }
                }
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        [NotNull]
        private static string GetMessagePrefix_ForUndefinedOperatorType([NotNull] Operator entity)
        {
            return GetMessagePrefix(ResourceFormatter.Operator, entity.Name);
        }

        [NotNull]
        private static string GetMessagePrefix_ForOtherOperartorType([NotNull] Operator entity)
        {
            string operatorTypeDisplayName = ResourceFormatter.GetOperatorTypeText(entity);

            // Use Name
            if (!string.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        /// <param name="number">1-based</param>
        [NotNull]
        public static string GetMessagePrefix([NotNull] Outlet entity, int number)
        {
            if (entity == null) throw new NullException(() => entity);

            string messagePrefix = $"{ResourceFormatter.Outlet} {number}: ";
            return messagePrefix;
        }

        /// <param name="number">1-based</param>
        [NotNull]
        public static string GetMessagePrefix([NotNull] Outlet entity, int? number = null)
        {
            if (entity == null) throw new NullException(() => entity);

            if (!string.IsNullOrEmpty(entity.Name))
            {
                return $"{ResourceFormatter.Outlet} '{entity.Name}': ";
            }
            // ReSharper disable once RedundantIfElseBlock
            else if (number.HasValue)
            {
                return $"{ResourceFormatter.Outlet} {number}: ";
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                return ResourceFormatter.Outlet + ": ";
            }
        }

        [NotNull]
        public static string GetMessagePrefix([NotNull] Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(ResourceFormatter.Patch, entity.Name);
        }

        [NotNull]
        public static string GetMessagePrefix([NotNull] Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(ResourceFormatter.Sample, entity.Name);
        }

        [NotNull]
        public static string GetMessagePrefix([NotNull] Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(ResourceFormatter.Scale, entity.Name);
        }

        [NotNull]
        public static string GetMessagePrefix([NotNull] Tone entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // TODO: Make a better message prefix
            return ResourceFormatter.Tone;
        }

        /// <summary> Uses the name in the message or otherwise the entityTypeDisplayName. </summary>
        [NotNull]
        private static string GetMessagePrefix(string entityTypeDisplayName, [CanBeNull] string name)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (string.IsNullOrEmpty(name))
            {
                return $"{entityTypeDisplayName}: ";
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                return $"{entityTypeDisplayName} '{name}': ";
            }
        }
    }
}