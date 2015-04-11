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
        private const string OPERATOR_TAG_PREFIX = "OP ";
        private const string INLET_TAG_PREFIX = "IN ";
        private const string OUTLET_TAG_PREFIX = "OUT ";

        internal static string FormatOperatorTag(int operatorID)
        {
            return String.Format("{0}{1}", OPERATOR_TAG_PREFIX, operatorID);
        }

        internal static string FormatInletTag(int inletID)
        {
            return String.Format("{0}{1}", INLET_TAG_PREFIX, inletID);
        }

        internal static string FormatOutletTag(int outletID)
        {
            return String.Format("{0}{1}", OUTLET_TAG_PREFIX, outletID);
        }

        public static int GetOperatorID(string tag)
        {
            return GetID(tag, OPERATOR_TAG_PREFIX);
        }

        public static int GetInletID(string tag)
        {
            return GetID(tag, INLET_TAG_PREFIX);
        }

        public static int GetOutletID(string tag)
        {
            return GetID(tag, OUTLET_TAG_PREFIX);
        }

        private static int GetID(string tag, string tagPrefix)
        {
            if (tag == null)
            {
                throw new NullException(() => tag);
            }

            if (!tag.StartsWith(tagPrefix))
            {
                throw new Exception(String.Format("tag '{0}' doest not start with '{1}'.", tag, tagPrefix));
            }

            string idString = tag.CutLeft(tagPrefix.Length);

            return Int32.Parse(idString);
        }

        public static bool IsOperatorTag(string tag)
        {
            return IsIDTag(tag, OPERATOR_TAG_PREFIX);
        }

        public static bool IsInletTag(string tag)
        {
            return IsIDTag(tag, INLET_TAG_PREFIX);
        }

        public static bool IsOutletTag(string tag)
        {
            return IsIDTag(tag, OUTLET_TAG_PREFIX);
        }

        private static bool IsIDTag(string tag, string tagPrefix)
        {
            if (tag == null)
            {
                return false;
            }

            if (!tag.StartsWith(tagPrefix))
            {
                return false;
            }

            // TRADE-OFF: It is not full-proof to not check that the rest of the tag parses to an ID, but it is much faster this way.
            return true;
        }
    }
}
