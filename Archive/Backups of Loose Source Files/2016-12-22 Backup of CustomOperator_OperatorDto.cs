//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal class CustomOperator_OperatorDto : OperatorDtoBase
//    {
//        public override string OperatorTypeName => nameof(OperatorTypeEnum.CustomOperator);

//        public string OutletName { get; set; }
//        public DimensionEnum OutletDimensionEnum { get; set; }

//        /// <summary> Not consecutive. Does not necessarily start at 0. </summary>
//        public int? OutletNumber { get; set; }
//        public int? UnderlyingPatchID { get; set; }

//        public int OperatorID { get; set; }
//        public int OutletListIndex { get; set; }

//        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; }
//    }
//}