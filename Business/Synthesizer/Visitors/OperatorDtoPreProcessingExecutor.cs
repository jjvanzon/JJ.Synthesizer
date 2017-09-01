using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;

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

            new OperatorDtoVisitor_DimensionStackLevels().Execute(dto);
            dto = new OperatorDtoVisitor_TransformationsToPositionInputs().Execute(dto);
            new OperatorDtoVisitor_InfrastructureVariables(_samplingRate, _targetChannelCount).Execute(dto);
            dto = new OperatorDtoVisitor_MathSimplification().Execute(dto);
            dto = new OperatorDtoVisitor_BooleanBoundaries().Execute(dto);
            dto = new OperatorDtoVisitor_MachineOptimization().Execute(dto);
            dto = new OperatorDtoVisitor_Rewriting().Execute(dto);
            dto = new OperatorDtoVisitor_ProgrammerLaziness().Execute(dto);
            new OperatorDtoVisitor_OperationIdentityAssignment().Execute(dto);
            dto = new OperatorDtoVisitor_OperationIdentityDeduplication().Execute(dto);

            AssertZeroOperatorIDsWhereNeeded(dto);

            return dto;
        }

        private static readonly HashSet<OperatorTypeEnum> _operatorTypeEnums_With_OperatorID_0_Allowed = new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.Number,
            OperatorTypeEnum.DoubleToBoolean,
            OperatorTypeEnum.BooleanToDouble,
            OperatorTypeEnum.VariableInput
        };

        private static void AssertZeroOperatorIDsWhereNeeded(IOperatorDto dto)
        {
            IList<IOperatorDto> operatorDtosWithZeroOperatorID = dto.UnionRecursive(x => x.Inputs.Where(y => y.IsVar).Select(y => y.Var))
                                                                    .Where(x => !_operatorTypeEnums_With_OperatorID_0_Allowed.Contains(x.OperatorTypeEnum) && x.OperatorID == 0)
                                                                    .ToArray();
            if (operatorDtosWithZeroOperatorID.Count != 0)
            {
                string concatinatedAllowedOperatorTypeEnums = string.Join(", ", _operatorTypeEnums_With_OperatorID_0_Allowed);
                string distinctConcatinatedActualOperatorDtoTypeNames = string.Join(", ", operatorDtosWithZeroOperatorID.Select(x => x.GetType().Name).Distinct());
                throw new Exception(
                    "Error pre-processing OperatorDto's. " +
                    $"There are {operatorDtosWithZeroOperatorID.Count} OperatorDto's with OperatorID = 0 with OperatorTypes that are not allowed. " +
                    $"Allowed {nameof(OperatorTypeEnum)}s: {concatinatedAllowedOperatorTypeEnums} " +
                    $"Dto types: {{{distinctConcatinatedActualOperatorDtoTypeNames}}}");
            }
        }
    }
}