using System;

namespace JJ.Business.Synthesizer.Helpers
{
	public class AlreadyDoneIsNullException : Exception
	{
		public override string Message => "alreadyDone is null. Pass a (new) HashSet<object>.";
	}
}