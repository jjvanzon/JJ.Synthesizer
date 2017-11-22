using System;
using System.Reflection;

namespace JJ.Business.SynthesizerPrototype.Helpers
{
	public class VisitationCannotBeHandledException : Exception
	{
		private readonly string _message;

		public VisitationCannotBeHandledException(MethodBase method)
		{
			if (method != null)
			{
				_message = $"Error in {method.Name}. The variation could not be handled.";
			}
			else
			{
				_message = "Error in visitation. The variation could not be handled.";
			}
		}

		public override string Message => _message;
	}
}
