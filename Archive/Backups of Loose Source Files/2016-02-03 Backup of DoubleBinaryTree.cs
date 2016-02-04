//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JJ.Business.Synthesizer.Helpers
//{
//    internal class DoubleBinaryTree
//    {
//        private class Node
//        {
//            public Node Left { get; set; }
//            public Node Right { get; set; }

//            public double Value { get; set; }
//        }

//        private Node _root;

//        public void Add(double value)
//        {
//            if (_root == null)
//            {
//                _root = new Node
//                {
//                    Value = value
//                };
//            }
//            else
//            {
//                Node node = FindNode(value);
//            }
//        }

//        private Node FindNode(double value)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
