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

        public static string GetMessagePrefix(Operator entity, ISampleRepository sampleRepository, ICurveRepository curveRepository, IDocumentRepository documentRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            OperatorTypeEnum operatorTypeEnum = entity.GetOperatorTypeEnum();

            if (operatorTypeEnum == OperatorTypeEnum.Undefined)
            {
                return GetMessagePrefix(PropertyDisplayNames.Operator, entity.Name);
            }

            // Prefer Operator's explicit Name.
            if (!String.IsNullOrEmpty(entity.Name))
            {
                return GetMessagePrefix(ResourceHelper.GetOperatorTypeDisplayName(entity), entity.Name);
            }

            // No Operator Name: do specific thing for specific OperatorType
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Value:
                    {
                        var wrapper = new Value_OperatorWrapper(entity);
                        string formattedValue = wrapper.Value.ToString("0.######");
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
                                return GetMessagePrefix(ResourceHelper.GetOperatorTypeDisplayName(operatorTypeEnum), underlyingEntity.Name);
                            }
                        }
                        break;
                    }

                case OperatorTypeEnum.CurveIn:
                    {
                        var wrapper = new CurveIn_OperatorWrapper(entity, curveRepository);
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

                case OperatorTypeEnum.CustomOperator:
                    {
                        var wrapper = new Custom_OperatorWrapper(entity, documentRepository);
                        Document underlyingEntity = wrapper.UnderlyingDocument;
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
            // Underfined, Value, Sample, Curve or CustomOperator.

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
