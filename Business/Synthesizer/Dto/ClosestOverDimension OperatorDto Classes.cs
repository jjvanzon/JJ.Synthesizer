using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverDimension_OperatorDto : OperatorDtoBase_WithCollectionRecalculation
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverDimension;

        public InputDto Input { get; set; }
        public InputDto Collection { get; set; }
        public InputDto From { get; set; }
        public InputDto Till { get; set; }
        public InputDto Step { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Input, Collection, From, Till, Step };
            set
            {
                var array = value.ToArray();
                Input = array[0];
                Collection = array[1];
                From = array[2];
                Till = array[3];
                Step = array[4];
            }
        }
    }

    internal class ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous : ClosestOverDimension_OperatorDto
    { }

    internal class ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset : ClosestOverDimension_OperatorDto
    { }
}