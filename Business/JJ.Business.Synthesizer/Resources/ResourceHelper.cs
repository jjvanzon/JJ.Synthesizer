using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Resources
{
    public static class ResourceHelper
    {
        /// <summary> You can use this overload if the object resourceName's ToString converts it to the resource key. </summary>
        public static string GetPropertyDisplayName(object resourceName)
        {
            return GetPropertyDisplayName(resourceName.ToString());
        }

        public static string GetPropertyDisplayName(string resourceName)
        {
            string str = PropertyDisplayNames.ResourceManager.GetString(resourceName);
            return str;
        }

        // InletType

        public static string GetInletTypeDisplayName(InletTypeEnum enumValue)
        {
            return PropertyDisplayNames.ResourceManager.GetString(enumValue.ToString());
        }

        public static string GetInletTypeDisplayName(InletType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return PropertyDisplayNames.ResourceManager.GetString(entity.Name);
        }

        // OperatorType

        public static string GetOperatorTypeDisplayName(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            return GetOperatorTypeDisplayName(op.GetOperatorTypeEnum());
        }

        public static string GetOperatorTypeDisplayName(OperatorType operatorType)
        {
            if (operatorType == null) throw new NullException(() => operatorType);

            return PropertyDisplayNames.ResourceManager.GetString(operatorType.Name);
        }

        public static string GetOperatorTypeDisplayName(OperatorTypeEnum operatorTypeEnum)
        {
            return PropertyDisplayNames.ResourceManager.GetString(operatorTypeEnum.ToString());
        }

        // OutletType

        public static string GetOutletTypeDisplayName(OutletTypeEnum enumValue)
        {
            return PropertyDisplayNames.ResourceManager.GetString(enumValue.ToString());
        }

        public static string GetOutletTypeDisplayName(OutletType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return PropertyDisplayNames.ResourceManager.GetString(entity.Name);
        }

        // ScaleType Singular

        public static string GetScaleTypeDisplayNameSingular(Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            return GetScaleTypeDisplayNameSingular(scale.ScaleType);
        }

        public static string GetScaleTypeDisplayNameSingular(ScaleType scaleType)
        {
            if (scaleType == null) throw new NullException(() => scaleType);

            return GetScaleTypeDisplayNameSingular(scaleType.Name);
        }

        public static string GetScaleTypeDisplayNameSingular(ScaleTypeEnum scaleTypeEnum)
        {
            return GetScaleTypeDisplayNameSingular(scaleTypeEnum.ToString());
        }

        public static string GetScaleTypeDisplayNameSingular(string scaleTypeName)
        {
            return PropertyDisplayNames.ResourceManager.GetString(scaleTypeName);
        }

        // ScaleType Plural

        public static string GetScaleTypeDisplayNamePlural(Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            return GetScaleTypeDisplayNamePlural(scale.ScaleType);
        }

        public static string GetScaleTypeDisplayNamePlural(ScaleType scaleType)
        {
            if (scaleType == null) throw new NullException(() => scaleType);

            return GetScaleTypeDisplayNamePlural(scaleType.Name);
        }

        public static string GetScaleTypeDisplayNamePlural(string scaleTypeName)
        {
            ScaleTypeEnum scaleTypeEnum = EnumHelper.Parse<ScaleTypeEnum>(scaleTypeName);
            return GetScaleTypeDisplayNamePlural(scaleTypeEnum);
        }

        // Notice that the deepest overload has a different parameter than the singular variation.
        public static string GetScaleTypeDisplayNamePlural(ScaleTypeEnum scaleTypeEnum)
        {
            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    return GetPropertyDisplayName(PropertyNames.LiteralFrequencies);

                case ScaleTypeEnum.Factor:
                    return GetPropertyDisplayName(PropertyNames.Factors);

                case ScaleTypeEnum.Exponent:
                    return GetPropertyDisplayName(PropertyNames.Exponents);

                case ScaleTypeEnum.SemiTone:
                    return GetPropertyDisplayName(PropertyNames.SemiTones);

                case ScaleTypeEnum.Cent:
                    return GetPropertyDisplayName(PropertyNames.Cents);

                case ScaleTypeEnum.Undefined:
                    // A direct call to ResourceManager.GetString does not crash if the key does not exist,
                    // so do not throw an exception here.
                    return GetScaleTypeDisplayNameSingular(scaleTypeEnum);

                default:
                    throw new InvalidValueException(scaleTypeEnum);
            }
        }
    }
}
