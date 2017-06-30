//using JetBrains.Annotations;
//using JJ.Data.Synthesizer.Interfaces;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Helpers
//{
//    public class InletOrOutletTuple<T>
//        where T : class, IInletOrOutlet
//    {
//        public InletOrOutletTuple([NotNull] T sourceInletOrOutlet, T destInletOrOutlet)
//        {
//            SourceInletOrOutlet = sourceInletOrOutlet ?? throw new NullException(() => sourceInletOrOutlet);
//            DestInletOrOutlet = destInletOrOutlet;
//        }

//        public T SourceInletOrOutlet { get; }

//        /// <summary> nullable </summary>
//        public T DestInletOrOutlet { get; }
//    }
//}