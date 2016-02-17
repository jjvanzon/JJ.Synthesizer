using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    /// <summary> Caches several calculators for shared use between PatchCalculators, to save memory. </summary>
    public class CalculatorCache
    {
        /// <summary>
        /// This dictionary is about reusing the same CurveCalculator in multiple OperatorCalculator_Curve's
        /// in case they uses the same Curve, more than optimizing things by using a dictionary.
        /// </summary>
        private readonly Dictionary<Curve, ICurveCalculator> _curve_CurveCalculator_Dictionary = new Dictionary<Curve, ICurveCalculator>();
        private readonly object _curveLock = new object();

        /// <summary>
        /// This dictionary is about reusing the same SampleCalculator in multiple OperatorCalculator_Sample's
        /// in case they uses the same Sample, more than optimizing things by using a dictionary.
        /// </summary>
        private readonly Dictionary<Sample, ISampleCalculator> _sample_SampleCalculator_Dictionary = new Dictionary<Sample, ISampleCalculator>();
        private readonly object _sampleLock = new object();

        /// <summary> Caches several calculators for shared use between PatchCalculators, to save memory. </summary>
        public CalculatorCache()
        {
            int assumedSamplingRate = 44100;
            NoiseCalculator = new NoiseCalculator(assumedSamplingRate);
        }

        internal NoiseCalculator NoiseCalculator { get; private set; }

        internal ICurveCalculator GetCurveCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            lock (_curveLock)
            {
                ICurveCalculator curveCalculator;
                if (!_curve_CurveCalculator_Dictionary.TryGetValue(curve, out curveCalculator))
                {
                    curveCalculator =  CurveCalculatorFactory.CreateCurveCalculator(curve);
                    _curve_CurveCalculator_Dictionary.Add(curve, curveCalculator);
                }

                return curveCalculator;
            }
        }

        internal ISampleCalculator GetSampleCalculator(SampleInfo sampleInfo)
        {
            if (sampleInfo == null) throw new NullException(() => sampleInfo);
            if (sampleInfo.Sample == null) throw new NullException(() => sampleInfo.Sample);

            lock (_sampleLock)
            {
                ISampleCalculator sampleCalculator;
                if (!_sample_SampleCalculator_Dictionary.TryGetValue(sampleInfo.Sample, out sampleCalculator))
                {
                    sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sampleInfo.Sample, sampleInfo.Bytes);
                    _sample_SampleCalculator_Dictionary.Add(sampleInfo.Sample, sampleCalculator);
                }

                return sampleCalculator;
            }
        }
    }
}