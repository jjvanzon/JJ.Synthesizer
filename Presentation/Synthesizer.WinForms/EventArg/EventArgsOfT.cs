using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
	internal class EventArgs<T> : EventArgs
	{
		public T Value { get; }

		public EventArgs(T value)
		{
			Value = value;
		}
	}
}
