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

        /// <summary>
        /// Even though the RedBlackTree does not store duplicates,
        /// which is something you would want, this may not significantly affect the outcome.
        /// </summary>
        private RedBlackTree<double, double> _redBlackTree;

        private double _maximum;
        private double _lastTime;

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
            timeSliceDuration = 0.02;
            _sampleCountDouble = 100.0;

            _sampleDuration = timeSliceDuration / _sampleCountDouble;

            ResetState();
        }

        public override double Calculate(double time, int channelIndex)
        {
            bool mustUpdateForward = _lastTime < time;
            if (mustUpdateForward)
            {
                // TODO: It assumes time starts at 0.
                do
                {
                    UpdateStatistics(_lastTime, channelIndex);

                    _lastTime += _sampleDuration;
                }
                while (_lastTime < time);

                _maximum = _redBlackTree.GetMaximum();

                // Assign exactly the time, to make 'backward or forward' detection easier next time around.
                // Small deviations in sampling duration are not imporant (small in relation to the sample duration itself).
                _lastTime = time;

                return _maximum;
            }

            // For time going in reverse
            bool mustUpdateBackward = _lastTime > time;
            if (mustUpdateBackward)
            {
                do
                {
                    UpdateStatistics(_lastTime, channelIndex);

                    _lastTime -= _sampleDuration;
                }
                while (_lastTime > time);

                _maximum = _redBlackTree.GetMaximum();

                // Assign exactly the time, to make 'backward or forward' detection easier next time  around.
                // Small deviations in sampling duration are not imporant (small in relation to the sample duration itself).
                _lastTime = time;

                return _maximum;
            }

            // Check difference with brute force:
            //double treeMax = _maximum;
            //_maximum = _queue.Max();
            //if (treeMax != _maximum)
            //{
            //    int i = 0;
            //}

            return _maximum;
        }

        private void UpdateStatistics(double time, int channelIndex)
        {
            double newValue = _signalCalculator.Calculate(time, channelIndex);

            double oldValue = _queue.Dequeue();
            _queue.Enqueue(newValue);

            _redBlackTree.Delete(oldValue);
            _redBlackTree.Insert(newValue, newValue);
        }

        public override void ResetState()
        {
            _redBlackTree = new RedBlackTree<double, double>();
            _queue = CreateQueue();
            _maximum = 0.0;
            _lastTime = 0.0;

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
