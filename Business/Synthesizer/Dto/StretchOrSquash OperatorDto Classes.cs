using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    /// <summary> Base class. </summary>
    internal abstract class StretchOrSquash_OperatorDtoBase_WithOrigin : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public InputDto Signal { get; set; }
        public InputDto Factor { get; set; }
        public InputDto Origin { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Factor, Origin };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Factor = array[1];
                Origin = array[2];
            }
        }
    }

    /// <summary> Base class. </summary>
    internal abstract class StretchOrSquash_OperatorDtoBase_ZeroOrigin : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public InputDto Signal { get; set; }
        public InputDto Factor { get; set; }
        private static readonly InputDto _origin = 0;

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Factor, _origin };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Factor = array[1];
            }
        }
    }

    /// <summary> Base class. </summary>
    internal abstract class StretchOrSquash_OperatorDtoBase_NoOrigin : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public InputDto Signal { get; set; }
        public InputDto Factor { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Factor };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Factor = array[1];
            }
        }
    }
}
