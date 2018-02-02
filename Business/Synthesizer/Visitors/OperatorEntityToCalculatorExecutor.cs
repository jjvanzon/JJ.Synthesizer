using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Visitors
{
	internal class OperatorEntityToCalculatorExecutor
	{
		private readonly int _targetSamplingRate;
		private readonly int _targetChannelCount;
		private readonly CalculatorCache _calculatorCache;
		private readonly ICurveRepository _curveRepository;
		private readonly ISampleRepository _sampleRepository;
		private readonly ISpeakerSetupRepository _speakerSetupRepository;

		public OperatorEntityToCalculatorExecutor(
			int targetSamplingRate,
			int targetChannelCount,
			CalculatorCache calculatorCache,
			ICurveRepository curveRepository,
			ISampleRepository sampleRepository,
			ISpeakerSetupRepository speakerSetupRepository)
		{
			_targetChannelCount = targetChannelCount;
			_targetSamplingRate = targetSamplingRate;
			_targetChannelCount = targetChannelCount;
			_calculatorCache = calculatorCache;
			_curveRepository = curveRepository;
			_sampleRepository = sampleRepository;
			_speakerSetupRepository = speakerSetupRepository;
		}

		public ToCalculatorResult Execute(Outlet topLevelOutlet)
		{
			var visitor1 = new OperatorEntityToDtoVisitor(_calculatorCache, _curveRepository, _sampleRepository, _speakerSetupRepository);
			IOperatorDto dto = visitor1.Execute(topLevelOutlet);
			
			dto = new OperatorDtoPreProcessingExecutor(_targetSamplingRate, _targetChannelCount).Execute(dto);

			var visitor3 = new OperatorDtoToCalculatorVisitor_WithCacheOperators(
				_calculatorCache, 
				_curveRepository, 
				_sampleRepository);

			ToCalculatorResult result = visitor3.Execute(dto);

			return result;
		}
	}
}
