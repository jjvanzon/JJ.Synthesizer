//using System;
//using JJ.Business.Synthesizer.Factories;
//using JJ.Persistence.Synthesizer;

//namespace JJ.Business.Synthesizer.Tests.Helpers
//{
//    public class BarIndexer
//    {
//        private readonly OperatorFactory _operatorFactory;

//        public BarIndexer(OperatorFactory operatorFactory)
//        {
//            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
//            _operatorFactory = operatorFactory;
//        }

//        public Outlet this[double value] => _operatorFactory.Value(value);
//    }
//}