//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Data.Canonical;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Presentation.Synthesizer.Helpers
//{
//    [Obsolete("Use JJ.Business.Canonical.ResultHelper instead.", true)]
//    internal static class ResultHelper
//    {
//        public static void Assert(IResult result)
//        {
//            if (!result.Successful)
//            {
//                string formattedMessages = FormatMessages(result);
//                throw new Exception(formattedMessages);
//            }
//        }

//        public static string FormatMessages(IResult result)
//        {
//            if (result == null) throw new NullException(() => result);

//            if (result.Messages == null)
//            {
//                return null;
//            }

//            return FormatMessages(result.Messages);
//        }

//        public static string FormatMessages(IList<Message> messages)
//        {
//            if (messages.Count == 0)
//            {
//                return null;
//            }

//            return String.Join(Environment.NewLine, messages.Select(x => x.Text));
//        }
//    }
//}
