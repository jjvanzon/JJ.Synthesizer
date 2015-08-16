using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;

namespace JJ.Business.Synthesizer.Resources
{
    public static class ResourceHelper
    {
        public static string GetPropertyDisplayName(string resourceName)
        {
            string str = PropertyDisplayNames.ResourceManager.GetString(resourceName);
            return str;
        }

        public static string GetOperatorTypeDisplayName(OperatorTypeEnum operatorTypeEnum)
        {
            return PropertyDisplayNames.ResourceManager.GetString(operatorTypeEnum.ToString());
        }
    }
}
