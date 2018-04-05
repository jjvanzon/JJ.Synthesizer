//using System;
//using System.Windows.Forms;
//using Timer = System.Windows.Forms.Timer;

//namespace JJ.Presentation.Synthesizer.WinForms.Helpers
//{
//	internal class DelayedControlInvoker
//	{
//		private const int DELAY_MILLISECONDS = 100;

//		private static readonly Timer _timer = new Timer();

//		private readonly Control _control;

//		private Action _action;

//		public DelayedControlInvoker(Control control)
//		{
//			_control = control ?? throw new ArgumentNullException(nameof(control));
//			_timer.Enabled = true;
//			_timer.Interval = DELAY_MILLISECONDS;
//			_timer.Tick += _timer_Tick;
//		}

//		public void Invoke(Action action)
//		{
//			_action = action ?? throw new ArgumentNullException(nameof(action));
//			_timer.Start();
//		}

//		private void _timer_Tick(object sender, EventArgs e)
//		{
//			_timer.Stop();
//			_control.Invoke(_action);
//		}
//	}
//}
