using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Common.Exceptions;
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

        public static string GetDisplayName(InletType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            InletTypeEnum inletTypeEnum = (InletTypeEnum)entity.ID;

            return GetDisplayName(inletTypeEnum);
        }

        public static string GetDisplayName(InletTypeEnum enumValue)
        {
            string displayName = PropertyDisplayNames.ResourceManager.GetString(enumValue.ToString());

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            return displayName;
        }

        // InterpolationType

        public static string GetDisplayName(InterpolationType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            InterpolationTypeEnum inletTypeEnum = (InterpolationTypeEnum)entity.ID;

            return GetDisplayName(inletTypeEnum);
        }

        public static string GetDisplayName(InterpolationTypeEnum enumValue)
        {
            string displayName = PropertyDisplayNames.ResourceManager.GetString(enumValue.ToString());

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            return displayName;
        }

        // OperatorType

        public static string GetOperatorTypeDisplayName(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            return GetDisplayName(op.GetOperatorTypeEnum());
        }

        public static string GetDisplayName(OperatorType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            OperatorTypeEnum enumValue = (OperatorTypeEnum)entity.ID;

            return GetDisplayName(enumValue);
        }

        public static string GetDisplayName(OperatorTypeEnum enumValue)
        {
            string displayName = PropertyDisplayNames.ResourceManager.GetString(enumValue.ToString());

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            return displayName;
        }

        // OutletType

        public static string GetDisplayName(OutletType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            OutletTypeEnum outletTypeEnum = (OutletTypeEnum)entity.ID;

            return GetDisplayName(outletTypeEnum);
        }

        public static string GetDisplayName(OutletTypeEnum enumValue)
        {
            string displayName = PropertyDisplayNames.ResourceManager.GetString(enumValue.ToString());

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            return displayName;
        }

        // SpeakerSetup

        public static string GetDisplayName(SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);

            SpeakerSetupEnum outletTypeEnum = (SpeakerSetupEnum)entity.ID;

            return GetDisplayName(outletTypeEnum);
        }

        public static string GetDisplayName(SpeakerSetupEnum enumValue)
        {
            string displayName = PropertyDisplayNames.ResourceManager.GetString(enumValue.ToString());

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            return displayName;
        }

        // ResampleInterpolationType

        public static string GetDisplayName(ResampleInterpolationTypeEnum enumValue)
        {
            string displayName = PropertyDisplayNames.ResourceManager.GetString(enumValue.ToString());

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            return displayName;
        }

        // ScaleType Singular

        // TODO: For Scale implement overloads that take entity as such that unproxy is avoided.

        public static string GetScaleTypeDisplayNameSingular(Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            return GetDisplayNameSingular(scale.ScaleType);
        }

        public static string GetDisplayNameSingular(ScaleType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            ScaleTypeEnum enumValue = (ScaleTypeEnum)entity.ID;

            return GetDisplayNameSingular(enumValue);
        }

        public static string GetDisplayNameSingular(ScaleTypeEnum enumValue)
        {
            string displayName = PropertyDisplayNames.ResourceManager.GetString(enumValue.ToString());

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            return displayName;
        }

        // TODO: Perhaps remove this overload.
        internal static string GetScaleTypeDisplayNameSingular(string scaleTypeName)
        {
            return PropertyDisplayNames.ResourceManager.GetString(scaleTypeName);
        }

        // ScaleType Plural

        public static string GetScaleTypeDisplayNamePlural(Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            return GetDisplayNamePlural(scale.ScaleType);
        }

        public static string GetDisplayNamePlural(ScaleType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            ScaleTypeEnum enumValue = (ScaleTypeEnum)entity.ID;

            return GetDisplayNamePlural(enumValue);
        }

        // Notice that the deepest overload has a different parameter than the singular variation.
        public static string GetDisplayNamePlural(ScaleTypeEnum scaleTypeEnum)
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
                    return GetDisplayNameSingular(scaleTypeEnum);

                default:
                    throw new InvalidValueException(scaleTypeEnum);
            }
        }

        // TODO: Perhaps remove this overload
        internal static string GetScaleTypeDisplayNamePlural(string scaleTypeName)
        {
            ScaleTypeEnum scaleTypeEnum = EnumHelper.Parse<ScaleTypeEnum>(scaleTypeName);
            return GetDisplayNamePlural(scaleTypeEnum);
        }
    }
}
