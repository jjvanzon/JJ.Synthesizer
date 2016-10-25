


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
{

		internal class Add_OperatorCalculator_2Vars
			<
					TChildCalculator1
,					TChildCalculator2
			>
			: IOperatorCalculator_2Vars, IOperatorCalculator_Vars
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator

		{
				private readonly TChildCalculator1 _operandCalculator1;
				private readonly TChildCalculator2 _operandCalculator2;

				private TChildCalculator1 _calculator1;
				public IOperatorCalculator Calculator1
				{
					get { return _calculator1; }
					set { _calculator1 = (TChildCalculator1)value; }
				}

				private TChildCalculator2 _calculator2;
				public IOperatorCalculator Calculator2
				{
					get { return _calculator2; }
					set { _calculator2 = (TChildCalculator2)value; }
				}
			public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
			{
				switch (i)
				{
						case 0:
							_calculator1 = (TChildCalculator1)varOperatorCalculator;
							break;

						case 1:
							_calculator2 = (TChildCalculator2)varOperatorCalculator;
							break;


						default:
							throw new Exception(String.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return 
						_calculator1.Calculate()
+						_calculator2.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_3Vars
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
			>
			: IOperatorCalculator_3Vars, IOperatorCalculator_Vars
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator

		{
				private readonly TChildCalculator1 _operandCalculator1;
				private readonly TChildCalculator2 _operandCalculator2;
				private readonly TChildCalculator3 _operandCalculator3;

				private TChildCalculator1 _calculator1;
				public IOperatorCalculator Calculator1
				{
					get { return _calculator1; }
					set { _calculator1 = (TChildCalculator1)value; }
				}

				private TChildCalculator2 _calculator2;
				public IOperatorCalculator Calculator2
				{
					get { return _calculator2; }
					set { _calculator2 = (TChildCalculator2)value; }
				}

				private TChildCalculator3 _calculator3;
				public IOperatorCalculator Calculator3
				{
					get { return _calculator3; }
					set { _calculator3 = (TChildCalculator3)value; }
				}
			public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
			{
				switch (i)
				{
						case 0:
							_calculator1 = (TChildCalculator1)varOperatorCalculator;
							break;

						case 1:
							_calculator2 = (TChildCalculator2)varOperatorCalculator;
							break;

						case 2:
							_calculator3 = (TChildCalculator3)varOperatorCalculator;
							break;


						default:
							throw new Exception(String.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return 
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_4Vars
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
,					TChildCalculator4
			>
			: IOperatorCalculator_4Vars, IOperatorCalculator_Vars
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
				where TChildCalculator4 : IOperatorCalculator

		{
				private readonly TChildCalculator1 _operandCalculator1;
				private readonly TChildCalculator2 _operandCalculator2;
				private readonly TChildCalculator3 _operandCalculator3;
				private readonly TChildCalculator4 _operandCalculator4;

				private TChildCalculator1 _calculator1;
				public IOperatorCalculator Calculator1
				{
					get { return _calculator1; }
					set { _calculator1 = (TChildCalculator1)value; }
				}

				private TChildCalculator2 _calculator2;
				public IOperatorCalculator Calculator2
				{
					get { return _calculator2; }
					set { _calculator2 = (TChildCalculator2)value; }
				}

				private TChildCalculator3 _calculator3;
				public IOperatorCalculator Calculator3
				{
					get { return _calculator3; }
					set { _calculator3 = (TChildCalculator3)value; }
				}

				private TChildCalculator4 _calculator4;
				public IOperatorCalculator Calculator4
				{
					get { return _calculator4; }
					set { _calculator4 = (TChildCalculator4)value; }
				}
			public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
			{
				switch (i)
				{
						case 0:
							_calculator1 = (TChildCalculator1)varOperatorCalculator;
							break;

						case 1:
							_calculator2 = (TChildCalculator2)varOperatorCalculator;
							break;

						case 2:
							_calculator3 = (TChildCalculator3)varOperatorCalculator;
							break;

						case 3:
							_calculator4 = (TChildCalculator4)varOperatorCalculator;
							break;


						default:
							throw new Exception(String.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return 
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
+						_calculator4.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_5Vars
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
,					TChildCalculator4
,					TChildCalculator5
			>
			: IOperatorCalculator_5Vars, IOperatorCalculator_Vars
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
				where TChildCalculator4 : IOperatorCalculator
				where TChildCalculator5 : IOperatorCalculator

		{
				private readonly TChildCalculator1 _operandCalculator1;
				private readonly TChildCalculator2 _operandCalculator2;
				private readonly TChildCalculator3 _operandCalculator3;
				private readonly TChildCalculator4 _operandCalculator4;
				private readonly TChildCalculator5 _operandCalculator5;

				private TChildCalculator1 _calculator1;
				public IOperatorCalculator Calculator1
				{
					get { return _calculator1; }
					set { _calculator1 = (TChildCalculator1)value; }
				}

				private TChildCalculator2 _calculator2;
				public IOperatorCalculator Calculator2
				{
					get { return _calculator2; }
					set { _calculator2 = (TChildCalculator2)value; }
				}

				private TChildCalculator3 _calculator3;
				public IOperatorCalculator Calculator3
				{
					get { return _calculator3; }
					set { _calculator3 = (TChildCalculator3)value; }
				}

				private TChildCalculator4 _calculator4;
				public IOperatorCalculator Calculator4
				{
					get { return _calculator4; }
					set { _calculator4 = (TChildCalculator4)value; }
				}

				private TChildCalculator5 _calculator5;
				public IOperatorCalculator Calculator5
				{
					get { return _calculator5; }
					set { _calculator5 = (TChildCalculator5)value; }
				}
			public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
			{
				switch (i)
				{
						case 0:
							_calculator1 = (TChildCalculator1)varOperatorCalculator;
							break;

						case 1:
							_calculator2 = (TChildCalculator2)varOperatorCalculator;
							break;

						case 2:
							_calculator3 = (TChildCalculator3)varOperatorCalculator;
							break;

						case 3:
							_calculator4 = (TChildCalculator4)varOperatorCalculator;
							break;

						case 4:
							_calculator5 = (TChildCalculator5)varOperatorCalculator;
							break;


						default:
							throw new Exception(String.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return 
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
+						_calculator4.Calculate()
+						_calculator5.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_6Vars
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
,					TChildCalculator4
,					TChildCalculator5
,					TChildCalculator6
			>
			: IOperatorCalculator_6Vars, IOperatorCalculator_Vars
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
				where TChildCalculator4 : IOperatorCalculator
				where TChildCalculator5 : IOperatorCalculator
				where TChildCalculator6 : IOperatorCalculator

		{
				private readonly TChildCalculator1 _operandCalculator1;
				private readonly TChildCalculator2 _operandCalculator2;
				private readonly TChildCalculator3 _operandCalculator3;
				private readonly TChildCalculator4 _operandCalculator4;
				private readonly TChildCalculator5 _operandCalculator5;
				private readonly TChildCalculator6 _operandCalculator6;

				private TChildCalculator1 _calculator1;
				public IOperatorCalculator Calculator1
				{
					get { return _calculator1; }
					set { _calculator1 = (TChildCalculator1)value; }
				}

				private TChildCalculator2 _calculator2;
				public IOperatorCalculator Calculator2
				{
					get { return _calculator2; }
					set { _calculator2 = (TChildCalculator2)value; }
				}

				private TChildCalculator3 _calculator3;
				public IOperatorCalculator Calculator3
				{
					get { return _calculator3; }
					set { _calculator3 = (TChildCalculator3)value; }
				}

				private TChildCalculator4 _calculator4;
				public IOperatorCalculator Calculator4
				{
					get { return _calculator4; }
					set { _calculator4 = (TChildCalculator4)value; }
				}

				private TChildCalculator5 _calculator5;
				public IOperatorCalculator Calculator5
				{
					get { return _calculator5; }
					set { _calculator5 = (TChildCalculator5)value; }
				}

				private TChildCalculator6 _calculator6;
				public IOperatorCalculator Calculator6
				{
					get { return _calculator6; }
					set { _calculator6 = (TChildCalculator6)value; }
				}
			public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
			{
				switch (i)
				{
						case 0:
							_calculator1 = (TChildCalculator1)varOperatorCalculator;
							break;

						case 1:
							_calculator2 = (TChildCalculator2)varOperatorCalculator;
							break;

						case 2:
							_calculator3 = (TChildCalculator3)varOperatorCalculator;
							break;

						case 3:
							_calculator4 = (TChildCalculator4)varOperatorCalculator;
							break;

						case 4:
							_calculator5 = (TChildCalculator5)varOperatorCalculator;
							break;

						case 5:
							_calculator6 = (TChildCalculator6)varOperatorCalculator;
							break;


						default:
							throw new Exception(String.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return 
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
+						_calculator4.Calculate()
+						_calculator5.Calculate()
+						_calculator6.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_7Vars
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
,					TChildCalculator4
,					TChildCalculator5
,					TChildCalculator6
,					TChildCalculator7
			>
			: IOperatorCalculator_7Vars, IOperatorCalculator_Vars
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
				where TChildCalculator4 : IOperatorCalculator
				where TChildCalculator5 : IOperatorCalculator
				where TChildCalculator6 : IOperatorCalculator
				where TChildCalculator7 : IOperatorCalculator

		{
				private readonly TChildCalculator1 _operandCalculator1;
				private readonly TChildCalculator2 _operandCalculator2;
				private readonly TChildCalculator3 _operandCalculator3;
				private readonly TChildCalculator4 _operandCalculator4;
				private readonly TChildCalculator5 _operandCalculator5;
				private readonly TChildCalculator6 _operandCalculator6;
				private readonly TChildCalculator7 _operandCalculator7;

				private TChildCalculator1 _calculator1;
				public IOperatorCalculator Calculator1
				{
					get { return _calculator1; }
					set { _calculator1 = (TChildCalculator1)value; }
				}

				private TChildCalculator2 _calculator2;
				public IOperatorCalculator Calculator2
				{
					get { return _calculator2; }
					set { _calculator2 = (TChildCalculator2)value; }
				}

				private TChildCalculator3 _calculator3;
				public IOperatorCalculator Calculator3
				{
					get { return _calculator3; }
					set { _calculator3 = (TChildCalculator3)value; }
				}

				private TChildCalculator4 _calculator4;
				public IOperatorCalculator Calculator4
				{
					get { return _calculator4; }
					set { _calculator4 = (TChildCalculator4)value; }
				}

				private TChildCalculator5 _calculator5;
				public IOperatorCalculator Calculator5
				{
					get { return _calculator5; }
					set { _calculator5 = (TChildCalculator5)value; }
				}

				private TChildCalculator6 _calculator6;
				public IOperatorCalculator Calculator6
				{
					get { return _calculator6; }
					set { _calculator6 = (TChildCalculator6)value; }
				}

				private TChildCalculator7 _calculator7;
				public IOperatorCalculator Calculator7
				{
					get { return _calculator7; }
					set { _calculator7 = (TChildCalculator7)value; }
				}
			public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
			{
				switch (i)
				{
						case 0:
							_calculator1 = (TChildCalculator1)varOperatorCalculator;
							break;

						case 1:
							_calculator2 = (TChildCalculator2)varOperatorCalculator;
							break;

						case 2:
							_calculator3 = (TChildCalculator3)varOperatorCalculator;
							break;

						case 3:
							_calculator4 = (TChildCalculator4)varOperatorCalculator;
							break;

						case 4:
							_calculator5 = (TChildCalculator5)varOperatorCalculator;
							break;

						case 5:
							_calculator6 = (TChildCalculator6)varOperatorCalculator;
							break;

						case 6:
							_calculator7 = (TChildCalculator7)varOperatorCalculator;
							break;


						default:
							throw new Exception(String.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return 
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
+						_calculator4.Calculate()
+						_calculator5.Calculate()
+						_calculator6.Calculate()
+						_calculator7.Calculate()
;			}
		}
		internal class Add_OperatorCalculator_8Vars
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
,					TChildCalculator4
,					TChildCalculator5
,					TChildCalculator6
,					TChildCalculator7
,					TChildCalculator8
			>
			: IOperatorCalculator_8Vars, IOperatorCalculator_Vars
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
				where TChildCalculator4 : IOperatorCalculator
				where TChildCalculator5 : IOperatorCalculator
				where TChildCalculator6 : IOperatorCalculator
				where TChildCalculator7 : IOperatorCalculator
				where TChildCalculator8 : IOperatorCalculator

		{
				private readonly TChildCalculator1 _operandCalculator1;
				private readonly TChildCalculator2 _operandCalculator2;
				private readonly TChildCalculator3 _operandCalculator3;
				private readonly TChildCalculator4 _operandCalculator4;
				private readonly TChildCalculator5 _operandCalculator5;
				private readonly TChildCalculator6 _operandCalculator6;
				private readonly TChildCalculator7 _operandCalculator7;
				private readonly TChildCalculator8 _operandCalculator8;

				private TChildCalculator1 _calculator1;
				public IOperatorCalculator Calculator1
				{
					get { return _calculator1; }
					set { _calculator1 = (TChildCalculator1)value; }
				}

				private TChildCalculator2 _calculator2;
				public IOperatorCalculator Calculator2
				{
					get { return _calculator2; }
					set { _calculator2 = (TChildCalculator2)value; }
				}

				private TChildCalculator3 _calculator3;
				public IOperatorCalculator Calculator3
				{
					get { return _calculator3; }
					set { _calculator3 = (TChildCalculator3)value; }
				}

				private TChildCalculator4 _calculator4;
				public IOperatorCalculator Calculator4
				{
					get { return _calculator4; }
					set { _calculator4 = (TChildCalculator4)value; }
				}

				private TChildCalculator5 _calculator5;
				public IOperatorCalculator Calculator5
				{
					get { return _calculator5; }
					set { _calculator5 = (TChildCalculator5)value; }
				}

				private TChildCalculator6 _calculator6;
				public IOperatorCalculator Calculator6
				{
					get { return _calculator6; }
					set { _calculator6 = (TChildCalculator6)value; }
				}

				private TChildCalculator7 _calculator7;
				public IOperatorCalculator Calculator7
				{
					get { return _calculator7; }
					set { _calculator7 = (TChildCalculator7)value; }
				}

				private TChildCalculator8 _calculator8;
				public IOperatorCalculator Calculator8
				{
					get { return _calculator8; }
					set { _calculator8 = (TChildCalculator8)value; }
				}
			public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
			{
				switch (i)
				{
						case 0:
							_calculator1 = (TChildCalculator1)varOperatorCalculator;
							break;

						case 1:
							_calculator2 = (TChildCalculator2)varOperatorCalculator;
							break;

						case 2:
							_calculator3 = (TChildCalculator3)varOperatorCalculator;
							break;

						case 3:
							_calculator4 = (TChildCalculator4)varOperatorCalculator;
							break;

						case 4:
							_calculator5 = (TChildCalculator5)varOperatorCalculator;
							break;

						case 5:
							_calculator6 = (TChildCalculator6)varOperatorCalculator;
							break;

						case 6:
							_calculator7 = (TChildCalculator7)varOperatorCalculator;
							break;

						case 7:
							_calculator8 = (TChildCalculator8)varOperatorCalculator;
							break;


						default:
							throw new Exception(String.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return 
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
+						_calculator4.Calculate()
+						_calculator5.Calculate()
+						_calculator6.Calculate()
+						_calculator7.Calculate()
+						_calculator8.Calculate()
;			}
		}
}
