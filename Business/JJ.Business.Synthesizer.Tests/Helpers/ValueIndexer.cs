using JJ.Business.Synthesizer.Factories;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class ValueIndexer
    {
        private readonly OperatorFactory _operatorFactory;
        public ValueIndexer(OperatorFactory operatorFactory) => _operatorFactory = operatorFactory;

        public Outlet this[double value] => _operatorFactory.Value(value);
    }
}