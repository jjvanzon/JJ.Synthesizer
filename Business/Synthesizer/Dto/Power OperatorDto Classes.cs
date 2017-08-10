using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Power_OperatorDto : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Power;

        public InputDto Base { get; set; }
        public InputDto Exponent { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Base, Exponent };
            set
            {
                var array = value.ToArray();
                Base = array[0];
                Exponent = array[1];
            }
        }
    }

    internal class Power_OperatorDto_VarBase_VarExponent : Power_OperatorDto
    { }

    internal class Power_OperatorDto_VarBase_ConstExponent : Power_OperatorDto
    { }

    internal class Power_OperatorDto_ConstBase_VarExponent : Power_OperatorDto
    { }

    internal class Power_OperatorDto_ConstBase_ConstExponent : Power_OperatorDto
    { }

    /// <summary> For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_Exponent2 : Power_OperatorDto_VarBase_FixedExponent
    {
        public Power_OperatorDto_VarBase_Exponent2()
            : base(2)
        { }
    }

    /// <summary> For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_Exponent3 : Power_OperatorDto_VarBase_FixedExponent
    {
        public Power_OperatorDto_VarBase_Exponent3()
            : base(3)
        { }
    }

    /// <summary> For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_Exponent4 : Power_OperatorDto_VarBase_FixedExponent
    {
        public Power_OperatorDto_VarBase_Exponent4()
            : base(4)
        { }
    }

    /// <summary> Base class. For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_FixedExponent : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Power;

        public Power_OperatorDto_VarBase_FixedExponent(double exponent)
        {
            _exponent = InputDtoFactory.CreateInputDto(exponent);
        }

        private readonly InputDto _exponent;

        public InputDto Base { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Base, _exponent };
            set
            {
                var array = value.ToArray();
                Base = array[0];
            }
        }
    }
}
