using System;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;
using System.Linq;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_TransformationsToPositionInputs : OperatorDtoVisitorBase
    {
        private Dictionary<(DimensionEnum, string), Queue<IOperatorDto>> _transformationQueueDictionary;

        public IOperatorDto Execute(IOperatorDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            _transformationQueueDictionary = new Dictionary<(DimensionEnum, string), Queue<IOperatorDto>>();

            dto = Visit_OperatorDto_Polymorphic(dto);

            ReplaceEmptyPositionsWithVariableInputs(dto);

            return dto;
        }

        /// <summary>
        /// The way transformations are chained together in the visitation,
        /// the deepest transformations do not get a Position assigned,
        /// so must be assigned a variable input
        /// </summary>
        private static void ReplaceEmptyPositionsWithVariableInputs(IOperatorDto dto)
        {
            IList<IOperatorDto_PositionTransformation> transformationsWithEmptyPosition =
                dto.SelectRecursive(x => x.Inputs.Select(y => y.VarOrConst))
                   .OfType<IOperatorDto_PositionTransformation>()
                   .Where(x => x.Position.IsConstSpecialValue)
                   .ToArray();

            foreach (IOperatorDto_PositionTransformation transformationWithEmptyPosition in transformationsWithEmptyPosition)
            {
                transformationWithEmptyPosition.Position = CreatePositionInput(
                    null,
                    transformationWithEmptyPosition.StandardDimensionEnum,
                    transformationWithEmptyPosition.CanonicalCustomDimensionName);
            }
        }

        [DebuggerHidden]
        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            return WithAlreadyProcessedCheck(dto, () => Process_OperatorDto_Polymorphic(dto));
        }

        private IOperatorDto Process_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            switch (dto)
            {
                case SetDimension_OperatorDto dto2:
                    return Visit_SetDimension_OperatorDto(dto2);

                case Squash_OperatorDto dto2:
                    return Visit_Squash_OperatorDto(dto2);

                case GetDimension_OperatorDto dto2:
                    return Visit_GetDimension_OperatorDto(dto2);

                case IOperatorDto_PositionTransformation dto2:
                    return Process_OperatorDto_PositionTransformation(dto2);

                default:
                    return Process_OperatorDto_PotentialReaders(dto);
            }
        }

        protected override IOperatorDto Visit_SetDimension_OperatorDto(SetDimension_OperatorDto dto)
        {
            Queue<IOperatorDto> transformationQueue = GetTransformationQueue(dto);

            transformationQueue.Add(dto.Position.VarOrConst);

            dto.Signal = VisitInputDto(dto.Signal);
            dto.Position = VisitInputDto(dto.Position);

            transformationQueue.Remove();

            dto.Signal = new Number_OperatorDto(double.NaN);

            return dto.Position.VarOrConst;
        }

        protected override IOperatorDto Visit_Squash_OperatorDto(Squash_OperatorDto dto)
        {
            Queue<IOperatorDto> transformationQueue = GetTransformationQueue(dto);

            transformationQueue.Add(dto);

            dto.Signal = VisitInputDto(dto.Signal);
            dto.Position = VisitInputDto(dto.Position);
            dto.Factor = VisitInputDto(dto.Factor);

            dto.Origin = VisitInputDto(dto.Origin);

            transformationQueue.Remove();

            dto.Signal = new Number_OperatorDto(double.NaN);

            return dto;
        }

        protected override IOperatorDto Visit_GetDimension_OperatorDto(GetDimension_OperatorDto dto)
        {
            Queue<IOperatorDto> transformationQueue = GetTransformationQueue(dto);

            dto.Position = CreatePositionInput(transformationQueue.PeekOrDefault(), dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName);

            return dto;
        }

        private IOperatorDto Process_OperatorDto_PositionTransformation(IOperatorDto_PositionTransformation dto)
        {
            // Visit normally
            var inputDtoList = new List<InputDto>();
            foreach (InputDto inputDto in dto.Inputs)
            {
                bool mustSkip = inputDto == dto.Signal; // Signal will have special visitation
                if (mustSkip)
                {
                    inputDtoList.Add(inputDto);
                    continue;
                }

                InputDto inputDto2 = VisitInputDto(inputDto);
                inputDtoList.Add(inputDto2);
            }
            dto.Inputs = inputDtoList;

            var transformationQueue = GetTransformationQueue(dto);

            // Chain Transformations Together
            if (transformationQueue.PeekOrDefault() is IOperatorDto_PositionTransformation previousTransformation)
            {
                previousTransformation.Position = CreatePositionInput(dto, dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName);
            }

            // Position transformation only applies to signal, not factor or difference, for instance.
            // So visit especially for that.

            // Queue Position Transformation
            transformationQueue.Add(dto);

            // Visit Signal
            dto.Signal = VisitInputDto(dto.Signal);

            // Dequeue Position Transformation
            transformationQueue.Remove();

            // Replace position transformation with its signal.
            IOperatorDto dto2 = dto.Signal.VarOrConst;

            // Annul position transformation signal.
            dto.Signal = new Number_OperatorDto(double.NaN);

            return dto2;
        }

        private IOperatorDto Process_OperatorDto_PotentialReaders(IOperatorDto dto)
        {
            var positionReaderDto = dto as IOperatorDto_PositionReader;
            if (positionReaderDto != null)
            {
                var transformationQueue = GetTransformationQueue(positionReaderDto);
                positionReaderDto.Position = CreatePositionInput(transformationQueue.PeekOrDefault(), positionReaderDto.StandardDimensionEnum, positionReaderDto.CanonicalCustomDimensionName);
            }   

            var channelReaderDto = dto as IOperatorDto_WithAdditionalChannelDimension;
            if (channelReaderDto != null)
            {
                var transformationQueue = GetTransformationQueue(DimensionEnum.Channel, "");
                channelReaderDto.Channel = CreatePositionInput(transformationQueue.PeekOrDefault(), DimensionEnum.Channel, "");
            }

            var inputDtoList = new List<InputDto>();
            foreach (InputDto inputDto in dto.Inputs)
            {
                bool mustSkip = inputDto == positionReaderDto?.Position || // Position is just assigned in this visitor.
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

            return dto;
        }

        /// <param name="inputOperatorDto">nullable</param>
        private static InputDto CreatePositionInput(IOperatorDto inputOperatorDto, DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
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

        private Queue<IOperatorDto> GetTransformationQueue(IOperatorDto_WithDimension dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return GetTransformationQueue(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName);
        }

        private Queue<IOperatorDto> GetTransformationQueue(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
        {
            var key = (standardDimensionEnum, canonicalCustomDimensionName);

            if (!_transformationQueueDictionary.TryGetValue(key, out Queue<IOperatorDto> queue))
            {
                queue = new Queue<IOperatorDto>();
                _transformationQueueDictionary[key] = queue;
            }

            return queue;
        }
    }
}
