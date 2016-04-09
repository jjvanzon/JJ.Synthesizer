using System;
using System.Linq.Expressions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Common.Exceptions;
using JJ.Framework.Reflection;
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

            if (String.IsNullOrEmpty(str))
            {
                str = resourceName;
            }

            return str;
        }

        public static string GetPropertyDisplayName(Expression<Func<object>> resourceNameExpression)
        {
            if (resourceNameExpression == null) throw new NullException(() => resourceNameExpression);

            string resourceName = ExpressionHelper.GetName(resourceNameExpression);
            string str = GetPropertyDisplayName(resourceName);
            return str;
        }

        // Dimension

        public static string GetDisplayName(Dimension entity)
        {
            if (entity == null) throw new NullException(() => entity);

            DimensionEnum dimensionEnum = (DimensionEnum)entity.ID;

            return GetDisplayName(dimensionEnum);
        }

        public static string GetDisplayName(DimensionEnum enumValue)
        {
            return GetPropertyDisplayName(enumValue.ToString());
        }

        // InterpolationType

        public static string GetDisplayName(InterpolationType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            InterpolationTypeEnum dimensionEnum = (InterpolationTypeEnum)entity.ID;

            return GetDisplayName(dimensionEnum);
        }

        public static string GetDisplayName(InterpolationTypeEnum enumValue)
        {
            return GetPropertyDisplayName(enumValue.ToString());
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
            return GetPropertyDisplayName(enumValue.ToString());
        }

        // SpeakerSetup

        public static string GetDisplayName(SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);

            SpeakerSetupEnum dimensionEnum = (SpeakerSetupEnum)entity.ID;

            return GetDisplayName(dimensionEnum);
        }

        public static string GetDisplayName(SpeakerSetupEnum enumValue)
        {
            return GetPropertyDisplayName(enumValue.ToString());
        }

        // ResampleInterpolationType

        public static string GetDisplayName(ResampleInterpolationTypeEnum enumValue)
        {
            return GetPropertyDisplayName(enumValue.ToString());
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
            return GetPropertyDisplayName(enumValue.ToString());
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

        // FilterType

        public static string GetDisplayName(FilterTypeEnum filterTypeEnum)
        {
            return GetPropertyDisplayName(filterTypeEnum.ToString());
        }
    }
}
