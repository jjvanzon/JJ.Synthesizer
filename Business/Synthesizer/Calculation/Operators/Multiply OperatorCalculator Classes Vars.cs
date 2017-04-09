


using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{

		internal class Multiply_OperatorCalculator_2Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
			
			public Multiply_OperatorCalculator_2Vars(
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
*						_operandCalculator2.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_3Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
			
			public Multiply_OperatorCalculator_3Vars(
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
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_4Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
			
			public Multiply_OperatorCalculator_4Vars(
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
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_5Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
			
			public Multiply_OperatorCalculator_5Vars(
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
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_6Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
			
			public Multiply_OperatorCalculator_6Vars(
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
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_7Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
			
			public Multiply_OperatorCalculator_7Vars(
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
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_8Vars : OperatorCalculatorBase_WithChildCalculators
		{
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
			
			public Multiply_OperatorCalculator_8Vars(
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
*						_operandCalculator2.Calculate()
*						_operandCalculator3.Calculate()
*						_operandCalculator4.Calculate()
*						_operandCalculator5.Calculate()
*						_operandCalculator6.Calculate()
*						_operandCalculator7.Calculate()
*						_operandCalculator8.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_9Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Multiply_OperatorCalculator_9Vars(
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
		internal class Multiply_OperatorCalculator_10Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Multiply_OperatorCalculator_10Vars(
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
		internal class Multiply_OperatorCalculator_11Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Multiply_OperatorCalculator_11Vars(
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
		internal class Multiply_OperatorCalculator_12Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Multiply_OperatorCalculator_12Vars(
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
		internal class Multiply_OperatorCalculator_13Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Multiply_OperatorCalculator_13Vars(
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
		internal class Multiply_OperatorCalculator_14Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Multiply_OperatorCalculator_14Vars(
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
		internal class Multiply_OperatorCalculator_15Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Multiply_OperatorCalculator_15Vars(
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
		internal class Multiply_OperatorCalculator_16Vars : OperatorCalculatorBase_WithChildCalculators
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
			
			public Multiply_OperatorCalculator_16Vars(
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
*						_operandCalculator16.Calculate()
;			}
		}
}
