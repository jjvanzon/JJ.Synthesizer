using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_PreProcessing
    {
        private readonly int _targetChannelCount;

        public OperatorDtoVisitor_PreProcessing(int targetChannelCount)
        {
            _targetChannelCount = targetChannelCount;
        }

        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            if (dto == null) throw new NullException(() => dto);

            //dto = new OperatorDtoVisitor_ClassSpecialization(_targetChannelCount).Execute(dto);
            dto = new OperatorDtoVisitor_MathSimplification(_targetChannelCount).Execute(dto);
            dto = new OperatorDtoVisitor_MachineOptimization().Execute(dto);
            dto = new OperatorDtoVisitor_ProgrammerLaziness().Execute(dto);

            return dto;
        }
    }
}
