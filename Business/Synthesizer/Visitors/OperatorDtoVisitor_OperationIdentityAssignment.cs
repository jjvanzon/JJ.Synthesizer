using System.Collections.Generic;
using System.Text;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_OperationIdentityAssignment : OperatorDtoVisitorBase
    {
        private Stack<string> _stack;

        public void Execute(IOperatorDto dto)
        {
            _stack = new Stack<string>();

            Visit_OperatorDto_Polymorphic(dto);
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            return WithAlreadyProcessedCheck(dto, () =>
            {
                if (!string.IsNullOrEmpty(dto.OperationIdentity))
                {
                    _stack.Push(dto.OperationIdentity);
                    return dto;
                }

                var sb = new StringBuilder();

                sb.Append(dto.OperatorTypeEnum);

                sb.Append('(');

                var arguments = new List<string>(dto.Inputs.Count);

                // Some operators require more than just their inputs to identify their operation.
                switch (dto)
                {
                    case Number_OperatorDto castedDto:
                        arguments.Add($"{FormatNumber(castedDto.Number)}");
                        break;

                    case VariableInput_OperatorDto castedDto:
                    {
                        arguments.Add($"{nameof(castedDto.Position)}={castedDto.Position}");
                        break;
                    }

                    case Curve_OperatorDto castedDto:
                    {
                        arguments.Add($"{nameof(castedDto.CurveID)}={castedDto.CurveID}");
                        break;
                    }

                    case SampleWithRate1_OperatorDto castedDto:
                    {
                        arguments.Add($"{nameof(castedDto.SampleID)}={castedDto.SampleID}");
                        break;
                    }

                    // TODO: There might be more operator types that need this.
                }

                {
                    if (dto is IOperatorDto_WithDimension castedDto)
                    {
                        if (castedDto.StandardDimensionEnum != Enums.DimensionEnum.Undefined)
                        {
                            arguments.Add($"StandardDimension={castedDto.StandardDimensionEnum}");
                        }
                        if (!string.IsNullOrEmpty(castedDto.CanonicalCustomDimensionName))
                        {
                            arguments.Add($"CustomDimension={castedDto.CanonicalCustomDimensionName}");
                        }
                    }
                }


                foreach (InputDto inputDto in dto.Inputs)
                {
                    if (inputDto.Var != null)
                    {
                        Visit_OperatorDto_Polymorphic(inputDto.Var);
                    }
                    else
                    {
                        _stack.Push(FormatNumber(inputDto.Const));
                    }

                    string argument = _stack.Pop();
                    arguments.Add(argument);
                }

                string concatinatedArguments = string.Join(",", arguments);
                sb.Append(concatinatedArguments);

                sb.Append(')');

                dto.OperationIdentity = sb.ToString();

                _stack.Push(dto.OperationIdentity);

                return dto;
            });
        }

        private static string FormatNumber(double number) => number.ToString();
    }
}