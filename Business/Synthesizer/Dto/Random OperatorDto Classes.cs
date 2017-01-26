using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Random_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Random;

        public OperatorDtoBase RateOperatorDto { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }

        /// <summary> Used as a cache key. </summary>
        public int OperatorID { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { RateOperatorDto }; }
            set { RateOperatorDto = value[0]; }
        }
    }

    internal class Random_OperatorDto_Block : Random_OperatorDto
    { }

    internal class Random_OperatorDto_Stripe : Random_OperatorDto
    { }

    internal class Random_OperatorDto_Line : Random_OperatorDto
    { }

    internal class Random_OperatorDto_CubicEquidistant : Random_OperatorDto
    { }

    internal class Random_OperatorDto_CubicAbruptSlope : Random_OperatorDto
    { }

    internal class Random_OperatorDto_CubicSmoothSlope : Random_OperatorDto
    { }

    internal class Random_OperatorDto_Hermite : Random_OperatorDto
    { }
}
