


using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{

		internal class Add_OperatorCalculator_2Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
			
			public Add_OperatorCalculator_2Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
)			: base
			(new [] { 
					itemCalculator1
,					itemCalculator2
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_3Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
			
			public Add_OperatorCalculator_3Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
)			: base
			(new [] { 
					itemCalculator1
,					itemCalculator2
,					itemCalculator3
			})
			{
					_itemCalculator1 = itemCalculator1 ?? throw new NullException(() => itemCalculator1);
					_itemCalculator2 = itemCalculator2 ?? throw new NullException(() => itemCalculator2);
					_itemCalculator3 = itemCalculator3 ?? throw new NullException(() => itemCalculator3);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_4Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
			
			public Add_OperatorCalculator_4Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_5Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
			
			public Add_OperatorCalculator_5Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_6Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
			
			public Add_OperatorCalculator_6Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_7Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
			
			public Add_OperatorCalculator_7Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_itemCalculator1.Calculate()
+						_itemCalculator2.Calculate()
+						_itemCalculator3.Calculate()
+						_itemCalculator4.Calculate()
+						_itemCalculator5.Calculate()
+						_itemCalculator6.Calculate()
+						_itemCalculator7.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_8Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _itemCalculator1;
				private readonly OperatorCalculatorBase _itemCalculator2;
				private readonly OperatorCalculatorBase _itemCalculator3;
				private readonly OperatorCalculatorBase _itemCalculator4;
				private readonly OperatorCalculatorBase _itemCalculator5;
				private readonly OperatorCalculatorBase _itemCalculator6;
				private readonly OperatorCalculatorBase _itemCalculator7;
				private readonly OperatorCalculatorBase _itemCalculator8;
			
			public Add_OperatorCalculator_8Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_9Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_9Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_10Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_10Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_11Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_11Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_12Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_12Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_13Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_13Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_14Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_14Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_15Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_15Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_16Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_16Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_17Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_17Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_18Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_18Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_19Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_19Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_20Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_20Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_21Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_21Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_22Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_22Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_23Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_23Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_24Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_24Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
,					OperatorCalculatorBase itemCalculator24
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_25Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_25Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
,					OperatorCalculatorBase itemCalculator24
,					OperatorCalculatorBase itemCalculator25
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_26Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_26Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
,					OperatorCalculatorBase itemCalculator24
,					OperatorCalculatorBase itemCalculator25
,					OperatorCalculatorBase itemCalculator26
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_27Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_27Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
,					OperatorCalculatorBase itemCalculator24
,					OperatorCalculatorBase itemCalculator25
,					OperatorCalculatorBase itemCalculator26
,					OperatorCalculatorBase itemCalculator27
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_28Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_28Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
,					OperatorCalculatorBase itemCalculator24
,					OperatorCalculatorBase itemCalculator25
,					OperatorCalculatorBase itemCalculator26
,					OperatorCalculatorBase itemCalculator27
,					OperatorCalculatorBase itemCalculator28
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_29Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_29Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
,					OperatorCalculatorBase itemCalculator24
,					OperatorCalculatorBase itemCalculator25
,					OperatorCalculatorBase itemCalculator26
,					OperatorCalculatorBase itemCalculator27
,					OperatorCalculatorBase itemCalculator28
,					OperatorCalculatorBase itemCalculator29
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_30Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_30Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
,					OperatorCalculatorBase itemCalculator24
,					OperatorCalculatorBase itemCalculator25
,					OperatorCalculatorBase itemCalculator26
,					OperatorCalculatorBase itemCalculator27
,					OperatorCalculatorBase itemCalculator28
,					OperatorCalculatorBase itemCalculator29
,					OperatorCalculatorBase itemCalculator30
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_31Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_31Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
,					OperatorCalculatorBase itemCalculator24
,					OperatorCalculatorBase itemCalculator25
,					OperatorCalculatorBase itemCalculator26
,					OperatorCalculatorBase itemCalculator27
,					OperatorCalculatorBase itemCalculator28
,					OperatorCalculatorBase itemCalculator29
,					OperatorCalculatorBase itemCalculator30
,					OperatorCalculatorBase itemCalculator31
)			: base
			(new [] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_32Vars : OperatorCalculatorBase_WithChildCalculators
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
				private readonly OperatorCalculatorBase _itemCalculator32;
			
			public Add_OperatorCalculator_32Vars(
					OperatorCalculatorBase itemCalculator1
,					OperatorCalculatorBase itemCalculator2
,					OperatorCalculatorBase itemCalculator3
,					OperatorCalculatorBase itemCalculator4
,					OperatorCalculatorBase itemCalculator5
,					OperatorCalculatorBase itemCalculator6
,					OperatorCalculatorBase itemCalculator7
,					OperatorCalculatorBase itemCalculator8
,					OperatorCalculatorBase itemCalculator9
,					OperatorCalculatorBase itemCalculator10
,					OperatorCalculatorBase itemCalculator11
,					OperatorCalculatorBase itemCalculator12
,					OperatorCalculatorBase itemCalculator13
,					OperatorCalculatorBase itemCalculator14
,					OperatorCalculatorBase itemCalculator15
,					OperatorCalculatorBase itemCalculator16
,					OperatorCalculatorBase itemCalculator17
,					OperatorCalculatorBase itemCalculator18
,					OperatorCalculatorBase itemCalculator19
,					OperatorCalculatorBase itemCalculator20
,					OperatorCalculatorBase itemCalculator21
,					OperatorCalculatorBase itemCalculator22
,					OperatorCalculatorBase itemCalculator23
,					OperatorCalculatorBase itemCalculator24
,					OperatorCalculatorBase itemCalculator25
,					OperatorCalculatorBase itemCalculator26
,					OperatorCalculatorBase itemCalculator27
,					OperatorCalculatorBase itemCalculator28
,					OperatorCalculatorBase itemCalculator29
,					OperatorCalculatorBase itemCalculator30
,					OperatorCalculatorBase itemCalculator31
,					OperatorCalculatorBase itemCalculator32
)			: base
			(new [] { 
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
,					itemCalculator32
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
					_itemCalculator32 = itemCalculator32 ?? throw new NullException(() => itemCalculator32);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
+						_itemCalculator32.Calculate()
;			}
		}
}
