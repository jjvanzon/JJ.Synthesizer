using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    /// <summary> OperatorDtoVisitor_AssignOperationIdentities must have run first. </summary>
    internal class OperatorDtoVisitor_OperationIdentityDeduplication : OperatorDtoVisitorBase
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
            string key = dto.OperationIdentity ?? "";

            if (_dictionary.TryGetValue(key, out IOperatorDto existingDto))
            {
                return existingDto;
            }
            else
            {
                IOperatorDto dto2 = base.Visit_OperatorDto_Polymorphic(dto);

                // TODO: It is weird that I need to do a dictionary check here again. Not sure why.
                // for some reason in deeper recursion a DTO could already be added.
                if (_dictionary.TryGetValue(key, out existingDto))
                {
                    return existingDto;
                }

                _dictionary.Add(dto2.OperationIdentity, dto2);

                return dto2;
            }
        }
    }
}
