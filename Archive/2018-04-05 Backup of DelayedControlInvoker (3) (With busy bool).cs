//using System;
//using System.Timers;
//using System.Windows.Forms;

//namespace JJ.Presentation.Synthesizer.WinForms.Helpers
//{
//	internal class DelayedControlInvoker
//	{
//		private const double DELAY_MILLISECONDS = 100;

//		// WARNING:
//		// I want the next delayed invokation to cancel the previous one.
//		// This System Timer uses the thread pool, so each invoke will not cancel previous one other.
//		// It will basically do its very best to make sure the invokation goes off.
//		// So we need to write logic to specifically prevent that, keeping the multi-threaded nature of this class in mind.
//		// 
//		// WinForms Timer is no option. The benefit of the WinForms Timer would be that it cancels previous invokations.
//		// But starting WinForms Timer should be done on the UI thread,
//		// requiring you to invoke the UI thread, which you were specifically trying to avoid.

//		private static readonly System.Timers.Timer _timer = new System.Timers.Timer(DELAY_MILLISECONDS) { AutoReset = true };

//		//private readonly object _lock = new object();
//		private readonly Control _control;

//		private Action _action;
//		private volatile bool _busy;

//		public DelayedControlInvoker(Control control)
//		{
//			_control = control ?? throw new ArgumentNullException(nameof(control));
//			_timer.Elapsed += _timer_Elapsed;
//		}

//		public void Invoke(Action action)
//		{
//			if (_busy)
//			{
//				return;
//			}

//			_action = action ?? throw new ArgumentNullException(nameof(action));

//			_timer.Start();
//		}

//		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
//		{
//			if (_busy)
//			{
//				return;
//			}
//			_busy = true;

//			try
//			{
//				_timer.Stop();
//				_control.Invoke(_action);
//			}
//			finally
//			{
//				_busy = false;
//			}
//		}
//	}
//}