//using JJ.Business.CanonicalModel;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Presentation.Synthesizer.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JJ.Presentation.Synthesizer.Presenters
//{
//    public class CannotDeletePresenter
//    {
//        public CannotDeleteViewModel Show(IDNameAndObjectType idNameAndObjectType, IList<Message> messages)
//        {
//            if (messages == null) throw new NullException(() => messages);
//            if (idNameAndObjectType == null) throw new NullException(() => idNameAndObjectType);
//            if (String.IsNullOrEmpty(idNameAndObjectType.ObjectType)) throw new NullOrEmptyException(() => idNameAndObjectType.ObjectType);

//            var viewModel = new CannotDeleteViewModel
//            {
//                Object = idNameAndObjectType,
//                Messages = messages
//            };

//            return viewModel;
//        }

//        internal CannotDeleteViewModel Show(int id, string name, string objectTypeName, IList<Message> messages)
//        {
//            if (String.IsNullOrEmpty(objectTypeName)) throw new NullOrEmptyException(() => objectTypeName);
//            if (messages == null) throw new NullException(() => messages);

//            var viewModel = new CannotDeleteViewModel
//            {
//                Object = new IDNameAndObjectType
//                {
//                    ID = id, 
//                    Name = name, 
//                    ObjectType = objectTypeName
//                },
//                Messages = messages
//            };

//            return viewModel;
//        }
//    }
//}
