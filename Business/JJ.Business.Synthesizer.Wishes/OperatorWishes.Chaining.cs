using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.Wishes
{
    public class ChainedOutlet
    {
        private readonly SynthWishes x;
        private readonly Outlet _firstOperand;

        public ChainedOutlet(SynthWishes synthWishes, Outlet firstFirstOperand)
        {
            x = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _firstOperand = firstFirstOperand ?? throw new ArgumentNullException(nameof(firstFirstOperand));
        }
        
        public static implicit operator Outlet(ChainedOutlet chainedOutlet) 
            => chainedOutlet._firstOperand;

        public ChainedOutlet Multiply(Outlet operandB)
        {
            var multiply = x.Multiply(_firstOperand, operandB);
            return new ChainedOutlet(x, multiply);
        }

        public ChainedOutlet Multiply(double operandB)
        {
            var multiply = x.Multiply(_firstOperand, x._[operandB]);
            return new ChainedOutlet(x, multiply);
        }

        public ChainedOutlet Panbrello(Outlet speed = default, Outlet depth = default)
        {
            var panbrello = x.Panbrello(_firstOperand, (speed, depth));
            return new ChainedOutlet(x, panbrello);
        }

        /// <inheritdoc cref="docs._panbrello" />
        public ChainedOutlet Panbrello(double speed = default, double depth = default)
        {
            if (speed == default) speed = 1;
            if (depth == default) depth = 1;
            
            var panbrello = x.Panbrello(_firstOperand, (x._[speed], x._[depth]));
            
            return new ChainedOutlet(x, panbrello);
        }
    }
}
