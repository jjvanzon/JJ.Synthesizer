//using System;
//using System.Timers;
//using System.Windows.Forms;
//using Timer = System.Timers.Timer;

//namespace JJ.Presentation.Synthesizer.WinForms.Helpers
//{
//	internal class DelayedControlInvoker
//	{
//		private const double DELAY_MILLISECONDS = 100;

//		private static readonly Timer _timer = new Timer(DELAY_MILLISECONDS) { AutoReset = false };

//		private readonly Control _control;

//		private Action _action;

//		public DelayedControlInvoker(Control control)
//		{
//			_control = control ?? throw new ArgumentNullException(nameof(control));
//			_timer.Elapsed += _timer_Elapsed;
//		}

//		public void Invoke(Action action)
//		{
//			_action = action ?? throw new ArgumentNullException(nameof(action));
//			_timer.Enabled = true;
//		}

//		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
//		{
//			_timer.Enabled = false;
//			_timer.Stop();

//			_control.Invoke(_action);
//		}
//	}
//}
