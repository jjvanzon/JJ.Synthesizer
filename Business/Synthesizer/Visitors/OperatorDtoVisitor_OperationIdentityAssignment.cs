using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_OperationIdentityAssignment : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        private const char PATH_SEPARATOR = '\\';
        private const char VALUE_SEPARATOR = '|';

        private Stack<string> _operationIdentityPartStack;

        public void Execute(IOperatorDto dto)
        {
            _operationIdentityPartStack = new Stack<string>();

            Visit_OperatorDto_Polymorphic(dto);
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            string operationIdentityPart = FormatOperationIdentityPart_Polymorphic(dto);

            _operationIdentityPartStack.Push(operationIdentityPart);

            base.Visit_OperatorDto_Polymorphic(dto);

            dto.OperationIdentity = FormatOperationIdentityPath(_operationIdentityPartStack.Reverse());

            _operationIdentityPartStack.Pop();

            return dto;
        }

        private static string FormatOperationIdentityPart_Polymorphic(IOperatorDto dto)
        {
            if (dto is Number_OperatorDto dto2)
            {
                return FormatIdentityPart_ForNumber(dto2);
            }
            else if (dto is DimensionToOutlets_Outlet_OperatorDto dto3)
            {
                return FormatIdentityPart_ForDimensionToOutlets(dto3);
            }
            else
            {
                return FormatIdentityPart_ForOtherOperatorType(dto);
            }
        }

        private static string FormatIdentityPart_ForNumber(Number_OperatorDto dto)
        {
            return $"{dto.OperatorTypeEnum}{VALUE_SEPARATOR}{dto.Number}";
        }

        private static string FormatIdentityPart_ForDimensionToOutlets(DimensionToOutlets_Outlet_OperatorDto dto)
        {
            return $"{dto.OperatorTypeEnum}{VALUE_SEPARATOR}{dto.OutletPosition}";
        }

        /// <summary>
        /// For now, any OperationIdentity includes the OperatorID,
        /// which means an add operator never gets reused if the input is the same...
        /// But it is in the safe side, to avoid incorrect deduplications for now.
        /// </summary>
        private static string FormatIdentityPart_ForOtherOperatorType(IOperatorDto dto)
        {
            return $"{dto.OperatorTypeEnum}{VALUE_SEPARATOR}{dto.OperatorID}";
        }

        private string FormatOperationIdentityPath(IEnumerable<string> operationIdentityParts)
        {
            return StringHelper.Join(PATH_SEPARATOR, operationIdentityParts);
        }
    }
}
