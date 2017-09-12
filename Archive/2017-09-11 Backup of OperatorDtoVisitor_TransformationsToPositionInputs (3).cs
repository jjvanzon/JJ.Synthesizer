//using System;
//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Collections;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_TransformationsToPositionInputs : OperatorDtoVisitorBase_AfterProgrammerLaziness
//    {
//        private Dictionary<(DimensionEnum, string), Stack<IOperatorDto_WithPositionOutput>> _dimension_To_OperatorDto_WithPositionOutput_Stack_Dictionary;

//        public IOperatorDto Execute(IOperatorDto dto)
//        {
//            if (dto == null) throw new ArgumentNullException(nameof(dto));

//            _dimension_To_OperatorDto_WithPositionOutput_Stack_Dictionary = new Dictionary<(DimensionEnum, string), Stack<IOperatorDto_WithPositionOutput>>();

//            dto = Visit_OperatorDto_Polymorphic(dto);

//            return dto;
//        }

//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            IOperatorDto func()
//            {
//                var positionReaderDto = dto as IOperatorDto_PositionReader;
//                if (positionReaderDto != null)
//                {
//                    var transformationStack = TryGet_OperatorDto_WithPositionOutput_Stack(positionReaderDto);
//                    positionReaderDto.Position = CreatePositionInput(transformationStack.PeekOrDefault(), positionReaderDto.StandardDimensionEnum, positionReaderDto.CanonicalCustomDimensionName);
//                }

//                var channelReaderDto = dto as IOperatorDto_WithAdditionalChannelDimension;
//                if (channelReaderDto != null)
//                {
//                    var channelTransformationStack = Get_OperatorDto_WithPositionOutput_Stack(DimensionEnum.Channel, "");
//                    channelReaderDto.Channel = CreatePositionInput(channelTransformationStack.PeekOrDefault(), DimensionEnum.Channel, "");
//                }

//                var dtoWithPositionOutput = dto as IOperatorDto_WithPositionOutput;

//                if (dtoWithPositionOutput != null)
//                {
//                    // Push Position Writer
//                    Stack<IOperatorDto_WithPositionOutput> transformationStack = TryGet_OperatorDto_WithPositionOutput_Stack(dtoWithPositionOutput);
//                    transformationStack.Push(dtoWithPositionOutput);
//                }

//                // Visit Inputs
//                var inputDtoList = new List<InputDto>();
//                foreach (InputDto inputDto in dto.Inputs)
//                {
//                    bool mustProcess = inputDto != positionReaderDto?.Position &&
//                                       inputDto !=  channelReaderDto?.Channel;
//                    if (!mustProcess)
//                    {
//                        continue;
//                    }

//                    InputDto inputDto2 = VisitInputDto(inputDto);
//                    inputDtoList.Add(inputDto2);
//                }
//                dto.Inputs = inputDtoList;


//                if (dtoWithPositionOutput != null)
//                {
//                    // Replace position writer with its signal.
//                    if (dtoWithPositionOutput.Signal.IsVar)
//                    {
//                        dto = dtoWithPositionOutput.Signal.Var;
//                    }
//                    else
//                    {
//                        dto = new Number_OperatorDto(dtoWithPositionOutput.Signal);
//                    }

//                    // Annul position writer signal, so it does not accidentally get used anymore.
//                    // -1 is less confusing than 0 as a quasi-null.
//                    // NaN is no option, because NaN inputs => NaN output.

//                    if (!(dto is Cache_OperatorDto)) // Cache needs the signal, even though it also needs everything to be a position reader.
//                    {
//                        dtoWithPositionOutput.Signal = new Number_OperatorDto(-1);
//                    }

//                    // Pop Position Writer
//                    Stack<IOperatorDto_WithPositionOutput> transformationStack = TryGet_OperatorDto_WithPositionOutput_Stack(dtoWithPositionOutput);
//                    transformationStack.Pop();
//                }

//                return dto;
//            }

//            return WithAlreadyProcessedCheck(dto, func);
//        }

//        private static InputDto CreatePositionInput(IOperatorDto_WithDimension inputOperatorDto, DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
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

//        private Stack<IOperatorDto_WithPositionOutput> TryGet_OperatorDto_WithPositionOutput_Stack(IOperatorDto_WithDimension positionReaderDto)
//        {
//            if (positionReaderDto == null)
//            {
//                return null;
//            }

//            return Get_OperatorDto_WithPositionOutput_Stack(positionReaderDto);
//        }

//        private Stack<IOperatorDto_WithPositionOutput> Get_OperatorDto_WithPositionOutput_Stack(IOperatorDto_WithDimension castedDto)
//        {
//            if (castedDto == null) throw new ArgumentNullException(nameof(castedDto));

//            return Get_OperatorDto_WithPositionOutput_Stack(castedDto.StandardDimensionEnum, castedDto.CanonicalCustomDimensionName);
//        }

//        private Stack<IOperatorDto_WithPositionOutput> Get_OperatorDto_WithPositionOutput_Stack(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
//        {
//            var key = (standardDimensionEnum, canonicalCustomDimensionName);

//            if (!_dimension_To_OperatorDto_WithPositionOutput_Stack_Dictionary.TryGetValue(key, out Stack<IOperatorDto_WithPositionOutput> stack))
//            {
//                stack = new Stack<IOperatorDto_WithPositionOutput>();
//                _dimension_To_OperatorDto_WithPositionOutput_Stack_Dictionary[key] = stack;
//            }

//            return stack;
//        }
//    }
//}
