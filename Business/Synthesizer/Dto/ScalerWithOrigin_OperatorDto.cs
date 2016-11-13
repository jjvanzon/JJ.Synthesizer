using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ScalerWithOrigin_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Scaler);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase SourceValueAOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase SourceValueBOperatorDto => InputOperatorDtos[2];
        public OperatorDtoBase TargetValueAOperatorDto => InputOperatorDtos[3];
        public OperatorDtoBase TargetValueBOperatorDto => InputOperatorDtos[4];

        public ScalerWithOrigin_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase sourceValueAOperatorDto,
            OperatorDtoBase sourceValueBOperatorDto,
            OperatorDtoBase targetValueAOperatorDto,
            OperatorDtoBase targetValueBOperatorDto)
            : base(new OperatorDtoBase[] {
                signalOperatorDto,
                sourceValueAOperatorDto,
                sourceValueBOperatorDto,

                targetValueAOperatorDto, targetValueBOperatorDto })
        { }
    }
}