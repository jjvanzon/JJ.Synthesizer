using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ScalerWithOrigin_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Scaler);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto SourceValueAOperatorDto => InputOperatorDtos[1];
        public OperatorDto SourceValueBOperatorDto => InputOperatorDtos[2];
        public OperatorDto TargetValueAOperatorDto => InputOperatorDtos[3];
        public OperatorDto TargetValueBOperatorDto => InputOperatorDtos[4];

        public ScalerWithOrigin_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto sourceValueAOperatorDto,
            OperatorDto sourceValueBOperatorDto,
            OperatorDto targetValueAOperatorDto,
            OperatorDto targetValueBOperatorDto)
            : base(new OperatorDto[] {
                signalOperatorDto,
                sourceValueAOperatorDto,
                sourceValueBOperatorDto,

                targetValueAOperatorDto, targetValueBOperatorDto })
        { }
    }
}