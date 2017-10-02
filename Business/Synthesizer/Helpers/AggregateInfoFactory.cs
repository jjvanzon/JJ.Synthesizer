using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class AggregateInfoFactory
    {
        public static AggregateInfo CreateAggregateInfo(IEnumerable<InputDto> inputDtos)
        {
            IList<InputDto> constInputDtos = inputDtos.Where(x => x.IsConst).ToArray();

            IList<InputDto> varInputDtos = inputDtos.Except(constInputDtos).ToArray();

            bool hasVars = varInputDtos.Any();
            bool hasConsts = constInputDtos.Any();

            var varsConsts_InputDto = new AggregateInfo
            {
                Vars = varInputDtos,
                Consts = constInputDtos,
                HasConsts = hasConsts,
                HasVars = hasVars,
                OnlyConsts = !hasVars,
                OnlyVars = !hasConsts,
                IsEmpty = !inputDtos.Any()
            };

            if (constInputDtos.Count == 1)
            {
                InputDto constInputDto = constInputDtos.Single();
                varsConsts_InputDto.Const = constInputDto;
                varsConsts_InputDto.ConstIsZero = constInputDto.IsConstZero;
                varsConsts_InputDto.ConstIsOne = constInputDto.IsConstOne;
            }

            return varsConsts_InputDto;
        }

    }
}
