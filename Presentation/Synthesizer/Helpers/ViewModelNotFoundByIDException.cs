using System;

namespace JJ.Presentation.Synthesizer.Helpers
{
	public class ViewModelNotFoundByIDException<TViewModel> : Exception
	{
		public override string Message { get; }

		public ViewModelNotFoundByIDException(int id)
		{
			Message = $"{typeof(TViewModel).Name} with id '{id}' not found.";
		}
	}
}
