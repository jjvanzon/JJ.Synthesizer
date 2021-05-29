using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
// ReSharper disable PossibleMultipleEnumeration

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class AggregateInfoFactory
    {
        public static AggregateInfo CreateAggregateInfo(IEnumerable<InputDto> inputDtos)
        {
            if (inputDtos == null) throw new ArgumentNullException(nameof(inputDtos));
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

                // Do not fall into this trap:
                // Only assign ConstIsZero and ConstIsOne if there is a single const.
                // Do not assign ConstIsZero and ConstIsOne if all consts are zero or one.
                // It depends on the specific aggregation function (Sum, Product, Average, etc.)
                // how multiple constants coalesce to 1 or 0,
                // which is handled by MathSimplification.
                varsConsts_InputDto.ConstIsZero = constInputDto.IsConstZero;
                varsConsts_InputDto.ConstIsOne = constInputDto.IsConstOne;
            }

            return varsConsts_InputDto;
        }

    }
}
