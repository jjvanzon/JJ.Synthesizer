using JJ.Business.Synthesizer.Visitors;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    public class OperatorCalculatorNew
    {
        private OperatorCalculatorBase _rootOperatorCalculator;

        /// <summary>
        /// TODO: the rootOperator should probably become the rootOutlet. 
        /// </summary>
        public OperatorCalculatorNew(Operator rootOperator)
        {
            if (rootOperator == null) throw new NullException(() => rootOperator);

            var visitor = new OperatorCalculatorVisitor();
            _rootOperatorCalculator = visitor.Execute(rootOperator);
        }

        public virtual double Calculate(double time, int channelIndex)
        {
            return _rootOperatorCalculator.Calculate(time, channelIndex);
        }
    }
}
