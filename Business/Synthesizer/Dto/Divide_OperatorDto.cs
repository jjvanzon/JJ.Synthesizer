using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Divide_OperatorDto : Divide_OperatorDto_VarNumerator_VarDenominator_VarOrigin
    {
        public Divide_OperatorDto(OperatorDtoBase numeratorOperatorDto, OperatorDtoBase denominatorOperatorDto, OperatorDtoBase originOperatorDto)
            : base(numeratorOperatorDto, denominatorOperatorDto, originOperatorDto)
        { }
    }

    internal class Divide_OperatorDto_VarNumerator_VarDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase DenominatorOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase OriginOperatorDto => InputOperatorDtos[1];

        public Divide_OperatorDto_VarNumerator_VarDenominator_VarOrigin(
            OperatorDtoBase numeratorOperatorDto,
            OperatorDtoBase denominatorOperatorDto,
            OperatorDtoBase originOperatorDto)
            : base(new OperatorDtoBase[] { numeratorOperatorDto, denominatorOperatorDto, originOperatorDto })
        { }
    }

    internal class Divide_OperatorDto_VarNumerator_VarDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase DenominatorOperatorDto => InputOperatorDtos[1];
        public double Origin { get; set; }

        public Divide_OperatorDto_VarNumerator_VarDenominator_ConstOrigin(
            OperatorDtoBase numeratorOperatorDto,
            OperatorDtoBase denominatorOperatorDto,
            double origin)
            : base(new OperatorDtoBase[] { numeratorOperatorDto, denominatorOperatorDto })
        {
            Origin = origin;
        }
    }

    internal class Divide_OperatorDto_VarNumerator_VarDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase DenominatorOperatorDto => InputOperatorDtos[1];

        public Divide_OperatorDto_VarNumerator_VarDenominator_ZeroOrigin(
            OperatorDtoBase numeratorOperatorDto,
            OperatorDtoBase denominatorOperatorDto)
            : base(new OperatorDtoBase[] { numeratorOperatorDto, denominatorOperatorDto })
        { }
    }

    internal class Divide_OperatorDto_VarNumerator_ConstDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto => InputOperatorDtos[0];
        public double Denominator { get; set; }
        public OperatorDtoBase OriginOperatorDto => InputOperatorDtos[1];

        public Divide_OperatorDto_VarNumerator_ConstDenominator_VarOrigin(
            OperatorDtoBase numeratorOperatorDto,
            double denominator,
            OperatorDtoBase originOperatorDto)
            : base(new OperatorDtoBase[] { numeratorOperatorDto, originOperatorDto })
        {
            Denominator = denominator;
        }
    }

    internal class Divide_OperatorDto_VarNumerator_ConstDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto => InputOperatorDtos[0];
        public double Denominator { get; set; }
        public double Origin { get; set; }

        public Divide_OperatorDto_VarNumerator_ConstDenominator_ConstOrigin(
            OperatorDtoBase numeratorOperatorDto,
            double denominator,
            double origin)
            : base(new OperatorDtoBase[] { numeratorOperatorDto })
        {
            Denominator = denominator;
            Origin = origin;
        }
    }

    internal class Divide_OperatorDto_VarNumerator_ConstDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDtoBase NumeratorOperatorDto => InputOperatorDtos[0];
        public double Denominator { get; set; }

        public Divide_OperatorDto_VarNumerator_ConstDenominator_ZeroOrigin(
            OperatorDtoBase numeratorOperatorDto,
            double denominator)
            : base(new OperatorDtoBase[] { numeratorOperatorDto })
        {
            Denominator = denominator;
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_VarDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public OperatorDtoBase DenominatorOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase OriginOperatorDto => InputOperatorDtos[1];

        public Divide_OperatorDto_ConstNumerator_VarDenominator_VarOrigin(
            double numerator,
            OperatorDtoBase denominatorOperatorDto,
            OperatorDtoBase originOperatorDto)
            : base(new OperatorDtoBase[] { denominatorOperatorDto, originOperatorDto })
        {
            Numerator = numerator;
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_VarDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public OperatorDtoBase DenominatorOperatorDto => InputOperatorDtos[0];
        public double Origin { get; set; }

        public Divide_OperatorDto_ConstNumerator_VarDenominator_ConstOrigin(
            double numerator,
            OperatorDtoBase denominatorOperatorDto,
            double origin)
            : base(new OperatorDtoBase[] { denominatorOperatorDto } )
        {
            Numerator = numerator;
            Origin = origin;
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_VarDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public OperatorDtoBase DenominatorOperatorDto => InputOperatorDtos[0];

        public Divide_OperatorDto_ConstNumerator_VarDenominator_ZeroOrigin(
            double numerator,
            OperatorDtoBase denominatorOperatorDto)
            : base(new OperatorDtoBase[] { denominatorOperatorDto })
        {
            Numerator = numerator;
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_ConstDenominator_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public double Denominator { get; set; }
        public OperatorDtoBase OriginOperatorDto => InputOperatorDtos[0];

        public Divide_OperatorDto_ConstNumerator_ConstDenominator_VarOrigin(
            double numerator,
            double denominator,
            OperatorDtoBase originOperatorDto)
            : base(new OperatorDtoBase[] { originOperatorDto })
        {
            Numerator = numerator;
            Denominator = denominator;
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_ConstDenominator_ConstOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public double Denominator { get; set; }
        public double Origin { get; set; }

        public Divide_OperatorDto_ConstNumerator_ConstDenominator_ConstOrigin(
            double numerator,
            double denominator,
            double origin)
            : base(new OperatorDtoBase[0])
        {
            Numerator = numerator;
            Denominator = denominator;
            Origin = origin;
        }
    }

    internal class Divide_OperatorDto_ConstNumerator_ConstDenominator_ZeroOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public double Numerator { get; set; }
        public double Denominator { get; set; }

        public Divide_OperatorDto_ConstNumerator_ConstDenominator_ZeroOrigin(
            double numerator,
            double denominator)
            : base(new OperatorDtoBase[0])
        {
            Numerator = numerator;
            Denominator = denominator;
        }
    }
}
