using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Common;
using JJ.Framework.Wishes.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Logging;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.Logging;
using static System.Environment;
using static System.IO.File;
using static System.String;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.FilledInHelper;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.Wishes.Logging.LoggingFactory;

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public void LogAction(TapeAction action, string message = null)
            => LogWishes.LogAction(action, message);
        
        public void LogAction(Tape entity, string action, string message = null)
            => LogWishes.LogAction(entity, action, message);
        
        public void LogAction(Buff entity, string action, string message = null)
            => LogWishes.LogAction(entity, action, message);
        
        public void LogAction(Sample entity, string action, string message = null)
            => LogWishes.LogAction(entity, action, message);
        
        public void LogAction(AudioFileOutput entity, string action, string message = null)
            => LogWishes.LogAction(entity, action, message);
        
        public void LogAction(FlowNode entity, string action, string message = null)
            => LogWishes.LogAction(entity, action, message);
        
        public void LogAction(object entity, string action, string name, string message = null)
            => LogWishes.LogAction(entity, action, name, message);
        
        public void LogAction(string typeName, string message)
            => LogWishes.LogAction(typeName, message);
        
        public void LogAction(string typeName, string action, string message)
            => LogWishes.LogAction(typeName, action, message);
        
        public void LogAction(string typeName, string action, string objectName, string message)
            => LogWishes.LogAction(typeName, action, objectName, message);
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensions
    {
        public static void LogAction(this FlowNode        entity, string action, string message = null) => GetLogWishes(entity, x => x.SynthWishes).LogAction(entity, action, message);
        public static void LogAction(this Tape            entity, string action, string message = null) => GetLogWishes(entity, x => x.SynthWishes).LogAction(entity, action, message);
        public static void LogAction(this TapeAction      action,                string message = null) => GetLogWishes(action, x => x.SynthWishes).LogAction(action, message);
        public static void LogAction(this Buff            entity, string action, string message = null) => GetLogWishes(entity, x => x.SynthWishes).LogAction(entity, action, message);
        public static void LogAction(this AudioFileOutput entity, SynthWishes synthWishes, string action, string message = null) => GetLogWishes(entity, x => synthWishes).LogAction(entity, action, message);
        public static void LogAction(this Sample          entity, SynthWishes synthWishes, string action, string message = null) => GetLogWishes(entity, x => synthWishes).LogAction(entity, action, message);
    }

    public partial class LogWishes
    {
        
        // Actions
                
        public void LogAction(FlowNode entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Operator), action, entity.Name, message));
        }
        
        public void LogAction(Tape entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Tape), action, entity.Descriptor(), message));
        }

        public void LogAction(TapeAction action, string message = null)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            Log(ActionMessage("Action", action.Type, action.Tape.Descriptor(), message));
        }
        
        public void LogAction(Buff entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Buff), action, entity.Name, message));
        }
        
        public void LogAction(AudioFileOutput entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage("Out", action, entity.Name, message ?? ConfigLog(entity)));
        }
        
        public void LogAction(Sample entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Sample), action, entity.Name, message ?? ConfigLog(entity)));
        }
        
        public void LogAction(object entity, string action, string name, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(entity.GetType().Name, action, name, message));
        }
        
        public void LogAction(string typeName, string message) 
            => Log(ActionMessage(typeName, null, null, message));
        
        public void LogAction(string typeName, string action, string message) 
            => Log(ActionMessage(typeName, action, null, message));
        
        public void LogAction(string typeName, string action, string objectName, string message) 
            => Log(ActionMessage(typeName, action, objectName, message));

        public string ActionMessage(string typeName, ActionEnum action, string objectName, string message)
            => ActionMessage(typeName, action.ToString(), objectName, message);
        
        public string ActionMessage(string typeName, string action, string objectName, string message)
        {
            string text = PrettyTime();
                
            if (Has(typeName))
            {
                text += " [" + typeName.ToUpper() + "]";
            }
            
            if (Has(action))
            {
                text += " " + action;
            }
            
            if (Has(objectName))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + '"' + objectName + '"';
            }
            
            if (Has(message))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + message;
            }
            
            return text;
        }
    }

}