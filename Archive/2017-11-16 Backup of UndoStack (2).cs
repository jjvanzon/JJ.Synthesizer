//using JJ.Framework.Exceptions;
//using JJ.Presentation.Synthesizer.ViewModels;

//namespace JJ.Presentation.Synthesizer.Helpers
//{
//    internal class UndoStack
//    {
//        private readonly ViewModelBase[] _array;

//        private int _currentEverIncreasingIndex;

//        /// <summary> Negative at first. </summary>
//        private int _bottomIndex;

//        public UndoStack(int size = 100)
//        {
//            if (size <= 0) throw new LessThanOrEqualException(() => size, 0);

//            _array = new ViewModelBase[size];

//            _bottomIndex = -size;
//        }

//        public void Push(ViewModelBase item)
//        {
//            int arrayIndex = _currentEverIncreasingIndex % _array.Length;

//            _array[arrayIndex] = item;

//            _currentEverIncreasingIndex++;

//            _bottomIndex++;
//        }
//    }
//}
