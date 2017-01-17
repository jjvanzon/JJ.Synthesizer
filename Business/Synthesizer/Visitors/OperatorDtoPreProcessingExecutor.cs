using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    /// <summary>
    /// This combinator class may currently seem yet another useless layer,
    /// but when multiple calculation output formats become possible, you need a combinator on this level.
    /// </summary>
    internal class OperatorDtoPreProcessingExecutor
    {
        private readonly int _targetChannelCount;

        public OperatorDtoPreProcessingExecutor(int targetChannelCount)
        {
            _targetChannelCount = targetChannelCount;
        }

        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            if (dto == null) throw new NullException(() => dto);

            dto = new OperatorDtoVisitor_MathSimplification(_targetChannelCount).Execute(dto);
            dto = new OperatorDtoVisitor_MachineOptimization().Execute(dto);
            dto = new OperatorDtoVisitor_ProgrammerLaziness().Execute(dto);
            new OperatorDtoVisitor_StackLevel_PerDimensionWriter().Execute(dto);

            return dto;
        }
    }
}
