using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Wishes.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Text.StringWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        public void LogAction(FlowNode entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction<Operator>(action, entity.Name, message);
        }
        
        public string ActionMessage(FlowNode entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage<Operator>(action, entity.Name, message);
        }
            
        public void LogAction(FlowNode entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction<Operator>(action, entity.Name, message);
        }
            
        public string ActionMessage(FlowNode entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage<Operator>(action, entity.Name, message);
        }

        public void LogAction(Tape entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction(entity, action, entity.Descriptor(), message);
        }

        public string ActionMessage(Tape entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Descriptor(), message);
        }
        
        public void LogAction(Tape entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction(entity, action, entity.Descriptor(), message);
        }
        
        public string ActionMessage(Tape entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Descriptor(), message);
        }

        /// <inheritdoc cref="_logtapeaction" />
        public void Log(TapeAction action, string message = null)
            => LogAction(action, message);

        /// <inheritdoc cref="_logtapeaction" />
        public string Message(TapeAction action, string message = null)
            => ActionMessage(action, message);
        
        public void LogAction(TapeAction action, string message = null)
        {
            if (action == null) throw new NullException(() => action);
            LogAction("Actions", action.Type, action.Tape.Descriptor(), message);
        }
        
        public string ActionMessage(TapeAction action, string message = null)
        {
            if (action == null) throw new NullException(() => action);
            return ActionMessage("Actions", action.Type, action.Tape.Descriptor(), message);
        }
        
        public void LogAction(Buff entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction(entity, action, entity.Name, message);
        }

        public string ActionMessage(Buff entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message);
        }

        public void LogAction(Buff entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction(entity, action, entity.Name, message);
        }

        public string ActionMessage(Buff entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message);
        }

        public void LogAction(AudioFileOutput entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction("Out", action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public string ActionMessage(AudioFileOutput entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage("Out", action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public void LogAction(AudioFileOutput entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction("Out", action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public string ActionMessage(AudioFileOutput entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage("Out", action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public void LogAction(Sample entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction(entity, action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public string ActionMessage(Sample entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public void LogAction(Sample entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction(entity, action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public string ActionMessage(Sample entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message ?? ConfigLog(entity));
        }

        // ReSharper disable once UnusedParameter.Global
        public void LogAction(byte[] entity, ActionEnum action, string message = null)
        {
            LogAction("Memory", action, "", message);
        }

        // ReSharper disable once UnusedParameter.Global
        public string ActionMessage(byte[] entity, ActionEnum action, string message = null)
        {
            return ActionMessage("Memory", action, "", message);
        }
        
        // ReSharper disable once UnusedParameter.Global
        public void LogAction(byte[] entity, string action, string message = null)
        {
            LogAction("Memory", action, "", message);
        }

        // ReSharper disable once UnusedParameter.Global
        public string ActionMessage(byte[] entity, string action, string message = null)
        {
            return ActionMessage("Memory", action, "", message);
        }
        
        public void LogAction(object entity, ActionEnum action, string name, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction(entity.GetType().Name, action, name, message);
        }
        
        public string ActionMessage(object entity, ActionEnum action, string name, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.GetType().Name, action, name, message);
        }
        
        public void LogAction(object entity, string action, string name, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            LogAction(entity.GetType().Name, action, name, message);
        }
        
        public string ActionMessage(object entity, string action, string name, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.GetType().Name, action, name, message);
        }
        
        public void LogAction<TEntity>(string message) 
            => LogAction(typeof(TEntity).Name, null, null, message);
        
        public string ActionMessage<TEntity>(string message) 
            => ActionMessage(typeof(TEntity).Name, null, null, message);
        
        public void LogAction<TEntity>(ActionEnum action, string message) 
            => LogAction(typeof(TEntity).Name, action, null, message);
        
        public string ActionMessage<TEntity>(ActionEnum action, string message) 
            => ActionMessage(typeof(TEntity).Name, action, null, message);
        
        public void LogAction<TEntity>(string action, string message) 
            => LogAction(typeof(TEntity).Name, action, null, message);
        
        public string ActionMessage<TEntity>(string action, string message) 
            => ActionMessage(typeof(TEntity).Name, action, null, message);
        
        public void LogAction<TEntity>(ActionEnum action, string name, string message) 
            => LogAction(typeof(TEntity).Name, action, name, message);
        
        public string ActionMessage<TEntity>(ActionEnum action, string name, string message) 
            => ActionMessage(typeof(TEntity).Name, action, name, message);
        
        public void LogAction<TEntity>(string action, string name, string message) 
            => LogAction(typeof(TEntity).Name, action, name, message);
        
        public string ActionMessage<TEntity>(string action, string name, string message) 
            => ActionMessage(typeof(TEntity).Name, action, name, message);
        
        public void LogAction(string typeName, string message) 
            => LogAction(typeName, null, null, message);
        
        public string ActionMessage(string typeName, string message) 
            => ActionMessage(typeName, null, null, message);
        
        public void LogAction(string typeName, ActionEnum action, string message = null) 
            => LogAction(typeName, action, null, message);
        
        public string ActionMessage(string typeName, ActionEnum action, string message = null) 
            => ActionMessage(typeName, action, null, message);
        
        public void LogAction(string typeName, string action, string message) 
            => LogAction(typeName, action, null, message);
        
        public string ActionMessage(string typeName, string action, string message) 
            => ActionMessage(typeName, action, null, message);
        
        // ReSharper disable MethodOverloadWithOptionalParameter
        
        public void LogAction(string typeName, ActionEnum action, string name, string message = null) 
            => LogAction(typeName, action.ToString(), name, message);

        public string ActionMessage(string typeName, ActionEnum action, string name, string message = null) 
            => ActionMessage(typeName, action.ToString(), name, message);
        
        public void LogAction(string typeName, string action, string name, string message = null) 
            => Log("Actions", ActionMessage(typeName, action, name, message));

        public string ActionMessage(string typeName, string action, string name, string message = null)
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
            
            if (Has(name))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + '"' + name + '"';
            }
            
            if (Has(message))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + message;
            }
            
            return text;
        }
    
        // ReSharper restore MethodOverloadWithOptionalParameter
    }
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public void LogAction(FlowNode        entity, string     action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(FlowNode        entity, ActionEnum action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(Tape            entity, string     action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(Tape            entity, ActionEnum action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(TapeAction      action,                                 string message = null) => Logging.LogAction(action,                 message);
        /// <inheritdoc cref="_logtapeaction" />
        public void Log      (TapeAction      action,                                 string message = null) => Logging.Log      (action,                 message);
        public void LogAction(Buff            entity, string     action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(Buff            entity, ActionEnum action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(Sample          entity, string     action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(Sample          entity, ActionEnum action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(AudioFileOutput entity, string     action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(AudioFileOutput entity, ActionEnum action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(byte[]          entity, string     action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(byte[]          entity, ActionEnum action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(object          entity, string     action, string name, string message = null) => Logging.LogAction(entity,   action, name, message);
        public void LogAction(object          entity, ActionEnum action, string name, string message = null) => Logging.LogAction(entity,   action, name, message);
        public void LogAction(string typeName,                                        string message       ) => Logging.LogAction(typeName,               message);
        public void LogAction(string typeName,        string     action,              string message       ) => Logging.LogAction(typeName, action,       message);
        public void LogAction(string typeName,        ActionEnum action,              string message = null) => Logging.LogAction(typeName, action,       message);
        public void LogAction(string typeName,        string     action, string name, string message       ) => Logging.LogAction(typeName, action, name, message);
        public void LogAction(string typeName,        ActionEnum action, string name, string message       ) => Logging.LogAction(typeName, action, name, message);
        public void LogAction<TEntity>(                                               string message       ) => Logging.LogAction<TEntity>(               message);
        public void LogAction<TEntity>(               string     action,              string message       ) => Logging.LogAction<TEntity>( action,       message);
        public void LogAction<TEntity>(               ActionEnum action,              string message       ) => Logging.LogAction<TEntity>( action,       message);
        public void LogAction<TEntity>(               string     action, string name, string message       ) => Logging.LogAction<TEntity>( action, name, message);
        public void LogAction<TEntity>(               ActionEnum action, string name, string message       ) => Logging.LogAction<TEntity>( action, name, message);
        
        public string ActionMessage(FlowNode        entity, string     action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(FlowNode        entity, ActionEnum action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(Tape            entity, string     action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(Tape            entity, ActionEnum action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(TapeAction      action,                                 string message = null) => Logging.ActionMessage(action,                 message);
        /// <inheritdoc cref="_logtapeaction" />
        public string Message      (TapeAction      action,                                 string message = null) => Logging.Message  (action,                 message);
        public string ActionMessage(Buff            entity, string     action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(Buff            entity, ActionEnum action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(Sample          entity, string     action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(Sample          entity, ActionEnum action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(AudioFileOutput entity, string     action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(AudioFileOutput entity, ActionEnum action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(byte[]          entity, string     action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(byte[]          entity, ActionEnum action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(object          entity, string     action, string name, string message = null) => Logging.ActionMessage(entity,   action, name, message);
        public string ActionMessage(object          entity, ActionEnum action, string name, string message = null) => Logging.ActionMessage(entity,   action, name, message);
        public string ActionMessage(string typeName,                                        string message       ) => Logging.ActionMessage(typeName,               message);
        public string ActionMessage(string typeName,        string     action,              string message       ) => Logging.ActionMessage(typeName, action,       message);
        public string ActionMessage(string typeName,        ActionEnum action,              string message = null) => Logging.ActionMessage(typeName, action,       message);
        public string ActionMessage(string typeName,        string     action, string name, string message       ) => Logging.ActionMessage(typeName, action, name, message);
        public string ActionMessage(string typeName,        ActionEnum action, string name, string message       ) => Logging.ActionMessage(typeName, action, name, message);
        public string ActionMessage<TEntity>(                                               string message       ) => Logging.ActionMessage<TEntity>(               message);
        public string ActionMessage<TEntity>(               string     action,              string message       ) => Logging.ActionMessage<TEntity>( action,       message);
        public string ActionMessage<TEntity>(               ActionEnum action,              string message       ) => Logging.ActionMessage<TEntity>( action,       message);
        public string ActionMessage<TEntity>(               string     action, string name, string message       ) => Logging.ActionMessage<TEntity>( action, name, message);
        public string ActionMessage<TEntity>(               ActionEnum action, string name, string message       ) => Logging.ActionMessage<TEntity>( action, name, message);

    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {
        public static void LogAction(this FlowNode        entity, string     action, string message = null) => entity.Logging  .LogAction(entity, action, message);
        public static void LogAction(this FlowNode        entity, ActionEnum action, string message = null) => entity.Logging  .LogAction(entity, action, message);
        public static void LogAction(this Tape            entity, string     action, string message = null) => entity.Logging  .LogAction(entity, action, message);
        public static void LogAction(this Tape            entity, ActionEnum action, string message = null) => entity.Logging  .LogAction(entity, action, message);
        /// <inheritdoc cref="_logtapeaction" />
        public static void Log      (this TapeAction      action,                    string message = null) => action.Logging  .Log      (action,         message);
        public static void LogAction(this TapeAction      action,                    string message = null) => action.Logging  .LogAction(action,         message);
        public static void LogAction(this Buff            entity, string     action, string message = null) => entity.Logging  .LogAction(entity, action, message);
        public static void LogAction(this Buff            entity, ActionEnum action, string message = null) => entity.Logging  .LogAction(entity, action, message);
        public static void LogAction(this byte[]          entity, string     action, string message = null) => Static          .LogAction(entity, action, message);
        public static void LogAction(this byte[]          entity, ActionEnum action, string message = null) => Static          .LogAction(entity, action, message);
        public static void LogAction(this AudioFileOutput entity, string     action, string message = null) => entity.Logging().LogAction(entity, action, message);
        public static void LogAction(this AudioFileOutput entity, ActionEnum action, string message = null) => entity.Logging().LogAction(entity, action, message);
        public static void LogAction(this Sample          entity, string     action, string message = null) => entity.Logging().LogAction(entity, action, message);
        public static void LogAction(this Sample          entity, ActionEnum action, string message = null) => entity.Logging().LogAction(entity, action, message);
        public static void LogAction(this AudioFileOutput entity, SynthWishes synthWishes, string     action, string message = null) => entity.Logging(synthWishes).LogAction(entity, action, message);
        public static void LogAction(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action, string message = null) => entity.Logging(synthWishes).LogAction(entity, action, message);
        public static void LogAction(this Sample          entity, SynthWishes synthWishes, string     action, string message = null) => entity.Logging(synthWishes).LogAction(entity, action, message);
        public static void LogAction(this Sample          entity, SynthWishes synthWishes, ActionEnum action, string message = null) => entity.Logging(synthWishes).LogAction(entity, action, message);

        public static string ActionMessage(this FlowNode        entity, string     action, string message = null) => entity.Logging  .ActionMessage(entity, action, message);
        public static string ActionMessage(this FlowNode        entity, ActionEnum action, string message = null) => entity.Logging  .ActionMessage(entity, action, message);
        public static string ActionMessage(this Tape            entity, string     action, string message = null) => entity.Logging  .ActionMessage(entity, action, message);
        public static string ActionMessage(this Tape            entity, ActionEnum action, string message = null) => entity.Logging  .ActionMessage(entity, action, message);
        /// <inheritdoc cref="_logtapeaction" />
        public static string Message      (this TapeAction      action,                    string message = null) => action.Logging  .Message      (action,         message);
        public static string ActionMessage(this TapeAction      action,                    string message = null) => action.Logging  .ActionMessage(action,         message);
        public static string ActionMessage(this Buff            entity, string     action, string message = null) => entity.Logging  .ActionMessage(entity, action, message);
        public static string ActionMessage(this Buff            entity, ActionEnum action, string message = null) => entity.Logging  .ActionMessage(entity, action, message);
        public static string ActionMessage(this byte[]          entity, string     action, string message = null) => Static          .ActionMessage(entity, action, message);
        public static string ActionMessage(this byte[]          entity, ActionEnum action, string message = null) => Static          .ActionMessage(entity, action, message);
        public static string ActionMessage(this AudioFileOutput entity, string     action, string message = null) => entity.Logging().ActionMessage(entity, action, message);
        public static string ActionMessage(this AudioFileOutput entity, ActionEnum action, string message = null) => entity.Logging().ActionMessage(entity, action, message);
        public static string ActionMessage(this Sample          entity, string     action, string message = null) => entity.Logging().ActionMessage(entity, action, message);
        public static string ActionMessage(this Sample          entity, ActionEnum action, string message = null) => entity.Logging().ActionMessage(entity, action, message);
        public static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, string     action, string message = null) => entity.Logging(synthWishes).ActionMessage(entity, action, message);
        public static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action, string message = null) => entity.Logging(synthWishes).ActionMessage(entity, action, message);
        public static string ActionMessage(this Sample          entity, SynthWishes synthWishes, string     action, string message = null) => entity.Logging(synthWishes).ActionMessage(entity, action, message);
        public static string ActionMessage(this Sample          entity, SynthWishes synthWishes, ActionEnum action, string message = null) => entity.Logging(synthWishes).ActionMessage(entity, action, message);
    }
}
