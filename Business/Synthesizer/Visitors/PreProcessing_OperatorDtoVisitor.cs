using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class PreProcessing_OperatorDtoVisitor
    {
        private readonly int _targetChannelCount;

        public PreProcessing_OperatorDtoVisitor(int targetChannelCount)
        {
            _targetChannelCount = targetChannelCount;
        }

        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            if (dto == null) throw new NullException(() => dto);

            dto = new ClassSpecialization_OperatorDtoVisitor(_targetChannelCount).Execute(dto);
            dto = new MathSimplification_OperatorDtoVisitor().Execute(dto);
            dto = new MachineOptimization_OperatorDtoVisitor().Execute(dto);

            return dto;
        }
    }
}
