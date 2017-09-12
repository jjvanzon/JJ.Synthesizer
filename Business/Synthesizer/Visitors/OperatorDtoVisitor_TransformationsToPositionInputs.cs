using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_TransformationsToPositionInputs : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        private Dictionary<(DimensionEnum, string), Stack<IOperatorDto_PositionTransformation>> _transformationStackDictionary;

        public IOperatorDto Execute(IOperatorDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            _transformationStackDictionary = new Dictionary<(DimensionEnum, string), Stack<IOperatorDto_PositionTransformation>>();

            dto = Visit_OperatorDto_Polymorphic(dto);

            return dto;
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            IOperatorDto func()
            {
                var positionReaderDto = dto as IOperatorDto_PositionReader;
                if (positionReaderDto != null)
                {
                    var transformationStack = TryGetTransformationStack(positionReaderDto);
                    positionReaderDto.Position = CreatePositionInput(transformationStack.PeekOrDefault(), positionReaderDto.StandardDimensionEnum, positionReaderDto.CanonicalCustomDimensionName);
                }

                var channelReaderDto = dto as IOperatorDto_WithAdditionalChannelDimension;
                if (channelReaderDto != null)
                {
                    var channelTransformationStack = GetTransformationStack(DimensionEnum.Channel, "");
                    channelReaderDto.Channel = CreatePositionInput(channelTransformationStack.PeekOrDefault(), DimensionEnum.Channel, "");
                }

                var transformationDto = dto as IOperatorDto_PositionTransformation;

                // Visit normally
                var inputDtoList = new List<InputDto>();
                foreach (InputDto inputDto in dto.Inputs)
                {
                    bool mustSkip = inputDto == transformationDto?.Signal || // Signal will have special visitation
                                    inputDto == positionReaderDto?.Position || // Position is just assigned in this visitor.
                                    inputDto == channelReaderDto?.Channel; // Channel is just assigned in this visitor.
                    if (mustSkip)
                    {
                        inputDtoList.Add(inputDto);
                        continue;
                    }

                    InputDto inputDto2 = VisitInputDto(inputDto);
                    inputDtoList.Add(inputDto2);
                }
                dto.Inputs = inputDtoList;

                // Position transformation only applies to signal, not factor or difference, for instance.
                // So visit especially for that.
                if (transformationDto != null)
                {
                    // Push Position Transformation
                    Stack<IOperatorDto_PositionTransformation> transformationStack = TryGetTransformationStack(transformationDto);
                    transformationStack.Push(transformationDto);

                    // Visit Signal
                    transformationDto.Signal = VisitInputDto(transformationDto.Signal);

                    // Replace position transformation with its signal.
                    dto = transformationDto.Signal.VarOrConst;

                    // Annul position transformation signal.
                    // (So it does not accidentally get used anymore.)
                    // (-1 is less confusing than 0 as a quasi-null.)
                    // (NaN is no option, because NaN inputs => NaN output.)
                    // TODO: Consider inserting NaN anyway and omitting the NaN math optimization,
                    // since is kind of pointless to performance-optimize a bad calculation.
                    if (!(dto is Cache_OperatorDto)) // Cache needs the signal, even though it also needs everything to be a position reader.
                    {
                        transformationDto.Signal = new Number_OperatorDto(-1);
                    }
                }

                if (transformationDto != null)
                {
                    // Pop Position Transformation
                    Stack<IOperatorDto_PositionTransformation> transformationStack = TryGetTransformationStack(transformationDto);
                    transformationStack.Pop();
                }

                return dto;
            }

            return WithAlreadyProcessedCheck(dto, func);
        }

        private static InputDto CreatePositionInput(IOperatorDto_WithDimension inputOperatorDto, DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
        {
            if (inputOperatorDto != null)
            {
                return InputDtoFactory.CreateInputDto(inputOperatorDto);
            }
            else
            {
                // PositionReaders that have Position inputs that are not position transformations,
                // take position inputs from the outside, so they become variable inputs.
                return new VariableInput_OperatorDto
                {
                    CanonicalCustomDimensionName = canonicalCustomDimensionName,
                    StandardDimensionEnum = standardDimensionEnum
                };
            }
        }

        private Stack<IOperatorDto_PositionTransformation> TryGetTransformationStack(IOperatorDto_WithDimension dto)
        {
            if (dto == null)
            {
                return null;
            }

            return GetTransformationStack(dto);
        }

        private Stack<IOperatorDto_PositionTransformation> GetTransformationStack(IOperatorDto_WithDimension dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return GetTransformationStack(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName);
        }

        private Stack<IOperatorDto_PositionTransformation> GetTransformationStack(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
        {
            var key = (standardDimensionEnum, canonicalCustomDimensionName);

            if (!_transformationStackDictionary.TryGetValue(key, out Stack<IOperatorDto_PositionTransformation> stack))
            {
                stack = new Stack<IOperatorDto_PositionTransformation>();
                _transformationStackDictionary[key] = stack;
            }

            return stack;
        }
    }
}
