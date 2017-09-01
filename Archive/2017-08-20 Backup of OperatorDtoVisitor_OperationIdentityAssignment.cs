//using System.Collections.Generic;
//using System.Text;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Framework.Collections;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_OperationIdentityAssignment : OperatorDtoVisitorBase_AfterTransformationsToPositionInputs
//    {
//        private Stack<string> _stack;

//        public void Execute(IOperatorDto dto)
//        {
//            _stack = new Stack<string>();

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

//            var dimensionWriter = dto as IOperatorDto_PositionWriter;

//            var arguments = new List<string>(dto.Inputs.Count);
//            foreach (InputDto inputDto in dto.Inputs.Except(dimensionWriter?.Signal))
//            {
//                if (inputDto.Var != null)
//                {
//                    Visit_OperatorDto_Polymorphic(inputDto.Var);
//                }
//                else
//                { 
//                    _stack.Push(FormatNumber(inputDto.Const));
//                }

//                string argument = _stack.Pop();
//                arguments.Add(argument);
//            }

//            // Some operators require more than just their inputs to identify their operation.
//            {
//                if (dto is IOperatorDto_WithDimension castedDto)
//                {
//                    arguments.Add($"{castedDto.CanonicalCustomDimensionName}");
//                    arguments.Add($"{castedDto.StandardDimensionEnum}");
//                }
//            }

//            switch (dto)
//            {
//                case Number_OperatorDto castedDto:
//                    arguments.Add($"{FormatNumber(castedDto.Number)}");
//                    break;

//                case VariableInput_OperatorDto castedDto:
//                {
//                    arguments.Add($"{castedDto.DimensionEnum}");
//                    arguments.Add($"{castedDto.CanonicalName}");
//                    arguments.Add($"{castedDto.Position}");
//                    break;
//                }

//                case Curve_OperatorDto castedDto:
//                {
//                    arguments.Add($"{castedDto.CurveID}");
//                    break;
//                }

//                case SampleWithRate1_OperatorDto castedDto:
//                {
//                    arguments.Add($"{castedDto.SampleID}");
//                    break;
//                }

//                // TODO: There are way more operator types that need this,
//                // basically anything that has something in their data property.
//            }

//            string concatinatedArguments = string.Join(",", arguments);
//            sb.Append(concatinatedArguments);

//            sb.Append(')');

//            dto.OperationIdentity = sb.ToString();

//            _stack.Push(dto.OperationIdentity);

//            return dto;
//        }

//        private static string FormatNumber(double number) => number.ToString();
//    }
//}