using System;

namespace JJ.Presentation.Synthesizer.Helpers
{
    public class ViewModelNotFoundByIDException<TViewModel> : Exception
    {
        private readonly string _message;

        public override string Message => _message; 

        public ViewModelNotFoundByIDException(int id)
        {
            _message = String.Format("{0} with id '{1}' not found.",  typeof(TViewModel).Name, id);
        }
    }
}
