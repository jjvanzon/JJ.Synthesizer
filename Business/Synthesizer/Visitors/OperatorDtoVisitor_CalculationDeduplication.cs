using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Collections;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Visitors
{
    /// <summary> Under construction. </summary>
    internal class OperatorDtoVisitor_CalculationDeduplication : OperatorDtoVisitorBase
    {
        private const char OPERATION_IDENTITY_PART_SEPARATOR = '|';

        public IOperatorDto Execute(IOperatorDto dto)
        {
            _operationIdentityPartStack = new Stack<string>();

            dto = Visit_OperatorDto_Polymorphic(dto);

            Dictionary<string, IList<IOperatorDto>> nonUniqueDictionary =
                dto.UnionRecursive(x => x.InputOperatorDtos)
                   .GroupBy(x => x.OperationIdentity)
                   .ToNonUniqueDictionary();

            // TODO: Replace by single pieces of the graph.
            // Oops: This is not possible with a lookup.
            // You have to process it recursively, because ... why again?
            //
            // It makes sense to replace multiple copies with the same calculation,
            // but what if a piece of the calculation inside that also gets replaced.
            // I suspect this would be no problem, 
            // since if you replace all deeper calculation copies within higher calculation copies,
            // you still end up with a single calculation with all redundant parts replaced.
            //
            // TODO: I think I need the parent in the DTO structure,
            // because I need to tie the parents of the duplicate calculation branches 
            // to the a single calculation copy.
            //
            // I think I am going to need a depth-first recursive visitation,
            // since I need to prefer replacing smaller calculations first,
            // (synonym for deeper calculations).
            //
            // Then when I found a calculation in the dedupped set,
            // I use that instead of the current one.
            // I can return a different DTO.
            //
            // I think I should be far better off makeing a second visitor,
            // that does it.
            // Then it will be simple dictionary lookups.
            // Instead of this guessing and missing the targets.
            // 
            // 'This should be simpler.'

            throw new NotImplementedException();
            return dto;
        }

        private Stack<string> _operationIdentityPartStack;

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            string operationIdentityPart = FormatIdentityPart(dto);

            _operationIdentityPartStack.Push(operationIdentityPart);

            dto.OperationIdentity = FormatOperationIdentity(_operationIdentityPartStack.Reverse());

            return base.Visit_OperatorDto_Polymorphic(dto);
        }

        private static string FormatIdentityPart(IOperatorDto dto)
        {
            // TODO: Different types of operator might get additional key parts,
            // like the number operator would require the actual number to be the key.
            return $"{dto.OperatorTypeEnum}";
        }

        private string FormatOperationIdentity(IEnumerable<string> operationIdentityParts)
        {
            return StringHelper.Join(OPERATION_IDENTITY_PART_SEPARATOR, operationIdentityParts);
        }
    }
}
