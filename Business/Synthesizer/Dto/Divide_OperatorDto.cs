using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Divide_OperatorDto : Divide_OperatorDto_VarNumerator_VarDenominator_VarOrigin
    { }

    internal class Divide_OperatorDto_VarNumerator_VarDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { NumeratorOperatorDto, DenominatorOperatorDto, OriginOperatorDto };

    }

    internal class Divide_OperatorDto_VarNumerator_VarDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { NumeratorOperatorDto, DenominatorOperatorDto };
    }

    internal class Divide_OperatorDto_VarNumerator_VarDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { NumeratorOperatorDto, DenominatorOperatorDto };
    }

    internal class Divide_OperatorDto_VarNumerator_ConstDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public double Denominator { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { NumeratorOperatorDto, OriginOperatorDto };
    }

    internal class Divide_OperatorDto_VarNumerator_ConstDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public double Denominator { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { NumeratorOperatorDto };
    }

    internal class Divide_OperatorDto_VarNumerator_ConstDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public double Denominator { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { NumeratorOperatorDto };
    }

    internal class Divide_OperatorDto_ConstNumerator_VarDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { DenominatorOperatorDto, OriginOperatorDto };
    }

    internal class Divide_OperatorDto_ConstNumerator_VarDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { DenominatorOperatorDto };
    }

    internal class Divide_OperatorDto_ConstNumerator_VarDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { DenominatorOperatorDto };
    }

    internal class Divide_OperatorDto_ConstNumerator_ConstDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public double Denominator { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { OriginOperatorDto };
    }

    internal class Divide_OperatorDto_ConstNumerator_ConstDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public double Denominator { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[0];
    }

    internal class Divide_OperatorDto_ConstNumerator_ConstDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public double Denominator { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[0];
    }
}
