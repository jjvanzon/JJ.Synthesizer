using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Wishes.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static System.IO.File;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using JJ.Business.Synthesizer.Wishes.Config;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        // LogAction
        
        public void LogAction(FlowNode        entity, ActionEnum action, string message = null) => LogAction(ActionMessage(entity, action, message));
        public void LogAction(FlowNode        entity, string     action, string message = null) => LogAction(ActionMessage(entity, action, message));
        public void LogAction(Tape            entity, ActionEnum action, string message = null) => LogAction(ActionMessage(entity, action, message));
        public void LogAction(Tape            entity, string     action, string message = null) => LogAction(ActionMessage(entity, action, message));
        public void LogAction(TapeAction      entity,                    string message = null) => LogAction(ActionMessage(entity,         message));
        public void LogAction(Buff            entity, ActionEnum action, string message = null) => LogAction(ActionMessage(entity, action, message));
        public void LogAction(Buff            entity, string     action, string message = null) => LogAction(ActionMessage(entity, action, message));
        public void LogAction(AudioFileOutput entity, ActionEnum action, string message = null) => LogAction(ActionMessage(entity, action, message));
        public void LogAction(AudioFileOutput entity, string     action, string message = null) => LogAction(ActionMessage(entity, action, message));
        public void LogAction(Sample          entity, ActionEnum action, string message = null) => LogAction(ActionMessage(entity, action, message));
        public void LogAction(Sample          entity, string     action, string message = null) => LogAction(ActionMessage(entity, action, message));

        /// <inheritdoc cref="_logtapeaction" />
        public void Log(TapeAction entity, string message = null) => LogAction(Message(entity, message));


        public void LogAction(object entity,                                   string message = null) => LogAction(ActionMessage(entity,                 message));
        public void LogAction(object entity,   ActionEnum action,              string message = null) => LogAction(ActionMessage(entity,   action,       message));
        public void LogAction(object entity,   string     action,              string message = null) => LogAction(ActionMessage(entity,   action,       message));
        public void LogAction(object entity,   ActionEnum action, string name, string message = null) => LogAction(ActionMessage(entity,   action, name, message));
        public void LogAction(object entity,   string     action, string name, string message = null) => LogAction(ActionMessage(entity,   action, name, message));
        public void LogAction<TEntity>(                                        string message = null) => LogAction(ActionMessage<TEntity>(               message));
        public void LogAction<TEntity>(        ActionEnum action,              string message = null) => LogAction(ActionMessage<TEntity>( action,       message));
        public void LogAction<TEntity>(        string     action,              string message = null) => LogAction(ActionMessage<TEntity>( action,       message));
        public void LogAction<TEntity>(        ActionEnum action, string name, string message = null) => LogAction(ActionMessage<TEntity>( action, name, message));
        public void LogAction<TEntity>(        string     action, string name, string message = null) => LogAction(ActionMessage<TEntity>( action, name, message));
        public void LogAction(string typeName,                                 string message = null) => LogAction(ActionMessage(typeName,               message));
        public void LogAction(string typeName, ActionEnum action,              string message = null) => LogAction(ActionMessage(typeName, action,       message));
        public void LogAction(string typeName, string     action,              string message = null) => LogAction(ActionMessage(typeName, action,       message));
        public void LogAction(string typeName, ActionEnum action, string name, string message = null) => LogAction(ActionMessage(typeName, action, name, message));
        public void LogAction(string typeName, string     action, string name, string message = null) => LogAction(ActionMessage(typeName, action, name, message));
        public void LogAction(byte[] entity,                                   string message = null) => LogAction(ActionMessage(entity,                 message));
        public void LogAction(byte[] entity,   ActionEnum action,              string message = null) => LogAction(ActionMessage(entity,   action,       message));
        public void LogAction(byte[] entity,   string     action,              string message = null) => LogAction(ActionMessage(entity,   action,       message));
        public void LogAction(byte[] entity,   ActionEnum action, string name, string message = null) => LogAction(ActionMessage(entity,   action, name, message));
        public void LogAction(byte[] entity,   string     action, string name, string message = null) => LogAction(ActionMessage(entity,   action, name, message));

        public void LogFileAction(string filePath, string sourceFilePath = null) => LogAction(FileActionMessage(filePath, sourceFilePath));

        private void LogAction(string actionMessage) => Log("Actions", actionMessage);

        // ActionMessages
        
        public string ActionMessage(FlowNode entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage<Operator>(action, entity.Name, message);
        }
        
        public string ActionMessage(FlowNode entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage<Operator>(action, entity.Name, message);
        }

        public string ActionMessage(Tape entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Descriptor, message);
        }
        
        public string ActionMessage(Tape entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Descriptor, message);
        }

        /// <inheritdoc cref="_logtapeaction" />
        public string Message(TapeAction action, string message = null)
            => ActionMessage(action, message);
        
        public string ActionMessage(TapeAction action, string message = null)
        {
            if (action == null) throw new NullException(() => action);
            return ActionMessage("Actions", action.Type, action.Tape.Descriptor(), message);
        }

        public string ActionMessage(Buff entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message);
        }

        public string ActionMessage(Buff entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message);
        }
        
        public string ActionMessage(AudioFileOutput entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage("Out", action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public string ActionMessage(AudioFileOutput entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage("Out", action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public string ActionMessage(Sample entity, ActionEnum action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public string ActionMessage(Sample entity, string action, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message ?? ConfigLog(entity));
        }

        public string ActionMessage(object entity,                                 string message = null) => ActionMessage(entity, "",     "", message);
        public string ActionMessage(object entity, ActionEnum action,              string message = null) => ActionMessage(entity, action, "", message);
        public string ActionMessage(object entity, string     action,              string message = null) => ActionMessage(entity, action, "", message);
        public string ActionMessage(object entity, ActionEnum action, string name, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.GetType().Name, action, name, message);
        }
        public string ActionMessage(object entity, string     action, string name, string message = null)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.GetType().Name, action, name, message);
        }
        
        
        // ReSharper disable UnusedParameter.Global
        public string ActionMessage(byte[] entity                                                       ) => ActionMessage("Memory", "Write", ""  , message: PrettyByteCount(entity));
        public string ActionMessage(byte[] entity,                                   string message     ) => ActionMessage("Memory", "Write", ""  , message                         );
        public string ActionMessage(byte[] entity,                      string name, int dummy = default) => ActionMessage("Memory", "Write", name, message: PrettyByteCount(entity));
        public string ActionMessage(byte[] entity,                      string name, string message     ) => ActionMessage("Memory", "Write", name, message                         );
        public string ActionMessage(byte[] entity,   string     action, string name, string message     ) => ActionMessage("Memory", action , name, message                         );
        public string ActionMessage(byte[] entity,   ActionEnum action                                  ) => ActionMessage("Memory", action , ""  , message: PrettyByteCount(entity));
        public string ActionMessage(byte[] entity,   ActionEnum action,              string message     ) => ActionMessage("Memory", action , ""  , message                         );
        public string ActionMessage(byte[] entity,   ActionEnum action, string name, int dummy = default) => ActionMessage("Memory", action , name, message: PrettyByteCount(entity));
        public string ActionMessage(byte[] entity,   ActionEnum action, string name, string message     ) => ActionMessage("Memory", action , name, message: PrettyByteCount(entity));

        public string ActionMessage<TEntity>(                                                           ) => ActionMessage(typeof(TEntity).Name, ""    , ""  , ""     );
        public string ActionMessage<TEntity>(                                        string message     ) => ActionMessage(typeof(TEntity).Name, ""    , ""  , message);
        public string ActionMessage<TEntity>(        string     action,              string message     ) => ActionMessage(typeof(TEntity).Name, action, ""  , message);
        public string ActionMessage<TEntity>(        string     action, string name, int dummy = default) => ActionMessage(typeof(TEntity).Name, action, name, ""     );
        public string ActionMessage<TEntity>(        string     action, string name, string message     ) => ActionMessage(typeof(TEntity).Name, action, name, message);
        public string ActionMessage<TEntity>(        ActionEnum action                                  ) => ActionMessage(typeof(TEntity).Name, action, ""  , ""     );
        public string ActionMessage<TEntity>(        ActionEnum action,              string message     ) => ActionMessage(typeof(TEntity).Name, action, ""  , message);
        public string ActionMessage<TEntity>(        ActionEnum action, string name, int dummy = default) => ActionMessage(typeof(TEntity).Name, action, name, ""     );
        public string ActionMessage<TEntity>(        ActionEnum action, string name, string message     ) => ActionMessage(typeof(TEntity).Name, action, name, message);
        
        public string ActionMessage(string typeName                                                     ) => ActionMessage(typeName,             "",       "",   ""     );
        public string ActionMessage(string typeName,                                 string message     ) => ActionMessage(typeName,             "",       "",   message);
        public string ActionMessage(string typeName, string     action,              int dummy = default) => ActionMessage(typeName,             action,   "",   ""     );
        public string ActionMessage(string typeName, string     action,              string message     ) => ActionMessage(typeName,             action,   "",   message);
        public string ActionMessage(string typeName, ActionEnum action                                  ) => ActionMessage(typeName,             action,   "",   ""     );
        public string ActionMessage(string typeName, ActionEnum action,              string message     ) => ActionMessage(typeName,             action,   "",   message);
        public string ActionMessage(string typeName, ActionEnum action, string name, int dummy = default) => ActionMessage(typeName,             action,   name, ""     );
        public string ActionMessage(string typeName, ActionEnum action, string name, string message = "") => ActionMessage(typeName,          $"{action}", name, message);
        // ReSharper restore UnusedParameter.Global

        public string ActionMessage(string typeName, string     action, string name, string message)
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
        
        public string MemoryActionMessage(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            return ActionMessage(tape.Bytes, name: tape.Descriptor);
        }
        
        public string MemoryActionMessage(Buff buff)
        {
            if (buff == null) throw new NullException(() => buff);
            
            if (buff.Tape != null)
            {
                return MemoryActionMessage(buff.Tape);
            }
            else
            {
                return ActionMessage(buff.Bytes);
            }
        }


        public string FileActionMessage(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            
            string formattedFilePath = FormatFilePathIfExists(tape.FilePathResolved);
            if (!Has(formattedFilePath)) return "";
            
            return ActionMessage("File", ActionEnum.Save, formattedFilePath, tape.Descriptor);
        }
                
        public string FileActionMessage(Buff buff)
        {
            if (buff == null) throw new NullException(() => buff);
            
            if (buff.Tape != null)
            {
                return FileActionMessage(buff.Tape);
            }
            else
            {
                return FileActionMessage(buff.FilePath);
            }
        }

        public string FileActionMessage(string filePath, string sourceFilePath = null)
        {
            string formattedFilePath = FormatFilePathIfExists(filePath, sourceFilePath);
            if (!Has(formattedFilePath)) return "";
            
            return ActionMessage("File", ActionEnum.Save, formattedFilePath);
        }
        
        public string FormatFilePathIfExists(string filePath, string sourceFilePath = null)
        {
            if (!Has(filePath)) return "";
            if (!Exists(filePath)) return "";
            string formattedSourceFile = Has(sourceFilePath) ? $" (copied {sourceFilePath})" : "";
            return filePath + formattedSourceFile;
        }
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
        public void LogAction(AudioFileOutput entity, string     action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(AudioFileOutput entity, ActionEnum action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(Sample          entity, string     action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(Sample          entity, ActionEnum action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(byte[]          entity, string     action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(byte[]          entity, ActionEnum action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogOutputFile(string filePath, string sourceFilePath = null)                             => Logging.LogFileAction(filePath, sourceFilePath);
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
        public string ActionMessage(AudioFileOutput entity, string     action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(AudioFileOutput entity, ActionEnum action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(Sample          entity, string     action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
        public string ActionMessage(Sample          entity, ActionEnum action,              string message = null) => Logging.ActionMessage(entity,   action,       message);
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

        public   static void LogOutputFile (this FlowNode       entity, string filePath         , string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogOutputFile (this Tape           entity, string filePath /*= ""*/, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeConfig     entity, string filePath /*= ""*/, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeActions    entity, string filePath /*= ""*/, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeAction     entity, string filePath /*= ""*/, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogOutputFile (this Buff           entity, string filePath /*= ""*/, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        internal static void LogOutputFile (this ConfigResolver entity, string filePath         , string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
    }
}
