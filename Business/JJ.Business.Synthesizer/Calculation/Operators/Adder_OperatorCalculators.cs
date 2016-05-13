


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{


		internal class Adder_OperatorCalculator3 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;

			public Adder_OperatorCalculator3(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
)			: base
			(new OperatorCalculatorBase[] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
;
			}
		}

		internal class Adder_OperatorCalculator4 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;

			public Adder_OperatorCalculator4(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
)			: base
			(new OperatorCalculatorBase[] { 
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
			}
			 
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override double Calculate()
			{
				return 
						_operandCalculator1.Calculate()
+						_operandCalculator2.Calculate()
+						_operandCalculator3.Calculate()
+						_operandCalculator4.Calculate()
;
			}
		}

		internal class Adder_OperatorCalculator5 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;

			public Adder_OperatorCalculator5(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
)			: base
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator6 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;

			public Adder_OperatorCalculator6(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
)			: base
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator7 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;

			public Adder_OperatorCalculator7(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
)			: base
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator8 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;

			public Adder_OperatorCalculator8(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
,					OperatorCalculatorBase operandCalculator4
,					OperatorCalculatorBase operandCalculator5
,					OperatorCalculatorBase operandCalculator6
,					OperatorCalculatorBase operandCalculator7
,					OperatorCalculatorBase operandCalculator8
)			: base
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator9 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;

			public Adder_OperatorCalculator9(
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
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator10 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;

			public Adder_OperatorCalculator10(
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
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator11 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;

			public Adder_OperatorCalculator11(
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
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator12 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;

			public Adder_OperatorCalculator12(
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
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator13 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;

			public Adder_OperatorCalculator13(
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
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator14 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;

			public Adder_OperatorCalculator14(
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
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator15 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;

			public Adder_OperatorCalculator15(
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
			(new OperatorCalculatorBase[] { 
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
;
			}
		}

		internal class Adder_OperatorCalculator16 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;

			public Adder_OperatorCalculator16(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
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
;
			}
		}

		internal class Adder_OperatorCalculator17 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;

			public Adder_OperatorCalculator17(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
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
;
			}
		}

		internal class Adder_OperatorCalculator18 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;

			public Adder_OperatorCalculator18(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
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
;
			}
		}

		internal class Adder_OperatorCalculator19 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;

			public Adder_OperatorCalculator19(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
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
;
			}
		}

		internal class Adder_OperatorCalculator20 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;

			public Adder_OperatorCalculator20(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
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
;
			}
		}

		internal class Adder_OperatorCalculator21 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;

			public Adder_OperatorCalculator21(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
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
;
			}
		}

		internal class Adder_OperatorCalculator22 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;

			public Adder_OperatorCalculator22(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
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
;
			}
		}

		internal class Adder_OperatorCalculator23 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;

			public Adder_OperatorCalculator23(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
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
;
			}
		}

		internal class Adder_OperatorCalculator24 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;
				private OperatorCalculatorBase _operandCalculator24;

			public Adder_OperatorCalculator24(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
					if (operandCalculator24 == null) throw new NullException(() => operandCalculator24);
					_operandCalculator24 = operandCalculator24;
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
;
			}
		}

		internal class Adder_OperatorCalculator25 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;
				private OperatorCalculatorBase _operandCalculator24;
				private OperatorCalculatorBase _operandCalculator25;

			public Adder_OperatorCalculator25(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
					if (operandCalculator24 == null) throw new NullException(() => operandCalculator24);
					_operandCalculator24 = operandCalculator24;
					if (operandCalculator25 == null) throw new NullException(() => operandCalculator25);
					_operandCalculator25 = operandCalculator25;
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
;
			}
		}

		internal class Adder_OperatorCalculator26 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;
				private OperatorCalculatorBase _operandCalculator24;
				private OperatorCalculatorBase _operandCalculator25;
				private OperatorCalculatorBase _operandCalculator26;

			public Adder_OperatorCalculator26(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
					if (operandCalculator24 == null) throw new NullException(() => operandCalculator24);
					_operandCalculator24 = operandCalculator24;
					if (operandCalculator25 == null) throw new NullException(() => operandCalculator25);
					_operandCalculator25 = operandCalculator25;
					if (operandCalculator26 == null) throw new NullException(() => operandCalculator26);
					_operandCalculator26 = operandCalculator26;
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
;
			}
		}

		internal class Adder_OperatorCalculator27 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;
				private OperatorCalculatorBase _operandCalculator24;
				private OperatorCalculatorBase _operandCalculator25;
				private OperatorCalculatorBase _operandCalculator26;
				private OperatorCalculatorBase _operandCalculator27;

			public Adder_OperatorCalculator27(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
					if (operandCalculator24 == null) throw new NullException(() => operandCalculator24);
					_operandCalculator24 = operandCalculator24;
					if (operandCalculator25 == null) throw new NullException(() => operandCalculator25);
					_operandCalculator25 = operandCalculator25;
					if (operandCalculator26 == null) throw new NullException(() => operandCalculator26);
					_operandCalculator26 = operandCalculator26;
					if (operandCalculator27 == null) throw new NullException(() => operandCalculator27);
					_operandCalculator27 = operandCalculator27;
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
;
			}
		}

		internal class Adder_OperatorCalculator28 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;
				private OperatorCalculatorBase _operandCalculator24;
				private OperatorCalculatorBase _operandCalculator25;
				private OperatorCalculatorBase _operandCalculator26;
				private OperatorCalculatorBase _operandCalculator27;
				private OperatorCalculatorBase _operandCalculator28;

			public Adder_OperatorCalculator28(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
					if (operandCalculator24 == null) throw new NullException(() => operandCalculator24);
					_operandCalculator24 = operandCalculator24;
					if (operandCalculator25 == null) throw new NullException(() => operandCalculator25);
					_operandCalculator25 = operandCalculator25;
					if (operandCalculator26 == null) throw new NullException(() => operandCalculator26);
					_operandCalculator26 = operandCalculator26;
					if (operandCalculator27 == null) throw new NullException(() => operandCalculator27);
					_operandCalculator27 = operandCalculator27;
					if (operandCalculator28 == null) throw new NullException(() => operandCalculator28);
					_operandCalculator28 = operandCalculator28;
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
;
			}
		}

		internal class Adder_OperatorCalculator29 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;
				private OperatorCalculatorBase _operandCalculator24;
				private OperatorCalculatorBase _operandCalculator25;
				private OperatorCalculatorBase _operandCalculator26;
				private OperatorCalculatorBase _operandCalculator27;
				private OperatorCalculatorBase _operandCalculator28;
				private OperatorCalculatorBase _operandCalculator29;

			public Adder_OperatorCalculator29(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
					if (operandCalculator24 == null) throw new NullException(() => operandCalculator24);
					_operandCalculator24 = operandCalculator24;
					if (operandCalculator25 == null) throw new NullException(() => operandCalculator25);
					_operandCalculator25 = operandCalculator25;
					if (operandCalculator26 == null) throw new NullException(() => operandCalculator26);
					_operandCalculator26 = operandCalculator26;
					if (operandCalculator27 == null) throw new NullException(() => operandCalculator27);
					_operandCalculator27 = operandCalculator27;
					if (operandCalculator28 == null) throw new NullException(() => operandCalculator28);
					_operandCalculator28 = operandCalculator28;
					if (operandCalculator29 == null) throw new NullException(() => operandCalculator29);
					_operandCalculator29 = operandCalculator29;
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
;
			}
		}

		internal class Adder_OperatorCalculator30 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;
				private OperatorCalculatorBase _operandCalculator24;
				private OperatorCalculatorBase _operandCalculator25;
				private OperatorCalculatorBase _operandCalculator26;
				private OperatorCalculatorBase _operandCalculator27;
				private OperatorCalculatorBase _operandCalculator28;
				private OperatorCalculatorBase _operandCalculator29;
				private OperatorCalculatorBase _operandCalculator30;

			public Adder_OperatorCalculator30(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
					if (operandCalculator24 == null) throw new NullException(() => operandCalculator24);
					_operandCalculator24 = operandCalculator24;
					if (operandCalculator25 == null) throw new NullException(() => operandCalculator25);
					_operandCalculator25 = operandCalculator25;
					if (operandCalculator26 == null) throw new NullException(() => operandCalculator26);
					_operandCalculator26 = operandCalculator26;
					if (operandCalculator27 == null) throw new NullException(() => operandCalculator27);
					_operandCalculator27 = operandCalculator27;
					if (operandCalculator28 == null) throw new NullException(() => operandCalculator28);
					_operandCalculator28 = operandCalculator28;
					if (operandCalculator29 == null) throw new NullException(() => operandCalculator29);
					_operandCalculator29 = operandCalculator29;
					if (operandCalculator30 == null) throw new NullException(() => operandCalculator30);
					_operandCalculator30 = operandCalculator30;
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
;
			}
		}

		internal class Adder_OperatorCalculator31 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;
				private OperatorCalculatorBase _operandCalculator24;
				private OperatorCalculatorBase _operandCalculator25;
				private OperatorCalculatorBase _operandCalculator26;
				private OperatorCalculatorBase _operandCalculator27;
				private OperatorCalculatorBase _operandCalculator28;
				private OperatorCalculatorBase _operandCalculator29;
				private OperatorCalculatorBase _operandCalculator30;
				private OperatorCalculatorBase _operandCalculator31;

			public Adder_OperatorCalculator31(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
					if (operandCalculator24 == null) throw new NullException(() => operandCalculator24);
					_operandCalculator24 = operandCalculator24;
					if (operandCalculator25 == null) throw new NullException(() => operandCalculator25);
					_operandCalculator25 = operandCalculator25;
					if (operandCalculator26 == null) throw new NullException(() => operandCalculator26);
					_operandCalculator26 = operandCalculator26;
					if (operandCalculator27 == null) throw new NullException(() => operandCalculator27);
					_operandCalculator27 = operandCalculator27;
					if (operandCalculator28 == null) throw new NullException(() => operandCalculator28);
					_operandCalculator28 = operandCalculator28;
					if (operandCalculator29 == null) throw new NullException(() => operandCalculator29);
					_operandCalculator29 = operandCalculator29;
					if (operandCalculator30 == null) throw new NullException(() => operandCalculator30);
					_operandCalculator30 = operandCalculator30;
					if (operandCalculator31 == null) throw new NullException(() => operandCalculator31);
					_operandCalculator31 = operandCalculator31;
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
;
			}
		}

		internal class Adder_OperatorCalculator32 : OperatorCalculatorBase_WithChildCalculators
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;
				private OperatorCalculatorBase _operandCalculator4;
				private OperatorCalculatorBase _operandCalculator5;
				private OperatorCalculatorBase _operandCalculator6;
				private OperatorCalculatorBase _operandCalculator7;
				private OperatorCalculatorBase _operandCalculator8;
				private OperatorCalculatorBase _operandCalculator9;
				private OperatorCalculatorBase _operandCalculator10;
				private OperatorCalculatorBase _operandCalculator11;
				private OperatorCalculatorBase _operandCalculator12;
				private OperatorCalculatorBase _operandCalculator13;
				private OperatorCalculatorBase _operandCalculator14;
				private OperatorCalculatorBase _operandCalculator15;
				private OperatorCalculatorBase _operandCalculator16;
				private OperatorCalculatorBase _operandCalculator17;
				private OperatorCalculatorBase _operandCalculator18;
				private OperatorCalculatorBase _operandCalculator19;
				private OperatorCalculatorBase _operandCalculator20;
				private OperatorCalculatorBase _operandCalculator21;
				private OperatorCalculatorBase _operandCalculator22;
				private OperatorCalculatorBase _operandCalculator23;
				private OperatorCalculatorBase _operandCalculator24;
				private OperatorCalculatorBase _operandCalculator25;
				private OperatorCalculatorBase _operandCalculator26;
				private OperatorCalculatorBase _operandCalculator27;
				private OperatorCalculatorBase _operandCalculator28;
				private OperatorCalculatorBase _operandCalculator29;
				private OperatorCalculatorBase _operandCalculator30;
				private OperatorCalculatorBase _operandCalculator31;
				private OperatorCalculatorBase _operandCalculator32;

			public Adder_OperatorCalculator32(
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
			(new OperatorCalculatorBase[] { 
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
					if (operandCalculator16 == null) throw new NullException(() => operandCalculator16);
					_operandCalculator16 = operandCalculator16;
					if (operandCalculator17 == null) throw new NullException(() => operandCalculator17);
					_operandCalculator17 = operandCalculator17;
					if (operandCalculator18 == null) throw new NullException(() => operandCalculator18);
					_operandCalculator18 = operandCalculator18;
					if (operandCalculator19 == null) throw new NullException(() => operandCalculator19);
					_operandCalculator19 = operandCalculator19;
					if (operandCalculator20 == null) throw new NullException(() => operandCalculator20);
					_operandCalculator20 = operandCalculator20;
					if (operandCalculator21 == null) throw new NullException(() => operandCalculator21);
					_operandCalculator21 = operandCalculator21;
					if (operandCalculator22 == null) throw new NullException(() => operandCalculator22);
					_operandCalculator22 = operandCalculator22;
					if (operandCalculator23 == null) throw new NullException(() => operandCalculator23);
					_operandCalculator23 = operandCalculator23;
					if (operandCalculator24 == null) throw new NullException(() => operandCalculator24);
					_operandCalculator24 = operandCalculator24;
					if (operandCalculator25 == null) throw new NullException(() => operandCalculator25);
					_operandCalculator25 = operandCalculator25;
					if (operandCalculator26 == null) throw new NullException(() => operandCalculator26);
					_operandCalculator26 = operandCalculator26;
					if (operandCalculator27 == null) throw new NullException(() => operandCalculator27);
					_operandCalculator27 = operandCalculator27;
					if (operandCalculator28 == null) throw new NullException(() => operandCalculator28);
					_operandCalculator28 = operandCalculator28;
					if (operandCalculator29 == null) throw new NullException(() => operandCalculator29);
					_operandCalculator29 = operandCalculator29;
					if (operandCalculator30 == null) throw new NullException(() => operandCalculator30);
					_operandCalculator30 = operandCalculator30;
					if (operandCalculator31 == null) throw new NullException(() => operandCalculator31);
					_operandCalculator31 = operandCalculator31;
					if (operandCalculator32 == null) throw new NullException(() => operandCalculator32);
					_operandCalculator32 = operandCalculator32;
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
;
			}
		}
}
