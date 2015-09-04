using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
