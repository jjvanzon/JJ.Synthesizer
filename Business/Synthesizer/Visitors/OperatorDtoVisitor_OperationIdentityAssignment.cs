using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_OperationIdentityAssignment : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        private Stack<string> _stack;

        public void Execute(IOperatorDto dto)
        {
            _stack = new Stack<string>();

            Visit_OperatorDto_Polymorphic(dto);
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            var sb = new StringBuilder();

            sb.Append(dto.OperatorTypeEnum);

            sb.Append('(');

            foreach (InputDto inputDto in dto.InputDtos)
            {
                if (inputDto.Var != null)
                {
                    Visit_OperatorDto_Polymorphic(inputDto.Var);
                }
                else
                {
                    _stack.Push(FormatNumber(inputDto.Const.Value));
                }
            }

            IList<string> arguments = dto.InputDtos
                                         .Select(x => _stack.Pop())
                                         .Reverse()
                                         .ToList();

            // Some operators require more than just their inputs to identify them.
            if (dto is Number_OperatorDto number_OperatorDto)
            {
                arguments.Add($"{FormatNumber(number_OperatorDto.Number)}");
            }
            else if (dto is DimensionToOutlets_Outlet_OperatorDto dimensionToOutlets_Outlet_OperatorDto)
            {
                arguments.Add($"[{dimensionToOutlets_Outlet_OperatorDto.OutletPosition}]");
            }

            // TODO: Variable input and dimensions also need special handling.

            string concatinatedArguments = string.Join(",", arguments);
            sb.Append(concatinatedArguments);
            
            sb.Append(')');

            dto.OperationIdentity = sb.ToString();

            _stack.Push(dto.OperationIdentity);

            return dto;
        }

        private static string FormatNumber(double number) => number.ToString();
    }
}
