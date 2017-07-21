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

            new OperatorDtoVisitor_InfrastructureVariables(_samplingRate, _targetChannelCount).Execute(dto);
            dto = new OperatorDtoVisitor_MathSimplification().Execute(dto);
            // Temporarily (2017-07-21) Outcommented, until C# code generator can handle everything around booleans.
            //dto = new OperatorDtoVisitor_BooleanBoundaries().Execute(dto);
            dto = new OperatorDtoVisitor_MachineOptimization().Execute(dto);
            dto = new OperatorDtoVisitor_Rewriting().Execute(dto);
            dto = new OperatorDtoVisitor_ProgrammerLaziness().Execute(dto);
            new OperatorDtoVisitor_DimensionStackLevels().Execute(dto);

            AssertZeroOperatorIDs(dto);

            return dto;
        }

        private static void AssertZeroOperatorIDs(IOperatorDto dto)
        {
            IList<IOperatorDto> operatorDtosWithZeroOperatorID = dto.UnionRecursive(x => x.InputOperatorDtos)
                                                                    .Where(x => x.OperatorTypeEnum != OperatorTypeEnum.Number &&
                                                                                x.OperatorTypeEnum != OperatorTypeEnum.DoubleToBoolean &&
                                                                                x.OperatorTypeEnum != OperatorTypeEnum.BooleanToDouble &&
                                                                                x.OperatorID == 0)
                                                                    .ToArray();
            if (operatorDtosWithZeroOperatorID.Count != 0)
            {
                string distinctConcatinatedOperatorDtoTypeNames = string.Join(", ", operatorDtosWithZeroOperatorID.Select(x => x.GetType().Name).Distinct());
                throw new Exception($"Error pre-processing OperatorDto's. There are {operatorDtosWithZeroOperatorID.Count} non-Number OperatorDto's with OperatorID = 0. Dto types: {{{distinctConcatinatedOperatorDtoTypeNames}}}");
            }
        }
    }
}
