using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg
{
    public static class TagHelper
    {
        // The the shorter the prefixes, the faster it is.
        private const string OPERATOR_TAG_PREFIX = "op ";
        private const string INLET_TAG_PREFIX = "in ";
        private const string OUTLET_TAG_PREFIX = "out ";

        // Get Tag

        internal static object GetOperatorTag(int operatorID)
        {
            return String.Format("{0}{1}", OPERATOR_TAG_PREFIX, operatorID);
        }

        internal static object GetInletTag(int inletID)
        {
            return String.Format("{0}{1}", INLET_TAG_PREFIX, inletID);
        }

        internal static object GetOutletTag(int outletID)
        {
            return String.Format("{0}{1}", OUTLET_TAG_PREFIX, outletID);
        }

        // Get ID

        public static int GetOperatorID(object tag)
        {
            return GetID(tag, OPERATOR_TAG_PREFIX);
        }

        public static int GetInletID(object tag)
        {
            return GetID(tag, INLET_TAG_PREFIX);
        }

        public static int GetOutletID(object tag)
        {
            return GetID(tag, OUTLET_TAG_PREFIX);
        }

        private static int GetID(object tag, string tagPrefix)
        {
            if (tag == null)
            {
                throw new NullException(() => tag);
            }

            string tagString = Convert.ToString(tag);

            if (!tagString.StartsWith(tagPrefix))
            {
                throw new Exception(String.Format("tag '{0}' doest not start with '{1}'.", tag, tagPrefix));
            }

            string idString = tagString.CutLeft(tagPrefix.Length);

            return Int32.Parse(idString);
        }

        // Is Tag

        public static bool IsOperatorTag(object tag)
        {
            return IsIDTag(tag, OPERATOR_TAG_PREFIX);
        }

        public static bool IsInletTag(object tag)
        {
            return IsIDTag(tag, INLET_TAG_PREFIX);
        }

        public static bool IsOutletTag(object tag)
        {
            return IsIDTag(tag, OUTLET_TAG_PREFIX);
        }

        private static bool IsIDTag(object tag, string tagPrefix)
        {
            if (tag == null)
            {
                return false;
            }

            string tagString = Convert.ToString(tag);

            if (!tagString.StartsWith(tagPrefix))
            {
                return false;
            }

            // TRADE-OFF: It is not full-proof to not check that the rest of the tag parses to an ID, but it is much faster this way.
            return true;
        }

        // TryGet ID

        public static int TryGetOperatorID(object tag)
        {
            return TryGetID(tag, OPERATOR_TAG_PREFIX);
        }

        public static int TryGetInletID(object tag)
        {
            return TryGetID(tag, INLET_TAG_PREFIX);
        }

        public static int TryGetOutletID(object tag)
        {
            return TryGetID(tag, OUTLET_TAG_PREFIX);
        }

        private static int TryGetID(object tag, string tagPrefix)
        {
            if (tag == null)
            {
                return 0;
            }

            string tagString = tag as string;
            if (tagString == null)
            {
                return 0;
            }

            if (!tagString.StartsWith(tagPrefix))
            {
                return 0;
            }

            string idString = tagString.CutLeft(tagPrefix.Length);
            int id = 0;
            Int32.TryParse(idString, out id);
            return id;
        }
    }
}
