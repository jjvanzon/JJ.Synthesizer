using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    // Const-Const-Zero does not exist.
    // Const-Var-Zero does not exist.

    internal abstract class StretchOrSquash_OperatorDto : StretchOrSquash_OperatorDto_VarSignal_VarFactor_VarOrigin
    { }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin : OperatorDtoBase_VarSignal
    {
        public double Factor { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_ZeroOrigin : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, FactorOperatorDto }; }
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }
    }

    // Const-Const-Const does not exist.
    // Const-Const-Var does not exist.
    // Const-Var-Const does not exist.
    // Const-Var-Var does not exist.

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_ConstOrigin : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Factor { get; set; }
        public double Origin { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_VarOrigin : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Factor { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, OriginOperatorDto }; }
            set { SignalOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_ConstOrigin : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }
        public double Origin { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, FactorOperatorDto }; }
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_VarOrigin : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, FactorOperatorDto, OriginOperatorDto }; }
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }

    // For Time Dimension

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, FactorOperatorDto }; }
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }
    }

    internal abstract class StretchOrSquash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting : OperatorDtoBase_VarSignal
    {
        public double Factor { get; set; }
    }
}
