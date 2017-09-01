//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Collections;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    /// <summary>
//    /// TODO: Make the base class more specific (one with suffix _After*).
//    /// </summary>
//    internal class OperatorDtoVisitor_TransformationsToPositionInputs : OperatorDtoVisitorBase
//    {
//        private Dictionary<(DimensionEnum, string), Stack<IOperatorDto_PositionWriter>> _dimension_ToPositionWriterStack_Dictionary;

//        public void Execute(IOperatorDto dto)
//        {
//            if (dto == null) throw new ArgumentNullException(nameof(dto));

//            _dimension_ToPositionWriterStack_Dictionary = new Dictionary<(DimensionEnum, string), Stack<IOperatorDto_PositionWriter>>();

//            Visit_OperatorDto_Polymorphic(dto);
//        }

//        // TODO: Make a separate method for readers and writers?
//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            var positionReaderDto = dto as IOperatorDto_PositionReader;
//            var positionWriterDto = dto as IOperatorDto_PositionWriter;
//            Stack<IOperatorDto_PositionWriter> transformationStack = GetTransformationStack(positionReaderDto);

//            // Visit Non-Signal Inputs
//            foreach (IOperatorDto inputOperatorDto in dto.Inputs
//                                                         .Except(positionWriterDto?.Signal)
//                                                         .Where(x => x.IsVar)
//                                                         .Select(x => x.Var))
//            {
//                Visit_OperatorDto_Polymorphic(inputOperatorDto);
//            }

//            // Push Position Writer
//            if (positionWriterDto != null)
//            {
//                transformationStack.Push(positionWriterDto);
//            }

//            // Visit Signal Input
//            if (positionWriterDto != null)
//            {
//                if (positionWriterDto.SignalInput.IsVar)
//                {
//                    base.Visit_OperatorDto_Polymorphic(positionWriterDto.SignalInput.Var);
//                }
//            }

//            // Set Position Reader's Position Input
//            if (positionReaderDto != null)
//            {
//                IOperatorDto_PositionWriter inputPositionWriterDto = transformationStack.Peek();
//                positionReaderDto.PositionInput = InputDtoFactory.CreateInputDto(inputPositionWriterDto);
//            }

//            // Pop Position Writer
//            if (positionWriterDto != null)
//            {
//                transformationStack.Pop();
//            }

//            return dto;
//        }

//        private Stack<IOperatorDto_PositionWriter> GetTransformationStack(IOperatorDto_WithDimension castedDto)
//        {
//            return GetTransformationStack(castedDto.StandardDimensionEnum, castedDto.CanonicalCustomDimensionName);
//        }

//        private Stack<IOperatorDto_PositionWriter> GetTransformationStack(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
//        {
//            var key = (standardDimensionEnum, canonicalCustomDimensionName);

//            if (!_dimension_ToPositionWriterStack_Dictionary.TryGetValue(key, out Stack<IOperatorDto_PositionWriter> stack))
//            {
//                stack = new Stack<IOperatorDto_PositionWriter>();
//                _dimension_ToPositionWriterStack_Dictionary[key] = stack;
//            }

//            return stack;
//        }
//    }
//}
