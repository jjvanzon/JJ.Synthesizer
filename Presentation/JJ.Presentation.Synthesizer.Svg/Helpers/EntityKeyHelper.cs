using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Presentation.Synthesizer.Svg.Structs;

namespace JJ.Presentation.Synthesizer.Svg.Helpers
{
    /// <summary>
    /// TODO: Rename to SvgTagHelper?
    /// </summary>
    public static class EntityKeyHelper
    {
        // The the shorter the prefixes, the faster it will be.
        private const string OPERATOR_TAG_PREFIX = "op ";
        private const string INLET_TAG_PREFIX = "in ";
        private const string OUTLET_TAG_PREFIX = "out ";
        private const string KEY_VALUE_SEPARATOR = "|";

        // Get Tag

        public static object GetOperatorTag(int operatorIndexNumber)
        {
            return String.Format("{0}{1}", OPERATOR_TAG_PREFIX, operatorIndexNumber);
        }

        public static object GetInletTag(InletOrOutletKey inletKey)
        {
            return GetInletTag(inletKey.OperatorIndexNumber, inletKey.ListIndex);
        }

        private static object GetInletTag(int operatorIndexNumber, int inletListIndex)
        {
            return String.Format("{0}{1}{2}{3}", INLET_TAG_PREFIX, operatorIndexNumber, KEY_VALUE_SEPARATOR, inletListIndex);
        }

        public static object GetOutletTag(InletOrOutletKey outletKey)
        {
            return GetOutletTag(outletKey.OperatorIndexNumber, outletKey.ListIndex);
        }

        private static object GetOutletTag(int operatorIndexNumber, int outletListIndex)
        {
            return String.Format("{0}{1}{2}{3}", OUTLET_TAG_PREFIX, operatorIndexNumber, KEY_VALUE_SEPARATOR, outletListIndex);
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

        // TryGet Key

        public static int? TryGetOperatorIndexNumber(object tag)
        {
            if (!IsOperatorTag(tag))
            {
                return null;
            }

            int operatorIndexNumber = GetOperatorIndexNumber(tag);
            return operatorIndexNumber;
        }

        public static InletOrOutletKey? TryGetInletKey(object tag)
        {
            if (!IsInletTag(tag))
            {
                return null;
            }

            InletOrOutletKey inletKey = GetInletKey(tag);
            return inletKey;
        }

        public static InletOrOutletKey? TryGetOutletKey(object tag)
        {
            if (!IsOutletTag(tag))
            {
                return null;
            }

            InletOrOutletKey outletKey = GetOutletKey(tag);
            return outletKey;
        }

        // Get Key

        public static int GetOperatorIndexNumber(object tag)
        {
            return GetInt32(tag, OPERATOR_TAG_PREFIX);
        }

        public static InletOrOutletKey GetInletKey(object tag)
        {
            return GetInletOrOutletKey(tag, INLET_TAG_PREFIX);
        }

        public static InletOrOutletKey GetOutletKey(object tag)
        {
            return GetInletOrOutletKey(tag, OUTLET_TAG_PREFIX);
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
                throw new Exception(String.Format("tag '{0}' does not start with '{1}'.", tag, tagPrefix));
            }

            string idString = tagString.CutLeft(tagPrefix.Length);

            return Int32.Parse(idString);
        }

        private static InletOrOutletKey GetInletOrOutletKey(object tag, string tagPrefix)
        {
            if (tag == null)
            {
                throw new NullException(() => tag);
            }

            string tagString = Convert.ToString(tag);

            if (!tagString.StartsWith(tagPrefix))
            {
                throw new Exception(String.Format("tag '{0}' does not start with '{1}'.", tag, tagPrefix));
            }

            string keyString = tagString.CutLeft(tagPrefix.Length);

            string[] keyValues = keyString.Split(KEY_VALUE_SEPARATOR);
            if (keyValues.Length != 2)
            {
                throw new Exception(String.Format("key '{0}' does not have 2 values separated by '{1}'.", keyString, KEY_VALUE_SEPARATOR));
            }

            string operatorIndexNumberString = keyValues[0];
            int operatorIndexNumber = 0;
            if (!Int32.TryParse(operatorIndexNumberString, out operatorIndexNumber))
            {
                throw new Exception(String.Format("operatorIndexNumberString '{0}' is not a valid Int32.", operatorIndexNumberString));
            }

            string inletOrOutletListIndexString = keyValues[1];
            int inletOrOutletListIndex = 0;
            if (!Int32.TryParse(inletOrOutletListIndexString, out inletOrOutletListIndex))
            {
                throw new Exception(String.Format("inletOrOutletListIndexString '{0}' is not a valid Int32.", inletOrOutletListIndexString));
            }

            return new InletOrOutletKey(operatorIndexNumber, inletOrOutletListIndex);
        }
    }
}
