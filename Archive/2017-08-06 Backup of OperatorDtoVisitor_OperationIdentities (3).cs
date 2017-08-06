//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_OperationIdentities : OperatorDtoVisitorBase_AfterProgrammerLaziness
//    {
//        private const string PATH_SEPARATOR = "$$";
//        private const char VALUE_SEPARATOR = '|';

//        private Stack<string> _operationIdentityPartStack;

//        public void Execute(IOperatorDto dto)
//        {
//            _operationIdentityPartStack = new Stack<string>();

//            Visit_OperatorDto_Polymorphic(dto);
//        }

//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            if (dto is Number_OperatorDto number_OperatorDto)
//            {
//                return Visit_Number_OperatorDto(number_OperatorDto);
//            }
//            else if (dto is DimensionToOutlets_Outlet_OperatorDto dimensionToOutlets_Outlet_OperatorDto)
//            {
//                return Visit_DimensionToOutlets_Outlet_OperatorDto(dimensionToOutlets_Outlet_OperatorDto);
//            }
//            else
//            {
//                return TemplateMethod(dto, () => FormatIdentityPart_ForOtherOperatorType(dto));
//            }
//        }

//        protected override IOperatorDto Visit_OperatorDto_Base(IOperatorDto dto)
//        {
//            base.Visit_OperatorDto_Base(dto);

//            dto.OperationIdentity = FormatOperationIdentity(_operationIdentityPartStack.Reverse());

//            return dto;
//        }

//        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto) 
//            => TemplateMethod(dto, () => FormatIdentityPart_ForNumber(dto));

//        protected override IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto) 
//            => TemplateMethod(dto, () => FormatIdentityPart_ForNumber(dto));

//        protected override IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto) 
//            => TemplateMethod(dto, () => FormatIdentityPart_ForNumber(dto));

//        protected override IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto) 
//            => TemplateMethod(dto, () => FormatIdentityPart_ForNumber(dto));

//        private IOperatorDto TemplateMethod(IOperatorDto dto, Func<string> formatOperationIdentityPartDelegate)
//        {
//            string operationIdentityPart = formatOperationIdentityPartDelegate();

//            _operationIdentityPartStack.Push(operationIdentityPart);

//            dto.OperationIdentity = FormatOperationIdentity(_operationIdentityPartStack.Reverse());

//            _operationIdentityPartStack.Pop();

//            return dto;
//        }

//        protected override IOperatorDto Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto) 
//            => TemplateMethod(dto, () => FormatIdentityPart_ForDimensionToOutlets(dto));

//        private static string FormatIdentityPart_ForNumber(Number_OperatorDto dto) 
//            => $"{dto.OperatorTypeEnum}{VALUE_SEPARATOR}{dto.Number}";

//        private static string FormatIdentityPart_ForDimensionToOutlets(DimensionToOutlets_Outlet_OperatorDto dto) 
//            => $"{dto.OperatorTypeEnum}{VALUE_SEPARATOR}{dto.OutletPosition}";

//        private static string FormatIdentityPart_ForOtherOperatorType(IOperatorDto dto) 
//            => $"{dto.OperatorTypeEnum}{dto.OperatorID}";

//        private string FormatOperationIdentity(IEnumerable<string> operationIdentityParts) 
//            => string.Join(PATH_SEPARATOR, operationIdentityParts);
//    }
//}
