


using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{

		internal class Multiply_OperatorCalculator_1Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_1Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
				double constValue)
			: base(new[] { 
					operandCalculator1
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_2Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_2Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_3Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_3Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_4Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_4Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_5Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_5Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_6Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_6Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_7Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_7Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
					if (operandCalculator7 == null) throw new NullException(() => operandCalculator7);
					_operandCalculator7 = operandCalculator7;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_8Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_8Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
					OperatorCalculatorBase operandCalculator8,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
,					operandCalculator8
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
					if (operandCalculator7 == null) throw new NullException(() => operandCalculator7);
					_operandCalculator7 = operandCalculator7;
					if (operandCalculator8 == null) throw new NullException(() => operandCalculator8);
					_operandCalculator8 = operandCalculator8;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
*						_operandCalculator8.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_9Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
				private readonly OperatorCalculatorBase _operandCalculator9;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_9Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
					OperatorCalculatorBase operandCalculator8,
					OperatorCalculatorBase operandCalculator9,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
,					operandCalculator8
,					operandCalculator9
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
					if (operandCalculator7 == null) throw new NullException(() => operandCalculator7);
					_operandCalculator7 = operandCalculator7;
					if (operandCalculator8 == null) throw new NullException(() => operandCalculator8);
					_operandCalculator8 = operandCalculator8;
					if (operandCalculator9 == null) throw new NullException(() => operandCalculator9);
					_operandCalculator9 = operandCalculator9;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
*						_operandCalculator8.Calculate()
*						_operandCalculator9.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_10Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
				private readonly OperatorCalculatorBase _operandCalculator9;
				private readonly OperatorCalculatorBase _operandCalculator10;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_10Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
					OperatorCalculatorBase operandCalculator8,
					OperatorCalculatorBase operandCalculator9,
					OperatorCalculatorBase operandCalculator10,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
,					operandCalculator8
,					operandCalculator9
,					operandCalculator10
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
					if (operandCalculator7 == null) throw new NullException(() => operandCalculator7);
					_operandCalculator7 = operandCalculator7;
					if (operandCalculator8 == null) throw new NullException(() => operandCalculator8);
					_operandCalculator8 = operandCalculator8;
					if (operandCalculator9 == null) throw new NullException(() => operandCalculator9);
					_operandCalculator9 = operandCalculator9;
					if (operandCalculator10 == null) throw new NullException(() => operandCalculator10);
					_operandCalculator10 = operandCalculator10;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
*						_operandCalculator8.Calculate()
*						_operandCalculator9.Calculate()
*						_operandCalculator10.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_11Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
				private readonly OperatorCalculatorBase _operandCalculator9;
				private readonly OperatorCalculatorBase _operandCalculator10;
				private readonly OperatorCalculatorBase _operandCalculator11;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_11Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
					OperatorCalculatorBase operandCalculator8,
					OperatorCalculatorBase operandCalculator9,
					OperatorCalculatorBase operandCalculator10,
					OperatorCalculatorBase operandCalculator11,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
,					operandCalculator8
,					operandCalculator9
,					operandCalculator10
,					operandCalculator11
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
					if (operandCalculator7 == null) throw new NullException(() => operandCalculator7);
					_operandCalculator7 = operandCalculator7;
					if (operandCalculator8 == null) throw new NullException(() => operandCalculator8);
					_operandCalculator8 = operandCalculator8;
					if (operandCalculator9 == null) throw new NullException(() => operandCalculator9);
					_operandCalculator9 = operandCalculator9;
					if (operandCalculator10 == null) throw new NullException(() => operandCalculator10);
					_operandCalculator10 = operandCalculator10;
					if (operandCalculator11 == null) throw new NullException(() => operandCalculator11);
					_operandCalculator11 = operandCalculator11;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
*						_operandCalculator8.Calculate()
*						_operandCalculator9.Calculate()
*						_operandCalculator10.Calculate()
*						_operandCalculator11.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_12Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
				private readonly OperatorCalculatorBase _operandCalculator9;
				private readonly OperatorCalculatorBase _operandCalculator10;
				private readonly OperatorCalculatorBase _operandCalculator11;
				private readonly OperatorCalculatorBase _operandCalculator12;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_12Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
					OperatorCalculatorBase operandCalculator8,
					OperatorCalculatorBase operandCalculator9,
					OperatorCalculatorBase operandCalculator10,
					OperatorCalculatorBase operandCalculator11,
					OperatorCalculatorBase operandCalculator12,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
,					operandCalculator8
,					operandCalculator9
,					operandCalculator10
,					operandCalculator11
,					operandCalculator12
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
					if (operandCalculator7 == null) throw new NullException(() => operandCalculator7);
					_operandCalculator7 = operandCalculator7;
					if (operandCalculator8 == null) throw new NullException(() => operandCalculator8);
					_operandCalculator8 = operandCalculator8;
					if (operandCalculator9 == null) throw new NullException(() => operandCalculator9);
					_operandCalculator9 = operandCalculator9;
					if (operandCalculator10 == null) throw new NullException(() => operandCalculator10);
					_operandCalculator10 = operandCalculator10;
					if (operandCalculator11 == null) throw new NullException(() => operandCalculator11);
					_operandCalculator11 = operandCalculator11;
					if (operandCalculator12 == null) throw new NullException(() => operandCalculator12);
					_operandCalculator12 = operandCalculator12;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
*						_operandCalculator8.Calculate()
*						_operandCalculator9.Calculate()
*						_operandCalculator10.Calculate()
*						_operandCalculator11.Calculate()
*						_operandCalculator12.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_13Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
				private readonly OperatorCalculatorBase _operandCalculator9;
				private readonly OperatorCalculatorBase _operandCalculator10;
				private readonly OperatorCalculatorBase _operandCalculator11;
				private readonly OperatorCalculatorBase _operandCalculator12;
				private readonly OperatorCalculatorBase _operandCalculator13;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_13Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
					OperatorCalculatorBase operandCalculator8,
					OperatorCalculatorBase operandCalculator9,
					OperatorCalculatorBase operandCalculator10,
					OperatorCalculatorBase operandCalculator11,
					OperatorCalculatorBase operandCalculator12,
					OperatorCalculatorBase operandCalculator13,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
,					operandCalculator8
,					operandCalculator9
,					operandCalculator10
,					operandCalculator11
,					operandCalculator12
,					operandCalculator13
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
					if (operandCalculator7 == null) throw new NullException(() => operandCalculator7);
					_operandCalculator7 = operandCalculator7;
					if (operandCalculator8 == null) throw new NullException(() => operandCalculator8);
					_operandCalculator8 = operandCalculator8;
					if (operandCalculator9 == null) throw new NullException(() => operandCalculator9);
					_operandCalculator9 = operandCalculator9;
					if (operandCalculator10 == null) throw new NullException(() => operandCalculator10);
					_operandCalculator10 = operandCalculator10;
					if (operandCalculator11 == null) throw new NullException(() => operandCalculator11);
					_operandCalculator11 = operandCalculator11;
					if (operandCalculator12 == null) throw new NullException(() => operandCalculator12);
					_operandCalculator12 = operandCalculator12;
					if (operandCalculator13 == null) throw new NullException(() => operandCalculator13);
					_operandCalculator13 = operandCalculator13;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
*						_operandCalculator8.Calculate()
*						_operandCalculator9.Calculate()
*						_operandCalculator10.Calculate()
*						_operandCalculator11.Calculate()
*						_operandCalculator12.Calculate()
*						_operandCalculator13.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_14Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
				private readonly OperatorCalculatorBase _operandCalculator9;
				private readonly OperatorCalculatorBase _operandCalculator10;
				private readonly OperatorCalculatorBase _operandCalculator11;
				private readonly OperatorCalculatorBase _operandCalculator12;
				private readonly OperatorCalculatorBase _operandCalculator13;
				private readonly OperatorCalculatorBase _operandCalculator14;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_14Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
					OperatorCalculatorBase operandCalculator8,
					OperatorCalculatorBase operandCalculator9,
					OperatorCalculatorBase operandCalculator10,
					OperatorCalculatorBase operandCalculator11,
					OperatorCalculatorBase operandCalculator12,
					OperatorCalculatorBase operandCalculator13,
					OperatorCalculatorBase operandCalculator14,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
,					operandCalculator8
,					operandCalculator9
,					operandCalculator10
,					operandCalculator11
,					operandCalculator12
,					operandCalculator13
,					operandCalculator14
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
					if (operandCalculator7 == null) throw new NullException(() => operandCalculator7);
					_operandCalculator7 = operandCalculator7;
					if (operandCalculator8 == null) throw new NullException(() => operandCalculator8);
					_operandCalculator8 = operandCalculator8;
					if (operandCalculator9 == null) throw new NullException(() => operandCalculator9);
					_operandCalculator9 = operandCalculator9;
					if (operandCalculator10 == null) throw new NullException(() => operandCalculator10);
					_operandCalculator10 = operandCalculator10;
					if (operandCalculator11 == null) throw new NullException(() => operandCalculator11);
					_operandCalculator11 = operandCalculator11;
					if (operandCalculator12 == null) throw new NullException(() => operandCalculator12);
					_operandCalculator12 = operandCalculator12;
					if (operandCalculator13 == null) throw new NullException(() => operandCalculator13);
					_operandCalculator13 = operandCalculator13;
					if (operandCalculator14 == null) throw new NullException(() => operandCalculator14);
					_operandCalculator14 = operandCalculator14;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
*						_operandCalculator8.Calculate()
*						_operandCalculator9.Calculate()
*						_operandCalculator10.Calculate()
*						_operandCalculator11.Calculate()
*						_operandCalculator12.Calculate()
*						_operandCalculator13.Calculate()
*						_operandCalculator14.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_15Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
				private readonly OperatorCalculatorBase _operandCalculator9;
				private readonly OperatorCalculatorBase _operandCalculator10;
				private readonly OperatorCalculatorBase _operandCalculator11;
				private readonly OperatorCalculatorBase _operandCalculator12;
				private readonly OperatorCalculatorBase _operandCalculator13;
				private readonly OperatorCalculatorBase _operandCalculator14;
				private readonly OperatorCalculatorBase _operandCalculator15;
			private readonly double _constValue;
			
			public Multiply_OperatorCalculator_15Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
					OperatorCalculatorBase operandCalculator8,
					OperatorCalculatorBase operandCalculator9,
					OperatorCalculatorBase operandCalculator10,
					OperatorCalculatorBase operandCalculator11,
					OperatorCalculatorBase operandCalculator12,
					OperatorCalculatorBase operandCalculator13,
					OperatorCalculatorBase operandCalculator14,
					OperatorCalculatorBase operandCalculator15,
				double constValue)
			: base(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
,					operandCalculator8
,					operandCalculator9
,					operandCalculator10
,					operandCalculator11
,					operandCalculator12
,					operandCalculator13
,					operandCalculator14
,					operandCalculator15
			})
			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
					if (operandCalculator5 == null) throw new NullException(() => operandCalculator5);
					_operandCalculator5 = operandCalculator5;
					if (operandCalculator6 == null) throw new NullException(() => operandCalculator6);
					_operandCalculator6 = operandCalculator6;
					if (operandCalculator7 == null) throw new NullException(() => operandCalculator7);
					_operandCalculator7 = operandCalculator7;
					if (operandCalculator8 == null) throw new NullException(() => operandCalculator8);
					_operandCalculator8 = operandCalculator8;
					if (operandCalculator9 == null) throw new NullException(() => operandCalculator9);
					_operandCalculator9 = operandCalculator9;
					if (operandCalculator10 == null) throw new NullException(() => operandCalculator10);
					_operandCalculator10 = operandCalculator10;
					if (operandCalculator11 == null) throw new NullException(() => operandCalculator11);
					_operandCalculator11 = operandCalculator11;
					if (operandCalculator12 == null) throw new NullException(() => operandCalculator12);
					_operandCalculator12 = operandCalculator12;
					if (operandCalculator13 == null) throw new NullException(() => operandCalculator13);
					_operandCalculator13 = operandCalculator13;
					if (operandCalculator14 == null) throw new NullException(() => operandCalculator14);
					_operandCalculator14 = operandCalculator14;
					if (operandCalculator15 == null) throw new NullException(() => operandCalculator15);
					_operandCalculator15 = operandCalculator15;
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
*						_operandCalculator8.Calculate()
*						_operandCalculator9.Calculate()
*						_operandCalculator10.Calculate()
*						_operandCalculator11.Calculate()
*						_operandCalculator12.Calculate()
*						_operandCalculator13.Calculate()
*						_operandCalculator14.Calculate()
*						_operandCalculator15.Calculate()
;			}
		}
}
