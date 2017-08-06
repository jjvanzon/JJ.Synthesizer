using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_OperationIdentities : OperatorDtoVisitorBase
    {
        private const string PATH_SEPARATOR = "$$";
        private const char VALUE_SEPARATOR = '|';

        private Stack<string> _operationIdentityPartStack;

        public IOperatorDto Execute(IOperatorDto dto)
        {
            _operationIdentityPartStack = new Stack<string>();

            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            string operationIdentityPart = FormatIdentityPart(dto);

            _operationIdentityPartStack.Push(operationIdentityPart);

            dto.OperationIdentity = FormatOperationIdentity(_operationIdentityPartStack.Reverse());

            base.Visit_OperatorDto_Polymorphic(dto);

            _operationIdentityPartStack.Pop();

            return dto;
        }

        private static string FormatIdentityPart(IOperatorDto dto)
        {
            // TODO: Different types of operator might get additional key parts,
            // like the number operator would require the actual number to be the key.

            switch (dto.OperatorTypeEnum)
            {
                case OperatorTypeEnum.Number:
                {
                    // TODO: You need the specific OperatorDto type,
                    // but should you not just use specialized visitation methods for that?
                    var dto2 = (Number_OperatorDto)dto;
                    return $"{dto.OperatorTypeEnum}{VALUE_SEPARATOR}{dto2.Number}";
                }
                
                case OperatorTypeEnum.DimensionToOutlets:
                {
                    // TODO: You need the specific OperatorDto type,
                    // but should you not just use specialized visitation methods for that?
                    var dto2 = (DimensionToOutlets_Outlet_OperatorDto)dto;
                    return $"{dto.OperatorTypeEnum}{VALUE_SEPARATOR}{dto2.OutletPosition}";
                }

                default:
                    return $"{dto.OperatorTypeEnum}{dto.OperatorID}";
            }
        }

        private string FormatOperationIdentity(IEnumerable<string> operationIdentityParts)
        {
            return string.Join(PATH_SEPARATOR, operationIdentityParts);
        }
    }
}
