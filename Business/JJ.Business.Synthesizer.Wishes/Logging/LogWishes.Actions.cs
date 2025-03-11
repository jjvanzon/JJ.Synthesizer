using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Wishes.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.Config;
using static System.IO.File;
using static System.String;
using static JJ.Business.Synthesizer.Wishes.Logging.LogActions;
using static JJ.Business.Synthesizer.Wishes.Logging.LogCats;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Business.Synthesizer.Wishes.TapeWishes.ActionEnum;
// ReSharper disable UnusedParameter.Global


namespace JJ.Business.Synthesizer.Wishes.Logging
{
    internal partial class LogWishes
    {
        // ActionMessage (On Entities)
        
        public   string ActionMessage(FlowNode        entity, object action                             ) => ActionMessage(entity, action    , ""     );
        public   string ActionMessage(FlowNode        entity, object action,              string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage<Operator>(action, entity.Name, message);
        }
        
        internal string ActionMessage(ConfigResolver  entity, object action                             ) => ActionMessage(entity, action, "", ""     );
        internal string ActionMessage(ConfigResolver  entity, object action,              string message) => ActionMessage(entity, action, "", message);
        internal string ActionMessage(ConfigResolver  entity, object action, string name, string message)
        {
            return ActionMessage(LogCats.Config, action, name, message);
        }
        
        internal string ActionMessage(ConfigSection   entity, object action                             ) => ActionMessage(entity, action, "", ""     );
        internal string ActionMessage(ConfigSection   entity, object action,              string message) => ActionMessage(entity, action, "", message);
        internal string ActionMessage(ConfigSection   entity, object action, string name, string message)
        {
            return ActionMessage(LogCats.Config, action, name, message);
        }
        
        public   string ActionMessage(Tape            entity, object action                             ) => ActionMessage(entity, action    , ""     );
        public   string ActionMessage(Tape            entity, object action,              string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Descriptor, message);
        }
        
        public   string ActionMessage(TapeConfig      entity, object action                             ) => ActionMessage(entity, action    , ""     );
        public   string ActionMessage(TapeConfig      entity, object action,              string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.Tape, action, message);
        }
        
        public   string ActionMessage(TapeActions     entity, object action                             ) => ActionMessage(entity, action    , ""     );
        public   string ActionMessage(TapeActions     entity, object action,              string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.Tape, action, message);
        }
        
        /// <inheritdoc cref="_logtapeaction" />
        public   string Message      (TapeAction      action                                            ) => ActionMessage(        action    , ""     );
        /// <inheritdoc cref="_logtapeaction" />
        public   string Message      (TapeAction      action,                             string message) => ActionMessage(        action    , message);
        /// <inheritdoc cref="_logtapeaction" />
        public   string ActionMessage(TapeAction      action                                            ) => ActionMessage(        action    , ""     );
        /// <inheritdoc cref="_logtapeaction" />
        public   string ActionMessage(TapeAction      action,                             string message)
        {
            if (action == null) throw new NullException(() => action);
            return ActionMessage(Actions, action.Type, action.Tape.Descriptor(), message);
        }
        
        public   string ActionMessage(Buff            entity, object action                             ) => ActionMessage(entity, action    , ""      );
        public   string ActionMessage(Buff            entity, object action,              string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message);
        }
        
        public   string ActionMessage(AudioFileOutput entity, object action                             ) => ActionMessage(entity, action    , ""      );
        public   string ActionMessage(AudioFileOutput entity, object action,              string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage("Out", action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public   string ActionMessage(Sample          entity, object action                             ) => ActionMessage(entity, action    , ""      );
        public   string ActionMessage(Sample          entity, object action,              string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message ?? ConfigLog(entity));
        }

        // ActionMessage (On Simple Types)
        
        public string ActionMessage(object entity                                                   ) => ActionMessage(entity,          ""    , ""  , ""     );
        public string ActionMessage(object entity,        object action                             ) => ActionMessage(entity,          action, ""  , ""     );
        public string ActionMessage(object entity,        object action,              string message) => ActionMessage(entity,          action, ""  , message);
        public string ActionMessage(object entity,        object action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.GetType().Name, action, name, message);
        }

        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj                                            ) => ActionMessage<TEntity>(        ""    , ""  , ""     );
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, object action                             ) => ActionMessage<TEntity>(        action, ""  , ""     );
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, object action,              string message) => ActionMessage<TEntity>(        action, ""  , message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, object action, string name, string message) => ActionMessage<TEntity>(        action, name, message);
        
        public string ActionMessage<TEntity>(                                                       ) => ActionMessage(typeof(TEntity), ""    , ""  , ""     );
        public string ActionMessage<TEntity>(             object action                             ) => ActionMessage(typeof(TEntity), action, ""  , ""     );
        public string ActionMessage<TEntity>(             object action,              string message) => ActionMessage(typeof(TEntity), action, ""  , message);
        public string ActionMessage<TEntity>(             object action, string name, string message) => ActionMessage(typeof(TEntity), action, name, message);

        public string ActionMessage(Type entityType                                                 ) => ActionMessage(entityType,      ""    , ""  , ""     );
        public string ActionMessage(Type entityType,      object action                             ) => ActionMessage(entityType,      action, ""  , ""     );
        public string ActionMessage(Type entityType,      object action,              string message) => ActionMessage(entityType,      action, ""  , message);
        public string ActionMessage(Type entityType,      object action, string name, string message)
        {
            if (entityType == null) throw new NullException(() => entityType);
            return ActionMessage(entityType.Name, action, name, message);
        }
        
        public string ActionMessage(string typeName                                                 ) => ActionMessage(typeName,        ""    , ""  , ""     );
        public string ActionMessage(string typeName,      object action                             ) => ActionMessage(typeName,        action, ""  , ""     );
        public string ActionMessage(string typeName,      object action,              string message) => ActionMessage(typeName,        action, ""  , message);
        public string ActionMessage(string typeName,      object action, string name, string message)
        {
            var elements = new List<string>(10);
            //string text = PrettyTime();
                
            //if (Has(typeName))
            //{
            //    text += " [" + typeName.ToUpper() + "]";
            //}
            
            if (Has(action))
            {
                elements.Add($"{action}");
            }
            
            if (Has(name))
            {
                if (!elements.Last().EndsWithPunctuation()) elements.Add(":");
                elements.Add(@" ");
                elements.Add(@"""");
                elements.Add(name);
                elements.Add(@"""");
            }
            
            if (Has(message))
            {
                if (!elements.Last().EndsWithPunctuation()) elements.Add(":");
                elements.Add(" ");
                elements.Add(message);
            }

            return Join("", elements);
        }

        // Memory Message (On Simple Types)
        
        // (Always tagged [MEMORY] here, so no need for target types: object. entity Type or TEntity.)
        
        public string MemoryActionMessage(int byteCount                                            ) => MemoryActionMessage(byteCount         , Write , ""  , message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int byteCount,                             string message) => MemoryActionMessage(byteCount         , Write , ""  , message                            );
        public string MemoryActionMessage(int byteCount,                string name, string message) => MemoryActionMessage(byteCount         , Write , name, message                            );
        public string MemoryActionMessage(int byteCount, object action, string name, string message)
        {
            if (!Has(byteCount)) return "";
            if (!Logger.WillLog("Memory")) return "";
            return ActionMessage("Memory", action, name, message);
        }

        public string MemoryActionMessage(byte[] bytes                                             ) => MemoryActionMessage(bytes?.Length ?? 0                       );
        public string MemoryActionMessage(byte[] bytes ,                             string message) => MemoryActionMessage(bytes?.Length ?? 0,               message);
        public string MemoryActionMessage(byte[] bytes ,                string name, string message) => MemoryActionMessage(bytes?.Length ?? 0,         name, message);
        public string MemoryActionMessage(byte[] bytes , object action, string name, string message) => MemoryActionMessage(bytes?.Length ?? 0, action, name, message);
                                                                    
        public string       ActionMessage(byte[] bytes                                             ) => MemoryActionMessage(bytes                                    );
        public string       ActionMessage(byte[] bytes ,                             string message) => MemoryActionMessage(bytes             ,               message);
        public string       ActionMessage(byte[] bytes ,                string name, string message) => MemoryActionMessage(bytes             ,         name, message);
        public string       ActionMessage(byte[] bytes , object action, string name, string message) => MemoryActionMessage(bytes             , action, name, message);
        
        // Memory Action Message (On Entities)
        
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes                                            ) => MemoryActionMessage(entity, bytes, Write,        ""     );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                             string message) => MemoryActionMessage(entity, bytes, Write,        message);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, object action,              string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(bytes, action, entity.Name, message);
        }

        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes                                            ) => MemoryActionMessage(        bytes                       );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                             string message) => MemoryActionMessage(        bytes,               message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                string name, string message) => MemoryActionMessage(        bytes,         name, message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, object action, string name, string message) => MemoryActionMessage(        bytes, action, name, message);

        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes                                            ) => MemoryActionMessage(        bytes                       );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                             string message) => MemoryActionMessage(        bytes,               message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                string name, string message) => MemoryActionMessage(        bytes,         name, message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, object action, string name, string message) => MemoryActionMessage(        bytes, action, name, message);
        
        public   string MemoryActionMessage(Tape            entity                               ) => MemoryActionMessage(entity, Write, ""     );
        public   string MemoryActionMessage(Tape            entity,                string message) => MemoryActionMessage(entity, Write, message);
        public   string MemoryActionMessage(Tape            entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Bytes, action, entity.Descriptor, Coalesce(message, PrettyByteCount(entity.Bytes)));
        }

        public   string MemoryActionMessage(TapeConfig      entity                               ) => MemoryActionMessage(entity, Write, ""     );
        public   string MemoryActionMessage(TapeConfig      entity,                string message) => MemoryActionMessage(entity, Write, message);
        public   string MemoryActionMessage(TapeConfig      entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, message);
        }
        
        public   string MemoryActionMessage(TapeActions     entity                               ) => MemoryActionMessage(entity, Write, ""     );
        public   string MemoryActionMessage(TapeActions     entity,                string message) => MemoryActionMessage(entity, Write, message);
        public   string MemoryActionMessage(TapeActions     entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, message);
        }
        
        public   string MemoryActionMessage(TapeAction      entity                               ) => MemoryActionMessage(entity, Write, ""     );
        public   string MemoryActionMessage(TapeAction      entity,                string message) => MemoryActionMessage(entity, Write, message);
        public   string MemoryActionMessage(TapeAction      entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, message);
        }

        public   string MemoryActionMessage(Buff            entity                               ) => MemoryActionMessage(entity, Write, ""     );
        public   string MemoryActionMessage(Buff            entity,                string message) => MemoryActionMessage(entity, Write, message);
        public   string MemoryActionMessage(Buff            entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            
            if (entity.Tape != null)
            {
                return MemoryActionMessage(entity.Tape, action, message);
            }
            else if (entity.UnderlyingAudioFileOutput != null)
            {
                return MemoryActionMessage(entity.UnderlyingAudioFileOutput, entity.Bytes, action, message);
                
            }
            else
            {
                return MemoryActionMessage(entity.Bytes, action, "", message);
            }
        }
        
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes                               ) => MemoryActionMessage(entity, bytes, Write, ""     );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                string message) => MemoryActionMessage(entity, bytes, Write, message);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(bytes, action, entity.Name, message);
        }
        
        public   string MemoryActionMessage(Sample          entity                               ) => MemoryActionMessage(entity, Write, ""     );
        public   string MemoryActionMessage(Sample          entity,                string message) => MemoryActionMessage(entity, Write, message);
        public   string MemoryActionMessage(Sample          entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Bytes, action, entity.Name, message);
        }
        
        // File Message (On Simple Types)
        
        public string FileActionMessage(string filePath                                                      ) => FileActionMessage(filePath, Save  , "", ""            );
        public string FileActionMessage(string filePath,                                string sourceFilePath) => FileActionMessage(filePath, Save  , "", sourceFilePath);
        public string FileActionMessage(string filePath, object action,                 string sourceFilePath) => FileActionMessage(filePath, action, "", sourceFilePath);
        public string FileActionMessage(string filePath, object action, string message, string sourceFilePath)
        {
            if (!Logger.WillLog("File")) return "";
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

        // File Action Messages (On Entities)

        public   string FileActionMessage(FlowNode       entity, string filePath                                                      ) => FileActionMessage(filePath                                 );
        public   string FileActionMessage(FlowNode       entity, string filePath,                                string sourceFilePath) => FileActionMessage(filePath,                  sourceFilePath);
        public   string FileActionMessage(FlowNode       entity, string filePath, object action,                 string sourceFilePath) => FileActionMessage(filePath, action         , sourceFilePath);
        public   string FileActionMessage(FlowNode       entity, string filePath, object action, string message, string sourceFilePath) => FileActionMessage(filePath, action, message, sourceFilePath);

        internal string FileActionMessage(ConfigResolver entity, string filePath                                                      ) => FileActionMessage(filePath                                 );
        internal string FileActionMessage(ConfigResolver entity, string filePath,                                string sourceFilePath) => FileActionMessage(filePath,                  sourceFilePath);
        internal string FileActionMessage(ConfigResolver entity, string filePath, object action,                 string sourceFilePath) => FileActionMessage(filePath, action         , sourceFilePath);
        internal string FileActionMessage(ConfigResolver entity, string filePath, object action, string message, string sourceFilePath) => FileActionMessage(filePath, action, message, sourceFilePath);

        internal string FileActionMessage(ConfigSection  entity, string filePath                                                      ) => FileActionMessage(filePath                                 );
        internal string FileActionMessage(ConfigSection  entity, string filePath,                                string sourceFilePath) => FileActionMessage(filePath,                  sourceFilePath);
        internal string FileActionMessage(ConfigSection  entity, string filePath, object action,                 string sourceFilePath) => FileActionMessage(filePath, action         , sourceFilePath);
        internal string FileActionMessage(ConfigSection  entity, string filePath, object action, string message, string sourceFilePath) => FileActionMessage(filePath, action, message, sourceFilePath);

        public string FileActionMessage(Tape            entity                               ) => FileActionMessage(entity, Save, ""     );
        public string FileActionMessage(Tape            entity,                string message) => FileActionMessage(entity, Save, message);
        public string FileActionMessage(Tape            entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.FilePathResolved, action, message);
        }
        
        public string FileActionMessage(TapeConfig      entity                               ) => FileActionMessage(entity, Save, ""     );
        public string FileActionMessage(TapeConfig      entity,                string message) => FileActionMessage(entity, Save, message);
        public string FileActionMessage(TapeConfig      entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.Tape, action, message);
        }

        public string FileActionMessage(TapeActions     entity                               ) => FileActionMessage(entity, Save, ""     );
        public string FileActionMessage(TapeActions     entity,                string message) => FileActionMessage(entity, Save, message);
        public string FileActionMessage(TapeActions     entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.Tape, action, message);
        }

        public string FileActionMessage(TapeAction      entity                               ) => FileActionMessage(entity, Save, ""     );
        public string FileActionMessage(TapeAction      entity,                string message) => FileActionMessage(entity, Save, message);
        public string FileActionMessage(TapeAction      entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.Tape, Coalesce(action, $"{entity.Type}"), message);
        }
        
        public string FileActionMessage(Buff            entity                               ) => FileActionMessage(entity, Save, ""     );
        public string FileActionMessage(Buff            entity,                string message) => FileActionMessage(entity, Save, message);
        public string FileActionMessage(Buff            entity, object action, string message)
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
                
        public string FileActionMessage(AudioFileOutput entity                               ) => FileActionMessage(entity, Save, ""     );
        public string FileActionMessage(AudioFileOutput entity,                string message) => FileActionMessage(entity, Save, message);
        public string FileActionMessage(AudioFileOutput entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.FilePath, action, message);
        }
                
        public string FileActionMessage(Sample          entity                               ) => FileActionMessage(entity, Save, ""     );
        public string FileActionMessage(Sample          entity,                string message) => FileActionMessage(entity, Save, message);
        public string FileActionMessage(Sample          entity, object action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.Location, action, message);
        }

        // ReSharper restore UnusedParameter.Global
        
        // LogAction (On Entities)
        
        public   FlowNode        LogAction(FlowNode        entity, object action                 ) { LogActionBase(LogCats.Operator, ActionMessage(entity, action               )); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, object action,  string message) { LogActionBase(LogCats.Operator, ActionMessage(entity, action, message      )); return entity; }
                                 
        internal ConfigResolver  LogAction(ConfigResolver  entity                                            ) { LogActionBase(LogCats.Config,   ActionMessage(entity                       )); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, object action                             ) { LogActionBase(LogCats.Config,   ActionMessage(entity, action               )); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, object action,              string message) { LogActionBase(LogCats.Config,   ActionMessage(entity, action, message      )); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, object action, string name, string message) { LogActionBase(LogCats.Config,   ActionMessage(entity, action, name, message)); return entity; }
        
        internal ConfigSection   LogAction(ConfigSection   entity                                            ) { LogActionBase(LogCats.Config,   ActionMessage(entity                       )); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, object action                             ) { LogActionBase(LogCats.Config,   ActionMessage(entity, action               )); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string action,              string message) { LogActionBase(LogCats.Config,   ActionMessage(entity, action, message      )); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string action, string name, string message) { LogActionBase(LogCats.Config,   ActionMessage(entity, action, name, message)); return entity; }

        public   Tape            LogAction(Tape            entity, object action                 ) { LogActionBase(LogCats.Tape, ActionMessage(entity, action         )); return entity; }
        public   Tape            LogAction(Tape            entity, object action,  string message) { LogActionBase(LogCats.Tape, ActionMessage(entity, action, message)); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, object action                 ) { LogActionBase(LogCats.Tape, ActionMessage(entity, action         )); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, object action,  string message) { LogActionBase(LogCats.Tape, ActionMessage(entity, action, message)); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, object action                 ) { LogActionBase(LogCats.Tape, ActionMessage(entity, action         )); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, object action,  string message) { LogActionBase(LogCats.Tape, ActionMessage(entity, action, message)); return entity; }

        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      Log      (TapeAction      action                ) { LogActionBase(LogCats.Actions,       Message(action         )); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      Log      (TapeAction      action, string message) { LogActionBase(LogCats.Actions,       Message(action, message)); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      LogAction(TapeAction      action                ) { LogActionBase(LogCats.Actions, ActionMessage(action         )); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      LogAction(TapeAction      action, string message) { LogActionBase(LogCats.Actions, ActionMessage(action, message)); return action; }

        public   Buff            LogAction(Buff            entity, object action                 ) { LogActionBase(LogCats.Buff,   ActionMessage(entity, action         )); return entity; }
        public   Buff            LogAction(Buff            entity, object action,  string message) { LogActionBase(LogCats.Buff,   ActionMessage(entity, action, message)); return entity; }

        public   AudioFileOutput LogAction(AudioFileOutput entity, object action                 ) { LogActionBase(LogCats.Out,    ActionMessage(entity, action         )); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, object action,  string message) { LogActionBase(LogCats.Out,    ActionMessage(entity, action, message)); return entity; }
        
        public   Sample          LogAction(Sample          entity, object action                 ) { LogActionBase(LogCats.Sample, ActionMessage(entity, action         )); return entity; }
        public   Sample          LogAction(Sample          entity, object action,  string message) { LogActionBase(LogCats.Sample, ActionMessage(entity, action, message)); return entity; }
        
        // LogAction (On Simple Types)

        public void LogAction(object entity                                              ) => LogActionBase(entity,     ActionMessage(entity                           ));
        public void LogAction(object entity,   object action                             ) => LogActionBase(entity,     ActionMessage(entity,     action               ));
        public void LogAction(object entity,   object action,              string message) => LogActionBase(entity,     ActionMessage(entity,     action,       message));
        public void LogAction(object entity,   object action, string name, string message) => LogActionBase(entity,     ActionMessage(entity,     action, name, message));
        public void LogAction<TEntity>(                                                  ) => LogActionBase<TEntity>(   ActionMessage<TEntity>(                        ));
        public void LogAction<TEntity>(        object action                             ) => LogActionBase<TEntity>(   ActionMessage<TEntity>(   action               ));
        public void LogAction<TEntity>(        object action,              string message) => LogActionBase<TEntity>(   ActionMessage<TEntity>(   action,       message));
        public void LogAction<TEntity>(        object action, string name, string message) => LogActionBase<TEntity>(   ActionMessage<TEntity>(   action, name, message));
        public void LogAction(Type entityType                                            ) => LogActionBase(entityType, ActionMessage(entityType                       ));
        public void LogAction(Type entityType, object action                             ) => LogActionBase(entityType, ActionMessage(entityType, action               ));
        public void LogAction(Type entityType, object action,              string message) => LogActionBase(entityType, ActionMessage(entityType, action,       message));
        public void LogAction(Type entityType, object action, string name, string message) => LogActionBase(entityType, ActionMessage(entityType, action, name, message));
        public void LogAction(string typeName                                            ) => LogActionBase(typeName,   ActionMessage(typeName                         ));
        public void LogAction(string typeName, object action                             ) => LogActionBase(typeName,   ActionMessage(typeName,   action               ));
        public void LogAction(string typeName, object action,              string message) => LogActionBase(typeName,   ActionMessage(typeName,   action,       message));
        public void LogAction(string typeName, object action, string name, string message) => LogActionBase(typeName,   ActionMessage(typeName,   action, name, message));
        
        // Memory Action Logging (On Simple Types)
        
        public void   LogMemoryAction(int byteCount                                            ) => LogMemoryActionBase(MemoryActionMessage(byteCount                       ));
        public void   LogMemoryAction(int byteCount,                             string message) => LogMemoryActionBase(MemoryActionMessage(byteCount,               message));
        public void   LogMemoryAction(int byteCount,                string name, string message) => LogMemoryActionBase(MemoryActionMessage(byteCount,         name, message));
        public void   LogMemoryAction(int byteCount, object action, string name, string message) => LogMemoryActionBase(MemoryActionMessage(byteCount, action, name, message));
        public byte[] LogMemoryAction(byte[] bytes                                             )  { LogMemoryActionBase(MemoryActionMessage(bytes                           )); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,                              string message)  { LogMemoryActionBase(MemoryActionMessage(bytes,                   message)); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,                 string name, string message)  { LogMemoryActionBase(MemoryActionMessage(bytes,             name, message)); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,  object action, string name, string message)  { LogMemoryActionBase(MemoryActionMessage(bytes,     action, name, message)); return bytes; }
        public byte[] LogAction      (byte[] bytes                                             )  { LogMemoryActionBase(      ActionMessage(bytes                           )); return bytes; }
        public byte[] LogAction      (byte[] bytes,                              string message)  { LogMemoryActionBase(      ActionMessage(bytes,                   message)); return bytes; }
        public byte[] LogAction      (byte[] bytes,                 string name, string message)  { LogMemoryActionBase(      ActionMessage(bytes,             name, message)); return bytes; }
        public byte[] LogAction      (byte[] bytes,  object action, string name, string message)  { LogMemoryActionBase(      ActionMessage(bytes,     action, name, message)); return bytes; }

        // Memory Action Logging (On Entities)

        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes                               ) { LogMemoryActionBase(MemoryActionMessage(entity, bytes                 )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes,         message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, object action, string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes, action, message)); return entity; }
        
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes                                            ) { LogMemoryActionBase(MemoryActionMessage(entity, bytes                       )); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                             string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes,               message)); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                string name, string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes,         name, message)); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, object action, string name, string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes                                            ) { LogMemoryActionBase(MemoryActionMessage(entity, bytes                       )); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                             string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes,               message)); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                string name, string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes,         name, message)); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, object action, string name, string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }

        public   Tape            LogMemoryAction(Tape            entity                               ) { LogMemoryActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   Tape            LogMemoryAction(Tape            entity,                string message) { LogMemoryActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, object action, string message) { LogMemoryActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity                               ) { LogMemoryActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity,                string message) { LogMemoryActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, object action, string message) { LogMemoryActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity                               ) { LogMemoryActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity,                string message) { LogMemoryActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, object action, string message) { LogMemoryActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity                               ) { LogMemoryActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity,                string message) { LogMemoryActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, object action, string message) { LogMemoryActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   Buff            LogMemoryAction(Buff            entity                               ) { LogMemoryActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   Buff            LogMemoryAction(Buff            entity,                string message) { LogMemoryActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, object action, string message) { LogMemoryActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes                               ) { LogMemoryActionBase(MemoryActionMessage(entity, bytes                 )); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes,                string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes,         message)); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, object action, string message) { LogMemoryActionBase(MemoryActionMessage(entity, bytes, action, message)); return entity; }
        
        public   Sample          LogMemoryAction(Sample          entity                               ) { LogMemoryActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   Sample          LogMemoryAction(Sample          entity,                string message) { LogMemoryActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, object action, string message) { LogMemoryActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        
        // LogFileAction
        
        public   string         LogFileAction(                       string filePath                                                      ) { LogFileActionBase(FileActionMessage(        filePath                                 )); return filePath; }
        public   string         LogFileAction(                       string filePath,                                string sourceFilePath) { LogFileActionBase(FileActionMessage(        filePath,                  sourceFilePath)); return filePath; }
        public   string         LogFileAction(                       string filePath, object action,                 string sourceFilePath) { LogFileActionBase(FileActionMessage(        filePath, action,          sourceFilePath)); return filePath; }
        public   string         LogFileAction(                       string filePath, object action, string message, string sourceFilePath) { LogFileActionBase(FileActionMessage(        filePath, action, message, sourceFilePath)); return filePath; }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath                                                      ) { LogFileActionBase(FileActionMessage(entity, filePath                                 )); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath,                                string sourceFilePath) { LogFileActionBase(FileActionMessage(entity, filePath,                  sourceFilePath)); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, object action,                 string sourceFilePath) { LogFileActionBase(FileActionMessage(entity, filePath, action,          sourceFilePath)); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, object action, string message, string sourceFilePath) { LogFileActionBase(FileActionMessage(entity, filePath, action, message, sourceFilePath)); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath                                                      ) { LogFileActionBase(FileActionMessage(entity, filePath                                 )); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath,                                string sourceFilePath) { LogFileActionBase(FileActionMessage(entity, filePath,                  sourceFilePath)); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, object action,                 string sourceFilePath) { LogFileActionBase(FileActionMessage(entity, filePath, action,          sourceFilePath)); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, object action, string message, string sourceFilePath) { LogFileActionBase(FileActionMessage(entity, filePath, action, message, sourceFilePath)); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath                                                      ) { LogFileActionBase(FileActionMessage(entity, filePath                                 )); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath,                                string sourceFilePath) { LogFileActionBase(FileActionMessage(entity, filePath,                  sourceFilePath)); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, object action,                 string sourceFilePath) { LogFileActionBase(FileActionMessage(entity, filePath, action,          sourceFilePath)); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, object action, string message, string sourceFilePath) { LogFileActionBase(FileActionMessage(entity, filePath, action, message, sourceFilePath)); return entity;   }

        public Tape            LogFileAction(Tape            entity                               ) { LogFileActionBase(FileActionMessage(entity                 )); return entity; }
        public Tape            LogFileAction(Tape            entity,                string message) { LogFileActionBase(FileActionMessage(entity,         message)); return entity; }
        public Tape            LogFileAction(Tape            entity, object action, string message) { LogFileActionBase(FileActionMessage(entity, action, message)); return entity; }
        public TapeConfig      LogFileAction(TapeConfig      entity                               ) { LogFileActionBase(FileActionMessage(entity                 )); return entity; }
        public TapeConfig      LogFileAction(TapeConfig      entity,                string message) { LogFileActionBase(FileActionMessage(entity,         message)); return entity; }
        public TapeConfig      LogFileAction(TapeConfig      entity, object action, string message) { LogFileActionBase(FileActionMessage(entity, action, message)); return entity; }
        public TapeActions     LogFileAction(TapeActions     entity                               ) { LogFileActionBase(FileActionMessage(entity                 )); return entity; }
        public TapeActions     LogFileAction(TapeActions     entity,                string message) { LogFileActionBase(FileActionMessage(entity,         message)); return entity; }
        public TapeActions     LogFileAction(TapeActions     entity, object action, string message) { LogFileActionBase(FileActionMessage(entity, action, message)); return entity; }
        public TapeAction      LogFileAction(TapeAction      entity                               ) { LogFileActionBase(FileActionMessage(entity                 )); return entity; }
        public TapeAction      LogFileAction(TapeAction      entity,                string message) { LogFileActionBase(FileActionMessage(entity,         message)); return entity; }
        public TapeAction      LogFileAction(TapeAction      entity, object action, string message) { LogFileActionBase(FileActionMessage(entity, action, message)); return entity; }
        public Buff            LogFileAction(Buff            entity                               ) { LogFileActionBase(FileActionMessage(entity                 )); return entity; }
        public Buff            LogFileAction(Buff            entity,                string message) { LogFileActionBase(FileActionMessage(entity,         message)); return entity; }
        public Buff            LogFileAction(Buff            entity, object action, string message) { LogFileActionBase(FileActionMessage(entity, action, message)); return entity; }
        public AudioFileOutput LogFileAction(AudioFileOutput entity                               ) { LogFileActionBase(FileActionMessage(entity                 )); return entity; }
        public AudioFileOutput LogFileAction(AudioFileOutput entity,                string message) { LogFileActionBase(FileActionMessage(entity,         message)); return entity; }
        public AudioFileOutput LogFileAction(AudioFileOutput entity, object action, string message) { LogFileActionBase(FileActionMessage(entity, action, message)); return entity; }
        public Sample          LogFileAction(Sample          entity                               ) { LogFileActionBase(FileActionMessage(entity                 )); return entity; }
        public Sample          LogFileAction(Sample          entity,                string message) { LogFileActionBase(FileActionMessage(entity,         message)); return entity; }
        public Sample          LogFileAction(Sample          entity, object action, string message) { LogFileActionBase(FileActionMessage(entity, action, message)); return entity; }
        
        private void LogFileActionBase(                     string actionMessage) => Log(LogCats.File,         actionMessage);
        private void LogMemoryActionBase(                   string actionMessage) => Log(LogCats.Memory,       actionMessage);
        private void LogActionBase(string category,         string actionMessage) => Log(category,             actionMessage);
        private void LogActionBase<TEntity>(                string actionMessage) => Log(typeof(TEntity).Name, actionMessage);
        // ReSharper disable once UnusedParameter.Local
        private void LogActionBase<TEntity>(TEntity entity, string actionMessage) => Log(typeof(TEntity).Name, actionMessage);
        private void LogActionBase(Type entityType,         string actionMessage)
        {
            if (entityType == null) throw new NullException(() => entityType);
            Log(entityType.Name, actionMessage);
        }
    }
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // ActionMessage (On Entities)
        
        public   string ActionMessage(FlowNode        entity, object action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(FlowNode        entity, object action,              string message) => Logging.ActionMessage(entity,   action,       message);
        internal string ActionMessage(ConfigResolver  entity                                            ) => Logging.ActionMessage(entity                         );
        internal string ActionMessage(ConfigResolver  entity, object action                             ) => Logging.ActionMessage(entity,   action               );
        internal string ActionMessage(ConfigResolver  entity, object action,              string message) => Logging.ActionMessage(entity,   action,       message);
        internal string ActionMessage(ConfigResolver  entity, object action, string name, string message) => Logging.ActionMessage(entity,   action, name, message);
        internal string ActionMessage(ConfigSection   entity                                            ) => Logging.ActionMessage(entity                         );
        internal string ActionMessage(ConfigSection   entity, object action                             ) => Logging.ActionMessage(entity,   action               );
        internal string ActionMessage(ConfigSection   entity, object action,              string message) => Logging.ActionMessage(entity,   action,       message);
        internal string ActionMessage(ConfigSection   entity, object action, string name, string message) => Logging.ActionMessage(entity,   action, name, message);
        public   string ActionMessage(Tape            entity, object action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(Tape            entity, object action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(TapeActions     entity, object action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(TapeActions     entity, object action,              string message) => Logging.ActionMessage(entity,   action,       message);
        /// <inheritdoc cref="_logtapeaction" />
        public   string Message      (TapeAction      action                                            ) => Logging.ActionMessage(          action               );
        /// <inheritdoc cref="_logtapeaction" />
        public   string Message      (TapeAction      action,                             string message) => Logging.ActionMessage(          action,       message);
        /// <inheritdoc cref="_logtapeaction" />
        public   string ActionMessage(TapeAction      action                                            ) => Logging.ActionMessage(          action               );
        /// <inheritdoc cref="_logtapeaction" />
        public   string ActionMessage(TapeAction      action,                             string message) => Logging.ActionMessage(          action,       message);
        public   string ActionMessage(Buff            entity, object action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(Buff            entity, object action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(AudioFileOutput entity, object action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(AudioFileOutput entity, object action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(Sample          entity, object action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(Sample          entity, object action,              string message) => Logging.ActionMessage(entity,   action,       message);

        // ActionMessage (On Simple Types)
        
        public   string ActionMessage(object entity                                                     ) => Logging.ActionMessage(entity                          );
        public   string ActionMessage(object entity,          object action                             ) => Logging.ActionMessage(entity,    action               );
        public   string ActionMessage(object entity,          object action,              string message) => Logging.ActionMessage(entity,    action,       message);
        public   string ActionMessage(object entity,          object action, string name, string message) => Logging.ActionMessage(entity,    action, name, message);

        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj                                             ) => Logging.ActionMessage(obj                              );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj,  object action                             ) => Logging.ActionMessage(obj,        action               );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj,  object action,              string message) => Logging.ActionMessage(obj,        action,       message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj,  object action, string name, string message) => Logging.ActionMessage(obj,        action, name, message);
        public   string ActionMessage<TEntity>(                                                        ) => Logging.ActionMessage<TEntity>(                        );
        public   string ActionMessage<TEntity>(              object action                             ) => Logging.ActionMessage<TEntity>(   action               );
        public   string ActionMessage<TEntity>(              object action,              string message) => Logging.ActionMessage<TEntity>(   action,       message);
        public   string ActionMessage<TEntity>(              object action, string name, string message) => Logging.ActionMessage<TEntity>(   action, name, message);
        public   string ActionMessage(Type entityType                                                  ) => Logging.ActionMessage(entityType                       );
        public   string ActionMessage(Type entityType,       object action                             ) => Logging.ActionMessage(entityType, action               );
        public   string ActionMessage(Type entityType,       object action,              string message) => Logging.ActionMessage(entityType, action,       message);
        public   string ActionMessage(Type entityType,       object action, string name, string message) => Logging.ActionMessage(entityType, action, name, message);
        public   string ActionMessage(string typeName                                                  ) => Logging.ActionMessage(typeName                         );
        public   string ActionMessage(string typeName,       object action                             ) => Logging.ActionMessage(typeName,   action               );
        public   string ActionMessage(string typeName,       object action,              string message) => Logging.ActionMessage(typeName,   action,       message);
        public   string ActionMessage(string typeName,       object action, string name, string message) => Logging.ActionMessage(typeName,   action, name, message);

        // Memory Message (On Simple Types)

        // (Always tagged [MEMORY] here, so no need for target types: object. entity Type or TEntity.)
                
        public string MemoryActionMessage(int byteCount                                            ) => Logging.MemoryActionMessage(byteCount                       );
        public string MemoryActionMessage(int byteCount,                             string message) => Logging.MemoryActionMessage(byteCount,               message);
        public string MemoryActionMessage(int byteCount,                string name, string message) => Logging.MemoryActionMessage(byteCount,         name, message);
        public string MemoryActionMessage(int byteCount, object action, string name, string message) => Logging.MemoryActionMessage(byteCount, action, name, message);
        public string MemoryActionMessage(byte[] bytes                                             ) => Logging.MemoryActionMessage(bytes                           );
        public string MemoryActionMessage(byte[] bytes,                              string message) => Logging.MemoryActionMessage(bytes,                   message);
        public string MemoryActionMessage(byte[] bytes,                 string name, string message) => Logging.MemoryActionMessage(bytes,             name, message);
        public string MemoryActionMessage(byte[] bytes,  object action, string name, string message) => Logging.MemoryActionMessage(bytes,     action, name, message);
        public string ActionMessage      (byte[] bytes                                             ) => Logging.      ActionMessage(bytes                           );
        public string ActionMessage      (byte[] bytes,                              string message) => Logging.      ActionMessage(bytes,                   message);
        public string ActionMessage      (byte[] bytes,                 string name, string message) => Logging.      ActionMessage(bytes,             name, message);
        public string ActionMessage      (byte[] bytes,  object action, string name, string message) => Logging.      ActionMessage(bytes,     action, name, message);
        
        // Memory Action Message (On Entities)
        
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes                                            ) => Logging.MemoryActionMessage(entity, bytes                       );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                             string message) => Logging.MemoryActionMessage(entity, bytes,               message);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, object action,              string message) => Logging.MemoryActionMessage(entity, bytes, action,       message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes                                            ) => Logging.MemoryActionMessage(entity, bytes                       );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                             string message) => Logging.MemoryActionMessage(entity, bytes,               message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                string name, string message) => Logging.MemoryActionMessage(entity, bytes,         name, message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, object action, string name, string message) => Logging.MemoryActionMessage(entity, bytes, action, name, message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes                                            ) => Logging.MemoryActionMessage(entity, bytes                       );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                             string message) => Logging.MemoryActionMessage(entity, bytes,               message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                string name, string message) => Logging.MemoryActionMessage(entity, bytes,         name, message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, object action, string name, string message) => Logging.MemoryActionMessage(entity, bytes, action, name, message);
        
        public   string MemoryActionMessage(Tape            entity                                                          ) => Logging.MemoryActionMessage(entity                              );
        public   string MemoryActionMessage(Tape            entity,                                           string message) => Logging.MemoryActionMessage(entity,                      message);
        public   string MemoryActionMessage(Tape            entity,               object action,              string message) => Logging.MemoryActionMessage(entity,        action,       message);
        public   string MemoryActionMessage(TapeConfig      entity                                                          ) => Logging.MemoryActionMessage(entity                              );
        public   string MemoryActionMessage(TapeConfig      entity,                                           string message) => Logging.MemoryActionMessage(entity,                      message);
        public   string MemoryActionMessage(TapeConfig      entity,               object action,              string message) => Logging.MemoryActionMessage(entity,        action,       message);
        public   string MemoryActionMessage(TapeActions     entity                                                          ) => Logging.MemoryActionMessage(entity                              );
        public   string MemoryActionMessage(TapeActions     entity,                                           string message) => Logging.MemoryActionMessage(entity,                      message);
        public   string MemoryActionMessage(TapeActions     entity,               object action,              string message) => Logging.MemoryActionMessage(entity,        action,       message);
        public   string MemoryActionMessage(TapeAction      entity                                                          ) => Logging.MemoryActionMessage(entity                              );
        public   string MemoryActionMessage(TapeAction      entity,                                           string message) => Logging.MemoryActionMessage(entity,                      message);
        public   string MemoryActionMessage(TapeAction      entity,               object action,              string message) => Logging.MemoryActionMessage(entity,        action,       message);
        public   string MemoryActionMessage(Buff            entity                                                          ) => Logging.MemoryActionMessage(entity                              );
        public   string MemoryActionMessage(Buff            entity,                                           string message) => Logging.MemoryActionMessage(entity,                      message);
        public   string MemoryActionMessage(Buff            entity,               object action,              string message) => Logging.MemoryActionMessage(entity,        action,       message);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes                                            ) => Logging.MemoryActionMessage(entity, bytes                       );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                             string message) => Logging.MemoryActionMessage(entity, bytes,               message);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, object action,              string message) => Logging.MemoryActionMessage(entity, bytes, action,       message);
        public   string MemoryActionMessage(Sample          entity                                                          ) => Logging.MemoryActionMessage(entity                              );
        public   string MemoryActionMessage(Sample          entity,                                           string message) => Logging.MemoryActionMessage(entity,                      message);
        public   string MemoryActionMessage(Sample          entity, object action,                            string message) => Logging.MemoryActionMessage(entity,        action,       message);
        
        // File Message (On Simple Types)

        public string FileActionMessage(string filePath                                                      ) => Logging.FileActionMessage(filePath                                 );
        public string FileActionMessage(string filePath,                                string sourceFilePath) => Logging.FileActionMessage(filePath,                  sourceFilePath);
        public string FileActionMessage(string filePath, object action,                 string sourceFilePath) => Logging.FileActionMessage(filePath, action,          sourceFilePath);
        public string FileActionMessage(string filePath, object action, string message, string sourceFilePath) => Logging.FileActionMessage(filePath, action, message, sourceFilePath);       
                
        // File Action Messages (On Entities)

        public   string FileActionMessage(FlowNode        entity, string filePath                                                      ) => Logging.FileActionMessage(entity, filePath                                 );
        public   string FileActionMessage(FlowNode        entity, string filePath,                                string sourceFilePath) => Logging.FileActionMessage(entity, filePath,                  sourceFilePath);
        public   string FileActionMessage(FlowNode        entity, string filePath, object action,                 string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action,          sourceFilePath);
        public   string FileActionMessage(FlowNode        entity, string filePath, object action, string message, string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        internal string FileActionMessage(ConfigResolver  entity, string filePath                                                      ) => Logging.FileActionMessage(entity, filePath                                 );
        internal string FileActionMessage(ConfigResolver  entity, string filePath,                                string sourceFilePath) => Logging.FileActionMessage(entity, filePath,                  sourceFilePath);
        internal string FileActionMessage(ConfigResolver  entity, string filePath, object action,                 string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal string FileActionMessage(ConfigResolver  entity, string filePath, object action, string message, string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        internal string FileActionMessage(ConfigSection   entity, string filePath                                                      ) => Logging.FileActionMessage(entity, filePath                                 );
        internal string FileActionMessage(ConfigSection   entity, string filePath,                                string sourceFilePath) => Logging.FileActionMessage(entity, filePath,                  sourceFilePath);
        internal string FileActionMessage(ConfigSection   entity, string filePath, object action,                 string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal string FileActionMessage(ConfigSection   entity, string filePath, object action, string message, string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        public   string FileActionMessage(Tape            entity                               ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(Tape            entity,                string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(Tape            entity, object action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(TapeConfig      entity                               ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(TapeConfig      entity,                string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(TapeConfig      entity, object action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(TapeActions     entity                               ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(TapeActions     entity,                string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(TapeActions     entity, object action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(TapeAction      entity                               ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(TapeAction      entity,                string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(TapeAction      entity, object action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(Buff            entity                               ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(Buff            entity,                string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(Buff            entity, object action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(AudioFileOutput entity                               ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(AudioFileOutput entity,                string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(AudioFileOutput entity, object action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(Sample          entity                               ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(Sample          entity,                string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(Sample          entity, object action, string message) => Logging.FileActionMessage(entity, action, message);
        
        // LogAction
        
        public   FlowNode        LogAction(FlowNode        entity, object action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, object action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        
        internal ConfigResolver  LogAction(ConfigResolver  entity                                            ) { Logging.LogAction(entity                       ); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, object action                             ) { Logging.LogAction(entity, action               ); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, object action,              string message) { Logging.LogAction(entity, action,       message); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, object action, string name, string message) { Logging.LogAction(entity, action, name, message); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity                                            ) { Logging.LogAction(entity                       ); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, object action                             ) { Logging.LogAction(entity, action               ); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, object action,              string message) { Logging.LogAction(entity, action,       message); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, object action, string name, string message) { Logging.LogAction(entity, action, name, message); return entity; }
        
        public   Tape            LogAction(Tape            entity, object action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   Tape            LogAction(Tape            entity, object action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, object action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, object action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, object action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, object action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      Log      (TapeAction      action                ) { Logging.Log      (action         ); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      Log      (TapeAction      action, string message) { Logging.Log      (action, message); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      LogAction(TapeAction      action                ) { Logging.LogAction(action         ); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      LogAction(TapeAction      action, string message) { Logging.LogAction(action, message); return action; }        
        
        public   Buff            LogAction(Buff            entity, object action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   Buff            LogAction(Buff            entity, object action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, object action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, object action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   Sample          LogAction(Sample          entity, object action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   Sample          LogAction(Sample          entity, object action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        
        // LogAction (On Simple Types)
        
        public void LogAction(object entity                                              ) => Logging.LogAction(entity                           );
        public void LogAction(object entity,   object action                             ) => Logging.LogAction(entity,     action               );
        public void LogAction(object entity,   object action,              string message) => Logging.LogAction(entity,     action,       message);
        public void LogAction(object entity,   object action, string name, string message) => Logging.LogAction(entity,     action, name, message);
        public void LogAction<TEntity>(                                                  ) => Logging.LogAction<TEntity>(                        );
        public void LogAction<TEntity>(        object action                             ) => Logging.LogAction<TEntity>(   action               );
        public void LogAction<TEntity>(        object action,              string message) => Logging.LogAction<TEntity>(   action,       message);
        public void LogAction<TEntity>(        object action, string name, string message) => Logging.LogAction<TEntity>(   action, name, message);
        public void LogAction(Type entityType                                            ) => Logging.LogAction(entityType                       );
        public void LogAction(Type entityType, object action                             ) => Logging.LogAction(entityType, action               );
        public void LogAction(Type entityType, object action,              string message) => Logging.LogAction(entityType, action,       message);
        public void LogAction(Type entityType, object action, string name, string message) => Logging.LogAction(entityType, action, name, message);
        public void LogAction(string typeName                                            ) => Logging.LogAction(typeName                         );
        public void LogAction(string typeName, object action                             ) => Logging.LogAction(typeName,   action               );
        public void LogAction(string typeName, object action,              string message) => Logging.LogAction(typeName,   action,       message);
        public void LogAction(string typeName, object action, string name, string message) => Logging.LogAction(typeName,   action, name, message);

        // Memory Action Logging (On Simple Types)

        public void   LogMemoryAction(int byteCount                                            ) => Logging.LogMemoryAction(byteCount                       );
        public void   LogMemoryAction(int byteCount,                             string message) => Logging.LogMemoryAction(byteCount,               message);
        public void   LogMemoryAction(int byteCount,                string name, string message) => Logging.LogMemoryAction(byteCount,         name, message);
        public void   LogMemoryAction(int byteCount, object action, string name, string message) => Logging.LogMemoryAction(byteCount, action, name, message);
        public byte[] LogMemoryAction(byte[] bytes                                             ) => Logging.LogMemoryAction(bytes                           );
        public byte[] LogMemoryAction(byte[] bytes,                              string message) => Logging.LogMemoryAction(bytes,                   message);
        public byte[] LogMemoryAction(byte[] bytes,                 string name, string message) => Logging.LogMemoryAction(bytes,             name, message);
        public byte[] LogMemoryAction(byte[] bytes,  object action, string name, string message) => Logging.LogMemoryAction(bytes,     action, name, message);
        public byte[] LogAction      (byte[] bytes                                             ) => Logging.LogAction      (bytes                           );
        public byte[] LogAction      (byte[] bytes,                              string message) => Logging.LogAction      (bytes,                   message);
        public byte[] LogAction      (byte[] bytes,                 string name, string message) => Logging.LogAction      (bytes,             name, message);
        public byte[] LogAction      (byte[] bytes,  object action, string name, string message) => Logging.LogAction      (bytes,     action, name, message);

        // Memory Action Logging (On Entities)
        
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes                               ) => Logging.LogMemoryAction(entity, bytes                 );
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                string message) => Logging.LogMemoryAction(entity, bytes,         message);
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, object action, string message) => Logging.LogMemoryAction(entity, bytes, action, message);

        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes                                            ) => Logging.LogMemoryAction(entity, bytes                       );
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                             string message) => Logging.LogMemoryAction(entity, bytes,               message);
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                string name, string message) => Logging.LogMemoryAction(entity, bytes,         name, message);
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, object action, string name, string message) => Logging.LogMemoryAction(entity, bytes, action, name, message);
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes                                            ) => Logging.LogMemoryAction(entity, bytes                       );
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                             string message) => Logging.LogMemoryAction(entity, bytes,               message);
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                string name, string message) => Logging.LogMemoryAction(entity, bytes,         name, message);
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, object action, string name, string message) => Logging.LogMemoryAction(entity, bytes, action, name, message);

        public   Tape            LogMemoryAction(Tape            entity                               ) => Logging.LogMemoryAction(entity                 );
        public   Tape            LogMemoryAction(Tape            entity,                string message) => Logging.LogMemoryAction(entity,         message);
        public   Tape            LogMemoryAction(Tape            entity, object action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   TapeConfig      LogMemoryAction(TapeConfig      entity                               ) => Logging.LogMemoryAction(entity                 );
        public   TapeConfig      LogMemoryAction(TapeConfig      entity,                string message) => Logging.LogMemoryAction(entity,         message);
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, object action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   TapeActions     LogMemoryAction(TapeActions     entity                               ) => Logging.LogMemoryAction(entity                 );
        public   TapeActions     LogMemoryAction(TapeActions     entity,                string message) => Logging.LogMemoryAction(entity,         message);
        public   TapeActions     LogMemoryAction(TapeActions     entity, object action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   TapeAction      LogMemoryAction(TapeAction      entity                               ) => Logging.LogMemoryAction(entity                 );
        public   TapeAction      LogMemoryAction(TapeAction      entity,                string message) => Logging.LogMemoryAction(entity,         message);
        public   TapeAction      LogMemoryAction(TapeAction      entity, object action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   Buff            LogMemoryAction(Buff            entity                               ) => Logging.LogMemoryAction(entity                 );
        public   Buff            LogMemoryAction(Buff            entity,                string message) => Logging.LogMemoryAction(entity,         message);
        public   Buff            LogMemoryAction(Buff            entity, object action, string message) => Logging.LogMemoryAction(entity, action, message);
        
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes                               ) => Logging.LogMemoryAction(entity, bytes                 );
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes,                string message) => Logging.LogMemoryAction(entity, bytes,         message);
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, object action, string message) => Logging.LogMemoryAction(entity, bytes, action, message);
        
        public   Sample          LogMemoryAction(Sample          entity                               ) => Logging.LogMemoryAction(entity                 );
        public   Sample          LogMemoryAction(Sample          entity,                string message) => Logging.LogMemoryAction(entity,         message);
        public   Sample          LogMemoryAction(Sample          entity, object action, string message) => Logging.LogMemoryAction(entity, action, message);

        // LogFileAction
        
        public   string         LogFileAction(                       string filePath                                                      ) => Logging.LogFileAction(        filePath                                 );
        public   string         LogFileAction(                       string filePath,                                string sourceFilePath) => Logging.LogFileAction(        filePath,                  sourceFilePath);
        public   string         LogFileAction(                       string filePath, object action,                 string sourceFilePath) => Logging.LogFileAction(        filePath, action,          sourceFilePath);
        public   string         LogFileAction(                       string filePath, object action, string message, string sourceFilePath) => Logging.LogFileAction(        filePath, action, message, sourceFilePath);
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath                                                      ) => Logging.LogFileAction(entity, filePath                                 );
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath,                                string sourceFilePath) => Logging.LogFileAction(entity, filePath,                  sourceFilePath);
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, object action,                 string sourceFilePath) => Logging.LogFileAction(entity, filePath, action,          sourceFilePath);
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, object action, string message, string sourceFilePath) => Logging.LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath                                                      ) => Logging.LogFileAction(entity, filePath                                 );
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath,                                string sourceFilePath) => Logging.LogFileAction(entity, filePath,                  sourceFilePath);
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, object action,                 string sourceFilePath) => Logging.LogFileAction(entity, filePath, action,          sourceFilePath);
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, object action, string message, string sourceFilePath) => Logging.LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath                                                      ) => Logging.LogFileAction(entity, filePath                                 );
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath,                                string sourceFilePath) => Logging.LogFileAction(entity, filePath,                  sourceFilePath);
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, object action,                 string sourceFilePath) => Logging.LogFileAction(entity, filePath, action,          sourceFilePath);
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, object action, string message, string sourceFilePath) => Logging.LogFileAction(entity, filePath, action, message, sourceFilePath);
        
        public Tape            LogFileAction(Tape            entity                               ) => Logging.LogFileAction(entity                 );
        public Tape            LogFileAction(Tape            entity,                string message) => Logging.LogFileAction(entity,         message);
        public Tape            LogFileAction(Tape            entity, object action, string message) => Logging.LogFileAction(entity, action, message);
        public TapeConfig      LogFileAction(TapeConfig      entity                               ) => Logging.LogFileAction(entity                 );
        public TapeConfig      LogFileAction(TapeConfig      entity,                string message) => Logging.LogFileAction(entity,         message);
        public TapeConfig      LogFileAction(TapeConfig      entity, object action, string message) => Logging.LogFileAction(entity, action, message);
        public TapeActions     LogFileAction(TapeActions     entity                               ) => Logging.LogFileAction(entity                 );
        public TapeActions     LogFileAction(TapeActions     entity,                string message) => Logging.LogFileAction(entity,         message);
        public TapeActions     LogFileAction(TapeActions     entity, object action, string message) => Logging.LogFileAction(entity, action, message);
        public TapeAction      LogFileAction(TapeAction      entity                               ) => Logging.LogFileAction(entity                 );
        public TapeAction      LogFileAction(TapeAction      entity,                string message) => Logging.LogFileAction(entity,         message);
        public TapeAction      LogFileAction(TapeAction      entity, object action, string message) => Logging.LogFileAction(entity, action, message);
        public Buff            LogFileAction(Buff            entity                               ) => Logging.LogFileAction(entity                 );
        public Buff            LogFileAction(Buff            entity,                string message) => Logging.LogFileAction(entity,         message);
        public Buff            LogFileAction(Buff            entity, object action, string message) => Logging.LogFileAction(entity, action, message);
        public AudioFileOutput LogFileAction(AudioFileOutput entity                               ) => Logging.LogFileAction(entity                 );
        public AudioFileOutput LogFileAction(AudioFileOutput entity,                string message) => Logging.LogFileAction(entity,         message);
        public AudioFileOutput LogFileAction(AudioFileOutput entity, object action, string message) => Logging.LogFileAction(entity, action, message);
        public Sample          LogFileAction(Sample          entity                               ) => Logging.LogFileAction(entity                 );
        public Sample          LogFileAction(Sample          entity,                string message) => Logging.LogFileAction(entity,         message);
        public Sample          LogFileAction(Sample          entity, object action, string message) => Logging.LogFileAction(entity, action, message);
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{

    
    public static partial class LogExtensionWishes
    {
        // ActionMessage (On Entities)
        
        public   static string ActionMessage(this FlowNode        entity, object action                ) => ResolveLogging(entity).ActionMessage(entity, action         );
        public   static string ActionMessage(this FlowNode        entity, object action, string message) => ResolveLogging(entity).ActionMessage(entity, action, message);
        public   static string ActionMessage(this Tape            entity, object action                ) => ResolveLogging(entity).ActionMessage(entity, action         );
        public   static string ActionMessage(this Tape            entity, object action, string message) => ResolveLogging(entity).ActionMessage(entity, action, message);
        public   static string ActionMessage(this TapeActions     entity, object action                ) => ResolveLogging(entity).ActionMessage(entity, action         );
        public   static string ActionMessage(this TapeActions     entity, object action, string message) => ResolveLogging(entity).ActionMessage(entity, action, message);
        /// <inheritdoc cref="_logtapeaction" />
        public   static string Message      (this TapeAction      action                               ) => ResolveLogging(action).ActionMessage(action                 );
        /// <inheritdoc cref="_logtapeaction" />
        public   static string Message      (this TapeAction      action,                string message) => ResolveLogging(action).ActionMessage(action,         message);
        /// <inheritdoc cref="_logtapeaction" />
        public   static string ActionMessage(this TapeAction      action                               ) => ResolveLogging(action).ActionMessage(action                 );
        /// <inheritdoc cref="_logtapeaction" />
        public   static string ActionMessage(this TapeAction      action,                string message) => ResolveLogging(action).ActionMessage(action,         message);
        public   static string ActionMessage(this Buff            entity, object action                ) => ResolveLogging(entity).ActionMessage(entity, action         );
        public   static string ActionMessage(this Buff            entity, object action, string message) => ResolveLogging(entity).ActionMessage(entity, action, message);
        public   static string ActionMessage(this AudioFileOutput entity, object action                ) => ResolveLogging(entity).ActionMessage(entity, action         );
        public   static string ActionMessage(this AudioFileOutput entity, object action, string message) => ResolveLogging(entity).ActionMessage(entity, action, message);
        public   static string ActionMessage(this Sample          entity, object action                ) => ResolveLogging(entity).ActionMessage(entity, action         );
        public   static string ActionMessage(this Sample          entity, object action, string message) => ResolveLogging(entity).ActionMessage(entity, action, message);

        public   static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, object action                ) => ResolveLogging(entity, synthWishes).ActionMessage(entity, action         );
        public   static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, object action, string message) => ResolveLogging(entity, synthWishes).ActionMessage(entity, action, message);
        public   static string ActionMessage(this Sample          entity, SynthWishes synthWishes, object action                ) => ResolveLogging(entity, synthWishes).ActionMessage(entity, action         );
        public   static string ActionMessage(this Sample          entity, SynthWishes synthWishes, object action, string message) => ResolveLogging(entity, synthWishes).ActionMessage(entity, action, message);
        
        // ActionMessage (On Simple Types)
        
        public   static string ActionMessage(this object entity                                                   ) => ResolveLogging(entity    ).ActionMessage(entity                           );
        public   static string ActionMessage(this object entity,        object action                             ) => ResolveLogging(entity    ).ActionMessage(entity,     action               );
        public   static string ActionMessage(this object entity,        object action,              string message) => ResolveLogging(entity    ).ActionMessage(entity,     action,       message);
        public   static string ActionMessage(this object entity,        object action, string name, string message) => ResolveLogging(entity    ).ActionMessage(entity,     action, name, message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj                                            ) => ResolveLogging(obj       ).ActionMessage(obj                              );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, object action                             ) => ResolveLogging(obj       ).ActionMessage(obj,        action               );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, object action,              string message) => ResolveLogging(obj       ).ActionMessage(obj,        action,       message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, object action, string name, string message) => ResolveLogging(obj       ).ActionMessage(obj,        action, name, message);
        public   static string ActionMessage(this Type entityType                                                 ) => ResolveLogging(entityType).ActionMessage(entityType                       );
        public   static string ActionMessage(this Type entityType,      object action                             ) => ResolveLogging(entityType).ActionMessage(entityType, action               );
        public   static string ActionMessage(this Type entityType,      object action,              string message) => ResolveLogging(entityType).ActionMessage(entityType, action,       message);
        public   static string ActionMessage(this Type entityType,      object action, string name, string message) => ResolveLogging(entityType).ActionMessage(entityType, action, name, message);
        public   static string ActionMessage(this string typeName                                                 ) => ResolveLogging(typeName  ).ActionMessage(typeName                         );
        public   static string ActionMessage(this string typeName,      object action                             ) => ResolveLogging(typeName  ).ActionMessage(typeName,   action               );
        public   static string ActionMessage(this string typeName,      object action,              string message) => ResolveLogging(typeName  ).ActionMessage(typeName,   action,       message);
        public   static string ActionMessage(this string typeName,      object action, string name, string message) => ResolveLogging(typeName  ).ActionMessage(typeName,   action, name, message);

        // Memory Message (On Simple Types)

        // (Always tagged [MEMORY] here, so no need for target types: object. entity Type or TEntity.)
                
        public static string MemoryActionMessage(this int byteCount                                            ) => ResolveLogging(byteCount).MemoryActionMessage(byteCount                       );
        public static string MemoryActionMessage(this int byteCount,                             string message) => ResolveLogging(byteCount).MemoryActionMessage(byteCount,               message);
        public static string MemoryActionMessage(this int byteCount,                string name, string message) => ResolveLogging(byteCount).MemoryActionMessage(byteCount,         name, message);
        public static string MemoryActionMessage(this int byteCount, object action, string name, string message) => ResolveLogging(byteCount).MemoryActionMessage(byteCount, action, name, message);
        public static string MemoryActionMessage(this byte[] bytes                                             ) => ResolveLogging(bytes    ).MemoryActionMessage(bytes                           );
        public static string MemoryActionMessage(this byte[] bytes,                              string message) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,                   message);
        public static string MemoryActionMessage(this byte[] bytes,                 string name, string message) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,             name, message);
        public static string MemoryActionMessage(this byte[] bytes,  object action, string name, string message) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,     action, name, message);
        public static string ActionMessage      (this byte[] bytes                                             ) => ResolveLogging(bytes    ).      ActionMessage(bytes                           );
        public static string ActionMessage      (this byte[] bytes,                              string message) => ResolveLogging(bytes    ).      ActionMessage(bytes,                   message);
        public static string ActionMessage      (this byte[] bytes,                 string name, string message) => ResolveLogging(bytes    ).      ActionMessage(bytes,             name, message);
        public static string ActionMessage      (this byte[] bytes,  object action, string name, string message) => ResolveLogging(bytes    ).      ActionMessage(bytes,     action, name, message);
        
        // Memory Action Message (On Entities)
        
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes                                            ) => ResolveLogging(entity).MemoryActionMessage(        bytes                       );
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes,                             string message) => ResolveLogging(entity).MemoryActionMessage(        bytes,               message);
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes,                string name, string message) => ResolveLogging(entity).MemoryActionMessage(        bytes,         name, message);
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes, object action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(        bytes, action, name, message);
        
        public   static string MemoryActionMessage(this Tape            entity                                                          ) => ResolveLogging(entity).MemoryActionMessage(entity                             );
        public   static string MemoryActionMessage(this Tape            entity,                                           string message) => ResolveLogging(entity).MemoryActionMessage(entity,                     message);
        public   static string MemoryActionMessage(this Tape            entity,               object action,              string message) => ResolveLogging(entity).MemoryActionMessage(entity,        action,      message);
        public   static string MemoryActionMessage(this TapeConfig      entity                                                          ) => ResolveLogging(entity).MemoryActionMessage(entity                             );
        public   static string MemoryActionMessage(this TapeConfig      entity,                                           string message) => ResolveLogging(entity).MemoryActionMessage(entity,                     message);
        public   static string MemoryActionMessage(this TapeConfig      entity,               object action,              string message) => ResolveLogging(entity).MemoryActionMessage(entity,        action,      message);
        public   static string MemoryActionMessage(this TapeActions     entity                                                          ) => ResolveLogging(entity).MemoryActionMessage(entity                             );
        public   static string MemoryActionMessage(this TapeActions     entity,                                           string message) => ResolveLogging(entity).MemoryActionMessage(entity,                     message);
        public   static string MemoryActionMessage(this TapeActions     entity,               object action,              string message) => ResolveLogging(entity).MemoryActionMessage(entity,        action,      message);
        public   static string MemoryActionMessage(this TapeAction      entity                                                          ) => ResolveLogging(entity).MemoryActionMessage(entity                             );
        public   static string MemoryActionMessage(this TapeAction      entity,                                           string message) => ResolveLogging(entity).MemoryActionMessage(entity,                     message);
        public   static string MemoryActionMessage(this TapeAction      entity,               object action,              string message) => ResolveLogging(entity).MemoryActionMessage(entity,        action,      message);
        public   static string MemoryActionMessage(this Buff            entity                                                          ) => ResolveLogging(entity).MemoryActionMessage(entity                             );
        public   static string MemoryActionMessage(this Buff            entity,                                           string message) => ResolveLogging(entity).MemoryActionMessage(entity,                     message);
        public   static string MemoryActionMessage(this Buff            entity,               object action,              string message) => ResolveLogging(entity).MemoryActionMessage(entity,        action,      message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, byte[] bytes                                            ) => ResolveLogging(entity).MemoryActionMessage(entity, bytes                      );
        public   static string MemoryActionMessage(this AudioFileOutput entity, byte[] bytes,                             string message) => ResolveLogging(entity).MemoryActionMessage(entity, bytes,              message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, byte[] bytes, object action,              string message) => ResolveLogging(entity).MemoryActionMessage(entity, bytes, action,      message);
        public   static string MemoryActionMessage(this Sample          entity                                                          ) => ResolveLogging(entity).MemoryActionMessage(entity                             );
        public   static string MemoryActionMessage(this Sample          entity,                                           string message) => ResolveLogging(entity).MemoryActionMessage(entity,                     message);
        public   static string MemoryActionMessage(this Sample          entity,               object action,              string message) => ResolveLogging(entity).MemoryActionMessage(entity,        action,      message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes                               ) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, bytes                 );
        public   static string MemoryActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes,                string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, bytes,         message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, object action, string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, bytes, action, message);
        public   static string MemoryActionMessage(this Sample          entity, SynthWishes synthWishes                                             ) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity                        );
        public   static string MemoryActionMessage(this Sample          entity, SynthWishes synthWishes,                              string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity,                message);
        public   static string MemoryActionMessage(this Sample          entity, SynthWishes synthWishes,               object action, string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity,        action, message);
        
        // File Message (On Simple Types)

        public static string FileActionMessage(this string filePath                                                      ) => ResolveLogging(filePath).FileActionMessage(filePath                                 );
        public static string FileActionMessage(this string filePath,                                string sourceFilePath) => ResolveLogging(filePath).FileActionMessage(filePath,                  sourceFilePath);
        public static string FileActionMessage(this string filePath, object action,                 string sourceFilePath) => ResolveLogging(filePath).FileActionMessage(filePath, action,          sourceFilePath);
        public static string FileActionMessage(this string filePath, object action, string message, string sourceFilePath) => ResolveLogging(filePath).FileActionMessage(filePath, action, message, sourceFilePath);       
                
        // File Action Messages (On Entities)

        public   static string FileActionMessage(this FlowNode        entity, string filePath                                                      ) => ResolveLogging(entity).FileActionMessage(entity, filePath                                 );
        public   static string FileActionMessage(this FlowNode        entity, string filePath,                                string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath,                  sourceFilePath);
        public   static string FileActionMessage(this FlowNode        entity, string filePath, object action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        public   static string FileActionMessage(this FlowNode        entity, string filePath, object action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        public   static string FileActionMessage(this Tape            entity                               ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this Tape            entity,                string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this Tape            entity, object action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this TapeConfig      entity                               ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this TapeConfig      entity,                string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this TapeConfig      entity, object action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this TapeActions     entity                               ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this TapeActions     entity,                string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this TapeActions     entity, object action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this TapeAction      entity                               ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this TapeAction      entity,                string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this TapeAction      entity, object action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Buff            entity                               ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this Buff            entity,                string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this Buff            entity, object action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this AudioFileOutput entity                               ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this AudioFileOutput entity,                string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this AudioFileOutput entity, object action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Sample          entity                               ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this Sample          entity,                string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this Sample          entity, object action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this AudioFileOutput entity, SynthWishes synthWishes                               ) => ResolveLogging(entity, synthWishes).FileActionMessage(entity                 );
        public   static string FileActionMessage(this AudioFileOutput entity, SynthWishes synthWishes,                string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, object action, string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Sample          entity, SynthWishes synthWishes                               ) => ResolveLogging(entity, synthWishes).FileActionMessage(entity                 );
        public   static string FileActionMessage(this Sample          entity, SynthWishes synthWishes,                string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this Sample          entity, SynthWishes synthWishes, object action, string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action, message);
        
        // LogAction
        
        public   static FlowNode        LogAction(this FlowNode        entity, object action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static FlowNode        LogAction(this FlowNode        entity, object action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static Tape            LogAction(this Tape            entity, object action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static Tape            LogAction(this Tape            entity, object action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static TapeConfig      LogAction(this TapeConfig      entity, object action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static TapeConfig      LogAction(this TapeConfig      entity, object action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static TapeActions     LogAction(this TapeActions     entity, object action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static TapeActions     LogAction(this TapeActions     entity, object action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        
        /// <inheritdoc cref="_logtapeaction" />
        public   static TapeAction      Log      (this TapeAction      action                                ) { ResolveLogging(action).Log      (action                 ); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   static TapeAction      Log      (this TapeAction      action,                 string message) { ResolveLogging(action).Log      (action,         message); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   static TapeAction      LogAction(this TapeAction      action                                ) { ResolveLogging(action).LogAction(action                 ); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   static TapeAction      LogAction(this TapeAction      action,                 string message) { ResolveLogging(action).LogAction(action,         message); return action; }        
        public   static Buff            LogAction(this Buff            entity, object action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static Buff            LogAction(this Buff            entity, object action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, object action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, object action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static Sample          LogAction(this Sample          entity, object action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static Sample          LogAction(this Sample          entity, object action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }

        public   static AudioFileOutput LogAction(this AudioFileOutput entity, SynthWishes synthWishes, object action                 ) { ResolveLogging(entity, synthWishes).LogAction(entity, action         ); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, SynthWishes synthWishes, object action,  string message) { ResolveLogging(entity, synthWishes).LogAction(entity, action, message); return entity; }
        public   static Sample          LogAction(this Sample          entity, SynthWishes synthWishes, object action                 ) { ResolveLogging(entity, synthWishes).LogAction(entity, action         ); return entity; }
        public   static Sample          LogAction(this Sample          entity, SynthWishes synthWishes, object action,  string message) { ResolveLogging(entity, synthWishes).LogAction(entity, action, message); return entity; }
        
        // LogAction (On Simple Types)
        
        public static void LogAction(this object entity                                              ) => ResolveLogging(entity    ).LogAction(entity                           );
        public static void LogAction(this object entity,   object action                             ) => ResolveLogging(entity    ).LogAction(entity,     action               );
        public static void LogAction(this object entity,   object action,              string message) => ResolveLogging(entity    ).LogAction(entity,     action,       message);
        public static void LogAction(this object entity,   object action, string name, string message) => ResolveLogging(entity    ).LogAction(entity,     action, name, message);
        public static void LogAction(this Type entityType                                            ) => ResolveLogging(entityType).LogAction(entityType                       );
        public static void LogAction(this Type entityType, object action                             ) => ResolveLogging(entityType).LogAction(entityType, action               );
        public static void LogAction(this Type entityType, object action,              string message) => ResolveLogging(entityType).LogAction(entityType, action,       message);
        public static void LogAction(this Type entityType, object action, string name, string message) => ResolveLogging(entityType).LogAction(entityType, action, name, message);
        public static void LogAction(this string typeName                                            ) => ResolveLogging(typeName  ).LogAction(typeName                         );
        public static void LogAction(this string typeName, object action                             ) => ResolveLogging(typeName  ).LogAction(typeName,   action               );
        public static void LogAction(this string typeName, object action,              string message) => ResolveLogging(typeName  ).LogAction(typeName,   action,       message);
        public static void LogAction(this string typeName, object action, string name, string message) => ResolveLogging(typeName  ).LogAction(typeName,   action, name, message);

        // Memory Action Logging (On Simple Types)

        public static byte[] LogMemoryAction(this byte[] bytes                                             ) => ResolveLogging(bytes).LogMemoryAction(bytes                           );
        public static byte[] LogMemoryAction(this byte[] bytes,                              string message) => ResolveLogging(bytes).LogMemoryAction(bytes,                   message);
        public static byte[] LogMemoryAction(this byte[] bytes,                 string name, string message) => ResolveLogging(bytes).LogMemoryAction(bytes,             name, message);
        public static byte[] LogMemoryAction(this byte[] bytes,  object action, string name, string message) => ResolveLogging(bytes).LogMemoryAction(bytes,     action, name, message);
        public static byte[] LogAction      (this byte[] bytes                                             ) => ResolveLogging(bytes).LogAction      (bytes                           );
        public static byte[] LogAction      (this byte[] bytes,                              string message) => ResolveLogging(bytes).LogAction      (bytes,                   message);
        public static byte[] LogAction      (this byte[] bytes,                 string name, string message) => ResolveLogging(bytes).LogAction      (bytes,             name, message);
        public static byte[] LogAction      (this byte[] bytes,  object action, string name, string message) => ResolveLogging(bytes).LogAction      (bytes,     action, name, message);

        // Memory Action Logging (On Entities)
        
        public static FlowNode        LogMemoryAction(this FlowNode        entity, byte[] bytes                               ) => ResolveLogging(entity).LogMemoryAction(entity, bytes                 );
        public static FlowNode        LogMemoryAction(this FlowNode        entity, byte[] bytes,                string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         message);
        public static FlowNode        LogMemoryAction(this FlowNode        entity, byte[] bytes, object action, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, message);
        public static Tape            LogMemoryAction(this Tape            entity                                             ) => ResolveLogging(entity).LogMemoryAction(entity                        );
        public static Tape            LogMemoryAction(this Tape            entity,                              string message) => ResolveLogging(entity).LogMemoryAction(entity,                message);
        public static Tape            LogMemoryAction(this Tape            entity,               object action, string message) => ResolveLogging(entity).LogMemoryAction(entity,        action, message);
        public static TapeConfig      LogMemoryAction(this TapeConfig      entity                                             ) => ResolveLogging(entity).LogMemoryAction(entity                        );
        public static TapeConfig      LogMemoryAction(this TapeConfig      entity,                              string message) => ResolveLogging(entity).LogMemoryAction(entity,                message);
        public static TapeConfig      LogMemoryAction(this TapeConfig      entity,               object action, string message) => ResolveLogging(entity).LogMemoryAction(entity,        action, message);
        public static TapeActions     LogMemoryAction(this TapeActions     entity                                             ) => ResolveLogging(entity).LogMemoryAction(entity                        );
        public static TapeActions     LogMemoryAction(this TapeActions     entity,                              string message) => ResolveLogging(entity).LogMemoryAction(entity,                message);
        public static TapeActions     LogMemoryAction(this TapeActions     entity,               object action, string message) => ResolveLogging(entity).LogMemoryAction(entity,        action, message);
        public static TapeAction      LogMemoryAction(this TapeAction      entity                                             ) => ResolveLogging(entity).LogMemoryAction(entity                        );
        public static TapeAction      LogMemoryAction(this TapeAction      entity,                              string message) => ResolveLogging(entity).LogMemoryAction(entity,                message);
        public static TapeAction      LogMemoryAction(this TapeAction      entity,               object action, string message) => ResolveLogging(entity).LogMemoryAction(entity,        action, message);
        public static Buff            LogMemoryAction(this Buff            entity                                             ) => ResolveLogging(entity).LogMemoryAction(entity                        );
        public static Buff            LogMemoryAction(this Buff            entity,                              string message) => ResolveLogging(entity).LogMemoryAction(entity,                message);
        public static Buff            LogMemoryAction(this Buff            entity,               object action, string message) => ResolveLogging(entity).LogMemoryAction(entity,        action, message);
        public static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, byte[] bytes                               ) => ResolveLogging(entity).LogMemoryAction(entity, bytes                 );
        public static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, byte[] bytes,                string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         message);
        public static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, byte[] bytes, object action, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, message);
        public static Sample          LogMemoryAction(this Sample          entity                                             ) => ResolveLogging(entity).LogMemoryAction(entity                        );
        public static Sample          LogMemoryAction(this Sample          entity,                              string message) => ResolveLogging(entity).LogMemoryAction(entity,                message);
        public static Sample          LogMemoryAction(this Sample          entity,               object action, string message) => ResolveLogging(entity).LogMemoryAction(entity,        action, message);
        public static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes                               ) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, bytes                 );
        public static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes,                string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, bytes,         message);
        public static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, object action, string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, bytes, action, message);
        public static Sample          LogMemoryAction(this Sample          entity, SynthWishes synthWishes                                             ) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity                        );
        public static Sample          LogMemoryAction(this Sample          entity, SynthWishes synthWishes,                              string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity,                message);
        public static Sample          LogMemoryAction(this Sample          entity, SynthWishes synthWishes,               object action, string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity,        action, message);

        // LogFileAction
        
        public static string          LogFileAction(this                        string filePath                                                      ) => ResolveLogging(filePath).LogFileAction(        filePath                                 );
        public static string          LogFileAction(this                        string filePath,                                string sourceFilePath) => ResolveLogging(filePath).LogFileAction(        filePath,                  sourceFilePath);
        public static string          LogFileAction(this                        string filePath, object action,                 string sourceFilePath) => ResolveLogging(filePath).LogFileAction(        filePath, action,          sourceFilePath);
        public static string          LogFileAction(this                        string filePath, object action, string message, string sourceFilePath) => ResolveLogging(filePath).LogFileAction(        filePath, action, message, sourceFilePath);
        public static FlowNode        LogFileAction(this FlowNode       entity, string filePath                                                      ) => ResolveLogging(entity  ).LogFileAction(entity, filePath                                 );
        public static FlowNode        LogFileAction(this FlowNode       entity, string filePath,                                string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath,                  sourceFilePath);
        public static FlowNode        LogFileAction(this FlowNode       entity, string filePath, object action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        public static FlowNode        LogFileAction(this FlowNode       entity, string filePath, object action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        public static Tape            LogFileAction(this Tape            entity                                                                      ) => ResolveLogging(entity  ).LogFileAction(entity                                           );
        public static Tape            LogFileAction(this Tape            entity,                                string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,                   message                );
        public static Tape            LogFileAction(this Tape            entity,                 object action, string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,           action, message                );
        public static TapeConfig      LogFileAction(this TapeConfig      entity                                                                      ) => ResolveLogging(entity  ).LogFileAction(entity                                           );
        public static TapeConfig      LogFileAction(this TapeConfig      entity,                                string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,                   message                );
        public static TapeConfig      LogFileAction(this TapeConfig      entity,                 object action, string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,           action, message                );
        public static TapeActions     LogFileAction(this TapeActions     entity                                                                      ) => ResolveLogging(entity  ).LogFileAction(entity                                           );
        public static TapeActions     LogFileAction(this TapeActions     entity,                                string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,                   message                );
        public static TapeActions     LogFileAction(this TapeActions     entity,                 object action, string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,           action, message                );
        public static TapeAction      LogFileAction(this TapeAction      entity                                                                      ) => ResolveLogging(entity  ).LogFileAction(entity                                           );
        public static TapeAction      LogFileAction(this TapeAction      entity,                                string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,                   message                );
        public static TapeAction      LogFileAction(this TapeAction      entity,                 object action, string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,           action, message                );
        public static Buff            LogFileAction(this Buff            entity                                                                      ) => ResolveLogging(entity  ).LogFileAction(entity                                           );
        public static Buff            LogFileAction(this Buff            entity,                                string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,                   message                );
        public static Buff            LogFileAction(this Buff            entity,                 object action, string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,           action, message                );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity                                                                      ) => ResolveLogging(entity  ).LogFileAction(entity                                           );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity,                                string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,                   message                );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity,                 object action, string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,           action, message                );
        public static Sample          LogFileAction(this Sample          entity                                                                      ) => ResolveLogging(entity  ).LogFileAction(entity                                           );
        public static Sample          LogFileAction(this Sample          entity,                                string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,                   message                );
        public static Sample          LogFileAction(this Sample          entity,                 object action, string message                       ) => ResolveLogging(entity  ).LogFileAction(entity,           action, message                );
        public static string          LogFileAction(this string        filePath, SynthWishes synthWishes                                                      ) => ResolveLogging(filePath, synthWishes).LogFileAction(filePath                                 );
        public static string          LogFileAction(this string        filePath, SynthWishes synthWishes,                                string sourceFilePath) => ResolveLogging(filePath, synthWishes).LogFileAction(filePath,                  sourceFilePath);
        public static string          LogFileAction(this string        filePath, SynthWishes synthWishes, object action,                 string sourceFilePath) => ResolveLogging(filePath, synthWishes).LogFileAction(filePath, action,          sourceFilePath);
        public static string          LogFileAction(this string        filePath, SynthWishes synthWishes, object action, string message, string sourceFilePath) => ResolveLogging(filePath, synthWishes).LogFileAction(filePath, action, message, sourceFilePath);
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, SynthWishes synthWishes                                                      ) => ResolveLogging(entity,   synthWishes).LogFileAction(entity                                   );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, SynthWishes synthWishes,                string message                       ) => ResolveLogging(entity,   synthWishes).LogFileAction(entity,           message                );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, SynthWishes synthWishes, object action, string message                       ) => ResolveLogging(entity,   synthWishes).LogFileAction(entity,   action, message                );
        public static Sample          LogFileAction(this Sample          entity, SynthWishes synthWishes                                                      ) => ResolveLogging(entity,   synthWishes).LogFileAction(entity                                   );
        public static Sample          LogFileAction(this Sample          entity, SynthWishes synthWishes,                string message                       ) => ResolveLogging(entity,   synthWishes).LogFileAction(entity,           message                );
        public static Sample          LogFileAction(this Sample          entity, SynthWishes synthWishes, object action, string message                       ) => ResolveLogging(entity,   synthWishes).LogFileAction(entity,   action, message                );
    }
}