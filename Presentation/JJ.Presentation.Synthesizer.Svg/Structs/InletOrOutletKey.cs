using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Structs
{
    public struct InletOrOutletKey
    {
        public InletOrOutletKey(int operatorIndexNumber, int listIndex)
        {
            OperatorIndexNumber = operatorIndexNumber;
            ListIndex = listIndex;
        }

        public int OperatorIndexNumber;
        public int ListIndex;

        public override int GetHashCode()
        {
            return OperatorIndexNumber.GetHashCode() ^ ListIndex.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = (InletOrOutletKey)obj;

            return OperatorIndexNumber == other.OperatorIndexNumber &&
                   ListIndex == other.ListIndex;
        }

        public static bool operator ==(InletOrOutletKey first, InletOrOutletKey second)
        {
            return Equals(first, second);
        }

        public static bool operator !=(InletOrOutletKey first, InletOrOutletKey second)
        {
            return !Equals(first, second);
        }
    }
}
