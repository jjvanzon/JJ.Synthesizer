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

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { NumeratorOperatorDto, DenominatorOperatorDto, OriginOperatorDto }; }
            set { NumeratorOperatorDto = value[0]; DenominatorOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }

    internal class Divide_OperatorDto_VarNumerator_VarDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { NumeratorOperatorDto, DenominatorOperatorDto }; }
            set { NumeratorOperatorDto = value[0]; DenominatorOperatorDto = value[1]; }
        }
    }

    internal class Divide_OperatorDto_VarNumerator_VarDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { NumeratorOperatorDto, DenominatorOperatorDto }; }
            set { NumeratorOperatorDto = value[0]; DenominatorOperatorDto = value[1]; }
        }
    }

    internal class Divide_OperatorDto_VarNumerator_ConstDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public double Denominator { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { NumeratorOperatorDto, OriginOperatorDto }; }
            set { NumeratorOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal class Divide_OperatorDto_VarNumerator_ConstDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public double Denominator { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { NumeratorOperatorDto }; }
            set { NumeratorOperatorDto = value[0]; }
        }
    }

    internal class Divide_OperatorDto_VarNumerator_ConstDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto { get; set; }
        public double Denominator { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { NumeratorOperatorDto }; }
            set { NumeratorOperatorDto = value[0]; }
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_VarDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { DenominatorOperatorDto, OriginOperatorDto }; }
            set { DenominatorOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_VarDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { DenominatorOperatorDto }; }
            set { DenominatorOperatorDto = value[0]; }
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_VarDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public OperatorDtoBase DenominatorOperatorDto { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { DenominatorOperatorDto }; }
            set { DenominatorOperatorDto = value[0]; }
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_ConstDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public double Denominator { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { OriginOperatorDto }; }
            set { OriginOperatorDto = value[0]; }
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_ConstDenominator_ZeroOrigin : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public double Denominator { get; set; }
    }

    internal class Divide_OperatorDto_ConstNumerator_ConstDenominator_ConstOrigin : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public double Denominator { get; set; }
        public double Origin { get; set; }
    }
}
