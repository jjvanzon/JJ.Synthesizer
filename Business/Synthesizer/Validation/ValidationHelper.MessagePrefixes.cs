using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation
{
    internal static partial class ValidationHelper
    {
        public static string GetMessagePrefix(AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.AudioFileOutput, entity.Name);
        }

        public static string GetMessagePrefix(AudioOutput audioOutput)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);

            return PropertyDisplayNames.AudioOutput;
        }

        public static string GetMessagePrefix(Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.Curve, entity.Name);
        }

        public static string GetMessagePrefix(Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.Document, entity.Name);
        }

        /// <param name="number">1-based</param>
        public static string GetMessagePrefix(Inlet entity, int? number = null)
        {
            if (entity == null) throw new NullException(() => entity);

            if (!String.IsNullOrEmpty(entity.Name))
            {
                string messagePrefix = String.Format("{0} '{1}': ", PropertyDisplayNames.Inlet, entity.Name);
                return messagePrefix;
            }
            else if (number.HasValue)
            {
                string messagePrefix = String.Format("{0} {1}: ", PropertyDisplayNames.Inlet, number);
                return messagePrefix;
            }
            else
            {
                string messagePrefix = PropertyDisplayNames.Inlet + ": ";
                return messagePrefix;
            }
        }

        /// <param name="number">1-based</param>
        public static string GetMessagePrefix(Node entity, int number)
        {
            if (entity == null) throw new NullException(() => entity);

            string messagePrefix = String.Format("{0} {1}: ", PropertyDisplayNames.Node, number);
            return messagePrefix;
        }

        public static string GetMessagePrefix(
            Operator entity,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository)
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

        private static string GetMessagePrefix_ForCurveOperator(Operator entity, ICurveRepository curveRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);

            string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);

            // Use Operator Name
            if (!String.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
            }

            // Use Underlying Entity Name
            if (DataPropertyParser.DataIsWellFormed(entity))
            {
                int? underlyingEntityID = DataPropertyParser.TryParseInt32(entity, PropertyNames.CurveID);
                if (underlyingEntityID.HasValue)
                {
                    Curve underlyingEntity = curveRepository.TryGet(underlyingEntityID.Value);
                    if (underlyingEntity != null)
                    {
                        if (!String.IsNullOrEmpty(underlyingEntity.Name))
                        {
                            return GetMessagePrefix(operatorTypeDisplayName, underlyingEntity.Name);
                        }
                    }
                }
            }

            // Use OperatorTypeDisplayName only
            return operatorTypeDisplayName + ": ";
        }

        private static string GetMessagePrefix_ForCustomOperator(Operator entity, IPatchRepository patchRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);

            // Use Operator Name
            if (!String.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
            }

            // Use Underlying Entity Name
            if (DataPropertyParser.DataIsWellFormed(entity))
            {
                int? underlyingEntityID = DataPropertyParser.TryParseInt32(entity, PropertyNames.UnderlyingPatchID);
                if (underlyingEntityID.HasValue)
                {
                    Patch underlyingEntity = patchRepository.TryGet(underlyingEntityID.Value);
                    if (underlyingEntity != null)
                    {
                        if (!String.IsNullOrEmpty(underlyingEntity.Name))
                        {
                            return GetMessagePrefix(operatorTypeDisplayName, underlyingEntity.Name);
                        }
                    }
                }
            }

            // Use OperatorTypeDisplayName only
            return operatorTypeDisplayName + ": ";
        }

        private static string GetMessagePrefix_ForNumberOperator(Operator entity)
        {
            string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);

            // Use Operator Name
            if (!String.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
            }

            // Use Number
            if (DataPropertyParser.DataIsWellFormed(entity.Data))
            {
                double? number = DataPropertyParser.TryParseDouble(entity.Data, PropertyNames.Number);
                if (number.HasValue)
                {
                    string formattedValue = number.Value.ToString("0.######");
                    string messagePrefix = String.Format("{0} '{1}': ", operatorTypeDisplayName, formattedValue);
                    return messagePrefix;
                }
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        private static string GetMessagePrefix_ForPatchInlet(Operator entity)
        {
            string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);

            // Use Name
            if (!String.IsNullOrEmpty(entity.Name))
            {
                string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, entity.Name);
                return messagePrefix;
            }

            // Use Dimension
            if (entity.Inlets.Count == 1)
            {
                Inlet inlet = entity.Inlets[0];
                DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
                if (dimensionEnum != DimensionEnum.Undefined)
                {
                    string dimensionDisplayName = ResourceHelper.GetDisplayName(dimensionEnum);
                    string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, dimensionDisplayName);
                    return messagePrefix;
                }
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        private static string GetMessagePrefix_ForPatchOutlet(Operator entity)
        {
            string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);

            // Use Name
            if (!String.IsNullOrEmpty(entity.Name))
            {
                string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, entity.Name);
                return messagePrefix;
            }

            // Use Dimension
            if (entity.Outlets.Count == 1)
            {
                Outlet outlet = entity.Outlets[0];
                DimensionEnum dimensionEnum = outlet.GetDimensionEnum();
                if (dimensionEnum != DimensionEnum.Undefined)
                {
                    string dimensionDisplayName = ResourceHelper.GetDisplayName(dimensionEnum);
                    string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, dimensionDisplayName);
                    return messagePrefix;
                }
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        private static string GetMessagePrefix_ForSampleOperator(Operator entity, ISampleRepository sampleRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);

            // Use Operator Name
            if (!String.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
            }

            // Use Underlying Entity Name
            if (DataPropertyParser.DataIsWellFormed(entity))
            {
                int? underlyingEntityID = DataPropertyParser.TryParseInt32(entity, PropertyNames.SampleID);
                if (underlyingEntityID.HasValue)
                {
                    Sample underlyingEntity = sampleRepository.TryGet(underlyingEntityID.Value);
                    if (underlyingEntity != null)
                    {
                        if (!String.IsNullOrEmpty(underlyingEntity.Name))
                        {
                            return GetMessagePrefix(operatorTypeDisplayName, underlyingEntity.Name);
                        }
                    }
                }
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        private static string GetMessagePrefix_ForUndefinedOperatorType(Operator entity)
        {
            return GetMessagePrefix(PropertyDisplayNames.Operator, entity.Name);
        }

        private static string GetMessagePrefix_ForOtherOperartorType(Operator entity)
        {
            string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);

            // Use Name
            if (!String.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(operatorTypeDisplayName, entity.Name);
            }

            // Use OperatorTypeDisplayName
            return operatorTypeDisplayName + ": ";
        }

        /// <param name="number">1-based</param>
        public static string GetMessagePrefix(Outlet entity, int number)
        {
            if (entity == null) throw new NullException(() => entity);

            string messagePrefix = String.Format("{0} {1}: ", PropertyDisplayNames.Outlet, number);
            return messagePrefix;
        }

        /// <param name="number">1-based</param>
        public static string GetMessagePrefix(Outlet entity, int? number = null)
        {
            if (entity == null) throw new NullException(() => entity);

            if (!String.IsNullOrEmpty(entity.Name))
            {
                string messagePrefix = String.Format("{0} '{1}': ", PropertyDisplayNames.Outlet, entity.Name);
                return messagePrefix;
            }
            else if (number.HasValue)
            {
                string messagePrefix = String.Format("{0} {1}: ", PropertyDisplayNames.Outlet, number);
                return messagePrefix;
            }
            else
            {
                string messagePrefix = PropertyDisplayNames.Outlet + ": ";
                return messagePrefix;
            }
        }

        public static string GetMessagePrefix(Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.Patch, entity.Name);
        }

        public static string GetMessagePrefix(Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.Sample, entity.Name);
        }

        public static string GetMessagePrefix(Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.Scale, entity.Name);
        }

        public static string GetMessagePrefix(Tone entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // TODO: Make a better message prefix
            return PropertyDisplayNames.Tone;
        }

        /// <summary>
        /// Uses the name in the message or otherwise the entityTypeDisplayName.
        /// </summary>
        private static string GetMessagePrefix(string entityTypeDisplayName, string name)
        {
            string messagePrefix;
            if (String.IsNullOrEmpty(name))
            {
                messagePrefix = String.Format("{0}: ", entityTypeDisplayName);
            }
            else
            {
                messagePrefix = String.Format("{0} '{1}': ", entityTypeDisplayName, name);
            }
            return messagePrefix;
        }
    }
}
