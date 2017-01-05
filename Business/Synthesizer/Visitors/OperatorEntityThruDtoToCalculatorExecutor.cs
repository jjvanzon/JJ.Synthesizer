using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Dto;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorEntityThruDtoToCalculatorExecutor
    {
        private readonly int _targetSamplingRate;
        private readonly int _targetChannelCount;
        private readonly double _secondsBetweenApplyFilterVariables;
        private readonly CalculatorCache _calculatorCache;
        private readonly ICurveRepository _curveRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;

        public OperatorEntityThruDtoToCalculatorExecutor(
            int targetSamplingRate,
            int targetChannelCount,
            double secondsBetweenApplyFilterVariables,
            CalculatorCache calculatorCache,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository,
            ISampleRepository sampleRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            _targetChannelCount = targetChannelCount;
            _targetSamplingRate = targetSamplingRate;
            _targetChannelCount = targetChannelCount;
            _secondsBetweenApplyFilterVariables = secondsBetweenApplyFilterVariables;
            _calculatorCache = calculatorCache;
            _curveRepository = curveRepository;
            _patchRepository = patchRepository;
            _sampleRepository = sampleRepository;
            _speakerSetupRepository = speakerSetupRepository;
        }

        public ToCalculatorResult Execute(Outlet topLevelOutlet)
        {
            var visitor1 = new OperatorEntityToDtoVisitor(_curveRepository, _patchRepository, _sampleRepository, _speakerSetupRepository);
            OperatorDtoBase dto = visitor1.Execute(topLevelOutlet);
            
            dto = new OperatorDtoVisitor_PreProcessing(_targetChannelCount).Execute(dto);

            var visitor3 = new OperatorDtoToCalculatorVisitor_WithCacheOperators(
                _targetSamplingRate, 
                _targetChannelCount, 
                _secondsBetweenApplyFilterVariables, 
                _calculatorCache, 
                _curveRepository, 
                _sampleRepository);
            ToCalculatorResult result = visitor3.Execute(dto);

            return result;
        }
    }
}
