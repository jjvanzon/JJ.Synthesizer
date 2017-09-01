//using System.Linq;
//using JJ.Business.Synthesizer.Dto;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    /// <summary> Excludes visiting Signal inlets of PositionWriters to prevent infinite circular processing. </summary>
//    internal abstract class OperatorDtoVisitorBase_AfterTransformationsToPositionInputs : OperatorDtoVisitorBase_AfterProgrammerLaziness
//    {
//        protected override IOperatorDto Visit_OperatorDto_Base(IOperatorDto dto)
//        {
//            var castedDto = dto as IOperatorDto_PositionWriter;

//            // TODO: Remove this class completely?
//            dto.Inputs = dto.Inputs
//                            //.Where(x => x != castedDto.Signal)
//                            .Select(x => VisitInputDto(x))
//                            .ToArray();

//            return dto;
//        }
//    }
//}