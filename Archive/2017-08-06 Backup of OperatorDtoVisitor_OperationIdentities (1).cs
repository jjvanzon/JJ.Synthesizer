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
//            string operationIdentityPart = FormatIdentityPart(dto);

//            _operationIdentityPartStack.Push(operationIdentityPart);

//            dto.OperationIdentity = FormatOperationIdentity(_operationIdentityPartStack.Reverse());

//            base.Visit_OperatorDto_Polymorphic(dto);

//            _operationIdentityPartStack.Pop();

//            return dto;
//        }

//        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto) => ProcessNumber(dto);
//        protected override IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto) => ProcessNumber(dto);
//        protected override IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto) => ProcessNumber(dto);
//        protected override IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto) => ProcessNumber(dto);

//        private IOperatorDto ProcessNumber(Number_OperatorDto dto)
//        {
//            string operationIdentityPart = FormatIdentityPart_ForNumber(dto);

//            _operationIdentityPartStack.Push(operationIdentityPart);

//            dto.OperationIdentity = FormatOperationIdentity(_operationIdentityPartStack.Reverse());

//            _operationIdentityPartStack.Pop();

//            return dto;
//        }

//        private static string FormatIdentityPart(IOperatorDto dto)
//        {
//            // TODO: Different types of operator might get additional key parts,
//            // like the number operator would require the actual number to be the key.

//            switch (dto.OperatorTypeEnum)
//            {
//                case OperatorTypeEnum.Number:
//                {
//                    var dto2 = (Number_OperatorDto)dto;
//                    return FormatIdentityPart_ForNumber(dto2);
//                }

//                case OperatorTypeEnum.DimensionToOutlets:
//                {
//                    // TODO: You need the specific OperatorDto type,
//                    // but should you not just use specialized visitation methods for that?
//                    var dto2 = (DimensionToOutlets_Outlet_OperatorDto)dto;
//                    return FormatIdentityPart_ForDimensionToOutlets(dto2);
//                }

//                default:
//                    return $"{dto.OperatorTypeEnum}{dto.OperatorID}";
//            }
//        }

//        private static string FormatIdentityPart_ForNumber(Number_OperatorDto dto)
//        {
//            return $"{dto.OperatorTypeEnum}{VALUE_SEPARATOR}{dto.Number}";
//        }

//        private static string FormatIdentityPart_ForDimensionToOutlets(DimensionToOutlets_Outlet_OperatorDto dto)
//        {
//            return $"{dto.OperatorTypeEnum}{VALUE_SEPARATOR}{dto.OutletPosition}";
//        }

//        private string FormatOperationIdentity(IEnumerable<string> operationIdentityParts)
//        {
//            return string.Join(PATH_SEPARATOR, operationIdentityParts);
//        }
//    }
//}
