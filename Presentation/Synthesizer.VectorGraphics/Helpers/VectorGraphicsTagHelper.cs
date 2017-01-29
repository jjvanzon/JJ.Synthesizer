using JJ.Framework.Exceptions;
using JJ.Framework.Common;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
    public static class VectorGraphicsTagHelper
    {
        // The the shorter the prefixes, the faster it will be.
        private const string OPERATOR_TAG_PREFIX = "op ";
        private const string INLET_TAG_PREFIX = "in ";
        private const string OUTLET_TAG_PREFIX = "out ";

        // Get Tag

        public static object GetOperatorTag(int operatorID)
        {
            return string.Format("{0}{1}", OPERATOR_TAG_PREFIX, operatorID);
        }

        public static object GetInletTag(int inletID)
        {
            return string.Format("{0}{1}", INLET_TAG_PREFIX, inletID);
        }

        public static object GetOutletTag(int outletID)
        {
            return string.Format("{0}{1}", OUTLET_TAG_PREFIX, outletID);
        }

        // Is Tag

        public static bool IsOperatorTag(object tag)
        {
            return IsTag(tag, OPERATOR_TAG_PREFIX);
        }

        public static bool IsInletTag(object tag)
        {
            return IsTag(tag, INLET_TAG_PREFIX);
        }

        public static bool IsOutletTag(object tag)
        {
            return IsTag(tag, OUTLET_TAG_PREFIX);
        }

        private static bool IsTag(object tag, string tagPrefix)
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

        public static int? TryGetOperatorID(object tag)
        {
            if (!IsOperatorTag(tag))
            {
                return null;
            }

            int id = GetOperatorID(tag);
            return id;
        }

        public static int? TryGetInletID(object tag)
        {
            if (!IsInletTag(tag))
            {
                return null;
            }
            
            int id = GetInletID(tag);
            return id;
        }

        public static int? TryGetOutletID(object tag)
        {
            if (!IsOutletTag(tag))
            {
                return null;
            }
            
            int id = GetOutletID(tag);
            return id;
        }

        // Get ID

        public static int GetOperatorID(object tag)
        {
            return GetInt32(tag, OPERATOR_TAG_PREFIX);
        }

        public static int GetInletID(object tag)
        {
            return GetInt32(tag, INLET_TAG_PREFIX);
        }

        public static int GetOutletID(object tag)
        {
            return GetInt32(tag, OUTLET_TAG_PREFIX);
        }

        public static int GetNodeID(object tag)
        {
            return Convert.ToInt32(tag);
        }

        private static int GetInt32(object tag, string tagPrefix)
        {
            if (tag == null)
            {
                throw new NullException(() => tag);
            }

            string tagString = Convert.ToString(tag);

            if (!tagString.StartsWith(tagPrefix))
            {
                throw new Exception(string.Format("tag '{0}' does not start with '{1}'.", tag, tagPrefix));
            }

            string idString = tagString.CutLeft(tagPrefix.Length);

            return int.Parse(idString);
        }
    }
}
