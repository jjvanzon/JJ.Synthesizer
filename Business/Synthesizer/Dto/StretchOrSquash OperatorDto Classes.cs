using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class StretchOrSquash_OperatorDto : StretchOrSquash_OperatorDto_VarSignal_VarFactor_VarOrigin
    { }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public double Factor { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FactorOperatorDto }; }
            set { FactorOperatorDto = value[0]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Factor { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_ZeroOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, FactorOperatorDto }; }
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public double Factor { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_ConstFactor_VarOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public double Factor { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { OriginOperatorDto }; }
            set { OriginOperatorDto = value[0]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_VarFactor_ConstOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FactorOperatorDto }; }
            set { FactorOperatorDto = value[0]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_VarFactor_VarOrigin : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FactorOperatorDto, OriginOperatorDto }; }
            set { FactorOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_ConstOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Factor { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_VarOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Factor { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, OriginOperatorDto }; }
            set { SignalOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_ConstOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, FactorOperatorDto }; }
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_VarOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, FactorOperatorDto, OriginOperatorDto }; }
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }

    // For Time Dimension

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public double Factor { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }

    internal abstract class StretchOrSquash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking : OperatorDtoBase_WithDimension
    {
        public double Signal { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FactorOperatorDto }; }
            set { FactorOperatorDto = value[0]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Factor { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, FactorOperatorDto }; }
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }
    }
}
