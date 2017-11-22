using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Helpers
{
	public class VisitationCannotBeHandledException : Exception
	{
		public VisitationCannotBeHandledException([CallerMemberName] string callerMemberName = null)
		{
			if (!string.IsNullOrEmpty(callerMemberName))
			{
				Message = $"Error in {callerMemberName}. The variation could not be handled.";
			}
			else
			{
				Message = "Error in visitation. The variation could not be handled.";
			}
		}

		public override string Message { get; }
	}
}
