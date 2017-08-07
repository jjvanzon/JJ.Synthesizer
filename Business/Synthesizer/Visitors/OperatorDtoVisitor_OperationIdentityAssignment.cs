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
            base.Visit_OperatorDto_Polymorphic(dto);

            // TODO: Loop here yourself, pushing numbers,
            // and visiting input DTO's,
            // so you can pop them from the stack whether they were doubles in the DTO
            // or input OperatorDto's.

            var sb = new StringBuilder();

            sb.Append(dto.OperatorTypeEnum);

            sb.Append('(');

            IList<string> arguments = dto.InputOperatorDtos
                                         .Select(x => _stack.Pop())
                                         .Reverse()
                                         .ToArray();

            int argumentCount = arguments.Count;
            for (int i = 0; i < argumentCount; i++)
            {
                string argument = arguments[i];

                sb.Append(argument);

                bool isLast = i == argumentCount - 1;
                if (!isLast)
                {
                    sb.Append(',');
                }
            }

            sb.Append(')');

            dto.OperationIdentity = sb.ToString();

            _stack.Push(dto.OperationIdentity);

            return dto;
        }

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
            => ProcessNumber(dto);

        protected override IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
            => ProcessNumber(dto);

        protected override IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
            => ProcessNumber(dto);

        protected override IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
            => ProcessNumber(dto);

        private IOperatorDto ProcessNumber(Number_OperatorDto dto)
        {
            _stack.Push(dto.Number.ToString());
            return dto;
        }

        protected override IOperatorDto Visit_DimensionToOutlets_Outlet_OperatorDto(DimensionToOutlets_Outlet_OperatorDto dto)
        {
            _stack.Push($"{dto.OperatorTypeEnum}-{dto.OutletPosition}");
            return dto;
        }
    }
}
