using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class StretchOrSquash_OperatorDto : StretchOrSquash_OperatorDto_VarSignal_VarFactor_VarOrigin
    { }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public double Factor { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public IOperatorDto FactorOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FactorOperatorDto };
            set => FactorOperatorDto = value[0];
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public double Factor { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto };
            set => SignalOperatorDto = value[0];
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_ZeroOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto FactorOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, FactorOperatorDto };
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public double Factor { get; set; }
        public double Origin { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_ConstFactor_VarOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public double Factor { get; set; }
        public IOperatorDto OriginOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { OriginOperatorDto };
            set => OriginOperatorDto = value[0];
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_VarFactor_ConstOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public IOperatorDto FactorOperatorDto { get; set; }
        public double Origin { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FactorOperatorDto };
            set => FactorOperatorDto = value[0];
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_VarFactor_VarOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public IOperatorDto FactorOperatorDto { get; set; }
        public IOperatorDto OriginOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FactorOperatorDto, OriginOperatorDto };
            set { FactorOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_ConstOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public double Factor { get; set; }
        public double Origin { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto };
            set => SignalOperatorDto = value[0];
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_VarOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public double Factor { get; set; }
        public IOperatorDto OriginOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, OriginOperatorDto };
            set { SignalOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_ConstOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto FactorOperatorDto { get; set; }
        public double Origin { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, FactorOperatorDto };
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_VarOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto FactorOperatorDto { get; set; }
        public IOperatorDto OriginOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, FactorOperatorDto, OriginOperatorDto };
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }

    // For Time Dimension

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public double Factor { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public IOperatorDto FactorOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FactorOperatorDto };
            set => FactorOperatorDto = value[0];
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public double Factor { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto };
            set => SignalOperatorDto = value[0];
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto FactorOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, FactorOperatorDto };
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }
    }
}
