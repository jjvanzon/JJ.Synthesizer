using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Maximum_OperatorCalculator_RedBlackTree : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _sampleDuration;
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _sampleCountDouble;

        private Queue<double> _queue;
        private RedBlackTree<double, double> _redBlackTree;

        private double _maximum;
        private double _previousTime;
        private double _passedSampleTime;

        /// <param name="sampleDuration">
        /// sampleDuration might be internally corrected to exactly fit n times into timeSliceDuration.
        /// </param>
        public Maximum_OperatorCalculator_RedBlackTree(
            OperatorCalculatorBase signalCalculator,
            double timeSliceDuration,
            int sampleCount)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (timeSliceDuration <= 0.0) throw new LessThanException(() => timeSliceDuration, 0.0);
            if (sampleCount <= 0) throw new LessThanOrEqualException(() => sampleCount, 0);

            _signalCalculator = signalCalculator;
            _sampleCountDouble = sampleCount;

            // HACK
            _sampleCountDouble = 2205.0;

            _sampleDuration = timeSliceDuration / _sampleCountDouble;

            ResetState();
        }

        public override double Calculate(double time, int channelIndex)
        {
            // Update _passedSampleTime
            //double dt = time - _previousTime;
            //if (dt >= 0)
            //{
            //    _passedSampleTime += dt;
            //}
            //else
            //{
            //    // Substitute for Math.Abs().
            //    // This makes it work for time that goes in reverse and even time that quickly goes back and forth.
            //    _passedSampleTime -= dt;
            //}

            //if (_passedSampleTime >= _sampleDuration)
            //{
                double oldValue = _queue.Dequeue();
                double newValue = _signalCalculator.Calculate(time, channelIndex);
                _queue.Enqueue(newValue);

                _redBlackTree.Delete(oldValue);
                _redBlackTree.Insert(newValue, newValue);

                _maximum = _redBlackTree.GetMaximum();
                double treesMax = _maximum;
                _maximum = _queue.Max();

                //if (treesMax != _maximum)
                //{
                //    int i = 0;
                //}

                //_passedSampleTime = 0.0;
            //}

            //_previousTime = time;

            return _maximum;
        }

        public override void ResetState()
        {
            _redBlackTree = new RedBlackTree<double, double>();
            _queue = CreateQueue();
            _maximum = 0.0;
            _previousTime = 0.0;
            _passedSampleTime = 0.0;

            base.ResetState();
        }

        private Queue<double> CreateQueue()
        {
            int sampleCountInt = (int)(_sampleCountDouble);

            Queue<double> queue = new Queue<double>(sampleCountInt);

            for (int i = 0; i < sampleCountInt; i++)
            {
                queue.Enqueue(0.0);
            }

            return queue;
        }
    }
}
