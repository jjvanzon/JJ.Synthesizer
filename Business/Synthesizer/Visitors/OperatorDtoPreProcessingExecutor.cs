using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Visitors
{
    /// <summary>
    /// This combinator class may currently seem yet another useless layer,
    /// but when multiple calculation output formats become possible, you need a combinator on this level.
    /// </summary>
    internal class OperatorDtoPreProcessingExecutor
    {
        private readonly int _samplingRate;
        private readonly int _targetChannelCount;

        public OperatorDtoPreProcessingExecutor(int samplingRate, int targetChannelCount)
        {
            _samplingRate = samplingRate;
            _targetChannelCount = targetChannelCount;
        }

        public IOperatorDto Execute(IOperatorDto dto)
        {
            if (dto == null) throw new NullException(() => dto);

            dto = new OperatorDtoVisitor_TransformationsToPositionInputs().Execute(dto);
            new OperatorDtoVisitor_InfrastructureVariables(_samplingRate, _targetChannelCount).Execute(dto);
            dto = new OperatorDtoVisitor_MathSimplification().Execute(dto);
            dto = new OperatorDtoVisitor_BooleanBoundaries().Execute(dto);
            dto = new OperatorDtoVisitor_MachineOptimization().Execute(dto);
            dto = new OperatorDtoVisitor_Rewriting().Execute(dto);
            new OperatorDtoVisitor_OperationIdentityAssignment().Execute(dto);
            dto = new OperatorDtoVisitor_OperationIdentityDeduplication().Execute(dto);

            return dto;
        }
    }
}