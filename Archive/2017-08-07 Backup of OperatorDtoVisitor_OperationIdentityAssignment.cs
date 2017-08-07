//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    /// <summary> Under construction. </summary>
//    internal class OperatorDtoVisitor_OperationIdentityAssignment : OperatorDtoVisitorBase_AfterProgrammerLaziness
//    {
//        private Stack<string> _stack;
//        private Dictionary<(DimensionEnum, string), Stack<string>> _dimension_ToOperationIdentityStack_Dictionary;

//        public void Execute(IOperatorDto dto)
//        {
//            _stack = new Stack<string>();
//            _dimension_ToOperationIdentityStack_Dictionary = new Dictionary<(DimensionEnum, string), Stack<string>>();

//            Visit_OperatorDto_Polymorphic(dto);
//        }

//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            if (!string.IsNullOrEmpty(dto.OperationIdentity))
//            {
//                return dto;
//            }

//            var sb = new StringBuilder();

//            sb.Append(dto.OperatorTypeEnum);

//            sb.Append('(');

//            foreach (InputDto inputDto in dto.InputDtos)
//            {
//                if (inputDto.Var != null)
//                {
//                    Visit_OperatorDto_Polymorphic(inputDto.Var);
//                }
//                else if (inputDto.Const.HasValue)
//                {
//                    _stack.Push(FormatNumber(inputDto.Const.Value));
//                }
//                else
//                {
//                    throw new Exception(
//                        $"{nameof(inputDto)}.{nameof(inputDto.Var)} and " +
//                        $"{nameof(inputDto)}.{nameof(inputDto.Const)} cannot both be null.");
//                }
//            }

//            IList<string> arguments = dto.InputDtos
//                                         .Select(x => _stack.Pop())
//                                         .Reverse()
//                                         .ToList();

//            // Some operators require more than just their inputs to identify them.
//            if (dto is Number_OperatorDto number_OperatorDto)
//            {
//                arguments.Add($"{FormatNumber(number_OperatorDto.Number)}");
//            }
//            else if (dto is DimensionToOutlets_Outlet_OperatorDto dimensionToOutlets_Outlet_OperatorDto)
//            {
//                // TODO: This will become deprecated once all dimension transformations are handled the same way.
//                arguments.Add($"[{dimensionToOutlets_Outlet_OperatorDto.OutletPosition}]");
//            }

//            // TODO: Variable input and dimensions also need special handling.

//            if (dto is IOperatorDto_WithDimension operatorDto_WithDimension)
//            {

//                bool isDimensionWriter = VisitorHelper.IsDimensionWriter(dto);
//                bool isDimensionReader = !isDimensionWriter;

//                Stack<string> dimensionOperationIdentityStack = GetDimensionOperationIdentityStack(operatorDto_WithDimension);

//                if (isDimensionWriter)
//                {
//                    // TODO: Part of the dimension operation identity should be
//                    // the OperatorTypeEnum and any opertions behind the non-signal inlets.
//                    // That is a lot of information. I feel I do not have the reusable helper methods
//                    // to produce such a thing? Could I revisit and not do anything if OperationIdentity already filled in?
//                    // That way I avoid redundant processing.
//                }
//                else if (isDimensionReader)
//                {
//                    string dimensionTransformationArgument = string.Join("=>", dimensionOperationIdentityStack.Reverse());
//                    arguments.Add(dimensionTransformationArgument);
//                }

//                // TODO: Consider IOperatorDto_WithAdditionalChannelDimension.
//            }

//            string concatinatedArguments = string.Join(",", arguments);
//            sb.Append(concatinatedArguments);
            
//            sb.Append(')');

//            dto.OperationIdentity = sb.ToString();

//            _stack.Push(dto.OperationIdentity);

//            return dto;
//        }

//        private Stack<string> GetDimensionOperationIdentityStack(IOperatorDto_WithDimension operatorDto_WithDimension)
//        {
//            var key = (operatorDto_WithDimension.StandardDimensionEnum, operatorDto_WithDimension.CanonicalCustomDimensionName);

//            if (!_dimension_ToOperationIdentityStack_Dictionary.TryGetValue(key, out Stack<string> stack))
//            { 
//                stack = new Stack<string>();
//                _dimension_ToOperationIdentityStack_Dictionary.Add(key, stack);
//            }

//            return stack;
//        }

//        private static string FormatNumber(double number) => number.ToString();
//    }
//}
