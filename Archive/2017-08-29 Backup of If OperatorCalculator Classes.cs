//using System;
//using System.Linq.Expressions;
//using System.Runtime.CompilerServices;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class If_OperatorCalculator_VarCondition_VarThen_VarElse : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _conditionCalculator;
//        private readonly OperatorCalculatorBase _thenCalculator;
//        private readonly OperatorCalculatorBase _elseCalculator;

//        public If_OperatorCalculator_VarCondition_VarThen_VarElse(
//            OperatorCalculatorBase conditionCalculator,
//            OperatorCalculatorBase thenCalculator,
//            OperatorCalculatorBase elseCalculator)
//            : base(new[] { conditionCalculator, thenCalculator, elseCalculator })
//        {
//            _conditionCalculator = conditionCalculator ?? throw new NullException(() => conditionCalculator);
//            _thenCalculator = thenCalculator ?? throw new NullException(() => thenCalculator);
//            _elseCalculator = elseCalculator ?? throw new NullException(() => elseCalculator);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double condition = _conditionCalculator.Calculate();

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isTrue = condition != 0.0;

//            return isTrue ? _thenCalculator.Calculate() : _elseCalculator.Calculate();
//        }
//    }

//    internal class If_OperatorCalculator_VarCondition_ConstThen_VarElse : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _conditionCalculator;
//        private readonly double _then;
//        private readonly OperatorCalculatorBase _elseCalculator;

//        public If_OperatorCalculator_VarCondition_ConstThen_VarElse(
//            OperatorCalculatorBase conditionCalculator,
//            double then,
//            OperatorCalculatorBase elseCalculator)
//            : base(new[] { conditionCalculator, elseCalculator })
//        {
//            _conditionCalculator = conditionCalculator ?? throw new NullException(() => conditionCalculator);
//            _then = then;
//            _elseCalculator = elseCalculator ?? throw new NullException(() => elseCalculator);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double condition = _conditionCalculator.Calculate();

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isTrue = condition != 0.0;

//            return isTrue ? _then : _elseCalculator.Calculate();
//        }
//    }

//    internal class If_OperatorCalculator_VarCondition_VarThen_ConstElse : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _conditionCalculator;
//        private readonly OperatorCalculatorBase _thenCalculator;
//        private readonly double _else;

//        public If_OperatorCalculator_VarCondition_VarThen_ConstElse(
//            OperatorCalculatorBase conditionCalculator,
//            OperatorCalculatorBase thenCalculator,
//            double @else)
//            : base(new[] { conditionCalculator, thenCalculator })
//        {
//            _conditionCalculator = conditionCalculator ?? throw new NullException(() => conditionCalculator);
//            _thenCalculator = thenCalculator ?? throw new NullException(() => thenCalculator);
//            _else = @else;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double condition = _conditionCalculator.Calculate();

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isTrue = condition != 0.0;

//            return isTrue ? _thenCalculator.Calculate() : _else;
//        }
//    }

//    internal class If_OperatorCalculator_VarCondition_ConstThen_ConstElse : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _conditionCalculator;
//        private readonly double _then;
//        private readonly double _else;

//        public If_OperatorCalculator_VarCondition_ConstThen_ConstElse(
//            OperatorCalculatorBase conditionCalculator,
//            double then,
//            double @else)
//            : base(new[] { conditionCalculator })
//        {
//            _conditionCalculator = conditionCalculator ?? throw new NullException(() => conditionCalculator);
//            _then = then;
//            _else = @else;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double condition = _conditionCalculator.Calculate();

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            bool isTrue = condition != 0.0;

//            return isTrue ? _then : _else;
//        }
//    }
//}
