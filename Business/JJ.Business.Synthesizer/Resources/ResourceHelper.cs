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
        private static ResourceManager _propertyDisplayNamesResourceManager;

        static ResourceHelper()
        {
            _propertyDisplayNamesResourceManager = new ResourceManager(
                typeof(PropertyDisplayNames).FullName, 
                typeof(PropertyDisplayNames).Assembly);
        }

        public static string GetPropertyDisplayName(string resourceName)
        {
            string str = _propertyDisplayNamesResourceManager.GetString(resourceName);
            return str;
        }
    }
}
