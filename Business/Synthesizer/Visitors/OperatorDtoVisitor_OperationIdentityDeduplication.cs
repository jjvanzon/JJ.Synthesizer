using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    /// <summary> OperatorDtoVisitor_AssignOperationIdentities must have run first. </summary>
    [Obsolete("Not obsolete, but not finished. Cannot be used in its current form. See OperatorDtoVisitor_AssignOperationIdentities")]
    internal class OperatorDtoVisitor_OperationIdentityDeduplication : OperatorDtoVisitorBase_AfterProgrammerLaziness
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
            if (_dictionary.TryGetValue(dto.OperationIdentity, out IOperatorDto existingDto))
            {
                return existingDto;
            }
            else
            {
                IOperatorDto dto2 = base.Visit_OperatorDto_Polymorphic(dto);
                _dictionary.Add(dto2.OperationIdentity, dto2);
                return dto2;
            }
        }
    }
}
