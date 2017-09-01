


/*
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{

		internal class Add_OperatorCalculator_1Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_1Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
				double constValue)
			: base(new [] { 
					itemCalculator1
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_2Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_2Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_3Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_3Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_4Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_4Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_5Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_5Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_6Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_6Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_7Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_7Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_8Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_8Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_9Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_9Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_10Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_10Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_11Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_11Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_12Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_12Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_13Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_13Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_14Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_14Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_15Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_15Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_16Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_16Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_17Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_17Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_18Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_18Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_19Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_19Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_20Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_20Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_21Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_21Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_22Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_22Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_23Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
				private readonly OperatorCalculatorBase _itemCalculator23;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_23Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
					OperatorCalculatorBase itemCalculator23,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
,					itemCalculator23
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
					_itemCalculator23 = itemCalculator23 ?? throw new NullException(() => itemCalculator23);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
+						_itemCalculator23.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_24Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
				private readonly OperatorCalculatorBase _itemCalculator23;
				private readonly OperatorCalculatorBase _itemCalculator24;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_24Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
					OperatorCalculatorBase itemCalculator23,
					OperatorCalculatorBase itemCalculator24,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
,					itemCalculator23
,					itemCalculator24
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
					_itemCalculator23 = itemCalculator23 ?? throw new NullException(() => itemCalculator23);
					_itemCalculator24 = itemCalculator24 ?? throw new NullException(() => itemCalculator24);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
+						_itemCalculator23.Calculate()
+						_itemCalculator24.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_25Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
				private readonly OperatorCalculatorBase _itemCalculator23;
				private readonly OperatorCalculatorBase _itemCalculator24;
				private readonly OperatorCalculatorBase _itemCalculator25;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_25Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
					OperatorCalculatorBase itemCalculator23,
					OperatorCalculatorBase itemCalculator24,
					OperatorCalculatorBase itemCalculator25,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
,					itemCalculator23
,					itemCalculator24
,					itemCalculator25
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
					_itemCalculator23 = itemCalculator23 ?? throw new NullException(() => itemCalculator23);
					_itemCalculator24 = itemCalculator24 ?? throw new NullException(() => itemCalculator24);
					_itemCalculator25 = itemCalculator25 ?? throw new NullException(() => itemCalculator25);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
+						_itemCalculator23.Calculate()
+						_itemCalculator24.Calculate()
+						_itemCalculator25.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_26Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
				private readonly OperatorCalculatorBase _itemCalculator23;
				private readonly OperatorCalculatorBase _itemCalculator24;
				private readonly OperatorCalculatorBase _itemCalculator25;
				private readonly OperatorCalculatorBase _itemCalculator26;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_26Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
					OperatorCalculatorBase itemCalculator23,
					OperatorCalculatorBase itemCalculator24,
					OperatorCalculatorBase itemCalculator25,
					OperatorCalculatorBase itemCalculator26,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
,					itemCalculator23
,					itemCalculator24
,					itemCalculator25
,					itemCalculator26
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
					_itemCalculator23 = itemCalculator23 ?? throw new NullException(() => itemCalculator23);
					_itemCalculator24 = itemCalculator24 ?? throw new NullException(() => itemCalculator24);
					_itemCalculator25 = itemCalculator25 ?? throw new NullException(() => itemCalculator25);
					_itemCalculator26 = itemCalculator26 ?? throw new NullException(() => itemCalculator26);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
+						_itemCalculator23.Calculate()
+						_itemCalculator24.Calculate()
+						_itemCalculator25.Calculate()
+						_itemCalculator26.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_27Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
				private readonly OperatorCalculatorBase _itemCalculator23;
				private readonly OperatorCalculatorBase _itemCalculator24;
				private readonly OperatorCalculatorBase _itemCalculator25;
				private readonly OperatorCalculatorBase _itemCalculator26;
				private readonly OperatorCalculatorBase _itemCalculator27;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_27Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
					OperatorCalculatorBase itemCalculator23,
					OperatorCalculatorBase itemCalculator24,
					OperatorCalculatorBase itemCalculator25,
					OperatorCalculatorBase itemCalculator26,
					OperatorCalculatorBase itemCalculator27,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
,					itemCalculator23
,					itemCalculator24
,					itemCalculator25
,					itemCalculator26
,					itemCalculator27
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
					_itemCalculator23 = itemCalculator23 ?? throw new NullException(() => itemCalculator23);
					_itemCalculator24 = itemCalculator24 ?? throw new NullException(() => itemCalculator24);
					_itemCalculator25 = itemCalculator25 ?? throw new NullException(() => itemCalculator25);
					_itemCalculator26 = itemCalculator26 ?? throw new NullException(() => itemCalculator26);
					_itemCalculator27 = itemCalculator27 ?? throw new NullException(() => itemCalculator27);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
+						_itemCalculator23.Calculate()
+						_itemCalculator24.Calculate()
+						_itemCalculator25.Calculate()
+						_itemCalculator26.Calculate()
+						_itemCalculator27.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_28Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
				private readonly OperatorCalculatorBase _itemCalculator23;
				private readonly OperatorCalculatorBase _itemCalculator24;
				private readonly OperatorCalculatorBase _itemCalculator25;
				private readonly OperatorCalculatorBase _itemCalculator26;
				private readonly OperatorCalculatorBase _itemCalculator27;
				private readonly OperatorCalculatorBase _itemCalculator28;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_28Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
					OperatorCalculatorBase itemCalculator23,
					OperatorCalculatorBase itemCalculator24,
					OperatorCalculatorBase itemCalculator25,
					OperatorCalculatorBase itemCalculator26,
					OperatorCalculatorBase itemCalculator27,
					OperatorCalculatorBase itemCalculator28,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
,					itemCalculator23
,					itemCalculator24
,					itemCalculator25
,					itemCalculator26
,					itemCalculator27
,					itemCalculator28
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
					_itemCalculator23 = itemCalculator23 ?? throw new NullException(() => itemCalculator23);
					_itemCalculator24 = itemCalculator24 ?? throw new NullException(() => itemCalculator24);
					_itemCalculator25 = itemCalculator25 ?? throw new NullException(() => itemCalculator25);
					_itemCalculator26 = itemCalculator26 ?? throw new NullException(() => itemCalculator26);
					_itemCalculator27 = itemCalculator27 ?? throw new NullException(() => itemCalculator27);
					_itemCalculator28 = itemCalculator28 ?? throw new NullException(() => itemCalculator28);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
+						_itemCalculator23.Calculate()
+						_itemCalculator24.Calculate()
+						_itemCalculator25.Calculate()
+						_itemCalculator26.Calculate()
+						_itemCalculator27.Calculate()
+						_itemCalculator28.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_29Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
				private readonly OperatorCalculatorBase _itemCalculator23;
				private readonly OperatorCalculatorBase _itemCalculator24;
				private readonly OperatorCalculatorBase _itemCalculator25;
				private readonly OperatorCalculatorBase _itemCalculator26;
				private readonly OperatorCalculatorBase _itemCalculator27;
				private readonly OperatorCalculatorBase _itemCalculator28;
				private readonly OperatorCalculatorBase _itemCalculator29;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_29Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
					OperatorCalculatorBase itemCalculator23,
					OperatorCalculatorBase itemCalculator24,
					OperatorCalculatorBase itemCalculator25,
					OperatorCalculatorBase itemCalculator26,
					OperatorCalculatorBase itemCalculator27,
					OperatorCalculatorBase itemCalculator28,
					OperatorCalculatorBase itemCalculator29,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
,					itemCalculator23
,					itemCalculator24
,					itemCalculator25
,					itemCalculator26
,					itemCalculator27
,					itemCalculator28
,					itemCalculator29
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
					_itemCalculator23 = itemCalculator23 ?? throw new NullException(() => itemCalculator23);
					_itemCalculator24 = itemCalculator24 ?? throw new NullException(() => itemCalculator24);
					_itemCalculator25 = itemCalculator25 ?? throw new NullException(() => itemCalculator25);
					_itemCalculator26 = itemCalculator26 ?? throw new NullException(() => itemCalculator26);
					_itemCalculator27 = itemCalculator27 ?? throw new NullException(() => itemCalculator27);
					_itemCalculator28 = itemCalculator28 ?? throw new NullException(() => itemCalculator28);
					_itemCalculator29 = itemCalculator29 ?? throw new NullException(() => itemCalculator29);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
+						_itemCalculator23.Calculate()
+						_itemCalculator24.Calculate()
+						_itemCalculator25.Calculate()
+						_itemCalculator26.Calculate()
+						_itemCalculator27.Calculate()
+						_itemCalculator28.Calculate()
+						_itemCalculator29.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_30Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
				private readonly OperatorCalculatorBase _itemCalculator23;
				private readonly OperatorCalculatorBase _itemCalculator24;
				private readonly OperatorCalculatorBase _itemCalculator25;
				private readonly OperatorCalculatorBase _itemCalculator26;
				private readonly OperatorCalculatorBase _itemCalculator27;
				private readonly OperatorCalculatorBase _itemCalculator28;
				private readonly OperatorCalculatorBase _itemCalculator29;
				private readonly OperatorCalculatorBase _itemCalculator30;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_30Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
					OperatorCalculatorBase itemCalculator23,
					OperatorCalculatorBase itemCalculator24,
					OperatorCalculatorBase itemCalculator25,
					OperatorCalculatorBase itemCalculator26,
					OperatorCalculatorBase itemCalculator27,
					OperatorCalculatorBase itemCalculator28,
					OperatorCalculatorBase itemCalculator29,
					OperatorCalculatorBase itemCalculator30,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
,					itemCalculator23
,					itemCalculator24
,					itemCalculator25
,					itemCalculator26
,					itemCalculator27
,					itemCalculator28
,					itemCalculator29
,					itemCalculator30
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
					_itemCalculator23 = itemCalculator23 ?? throw new NullException(() => itemCalculator23);
					_itemCalculator24 = itemCalculator24 ?? throw new NullException(() => itemCalculator24);
					_itemCalculator25 = itemCalculator25 ?? throw new NullException(() => itemCalculator25);
					_itemCalculator26 = itemCalculator26 ?? throw new NullException(() => itemCalculator26);
					_itemCalculator27 = itemCalculator27 ?? throw new NullException(() => itemCalculator27);
					_itemCalculator28 = itemCalculator28 ?? throw new NullException(() => itemCalculator28);
					_itemCalculator29 = itemCalculator29 ?? throw new NullException(() => itemCalculator29);
					_itemCalculator30 = itemCalculator30 ?? throw new NullException(() => itemCalculator30);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
+						_itemCalculator23.Calculate()
+						_itemCalculator24.Calculate()
+						_itemCalculator25.Calculate()
+						_itemCalculator26.Calculate()
+						_itemCalculator27.Calculate()
+						_itemCalculator28.Calculate()
+						_itemCalculator29.Calculate()
+						_itemCalculator30.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_31Vars_1Const : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
				private readonly OperatorCalculatorBase _itemCalculator9;
				private readonly OperatorCalculatorBase _itemCalculator10;
				private readonly OperatorCalculatorBase _itemCalculator11;
				private readonly OperatorCalculatorBase _itemCalculator12;
				private readonly OperatorCalculatorBase _itemCalculator13;
				private readonly OperatorCalculatorBase _itemCalculator14;
				private readonly OperatorCalculatorBase _itemCalculator15;
				private readonly OperatorCalculatorBase _itemCalculator16;
				private readonly OperatorCalculatorBase _itemCalculator17;
				private readonly OperatorCalculatorBase _itemCalculator18;
				private readonly OperatorCalculatorBase _itemCalculator19;
				private readonly OperatorCalculatorBase _itemCalculator20;
				private readonly OperatorCalculatorBase _itemCalculator21;
				private readonly OperatorCalculatorBase _itemCalculator22;
				private readonly OperatorCalculatorBase _itemCalculator23;
				private readonly OperatorCalculatorBase _itemCalculator24;
				private readonly OperatorCalculatorBase _itemCalculator25;
				private readonly OperatorCalculatorBase _itemCalculator26;
				private readonly OperatorCalculatorBase _itemCalculator27;
				private readonly OperatorCalculatorBase _itemCalculator28;
				private readonly OperatorCalculatorBase _itemCalculator29;
				private readonly OperatorCalculatorBase _itemCalculator30;
				private readonly OperatorCalculatorBase _itemCalculator31;
			private readonly double _constValue;
			
			public Add_OperatorCalculator_31Vars_1Const(
					OperatorCalculatorBase itemCalculator1,
					OperatorCalculatorBase itemCalculator2,
					OperatorCalculatorBase itemCalculator3,
					OperatorCalculatorBase itemCalculator4,
					OperatorCalculatorBase itemCalculator5,
					OperatorCalculatorBase itemCalculator6,
					OperatorCalculatorBase itemCalculator7,
					OperatorCalculatorBase itemCalculator8,
					OperatorCalculatorBase itemCalculator9,
					OperatorCalculatorBase itemCalculator10,
					OperatorCalculatorBase itemCalculator11,
					OperatorCalculatorBase itemCalculator12,
					OperatorCalculatorBase itemCalculator13,
					OperatorCalculatorBase itemCalculator14,
					OperatorCalculatorBase itemCalculator15,
					OperatorCalculatorBase itemCalculator16,
					OperatorCalculatorBase itemCalculator17,
					OperatorCalculatorBase itemCalculator18,
					OperatorCalculatorBase itemCalculator19,
					OperatorCalculatorBase itemCalculator20,
					OperatorCalculatorBase itemCalculator21,
					OperatorCalculatorBase itemCalculator22,
					OperatorCalculatorBase itemCalculator23,
					OperatorCalculatorBase itemCalculator24,
					OperatorCalculatorBase itemCalculator25,
					OperatorCalculatorBase itemCalculator26,
					OperatorCalculatorBase itemCalculator27,
					OperatorCalculatorBase itemCalculator28,
					OperatorCalculatorBase itemCalculator29,
					OperatorCalculatorBase itemCalculator30,
					OperatorCalculatorBase itemCalculator31,
				double constValue)
			: base(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
,					itemCalculator4
,					itemCalculator5
,					itemCalculator6
,					itemCalculator7
,					itemCalculator8
,					itemCalculator9
,					itemCalculator10
,					itemCalculator11
,					itemCalculator12
,					itemCalculator13
,					itemCalculator14
,					itemCalculator15
,					itemCalculator16
,					itemCalculator17
,					itemCalculator18
,					itemCalculator19
,					itemCalculator20
,					itemCalculator21
,					itemCalculator22
,					itemCalculator23
,					itemCalculator24
,					itemCalculator25
,					itemCalculator26
,					itemCalculator27
,					itemCalculator28
,					itemCalculator29
,					itemCalculator30
,					itemCalculator31
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
					_itemCalculator4 = itemCalculator4 ?? throw new NullException(() => itemCalculator4);
					_itemCalculator5 = itemCalculator5 ?? throw new NullException(() => itemCalculator5);
					_itemCalculator6 = itemCalculator6 ?? throw new NullException(() => itemCalculator6);
					_itemCalculator7 = itemCalculator7 ?? throw new NullException(() => itemCalculator7);
					_itemCalculator8 = itemCalculator8 ?? throw new NullException(() => itemCalculator8);
					_itemCalculator9 = itemCalculator9 ?? throw new NullException(() => itemCalculator9);
					_itemCalculator10 = itemCalculator10 ?? throw new NullException(() => itemCalculator10);
					_itemCalculator11 = itemCalculator11 ?? throw new NullException(() => itemCalculator11);
					_itemCalculator12 = itemCalculator12 ?? throw new NullException(() => itemCalculator12);
					_itemCalculator13 = itemCalculator13 ?? throw new NullException(() => itemCalculator13);
					_itemCalculator14 = itemCalculator14 ?? throw new NullException(() => itemCalculator14);
					_itemCalculator15 = itemCalculator15 ?? throw new NullException(() => itemCalculator15);
					_itemCalculator16 = itemCalculator16 ?? throw new NullException(() => itemCalculator16);
					_itemCalculator17 = itemCalculator17 ?? throw new NullException(() => itemCalculator17);
					_itemCalculator18 = itemCalculator18 ?? throw new NullException(() => itemCalculator18);
					_itemCalculator19 = itemCalculator19 ?? throw new NullException(() => itemCalculator19);
					_itemCalculator20 = itemCalculator20 ?? throw new NullException(() => itemCalculator20);
					_itemCalculator21 = itemCalculator21 ?? throw new NullException(() => itemCalculator21);
					_itemCalculator22 = itemCalculator22 ?? throw new NullException(() => itemCalculator22);
					_itemCalculator23 = itemCalculator23 ?? throw new NullException(() => itemCalculator23);
					_itemCalculator24 = itemCalculator24 ?? throw new NullException(() => itemCalculator24);
					_itemCalculator25 = itemCalculator25 ?? throw new NullException(() => itemCalculator25);
					_itemCalculator26 = itemCalculator26 ?? throw new NullException(() => itemCalculator26);
					_itemCalculator27 = itemCalculator27 ?? throw new NullException(() => itemCalculator27);
					_itemCalculator28 = itemCalculator28 ?? throw new NullException(() => itemCalculator28);
					_itemCalculator29 = itemCalculator29 ?? throw new NullException(() => itemCalculator29);
					_itemCalculator30 = itemCalculator30 ?? throw new NullException(() => itemCalculator30);
					_itemCalculator31 = itemCalculator31 ?? throw new NullException(() => itemCalculator31);
				_constValue = constValue;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue +
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
+						_itemCalculator8.Calculate()
+						_itemCalculator9.Calculate()
+						_itemCalculator10.Calculate()
+						_itemCalculator11.Calculate()
+						_itemCalculator12.Calculate()
+						_itemCalculator13.Calculate()
+						_itemCalculator14.Calculate()
+						_itemCalculator15.Calculate()
+						_itemCalculator16.Calculate()
+						_itemCalculator17.Calculate()
+						_itemCalculator18.Calculate()
+						_itemCalculator19.Calculate()
+						_itemCalculator20.Calculate()
+						_itemCalculator21.Calculate()
+						_itemCalculator22.Calculate()
+						_itemCalculator23.Calculate()
+						_itemCalculator24.Calculate()
+						_itemCalculator25.Calculate()
+						_itemCalculator26.Calculate()
+						_itemCalculator27.Calculate()
+						_itemCalculator28.Calculate()
+						_itemCalculator29.Calculate()
+						_itemCalculator30.Calculate()
+						_itemCalculator31.Calculate()
;			}
		}
}
*/