using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    /// <summary> OperatorDtoVisitor_AssignOperationIdentities must have run first. </summary>
    internal class OperatorDtoVisitor_Deduplication : OperatorDtoVisitorBase
    {
        private Dictionary<string, IOperatorDto> _dictionary;

        public IOperatorDto Execute(IOperatorDto dto)
        {
            _dictionary = new Dictionary<string, IOperatorDto>();

            dto = Visit_OperatorDto_Polymorphic(dto);

            return dto;
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            string operationIdentity = dto.OperationIdentity;
            if (_dictionary.TryGetValue(operationIdentity, out IOperatorDto existingDto))
            {
                return existingDto;
            }
            else
            {
                IOperatorDto dto2 = base.Visit_OperatorDto_Polymorphic(dto);
                _dictionary.Add(operationIdentity, dto2);
                return dto2;
            }
        }
    }
}
