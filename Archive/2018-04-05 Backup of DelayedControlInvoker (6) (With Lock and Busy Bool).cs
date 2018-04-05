//using System;
//using System.Timers;
//using System.Windows.Forms;
//using Timer = System.Timers.Timer;

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

//		// Lock: so that multiple threads will only result in one call at a time.
//		// Busy: so that two threads will... what exactly?

//		private readonly Timer _timer = new Timer(DELAY_MILLISECONDS) { AutoReset = true };
//		private readonly object _lock = new object();
//		private readonly Control _control;

//		private bool _busy;
//		private Action _action;

//		public DelayedControlInvoker(Control control)
//		{
//			_control = control ?? throw new ArgumentNullException(nameof(control));
//			_timer.Elapsed += _timer_Elapsed;
//		}

//		public void InvokeWithDelay(Action action)
//		{
//			lock (_lock)
//			{
//				try
//				{
//					if (_busy) return;
//					_busy = true;

//					_action = action ?? throw new ArgumentNullException(nameof(action));
//					_timer.Start();
//				}
//				finally
//				{
//					_busy = false;
//				}
//			}
//		}

//		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
//		{
//			lock (_lock)
//			{
//				if (_busy) return;
//				_busy = true;

//				_timer.Stop();
//				_control.Invoke(_action);
//			}
//		}
//	}
//}