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
using JJ.Framework.Wishes.Common;
using static JJ.Business.Synthesizer.Wishes.TapeWishes.ActionEnum;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        // ActionMessages
        
        public string ActionMessage(FlowNode        entity, ActionEnum action                ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(FlowNode        entity, ActionEnum action, string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(FlowNode        entity, string     action                ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(FlowNode        entity, string     message, int dummy = 1) => ActionMessage(entity, "",          message);
        public string ActionMessage(FlowNode        entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage<Operator>(action, entity.Name, message);
        }

        public string ActionMessage(Tape            entity, ActionEnum action                ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(Tape            entity, ActionEnum action, string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(Tape            entity, string     action                ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(Tape            entity, string     message, int dummy = 1) => ActionMessage(entity, "",          message);
        public string ActionMessage(Tape            entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Descriptor, message);
        }

        public string ActionMessage(Buff            entity, ActionEnum action                ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(Buff            entity, ActionEnum action, string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(Buff            entity, string     action                ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(Buff            entity, string     message, int dummy = 1) => ActionMessage(entity, "",          message);
        public string ActionMessage(Buff            entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message);
        }
        
        public string ActionMessage(AudioFileOutput entity, ActionEnum action                ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(AudioFileOutput entity, ActionEnum action, string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(AudioFileOutput entity, string     action                ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(AudioFileOutput entity, string     message, int dummy = 1) => ActionMessage(entity, "",          message);
        public string ActionMessage(AudioFileOutput entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage("Out", action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public string ActionMessage(Sample          entity, ActionEnum action                ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(Sample          entity, ActionEnum action, string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(Sample          entity, string     action                ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(Sample          entity, string     message, int dummy = 1) => ActionMessage(entity, "",          message);
        public string ActionMessage(Sample          entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message ?? ConfigLog(entity));
        }
        
        /// <inheritdoc cref="_logtapeaction" />
        public string Message      (TapeAction      action                ) => ActionMessage(action, "");
        /// <inheritdoc cref="_logtapeaction" />
        public string Message      (TapeAction      action, string message) => ActionMessage(action, message);
        /// <inheritdoc cref="_logtapeaction" />
        public string ActionMessage(TapeAction      action                ) => ActionMessage(action, "");
        /// <inheritdoc cref="_logtapeaction" />
        public string ActionMessage(TapeAction      action, string message)
        {
            if (action == null) throw new NullException(() => action);
            return ActionMessage("Actions", action.Type, action.Tape.Descriptor(), message);
        }

        public string ActionMessage(object entity,   ActionEnum action                                  ) => ActionMessage(entity,               action  , ""  , ""     );
        public string ActionMessage(object entity,   ActionEnum action,              string message     ) => ActionMessage(entity,               action  , ""  , message);
        public string ActionMessage(object entity,   ActionEnum action, string name, int dummy = default) => ActionMessage(entity,            $"{action}", name, ""     );
        public string ActionMessage(object entity,   ActionEnum action, string name, string message     ) => ActionMessage(entity,            $"{action}", name, message);
        public string ActionMessage(object entity                                                       ) => ActionMessage(entity,               ""      , ""  , ""     );
        public string ActionMessage(object entity,                                   string message     ) => ActionMessage(entity,               ""      , ""  , message);
        public string ActionMessage(object entity,   string     action,              int dummy = default) => ActionMessage(entity,               action  , ""  , ""     );
        public string ActionMessage(object entity,   string     action,              string message     ) => ActionMessage(entity,               action  , ""  , message);
        public string ActionMessage(object entity,   string     action, string name, int dummy = default) => ActionMessage(entity,               action  , name, ""     );
        public string ActionMessage(object entity,   string     action, string name, string message     )
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.GetType().Name, action, name, message);
        }

        public string ActionMessage<TEntity>(        ActionEnum action                                  ) => ActionMessage(typeof(TEntity).Name, action  , ""  , ""     );
        public string ActionMessage<TEntity>(        ActionEnum action,              string message     ) => ActionMessage(typeof(TEntity).Name, action  , ""  , message);
        public string ActionMessage<TEntity>(        ActionEnum action, string name, int dummy = default) => ActionMessage(typeof(TEntity).Name, action  , name, ""     );
        public string ActionMessage<TEntity>(        ActionEnum action, string name, string message     ) => ActionMessage(typeof(TEntity).Name, action  , name, message);
        public string ActionMessage<TEntity>(                                                           ) => ActionMessage(typeof(TEntity).Name, ""      , ""  , ""     );
        public string ActionMessage<TEntity>(                                        string message     ) => ActionMessage(typeof(TEntity).Name, ""      , ""  , message);
        public string ActionMessage<TEntity>(        string     action,              int dummy = default) => ActionMessage(typeof(TEntity).Name, action  , ""  , ""     );
        public string ActionMessage<TEntity>(        string     action,              string message     ) => ActionMessage(typeof(TEntity).Name, action  , ""  , message);
        public string ActionMessage<TEntity>(        string     action, string name, int dummy = default) => ActionMessage(typeof(TEntity).Name, action  , name, ""     );
        public string ActionMessage<TEntity>(        string     action, string name, string message     ) => ActionMessage(typeof(TEntity).Name, action  , name, message);
        
        public string ActionMessage(string typeName, ActionEnum action                                  ) => ActionMessage(typeName,             action  , ""  , ""     );
        public string ActionMessage(string typeName, ActionEnum action,              string message     ) => ActionMessage(typeName,             action  , ""  , message);
        public string ActionMessage(string typeName, ActionEnum action, string name, int dummy = default) => ActionMessage(typeName,             action  , name, ""     );
        public string ActionMessage(string typeName, ActionEnum action, string name, string message     ) => ActionMessage(typeName,          $"{action}", name, message);
        public string ActionMessage(string typeName                                                     ) => ActionMessage(typeName,             ""      , ""  , ""     );
        public string ActionMessage(string typeName,                                 string message     ) => ActionMessage(typeName,             ""      , ""  , message);
        public string ActionMessage(string typeName, string     action,              int dummy = default) => ActionMessage(typeName,             action  , ""  , ""     );
        public string ActionMessage(string typeName, string     action,              string message     ) => ActionMessage(typeName,             action  , ""  , message);
        public string ActionMessage(string typeName, string     action, string name, int dummy = default) => ActionMessage(typeName,             action  , name, ""     );
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
                
        public string MemoryActionMessage(int      byteCount                                                     ) => MemoryActionMessage(byteCount, "Write"    , ""  , message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int      byteCount,                                 string message     ) => MemoryActionMessage(byteCount, "Write"    , ""  , message                            );
        public string MemoryActionMessage(int      byteCount, ActionEnum action                                  ) => MemoryActionMessage(byteCount, $"{action}", ""  , message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int      byteCount, ActionEnum action,              string message     ) => MemoryActionMessage(byteCount, $"{action}", ""  , message                            );
        public string MemoryActionMessage(int      byteCount, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(byteCount, $"{action}", name, message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int      byteCount, ActionEnum action, string name, string message     ) => MemoryActionMessage(byteCount, $"{action}", name, message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int      byteCount,                    string name, int dummy = default) => MemoryActionMessage(byteCount, "Write"    , name, message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int      byteCount,                    string name, string message     ) => MemoryActionMessage(byteCount, "Write"    , name, message                            );
        public string MemoryActionMessage(int      byteCount, string     action, string name, string message     )
        {
            return Has(byteCount) ? ActionMessage("Memory", action, name, message) : "";
        }

        public   string MemoryActionMessage(                        byte[] bytes                                                     ) => MemoryActionMessage(bytes?.Length ?? 0                             );
        public   string MemoryActionMessage(                        byte[] bytes,                                 string message     ) => MemoryActionMessage(bytes?.Length ?? 0,               message      );
        public   string MemoryActionMessage(                        byte[] bytes, ActionEnum action                                  ) => MemoryActionMessage(bytes?.Length ?? 0, action                     );   
        public   string MemoryActionMessage(                        byte[] bytes, ActionEnum action,              string message     ) => MemoryActionMessage(bytes?.Length ?? 0, action,       message      );
        public   string MemoryActionMessage(                        byte[] bytes, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(bytes?.Length ?? 0, action, name,         dummy);
        public   string MemoryActionMessage(                        byte[] bytes, ActionEnum action, string name, string message     ) => MemoryActionMessage(bytes?.Length ?? 0, action, name, message      );
        public   string MemoryActionMessage(                        byte[] bytes,                    string name, int dummy = default) => MemoryActionMessage(bytes?.Length ?? 0,         name,         dummy);
        public   string MemoryActionMessage(                        byte[] bytes,                    string name, string message     ) => MemoryActionMessage(bytes?.Length ?? 0,         name, message      );
        public   string MemoryActionMessage(                        byte[] bytes, string     action, string name, string message     ) => MemoryActionMessage(bytes?.Length ?? 0, action, name, message      );
                                                                    
        public   string ActionMessage      (                        byte[] bytes                                                     ) => MemoryActionMessage(bytes                             );
        public   string ActionMessage      (                        byte[] bytes,                                 string message     ) => MemoryActionMessage(bytes,               message      );
        public   string ActionMessage      (                        byte[] bytes, ActionEnum action                                  ) => MemoryActionMessage(bytes, action                     );   
        public   string ActionMessage      (                        byte[] bytes, ActionEnum action,              string message     ) => MemoryActionMessage(bytes, action,       message      );
        public   string ActionMessage      (                        byte[] bytes, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(bytes, action, name,         dummy);
        public   string ActionMessage      (                        byte[] bytes, ActionEnum action, string name, string message     ) => MemoryActionMessage(bytes, action, name, message      );
        public   string ActionMessage      (                        byte[] bytes,                    string name, int dummy = default) => MemoryActionMessage(bytes,         name,         dummy);
        public   string ActionMessage      (                        byte[] bytes,                    string name, string message     ) => MemoryActionMessage(bytes,         name, message      );
        public   string ActionMessage      (                        byte[] bytes, string     action, string name, string message     ) => MemoryActionMessage(bytes, action, name, message      );
                                                                   
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes                                                     ) => MemoryActionMessage(bytes                             );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                                 string message     ) => MemoryActionMessage(bytes,               message      );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action                                  ) => MemoryActionMessage(bytes, action                     );   
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action,              string message     ) => MemoryActionMessage(bytes, action,       message      );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(bytes, action, name,         dummy);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action, string name, string message     ) => MemoryActionMessage(bytes, action, name, message      );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                    string name, int dummy = default) => MemoryActionMessage(bytes,         name,         dummy);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                    string name, string message     ) => MemoryActionMessage(bytes,         name, message      );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, string     action, string name, string message     ) => MemoryActionMessage(bytes, action, name, message      );

        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes                                                     ) => MemoryActionMessage(bytes                             );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                                 string message     ) => MemoryActionMessage(bytes,               message      );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action                                  ) => MemoryActionMessage(bytes, action                     );   
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message     ) => MemoryActionMessage(bytes, action,       message      );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(bytes, action, name,         dummy);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message     ) => MemoryActionMessage(bytes, action, name, message      );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                    string name, int dummy = default) => MemoryActionMessage(bytes,         name,         dummy);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                    string name, string message     ) => MemoryActionMessage(bytes,         name, message      );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, string     action, string name, string message     ) => MemoryActionMessage(bytes, action, name, message      );

        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes                                                     ) => MemoryActionMessage(bytes                             );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                                 string message     ) => MemoryActionMessage(bytes,               message      );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action                                  ) => MemoryActionMessage(bytes, action                     );   
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action,              string message     ) => MemoryActionMessage(bytes, action,       message      );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(bytes, action, name,         dummy);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action, string name, string message     ) => MemoryActionMessage(bytes, action, name, message      );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                    string name, int dummy = default) => MemoryActionMessage(bytes,         name,         dummy);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                    string name, string message     ) => MemoryActionMessage(bytes,         name, message      );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, string     action, string name, string message     ) => MemoryActionMessage(bytes, action, name, message      );
        
        public string MemoryActionMessage(Tape        entity                                                     ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public string MemoryActionMessage(Tape        entity,                                 string message     ) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public string MemoryActionMessage(Tape        entity, ActionEnum action                                  ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public string MemoryActionMessage(Tape        entity, ActionEnum action,              string message     ) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public string MemoryActionMessage(Tape        entity, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public string MemoryActionMessage(Tape        entity, ActionEnum action, string name, string message     ) => MemoryActionMessage(entity, $"{action}", name, message);
        public string MemoryActionMessage(Tape        entity,                    string name, int dummy = default) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public string MemoryActionMessage(Tape        entity,                    string name, string message     ) => MemoryActionMessage(entity, "Write"    , name, message);
        public string MemoryActionMessage(Tape        entity, string     action, string name, string message     )
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Bytes, action, Coalesce(name, entity.Descriptor), Coalesce(message, PrettyByteCount(entity.Bytes)));;
        }

        public string MemoryActionMessage(TapeConfig  entity                                                     ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public string MemoryActionMessage(TapeConfig  entity,                                 string message     ) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public string MemoryActionMessage(TapeConfig  entity, ActionEnum action                                  ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public string MemoryActionMessage(TapeConfig  entity, ActionEnum action,              string message     ) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public string MemoryActionMessage(TapeConfig  entity, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public string MemoryActionMessage(TapeConfig  entity, ActionEnum action, string name, string message     ) => MemoryActionMessage(entity, $"{action}", name, message);
        public string MemoryActionMessage(TapeConfig  entity,                    string name, int dummy = default) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public string MemoryActionMessage(TapeConfig  entity,                    string name, string message     ) => MemoryActionMessage(entity, "Write"    , name, message);
        public string MemoryActionMessage(TapeConfig  entity, string     action, string name, string message     )
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, name, message);
        }
        
        public string MemoryActionMessage(TapeActions entity                                                     ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public string MemoryActionMessage(TapeActions entity,                                 string message     ) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public string MemoryActionMessage(TapeActions entity, ActionEnum action                                  ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public string MemoryActionMessage(TapeActions entity, ActionEnum action,              string message     ) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public string MemoryActionMessage(TapeActions entity, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public string MemoryActionMessage(TapeActions entity, ActionEnum action, string name, string message     ) => MemoryActionMessage(entity, $"{action}", name, message);
        public string MemoryActionMessage(TapeActions entity,                    string name, int dummy = default) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public string MemoryActionMessage(TapeActions entity,                    string name, string message     ) => MemoryActionMessage(entity, "Write"    , name, message);
        public string MemoryActionMessage(TapeActions entity, string     action, string name, string message     )
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, name, message);
        }
        
        public string MemoryActionMessage(TapeAction  entity                                                     ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public string MemoryActionMessage(TapeAction  entity,                                 string message     ) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public string MemoryActionMessage(TapeAction  entity, ActionEnum action                                  ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public string MemoryActionMessage(TapeAction  entity, ActionEnum action,              string message     ) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public string MemoryActionMessage(TapeAction  entity, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public string MemoryActionMessage(TapeAction  entity, ActionEnum action, string name, string message     ) => MemoryActionMessage(entity, $"{action}", name, message);
        public string MemoryActionMessage(TapeAction  entity,                    string name, int dummy = default) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public string MemoryActionMessage(TapeAction  entity,                    string name, string message     ) => MemoryActionMessage(entity, "Write"    , name, message);
        public string MemoryActionMessage(TapeAction  entity, string     action, string name, string message     )
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, name, message);
        }
        
        public string MemoryActionMessage(Buff        entity                                                     ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public string MemoryActionMessage(Buff        entity,                                 string message     ) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public string MemoryActionMessage(Buff        entity, ActionEnum action                                  ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public string MemoryActionMessage(Buff        entity, ActionEnum action,              string message     ) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public string MemoryActionMessage(Buff        entity, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public string MemoryActionMessage(Buff        entity, ActionEnum action, string name, string message     ) => MemoryActionMessage(entity, $"{action}", name, message);
        public string MemoryActionMessage(Buff        entity,                    string name, int dummy = default) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public string MemoryActionMessage(Buff        entity,                    string name, string message     ) => MemoryActionMessage(entity, "Write"    , name, message);
        public string MemoryActionMessage(Buff        entity, string     action, string name, string message     )
        {
            if (entity == null) throw new NullException(() => entity);
            
            if (entity.Tape != null)
            {
                return MemoryActionMessage(entity.Tape, action, name, message);
            }
            else
            {
                return MemoryActionMessage(entity.Bytes, action, name, message);
            }
        }

        public string MemoryActionMessage(Sample      entity                                                     ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public string MemoryActionMessage(Sample      entity,                                 string message     ) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public string MemoryActionMessage(Sample      entity, ActionEnum action                                  ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public string MemoryActionMessage(Sample      entity, ActionEnum action,              string message     ) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public string MemoryActionMessage(Sample      entity, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public string MemoryActionMessage(Sample      entity, ActionEnum action, string name, string message     ) => MemoryActionMessage(entity, $"{action}", name, message);
        public string MemoryActionMessage(Sample      entity,                    string name, int dummy = default) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public string MemoryActionMessage(Sample      entity,                    string name, string message     ) => MemoryActionMessage(entity, "Write"    , name, message);
        public string MemoryActionMessage(Sample      entity, string     action, string name, string message     )
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Bytes, action, name, message);
        }
        
        public string FileActionMessage(string filePath                                                          ) => FileActionMessage(filePath, Save       , ""     , ""            );
        public string FileActionMessage(string filePath,                                    string sourceFilePath) => FileActionMessage(filePath, Save       , ""     , sourceFilePath);
        public string FileActionMessage(string filePath, ActionEnum action                                       ) => FileActionMessage(filePath, $"{action}", ""     , ""            );
        public string FileActionMessage(string filePath, ActionEnum action,                 string sourceFilePath) => FileActionMessage(filePath, $"{action}", ""     , sourceFilePath);
        public string FileActionMessage(string filePath, ActionEnum action, string message, string sourceFilePath) => FileActionMessage(filePath, $"{action}", message, sourceFilePath);
        public string FileActionMessage(string filePath, string     action,                 int dummy = default  ) => FileActionMessage(filePath, action     , ""     , ""            );
        public string FileActionMessage(string filePath, string     action,                 string sourceFilePath) => FileActionMessage(filePath, action     , ""     , sourceFilePath);
        public string FileActionMessage(string filePath, string     action, string message, string sourceFilePath)
        {
            string filePathDescriptor = FormatFilePathIfExists(filePath, sourceFilePath);
            if (!Has(filePathDescriptor)) return "";
            return ActionMessage("File", action, filePathDescriptor, message);
        }
        
        public string FormatFilePathIfExists(string filePath, string sourceFilePath = null)
        {
            if (!Has(filePath)) return "";
            if (!Exists(filePath)) return "";
            string formattedSourceFile = Has(sourceFilePath) ? $" (copied {sourceFilePath})" : "";
            return filePath + formattedSourceFile;
        }

        public string FileActionMessage(Tape entity                                   ) => FileActionMessage(entity, Save       , ""     );
        public string FileActionMessage(Tape entity, ActionEnum action                ) => FileActionMessage(entity, $"{action}", ""     );
        public string FileActionMessage(Tape entity, ActionEnum action, string message) => FileActionMessage(entity, $"{action}", message);
        public string FileActionMessage(Tape entity, string     action                ) => FileActionMessage(entity, action     , ""     );
        public string FileActionMessage(Tape entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            string formattedFilePath = FormatFilePathIfExists(entity.FilePathResolved);
            if (!Has(formattedFilePath)) return "";
            return ActionMessage("File", action, formattedFilePath, message);
        }
                
        public string FileActionMessage(Buff entity                                   ) => FileActionMessage(entity, Save       , ""     );
        public string FileActionMessage(Buff entity, ActionEnum action                ) => FileActionMessage(entity, $"{action}", ""     );
        public string FileActionMessage(Buff entity, ActionEnum action, string message) => FileActionMessage(entity, $"{action}", message);
        public string FileActionMessage(Buff entity, string     action                ) => FileActionMessage(entity, action     , ""     );
        public string FileActionMessage(Buff entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            
            if (entity.Tape != null)
            {
                return FileActionMessage(entity.Tape, action, message);
            }
            else
            {
                return FileActionMessage(entity.FilePath, action, message);
            }
        }
        
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
