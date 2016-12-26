//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal class Unbundle_OperatorDto : OperatorDtoBase_WithDimension
//    {
//        public override string OperatorTypeName => nameof(OperatorTypeEnum.Unbundle);
        
//        public OperatorDtoBase InputOperatorDto { get; set; }
//        public int OutletListIndex { get; set; }

//        public override IList<OperatorDtoBase> InputOperatorDtos
//        {
//            get { return new OperatorDtoBase[] { InputOperatorDto }; }
//            set { InputOperatorDto = value[0]; }
//        }
//    }
//}
