//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Collections;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_TransformationsToPositionInputs : OperatorDtoVisitorBase_AfterProgrammerLaziness
//    {
//        private Dictionary<(DimensionEnum, string), Stack<IOperatorDto_PositionWriter>> _dimension_ToPositionWriterStack_Dictionary;

//        public void Execute(IOperatorDto dto)
//        {
//            if (dto == null) throw new ArgumentNullException(nameof(dto));

//            _dimension_ToPositionWriterStack_Dictionary = new Dictionary<(DimensionEnum, string), Stack<IOperatorDto_PositionWriter>>();

//            Visit_OperatorDto_Polymorphic(dto);
//        }

//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            var positionReaderDto = dto as IOperatorDto_PositionReader;
//            var positionWriterDto = dto as IOperatorDto_PositionWriter;
//            Stack<IOperatorDto_PositionWriter> transformationStack = TryGetPositionWriterStack(positionReaderDto);

//            // Visit Non-Signal Inputs
//            foreach (IOperatorDto inputOperatorDto in dto.Inputs
//                                                         .Except(positionWriterDto?.Signal)
//                                                         .Where(x => x.IsVar)
//                                                         .Select(x => x.Var))
//            {
//                Visit_OperatorDto_Polymorphic(inputOperatorDto);
//            }

//            if (positionReaderDto != null)
//            {
//                // Set Position Reader's Position Input
//                IOperatorDto_PositionWriter inputPositionWriterDto = transformationStack.PeekOrDefault();

//                if (inputPositionWriterDto != null)
//                {
//                    positionReaderDto.Position = InputDtoFactory.CreateInputDto(inputPositionWriterDto);
//                }
//                else
//                {
//                    var getDimension_OperatorDto = new GetDimension_OperatorDto();
//                    DtoCloner.CloneProperties(positionReaderDto, getDimension_OperatorDto);
//                    positionReaderDto.Position = getDimension_OperatorDto;
//                }
//            }

//            if (positionWriterDto != null)
//            {
//                // Push Position Writer
//                transformationStack.Push(positionWriterDto);

//                // Visit Signal Input
//                if (positionWriterDto.Signal.IsVar)
//                {
//                    Visit_OperatorDto_Polymorphic(positionWriterDto.Signal.Var);
//                }

//                // Pop Position Writer
//                transformationStack.Pop();
//            }

//            return dto;
//        }

//        private Stack<IOperatorDto_PositionWriter> TryGetPositionWriterStack(IOperatorDto_WithDimension positionReaderDto)
//        {
//            if (positionReaderDto == null)
//            {
//                return null;
//            }

//            return GetPositionWriterStack(positionReaderDto);
//        }

//        private Stack<IOperatorDto_PositionWriter> GetPositionWriterStack(IOperatorDto_WithDimension castedDto)
//        {
//            if (castedDto == null) throw new ArgumentNullException(nameof(castedDto));

//            return GetPositionWriterStack(castedDto.StandardDimensionEnum, castedDto.CanonicalCustomDimensionName);
//        }

//        private Stack<IOperatorDto_PositionWriter> GetPositionWriterStack(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
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
