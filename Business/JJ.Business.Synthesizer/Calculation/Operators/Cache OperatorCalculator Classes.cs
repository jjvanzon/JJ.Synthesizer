using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // MinTimeZero

    internal class Cache_OperatorCalculator_MultiChannel_MinTimeZero_Block : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Block[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTimeZero_Block(IList<ArrayCalculator_MinTimeZero_Block> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel_MinTimeZero_Stripe : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Stripe[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTimeZero_Stripe(IList<ArrayCalculator_MinTimeZero_Stripe> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel_MinTimeZero_Line : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Line[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTimeZero_Line(IList<ArrayCalculator_MinTimeZero_Line> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel_MinTimeZero_Cubic : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Cubic[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTimeZero_Cubic(IList<ArrayCalculator_MinTimeZero_Cubic> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel_MinTimeZero_Hermite : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Hermite[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTimeZero_Hermite(IList<ArrayCalculator_MinTimeZero_Hermite> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTimeZero_Block : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Block _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTimeZero_Block(ArrayCalculator_MinTimeZero_Block arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTimeZero_Stripe : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Stripe _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTimeZero_Stripe(ArrayCalculator_MinTimeZero_Stripe arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTimeZero_Line : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Line _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTimeZero_Line(ArrayCalculator_MinTimeZero_Line arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTimeZero_Cubic : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Cubic _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTimeZero_Cubic(ArrayCalculator_MinTimeZero_Cubic arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTimeZero_Hermite : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTimeZero_Hermite _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTimeZero_Hermite(ArrayCalculator_MinTimeZero_Hermite arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    // MinTime

    internal class Cache_OperatorCalculator_MultiChannel_MinTime_Block : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Block[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTime_Block(IList<ArrayCalculator_MinTime_Block> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel_MinTime_Stripe : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Stripe[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTime_Stripe(IList<ArrayCalculator_MinTime_Stripe> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel_MinTime_Line : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Line[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTime_Line(IList<ArrayCalculator_MinTime_Line> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel_MinTime_Cubic : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Cubic[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTime_Cubic(IList<ArrayCalculator_MinTime_Cubic> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_MultiChannel_MinTime_Hermite : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Hermite[] _arrayCalculators;

        public Cache_OperatorCalculator_MultiChannel_MinTime_Hermite(IList<ArrayCalculator_MinTime_Hermite> arrayCalculators)
        {
            if (arrayCalculators == null) throw new NullException(() => arrayCalculators);
            _arrayCalculators = arrayCalculators.ToArray();
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTime_Block : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Block _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTime_Block(ArrayCalculator_MinTime_Block arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTime_Stripe : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Stripe _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTime_Stripe(ArrayCalculator_MinTime_Stripe arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTime_Line : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Line _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTime_Line(ArrayCalculator_MinTime_Line arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTime_Cubic : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Cubic _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTime_Cubic(ArrayCalculator_MinTime_Cubic arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }

    internal class Cache_OperatorCalculator_SingleChannel_MinTime_Hermite : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinTime_Hermite _arrayCalculator;

        public Cache_OperatorCalculator_SingleChannel_MinTime_Hermite(ArrayCalculator_MinTime_Hermite arrayCalculator)
        {
            if (arrayCalculator == null) throw new NullException(() => arrayCalculator);
            _arrayCalculator = arrayCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }
}