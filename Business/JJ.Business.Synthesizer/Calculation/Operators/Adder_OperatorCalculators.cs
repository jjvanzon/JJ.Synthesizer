

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{


		internal class Adder_OperatorCalculator3 : OperatorCalculatorBase
		{
				private OperatorCalculatorBase _operandCalculator1;
				private OperatorCalculatorBase _operandCalculator2;
				private OperatorCalculatorBase _operandCalculator3;

			public Adder_OperatorCalculator3(
					OperatorCalculatorBase operandCalculator1
,					OperatorCalculatorBase operandCalculator2
,					OperatorCalculatorBase operandCalculator3
)			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
			}

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator4 : OperatorCalculatorBase
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
)			{
					if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
					_operandCalculator1 = operandCalculator1;
					if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
					_operandCalculator2 = operandCalculator2;
					if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);
					_operandCalculator3 = operandCalculator3;
					if (operandCalculator4 == null) throw new NullException(() => operandCalculator4);
					_operandCalculator4 = operandCalculator4;
			}

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator5 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator6 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator7 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator8 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator9 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator10 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator11 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator12 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator13 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator14 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator15 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator16 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator17 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator18 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator19 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator20 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator21 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator22 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator23 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator24 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
+						_operandCalculator24.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator25 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
+						_operandCalculator24.Calculate(time, channelIndex)
+						_operandCalculator25.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator26 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
+						_operandCalculator24.Calculate(time, channelIndex)
+						_operandCalculator25.Calculate(time, channelIndex)
+						_operandCalculator26.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator27 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
+						_operandCalculator24.Calculate(time, channelIndex)
+						_operandCalculator25.Calculate(time, channelIndex)
+						_operandCalculator26.Calculate(time, channelIndex)
+						_operandCalculator27.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator28 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
+						_operandCalculator24.Calculate(time, channelIndex)
+						_operandCalculator25.Calculate(time, channelIndex)
+						_operandCalculator26.Calculate(time, channelIndex)
+						_operandCalculator27.Calculate(time, channelIndex)
+						_operandCalculator28.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator29 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
+						_operandCalculator24.Calculate(time, channelIndex)
+						_operandCalculator25.Calculate(time, channelIndex)
+						_operandCalculator26.Calculate(time, channelIndex)
+						_operandCalculator27.Calculate(time, channelIndex)
+						_operandCalculator28.Calculate(time, channelIndex)
+						_operandCalculator29.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator30 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
+						_operandCalculator24.Calculate(time, channelIndex)
+						_operandCalculator25.Calculate(time, channelIndex)
+						_operandCalculator26.Calculate(time, channelIndex)
+						_operandCalculator27.Calculate(time, channelIndex)
+						_operandCalculator28.Calculate(time, channelIndex)
+						_operandCalculator29.Calculate(time, channelIndex)
+						_operandCalculator30.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator31 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
+						_operandCalculator24.Calculate(time, channelIndex)
+						_operandCalculator25.Calculate(time, channelIndex)
+						_operandCalculator26.Calculate(time, channelIndex)
+						_operandCalculator27.Calculate(time, channelIndex)
+						_operandCalculator28.Calculate(time, channelIndex)
+						_operandCalculator29.Calculate(time, channelIndex)
+						_operandCalculator30.Calculate(time, channelIndex)
+						_operandCalculator31.Calculate(time, channelIndex)
;
			}
		}

		internal class Adder_OperatorCalculator32 : OperatorCalculatorBase
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
)			{
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

			public override double Calculate(double time, int channelIndex)
			{
				return 
						_operandCalculator1.Calculate(time, channelIndex)
+						_operandCalculator2.Calculate(time, channelIndex)
+						_operandCalculator3.Calculate(time, channelIndex)
+						_operandCalculator4.Calculate(time, channelIndex)
+						_operandCalculator5.Calculate(time, channelIndex)
+						_operandCalculator6.Calculate(time, channelIndex)
+						_operandCalculator7.Calculate(time, channelIndex)
+						_operandCalculator8.Calculate(time, channelIndex)
+						_operandCalculator9.Calculate(time, channelIndex)
+						_operandCalculator10.Calculate(time, channelIndex)
+						_operandCalculator11.Calculate(time, channelIndex)
+						_operandCalculator12.Calculate(time, channelIndex)
+						_operandCalculator13.Calculate(time, channelIndex)
+						_operandCalculator14.Calculate(time, channelIndex)
+						_operandCalculator15.Calculate(time, channelIndex)
+						_operandCalculator16.Calculate(time, channelIndex)
+						_operandCalculator17.Calculate(time, channelIndex)
+						_operandCalculator18.Calculate(time, channelIndex)
+						_operandCalculator19.Calculate(time, channelIndex)
+						_operandCalculator20.Calculate(time, channelIndex)
+						_operandCalculator21.Calculate(time, channelIndex)
+						_operandCalculator22.Calculate(time, channelIndex)
+						_operandCalculator23.Calculate(time, channelIndex)
+						_operandCalculator24.Calculate(time, channelIndex)
+						_operandCalculator25.Calculate(time, channelIndex)
+						_operandCalculator26.Calculate(time, channelIndex)
+						_operandCalculator27.Calculate(time, channelIndex)
+						_operandCalculator28.Calculate(time, channelIndex)
+						_operandCalculator29.Calculate(time, channelIndex)
+						_operandCalculator30.Calculate(time, channelIndex)
+						_operandCalculator31.Calculate(time, channelIndex)
+						_operandCalculator32.Calculate(time, channelIndex)
;
			}
		}
}
