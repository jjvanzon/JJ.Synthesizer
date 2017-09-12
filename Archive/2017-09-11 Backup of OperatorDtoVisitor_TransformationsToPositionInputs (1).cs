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
//        private Dictionary<(DimensionEnum, string), Queue<IOperatorDto_WithPositionOutput>> _dimension_ToPositionWriterQueue_Dictionary;

//        public IOperatorDto Execute(IOperatorDto dto)
//        {
//            if (dto == null) throw new ArgumentNullException(nameof(dto));

//            _dimension_ToPositionWriterQueue_Dictionary = new Dictionary<(DimensionEnum, string), Queue<IOperatorDto_WithPositionOutput>>();

//            dto = Visit_OperatorDto_Polymorphic(dto);

//            return dto;
//        }

//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            return WithAlreadyProcessedCheck(dto, () =>
//            {
//                var positionReaderDto = dto as IOperatorDto_PositionReader;
//                var positionWriterDto = dto as IOperatorDto_WithPositionOutput;
//                var dtoWithAdditionalChannelDimension = dto as IOperatorDto_WithAdditionalChannelDimension;

//                Queue<IOperatorDto_WithPositionOutput> transformationQueue = TryGetPositionWriterQueue(positionReaderDto);
//                Queue<IOperatorDto_WithPositionOutput> channelTransformationQueue = null;
//                if (dtoWithAdditionalChannelDimension != null)
//                {
//                    channelTransformationQueue = GetPositionWriterQueue(DimensionEnum.Channel, "");
//                }

//                // Visit Non-Signal Inputs
//                // (and leave Signal Input intact.)
//                var inputDtoList = new List<InputDto>();
//                foreach (InputDto inputDto in dto.Inputs)
//                {
//                    if (inputDto == positionWriterDto?.Signal)
//                    {
//                        inputDtoList.Add(inputDto);
//                    }
//                    else
//                    {
//                        InputDto inputDto2 = VisitInputDto(inputDto);
//                        inputDtoList.Add(inputDto2);
//                    }
//                }
//                dto.Inputs = inputDtoList;

//                if (positionReaderDto != null)
//                {
//                    // Set Position Reader's Position Input
//                    IOperatorDto_WithPositionOutput inputPositionWriterDto = transformationQueue.PeekOrDefault();

//                    if (inputPositionWriterDto != null)
//                    {
//                        positionReaderDto.Position = InputDtoFactory.CreateInputDto(inputPositionWriterDto);
//                    }
//                    else
//                    {
//                        // PositionReaders that have Position inputs that are not position transformations,
//                        // take position inputs from the outside, so they become variable inputs.
//                        var variableInput_OperatorDto = new VariableInput_OperatorDto
//                        {
//                            CanonicalCustomDimensionName = positionReaderDto.CanonicalCustomDimensionName,
//                            StandardDimensionEnum = positionReaderDto.StandardDimensionEnum
//                        };
//                        positionReaderDto.Position = variableInput_OperatorDto;
//                    }
//                }

//                if (dtoWithAdditionalChannelDimension != null)
//                {
//                    // Set Position Reader's Position Input
//                    IOperatorDto_WithPositionOutput inputPositionWriterDto = channelTransformationQueue.PeekOrDefault();

//                    if (inputPositionWriterDto != null)
//                    {
//                        dtoWithAdditionalChannelDimension.Channel = InputDtoFactory.CreateInputDto(inputPositionWriterDto);
//                    }
//                    else
//                    {
//                        // PositionReaders that have Position inputs that are not position transformations,
//                        // take position inputs from the outside, so they become variable inputs.
//                        var variableInput_OperatorDto = new VariableInput_OperatorDto
//                        {
//                            StandardDimensionEnum = DimensionEnum.Channel
//                        };
//                        dtoWithAdditionalChannelDimension.Channel = variableInput_OperatorDto;
//                    }
//                }

//                if (positionWriterDto != null)
//                {
//                    // Push Position Writer
//                    transformationQueue.Enqueue(positionWriterDto);

//                    // Visit Signal Input
//                    if (positionWriterDto.Signal.IsVar)
//                    {
//                        // TODO: Use VisitInputDto instead?
//                        IOperatorDto signal2 = Visit_OperatorDto_Polymorphic(positionWriterDto.Signal.Var);
//                        positionWriterDto.Signal = InputDtoFactory.CreateInputDto(signal2);
//                    }

//                    // Replace position writer with its signal.
//                    if (positionWriterDto.Signal.IsVar)
//                    {
//                        dto = positionWriterDto.Signal.Var;
//                    }
//                    else
//                    {
//                        dto = new Number_OperatorDto(positionWriterDto.Signal);
//                    }

//                    // Annul position writer signal, so it does not accidentally get used anymore.
//                    // -1 is less confusing than 0 as a quasi-null.
//                    // NaN is no option, because NaN inputs => NaN output.

//                    if (!(dto is Cache_OperatorDto)
//                    ) // Cache needs the signal, even though it also needs everything to be a position reader.
//                    {
//                        positionWriterDto.Signal = new Number_OperatorDto(-1);
//                    }

//                    // Pop Position Writer
//                    transformationQueue.Dequeue();
//                }

//                return dto;
//            });
//        }

//        private Queue<IOperatorDto_WithPositionOutput> TryGetPositionWriterQueue(IOperatorDto_WithDimension positionReaderDto)
//        {
//            if (positionReaderDto == null)
//            {
//                return null;
//            }

//            return GetPositionWriterQueue(positionReaderDto);
//        }

//        private Queue<IOperatorDto_WithPositionOutput> GetPositionWriterQueue(IOperatorDto_WithDimension castedDto)
//        {
//            if (castedDto == null) throw new ArgumentNullException(nameof(castedDto));

//            return GetPositionWriterQueue(castedDto.StandardDimensionEnum, castedDto.CanonicalCustomDimensionName);
//        }

//        private Queue<IOperatorDto_WithPositionOutput> GetPositionWriterQueue(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
//        {
//            var key = (standardDimensionEnum, canonicalCustomDimensionName);

//            if (!_dimension_ToPositionWriterQueue_Dictionary.TryGetValue(key, out Queue<IOperatorDto_WithPositionOutput> stack))
//            {
//                stack = new Queue<IOperatorDto_WithPositionOutput>();
//                _dimension_ToPositionWriterQueue_Dictionary[key] = stack;
//            }

//            return stack;
//        }
//    }
//}
