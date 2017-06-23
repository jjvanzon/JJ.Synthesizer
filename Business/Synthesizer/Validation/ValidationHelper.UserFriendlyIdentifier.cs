using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary>
    /// Identifiers in this case are user-friendly identifications of an entity.
    /// They are used in places where the data might not be valid yet,
    /// so contains fallbacks to other means of identification.
    /// It can happen that the identification is non-unique.
    /// But they attempt to be the clearest identification to the user.
    /// Quotes are added around names, so are already part of the identifier.
    /// </summary>
    public static partial class ValidationHelper
    {
        public static string GetUserFriendlyIdentifier(AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return GetUserFriendlyIdentifier_WithName_AndNoNameFallback(entity.Name);
        }

        public static string GetUserFriendlyIdentifier(AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return ResourceFormatter.AudioOutput;
        }

        public static string GetUserFriendlyIdentifier(Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return GetUserFriendlyIdentifier_WithName_AndNoNameFallback(entity.Name);
        }

        public static string GetUserFriendlyIdentifier(Document entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return GetUserFriendlyIdentifier_WithName_AndNoNameFallback(entity.Name);
        }

        /// <summary>
        /// Returns a string formatted like one of the following:
        /// '{0}' /
        /// 'no Name' /
        /// '{0}' (Alias '{1}') /
        /// 'no Name' (Alias '{1}')
        /// </summary>

        public static string GetUserFriendlyIdentifier_ForLowerDocumentReference(DocumentReference lowerDocumentReference)
        {
            if (lowerDocumentReference == null) throw new NullException(() => lowerDocumentReference);

            var sb = new StringBuilder();

            if (lowerDocumentReference.LowerDocument != null)
            {
                string lowerDocumentIdentifier = GetUserFriendlyIdentifier(lowerDocumentReference.LowerDocument);
                sb.Append(lowerDocumentIdentifier);
            }

            if (!string.IsNullOrWhiteSpace(lowerDocumentReference.Alias))
            {
                sb.Append($" ({ResourceFormatter.Alias} '{lowerDocumentReference.Alias}')");
            }

            return sb.ToString();
        }

        public static string GetUserFriendlyIdentifier_ForHigherDocumentReference(DocumentReference higherDocumentReference)
        {
            if (higherDocumentReference == null) throw new NullException(() => higherDocumentReference);

            if (higherDocumentReference.HigherDocument != null)
            {
                string higherDocumentIdentifier = GetUserFriendlyIdentifier(higherDocumentReference.HigherDocument);
                return higherDocumentIdentifier;
            }

            return ValidationResourceFormatter.IsEmpty(ResourceFormatter.HigherDocument);
        }

        public static string GetUserFriendlyIdentifier(IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new NullException(() => inletOrOutlet);

            string userFriendlyIdentifier = GetUserFriendlyIdentifier_WithName_DimensionEnum_AndPosition(
                inletOrOutlet.Name,
                inletOrOutlet.GetDimensionEnum(),
                inletOrOutlet.Position);

            return userFriendlyIdentifier;
        }

        /// <param name="number">1-based</param>

        public static string GetUserFriendlyIdentifier(Node entity, int number)
        {
            if (entity == null) throw new NullException(() => entity);
            return number.ToString();
        }

        public static string GetUserFriendlyIdentifier(
            Operator entity,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            OperatorTypeEnum operatorTypeEnum = entity.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Curve:
                    return GetUserFriendlyIdentifier_ForCurveOperator(entity, curveRepository);

                case OperatorTypeEnum.CustomOperator:
                    return GetUserFriendlyIdentifier_ForCustomOperator(entity);

                case OperatorTypeEnum.Number:
                    return GetUserFriendlyIdentifier_ForNumberOperator(entity);

                case OperatorTypeEnum.PatchInlet:
                    return GetUserFriendlyIdentifier_ForPatchInlet(entity);

                case OperatorTypeEnum.PatchOutlet:
                    return GetUserFriendlyIdentifier_ForPatchOutlet(entity);

                case OperatorTypeEnum.Sample:
                    return GetUserFriendlyIdentifier_ForSampleOperator(entity, sampleRepository);

                //case OperatorTypeEnum.Undefined:
                //    return GetUserFriendlyIdentifier_ForUndefinedOperatorType(entity);

                default:
                    return GetUserFriendlyIdentifier_ForOtherOperartorType(entity);
            }
        }

        public static string GetUserFriendlyIdentifier_ForCurveOperator(Operator entity, ICurveRepository curveRepository)
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
                int? underlyingEntityID = DataPropertyParser.TryParseInt32(entity, nameof(Curve_OperatorWrapper.CurveID));
                // ReSharper disable once InvertIf
                if (underlyingEntityID.HasValue)
                {
                    Curve underlyingEntity = curveRepository.TryGet(underlyingEntityID.Value);
                    if (underlyingEntity != null)
                    {
                        return GetUserFriendlyIdentifier(underlyingEntity);
                    }
                }
            }

            // Mention 'no name' only
            return $"'{ResourceFormatter.GetOperatorTypeDisplayName(entity)}'";
        }

        public static string GetUserFriendlyIdentifier_ForCustomOperator(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // Use Operator Name
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            // Use Underlying Entity Name
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (entity.UnderlyingPatch != null)
            {
                return GetUserFriendlyIdentifier(entity.UnderlyingPatch);
            }

            // Mention 'no name' only
            return $"'{ResourceFormatter.GetOperatorTypeDisplayName(entity)}'";
        }

        private static string GetUserFriendlyIdentifier_ForNumberOperator(Operator entity)
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
                double? number = DataPropertyParser.TryParseDouble(entity.Data, nameof(Number_OperatorWrapper.Number));
                // ReSharper disable once InvertIf
                if (number.HasValue)
                {
                    return $"'{FormatNumber(number.Value)}'";
                }
            }

            // Mention 'no name' only
            return $"'{ResourceFormatter.GetOperatorTypeDisplayName(entity)}'";
        }

        public static string GetUserFriendlyIdentifier_ForPatchInlet(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // ReSharper disable once InvertIf
            if (entity.Inlets.Count == 1)
            {
                Inlet inlet = entity.Inlets[0];
                string identifier = GetUserFriendlyIdentifier_WithName_DimensionEnum_AndPosition(inlet.Name, inlet.GetDimensionEnum(), inlet.Position);
                return identifier;
            }

            // Mention 'no name' only
            return $"'{ResourceFormatter.GetOperatorTypeDisplayName(entity)}'";
        }

        public static string GetUserFriendlyIdentifier_ForPatchOutlet(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // ReSharper disable once InvertIf
            if (entity.Outlets.Count == 1)
            {
                Outlet outlet = entity.Outlets[0];
                string identifier = GetUserFriendlyIdentifier_WithName_DimensionEnum_AndPosition(outlet.Name, outlet.GetDimensionEnum(), outlet.Position);
                return identifier;
            }

            // Mention 'no name' only
            return $"'{ResourceFormatter.GetOperatorTypeDisplayName(entity)}'";
        }

        public static string GetUserFriendlyIdentifier_ForSampleOperator(Operator entity, ISampleRepository sampleRepository)
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
                int? underlyingEntityID = DataPropertyParser.TryParseInt32(entity, nameof(Sample_OperatorWrapper.SampleID));
                // ReSharper disable once InvertIf
                if (underlyingEntityID.HasValue)
                {
                    Sample underlyingEntity = sampleRepository.TryGet(underlyingEntityID.Value);
                    if (underlyingEntity != null)
                    {
                        return GetUserFriendlyIdentifier(underlyingEntity);
                    }
                }
            }

            // Mention 'no name' only
            return $"'{ResourceFormatter.GetOperatorTypeDisplayName(entity)}'";
        }

        private static string GetUserFriendlyIdentifier_ForOtherOperartorType(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // Use Operator Name
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            // Mention 'no name' only
            return $"'{ResourceFormatter.GetOperatorTypeDisplayName(entity)}'";
        }

        public static string GetUserFriendlyIdentifier(Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return GetUserFriendlyIdentifier_WithName_AndNoNameFallback(entity.Name);
        }

        public static string GetUserFriendlyIdentifier(Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            if (!string.IsNullOrWhiteSpace(entity.OriginalLocation))
            {
                return $"'{entity.OriginalLocation}'";
            }

            // Message prefix would become something like: 
            // Sample '16-Bit Mono 44100Hz WAV-File'
            // ReSharper disable once UseStringInterpolation

            var sb = new StringBuilder();

            sb.Append('\'');

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (entity.SampleDataType != null)
            {
                sb.Append(ResourceFormatter.GetDisplayName(entity.SampleDataType));
            }

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (entity.SpeakerSetup != null)
            {
                sb.Append(ResourceFormatter.GetDisplayName(entity.SpeakerSetup));
            }

            sb.Append($"{entity.SamplingRate}Hz");

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (entity.AudioFileFormat != null)
            {
                sb.Append(ResourceFormatter.GetDisplayName(entity.AudioFileFormat));
            }

            sb.Append('\'');

            return sb.ToString();
        }

        public static string GetUserFriendlyIdentifier(Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                return $"'{entity.Name}'";
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (entity.ScaleType != null)
            {
                return $"'{ResourceFormatter.GetScaleTypeDisplayNamePlural(entity)}'";
            }

            return GetNoNameIdentifier();
        }

        public static string GetUserFriendlyIdentifier(Tone entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var sb = new StringBuilder();

            sb.Append('\'');
            sb.Append($"{ResourceFormatter.Octave} {entity.Octave}, ");

            // ReSharper disable once ConstantConditionalAccessQualifier
            if (entity.Scale?.ScaleType != null)
            {
                sb.Append($"{ResourceFormatter.GetScaleTypeDisplayNameSingular(entity.Scale)} ");
            }

            sb.Append($"{FormatNumber(entity.Number)}");
            sb.Append('\'');

            return sb.ToString();
        }

        // Helpers

        private static string GetUserFriendlyIdentifier_WithName_AndNoNameFallback([CanBeNull] string name)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!string.IsNullOrWhiteSpace(name))
            {
                return $"'{name}'";
            }
            else
            {
                return GetNoNameIdentifier();
            }
        }

        private static string GetUserFriendlyIdentifier_WithName_DimensionEnum_AndPosition([CanBeNull] string name, DimensionEnum dimensionEnum, int position)
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

            // Use Position
            string identifier = $"({ResourceFormatter.Position} = {position})";
            return identifier;
        }

        private static string GetNoNameIdentifier()
        {
            return $"'{CommonResourceFormatter.NoObject_WithName(CommonResourceFormatter.Name)}'";
        }

        private static string FormatNumber(double number)
        {
            return $"{number:0.######}";
        }
    }
}