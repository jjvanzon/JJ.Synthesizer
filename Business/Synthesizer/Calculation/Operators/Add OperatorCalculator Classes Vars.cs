


using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{

		internal class Add_OperatorCalculator_2Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
			
			public Add_OperatorCalculator_2Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
)			: base
			(new[] { 
					operandCalculator1
,					operandCalculator2
			})
			{
			    _operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
			    _operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_3Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
			
			public Add_OperatorCalculator_3Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
)			: base
			(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
			})
			{
			    _operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
			    _operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
			    _operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_4Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
			
			public Add_OperatorCalculator_4Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
)			: base
			(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
			})
			{
			    _operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
			    _operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
			    _operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
			    _operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_5Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
			
			public Add_OperatorCalculator_5Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
)			: base
			(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
			})
			{
			    _operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
			    _operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
			    _operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
			    _operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
			    _operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_6Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
			
			public Add_OperatorCalculator_6Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
)			: base
			(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
			})
			{
			    _operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
			    _operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
			    _operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
			    _operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
			    _operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
			    _operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_7Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
			
			public Add_OperatorCalculator_7Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
)			: base
			(new[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
			})
			{
			    _operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
			    _operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
			    _operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
			    _operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
			    _operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
			    _operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
			    _operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
+						_operandCalculator5.Calculate()
+						_operandCalculator6.Calculate()
+						_operandCalculator7.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_8Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
			
			public Add_OperatorCalculator_8Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
)			: base
			(new[] { 
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
			    _operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
			    _operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
			    _operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
			    _operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
			    _operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
			    _operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
			    _operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
			    _operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_9Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_9Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
)			: base
			(new[] { 
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
			    _operandCalculator1 = operandCalculator1 ?? throw new NullException(() => operandCalculator1);
			    _operandCalculator2 = operandCalculator2 ?? throw new NullException(() => operandCalculator2);
			    _operandCalculator3 = operandCalculator3 ?? throw new NullException(() => operandCalculator3);
			    _operandCalculator4 = operandCalculator4 ?? throw new NullException(() => operandCalculator4);
			    _operandCalculator5 = operandCalculator5 ?? throw new NullException(() => operandCalculator5);
			    _operandCalculator6 = operandCalculator6 ?? throw new NullException(() => operandCalculator6);
			    _operandCalculator7 = operandCalculator7 ?? throw new NullException(() => operandCalculator7);
			    _operandCalculator8 = operandCalculator8 ?? throw new NullException(() => operandCalculator8);
			    _operandCalculator9 = operandCalculator9 ?? throw new NullException(() => operandCalculator9);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_10Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_10Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
)			: base
			(new[] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_11Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_11Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
)			: base
			(new[] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_12Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_12Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
)			: base
			(new[] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_13Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_13Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
)			: base
			(new[] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_14Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_14Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
)			: base
			(new[] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_15Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_15Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
)			: base
			(new[] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_16Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_16Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
)			: base
			(new[] { 
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
,					operandCalculator16
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_17Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_17Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_18Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_18Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_19Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_19Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_20Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_20Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_21Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_21Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_22Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_22Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_23Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_23Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_24Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_24Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
,					OperatorCalculatorBase operandCalculator24
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
,					operandCalculator24
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_25Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_25Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
,					OperatorCalculatorBase operandCalculator24
,					OperatorCalculatorBase operandCalculator25
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
,					operandCalculator24
,					operandCalculator25
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_26Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_26Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
,					OperatorCalculatorBase operandCalculator24
,					OperatorCalculatorBase operandCalculator25
,					OperatorCalculatorBase operandCalculator26
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
,					operandCalculator24
,					operandCalculator25
,					operandCalculator26
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_27Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_27Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
,					OperatorCalculatorBase operandCalculator24
,					OperatorCalculatorBase operandCalculator25
,					OperatorCalculatorBase operandCalculator26
,					OperatorCalculatorBase operandCalculator27
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
,					operandCalculator24
,					operandCalculator25
,					operandCalculator26
,					operandCalculator27
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_28Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_28Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
,					OperatorCalculatorBase operandCalculator24
,					OperatorCalculatorBase operandCalculator25
,					OperatorCalculatorBase operandCalculator26
,					OperatorCalculatorBase operandCalculator27
,					OperatorCalculatorBase operandCalculator28
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
,					operandCalculator24
,					operandCalculator25
,					operandCalculator26
,					operandCalculator27
,					operandCalculator28
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_29Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_29Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
,					OperatorCalculatorBase operandCalculator24
,					OperatorCalculatorBase operandCalculator25
,					OperatorCalculatorBase operandCalculator26
,					OperatorCalculatorBase operandCalculator27
,					OperatorCalculatorBase operandCalculator28
,					OperatorCalculatorBase operandCalculator29
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
,					operandCalculator24
,					operandCalculator25
,					operandCalculator26
,					operandCalculator27
,					operandCalculator28
,					operandCalculator29
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_30Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_30Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
,					OperatorCalculatorBase operandCalculator24
,					OperatorCalculatorBase operandCalculator25
,					OperatorCalculatorBase operandCalculator26
,					OperatorCalculatorBase operandCalculator27
,					OperatorCalculatorBase operandCalculator28
,					OperatorCalculatorBase operandCalculator29
,					OperatorCalculatorBase operandCalculator30
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
,					operandCalculator24
,					operandCalculator25
,					operandCalculator26
,					operandCalculator27
,					operandCalculator28
,					operandCalculator29
,					operandCalculator30
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_31Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Add_OperatorCalculator_31Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
,					OperatorCalculatorBase operandCalculator24
,					OperatorCalculatorBase operandCalculator25
,					OperatorCalculatorBase operandCalculator26
,					OperatorCalculatorBase operandCalculator27
,					OperatorCalculatorBase operandCalculator28
,					OperatorCalculatorBase operandCalculator29
,					OperatorCalculatorBase operandCalculator30
,					OperatorCalculatorBase operandCalculator31
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
,					operandCalculator24
,					operandCalculator25
,					operandCalculator26
,					operandCalculator27
,					operandCalculator28
,					operandCalculator29
,					operandCalculator30
,					operandCalculator31
			})
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
		internal class Add_OperatorCalculator_32Vars : OperatorCalculatorBase_WithChildCalculators
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
				private readonly OperatorCalculatorBase _operandCalculator32;
			
			public Add_OperatorCalculator_32Vars(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
,					OperatorCalculatorBase operandCalculator10
,					OperatorCalculatorBase operandCalculator11
,					OperatorCalculatorBase operandCalculator12
,					OperatorCalculatorBase operandCalculator13
,					OperatorCalculatorBase operandCalculator14
,					OperatorCalculatorBase operandCalculator15
,					OperatorCalculatorBase operandCalculator16
,					OperatorCalculatorBase operandCalculator17
,					OperatorCalculatorBase operandCalculator18
,					OperatorCalculatorBase operandCalculator19
,					OperatorCalculatorBase operandCalculator20
,					OperatorCalculatorBase operandCalculator21
,					OperatorCalculatorBase operandCalculator22
,					OperatorCalculatorBase operandCalculator23
,					OperatorCalculatorBase operandCalculator24
,					OperatorCalculatorBase operandCalculator25
,					OperatorCalculatorBase operandCalculator26
,					OperatorCalculatorBase operandCalculator27
,					OperatorCalculatorBase operandCalculator28
,					OperatorCalculatorBase operandCalculator29
,					OperatorCalculatorBase operandCalculator30
,					OperatorCalculatorBase operandCalculator31
,					OperatorCalculatorBase operandCalculator32
)			: base
			(new[] { 
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
,					operandCalculator16
,					operandCalculator17
,					operandCalculator18
,					operandCalculator19
,					operandCalculator20
,					operandCalculator21
,					operandCalculator22
,					operandCalculator23
,					operandCalculator24
,					operandCalculator25
,					operandCalculator26
,					operandCalculator27
,					operandCalculator28
,					operandCalculator29
,					operandCalculator30
,					operandCalculator31
,					operandCalculator32
			})
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
			    _operandCalculator32 = operandCalculator32 ?? throw new NullException(() => operandCalculator32);
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
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
+						_operandCalculator32.Calculate()
;			}
		}
}
