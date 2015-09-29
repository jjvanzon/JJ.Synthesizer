using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Resources
{
    public static class ResourceHelper
    {
        public static string GetPropertyDisplayName(string resourceName)
        {
            string str = PropertyDisplayNames.ResourceManager.GetString(resourceName);
            return str;
        }

        public static string GetOperatorTypeDisplayName(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            return GetOperatorTypeDisplayName(op.GetOperatorTypeEnum());
        }

        public static string GetOperatorTypeDisplayName(OperatorTypeEnum operatorTypeEnum)
        {
            return PropertyDisplayNames.ResourceManager.GetString(operatorTypeEnum.ToString());
        }

        public static string GetScaleTypeDisplayName(Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            return GetScaleTypeDisplayName(scale.ScaleType);
        }

        public static string GetScaleTypeDisplayName(ScaleType scaleType)
        {
            if (scaleType == null) throw new NullException(() => scaleType);

            return PropertyDisplayNames.ResourceManager.GetString(scaleType.Name);
        }

        public static string GetScaleTypeDisplayName(ScaleTypeEnum operatorTypeEnum)
        {
            return PropertyDisplayNames.ResourceManager.GetString(operatorTypeEnum.ToString());
        }
    }
}
