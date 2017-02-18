//using System;
//using JetBrains.Annotations;
//using JJ.Business.Synthesizer.Calculation;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_ArrayDtos : OperatorDtoVisitorBase_AfterProgrammerLaziness
//    {
//        private readonly CalculatorCache _calculatorCache;
//        private readonly ISampleRepository _sampleRepository;
//        private readonly ICurveRepository _curveRepository;

//        public OperatorDtoVisitor_ArrayDtos(
//            [NotNull] CalculatorCache calculatorCache,
//            [NotNull] ISampleRepository sampleRepository,
//            [NotNull] ICurveRepository curveRepository)
//        {
//            if (calculatorCache == null) throw new ArgumentNullException(nameof(calculatorCache));
//            if (sampleRepository == null) throw new ArgumentNullException(nameof(sampleRepository));
//            if (curveRepository == null) throw new ArgumentNullException(nameof(curveRepository));

//            _calculatorCache = calculatorCache;
//            _sampleRepository = sampleRepository;
//            _curveRepository = curveRepository;
//        }

//        public void Execute(IOperatorDto dto)
//        {
//            Visit_OperatorDto_Polymorphic(dto);
//        }

//        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_NoOriginShifting(Curve_OperatorDto_MinXZero_NoOriginShifting dto)
//        {
//            return Process_Curve_OperatorDtoBase_WithoutMinX(dto);
//        }

//        protected override IOperatorDto Visit_Curve_OperatorDto_MinXZero_WithOriginShifting(Curve_OperatorDto_MinXZero_WithOriginShifting dto)
//        {
//            return Process_Curve_OperatorDtoBase_WithoutMinX(dto);
//        }

//        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_NoOriginShifting(Curve_OperatorDto_MinX_NoOriginShifting dto)
//        {
//            return Process_Curve_OperatorDtoBase_WithoutMinX(dto);
//        }

//        protected override IOperatorDto Visit_Curve_OperatorDto_MinX_WithOriginShifting(Curve_OperatorDto_MinX_WithOriginShifting dto)
//        {
//            return Process_Curve_OperatorDtoBase_WithoutMinX(dto);
//        }

//        private IOperatorDto Process_Curve_OperatorDtoBase_WithoutMinX(Curve_OperatorDtoBase_WithoutMinX dto)
//        {
//            dto.ArrayDto = _calculatorCache.GetCurveArrayDto(dto.CurveID, _curveRepository);
//            return dto;
//        }

//        protected override IOperatorDto Visit_Noise_OperatorDto(Noise_OperatorDto dto)
//        {
//            dto.ArrayDto = _calculatorCache.GetNoiseArrayDto();
//            return dto;
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting(Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_NoOriginShifting(Sample_OperatorDto_ConstFrequency_NoOriginShifting dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting(Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_ConstFrequency_WithOriginShifting(Sample_OperatorDto_ConstFrequency_WithOriginShifting dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking(Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_NoPhaseTracking(Sample_OperatorDto_VarFrequency_NoPhaseTracking dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking(Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        protected override IOperatorDto Visit_Sample_OperatorDto_VarFrequency_WithPhaseTracking(Sample_OperatorDto_VarFrequency_WithPhaseTracking dto)
//        {
//            return Process_Sample_OperatorDto(dto);
//        }

//        private IOperatorDto Process_Sample_OperatorDto(ISample_OperatorDto_WithSampleID dto)
//        {
//            dto.ArrayDtos = _calculatorCache.GetSampleArrayDtos(dto.SampleID, _sampleRepository);
//            return dto;
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto_Block(Random_OperatorDto_Block dto)
//        {
//            dto.ArrayDto = _calculatorCache.GetRandomArrayDto_Block();
//            return dto;
//        }

//        protected override IOperatorDto Visit_Random_OperatorDto_Stripe_LagBehind(Random_OperatorDto_Stripe_LagBehind dto)
//        {
//            dto.ArrayDto = _calculatorCache.GetRandomArrayDto_Stripe();
//            return dto;
//        }
//    }
//}
