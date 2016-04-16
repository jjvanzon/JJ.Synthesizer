using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

        public static string GetMessagePrefixForChildDocument(Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.ChildDocument, entity.Name);
        }

        /// <param name="number">1-based</param>
        public static string GetMessagePrefix(Inlet entity, int? number = null)
        {
            if (entity == null) throw new NullException(() => entity);

            if (number.HasValue)
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

        public static string GetMessagePrefix_ForCustomOperator(Operator entity, IPatchRepository patchRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (entity.GetOperatorTypeEnum() != OperatorTypeEnum.CustomOperator) throw new NotEqualException(() => entity.OperatorType, OperatorTypeEnum.CustomOperator);

            // Prefer Operator's explicit Name.
            if (!String.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(ResourceHelper.GetOperatorTypeDisplayName(entity), entity.Name);
            }

            var wrapper = new CustomOperator_OperatorWrapper(entity, patchRepository);
            Patch underlyingPatch = wrapper.UnderlyingPatch;
            string underlyingEntityName = null;
            if (underlyingPatch != null)
            {
                underlyingEntityName = underlyingPatch.Name;
            }

            string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);

            string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, underlyingEntityName);
            return messagePrefix;
        }

        public static string GetMessagePrefix(
            Operator entity, 
            ISampleRepository sampleRepository, 
            ICurveRepository curveRepository, 
            IPatchRepository patchRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            OperatorTypeEnum operatorTypeEnum = entity.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Undefined:
                    return GetMessagePrefix(PropertyDisplayNames.Operator, entity.Name);

                case OperatorTypeEnum.CustomOperator:
                    return GetMessagePrefix_ForCustomOperator(entity, patchRepository);
            }

            // Prefer Operator's explicit Name.
            if (!String.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(ResourceHelper.GetOperatorTypeDisplayName(entity), entity.Name);
            }

            // TODO: Give the OperatorTypes below their own specialized GetMessagePrefix method, just like GetMessagePrefix_ForCustomOperator.

            // No Operator Name: do specific thing for specific OperatorType
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Number:
                    {
                        var wrapper = new Number_OperatorWrapper(entity);
                        string formattedValue = wrapper.Number.ToString("0.######");
                        return String.Format("{0} '{1}': ", ResourceHelper.GetOperatorTypeDisplayName(entity), formattedValue);
                    }

                case OperatorTypeEnum.Sample:
                    {
                        var wrapper = new Sample_OperatorWrapper(entity, sampleRepository);
                        Sample underlyingEntity = wrapper.Sample;
                        if (underlyingEntity != null)
                        {
                            if (!String.IsNullOrEmpty(underlyingEntity.Name))
                            {
                                return GetMessagePrefix(ResourceHelper.GetDisplayName(operatorTypeEnum), underlyingEntity.Name);
                            }
                        }
                        break;
                    }

                case OperatorTypeEnum.Curve:
                    {
                        var wrapper = new Curve_OperatorWrapper(entity, curveRepository);
                        Curve underlyingEntity = wrapper.Curve;
                        if (underlyingEntity != null)
                        {
                            if (!String.IsNullOrEmpty(underlyingEntity.Name))
                            {
                                return GetMessagePrefix(ResourceHelper.GetDisplayName(operatorTypeEnum), underlyingEntity.Name);
                            }
                        }
                        break;
                    }
            }

            // There is no Name and it is not OperatorType 
            // Undefined, Value, Sample, Curve or CustomOperator.

            // Then only use OperatorTypeDisplayName.

            // NOTE: Do not put this in the default case, 
            // because there are more conditions than the switch value alone
            // that must fallback to this last resort.

            return ResourceHelper.GetDisplayName(operatorTypeEnum) + ": ";
        }

        /// <param name="number">1-based</param>
        public static string GetMessagePrefix(Outlet entity, int number)
        {
            if (entity == null) throw new NullException(() => entity);

            string messagePrefix = String.Format("{0} {1}: ", PropertyDisplayNames.Outlet, number);
            return messagePrefix;
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

        /// <param name="number">1-based</param>
        public static string GetMessagePrefix(AudioFileOutputChannel entity, int number)
        {
            if (entity == null) throw new NullException(() => entity);

            string messagePrefix = String.Format("{0} {1}: ", PropertyDisplayNames.Channel, number);
            return messagePrefix;
        }
    }
}
