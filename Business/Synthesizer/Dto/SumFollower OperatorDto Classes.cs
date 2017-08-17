using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SumFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumFollower;
    }

    internal class SumFollower_OperatorDto_SignalVarOrConst_OtherInputsVar : OperatorDtoBase_AggregateFollower
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumFollower;
    }

    /// <summary> Slice length does not matter in this case. </summary>
    internal class SumFollower_OperatorDto_ConstSignal_VarSampleCount : SumFollower_OperatorDtoBase_WithoutSliceLength
    { }

    /// <summary> Slice length does not matter in this case. </summary>
    internal class SumFollower_OperatorDto_ConstSignal_ConstSampleCount : SumFollower_OperatorDtoBase_WithoutSliceLength
    { }

    /// <summary> Base class. </summary>
    internal abstract class SumFollower_OperatorDtoBase_WithoutSliceLength : OperatorDtoBase, IOperatorDto_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumFollower;

        public InputDto Signal { get; set; }
        public InputDto SampleCount { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, SampleCount };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                SampleCount = array[2];
            }
        }
    }
}