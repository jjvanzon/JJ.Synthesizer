using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Validation
{
    internal static class ValidationHelper
    {
        public static string GetMessagePrefix(AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.AudioFileOutput, entity.Name);
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

        public static string GetMessagePrefix(Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.Inlet, entity.Name);
        }

        /// <param name="i">1-based</param>
        public static string GetMessagePrefix(Node entity, int i)
        {
            if (entity == null) throw new NullException(() => entity);

            string messagePrefix = String.Format("{0} {1}: ", PropertyDisplayNames.Node, i);
            return messagePrefix;
        }

        public static string GetMessagePrefix_ForCustomOperator(Operator entity, IDocumentRepository documentRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (entity.GetOperatorTypeEnum() != OperatorTypeEnum.CustomOperator) throw new NotEqualException(() => entity.OperatorType, OperatorTypeEnum.CustomOperator);

            // Prefer Operator's explicit Name.
            if (!String.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(ResourceHelper.GetOperatorTypeDisplayName(entity), entity.Name);
            }

            var wrapper = new OperatorWrapper_CustomOperator(entity, documentRepository);
            Document underlyingEntity = wrapper.UnderlyingDocument;
            string underlyingEntityName = null;
            if (underlyingEntity != null)
            {
                underlyingEntityName = underlyingEntity.Name;
            }

            string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);

            string messagePrefix = GetMessagePrefix(operatorTypeDisplayName, underlyingEntityName);
            return messagePrefix;
        }

        public static string GetMessagePrefix(Operator entity, ISampleRepository sampleRepository, ICurveRepository curveRepository, IDocumentRepository documentRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            OperatorTypeEnum operatorTypeEnum = entity.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Undefined:
                    return GetMessagePrefix(PropertyDisplayNames.Operator, entity.Name);

                case OperatorTypeEnum.CustomOperator:
                    return GetMessagePrefix_ForCustomOperator(entity, documentRepository);
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
                        var wrapper = new OperatorWrapper_Number(entity);
                        string formattedValue = wrapper.Number.ToString("0.######");
                        return String.Format("{0} '{1}': ", ResourceHelper.GetOperatorTypeDisplayName(entity), formattedValue);
                    }

                case OperatorTypeEnum.Sample:
                    {
                        var wrapper = new OperatorWrapper_Sample(entity, sampleRepository);
                        Sample underlyingEntity = wrapper.Sample;
                        if (underlyingEntity != null)
                        {
                            if (!String.IsNullOrEmpty(underlyingEntity.Name))
                            {
                                return GetMessagePrefix(ResourceHelper.GetOperatorTypeDisplayName(operatorTypeEnum), underlyingEntity.Name);
                            }
                        }
                        break;
                    }

                case OperatorTypeEnum.Curve:
                    {
                        var wrapper = new OperatorWrapper_Curve(entity, curveRepository);
                        Curve underlyingEntity = wrapper.Curve;
                        if (underlyingEntity != null)
                        {
                            if (!String.IsNullOrEmpty(underlyingEntity.Name))
                            {
                                return GetMessagePrefix(ResourceHelper.GetOperatorTypeDisplayName(operatorTypeEnum), underlyingEntity.Name);
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

            return ResourceHelper.GetOperatorTypeDisplayName(operatorTypeEnum) + ": ";
        }

        public static string GetMessagePrefix(Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return GetMessagePrefix(PropertyDisplayNames.Outlet, entity.Name);
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

        internal static string GetMessagePrefix(Tone entity)
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

        public static string GetOperatorIdentifier(Operator op)
        {
            if (!String.IsNullOrEmpty(op.Name))
            {
                return op.Name;
            }

            if (op.OperatorType != null)
            {
                string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(op.OperatorType);
                return operatorTypeDisplayName;
            }

            return op.ID.ToString();
        }
    }
}
