


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{

		internal class Multiply_OperatorCalculator_1Const_1Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
				private readonly OperatorCalculatorBase _operandCalculator1;
			
			public Multiply_OperatorCalculator_1Const_1Var(
				double constValue,
					OperatorCalculatorBase operandCalculator1
)			: base(new OperatorCalculatorBase[] { 
					operandCalculator1
			})
			{
				_constValue = constValue;
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_1Const_2Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
			
			public Multiply_OperatorCalculator_1Const_2Var(
				double constValue,
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
)			: base(new OperatorCalculatorBase[] { 
					operandCalculator1
,					operandCalculator2
			})
			{
				_constValue = constValue;
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return _constValue *
						_operandCalculator1.Calculate()
*						_operandCalculator2.Calculate()
;			}
		}
		internal class Multiply_OperatorCalculator_1Const_3Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
			
			public Multiply_OperatorCalculator_1Const_3Var(
				double constValue,
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
)			: base(new OperatorCalculatorBase[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
			})
			{
				_constValue = constValue;
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
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
		internal class Multiply_OperatorCalculator_1Const_4Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
			
			public Multiply_OperatorCalculator_1Const_4Var(
				double constValue,
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
)			: base(new OperatorCalculatorBase[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
			})
			{
				_constValue = constValue;
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
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
		internal class Multiply_OperatorCalculator_1Const_5Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
			
			public Multiply_OperatorCalculator_1Const_5Var(
				double constValue,
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
)			: base(new OperatorCalculatorBase[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
			})
			{
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_6Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
			
			public Multiply_OperatorCalculator_1Const_6Var(
				double constValue,
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
)			: base(new OperatorCalculatorBase[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
			})
			{
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_7Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
			
			public Multiply_OperatorCalculator_1Const_7Var(
				double constValue,
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
)			: base(new OperatorCalculatorBase[] { 
					operandCalculator1
,					operandCalculator2
,					operandCalculator3
,					operandCalculator4
,					operandCalculator5
,					operandCalculator6
,					operandCalculator7
			})
			{
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_8Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
			
			public Multiply_OperatorCalculator_1Const_8Var(
				double constValue,
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
)			: base(new OperatorCalculatorBase[] { 
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
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_9Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
				private readonly OperatorCalculatorBase _operandCalculator1;
				private readonly OperatorCalculatorBase _operandCalculator2;
				private readonly OperatorCalculatorBase _operandCalculator3;
				private readonly OperatorCalculatorBase _operandCalculator4;
				private readonly OperatorCalculatorBase _operandCalculator5;
				private readonly OperatorCalculatorBase _operandCalculator6;
				private readonly OperatorCalculatorBase _operandCalculator7;
				private readonly OperatorCalculatorBase _operandCalculator8;
				private readonly OperatorCalculatorBase _operandCalculator9;
			
			public Multiply_OperatorCalculator_1Const_9Var(
				double constValue,
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
,					OperatorCalculatorBase operandCalculator9
)			: base(new OperatorCalculatorBase[] { 
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
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_10Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
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
			
			public Multiply_OperatorCalculator_1Const_10Var(
				double constValue,
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
)			: base(new OperatorCalculatorBase[] { 
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
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_11Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
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
			
			public Multiply_OperatorCalculator_1Const_11Var(
				double constValue,
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
)			: base(new OperatorCalculatorBase[] { 
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
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_12Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
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
			
			public Multiply_OperatorCalculator_1Const_12Var(
				double constValue,
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
)			: base(new OperatorCalculatorBase[] { 
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
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_13Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
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
			
			public Multiply_OperatorCalculator_1Const_13Var(
				double constValue,
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
)			: base(new OperatorCalculatorBase[] { 
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
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_14Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
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
			
			public Multiply_OperatorCalculator_1Const_14Var(
				double constValue,
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
)			: base(new OperatorCalculatorBase[] { 
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
				_constValue = constValue;
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
		internal class Multiply_OperatorCalculator_1Const_15Var : OperatorCalculatorBase_WithChildCalculators
		{
			private readonly double _constValue;
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
			
			public Multiply_OperatorCalculator_1Const_15Var(
				double constValue,
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
)			: base(new OperatorCalculatorBase[] { 
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
				_constValue = constValue;
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
