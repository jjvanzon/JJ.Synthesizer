using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Visitors
{
    [Obsolete("Not obsolete, but not finished. Cannot be used in its current form.")]
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
            if (!string.IsNullOrEmpty(dto.OperationIdentity))
            {
                return dto;
            }

            var sb = new StringBuilder();

            sb.Append(dto.OperatorTypeEnum);

            sb.Append('(');

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
            }

            IList<string> arguments = dto.Inputs
                                         .Select(x => _stack.Pop())
                                         .Reverse()
                                         .ToList();

            // Some operators require more than just their inputs to identify their operation.
            {
                if (dto is IOperatorDto_WithDimension castedDto)
                {
                    arguments.Add($"{castedDto.CanonicalCustomDimensionName}");
                    arguments.Add($"{castedDto.StandardDimensionEnum}");
                }
            }

            switch (dto.OperatorTypeEnum)
            {
                case OperatorTypeEnum.Number:
                {
                    var castedDto = (Number_OperatorDto)dto;
                    arguments.Add($"{FormatNumber(castedDto.Number)}");
                    break;
                }

                case OperatorTypeEnum.PatchInlet:
                {
                    var castedDto = (VariableInput_OperatorDto)dto;
                    arguments.Add($"{castedDto.DimensionEnum}");
                    arguments.Add($"{castedDto.CanonicalName}");
                    arguments.Add($"{castedDto.Position}");
                    break;
                }

                case OperatorTypeEnum.Curve:
                {
                    var castedDto = (Curve_OperatorDtoBase_WithoutMinX)dto;
                    arguments.Add($"{castedDto.CurveID}");
                    break;
                }

                case OperatorTypeEnum.Sample:
                {
                    var castedDto = (Sample_OperatorDtoBase_WithSampleID)dto;
                    arguments.Add($"{castedDto.SampleID}");
                    break;
                }

                // TODO: There are way more operator types that need this, basically anything that has
                // something in their data property.
            }

            // TODO: 
            // If a dimension is applicable to an operation,
            // all dimension transformations done in front of the operation,
            // must become part of the OperationIdentity.
            // But this is so difficult.
            // It would have been easy if the dimension tranformation were
            // converted to just regular input earlier on in the DTO processing.
            // Which as of now (2017-08-07) is not the case yet.

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