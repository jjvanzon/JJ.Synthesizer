


using System;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{

    public struct Add_OperatorCalculator_1Vars_1Const
			<
					TChildCalculator1
			>
			: IOperatorCalculator_Vars_1Const
				where TChildCalculator1 : IOperatorCalculator
		{
			private double _constValue;
            public double ConstValue
            {
				get { return _constValue; }
                set { _constValue = value; }
            }
				private TChildCalculator1 _calculator1;

			public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
			{
				switch (i)
				{
						case 0:
							_calculator1 = (TChildCalculator1)varOperatorCalculator;
							break;


						default:
							throw new Exception(string.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return _constValue +
						_calculator1.Calculate()
;			}
		}
		public struct Add_OperatorCalculator_2Vars_1Const
			<
					TChildCalculator1
,					TChildCalculator2
			>
			: IOperatorCalculator_Vars_1Const
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
		{
			private double _constValue;
            public double ConstValue
            {
				get { return _constValue; }
                set { _constValue = value; }
            }
				private TChildCalculator1 _calculator1;
				private TChildCalculator2 _calculator2;

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
							throw new Exception(string.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return _constValue +
						_calculator1.Calculate()
+						_calculator2.Calculate()
;			}
		}
		public struct Add_OperatorCalculator_3Vars_1Const
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
			>
			: IOperatorCalculator_Vars_1Const
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
		{
			private double _constValue;
            public double ConstValue
            {
				get { return _constValue; }
                set { _constValue = value; }
            }
				private TChildCalculator1 _calculator1;
				private TChildCalculator2 _calculator2;
				private TChildCalculator3 _calculator3;

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
							throw new Exception(string.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return _constValue +
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
;			}
		}
		public struct Add_OperatorCalculator_4Vars_1Const
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
,					TChildCalculator4
			>
			: IOperatorCalculator_Vars_1Const
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
				where TChildCalculator4 : IOperatorCalculator
		{
			private double _constValue;
            public double ConstValue
            {
				get { return _constValue; }
                set { _constValue = value; }
            }
				private TChildCalculator1 _calculator1;
				private TChildCalculator2 _calculator2;
				private TChildCalculator3 _calculator3;
				private TChildCalculator4 _calculator4;

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
							throw new Exception(string.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return _constValue +
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
+						_calculator4.Calculate()
;			}
		}
		public struct Add_OperatorCalculator_5Vars_1Const
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
,					TChildCalculator4
,					TChildCalculator5
			>
			: IOperatorCalculator_Vars_1Const
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
				where TChildCalculator4 : IOperatorCalculator
				where TChildCalculator5 : IOperatorCalculator
		{
			private double _constValue;
            public double ConstValue
            {
				get { return _constValue; }
                set { _constValue = value; }
            }
				private TChildCalculator1 _calculator1;
				private TChildCalculator2 _calculator2;
				private TChildCalculator3 _calculator3;
				private TChildCalculator4 _calculator4;
				private TChildCalculator5 _calculator5;

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
							throw new Exception(string.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return _constValue +
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
+						_calculator4.Calculate()
+						_calculator5.Calculate()
;			}
		}
		public struct Add_OperatorCalculator_6Vars_1Const
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
,					TChildCalculator4
,					TChildCalculator5
,					TChildCalculator6
			>
			: IOperatorCalculator_Vars_1Const
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
				where TChildCalculator4 : IOperatorCalculator
				where TChildCalculator5 : IOperatorCalculator
				where TChildCalculator6 : IOperatorCalculator
		{
			private double _constValue;
            public double ConstValue
            {
				get { return _constValue; }
                set { _constValue = value; }
            }
				private TChildCalculator1 _calculator1;
				private TChildCalculator2 _calculator2;
				private TChildCalculator3 _calculator3;
				private TChildCalculator4 _calculator4;
				private TChildCalculator5 _calculator5;
				private TChildCalculator6 _calculator6;

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
							throw new Exception(string.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return _constValue +
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
+						_calculator4.Calculate()
+						_calculator5.Calculate()
+						_calculator6.Calculate()
;			}
		}
		public struct Add_OperatorCalculator_7Vars_1Const
			<
					TChildCalculator1
,					TChildCalculator2
,					TChildCalculator3
,					TChildCalculator4
,					TChildCalculator5
,					TChildCalculator6
,					TChildCalculator7
			>
			: IOperatorCalculator_Vars_1Const
				where TChildCalculator1 : IOperatorCalculator
				where TChildCalculator2 : IOperatorCalculator
				where TChildCalculator3 : IOperatorCalculator
				where TChildCalculator4 : IOperatorCalculator
				where TChildCalculator5 : IOperatorCalculator
				where TChildCalculator6 : IOperatorCalculator
				where TChildCalculator7 : IOperatorCalculator
		{
			private double _constValue;
            public double ConstValue
            {
				get { return _constValue; }
                set { _constValue = value; }
            }
				private TChildCalculator1 _calculator1;
				private TChildCalculator2 _calculator2;
				private TChildCalculator3 _calculator3;
				private TChildCalculator4 _calculator4;
				private TChildCalculator5 _calculator5;
				private TChildCalculator6 _calculator6;
				private TChildCalculator7 _calculator7;

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
							throw new Exception(string.Format("i {0} not valid.", i));
				}
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public double Calculate()
			{
				return _constValue +
						_calculator1.Calculate()
+						_calculator2.Calculate()
+						_calculator3.Calculate()
+						_calculator4.Calculate()
+						_calculator5.Calculate()
+						_calculator6.Calculate()
+						_calculator7.Calculate()
;			}
		}
}
