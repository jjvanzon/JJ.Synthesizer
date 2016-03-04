//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation
//{
//    internal class NodeValidator_Delete : FluentValidator<Node>
//    {
//        public NodeValidator_Delete(Node obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {

//            if (node.Curve.Nodes.Count <= 2)
//            {
//                MainViewModel.ValidationMessages.Add(new Message
//                {
//                    PropertyKey = PropertyNames.Nodes,
//                    Text = ValidationMessageFormatter.GreaterThanOrEqual(CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Nodes), 2)
//                });
//            }

//            throw new NotImplementedException();
//        }
//    }
//}
