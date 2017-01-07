using JJ.Business.SynthesizerPrototype.Dto;

namespace JJ.Business.SynthesizerPrototype.Visitors
{
    internal class OperatorDtoVisitor_StackLevel_Simple : OperatorDtoVisitorBase
    {
        private int _currentStackLevel;

        public void Execute(OperatorDtoBase dto)
        {
            _currentStackLevel = 0;

            Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            dto.DimensionStackLevel = _currentStackLevel;

            _currentStackLevel++;

            base.Visit_OperatorDto_Polymorphic(dto);

            _currentStackLevel--;

            return dto;
        }
    }
}
