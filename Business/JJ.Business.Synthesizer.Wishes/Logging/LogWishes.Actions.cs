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
using static JJ.Business.Synthesizer.Wishes.TapeWishes.ActionEnum;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        // ActionMessage (On Entities)
        
        public   string ActionMessage(FlowNode        entity, ActionEnum action                 ) => ActionMessage(entity, $"{action}", ""     );
        public   string ActionMessage(FlowNode        entity, ActionEnum action,  string message) => ActionMessage(entity, $"{action}", message);
        public   string ActionMessage(FlowNode        entity, string     action                 ) => ActionMessage(entity,    action  , ""     );
        public   string ActionMessage(FlowNode        entity, string     message, int dummy = 0 ) => ActionMessage(entity, "",          message);
        public   string ActionMessage(FlowNode        entity, string     action,  string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage<Operator>(action, entity.Name, message);
        }
        
        internal string ActionMessage(ConfigResolver entity, ActionEnum action                             ) => ActionMessage(entity,    action  , ""  , ""     );
        internal string ActionMessage(ConfigResolver entity, ActionEnum action,              string message) => ActionMessage(entity,    action  , ""  , message);
        internal string ActionMessage(ConfigResolver entity, ActionEnum action, string name, int dummy = 0 ) => ActionMessage(entity, $"{action}", name, ""     );
        internal string ActionMessage(ConfigResolver entity, ActionEnum action, string name, string message) => ActionMessage(entity, $"{action}", name, message);
        internal string ActionMessage(ConfigResolver entity                                                ) => ActionMessage(entity,    ""      , ""  , ""     );
        internal string ActionMessage(ConfigResolver entity,                                 string message) => ActionMessage(entity,    ""      , ""  , message);
        internal string ActionMessage(ConfigResolver entity, string     action,              int dummy = 0 ) => ActionMessage(entity,    action  , ""  , ""     );
        internal string ActionMessage(ConfigResolver entity, string     action,              string message) => ActionMessage(entity,    action  , ""  , message);
        internal string ActionMessage(ConfigResolver entity, string     action, string name, int dummy = 0 ) => ActionMessage(entity,    action  , name, ""     );
        internal string ActionMessage(ConfigResolver entity, string     action, string name, string message)
        {
            return ActionMessage("Config", action, name, message);
        }
        
        internal string ActionMessage(ConfigSection  entity, ActionEnum action                             ) => ActionMessage(entity,    action  , ""  , ""     );
        internal string ActionMessage(ConfigSection  entity, ActionEnum action,              string message) => ActionMessage(entity,    action  , ""  , message);
        internal string ActionMessage(ConfigSection  entity, ActionEnum action, string name, int dummy = 0 ) => ActionMessage(entity, $"{action}", name, ""     );
        internal string ActionMessage(ConfigSection  entity, ActionEnum action, string name, string message) => ActionMessage(entity, $"{action}", name, message);
        internal string ActionMessage(ConfigSection  entity                                                ) => ActionMessage(entity,    ""      , ""  , ""     );
        internal string ActionMessage(ConfigSection  entity,                                 string message) => ActionMessage(entity,    ""      , ""  , message);
        internal string ActionMessage(ConfigSection  entity, string     action,              int dummy = 0 ) => ActionMessage(entity,    action  , ""  , ""     );
        internal string ActionMessage(ConfigSection  entity, string     action,              string message) => ActionMessage(entity,    action  , ""  , message);
        internal string ActionMessage(ConfigSection  entity, string     action, string name, int dummy = 0 ) => ActionMessage(entity,    action  , name, ""     );
        internal string ActionMessage(ConfigSection  entity, string     action, string name, string message)
        {
            return ActionMessage("Config", action, name, message);
        }
        
        public string ActionMessage(Tape            entity, ActionEnum action                 ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(Tape            entity, ActionEnum action,  string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(Tape            entity, string     action                 ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(Tape            entity, string     message, int dummy = 0 ) => ActionMessage(entity, "",          message);
        public string ActionMessage(Tape            entity, string     action,  string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Descriptor, message);
        }
        
        public string ActionMessage(TapeConfig      entity, ActionEnum action                 ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(TapeConfig      entity, ActionEnum action,  string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(TapeConfig      entity, string     action                 ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(TapeConfig      entity, string     message, int dummy = 0 ) => ActionMessage(entity, "",          message);
        public string ActionMessage(TapeConfig      entity, string     action,  string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.Tape, action, message);
        }
        
        public string ActionMessage(TapeActions     entity, ActionEnum action                 ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(TapeActions     entity, ActionEnum action,  string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(TapeActions     entity, string     action                 ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(TapeActions     entity, string     message, int dummy = 0 ) => ActionMessage(entity, "",          message);
        public string ActionMessage(TapeActions     entity, string     action,  string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.Tape, action, message);
        }
        
        /// <inheritdoc cref="_logtapeaction" />
        public string Message      (TapeAction      action                ) => ActionMessage(action, ""     );
        /// <inheritdoc cref="_logtapeaction" />
        public string Message      (TapeAction      action, string message) => ActionMessage(action, message);
        /// <inheritdoc cref="_logtapeaction" />
        public string ActionMessage(TapeAction      action                ) => ActionMessage(action, ""     );
        /// <inheritdoc cref="_logtapeaction" />
        public string ActionMessage(TapeAction      action, string message)
        {
            if (action == null) throw new NullException(() => action);
            return ActionMessage("Actions", action.Type, action.Tape.Descriptor(), message);
        }
        
        public string ActionMessage(Buff            entity, ActionEnum action                 ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(Buff            entity, ActionEnum action,  string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(Buff            entity, string     action                 ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(Buff            entity, string     message, int dummy = 0 ) => ActionMessage(entity, "",          message);
        public string ActionMessage(Buff            entity, string     action,  string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message);
        }
        
        public string ActionMessage(AudioFileOutput entity, ActionEnum action                 ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(AudioFileOutput entity, ActionEnum action,  string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(AudioFileOutput entity, string     action                 ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(AudioFileOutput entity, string     message, int dummy = 0 ) => ActionMessage(entity, "",          message);
        public string ActionMessage(AudioFileOutput entity, string     action,  string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage("Out", action, entity.Name, message ?? ConfigLog(entity));
        }
        
        public string ActionMessage(Sample          entity, ActionEnum action                 ) => ActionMessage(entity, $"{action}", ""     );
        public string ActionMessage(Sample          entity, ActionEnum action,  string message) => ActionMessage(entity, $"{action}", message);
        public string ActionMessage(Sample          entity, string     action                 ) => ActionMessage(entity,    action  , ""     );
        public string ActionMessage(Sample          entity, string     message, int dummy = 0 ) => ActionMessage(entity, "",          message);
        public string ActionMessage(Sample          entity, string     action,  string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity, action, entity.Name, message ?? ConfigLog(entity));
        }

        // ActionMessage (On Simple Types)
        
        public string ActionMessage(object entity,   ActionEnum action                             ) => ActionMessage(entity,          action  , ""  , ""     );
        public string ActionMessage(object entity,   ActionEnum action,              string message) => ActionMessage(entity,          action  , ""  , message);
        public string ActionMessage(object entity,   ActionEnum action, string name, int dummy = 0 ) => ActionMessage(entity,       $"{action}", name, ""     );
        public string ActionMessage(object entity,   ActionEnum action, string name, string message) => ActionMessage(entity,       $"{action}", name, message);
        public string ActionMessage(object entity                                                  ) => ActionMessage(entity,          ""      , ""  , ""     );
        public string ActionMessage(object entity,                                   string message) => ActionMessage(entity,          ""      , ""  , message);
        public string ActionMessage(object entity,   string     action,              int dummy = 0 ) => ActionMessage(entity,          action  , ""  , ""     );
        public string ActionMessage(object entity,   string     action,              string message) => ActionMessage(entity,          action  , ""  , message);
        public string ActionMessage(object entity,   string     action, string name, int dummy = 0 ) => ActionMessage(entity,          action  , name, ""     );
        public string ActionMessage(object entity,   string     action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.GetType().Name, action, name, message);
        }

        public string ActionMessage<TEntity>(        ActionEnum action                             ) => ActionMessage(typeof(TEntity), action  , ""  , ""     );
        public string ActionMessage<TEntity>(        ActionEnum action,              string message) => ActionMessage(typeof(TEntity), action  , ""  , message);
        public string ActionMessage<TEntity>(        ActionEnum action, string name, int dummy = 0 ) => ActionMessage(typeof(TEntity), action  , name, ""     );
        public string ActionMessage<TEntity>(        ActionEnum action, string name, string message) => ActionMessage(typeof(TEntity), action  , name, message);
        public string ActionMessage<TEntity>(                                                      ) => ActionMessage(typeof(TEntity), ""      , ""  , ""     );
        public string ActionMessage<TEntity>(                                        string message) => ActionMessage(typeof(TEntity), ""      , ""  , message);
        public string ActionMessage<TEntity>(        string     action,              int dummy = 0 ) => ActionMessage(typeof(TEntity), action  , ""  , ""     );
        public string ActionMessage<TEntity>(        string     action,              string message) => ActionMessage(typeof(TEntity), action  , ""  , message);
        public string ActionMessage<TEntity>(        string     action, string name, int dummy = 0 ) => ActionMessage(typeof(TEntity), action  , name, ""     );
        public string ActionMessage<TEntity>(        string     action, string name, string message) => ActionMessage(typeof(TEntity), action  , name, message);

        public string ActionMessage(Type entityType, ActionEnum action                             ) => ActionMessage(entityType,      action  , ""  , ""     );
        public string ActionMessage(Type entityType, ActionEnum action,              string message) => ActionMessage(entityType,      action  , ""  , message);
        public string ActionMessage(Type entityType, ActionEnum action, string name, int dummy = 0 ) => ActionMessage(entityType,      action  , name, ""     );  
        public string ActionMessage(Type entityType, ActionEnum action, string name, string message) => ActionMessage(entityType,   $"{action}", name, message);
        public string ActionMessage(Type entityType                                                ) => ActionMessage(entityType,      ""      , ""  , ""     );
        public string ActionMessage(Type entityType,                                 string message) => ActionMessage(entityType,      ""      , ""  , message);
        public string ActionMessage(Type entityType, string     action,              int dummy = 0 ) => ActionMessage(entityType,      action  , ""  , ""     );
        public string ActionMessage(Type entityType, string     action,              string message) => ActionMessage(entityType,      action  , ""  , message);
        public string ActionMessage(Type entityType, string     action, string name, int dummy = 0 ) => ActionMessage(entityType,      action  , name, ""     );
        public string ActionMessage(Type entityType, string     action, string name, string message)
        {
            if (entityType == null) throw new NullException(() => entityType);
            return ActionMessage(entityType.Name, action, name, message);
        }
        
        public string ActionMessage(string typeName, ActionEnum action                             ) => ActionMessage(typeName,        action  , ""  , ""     );
        public string ActionMessage(string typeName, ActionEnum action,              string message) => ActionMessage(typeName,        action  , ""  , message);
        public string ActionMessage(string typeName, ActionEnum action, string name, int dummy = 0 ) => ActionMessage(typeName,        action  , name, ""     );
        public string ActionMessage(string typeName, ActionEnum action, string name, string message) => ActionMessage(typeName,     $"{action}", name, message);
        public string ActionMessage(string typeName                                                ) => ActionMessage(typeName,        ""      , ""  , ""     );
        public string ActionMessage(string typeName,                                 string message) => ActionMessage(typeName,        ""      , ""  , message);
        public string ActionMessage(string typeName, string     action,              int dummy = 0 ) => ActionMessage(typeName,        action  , ""  , ""     );
        public string ActionMessage(string typeName, string     action,              string message) => ActionMessage(typeName,        action  , ""  , message);
        public string ActionMessage(string typeName, string     action, string name, int dummy = 0 ) => ActionMessage(typeName,        action  , name, ""     );
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
                
        // Memory Message (On Simple Types)
        
        public string MemoryActionMessage(int byteCount                                                ) => MemoryActionMessage(byteCount, "Write"    , ""  , message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int byteCount,                                 string message) => MemoryActionMessage(byteCount, "Write"    , ""  , message                            );
        public string MemoryActionMessage(int byteCount, ActionEnum action                             ) => MemoryActionMessage(byteCount, $"{action}", ""  , message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int byteCount, ActionEnum action,              string message) => MemoryActionMessage(byteCount, $"{action}", ""  , message                            );
        public string MemoryActionMessage(int byteCount, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(byteCount, $"{action}", name, message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int byteCount, ActionEnum action, string name, string message) => MemoryActionMessage(byteCount, $"{action}", name, message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int byteCount,                    string name, int dummy = 0 ) => MemoryActionMessage(byteCount, "Write"    , name, message: PrettyByteCount(byteCount));
        public string MemoryActionMessage(int byteCount,                    string name, string message) => MemoryActionMessage(byteCount, "Write"    , name, message                            );
        public string MemoryActionMessage(int byteCount, string     action, string name, string message)
        {
            return Has(byteCount) ? ActionMessage("Memory", action, name, message) : "";
        }

        public string MemoryActionMessage(byte[] bytes                                                ) => MemoryActionMessage(bytes?.Length ?? 0                             );
        public string MemoryActionMessage(byte[] bytes,                                 string message) => MemoryActionMessage(bytes?.Length ?? 0,               message      );
        public string MemoryActionMessage(byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes?.Length ?? 0, action                     );   
        public string MemoryActionMessage(byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes?.Length ?? 0, action,       message      );
        public string MemoryActionMessage(byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes?.Length ?? 0, action, name,         dummy);
        public string MemoryActionMessage(byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes?.Length ?? 0, action, name, message      );
        public string MemoryActionMessage(byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes?.Length ?? 0,         name,         dummy);
        public string MemoryActionMessage(byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes?.Length ?? 0,         name, message      );
        public string MemoryActionMessage(byte[] bytes, string     action, string name, string message) => MemoryActionMessage(bytes?.Length ?? 0, action, name, message      );
                                                                    
        public string ActionMessage      (byte[] bytes                                                ) => MemoryActionMessage(bytes                             );
        public string ActionMessage      (byte[] bytes,                                 string message) => MemoryActionMessage(bytes,               message      );
        public string ActionMessage      (byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes, action                     );   
        public string ActionMessage      (byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes, action,       message      );
        public string ActionMessage      (byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes, action, name,         dummy);
        public string ActionMessage      (byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes, action, name, message      );
        public string ActionMessage      (byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes,         name,         dummy);
        public string ActionMessage      (byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes,         name, message      );
        public string ActionMessage      (byte[] bytes, string     action, string name, string message) => MemoryActionMessage(bytes, action, name, message      );
        
        // Memory Action Message (On Entities)
        
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes                                                ) => MemoryActionMessage(bytes                             );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                                 string message) => MemoryActionMessage(bytes,               message      );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes, action                     );   
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes, action,       message      );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes, action, name,         dummy);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes, action, name, message      );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes,         name,         dummy);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes,         name, message      );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, string     action, string name, string message) => MemoryActionMessage(bytes, action, name, message      );

        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes                                                ) => MemoryActionMessage(bytes                             );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                                 string message) => MemoryActionMessage(bytes,               message      );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes, action                     );   
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes, action,       message      );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes, action, name,         dummy);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes, action, name, message      );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes,         name,         dummy);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes,         name, message      );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, string     action, string name, string message) => MemoryActionMessage(bytes, action, name, message      );

        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes                                                ) => MemoryActionMessage(bytes                             );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                                 string message) => MemoryActionMessage(bytes,               message      );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes, action                     );   
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes, action,       message      );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes, action, name,         dummy);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes, action, name, message      );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes,         name,         dummy);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes,         name, message      );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, string     action, string name, string message) => MemoryActionMessage(bytes, action, name, message      );
        
        // TODO: Why pass a name if the entity has a name? Regular ActionMessage doesn't...
        
        public   string MemoryActionMessage(Tape            entity                                                ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public   string MemoryActionMessage(Tape            entity,                                 string message) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public   string MemoryActionMessage(Tape            entity, ActionEnum action                             ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public   string MemoryActionMessage(Tape            entity, ActionEnum action,              string message) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public   string MemoryActionMessage(Tape            entity, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public   string MemoryActionMessage(Tape            entity, ActionEnum action, string name, string message) => MemoryActionMessage(entity, $"{action}", name, message);
        public   string MemoryActionMessage(Tape            entity,                    string name, int dummy = 0 ) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public   string MemoryActionMessage(Tape            entity,                    string name, string message) => MemoryActionMessage(entity, "Write"    , name, message);
        public   string MemoryActionMessage(Tape            entity, string     action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Bytes, action, Coalesce(name, entity.Descriptor), Coalesce(message, PrettyByteCount(entity.Bytes)));
        }

        public   string MemoryActionMessage(TapeConfig      entity                                                ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public   string MemoryActionMessage(TapeConfig      entity,                                 string message) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public   string MemoryActionMessage(TapeConfig      entity, ActionEnum action                             ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public   string MemoryActionMessage(TapeConfig      entity, ActionEnum action,              string message) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public   string MemoryActionMessage(TapeConfig      entity, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public   string MemoryActionMessage(TapeConfig      entity, ActionEnum action, string name, string message) => MemoryActionMessage(entity, $"{action}", name, message);
        public   string MemoryActionMessage(TapeConfig      entity,                    string name, int dummy = 0 ) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public   string MemoryActionMessage(TapeConfig      entity,                    string name, string message) => MemoryActionMessage(entity, "Write"    , name, message);
        public   string MemoryActionMessage(TapeConfig      entity, string     action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, name, message);
        }
        
        public   string MemoryActionMessage(TapeActions     entity                                                ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public   string MemoryActionMessage(TapeActions     entity,                                 string message) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public   string MemoryActionMessage(TapeActions     entity, ActionEnum action                             ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public   string MemoryActionMessage(TapeActions     entity, ActionEnum action,              string message) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public   string MemoryActionMessage(TapeActions     entity, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public   string MemoryActionMessage(TapeActions     entity, ActionEnum action, string name, string message) => MemoryActionMessage(entity, $"{action}", name, message);
        public   string MemoryActionMessage(TapeActions     entity,                    string name, int dummy = 0 ) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public   string MemoryActionMessage(TapeActions     entity,                    string name, string message) => MemoryActionMessage(entity, "Write"    , name, message);
        public   string MemoryActionMessage(TapeActions     entity, string     action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, name, message);
        }
        
        public   string MemoryActionMessage(TapeAction      entity                                                ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public   string MemoryActionMessage(TapeAction      entity,                                 string message) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public   string MemoryActionMessage(TapeAction      entity, ActionEnum action                             ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public   string MemoryActionMessage(TapeAction      entity, ActionEnum action,              string message) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public   string MemoryActionMessage(TapeAction      entity, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public   string MemoryActionMessage(TapeAction      entity, ActionEnum action, string name, string message) => MemoryActionMessage(entity, $"{action}", name, message);
        public   string MemoryActionMessage(TapeAction      entity,                    string name, int dummy = 0 ) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public   string MemoryActionMessage(TapeAction      entity,                    string name, string message) => MemoryActionMessage(entity, "Write"    , name, message);
        public   string MemoryActionMessage(TapeAction      entity, string     action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, name, message);
        }

        public   string MemoryActionMessage(Buff            entity                                                ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public   string MemoryActionMessage(Buff            entity,                                 string message) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public   string MemoryActionMessage(Buff            entity, ActionEnum action                             ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public   string MemoryActionMessage(Buff            entity, ActionEnum action,              string message) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public   string MemoryActionMessage(Buff            entity, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public   string MemoryActionMessage(Buff            entity, ActionEnum action, string name, string message) => MemoryActionMessage(entity, $"{action}", name, message);
        public   string MemoryActionMessage(Buff            entity,                    string name, int dummy = 0 ) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public   string MemoryActionMessage(Buff            entity,                    string name, string message) => MemoryActionMessage(entity, "Write"    , name, message);
        public   string MemoryActionMessage(Buff            entity, string     action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            
            if (entity.Tape != null)
            {
                return MemoryActionMessage(entity.Tape, action, name, message);
            }
            else if (entity.UnderlyingAudioFileOutput != null)
            {
                return MemoryActionMessage(entity.UnderlyingAudioFileOutput, entity.Bytes, action, name, message);
                
            }
            else
            {
                return MemoryActionMessage(entity.Bytes, action, name, message);
            }
        }
        
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes                                                ) => MemoryActionMessage(bytes                             );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                                 string message) => MemoryActionMessage(bytes,               message      );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes, action                     );   
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes, action,       message      );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes, action, name,         dummy);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes, action, name, message      );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes,         name,         dummy);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes,         name, message      );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, string     action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(bytes, action, Coalesce(name, entity.Name), message);
        }
        
        public   string MemoryActionMessage(Sample          entity                                                ) => MemoryActionMessage(entity, "Write"    , ""  , ""     );
        public   string MemoryActionMessage(Sample          entity,                                 string message) => MemoryActionMessage(entity, "Write"    , ""  , message);
        public   string MemoryActionMessage(Sample          entity, ActionEnum action                             ) => MemoryActionMessage(entity, $"{action}", ""  , ""     );
        public   string MemoryActionMessage(Sample          entity, ActionEnum action,              string message) => MemoryActionMessage(entity, $"{action}", ""  , message);
        public   string MemoryActionMessage(Sample          entity, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(entity, $"{action}", name, ""     );
        public   string MemoryActionMessage(Sample          entity, ActionEnum action, string name, string message) => MemoryActionMessage(entity, $"{action}", name, message);
        public   string MemoryActionMessage(Sample          entity,                    string name, int dummy = 0 ) => MemoryActionMessage(entity, "Write"    , name, ""     );
        public   string MemoryActionMessage(Sample          entity,                    string name, string message) => MemoryActionMessage(entity, "Write"    , name, message);
        public   string MemoryActionMessage(Sample          entity, string     action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Bytes, action, Coalesce(name, entity.Name), message);
        }
        
        // File Message (On Simple Types)
        
        public string FileActionMessage(string filePath                                                          ) => FileActionMessage(filePath, Save       , ""     , ""            );
        public string FileActionMessage(string filePath,                                    string sourceFilePath) => FileActionMessage(filePath, Save       , ""     , sourceFilePath);
        public string FileActionMessage(string filePath, ActionEnum action                                       ) => FileActionMessage(filePath, $"{action}", ""     , ""            );
        public string FileActionMessage(string filePath, ActionEnum action,                 string sourceFilePath) => FileActionMessage(filePath, $"{action}", ""     , sourceFilePath);
        public string FileActionMessage(string filePath, ActionEnum action, string message, string sourceFilePath) => FileActionMessage(filePath, $"{action}", message, sourceFilePath);
        public string FileActionMessage(string filePath, string     action,                 int dummy = 0        ) => FileActionMessage(filePath, action     , ""     , ""            );
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

        // File Action Messages (On Entities)

        public   string FileActionMessage(FlowNode       entity, string filePath                                                          ) => FileActionMessage(filePath                                 );
        public   string FileActionMessage(FlowNode       entity, string filePath,                                    string sourceFilePath) => FileActionMessage(filePath,                  sourceFilePath);
        public   string FileActionMessage(FlowNode       entity, string filePath, ActionEnum action                                       ) => FileActionMessage(filePath, action                         );
        public   string FileActionMessage(FlowNode       entity, string filePath, ActionEnum action,                 string sourceFilePath) => FileActionMessage(filePath, action,          sourceFilePath);
        public   string FileActionMessage(FlowNode       entity, string filePath, ActionEnum action, string message, string sourceFilePath) => FileActionMessage(filePath, action, message, sourceFilePath);
        public   string FileActionMessage(FlowNode       entity, string filePath, string     action,                 int dummy = 0        ) => FileActionMessage(filePath, action         , dummy         );
        public   string FileActionMessage(FlowNode       entity, string filePath, string     action,                 string sourceFilePath) => FileActionMessage(filePath, action         , sourceFilePath);
        public   string FileActionMessage(FlowNode       entity, string filePath, string     action, string message, string sourceFilePath) => FileActionMessage(filePath, action, message, sourceFilePath);

        internal string FileActionMessage(ConfigResolver entity, string filePath                                                          ) => FileActionMessage(filePath                                 );
        internal string FileActionMessage(ConfigResolver entity, string filePath,                                    string sourceFilePath) => FileActionMessage(filePath,                  sourceFilePath);
        internal string FileActionMessage(ConfigResolver entity, string filePath, ActionEnum action                                       ) => FileActionMessage(filePath, action                         );
        internal string FileActionMessage(ConfigResolver entity, string filePath, ActionEnum action,                 string sourceFilePath) => FileActionMessage(filePath, action,          sourceFilePath);
        internal string FileActionMessage(ConfigResolver entity, string filePath, ActionEnum action, string message, string sourceFilePath) => FileActionMessage(filePath, action, message, sourceFilePath);
        internal string FileActionMessage(ConfigResolver entity, string filePath, string     action,                 int dummy = 0        ) => FileActionMessage(filePath, action         , dummy         );
        internal string FileActionMessage(ConfigResolver entity, string filePath, string     action,                 string sourceFilePath) => FileActionMessage(filePath, action         , sourceFilePath);
        internal string FileActionMessage(ConfigResolver entity, string filePath, string     action, string message, string sourceFilePath) => FileActionMessage(filePath, action, message, sourceFilePath);

        internal string FileActionMessage(ConfigSection  entity, string filePath                                                          ) => FileActionMessage(filePath                                 );
        internal string FileActionMessage(ConfigSection  entity, string filePath,                                    string sourceFilePath) => FileActionMessage(filePath,                  sourceFilePath);
        internal string FileActionMessage(ConfigSection  entity, string filePath, ActionEnum action                                       ) => FileActionMessage(filePath, action                         );
        internal string FileActionMessage(ConfigSection  entity, string filePath, ActionEnum action,                 string sourceFilePath) => FileActionMessage(filePath, action,          sourceFilePath);
        internal string FileActionMessage(ConfigSection  entity, string filePath, ActionEnum action, string message, string sourceFilePath) => FileActionMessage(filePath, action, message, sourceFilePath);
        internal string FileActionMessage(ConfigSection  entity, string filePath, string     action,                 int dummy = 0        ) => FileActionMessage(filePath, action         , dummy         );
        internal string FileActionMessage(ConfigSection  entity, string filePath, string     action,                 string sourceFilePath) => FileActionMessage(filePath, action         , sourceFilePath);
        internal string FileActionMessage(ConfigSection  entity, string filePath, string     action, string message, string sourceFilePath) => FileActionMessage(filePath, action, message, sourceFilePath);

        public string FileActionMessage(Tape            entity                                   ) => FileActionMessage(entity, Save       , ""     );
        public string FileActionMessage(Tape            entity, ActionEnum action                ) => FileActionMessage(entity, $"{action}", ""     );
        public string FileActionMessage(Tape            entity, ActionEnum action, string message) => FileActionMessage(entity, $"{action}", message);
        public string FileActionMessage(Tape            entity,                    string message) => FileActionMessage(entity, Save       , message);
        public string FileActionMessage(Tape            entity, string     action, int dummy = 0 ) => FileActionMessage(entity, action     , ""     );
        public string FileActionMessage(Tape            entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            string formattedFilePath = FormatFilePathIfExists(entity.FilePathResolved);
            if (!Has(formattedFilePath)) return "";
            return ActionMessage("File", action, formattedFilePath, message);
        }
        
        public string FileActionMessage(TapeConfig      entity                                   ) => FileActionMessage(entity, Save       , ""     );
        public string FileActionMessage(TapeConfig      entity, ActionEnum action                ) => FileActionMessage(entity, $"{action}", ""     );
        public string FileActionMessage(TapeConfig      entity, ActionEnum action, string message) => FileActionMessage(entity, $"{action}", message);
        public string FileActionMessage(TapeConfig      entity,                    string message) => FileActionMessage(entity, Save       , message);
        public string FileActionMessage(TapeConfig      entity, string     action, int dummy = 0 ) => FileActionMessage(entity, action     , ""     );
        public string FileActionMessage(TapeConfig      entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.Tape, action, message);
        }

        public string FileActionMessage(TapeActions     entity                                   ) => FileActionMessage(entity, Save       , ""     );
        public string FileActionMessage(TapeActions     entity, ActionEnum action                ) => FileActionMessage(entity, $"{action}", ""     );
        public string FileActionMessage(TapeActions     entity, ActionEnum action, string message) => FileActionMessage(entity, $"{action}", message);
        public string FileActionMessage(TapeActions     entity,                    string message) => FileActionMessage(entity, Save       , message);
        public string FileActionMessage(TapeActions     entity, string     action, int dummy = 0 ) => FileActionMessage(entity, action     , ""     );
        public string FileActionMessage(TapeActions     entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.Tape, action, message);
        }

        public string FileActionMessage(TapeAction      entity                                   ) => FileActionMessage(entity, Save       , ""     );
        public string FileActionMessage(TapeAction      entity, ActionEnum action                ) => FileActionMessage(entity, $"{action}", ""     );
        public string FileActionMessage(TapeAction      entity, ActionEnum action, string message) => FileActionMessage(entity, $"{action}", message);
        public string FileActionMessage(TapeAction      entity,                    string message) => FileActionMessage(entity, Save       , message);
        public string FileActionMessage(TapeAction      entity, string     action, int dummy = 0 ) => FileActionMessage(entity, action     , ""     );
        public string FileActionMessage(TapeAction      entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.Tape, Coalesce(action, $"{entity.Type}"), message);
        }
        
        public string FileActionMessage(Buff            entity                                   ) => FileActionMessage(entity, Save       , ""     );
        public string FileActionMessage(Buff            entity, ActionEnum action                ) => FileActionMessage(entity, $"{action}", ""     );
        public string FileActionMessage(Buff            entity, ActionEnum action, string message) => FileActionMessage(entity, $"{action}", message);
        public string FileActionMessage(Buff            entity,                    string message) => FileActionMessage(entity, Save       , message);
        public string FileActionMessage(Buff            entity, string     action, int dummy = 0 ) => FileActionMessage(entity, action     , ""     );
        public string FileActionMessage(Buff            entity, string     action, string message)
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
                
        public string FileActionMessage(AudioFileOutput entity                                   ) => FileActionMessage(entity, Save       , ""     );
        public string FileActionMessage(AudioFileOutput entity, ActionEnum action                ) => FileActionMessage(entity, $"{action}", ""     );
        public string FileActionMessage(AudioFileOutput entity, ActionEnum action, string message) => FileActionMessage(entity, $"{action}", message);
        public string FileActionMessage(AudioFileOutput entity,                    string message) => FileActionMessage(entity, Save       , message);
        public string FileActionMessage(AudioFileOutput entity, string     action, int dummy = 0 ) => FileActionMessage(entity, action     , ""     );
        public string FileActionMessage(AudioFileOutput entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.FilePath, action, message);
        }
                
        public string FileActionMessage(Sample          entity                                   ) => FileActionMessage(entity, Save       , ""     );
        public string FileActionMessage(Sample          entity, ActionEnum action                ) => FileActionMessage(entity, $"{action}", ""     );
        public string FileActionMessage(Sample          entity, ActionEnum action, string message) => FileActionMessage(entity, $"{action}", message);
        public string FileActionMessage(Sample          entity,                    string message) => FileActionMessage(entity, Save       , message);
        public string FileActionMessage(Sample          entity, string     action, int dummy = 0 ) => FileActionMessage(entity, action     , ""     );
        public string FileActionMessage(Sample          entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileActionMessage(entity.Location, action, message);
        }
        
        // LogAction (On Entities)
        
        public   FlowNode        LogAction(FlowNode        entity, ActionEnum action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, ActionEnum action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, string     action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, string     message, int dummy = 0 ) { LogActionBase(ActionMessage(entity, message, dummy)); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, string     action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        
        internal ConfigResolver  LogAction(ConfigResolver  entity, ActionEnum action                             ) { LogActionBase(ActionMessage(entity, action               )); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, ActionEnum action,              string message) { LogActionBase(ActionMessage(entity, action,       message)); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(ActionMessage(entity, action, name, dummy  )); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, ActionEnum action, string name, string message) { LogActionBase(ActionMessage(entity, action, name, message)); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity                                                ) { LogActionBase(ActionMessage(entity                       )); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity,                                 string message) { LogActionBase(ActionMessage(entity,               message)); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, string     action,              int dummy = 0 ) { LogActionBase(ActionMessage(entity, action,       dummy  )); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, string     action,              string message) { LogActionBase(ActionMessage(entity, action,       message)); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, string     action, string name, int dummy = 0 ) { LogActionBase(ActionMessage(entity, action, name, dummy  )); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, string     action, string name, string message) { LogActionBase(ActionMessage(entity, action, name, message)); return entity; }
        
        internal ConfigSection   LogAction(ConfigSection   entity, ActionEnum action                             ) { LogActionBase(ActionMessage(entity, action               )); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, ActionEnum action,              string message) { LogActionBase(ActionMessage(entity, action,       message)); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(ActionMessage(entity, action, name, dummy  )); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, ActionEnum action, string name, string message) { LogActionBase(ActionMessage(entity, action, name, message)); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity                                                ) { LogActionBase(ActionMessage(entity                       )); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity,                                 string message) { LogActionBase(ActionMessage(entity,               message)); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string     action,              int dummy = 0 ) { LogActionBase(ActionMessage(entity, action,       dummy  )); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string     action,              string message) { LogActionBase(ActionMessage(entity, action,       message)); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string     action, string name, int dummy = 0 ) { LogActionBase(ActionMessage(entity, action, name, dummy  )); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string     action, string name, string message) { LogActionBase(ActionMessage(entity, action, name, message)); return entity; }
        
        public   Tape            LogAction(Tape            entity, ActionEnum action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   Tape            LogAction(Tape            entity, ActionEnum action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        public   Tape            LogAction(Tape            entity, string     action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   Tape            LogAction(Tape            entity, string     message, int dummy = 0 ) { LogActionBase(ActionMessage(entity, message, dummy )); return entity; }
        public   Tape            LogAction(Tape            entity, string     action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
                                                         
        public   TapeConfig      LogAction(TapeConfig      entity, ActionEnum action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, ActionEnum action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, string     action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, string     message, int dummy = 0 ) { LogActionBase(ActionMessage(entity, message, dummy )); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, string     action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
                                                         
        public   TapeActions     LogAction(TapeActions     entity, ActionEnum action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, ActionEnum action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, string     action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, string     message, int dummy = 0 ) { LogActionBase(ActionMessage(entity, message, dummy )); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, string     action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }

        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      Log      (TapeAction      action                ) { LogActionBase(      Message(action         )); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      Log      (TapeAction      action, string message) { LogActionBase(      Message(action, message)); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      LogAction(TapeAction      action                ) { LogActionBase(ActionMessage(action         )); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      LogAction(TapeAction      action, string message) { LogActionBase(ActionMessage(action, message)); return action; }

        public   Buff            LogAction(Buff            entity, ActionEnum action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   Buff            LogAction(Buff            entity, ActionEnum action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        public   Buff            LogAction(Buff            entity, string     action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   Buff            LogAction(Buff            entity, string     message, int dummy = 0 ) { LogActionBase(ActionMessage(entity, message, dummy )); return entity; }
        public   Buff            LogAction(Buff            entity, string     action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }

        public   AudioFileOutput LogAction(AudioFileOutput entity, ActionEnum action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, ActionEnum action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, string     action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, string     message, int dummy = 0 ) { LogActionBase(ActionMessage(entity, message, dummy )); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, string     action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        
        public   Sample          LogAction(Sample          entity, ActionEnum action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   Sample          LogAction(Sample          entity, ActionEnum action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        public   Sample          LogAction(Sample          entity, string     action                 ) { LogActionBase(ActionMessage(entity, action         )); return entity; }
        public   Sample          LogAction(Sample          entity, string     message, int dummy = 0 ) { LogActionBase(ActionMessage(entity, message, dummy )); return entity; }
        public   Sample          LogAction(Sample          entity, string     action,  string message) { LogActionBase(ActionMessage(entity, action, message)); return entity; }
        
        // LogAction (On Sumple Types)

        
        public void LogAction(object entity,   ActionEnum action                             ) => LogActionBase(ActionMessage(entity, action               ));
        public void LogAction(object entity,   ActionEnum action,              string message) => LogActionBase(ActionMessage(entity, action,       message));
        public void LogAction(object entity,   ActionEnum action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(entity, action, name, dummy  ));
        public void LogAction(object entity,   ActionEnum action, string name, string message) => LogActionBase(ActionMessage(entity, action, name, message));
        public void LogAction(object entity                                                  ) => LogActionBase(ActionMessage(entity                       ));
        public void LogAction(object entity,                                   string message) => LogActionBase(ActionMessage(entity,               message));
        public void LogAction(object entity,   string     action,              int dummy = 0 ) => LogActionBase(ActionMessage(entity, action,       dummy  ));
        public void LogAction(object entity,   string     action,              string message) => LogActionBase(ActionMessage(entity, action,       message));
        public void LogAction(object entity,   string     action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(entity, action, name, dummy  ));
        public void LogAction(object entity,   string     action, string name, string message) => LogActionBase(ActionMessage(entity, action, name, message));

        public void LogAction<TEntity>(        ActionEnum action                             ) => LogActionBase(ActionMessage<TEntity>(action               ));
        public void LogAction<TEntity>(        ActionEnum action,              string message) => LogActionBase(ActionMessage<TEntity>(action,       message));
        public void LogAction<TEntity>(        ActionEnum action, string name, int dummy = 0 ) => LogActionBase(ActionMessage<TEntity>(action, name, dummy  ));
        public void LogAction<TEntity>(        ActionEnum action, string name, string message) => LogActionBase(ActionMessage<TEntity>(action, name, message));
        public void LogAction<TEntity>(                                                      ) => LogActionBase(ActionMessage<TEntity>(                     ));
        public void LogAction<TEntity>(                                        string message) => LogActionBase(ActionMessage<TEntity>(              message));
        public void LogAction<TEntity>(        string     action,              int dummy = 0 ) => LogActionBase(ActionMessage<TEntity>(action,       dummy  ));
        public void LogAction<TEntity>(        string     action,              string message) => LogActionBase(ActionMessage<TEntity>(action,       message));
        public void LogAction<TEntity>(        string     action, string name, int dummy = 0 ) => LogActionBase(ActionMessage<TEntity>(action, name, dummy  ));
        public void LogAction<TEntity>(        string     action, string name, string message) => LogActionBase(ActionMessage<TEntity>(action, name, message));
        
        public void LogAction(Type entityType, ActionEnum action                             ) => LogActionBase(ActionMessage(entityType, action               ));
        public void LogAction(Type entityType, ActionEnum action,              string message) => LogActionBase(ActionMessage(entityType, action,       message));
        public void LogAction(Type entityType, ActionEnum action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(entityType, action, name, dummy  ));
        public void LogAction(Type entityType, ActionEnum action, string name, string message) => LogActionBase(ActionMessage(entityType, action, name, message));
        public void LogAction(Type entityType                                                ) => LogActionBase(ActionMessage(entityType                       ));
        public void LogAction(Type entityType,                                 string message) => LogActionBase(ActionMessage(entityType,               message));
        public void LogAction(Type entityType, string     action,              int dummy = 0 ) => LogActionBase(ActionMessage(entityType, action,       dummy  ));
        public void LogAction(Type entityType, string     action,              string message) => LogActionBase(ActionMessage(entityType, action,       message));
        public void LogAction(Type entityType, string     action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(entityType, action, name, dummy  ));
        public void LogAction(Type entityType, string     action, string name, string message) => LogActionBase(ActionMessage(entityType, action, name, message));
        
        public void LogAction(string typeName, ActionEnum action                             ) => LogActionBase(ActionMessage(typeName, action               ));
        public void LogAction(string typeName, ActionEnum action,              string message) => LogActionBase(ActionMessage(typeName, action,       message));
        public void LogAction(string typeName, ActionEnum action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(typeName, action, name, dummy  ));
        public void LogAction(string typeName, ActionEnum action, string name, string message) => LogActionBase(ActionMessage(typeName, action, name, message));
        public void LogAction(string typeName                                                ) => LogActionBase(ActionMessage(typeName                       ));
        public void LogAction(string typeName,                                 string message) => LogActionBase(ActionMessage(typeName,               message));
        public void LogAction(string typeName, string     action,              int dummy = 0 ) => LogActionBase(ActionMessage(typeName, action,       dummy  ));
        public void LogAction(string typeName, string     action,              string message) => LogActionBase(ActionMessage(typeName, action,       message));
        public void LogAction(string typeName, string     action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(typeName, action, name, dummy  ));
        public void LogAction(string typeName, string     action, string name, string message) => LogActionBase(ActionMessage(typeName, action, name, message));
        
        // Memory Action Logging (On Simple Types)
        
        public void LogMemoryAction(int byteCount                                                ) => LogActionBase(MemoryActionMessage(byteCount                       ));
        public void LogMemoryAction(int byteCount,                                 string message) => LogActionBase(MemoryActionMessage(byteCount,               message));
        public void LogMemoryAction(int byteCount, ActionEnum action                             ) => LogActionBase(MemoryActionMessage(byteCount, action               ));
        public void LogMemoryAction(int byteCount, ActionEnum action,              string message) => LogActionBase(MemoryActionMessage(byteCount, action,       message));
        public void LogMemoryAction(int byteCount, ActionEnum action, string name, int dummy = 0 ) => LogActionBase(MemoryActionMessage(byteCount, action, name, dummy  ));
        public void LogMemoryAction(int byteCount, ActionEnum action, string name, string message) => LogActionBase(MemoryActionMessage(byteCount, action, name, message));
        public void LogMemoryAction(int byteCount,                    string name, int dummy = 0 ) => LogActionBase(MemoryActionMessage(byteCount,         name, dummy  ));
        public void LogMemoryAction(int byteCount,                    string name, string message) => LogActionBase(MemoryActionMessage(byteCount,         name, message));
        public void LogMemoryAction(int byteCount, string     action, string name, string message) => LogActionBase(MemoryActionMessage(byteCount, action, name, message));

        public void LogMemoryAction(byte[] bytes                                                ) => LogActionBase(MemoryActionMessage(bytes                       ));
        public void LogMemoryAction(byte[] bytes,                                 string message) => LogActionBase(MemoryActionMessage(bytes,               message));
        public void LogMemoryAction(byte[] bytes, ActionEnum action                             ) => LogActionBase(MemoryActionMessage(bytes, action               ));
        public void LogMemoryAction(byte[] bytes, ActionEnum action,              string message) => LogActionBase(MemoryActionMessage(bytes, action,       message));
        public void LogMemoryAction(byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => LogActionBase(MemoryActionMessage(bytes, action, name, dummy  ));
        public void LogMemoryAction(byte[] bytes, ActionEnum action, string name, string message) => LogActionBase(MemoryActionMessage(bytes, action, name, message));
        public void LogMemoryAction(byte[] bytes,                    string name, int dummy = 0 ) => LogActionBase(MemoryActionMessage(bytes,         name, dummy  ));
        public void LogMemoryAction(byte[] bytes,                    string name, string message) => LogActionBase(MemoryActionMessage(bytes,         name, message));
        public void LogMemoryAction(byte[] bytes, string     action, string name, string message) => LogActionBase(MemoryActionMessage(bytes, action, name, message));

        public void LogAction(byte[] bytes                                                ) => LogActionBase(ActionMessage(bytes                       ));
        public void LogAction(byte[] bytes,                                 string message) => LogActionBase(ActionMessage(bytes,               message));
        public void LogAction(byte[] bytes, ActionEnum action                             ) => LogActionBase(ActionMessage(bytes, action               ));
        public void LogAction(byte[] bytes, ActionEnum action,              string message) => LogActionBase(ActionMessage(bytes, action,       message));
        public void LogAction(byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(bytes, action, name, dummy  ));
        public void LogAction(byte[] bytes, ActionEnum action, string name, string message) => LogActionBase(ActionMessage(bytes, action, name, message));
        public void LogAction(byte[] bytes,                    string name, int dummy = 0 ) => LogActionBase(ActionMessage(bytes,         name, dummy  ));
        public void LogAction(byte[] bytes,                    string name, string message) => LogActionBase(ActionMessage(bytes,         name, message));
        public void LogAction(byte[] bytes, string     action, string name, string message) => LogActionBase(ActionMessage(bytes, action, name, message));

        // Memory Action Logging (On Entities)

        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes                                                ) { LogActionBase(MemoryActionMessage(entity, bytes                       )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                                 string message) { LogActionBase(MemoryActionMessage(entity, bytes,               message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, bytes, action               )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, bytes, action,       message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, dummy  )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes,         name, dummy  )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                    string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes,         name, message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }

        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes                                                ) { LogActionBase(MemoryActionMessage(entity, bytes                       )); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                                 string message) { LogActionBase(MemoryActionMessage(entity, bytes,               message)); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, bytes, action               )); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, bytes, action,       message)); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, dummy  )); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes,         name, dummy  )); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                    string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes,         name, message)); return entity; }
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }

        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes                                                ) { LogActionBase(MemoryActionMessage(entity, bytes                       )); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                                 string message) { LogActionBase(MemoryActionMessage(entity, bytes,               message)); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, bytes, action               )); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, bytes, action,       message)); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, dummy  )); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes,         name, dummy  )); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                    string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes,         name, message)); return entity; }
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }

        public   Tape            LogMemoryAction(Tape            entity                                                ) { LogActionBase(MemoryActionMessage(entity                       )); return entity; }
        public   Tape            LogMemoryAction(Tape            entity,                                 string message) { LogActionBase(MemoryActionMessage(entity,               message)); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, action               )); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, action,       message)); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, name, dummy  )); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }
        public   Tape            LogMemoryAction(Tape            entity,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity,         name, dummy  )); return entity; }
        public   Tape            LogMemoryAction(Tape            entity,                    string name, string message) { LogActionBase(MemoryActionMessage(entity,         name, message)); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }

        public   TapeConfig      LogMemoryAction(TapeConfig      entity                                                ) { LogActionBase(MemoryActionMessage(entity                       )); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity,                                 string message) { LogActionBase(MemoryActionMessage(entity,               message)); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, action               )); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, action,       message)); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, name, dummy  )); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity,         name, dummy  )); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity,                    string name, string message) { LogActionBase(MemoryActionMessage(entity,         name, message)); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }

        public   TapeActions     LogMemoryAction(TapeActions     entity                                                ) { LogActionBase(MemoryActionMessage(entity                       )); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity,                                 string message) { LogActionBase(MemoryActionMessage(entity,               message)); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, action               )); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, action,       message)); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, name, dummy  )); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity,         name, dummy  )); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity,                    string name, string message) { LogActionBase(MemoryActionMessage(entity,         name, message)); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }

        public   TapeAction      LogMemoryAction(TapeAction      entity                                                ) { LogActionBase(MemoryActionMessage(entity                       )); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity,                                 string message) { LogActionBase(MemoryActionMessage(entity,               message)); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, action               )); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, action,       message)); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, name, dummy  )); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity,         name, dummy  )); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity,                    string name, string message) { LogActionBase(MemoryActionMessage(entity,         name, message)); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }

        public   Buff            LogMemoryAction(Buff            entity                                                ) { LogActionBase(MemoryActionMessage(entity                       )); return entity; }
        public   Buff            LogMemoryAction(Buff            entity,                                 string message) { LogActionBase(MemoryActionMessage(entity,               message)); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, action               )); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, action,       message)); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, name, dummy  )); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }
        public   Buff            LogMemoryAction(Buff            entity,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity,         name, dummy  )); return entity; }
        public   Buff            LogMemoryAction(Buff            entity,                    string name, string message) { LogActionBase(MemoryActionMessage(entity,         name, message)); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }

        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes                                                ) { LogActionBase(MemoryActionMessage(entity, bytes                       )); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes,                                 string message) { LogActionBase(MemoryActionMessage(entity, bytes,               message)); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, bytes, action               )); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, bytes, action,       message)); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, dummy  )); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes,         name, dummy  )); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes,                    string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes,         name, message)); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }

        public   Sample          LogMemoryAction(Sample          entity                                                ) { LogActionBase(MemoryActionMessage(entity                       )); return entity; }
        public   Sample          LogMemoryAction(Sample          entity,                                 string message) { LogActionBase(MemoryActionMessage(entity,               message)); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, action               )); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, action,       message)); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, name, dummy  )); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }
        public   Sample          LogMemoryAction(Sample          entity,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity,         name, dummy  )); return entity; }
        public   Sample          LogMemoryAction(Sample          entity,                    string name, string message) { LogActionBase(MemoryActionMessage(entity,         name, message)); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, action, name, message)); return entity; }
        
        // TODO
        
        public void LogFileAction(string filePath, string sourceFilePath = null) => LogActionBase(FileActionMessage(filePath, sourceFilePath));

        private void LogActionBase(string actionMessage) => Log("Actions", actionMessage);
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
