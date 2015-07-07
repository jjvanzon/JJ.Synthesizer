//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JJ.Presentation.Synthesizer.Svg.Structs
//{
//    internal struct OutletKey
//    {
//        public int OperatorIndexNumber { get; set; }
//        public int OutletListIndex { get; set; }

//        public override int GetHashCode()
//        {
//            return OperatorIndexNumber.GetHashCode() ^ OutletListIndex.GetHashCode();
//        }

//        public override bool Equals(object obj)
//        {
//            var other = (OutletKey)obj;

//            return OperatorIndexNumber == other.OperatorIndexNumber &&
//                   OutletListIndex == other.OutletListIndex;
//        }
//    }
//}
