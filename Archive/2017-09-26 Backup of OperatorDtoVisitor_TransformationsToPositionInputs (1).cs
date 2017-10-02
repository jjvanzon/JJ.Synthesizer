//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Collections;
//using System.Linq;
//// ReSharper disable SuggestVarOrType_Elsewhere

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_TransformationsToPositionInputs : OperatorDtoVisitorBase_AfterProgrammerLaziness
//    {
//        private Dictionary<(DimensionEnum, string), Queue<IOperatorDto_PositionTransformation>> _transformationQueueDictionary;

//        public IOperatorDto Execute(IOperatorDto dto)
//        {
//            if (dto == null) throw new ArgumentNullException(nameof(dto));

//            _transformationQueueDictionary = new Dictionary<(DimensionEnum, string), Queue<IOperatorDto_PositionTransformation>>();

//            dto = Visit_OperatorDto_Polymorphic(dto);

//            ReplaceEmptyPositionsWithVariableInputs(dto);

//            return dto;
//        }

//        /// <summary>
//        /// The way transformations are chained together in the visitation,
//        /// the deepest transformations do not get a Position assigned,
//        /// so must be assigned a variable input
//        /// </summary>
//        private static void ReplaceEmptyPositionsWithVariableInputs(IOperatorDto dto)
//        {
//            IList<IOperatorDto_PositionTransformation> transformationsWithEmptyPosition =
//                dto.SelectRecursive(x => x.Inputs.Select(y => y.VarOrConst))
//                   .OfType<IOperatorDto_PositionTransformation>()
//                   .Where(x => x.Position.IsConstSpecialValue)
//                   .ToArray();

//            foreach (IOperatorDto_PositionTransformation transformationWithEmptyPosition in transformationsWithEmptyPosition)
//            {
//                transformationWithEmptyPosition.Position = CreatePositionInput(
//                    null,
//                    transformationWithEmptyPosition.StandardDimensionEnum,
//                    transformationWithEmptyPosition.CanonicalCustomDimensionName);
//            }
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

//            var transformationQueue = GetTransformationQueue(dto);

//            // Chain Transformations Together
//            IOperatorDto_PositionTransformation previousTransformation = transformationQueue.PeekOrDefault();
//            if (previousTransformation != null)
//            {
//                previousTransformation.Position = CreatePositionInput(dto, dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName);
//            }

//            // Position transformation only applies to signal, not factor or difference, for instance.
//            // So visit especially for that.

//            // Queue Position Transformation
//            transformationQueue.Add(dto);

//            // Visit Signal
//            dto.Signal = VisitInputDto(dto.Signal);

//            // Dequeue Position Transformation
//            transformationQueue.Remove();

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
//                var transformationQueue = GetTransformationQueue(positionReaderDto);
//                positionReaderDto.Position = CreatePositionInput(transformationQueue.PeekOrDefault(), positionReaderDto.StandardDimensionEnum, positionReaderDto.CanonicalCustomDimensionName);
//            }

//            var channelReaderDto = dto as IOperatorDto_WithAdditionalChannelDimension;
//            if (channelReaderDto != null)
//            {
//                var transformationQueue = GetTransformationQueue(DimensionEnum.Channel, "");
//                channelReaderDto.Channel = CreatePositionInput(transformationQueue.PeekOrDefault(), DimensionEnum.Channel, "");
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

//        /// <param name="inputOperatorDto">nullable</param>
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

//        private Queue<IOperatorDto_PositionTransformation> GetTransformationQueue(IOperatorDto_WithDimension dto)
//        {
//            if (dto == null) throw new ArgumentNullException(nameof(dto));

//            return GetTransformationQueue(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName);
//        }

//        private Queue<IOperatorDto_PositionTransformation> GetTransformationQueue(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
//        {
//            var key = (standardDimensionEnum, canonicalCustomDimensionName);

//            if (!_transformationQueueDictionary.TryGetValue(key, out Queue<IOperatorDto_PositionTransformation> queue))
//            {
//                queue = new Queue<IOperatorDto_PositionTransformation>();
//                _transformationQueueDictionary[key] = queue;
//            }

//            return queue;
//        }
//    }
//}
