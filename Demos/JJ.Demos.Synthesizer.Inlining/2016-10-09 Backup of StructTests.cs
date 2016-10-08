//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Framework.Testing;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace JJ.Demos.Synthesizer.Inlining
//{
//    [TestClass]
//    public class StructTests
//    {
//        private interface IStruct
//        {
//            double Calculate();
//        }

//        private struct ValueStruct : IStruct
//        {
//            public double _value;

//            public ValueStruct(int value)
//            {
//                _value = value;
//            }

//            [MethodImpl(MethodImplOptions.AggressiveInlining)]
//            public double Calculate()
//            {
//                return _value;
//            }
//        }

//        private struct MultiplyStruct<TChild1, TChild2> : IStruct
//            where TChild1 : IStruct
//            where TChild2 : IStruct
//        {
//            public TChild1 _child1;
//            public TChild1 _child2;

//            [MethodImpl(MethodImplOptions.AggressiveInlining)]
//            public double Calculate()
//            {
//                double value1 = _child1.Calculate();
//                double value2 = _child2.Calculate();

//                return value1 * value2;
//            }
//        }

//        [TestMethod]
//        public void Test_Structs()
//        {
//            MultiplyStruct<ValueStruct, ValueStruct> _multiplyStruct;

//            _multiplyStruct._child1 = new ValueStruct(2);
//            _multiplyStruct._child2 = new ValueStruct(3);

//            AssertHelper.AreEqual(6, () => _multiplyStruct.Calculate());
//        }
//    }
//}