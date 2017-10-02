//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Collections;
//// ReSharper disable SuggestVarOrType_Elsewhere

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_TransformationsToPositionInputs : OperatorDtoVisitorBase_AfterProgrammerLaziness
//    {
//        private Dictionary<(DimensionEnum, string), Stack<IOperatorDto_PositionTransformation>> _transformationStackDictionary;

//        public IOperatorDto Execute(IOperatorDto dto)
//        {
//            if (dto == null) throw new ArgumentNullException(nameof(dto));

//            _transformationStackDictionary = new Dictionary<(DimensionEnum, string), Stack<IOperatorDto_PositionTransformation>>();

//            dto = Visit_OperatorDto_Polymorphic(dto);

//            return dto;
//        }

//        [DebuggerHidden]
//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            return WithAlreadyProcessedCheck(dto, () => Process_OperatorDto_Polymorphic(dto));
//        }

//        [DebuggerHidden]
//        private IOperatorDto Process_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            if (dto is IOperatorDto_PositionTransformation castedDto)
//            {
//                return Process_OperatorDto_PositionTransformation(castedDto);
//            }
//            else
//            {
//                return Process_OperatorDto_Other(dto);
//            }
//        }

//        private IOperatorDto Process_OperatorDto_PositionTransformation(IOperatorDto_PositionTransformation dto)
//        {
//            // Visit normally
//            var inputDtoList = new List<InputDto>();
//            foreach (InputDto inputDto in dto.Inputs)
//            {
//                bool mustSkip = inputDto == dto.Signal; // Signal will have special visitation
//                if (mustSkip)
//                {
//                    inputDtoList.Add(inputDto);
//                    continue;
//                }

//                InputDto inputDto2 = VisitInputDto(inputDto);
//                inputDtoList.Add(inputDto2);
//            }
//            dto.Inputs = inputDtoList;

//            // Position transformation only applies to signal, not factor or difference, for instance.
//            // So visit especially for that.
//            var transformationStack = GetTransformationStack(dto);

//            // Push Position Transformation
//            transformationStack.Push(dto);

//            // Visit Signal
//            dto.Signal = VisitInputDto(dto.Signal);

//            // Pop Position Transformation
//            transformationStack.Pop();

//            // Replace position transformation with its signal.
//            IOperatorDto dto2 = dto.Signal.VarOrConst;

//            // Annul position transformation signal.
//            dto.Signal = new Number_OperatorDto(double.NaN);

//            return dto2;
//        }

//        private IOperatorDto Process_OperatorDto_Other(IOperatorDto dto)
//        {
//            var positionReaderDto = dto as IOperatorDto_PositionReader;
//            if (positionReaderDto != null)
//            {
//                var transformationStack = GetTransformationStack(positionReaderDto);
//                positionReaderDto.Position = CreatePositionInput(transformationStack.PeekOrDefault(), positionReaderDto.StandardDimensionEnum, positionReaderDto.CanonicalCustomDimensionName);
//            }

//            var channelReaderDto = dto as IOperatorDto_WithAdditionalChannelDimension;
//            if (channelReaderDto != null)
//            {
//                var transformationStack = GetTransformationStack(DimensionEnum.Channel, "");
//                channelReaderDto.Channel = CreatePositionInput(transformationStack.PeekOrDefault(), DimensionEnum.Channel, "");
//            }

//            var inputDtoList = new List<InputDto>();
//            foreach (InputDto inputDto in dto.Inputs)
//            {
//                bool mustSkip = inputDto == positionReaderDto?.Position || // Position is just assigned in this visitor.
//                                inputDto == channelReaderDto?.Channel; // Channel is just assigned in this visitor.
//                if (mustSkip)
//                {
//                    inputDtoList.Add(inputDto);
//                    continue;
//                }

//                InputDto inputDto2 = VisitInputDto(inputDto);
//                inputDtoList.Add(inputDto2);
//            }
//            dto.Inputs = inputDtoList;

//            return dto;
//        }

//        private static InputDto CreatePositionInput(IOperatorDto inputOperatorDto, DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
//        {
//            if (inputOperatorDto != null)
//            {
//                return InputDtoFactory.CreateInputDto(inputOperatorDto);
//            }
//            else
//            {
//                // PositionReaders that have Position inputs that are not position transformations,
//                // take position inputs from the outside, so they become variable inputs.
//                return new VariableInput_OperatorDto
//                {
//                    CanonicalCustomDimensionName = canonicalCustomDimensionName,
//                    StandardDimensionEnum = standardDimensionEnum
//                };
//            }
//        }

//        private Stack<IOperatorDto_PositionTransformation> GetTransformationStack(IOperatorDto_WithDimension dto)
//        {
//            if (dto == null) throw new ArgumentNullException(nameof(dto));

//            return GetTransformationStack(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName);
//        }

//        private Stack<IOperatorDto_PositionTransformation> GetTransformationStack(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
//        {
//            var key = (standardDimensionEnum, canonicalCustomDimensionName);

//            if (!_transformationStackDictionary.TryGetValue(key, out Stack<IOperatorDto_PositionTransformation> stack))
//            {
//                stack = new Stack<IOperatorDto_PositionTransformation>();
//                _transformationStackDictionary[key] = stack;
//            }

//            return stack;
//        }
//    }
//}
