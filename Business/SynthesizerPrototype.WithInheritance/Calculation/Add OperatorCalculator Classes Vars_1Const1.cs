


using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{

		internal class Add_OperatorCalculator_1Vars_1Const : OperatorCalculatorBase
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_1Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_2Vars_1Const : OperatorCalculatorBase
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_2Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_3Vars_1Const : OperatorCalculatorBase
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_3Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_4Vars_1Const : OperatorCalculatorBase
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_4Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_5Vars_1Const : OperatorCalculatorBase
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_5Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_6Vars_1Const : OperatorCalculatorBase
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_6Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_7Vars_1Const : OperatorCalculatorBase
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_7Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_8Vars_1Const : OperatorCalculatorBase
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
			
			public Add_OperatorCalculator_8Vars_1Const(
					OperatorCalculatorBase operandCalculator1,
					OperatorCalculatorBase operandCalculator2,
					OperatorCalculatorBase operandCalculator3,
					OperatorCalculatorBase operandCalculator4,
					OperatorCalculatorBase operandCalculator5,
					OperatorCalculatorBase operandCalculator6,
					OperatorCalculatorBase operandCalculator7,
					OperatorCalculatorBase operandCalculator8,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_9Vars_1Const : OperatorCalculatorBase
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
			
			public Add_OperatorCalculator_9Vars_1Const(
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
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_10Vars_1Const : OperatorCalculatorBase
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
			
			public Add_OperatorCalculator_10Vars_1Const(
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
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_11Vars_1Const : OperatorCalculatorBase
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
			
			public Add_OperatorCalculator_11Vars_1Const(
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
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_12Vars_1Const : OperatorCalculatorBase
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
			
			public Add_OperatorCalculator_12Vars_1Const(
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
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_13Vars_1Const : OperatorCalculatorBase
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
			
			public Add_OperatorCalculator_13Vars_1Const(
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
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_14Vars_1Const : OperatorCalculatorBase
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
			
			public Add_OperatorCalculator_14Vars_1Const(
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
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_15Vars_1Const : OperatorCalculatorBase
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
			
			public Add_OperatorCalculator_15Vars_1Const(
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
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_16Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_16Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_17Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_17Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_18Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_18Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_19Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_19Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_20Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_20Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_21Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_21Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_22Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_22Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_23Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
				private readonly OperatorCalculatorBase _operandCalculator23;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_23Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
					OperatorCalculatorBase operandCalculator23,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
					_operandCalculator23 = operandCalculator23 ?? throw new NullException(() => operandCalculator23);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
+						_operandCalculator23.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_24Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
				private readonly OperatorCalculatorBase _operandCalculator23;
				private readonly OperatorCalculatorBase _operandCalculator24;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_24Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
					OperatorCalculatorBase operandCalculator23,
					OperatorCalculatorBase operandCalculator24,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
					_operandCalculator23 = operandCalculator23 ?? throw new NullException(() => operandCalculator23);
					_operandCalculator24 = operandCalculator24 ?? throw new NullException(() => operandCalculator24);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
+						_operandCalculator23.Calculate()
+						_operandCalculator24.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_25Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
				private readonly OperatorCalculatorBase _operandCalculator23;
				private readonly OperatorCalculatorBase _operandCalculator24;
				private readonly OperatorCalculatorBase _operandCalculator25;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_25Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
					OperatorCalculatorBase operandCalculator23,
					OperatorCalculatorBase operandCalculator24,
					OperatorCalculatorBase operandCalculator25,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
					_operandCalculator23 = operandCalculator23 ?? throw new NullException(() => operandCalculator23);
					_operandCalculator24 = operandCalculator24 ?? throw new NullException(() => operandCalculator24);
					_operandCalculator25 = operandCalculator25 ?? throw new NullException(() => operandCalculator25);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
+						_operandCalculator23.Calculate()
+						_operandCalculator24.Calculate()
+						_operandCalculator25.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_26Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
				private readonly OperatorCalculatorBase _operandCalculator23;
				private readonly OperatorCalculatorBase _operandCalculator24;
				private readonly OperatorCalculatorBase _operandCalculator25;
				private readonly OperatorCalculatorBase _operandCalculator26;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_26Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
					OperatorCalculatorBase operandCalculator23,
					OperatorCalculatorBase operandCalculator24,
					OperatorCalculatorBase operandCalculator25,
					OperatorCalculatorBase operandCalculator26,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
					_operandCalculator23 = operandCalculator23 ?? throw new NullException(() => operandCalculator23);
					_operandCalculator24 = operandCalculator24 ?? throw new NullException(() => operandCalculator24);
					_operandCalculator25 = operandCalculator25 ?? throw new NullException(() => operandCalculator25);
					_operandCalculator26 = operandCalculator26 ?? throw new NullException(() => operandCalculator26);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
+						_operandCalculator23.Calculate()
+						_operandCalculator24.Calculate()
+						_operandCalculator25.Calculate()
+						_operandCalculator26.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_27Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
				private readonly OperatorCalculatorBase _operandCalculator23;
				private readonly OperatorCalculatorBase _operandCalculator24;
				private readonly OperatorCalculatorBase _operandCalculator25;
				private readonly OperatorCalculatorBase _operandCalculator26;
				private readonly OperatorCalculatorBase _operandCalculator27;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_27Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
					OperatorCalculatorBase operandCalculator23,
					OperatorCalculatorBase operandCalculator24,
					OperatorCalculatorBase operandCalculator25,
					OperatorCalculatorBase operandCalculator26,
					OperatorCalculatorBase operandCalculator27,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
					_operandCalculator23 = operandCalculator23 ?? throw new NullException(() => operandCalculator23);
					_operandCalculator24 = operandCalculator24 ?? throw new NullException(() => operandCalculator24);
					_operandCalculator25 = operandCalculator25 ?? throw new NullException(() => operandCalculator25);
					_operandCalculator26 = operandCalculator26 ?? throw new NullException(() => operandCalculator26);
					_operandCalculator27 = operandCalculator27 ?? throw new NullException(() => operandCalculator27);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
+						_operandCalculator23.Calculate()
+						_operandCalculator24.Calculate()
+						_operandCalculator25.Calculate()
+						_operandCalculator26.Calculate()
+						_operandCalculator27.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_28Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
				private readonly OperatorCalculatorBase _operandCalculator23;
				private readonly OperatorCalculatorBase _operandCalculator24;
				private readonly OperatorCalculatorBase _operandCalculator25;
				private readonly OperatorCalculatorBase _operandCalculator26;
				private readonly OperatorCalculatorBase _operandCalculator27;
				private readonly OperatorCalculatorBase _operandCalculator28;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_28Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
					OperatorCalculatorBase operandCalculator23,
					OperatorCalculatorBase operandCalculator24,
					OperatorCalculatorBase operandCalculator25,
					OperatorCalculatorBase operandCalculator26,
					OperatorCalculatorBase operandCalculator27,
					OperatorCalculatorBase operandCalculator28,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
					_operandCalculator23 = operandCalculator23 ?? throw new NullException(() => operandCalculator23);
					_operandCalculator24 = operandCalculator24 ?? throw new NullException(() => operandCalculator24);
					_operandCalculator25 = operandCalculator25 ?? throw new NullException(() => operandCalculator25);
					_operandCalculator26 = operandCalculator26 ?? throw new NullException(() => operandCalculator26);
					_operandCalculator27 = operandCalculator27 ?? throw new NullException(() => operandCalculator27);
					_operandCalculator28 = operandCalculator28 ?? throw new NullException(() => operandCalculator28);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
+						_operandCalculator23.Calculate()
+						_operandCalculator24.Calculate()
+						_operandCalculator25.Calculate()
+						_operandCalculator26.Calculate()
+						_operandCalculator27.Calculate()
+						_operandCalculator28.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_29Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
				private readonly OperatorCalculatorBase _operandCalculator23;
				private readonly OperatorCalculatorBase _operandCalculator24;
				private readonly OperatorCalculatorBase _operandCalculator25;
				private readonly OperatorCalculatorBase _operandCalculator26;
				private readonly OperatorCalculatorBase _operandCalculator27;
				private readonly OperatorCalculatorBase _operandCalculator28;
				private readonly OperatorCalculatorBase _operandCalculator29;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_29Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
					OperatorCalculatorBase operandCalculator23,
					OperatorCalculatorBase operandCalculator24,
					OperatorCalculatorBase operandCalculator25,
					OperatorCalculatorBase operandCalculator26,
					OperatorCalculatorBase operandCalculator27,
					OperatorCalculatorBase operandCalculator28,
					OperatorCalculatorBase operandCalculator29,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
					_operandCalculator23 = operandCalculator23 ?? throw new NullException(() => operandCalculator23);
					_operandCalculator24 = operandCalculator24 ?? throw new NullException(() => operandCalculator24);
					_operandCalculator25 = operandCalculator25 ?? throw new NullException(() => operandCalculator25);
					_operandCalculator26 = operandCalculator26 ?? throw new NullException(() => operandCalculator26);
					_operandCalculator27 = operandCalculator27 ?? throw new NullException(() => operandCalculator27);
					_operandCalculator28 = operandCalculator28 ?? throw new NullException(() => operandCalculator28);
					_operandCalculator29 = operandCalculator29 ?? throw new NullException(() => operandCalculator29);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
+						_operandCalculator23.Calculate()
+						_operandCalculator24.Calculate()
+						_operandCalculator25.Calculate()
+						_operandCalculator26.Calculate()
+						_operandCalculator27.Calculate()
+						_operandCalculator28.Calculate()
+						_operandCalculator29.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_30Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
				private readonly OperatorCalculatorBase _operandCalculator23;
				private readonly OperatorCalculatorBase _operandCalculator24;
				private readonly OperatorCalculatorBase _operandCalculator25;
				private readonly OperatorCalculatorBase _operandCalculator26;
				private readonly OperatorCalculatorBase _operandCalculator27;
				private readonly OperatorCalculatorBase _operandCalculator28;
				private readonly OperatorCalculatorBase _operandCalculator29;
				private readonly OperatorCalculatorBase _operandCalculator30;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_30Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
					OperatorCalculatorBase operandCalculator23,
					OperatorCalculatorBase operandCalculator24,
					OperatorCalculatorBase operandCalculator25,
					OperatorCalculatorBase operandCalculator26,
					OperatorCalculatorBase operandCalculator27,
					OperatorCalculatorBase operandCalculator28,
					OperatorCalculatorBase operandCalculator29,
					OperatorCalculatorBase operandCalculator30,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
					_operandCalculator23 = operandCalculator23 ?? throw new NullException(() => operandCalculator23);
					_operandCalculator24 = operandCalculator24 ?? throw new NullException(() => operandCalculator24);
					_operandCalculator25 = operandCalculator25 ?? throw new NullException(() => operandCalculator25);
					_operandCalculator26 = operandCalculator26 ?? throw new NullException(() => operandCalculator26);
					_operandCalculator27 = operandCalculator27 ?? throw new NullException(() => operandCalculator27);
					_operandCalculator28 = operandCalculator28 ?? throw new NullException(() => operandCalculator28);
					_operandCalculator29 = operandCalculator29 ?? throw new NullException(() => operandCalculator29);
					_operandCalculator30 = operandCalculator30 ?? throw new NullException(() => operandCalculator30);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
+						_operandCalculator23.Calculate()
+						_operandCalculator24.Calculate()
+						_operandCalculator25.Calculate()
+						_operandCalculator26.Calculate()
+						_operandCalculator27.Calculate()
+						_operandCalculator28.Calculate()
+						_operandCalculator29.Calculate()
+						_operandCalculator30.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_31Vars_1Const : OperatorCalculatorBase
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
				private readonly OperatorCalculatorBase _operandCalculator16;
				private readonly OperatorCalculatorBase _operandCalculator17;
				private readonly OperatorCalculatorBase _operandCalculator18;
				private readonly OperatorCalculatorBase _operandCalculator19;
				private readonly OperatorCalculatorBase _operandCalculator20;
				private readonly OperatorCalculatorBase _operandCalculator21;
				private readonly OperatorCalculatorBase _operandCalculator22;
				private readonly OperatorCalculatorBase _operandCalculator23;
				private readonly OperatorCalculatorBase _operandCalculator24;
				private readonly OperatorCalculatorBase _operandCalculator25;
				private readonly OperatorCalculatorBase _operandCalculator26;
				private readonly OperatorCalculatorBase _operandCalculator27;
				private readonly OperatorCalculatorBase _operandCalculator28;
				private readonly OperatorCalculatorBase _operandCalculator29;
				private readonly OperatorCalculatorBase _operandCalculator30;
				private readonly OperatorCalculatorBase _operandCalculator31;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_31Vars_1Const(
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
					OperatorCalculatorBase operandCalculator16,
					OperatorCalculatorBase operandCalculator17,
					OperatorCalculatorBase operandCalculator18,
					OperatorCalculatorBase operandCalculator19,
					OperatorCalculatorBase operandCalculator20,
					OperatorCalculatorBase operandCalculator21,
					OperatorCalculatorBase operandCalculator22,
					OperatorCalculatorBase operandCalculator23,
					OperatorCalculatorBase operandCalculator24,
					OperatorCalculatorBase operandCalculator25,
					OperatorCalculatorBase operandCalculator26,
					OperatorCalculatorBase operandCalculator27,
					OperatorCalculatorBase operandCalculator28,
					OperatorCalculatorBase operandCalculator29,
					OperatorCalculatorBase operandCalculator30,
					OperatorCalculatorBase operandCalculator31,
				double constValue)
			{
					_operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
					_operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
					_operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
					_operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
					_operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
					_operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
					_operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
					_operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
					_operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
					_operandCalculator10 = operandCalculator10 ?? throw new NullException(() => operandCalculator10);
					_operandCalculator11 = operandCalculator11 ?? throw new NullException(() => operandCalculator11);
					_operandCalculator12 = operandCalculator12 ?? throw new NullException(() => operandCalculator12);
					_operandCalculator13 = operandCalculator13 ?? throw new NullException(() => operandCalculator13);
					_operandCalculator14 = operandCalculator14 ?? throw new NullException(() => operandCalculator14);
					_operandCalculator15 = operandCalculator15 ?? throw new NullException(() => operandCalculator15);
					_operandCalculator16 = operandCalculator16 ?? throw new NullException(() => operandCalculator16);
					_operandCalculator17 = operandCalculator17 ?? throw new NullException(() => operandCalculator17);
					_operandCalculator18 = operandCalculator18 ?? throw new NullException(() => operandCalculator18);
					_operandCalculator19 = operandCalculator19 ?? throw new NullException(() => operandCalculator19);
					_operandCalculator20 = operandCalculator20 ?? throw new NullException(() => operandCalculator20);
					_operandCalculator21 = operandCalculator21 ?? throw new NullException(() => operandCalculator21);
					_operandCalculator22 = operandCalculator22 ?? throw new NullException(() => operandCalculator22);
					_operandCalculator23 = operandCalculator23 ?? throw new NullException(() => operandCalculator23);
					_operandCalculator24 = operandCalculator24 ?? throw new NullException(() => operandCalculator24);
					_operandCalculator25 = operandCalculator25 ?? throw new NullException(() => operandCalculator25);
					_operandCalculator26 = operandCalculator26 ?? throw new NullException(() => operandCalculator26);
					_operandCalculator27 = operandCalculator27 ?? throw new NullException(() => operandCalculator27);
					_operandCalculator28 = operandCalculator28 ?? throw new NullException(() => operandCalculator28);
					_operandCalculator29 = operandCalculator29 ?? throw new NullException(() => operandCalculator29);
					_operandCalculator30 = operandCalculator30 ?? throw new NullException(() => operandCalculator30);
					_operandCalculator31 = operandCalculator31 ?? throw new NullException(() => operandCalculator31);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
+						_operandCalculator8.Calculate()
+						_operandCalculator9.Calculate()
+						_operandCalculator10.Calculate()
+						_operandCalculator11.Calculate()
+						_operandCalculator12.Calculate()
+						_operandCalculator13.Calculate()
+						_operandCalculator14.Calculate()
+						_operandCalculator15.Calculate()
+						_operandCalculator16.Calculate()
+						_operandCalculator17.Calculate()
+						_operandCalculator18.Calculate()
+						_operandCalculator19.Calculate()
+						_operandCalculator20.Calculate()
+						_operandCalculator21.Calculate()
+						_operandCalculator22.Calculate()
+						_operandCalculator23.Calculate()
+						_operandCalculator24.Calculate()
+						_operandCalculator25.Calculate()
+						_operandCalculator26.Calculate()
+						_operandCalculator27.Calculate()
+						_operandCalculator28.Calculate()
+						_operandCalculator29.Calculate()
+						_operandCalculator30.Calculate()
+						_operandCalculator31.Calculate()
;			}
		}
}
