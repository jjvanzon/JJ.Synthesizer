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
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
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
        
        public string ActionMessage(object entity,        ActionEnum action                             ) => ActionMessage(entity,          action  , ""  , ""     );
        public string ActionMessage(object entity,        ActionEnum action,              string message) => ActionMessage(entity,          action  , ""  , message);
        public string ActionMessage(object entity,        ActionEnum action, string name, int dummy = 0 ) => ActionMessage(entity,       $"{action}", name, ""     );
        public string ActionMessage(object entity,        ActionEnum action, string name, string message) => ActionMessage(entity,       $"{action}", name, message);
        public string ActionMessage(object entity                                                       ) => ActionMessage(entity,          ""      , ""  , ""     );
        public string ActionMessage(object entity,                                        string message) => ActionMessage(entity,          ""      , ""  , message);
        public string ActionMessage(object entity,        string     action,              int dummy = 0 ) => ActionMessage(entity,          action  , ""  , ""     );
        public string ActionMessage(object entity,        string     action,              string message) => ActionMessage(entity,          action  , ""  , message);
        public string ActionMessage(object entity,        string     action, string name, int dummy = 0 ) => ActionMessage(entity,          action  , name, ""     );
        public string ActionMessage(object entity,        string     action, string name, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return ActionMessage(entity.GetType().Name, action, name, message);
        }

        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, ActionEnum action                             ) => ActionMessage<TEntity>(        action  , ""  , ""     );
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, ActionEnum action,              string message) => ActionMessage<TEntity>(        action  , ""  , message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, ActionEnum action, string name, int dummy = 0 ) => ActionMessage<TEntity>(     $"{action}", name, ""     );
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, ActionEnum action, string name, string message) => ActionMessage<TEntity>(     $"{action}", name, message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj                                                ) => ActionMessage<TEntity>(        ""      , ""  , ""     );
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj,                                 string message) => ActionMessage<TEntity>(        ""      , ""  , message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, string     action,              int dummy = 0 ) => ActionMessage<TEntity>(        action  , ""  , ""     );
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, string     action,              string message) => ActionMessage<TEntity>(        action  , ""  , message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, string     action, string name, int dummy = 0 ) => ActionMessage<TEntity>(        action  , name, ""     );
        /// <inheritdoc cref="actionmethodtentityobject />
        public string ActionMessage<TEntity>(TEntity obj, string     action, string name, string message) => ActionMessage<TEntity>(        action  , name, message);
        
        public string ActionMessage<TEntity>(             ActionEnum action                             ) => ActionMessage(typeof(TEntity), action  , ""  , ""     );
        public string ActionMessage<TEntity>(             ActionEnum action,              string message) => ActionMessage(typeof(TEntity), action  , ""  , message);
        public string ActionMessage<TEntity>(             ActionEnum action, string name, int dummy = 0 ) => ActionMessage(typeof(TEntity), action  , name, ""     );
        public string ActionMessage<TEntity>(             ActionEnum action, string name, string message) => ActionMessage(typeof(TEntity), action  , name, message);
        public string ActionMessage<TEntity>(                                                           ) => ActionMessage(typeof(TEntity), ""      , ""  , ""     );
        public string ActionMessage<TEntity>(                                             string message) => ActionMessage(typeof(TEntity), ""      , ""  , message);
        public string ActionMessage<TEntity>(             string     action,              int dummy = 0 ) => ActionMessage(typeof(TEntity), action  , ""  , ""     );
        public string ActionMessage<TEntity>(             string     action,              string message) => ActionMessage(typeof(TEntity), action  , ""  , message);
        public string ActionMessage<TEntity>(             string     action, string name, int dummy = 0 ) => ActionMessage(typeof(TEntity), action  , name, ""     );
        public string ActionMessage<TEntity>(             string     action, string name, string message) => ActionMessage(typeof(TEntity), action  , name, message);

        public string ActionMessage(Type entityType,      ActionEnum action                             ) => ActionMessage(entityType,      action  , ""  , ""     );
        public string ActionMessage(Type entityType,      ActionEnum action,              string message) => ActionMessage(entityType,      action  , ""  , message);
        public string ActionMessage(Type entityType,      ActionEnum action, string name, int dummy = 0 ) => ActionMessage(entityType,      action  , name, ""     );  
        public string ActionMessage(Type entityType,      ActionEnum action, string name, string message) => ActionMessage(entityType,   $"{action}", name, message);
        public string ActionMessage(Type entityType                                                     ) => ActionMessage(entityType,      ""      , ""  , ""     );
        public string ActionMessage(Type entityType,                                      string message) => ActionMessage(entityType,      ""      , ""  , message);
        public string ActionMessage(Type entityType,      string     action,              int dummy = 0 ) => ActionMessage(entityType,      action  , ""  , ""     );
        public string ActionMessage(Type entityType,      string     action,              string message) => ActionMessage(entityType,      action  , ""  , message);
        public string ActionMessage(Type entityType,      string     action, string name, int dummy = 0 ) => ActionMessage(entityType,      action  , name, ""     );
        public string ActionMessage(Type entityType,      string     action, string name, string message)
        {
            if (entityType == null) throw new NullException(() => entityType);
            return ActionMessage(entityType.Name, action, name, message);
        }
        
        public string ActionMessage(string typeName,      ActionEnum action                             ) => ActionMessage(typeName,        action  , ""  , ""     );
        public string ActionMessage(string typeName,      ActionEnum action,              string message) => ActionMessage(typeName,        action  , ""  , message);
        public string ActionMessage(string typeName,      ActionEnum action, string name, int dummy = 0 ) => ActionMessage(typeName,        action  , name, ""     );
        public string ActionMessage(string typeName,      ActionEnum action, string name, string message) => ActionMessage(typeName,     $"{action}", name, message);
        public string ActionMessage(string typeName                                                     ) => ActionMessage(typeName,        ""      , ""  , ""     );
        public string ActionMessage(string typeName,                                      string message) => ActionMessage(typeName,        ""      , ""  , message);
        public string ActionMessage(string typeName,      string     action,              int dummy = 0 ) => ActionMessage(typeName,        action  , ""  , ""     );
        public string ActionMessage(string typeName,      string     action,              string message) => ActionMessage(typeName,        action  , ""  , message);
        public string ActionMessage(string typeName,      string     action, string name, int dummy = 0 ) => ActionMessage(typeName,        action  , name, ""     );
        public string ActionMessage(string typeName,      string     action, string name, string message)
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
        
        // (Always tagged [MEMORY] here, so no need for target types: object. entity Type or TEntity.)
        
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

        public string MemoryActionMessage(byte[] bytes                                                ) => MemoryActionMessage(bytes?.Length ?? 0                       );
        public string MemoryActionMessage(byte[] bytes,                                 string message) => MemoryActionMessage(bytes?.Length ?? 0,               message);
        public string MemoryActionMessage(byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes?.Length ?? 0, action               );   
        public string MemoryActionMessage(byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes?.Length ?? 0, action,       message);
        public string MemoryActionMessage(byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes?.Length ?? 0, action, name, dummy  );
        public string MemoryActionMessage(byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes?.Length ?? 0, action, name, message);
        public string MemoryActionMessage(byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes?.Length ?? 0,         name, dummy  );
        public string MemoryActionMessage(byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes?.Length ?? 0,         name, message);
        public string MemoryActionMessage(byte[] bytes, string     action, string name, string message) => MemoryActionMessage(bytes?.Length ?? 0, action, name, message);
                                                                    
        public string       ActionMessage(byte[] bytes                                                ) => MemoryActionMessage(bytes                       );
        public string       ActionMessage(byte[] bytes,                                 string message) => MemoryActionMessage(bytes,               message);
        public string       ActionMessage(byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes, action               );   
        public string       ActionMessage(byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes, action,       message);
        public string       ActionMessage(byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes, action, name, dummy  );
        public string       ActionMessage(byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes, action, name, message);
        public string       ActionMessage(byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes,         name, dummy  );
        public string       ActionMessage(byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes,         name, message);
        public string       ActionMessage(byte[] bytes, string     action, string name, string message) => MemoryActionMessage(bytes, action, name, message);
        
        // Memory Action Message (On Entities)
        
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes                                   ) => MemoryActionMessage(entity, bytes, "Write"    , ""     );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                    string message) => MemoryActionMessage(entity, bytes, "Write"    , message);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action                ) => MemoryActionMessage(entity, bytes, $"{action}", ""     );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action, string message) => MemoryActionMessage(entity, bytes, $"{action}", message);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, string     action, int dummy = 0 ) => MemoryActionMessage(entity, bytes, "Write"    , ""     );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(bytes, action, entity.Name, message);
        }

        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes                                                ) => MemoryActionMessage(bytes                       );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                                 string message) => MemoryActionMessage(bytes,               message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes, action               );   
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes, action,       message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes, action, name, dummy  );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes, action, name, message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes,         name, dummy  );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes,         name, message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, string     action, string name, string message) => MemoryActionMessage(bytes, action, name, message);

        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes                                                ) => MemoryActionMessage(bytes                       );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                                 string message) => MemoryActionMessage(bytes,               message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action                             ) => MemoryActionMessage(bytes, action               );   
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action,              string message) => MemoryActionMessage(bytes, action,       message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => MemoryActionMessage(bytes, action, name, dummy  );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, string message) => MemoryActionMessage(bytes, action, name, message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                    string name, int dummy = 0 ) => MemoryActionMessage(bytes,         name, dummy  );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                    string name, string message) => MemoryActionMessage(bytes,         name, message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, string     action, string name, string message) => MemoryActionMessage(bytes, action, name, message);
        
        public   string MemoryActionMessage(Tape            entity                                   ) => MemoryActionMessage(entity, "Write"    , ""     );
        public   string MemoryActionMessage(Tape            entity,                    string message) => MemoryActionMessage(entity, "Write"    , message);
        public   string MemoryActionMessage(Tape            entity, ActionEnum action                ) => MemoryActionMessage(entity, $"{action}", ""     );
        public   string MemoryActionMessage(Tape            entity, ActionEnum action, string message) => MemoryActionMessage(entity, $"{action}", message);
        public   string MemoryActionMessage(Tape            entity, string     action, int dummy = 0 ) => MemoryActionMessage(entity,    action  , ""     );
        public   string MemoryActionMessage(Tape            entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Bytes, action, entity.Descriptor, Coalesce(message, PrettyByteCount(entity.Bytes)));
        }

        public   string MemoryActionMessage(TapeConfig      entity                                   ) => MemoryActionMessage(entity, "Write"    , ""     );
        public   string MemoryActionMessage(TapeConfig      entity,                    string message) => MemoryActionMessage(entity, "Write"    , message);
        public   string MemoryActionMessage(TapeConfig      entity, ActionEnum action                ) => MemoryActionMessage(entity, $"{action}", ""     );
        public   string MemoryActionMessage(TapeConfig      entity, ActionEnum action, string message) => MemoryActionMessage(entity, $"{action}", message);
        public   string MemoryActionMessage(TapeConfig      entity, string     action, int dummy = 0 ) => MemoryActionMessage(entity,    action  , ""     );
        public   string MemoryActionMessage(TapeConfig      entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, message);
        }
        
        public   string MemoryActionMessage(TapeActions     entity                                   ) => MemoryActionMessage(entity, "Write"    , ""     );
        public   string MemoryActionMessage(TapeActions     entity,                    string message) => MemoryActionMessage(entity, "Write"    , message);
        public   string MemoryActionMessage(TapeActions     entity, ActionEnum action                ) => MemoryActionMessage(entity, $"{action}", ""     );
        public   string MemoryActionMessage(TapeActions     entity, ActionEnum action, string message) => MemoryActionMessage(entity, $"{action}", message);
        public   string MemoryActionMessage(TapeActions     entity, string     action, int dummy = 0 ) => MemoryActionMessage(entity,    action  , ""     );
        public   string MemoryActionMessage(TapeActions     entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, message);
        }
        
        public   string MemoryActionMessage(TapeAction      entity                                   ) => MemoryActionMessage(entity, "Write"    , ""     );
        public   string MemoryActionMessage(TapeAction      entity,                    string message) => MemoryActionMessage(entity, "Write"    , message);
        public   string MemoryActionMessage(TapeAction      entity, ActionEnum action                ) => MemoryActionMessage(entity, $"{action}", ""     );
        public   string MemoryActionMessage(TapeAction      entity, ActionEnum action, string message) => MemoryActionMessage(entity, $"{action}", message);
        public   string MemoryActionMessage(TapeAction      entity, string     action, int dummy = 0 ) => MemoryActionMessage(entity, "Write"    , ""     );
        public   string MemoryActionMessage(TapeAction      entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Tape, action, message);
        }

        public   string MemoryActionMessage(Buff            entity                                   ) => MemoryActionMessage(entity, "Write"    , ""     );
        public   string MemoryActionMessage(Buff            entity,                    string message) => MemoryActionMessage(entity, "Write"    , message);
        public   string MemoryActionMessage(Buff            entity, ActionEnum action                ) => MemoryActionMessage(entity, $"{action}", ""     );
        public   string MemoryActionMessage(Buff            entity, ActionEnum action, string message) => MemoryActionMessage(entity, $"{action}", message);
        public   string MemoryActionMessage(Buff            entity, string     action, int dummy = 0 ) => MemoryActionMessage(entity, "Write"    , ""     );
        public   string MemoryActionMessage(Buff            entity, string     action, string message)
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
                return MemoryActionMessage(entity.Bytes, action, message);
            }
        }
        
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes                                   ) => MemoryActionMessage(entity, bytes, "Write"    , ""     );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                    string message) => MemoryActionMessage(entity, bytes, "Write"    , message);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action                ) => MemoryActionMessage(entity, bytes, $"{action}", ""     );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action, string message) => MemoryActionMessage(entity, bytes, $"{action}", message);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, string     action, int dummy = 0 ) => MemoryActionMessage(entity, bytes, "Write"    , ""     );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(bytes, action, entity.Name, message);
        }
        
        public   string MemoryActionMessage(Sample          entity                                   ) => MemoryActionMessage(entity, "Write"    , ""     );
        public   string MemoryActionMessage(Sample          entity,                    string message) => MemoryActionMessage(entity, "Write"    , message);
        public   string MemoryActionMessage(Sample          entity, ActionEnum action                ) => MemoryActionMessage(entity, $"{action}", ""     );
        public   string MemoryActionMessage(Sample          entity, ActionEnum action, string message) => MemoryActionMessage(entity, $"{action}", message);
        public   string MemoryActionMessage(Sample          entity, string     action, int dummy = 0 ) => MemoryActionMessage(entity, "Write"    , ""     );
        public   string MemoryActionMessage(Sample          entity, string     action, string message)
        {
            if (entity == null) throw new NullException(() => entity);
            return MemoryActionMessage(entity.Bytes, action, entity.Name, message);
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
        public   FlowNode        LogAction(FlowNode        entity, string     message, int dummy = 0 ) { LogActionBase(ActionMessage(entity, message, dummy )); return entity; }
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
        
        // LogAction (On Simple Types)
        
        public void LogAction(object entity,   ActionEnum action                             ) => LogActionBase(ActionMessage(entity,     action               ));
        public void LogAction(object entity,   ActionEnum action,              string message) => LogActionBase(ActionMessage(entity,     action,       message));
        public void LogAction(object entity,   ActionEnum action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(entity,     action, name, dummy  ));
        public void LogAction(object entity,   ActionEnum action, string name, string message) => LogActionBase(ActionMessage(entity,     action, name, message));
        public void LogAction(object entity                                                  ) => LogActionBase(ActionMessage(entity                           ));
        public void LogAction(object entity,                                   string message) => LogActionBase(ActionMessage(entity,                   message));
        public void LogAction(object entity,   string     action,              int dummy = 0 ) => LogActionBase(ActionMessage(entity,     action,       dummy  ));
        public void LogAction(object entity,   string     action,              string message) => LogActionBase(ActionMessage(entity,     action,       message));
        public void LogAction(object entity,   string     action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(entity,     action, name, dummy  ));
        public void LogAction(object entity,   string     action, string name, string message) => LogActionBase(ActionMessage(entity,     action, name, message));
        public void LogAction<TEntity>(        ActionEnum action                             ) => LogActionBase(ActionMessage<TEntity>(   action               ));
        public void LogAction<TEntity>(        ActionEnum action,              string message) => LogActionBase(ActionMessage<TEntity>(   action,       message));
        public void LogAction<TEntity>(        ActionEnum action, string name, int dummy = 0 ) => LogActionBase(ActionMessage<TEntity>(   action, name, dummy  ));
        public void LogAction<TEntity>(        ActionEnum action, string name, string message) => LogActionBase(ActionMessage<TEntity>(   action, name, message));
        public void LogAction<TEntity>(                                                      ) => LogActionBase(ActionMessage<TEntity>(                        ));
        public void LogAction<TEntity>(                                        string message) => LogActionBase(ActionMessage<TEntity>(                 message));
        public void LogAction<TEntity>(        string     action,              int dummy = 0 ) => LogActionBase(ActionMessage<TEntity>(   action,       dummy  ));
        public void LogAction<TEntity>(        string     action,              string message) => LogActionBase(ActionMessage<TEntity>(   action,       message));
        public void LogAction<TEntity>(        string     action, string name, int dummy = 0 ) => LogActionBase(ActionMessage<TEntity>(   action, name, dummy  ));
        public void LogAction<TEntity>(        string     action, string name, string message) => LogActionBase(ActionMessage<TEntity>(   action, name, message));
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
        public void LogAction(string typeName, ActionEnum action                             ) => LogActionBase(ActionMessage(typeName,   action               ));
        public void LogAction(string typeName, ActionEnum action,              string message) => LogActionBase(ActionMessage(typeName,   action,       message));
        public void LogAction(string typeName, ActionEnum action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(typeName,   action, name, dummy  ));
        public void LogAction(string typeName, ActionEnum action, string name, string message) => LogActionBase(ActionMessage(typeName,   action, name, message));
        public void LogAction(string typeName                                                ) => LogActionBase(ActionMessage(typeName                         ));
        public void LogAction(string typeName,                                 string message) => LogActionBase(ActionMessage(typeName,                 message));
        public void LogAction(string typeName, string     action,              int dummy = 0 ) => LogActionBase(ActionMessage(typeName,   action,       dummy  ));
        public void LogAction(string typeName, string     action,              string message) => LogActionBase(ActionMessage(typeName,   action,       message));
        public void LogAction(string typeName, string     action, string name, int dummy = 0 ) => LogActionBase(ActionMessage(typeName,   action, name, dummy  ));
        public void LogAction(string typeName, string     action, string name, string message) => LogActionBase(ActionMessage(typeName,   action, name, message));
        
        // Memory Action Logging (On Simple Types)
        
        public void   LogMemoryAction(int byteCount                                                ) => LogActionBase(MemoryActionMessage(byteCount                       ));
        public void   LogMemoryAction(int byteCount,                                 string message) => LogActionBase(MemoryActionMessage(byteCount,               message));
        public void   LogMemoryAction(int byteCount, ActionEnum action                             ) => LogActionBase(MemoryActionMessage(byteCount, action               ));
        public void   LogMemoryAction(int byteCount, ActionEnum action,              string message) => LogActionBase(MemoryActionMessage(byteCount, action,       message));
        public void   LogMemoryAction(int byteCount, ActionEnum action, string name, int dummy = 0 ) => LogActionBase(MemoryActionMessage(byteCount, action, name, dummy  ));
        public void   LogMemoryAction(int byteCount, ActionEnum action, string name, string message) => LogActionBase(MemoryActionMessage(byteCount, action, name, message));
        public void   LogMemoryAction(int byteCount,                    string name, int dummy = 0 ) => LogActionBase(MemoryActionMessage(byteCount,         name, dummy  ));
        public void   LogMemoryAction(int byteCount,                    string name, string message) => LogActionBase(MemoryActionMessage(byteCount,         name, message));
        public void   LogMemoryAction(int byteCount, string     action, string name, string message) => LogActionBase(MemoryActionMessage(byteCount, action, name, message));
        public byte[] LogMemoryAction(byte[] bytes                                                 )  { LogActionBase(MemoryActionMessage(bytes                           )); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,                                  string message)  { LogActionBase(MemoryActionMessage(bytes,                   message)); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,  ActionEnum action                             )  { LogActionBase(MemoryActionMessage(bytes,     action               )); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,  ActionEnum action,              string message)  { LogActionBase(MemoryActionMessage(bytes,     action,       message)); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,  ActionEnum action, string name, int dummy = 0 )  { LogActionBase(MemoryActionMessage(bytes,     action, name, dummy  )); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,  ActionEnum action, string name, string message)  { LogActionBase(MemoryActionMessage(bytes,     action, name, message)); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,                     string name, int dummy = 0 )  { LogActionBase(MemoryActionMessage(bytes,             name, dummy  )); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,                     string name, string message)  { LogActionBase(MemoryActionMessage(bytes,             name, message)); return bytes; }
        public byte[] LogMemoryAction(byte[] bytes,  string     action, string name, string message)  { LogActionBase(MemoryActionMessage(bytes,     action, name, message)); return bytes; }
        public byte[] LogAction      (byte[] bytes                                                 )  { LogActionBase(      ActionMessage(bytes                           )); return bytes; }
        public byte[] LogAction      (byte[] bytes,                                  string message)  { LogActionBase(      ActionMessage(bytes,                   message)); return bytes; }
        public byte[] LogAction      (byte[] bytes,  ActionEnum action                             )  { LogActionBase(      ActionMessage(bytes,     action               )); return bytes; }
        public byte[] LogAction      (byte[] bytes,  ActionEnum action,              string message)  { LogActionBase(      ActionMessage(bytes,     action,       message)); return bytes; }
        public byte[] LogAction      (byte[] bytes,  ActionEnum action, string name, int dummy = 0 )  { LogActionBase(      ActionMessage(bytes,     action, name, dummy  )); return bytes; }
        public byte[] LogAction      (byte[] bytes,  ActionEnum action, string name, string message)  { LogActionBase(      ActionMessage(bytes,     action, name, message)); return bytes; }
        public byte[] LogAction      (byte[] bytes,                     string name, int dummy = 0 )  { LogActionBase(      ActionMessage(bytes,             name, dummy  )); return bytes; }
        public byte[] LogAction      (byte[] bytes,                     string name, string message)  { LogActionBase(      ActionMessage(bytes,             name, message)); return bytes; }
        public byte[] LogAction      (byte[] bytes,  string     action, string name, string message)  { LogActionBase(      ActionMessage(bytes,     action, name, message)); return bytes; }

        // Memory Action Logging (On Entities)

        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes                                   ) { LogActionBase(MemoryActionMessage(entity, bytes                 )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                    string message) { LogActionBase(MemoryActionMessage(entity, bytes,         message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action                ) { LogActionBase(MemoryActionMessage(entity, bytes, action         )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, string     action, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes, action, dummy  )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, string     action, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, message)); return entity; }
        
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

        public   Tape            LogMemoryAction(Tape            entity                                   ) { LogActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   Tape            LogMemoryAction(Tape            entity,                    string message) { LogActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, ActionEnum action                ) { LogActionBase(MemoryActionMessage(entity, action         )); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, ActionEnum action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, string     action, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, dummy  )); return entity; }
        public   Tape            LogMemoryAction(Tape            entity, string     action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity                                   ) { LogActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity,                    string message) { LogActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, ActionEnum action                ) { LogActionBase(MemoryActionMessage(entity, action         )); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, ActionEnum action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, string     action, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, dummy  )); return entity; }
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, string     action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity                                   ) { LogActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity,                    string message) { LogActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, ActionEnum action                ) { LogActionBase(MemoryActionMessage(entity, action         )); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, ActionEnum action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, string     action, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, dummy  )); return entity; }
        public   TapeActions     LogMemoryAction(TapeActions     entity, string     action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity                                   ) { LogActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity,                    string message) { LogActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, ActionEnum action                ) { LogActionBase(MemoryActionMessage(entity, action         )); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, ActionEnum action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, string     action, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, dummy  )); return entity; }
        public   TapeAction      LogMemoryAction(TapeAction      entity, string     action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   Buff            LogMemoryAction(Buff            entity                                   ) { LogActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   Buff            LogMemoryAction(Buff            entity,                    string message) { LogActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, ActionEnum action                ) { LogActionBase(MemoryActionMessage(entity, action         )); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, ActionEnum action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, string     action, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, dummy  )); return entity; }
        public   Buff            LogMemoryAction(Buff            entity, string     action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes                                   ) { LogActionBase(MemoryActionMessage(entity, bytes                 )); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes,                    string message) { LogActionBase(MemoryActionMessage(entity, bytes,         message)); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, ActionEnum action                ) { LogActionBase(MemoryActionMessage(entity, bytes, action         )); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, ActionEnum action, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, message)); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, string     action, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes, action, dummy  )); return entity; }
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, string     action, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, message)); return entity; }
        
        public   Sample          LogMemoryAction(Sample          entity                                   ) { LogActionBase(MemoryActionMessage(entity                 )); return entity; }
        public   Sample          LogMemoryAction(Sample          entity,                    string message) { LogActionBase(MemoryActionMessage(entity,         message)); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, ActionEnum action                ) { LogActionBase(MemoryActionMessage(entity, action         )); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, ActionEnum action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, string     action, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, action, dummy  )); return entity; }
        public   Sample          LogMemoryAction(Sample          entity, string     action, string message) { LogActionBase(MemoryActionMessage(entity, action, message)); return entity; }
        
        // LogFileAction
        
        public   string         LogFileAction(                       string filePath                                                          ) { LogActionBase(FileActionMessage(        filePath                                 )); return filePath; }
        public   string         LogFileAction(                       string filePath,                                    string sourceFilePath) { LogActionBase(FileActionMessage(        filePath,                  sourceFilePath)); return filePath; }
        public   string         LogFileAction(                       string filePath, ActionEnum action                                       ) { LogActionBase(FileActionMessage(        filePath, action                         )); return filePath; }
        public   string         LogFileAction(                       string filePath, ActionEnum action,                 string sourceFilePath) { LogActionBase(FileActionMessage(        filePath, action,          sourceFilePath)); return filePath; }
        public   string         LogFileAction(                       string filePath, ActionEnum action, string message, string sourceFilePath) { LogActionBase(FileActionMessage(        filePath, action, message, sourceFilePath)); return filePath; }
        public   string         LogFileAction(                       string filePath, string     action,                 int dummy = 0        ) { LogActionBase(FileActionMessage(        filePath, action,          dummy         )); return filePath; }
        public   string         LogFileAction(                       string filePath, string     action,                 string sourceFilePath) { LogActionBase(FileActionMessage(        filePath, action,          sourceFilePath)); return filePath; }
        public   string         LogFileAction(                       string filePath, string     action, string message, string sourceFilePath) { LogActionBase(FileActionMessage(        filePath, action, message, sourceFilePath)); return filePath; }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath                                                          ) { LogActionBase(FileActionMessage(entity, filePath                                 )); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath,                                    string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath,                  sourceFilePath)); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, ActionEnum action                                       ) { LogActionBase(FileActionMessage(entity, filePath, action                         )); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, ActionEnum action,                 string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action,          sourceFilePath)); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, ActionEnum action, string message, string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action, message, sourceFilePath)); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, string     action,                 int dummy = 0        ) { LogActionBase(FileActionMessage(entity, filePath, action,          dummy         )); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, string     action,                 string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action,          sourceFilePath)); return entity;   }
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, string     action, string message, string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action, message, sourceFilePath)); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath                                                          ) { LogActionBase(FileActionMessage(entity, filePath                                 )); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath,                                    string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath,                  sourceFilePath)); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, ActionEnum action                                       ) { LogActionBase(FileActionMessage(entity, filePath, action                         )); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, ActionEnum action,                 string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action,          sourceFilePath)); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, ActionEnum action, string message, string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action, message, sourceFilePath)); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, string     action,                 int dummy = 0        ) { LogActionBase(FileActionMessage(entity, filePath, action,          dummy         )); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, string     action,                 string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action,          sourceFilePath)); return entity;   }
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, string     action, string message, string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action, message, sourceFilePath)); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath                                                          ) { LogActionBase(FileActionMessage(entity, filePath                                 )); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath,                                    string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath,                  sourceFilePath)); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, ActionEnum action                                       ) { LogActionBase(FileActionMessage(entity, filePath, action                         )); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, ActionEnum action,                 string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action,          sourceFilePath)); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, ActionEnum action, string message, string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action, message, sourceFilePath)); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, string     action,                 int dummy = 0        ) { LogActionBase(FileActionMessage(entity, filePath, action,          dummy         )); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, string     action,                 string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action,          sourceFilePath)); return entity;   }
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, string     action, string message, string sourceFilePath) { LogActionBase(FileActionMessage(entity, filePath, action, message, sourceFilePath)); return entity;   }

        public Tape            LogFileAction(Tape            entity                                   ) { LogActionBase(FileActionMessage(entity                 )); return entity; }
        public Tape            LogFileAction(Tape            entity, ActionEnum action                ) { LogActionBase(FileActionMessage(entity, action         )); return entity; }
        public Tape            LogFileAction(Tape            entity, ActionEnum action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public Tape            LogFileAction(Tape            entity,                    string message) { LogActionBase(FileActionMessage(entity,         message)); return entity; }
        public Tape            LogFileAction(Tape            entity, string     action, int dummy = 0 ) { LogActionBase(FileActionMessage(entity, action, dummy  )); return entity; }
        public Tape            LogFileAction(Tape            entity, string     action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public TapeConfig      LogFileAction(TapeConfig      entity                                   ) { LogActionBase(FileActionMessage(entity                 )); return entity; }
        public TapeConfig      LogFileAction(TapeConfig      entity, ActionEnum action                ) { LogActionBase(FileActionMessage(entity, action         )); return entity; }
        public TapeConfig      LogFileAction(TapeConfig      entity, ActionEnum action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public TapeConfig      LogFileAction(TapeConfig      entity,                    string message) { LogActionBase(FileActionMessage(entity,         message)); return entity; }
        public TapeConfig      LogFileAction(TapeConfig      entity, string     action, int dummy = 0 ) { LogActionBase(FileActionMessage(entity, action, dummy  )); return entity; }
        public TapeConfig      LogFileAction(TapeConfig      entity, string     action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public TapeActions     LogFileAction(TapeActions     entity                                   ) { LogActionBase(FileActionMessage(entity                 )); return entity; }
        public TapeActions     LogFileAction(TapeActions     entity, ActionEnum action                ) { LogActionBase(FileActionMessage(entity, action         )); return entity; }
        public TapeActions     LogFileAction(TapeActions     entity, ActionEnum action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public TapeActions     LogFileAction(TapeActions     entity,                    string message) { LogActionBase(FileActionMessage(entity,         message)); return entity; }
        public TapeActions     LogFileAction(TapeActions     entity, string     action, int dummy = 0 ) { LogActionBase(FileActionMessage(entity, action, dummy  )); return entity; }
        public TapeActions     LogFileAction(TapeActions     entity, string     action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public TapeAction      LogFileAction(TapeAction      entity                                   ) { LogActionBase(FileActionMessage(entity                 )); return entity; }
        public TapeAction      LogFileAction(TapeAction      entity, ActionEnum action                ) { LogActionBase(FileActionMessage(entity, action         )); return entity; }
        public TapeAction      LogFileAction(TapeAction      entity, ActionEnum action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public TapeAction      LogFileAction(TapeAction      entity,                    string message) { LogActionBase(FileActionMessage(entity,         message)); return entity; }
        public TapeAction      LogFileAction(TapeAction      entity, string     action, int dummy = 0 ) { LogActionBase(FileActionMessage(entity, action, dummy  )); return entity; }
        public TapeAction      LogFileAction(TapeAction      entity, string     action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public Buff            LogFileAction(Buff            entity                                   ) { LogActionBase(FileActionMessage(entity                 )); return entity; }
        public Buff            LogFileAction(Buff            entity, ActionEnum action                ) { LogActionBase(FileActionMessage(entity, action         )); return entity; }
        public Buff            LogFileAction(Buff            entity, ActionEnum action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public Buff            LogFileAction(Buff            entity,                    string message) { LogActionBase(FileActionMessage(entity,         message)); return entity; }
        public Buff            LogFileAction(Buff            entity, string     action, int dummy = 0 ) { LogActionBase(FileActionMessage(entity, action, dummy  )); return entity; }
        public Buff            LogFileAction(Buff            entity, string     action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public AudioFileOutput LogFileAction(AudioFileOutput entity                                   ) { LogActionBase(FileActionMessage(entity                 )); return entity; }
        public AudioFileOutput LogFileAction(AudioFileOutput entity, ActionEnum action                ) { LogActionBase(FileActionMessage(entity, action         )); return entity; }
        public AudioFileOutput LogFileAction(AudioFileOutput entity, ActionEnum action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public AudioFileOutput LogFileAction(AudioFileOutput entity,                    string message) { LogActionBase(FileActionMessage(entity,         message)); return entity; }
        public AudioFileOutput LogFileAction(AudioFileOutput entity, string     action, int dummy = 0 ) { LogActionBase(FileActionMessage(entity, action, dummy  )); return entity; }
        public AudioFileOutput LogFileAction(AudioFileOutput entity, string     action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public Sample          LogFileAction(Sample          entity                                   ) { LogActionBase(FileActionMessage(entity                 )); return entity; }
        public Sample          LogFileAction(Sample          entity, ActionEnum action                ) { LogActionBase(FileActionMessage(entity, action         )); return entity; }
        public Sample          LogFileAction(Sample          entity, ActionEnum action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        public Sample          LogFileAction(Sample          entity,                    string message) { LogActionBase(FileActionMessage(entity,         message)); return entity; }
        public Sample          LogFileAction(Sample          entity, string     action, int dummy = 0 ) { LogActionBase(FileActionMessage(entity, action, dummy  )); return entity; }
        public Sample          LogFileAction(Sample          entity, string     action, string message) { LogActionBase(FileActionMessage(entity, action, message)); return entity; }
        
        private void LogActionBase(string actionMessage) => Log("Actions", actionMessage);

    }
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // ActionMessage (On Entities)
        
        public   string ActionMessage(FlowNode        entity, ActionEnum action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(FlowNode        entity, ActionEnum action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(FlowNode        entity, string     action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(FlowNode        entity, string     message,             int dummy = 0 ) => Logging.ActionMessage(entity,   message,        dummy);
        public   string ActionMessage(FlowNode        entity, string     action,              string message) => Logging.ActionMessage(entity,   action,       message);
        internal string ActionMessage(ConfigResolver  entity, ActionEnum action                             ) => Logging.ActionMessage(entity,   action               );
        internal string ActionMessage(ConfigResolver  entity, ActionEnum action,              string message) => Logging.ActionMessage(entity,   action,       message);
        internal string ActionMessage(ConfigResolver  entity, ActionEnum action, string name, int dummy = 0 ) => Logging.ActionMessage(entity,   action, name         );
        internal string ActionMessage(ConfigResolver  entity, ActionEnum action, string name, string message) => Logging.ActionMessage(entity,   action, name, message);
        internal string ActionMessage(ConfigResolver  entity                                                ) => Logging.ActionMessage(entity                         );
        internal string ActionMessage(ConfigResolver  entity,                                 string message) => Logging.ActionMessage(entity,                 message);
        internal string ActionMessage(ConfigResolver  entity, string     action,              int dummy = 0 ) => Logging.ActionMessage(entity,   action,       dummy  );
        internal string ActionMessage(ConfigResolver  entity, string     action,              string message) => Logging.ActionMessage(entity,   action,       message);
        internal string ActionMessage(ConfigResolver  entity, string     action, string name, int dummy = 0 ) => Logging.ActionMessage(entity,   action, name, dummy  );
        internal string ActionMessage(ConfigResolver  entity, string     action, string name, string message) => Logging.ActionMessage(entity,   action, name, message);
        internal string ActionMessage(ConfigSection   entity, ActionEnum action                             ) => Logging.ActionMessage(entity,   action               );
        internal string ActionMessage(ConfigSection   entity, ActionEnum action,              string message) => Logging.ActionMessage(entity,   action,       message);
        internal string ActionMessage(ConfigSection   entity, ActionEnum action, string name, int dummy = 0 ) => Logging.ActionMessage(entity,   action, name         );
        internal string ActionMessage(ConfigSection   entity, ActionEnum action, string name, string message) => Logging.ActionMessage(entity,   action, name, message);
        internal string ActionMessage(ConfigSection   entity                                                ) => Logging.ActionMessage(entity                         );
        internal string ActionMessage(ConfigSection   entity,                                 string message) => Logging.ActionMessage(entity,                 message);
        internal string ActionMessage(ConfigSection   entity, string     action,              int dummy = 0 ) => Logging.ActionMessage(entity,   action,       dummy  );
        internal string ActionMessage(ConfigSection   entity, string     action,              string message) => Logging.ActionMessage(entity,   action,       message);
        internal string ActionMessage(ConfigSection   entity, string     action, string name, int dummy = 0 ) => Logging.ActionMessage(entity,   action, name, dummy  );
        internal string ActionMessage(ConfigSection   entity, string     action, string name, string message) => Logging.ActionMessage(entity,   action, name, message);
        public   string ActionMessage(Tape            entity, ActionEnum action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(Tape            entity, ActionEnum action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(Tape            entity, string     action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(Tape            entity, string     message,             int dummy = 0 ) => Logging.ActionMessage(entity,   message,        dummy);
        public   string ActionMessage(Tape            entity, string     action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(TapeActions     entity, ActionEnum action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(TapeActions     entity, ActionEnum action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(TapeActions     entity, string     action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(TapeActions     entity, string     message,             int dummy = 0 ) => Logging.ActionMessage(entity,   message,        dummy);
        public   string ActionMessage(TapeActions     entity, string     action,              string message) => Logging.ActionMessage(entity,   action,       message);
        /// <inheritdoc cref="_logtapeaction" />
        public   string Message      (TapeAction      action                ) => Logging.ActionMessage(action         );
        /// <inheritdoc cref="_logtapeaction" />
        public   string Message      (TapeAction      action, string message) => Logging.ActionMessage(action, message);
        /// <inheritdoc cref="_logtapeaction" />
        public   string ActionMessage(TapeAction      action                ) => Logging.ActionMessage(action         );
        /// <inheritdoc cref="_logtapeaction" />
        public   string ActionMessage(TapeAction      action, string message) => Logging.ActionMessage(action, message);
        public   string ActionMessage(Buff            entity, ActionEnum action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(Buff            entity, ActionEnum action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(Buff            entity, string     action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(Buff            entity, string     message,             int dummy = 0 ) => Logging.ActionMessage(entity,   message,        dummy);
        public   string ActionMessage(Buff            entity, string     action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(AudioFileOutput entity, ActionEnum action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(AudioFileOutput entity, ActionEnum action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(AudioFileOutput entity, string     action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(AudioFileOutput entity, string     message,             int dummy = 0 ) => Logging.ActionMessage(entity,   message,        dummy);
        public   string ActionMessage(AudioFileOutput entity, string     action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(Sample          entity, ActionEnum action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(Sample          entity, ActionEnum action,              string message) => Logging.ActionMessage(entity,   action,       message);
        public   string ActionMessage(Sample          entity, string     action                             ) => Logging.ActionMessage(entity,   action               );
        public   string ActionMessage(Sample          entity, string     message,             int dummy = 0 ) => Logging.ActionMessage(entity,   message,        dummy);
        public   string ActionMessage(Sample          entity, string     action,              string message) => Logging.ActionMessage(entity,   action,       message);

        // ActionMessage (On Simple Types)
        
        public   string ActionMessage(object entity,        ActionEnum action                             ) => Logging.ActionMessage(entity,     action               );
        public   string ActionMessage(object entity,        ActionEnum action,              string message) => Logging.ActionMessage(entity,     action,       message);
        public   string ActionMessage(object entity,        ActionEnum action, string name, int dummy = 0 ) => Logging.ActionMessage(entity,     action, name, dummy  );
        public   string ActionMessage(object entity,        ActionEnum action, string name, string message) => Logging.ActionMessage(entity,     action, name, message);
        public   string ActionMessage(object entity                                                       ) => Logging.ActionMessage(entity                           );
        public   string ActionMessage(object entity,                                        string message) => Logging.ActionMessage(entity,                   message);
        public   string ActionMessage(object entity,        string     action,              int dummy = 0 ) => Logging.ActionMessage(entity,     action               );
        public   string ActionMessage(object entity,        string     action,              string message) => Logging.ActionMessage(entity,     action,       message);
        public   string ActionMessage(object entity,        string     action, string name, int dummy = 0 ) => Logging.ActionMessage(entity,     action, name         );
        public   string ActionMessage(object entity,        string     action, string name, string message) => Logging.ActionMessage(entity,     action, name, message);

        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj, ActionEnum action                             ) => Logging.ActionMessage(obj,        action               );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj, ActionEnum action,              string message) => Logging.ActionMessage(obj,        action,       message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj, ActionEnum action, string name, int dummy = 0 ) => Logging.ActionMessage(obj,        action, name, dummy  );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj, ActionEnum action, string name, string message) => Logging.ActionMessage(obj,        action, name, message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj                                                ) => Logging.ActionMessage(obj                              );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj,                                 string message) => Logging.ActionMessage(obj,                      message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj, string     action,              int dummy = 0 ) => Logging.ActionMessage(obj,        action               );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj, string     action,              string message) => Logging.ActionMessage(obj,        action,       message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj, string     action, string name, int dummy = 0 ) => Logging.ActionMessage(obj,        action, name, dummy  );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   string ActionMessage<TEntity>(TEntity obj, string     action, string name, string message) => Logging.ActionMessage(obj,        action, name, message);
        public   string ActionMessage<TEntity>(             ActionEnum action                             ) => Logging.ActionMessage<TEntity>(   action               );
        public   string ActionMessage<TEntity>(             ActionEnum action,              string message) => Logging.ActionMessage<TEntity>(   action,       message);
        public   string ActionMessage<TEntity>(             ActionEnum action, string name, int dummy = 0 ) => Logging.ActionMessage<TEntity>(   action, name, dummy  );
        public   string ActionMessage<TEntity>(             ActionEnum action, string name, string message) => Logging.ActionMessage<TEntity>(   action, name, message);
        public   string ActionMessage<TEntity>(                                                           ) => Logging.ActionMessage<TEntity>(                        );
        public   string ActionMessage<TEntity>(                                             string message) => Logging.ActionMessage<TEntity>(                 message);
        public   string ActionMessage<TEntity>(             string     action,              int dummy = 0 ) => Logging.ActionMessage<TEntity>(   action               );
        public   string ActionMessage<TEntity>(             string     action,              string message) => Logging.ActionMessage<TEntity>(   action,       message);
        public   string ActionMessage<TEntity>(             string     action, string name, int dummy = 0 ) => Logging.ActionMessage<TEntity>(   action, name, dummy  );
        public   string ActionMessage<TEntity>(             string     action, string name, string message) => Logging.ActionMessage<TEntity>(   action, name, message);
        public   string ActionMessage(Type entityType,      ActionEnum action                             ) => Logging.ActionMessage(entityType, action               );
        public   string ActionMessage(Type entityType,      ActionEnum action,              string message) => Logging.ActionMessage(entityType, action,       message);
        public   string ActionMessage(Type entityType,      ActionEnum action, string name, int dummy = 0 ) => Logging.ActionMessage(entityType, action, name, dummy  );
        public   string ActionMessage(Type entityType,      ActionEnum action, string name, string message) => Logging.ActionMessage(entityType, action, name, message);
        public   string ActionMessage(Type entityType                                                     ) => Logging.ActionMessage(entityType                       );
        public   string ActionMessage(Type entityType,                                      string message) => Logging.ActionMessage(entityType,               message);
        public   string ActionMessage(Type entityType,      string     action,              int dummy = 0 ) => Logging.ActionMessage(entityType, action               );
        public   string ActionMessage(Type entityType,      string     action,              string message) => Logging.ActionMessage(entityType, action,       message);
        public   string ActionMessage(Type entityType,      string     action, string name, int dummy = 0 ) => Logging.ActionMessage(entityType, action, name, dummy  );
        public   string ActionMessage(Type entityType,      string     action, string name, string message) => Logging.ActionMessage(entityType, action, name, message);
        public   string ActionMessage(string typeName,      ActionEnum action                             ) => Logging.ActionMessage(typeName,   action               );
        public   string ActionMessage(string typeName,      ActionEnum action,              string message) => Logging.ActionMessage(typeName,   action,       message);
        public   string ActionMessage(string typeName,      ActionEnum action, string name, int dummy = 0 ) => Logging.ActionMessage(typeName,   action, name, dummy  );
        public   string ActionMessage(string typeName,      ActionEnum action, string name, string message) => Logging.ActionMessage(typeName,   action, name, message);
        public   string ActionMessage(string typeName                                                     ) => Logging.ActionMessage(typeName                         );
        public   string ActionMessage(string typeName,                                      string message) => Logging.ActionMessage(typeName,                 message);
        public   string ActionMessage(string typeName,      string     action,              int dummy = 0 ) => Logging.ActionMessage(typeName,   action               );
        public   string ActionMessage(string typeName,      string     action,              string message) => Logging.ActionMessage(typeName,   action,       message);
        public   string ActionMessage(string typeName,      string     action, string name, int dummy = 0 ) => Logging.ActionMessage(typeName,   action, name, dummy  );
        public   string ActionMessage(string typeName,      string     action, string name, string message) => Logging.ActionMessage(typeName,   action, name, message);

        // Memory Message (On Simple Types)

        // (Always tagged [MEMORY] here, so no need for target types: object. entity Type or TEntity.)
                
        public string MemoryActionMessage(int byteCount                                                ) => Logging.MemoryActionMessage(byteCount                       );
        public string MemoryActionMessage(int byteCount,                                 string message) => Logging.MemoryActionMessage(byteCount,               message);
        public string MemoryActionMessage(int byteCount, ActionEnum action                             ) => Logging.MemoryActionMessage(byteCount, action               );   
        public string MemoryActionMessage(int byteCount, ActionEnum action,              string message) => Logging.MemoryActionMessage(byteCount, action,       message);
        public string MemoryActionMessage(int byteCount, ActionEnum action, string name, int dummy = 0 ) => Logging.MemoryActionMessage(byteCount, action, name, dummy  );
        public string MemoryActionMessage(int byteCount, ActionEnum action, string name, string message) => Logging.MemoryActionMessage(byteCount, action, name, message);
        public string MemoryActionMessage(int byteCount,                    string name, int dummy = 0 ) => Logging.MemoryActionMessage(byteCount,         name, dummy  );
        public string MemoryActionMessage(int byteCount,                    string name, string message) => Logging.MemoryActionMessage(byteCount,         name, message);
        public string MemoryActionMessage(int byteCount, string     action, string name, string message) => Logging.MemoryActionMessage(byteCount, action, name, message);
        public string MemoryActionMessage(byte[] bytes                                                 ) => Logging.MemoryActionMessage(bytes                           );
        public string MemoryActionMessage(byte[] bytes,                                  string message) => Logging.MemoryActionMessage(bytes,                   message);
        public string MemoryActionMessage(byte[] bytes,  ActionEnum action                             ) => Logging.MemoryActionMessage(bytes,     action               );   
        public string MemoryActionMessage(byte[] bytes,  ActionEnum action,              string message) => Logging.MemoryActionMessage(bytes,     action,       message);
        public string MemoryActionMessage(byte[] bytes,  ActionEnum action, string name, int dummy = 0 ) => Logging.MemoryActionMessage(bytes,     action, name, dummy  );
        public string MemoryActionMessage(byte[] bytes,  ActionEnum action, string name, string message) => Logging.MemoryActionMessage(bytes,     action, name, message);
        public string MemoryActionMessage(byte[] bytes,                     string name, int dummy = 0 ) => Logging.MemoryActionMessage(bytes,             name, dummy  );
        public string MemoryActionMessage(byte[] bytes,                     string name, string message) => Logging.MemoryActionMessage(bytes,             name, message);
        public string MemoryActionMessage(byte[] bytes,  string     action, string name, string message) => Logging.MemoryActionMessage(bytes,     action, name, message);
        public string ActionMessage      (byte[] bytes                                                 ) => Logging.      ActionMessage(bytes                           );
        public string ActionMessage      (byte[] bytes,                                  string message) => Logging.      ActionMessage(bytes,                   message);
        public string ActionMessage      (byte[] bytes,  ActionEnum action                             ) => Logging.      ActionMessage(bytes,     action               );   
        public string ActionMessage      (byte[] bytes,  ActionEnum action,              string message) => Logging.      ActionMessage(bytes,     action,       message);
        public string ActionMessage      (byte[] bytes,  ActionEnum action, string name, int dummy = 0 ) => Logging.      ActionMessage(bytes,     action, name, dummy  );
        public string ActionMessage      (byte[] bytes,  ActionEnum action, string name, string message) => Logging.      ActionMessage(bytes,     action, name, message);
        public string ActionMessage      (byte[] bytes,                     string name, int dummy = 0 ) => Logging.      ActionMessage(bytes,             name, dummy  );
        public string ActionMessage      (byte[] bytes,                     string name, string message) => Logging.      ActionMessage(bytes,             name, message);
        public string ActionMessage      (byte[] bytes,  string     action, string name, string message) => Logging.      ActionMessage(bytes,     action, name, message);
        
        // Memory Action Message (On Entities)
        
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes                                                ) => Logging.MemoryActionMessage(bytes                       );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                                 string message) => Logging.MemoryActionMessage(bytes,               message);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action                             ) => Logging.MemoryActionMessage(bytes, action               );   
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action,              string message) => Logging.MemoryActionMessage(bytes, action,       message);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => Logging.MemoryActionMessage(bytes, action, name, dummy  );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, ActionEnum action, string name, string message) => Logging.MemoryActionMessage(bytes, action, name, message);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                    string name, int dummy = 0 ) => Logging.MemoryActionMessage(bytes,         name, dummy  );
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes,                    string name, string message) => Logging.MemoryActionMessage(bytes,         name, message);
        public   string MemoryActionMessage(FlowNode        entity, byte[] bytes, string     action, string name, string message) => Logging.MemoryActionMessage(bytes, action, name, message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes                                                ) => Logging.MemoryActionMessage(bytes                       );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                                 string message) => Logging.MemoryActionMessage(bytes,               message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action                             ) => Logging.MemoryActionMessage(bytes, action               );   
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message) => Logging.MemoryActionMessage(bytes, action,       message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => Logging.MemoryActionMessage(bytes, action, name, dummy  );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message) => Logging.MemoryActionMessage(bytes, action, name, message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                    string name, int dummy = 0 ) => Logging.MemoryActionMessage(bytes,         name, dummy  );
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes,                    string name, string message) => Logging.MemoryActionMessage(bytes,         name, message);
        internal string MemoryActionMessage(ConfigResolver  entity, byte[] bytes, string     action, string name, string message) => Logging.MemoryActionMessage(bytes, action, name, message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes                                                ) => Logging.MemoryActionMessage(bytes                       );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                                 string message) => Logging.MemoryActionMessage(bytes,               message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action                             ) => Logging.MemoryActionMessage(bytes, action               );   
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action,              string message) => Logging.MemoryActionMessage(bytes, action,       message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => Logging.MemoryActionMessage(bytes, action, name, dummy  );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, string message) => Logging.MemoryActionMessage(bytes, action, name, message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                    string name, int dummy = 0 ) => Logging.MemoryActionMessage(bytes,         name, dummy  );
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes,                    string name, string message) => Logging.MemoryActionMessage(bytes,         name, message);
        internal string MemoryActionMessage(ConfigSection   entity, byte[] bytes, string     action, string name, string message) => Logging.MemoryActionMessage(bytes, action, name, message);
        
        public   string MemoryActionMessage(Tape            entity                                   ) => Logging.MemoryActionMessage(entity                 );
        public   string MemoryActionMessage(Tape            entity,                    string message) => Logging.MemoryActionMessage(entity,         message);
        public   string MemoryActionMessage(Tape            entity, ActionEnum action                ) => Logging.MemoryActionMessage(entity, action         );
        public   string MemoryActionMessage(Tape            entity, ActionEnum action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(Tape            entity, string     action, int dummy = 0 ) => Logging.MemoryActionMessage(entity, action, dummy  );
        public   string MemoryActionMessage(Tape            entity, string     action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(TapeConfig      entity                                   ) => Logging.MemoryActionMessage(entity                 );
        public   string MemoryActionMessage(TapeConfig      entity,                    string message) => Logging.MemoryActionMessage(entity,         message);
        public   string MemoryActionMessage(TapeConfig      entity, ActionEnum action                ) => Logging.MemoryActionMessage(entity, action         );
        public   string MemoryActionMessage(TapeConfig      entity, ActionEnum action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(TapeConfig      entity, string     action, int dummy = 0 ) => Logging.MemoryActionMessage(entity, action, dummy  );
        public   string MemoryActionMessage(TapeConfig      entity, string     action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(TapeActions     entity                                   ) => Logging.MemoryActionMessage(entity                 );
        public   string MemoryActionMessage(TapeActions     entity,                    string message) => Logging.MemoryActionMessage(entity,         message);
        public   string MemoryActionMessage(TapeActions     entity, ActionEnum action                ) => Logging.MemoryActionMessage(entity, action         );
        public   string MemoryActionMessage(TapeActions     entity, ActionEnum action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(TapeActions     entity, string     action, int dummy = 0 ) => Logging.MemoryActionMessage(entity, action, dummy  );
        public   string MemoryActionMessage(TapeActions     entity, string     action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(TapeAction      entity                                   ) => Logging.MemoryActionMessage(entity                 );
        public   string MemoryActionMessage(TapeAction      entity,                    string message) => Logging.MemoryActionMessage(entity,         message);
        public   string MemoryActionMessage(TapeAction      entity, ActionEnum action                ) => Logging.MemoryActionMessage(entity, action         );
        public   string MemoryActionMessage(TapeAction      entity, ActionEnum action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(TapeAction      entity, string     action, int dummy = 0 ) => Logging.MemoryActionMessage(entity, action, dummy  );
        public   string MemoryActionMessage(TapeAction      entity, string     action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(Buff            entity                                   ) => Logging.MemoryActionMessage(entity                 );
        public   string MemoryActionMessage(Buff            entity,                    string message) => Logging.MemoryActionMessage(entity,         message);
        public   string MemoryActionMessage(Buff            entity, ActionEnum action                ) => Logging.MemoryActionMessage(entity, action         );
        public   string MemoryActionMessage(Buff            entity, ActionEnum action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(Buff            entity, string     action, int dummy = 0 ) => Logging.MemoryActionMessage(entity, action, dummy  );
        public   string MemoryActionMessage(Buff            entity, string     action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes                                   ) => Logging.MemoryActionMessage(entity, bytes                 );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes,                    string message) => Logging.MemoryActionMessage(entity, bytes,         message);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action                ) => Logging.MemoryActionMessage(entity, bytes, action         );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, ActionEnum action, string message) => Logging.MemoryActionMessage(entity, bytes, action, message);
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, string     action, int dummy = 0 ) => Logging.MemoryActionMessage(entity, bytes, action, dummy  );
        public   string MemoryActionMessage(AudioFileOutput entity, byte[] bytes, string     action, string message) => Logging.MemoryActionMessage(entity, bytes, action, message);
        public   string MemoryActionMessage(Sample          entity                                   ) => Logging.MemoryActionMessage(entity                 );
        public   string MemoryActionMessage(Sample          entity,                    string message) => Logging.MemoryActionMessage(entity,         message);
        public   string MemoryActionMessage(Sample          entity, ActionEnum action                ) => Logging.MemoryActionMessage(entity, action         );
        public   string MemoryActionMessage(Sample          entity, ActionEnum action, string message) => Logging.MemoryActionMessage(entity, action, message);
        public   string MemoryActionMessage(Sample          entity, string     action, int dummy = 0 ) => Logging.MemoryActionMessage(entity, action, dummy  );
        public   string MemoryActionMessage(Sample          entity, string     action, string message) => Logging.MemoryActionMessage(entity, action, message);
        
        // File Message (On Simple Types)

        public string FileActionMessage(string filePath                                                          ) => Logging.FileActionMessage(filePath                                 );
        public string FileActionMessage(string filePath,                                    string sourceFilePath) => Logging.FileActionMessage(filePath,                  sourceFilePath);
        public string FileActionMessage(string filePath, ActionEnum action                                       ) => Logging.FileActionMessage(filePath, action                         );
        public string FileActionMessage(string filePath, ActionEnum action,                 string sourceFilePath) => Logging.FileActionMessage(filePath, action,          sourceFilePath);
        public string FileActionMessage(string filePath, ActionEnum action, string message, string sourceFilePath) => Logging.FileActionMessage(filePath, action, message, sourceFilePath);
        public string FileActionMessage(string filePath, string     action,                 int dummy = 0        ) => Logging.FileActionMessage(filePath, action,          dummy         );
        public string FileActionMessage(string filePath, string     action,                 string sourceFilePath) => Logging.FileActionMessage(filePath, action,          sourceFilePath);
        public string FileActionMessage(string filePath, string     action, string message, string sourceFilePath) => Logging.FileActionMessage(filePath, action, message, sourceFilePath);       
                
        // File Action Messages (On Entities)

        public   string FileActionMessage(FlowNode        entity, string filePath                                                          ) => Logging.FileActionMessage(entity, filePath                                 );
        public   string FileActionMessage(FlowNode        entity, string filePath,                                    string sourceFilePath) => Logging.FileActionMessage(entity, filePath,                  sourceFilePath);
        public   string FileActionMessage(FlowNode        entity, string filePath, ActionEnum action                                       ) => Logging.FileActionMessage(entity, filePath, action                         );
        public   string FileActionMessage(FlowNode        entity, string filePath, ActionEnum action,                 string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action,          sourceFilePath);
        public   string FileActionMessage(FlowNode        entity, string filePath, ActionEnum action, string message, string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action, message, sourceFilePath);
        public   string FileActionMessage(FlowNode        entity, string filePath, string     action,                 int dummy = 0        ) => Logging.FileActionMessage(entity, filePath, action,          dummy         );
        public   string FileActionMessage(FlowNode        entity, string filePath, string     action,                 string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action,          sourceFilePath);
        public   string FileActionMessage(FlowNode        entity, string filePath, string     action, string message, string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        internal string FileActionMessage(ConfigResolver  entity, string filePath                                                          ) => Logging.FileActionMessage(entity, filePath                                 );
        internal string FileActionMessage(ConfigResolver  entity, string filePath,                                    string sourceFilePath) => Logging.FileActionMessage(entity, filePath,                  sourceFilePath);
        internal string FileActionMessage(ConfigResolver  entity, string filePath, ActionEnum action                                       ) => Logging.FileActionMessage(entity, filePath, action                         );
        internal string FileActionMessage(ConfigResolver  entity, string filePath, ActionEnum action,                 string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal string FileActionMessage(ConfigResolver  entity, string filePath, ActionEnum action, string message, string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action, message, sourceFilePath);
        internal string FileActionMessage(ConfigResolver  entity, string filePath, string     action,                 int dummy = 0        ) => Logging.FileActionMessage(entity, filePath, action,          dummy         );
        internal string FileActionMessage(ConfigResolver  entity, string filePath, string     action,                 string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal string FileActionMessage(ConfigResolver  entity, string filePath, string     action, string message, string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        internal string FileActionMessage(ConfigSection   entity, string filePath                                                          ) => Logging.FileActionMessage(entity, filePath                                 );
        internal string FileActionMessage(ConfigSection   entity, string filePath,                                    string sourceFilePath) => Logging.FileActionMessage(entity, filePath,                  sourceFilePath);
        internal string FileActionMessage(ConfigSection   entity, string filePath, ActionEnum action                                       ) => Logging.FileActionMessage(entity, filePath, action                         );
        internal string FileActionMessage(ConfigSection   entity, string filePath, ActionEnum action,                 string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal string FileActionMessage(ConfigSection   entity, string filePath, ActionEnum action, string message, string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action, message, sourceFilePath);
        internal string FileActionMessage(ConfigSection   entity, string filePath, string     action,                 int dummy = 0        ) => Logging.FileActionMessage(entity, filePath, action,          dummy         );
        internal string FileActionMessage(ConfigSection   entity, string filePath, string     action,                 string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal string FileActionMessage(ConfigSection   entity, string filePath, string     action, string message, string sourceFilePath) => Logging.FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        public   string FileActionMessage(Tape            entity                                   ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(Tape            entity, ActionEnum action                ) => Logging.FileActionMessage(entity, action         );
        public   string FileActionMessage(Tape            entity, ActionEnum action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(Tape            entity,                    string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(Tape            entity, string     action, int dummy = 0 ) => Logging.FileActionMessage(entity, action, dummy  );
        public   string FileActionMessage(Tape            entity, string     action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(TapeConfig      entity                                   ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(TapeConfig      entity, ActionEnum action                ) => Logging.FileActionMessage(entity, action         );
        public   string FileActionMessage(TapeConfig      entity, ActionEnum action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(TapeConfig      entity,                    string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(TapeConfig      entity, string     action, int dummy = 0 ) => Logging.FileActionMessage(entity, action, dummy  );
        public   string FileActionMessage(TapeConfig      entity, string     action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(TapeActions     entity                                   ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(TapeActions     entity, ActionEnum action                ) => Logging.FileActionMessage(entity, action         );
        public   string FileActionMessage(TapeActions     entity, ActionEnum action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(TapeActions     entity,                    string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(TapeActions     entity, string     action, int dummy = 0 ) => Logging.FileActionMessage(entity, action, dummy  );
        public   string FileActionMessage(TapeActions     entity, string     action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(TapeAction      entity                                   ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(TapeAction      entity, ActionEnum action                ) => Logging.FileActionMessage(entity, action         );
        public   string FileActionMessage(TapeAction      entity, ActionEnum action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(TapeAction      entity,                    string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(TapeAction      entity, string     action, int dummy = 0 ) => Logging.FileActionMessage(entity, action, dummy  );
        public   string FileActionMessage(TapeAction      entity, string     action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(Buff            entity                                   ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(Buff            entity, ActionEnum action                ) => Logging.FileActionMessage(entity, action         );
        public   string FileActionMessage(Buff            entity, ActionEnum action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(Buff            entity,                    string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(Buff            entity, string     action, int dummy = 0 ) => Logging.FileActionMessage(entity, action, dummy  );
        public   string FileActionMessage(Buff            entity, string     action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(AudioFileOutput entity                                   ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(AudioFileOutput entity, ActionEnum action                ) => Logging.FileActionMessage(entity, action         );
        public   string FileActionMessage(AudioFileOutput entity, ActionEnum action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(AudioFileOutput entity,                    string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(AudioFileOutput entity, string     action, int dummy = 0 ) => Logging.FileActionMessage(entity, action, dummy  );
        public   string FileActionMessage(AudioFileOutput entity, string     action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(Sample          entity                                   ) => Logging.FileActionMessage(entity                 );
        public   string FileActionMessage(Sample          entity, ActionEnum action                ) => Logging.FileActionMessage(entity, action         );
        public   string FileActionMessage(Sample          entity, ActionEnum action, string message) => Logging.FileActionMessage(entity, action, message);
        public   string FileActionMessage(Sample          entity,                    string message) => Logging.FileActionMessage(entity,         message);
        public   string FileActionMessage(Sample          entity, string     action, int dummy = 0 ) => Logging.FileActionMessage(entity, action, dummy  );
        public   string FileActionMessage(Sample          entity, string     action, string message) => Logging.FileActionMessage(entity, action, message);
        
        // LogAction
        
        public   FlowNode        LogAction(FlowNode        entity, ActionEnum action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, ActionEnum action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, string     action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, string     message, int dummy = 0 ) { Logging.LogAction(entity, message, dummy ); return entity; }
        public   FlowNode        LogAction(FlowNode        entity, string     action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        
        internal ConfigResolver  LogAction(ConfigResolver  entity, ActionEnum action                             ) { Logging.LogAction(entity, action               ); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, ActionEnum action,              string message) { Logging.LogAction(entity, action,       message); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, ActionEnum action, string name, int dummy = 0 ) { Logging.LogAction(entity, action, name, dummy  ); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, ActionEnum action, string name, string message) { Logging.LogAction(entity, action, name, message); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity                                                ) { Logging.LogAction(entity                       ); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity,                                 string message) { Logging.LogAction(entity,               message); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, string     action,              int dummy = 0 ) { Logging.LogAction(entity, action,       dummy  ); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, string     action,              string message) { Logging.LogAction(entity, action,       message); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, string     action, string name, int dummy = 0 ) { Logging.LogAction(entity, action, name, dummy  ); return entity; }
        internal ConfigResolver  LogAction(ConfigResolver  entity, string     action, string name, string message) { Logging.LogAction(entity, action, name, message); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, ActionEnum action                             ) { Logging.LogAction(entity, action               ); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, ActionEnum action,              string message) { Logging.LogAction(entity, action,       message); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, ActionEnum action, string name, int dummy = 0 ) { Logging.LogAction(entity, action, name, dummy  ); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, ActionEnum action, string name, string message) { Logging.LogAction(entity, action, name, message); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity                                                ) { Logging.LogAction(entity                       ); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity,                                 string message) { Logging.LogAction(entity,               message); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string     action,              int dummy = 0 ) { Logging.LogAction(entity, action,       dummy  ); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string     action,              string message) { Logging.LogAction(entity, action,       message); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string     action, string name, int dummy = 0 ) { Logging.LogAction(entity, action, name, dummy  ); return entity; }
        internal ConfigSection   LogAction(ConfigSection   entity, string     action, string name, string message) { Logging.LogAction(entity, action, name, message); return entity; }
        
        public   Tape            LogAction(Tape            entity, ActionEnum action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   Tape            LogAction(Tape            entity, ActionEnum action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   Tape            LogAction(Tape            entity, string     action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   Tape            LogAction(Tape            entity, string     message, int dummy = 0 ) { Logging.LogAction(entity, message, dummy ); return entity; }
        public   Tape            LogAction(Tape            entity, string     action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, ActionEnum action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, ActionEnum action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, string     action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, string     message, int dummy = 0 ) { Logging.LogAction(entity, message, dummy ); return entity; }
        public   TapeConfig      LogAction(TapeConfig      entity, string     action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, ActionEnum action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, ActionEnum action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, string     action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, string     message, int dummy = 0 ) { Logging.LogAction(entity, message, dummy ); return entity; }
        public   TapeActions     LogAction(TapeActions     entity, string     action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      Log      (TapeAction      action                ) { Logging.Log      (action         ); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      Log      (TapeAction      action, string message) { Logging.Log      (action, message); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      LogAction(TapeAction      action                ) { Logging.LogAction(action         ); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   TapeAction      LogAction(TapeAction      action, string message) { Logging.LogAction(action, message); return action; }        
        
        public   Buff            LogAction(Buff            entity, ActionEnum action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   Buff            LogAction(Buff            entity, ActionEnum action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   Buff            LogAction(Buff            entity, string     action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   Buff            LogAction(Buff            entity, string     message, int dummy = 0 ) { Logging.LogAction(entity, message, dummy ); return entity; }
        public   Buff            LogAction(Buff            entity, string     action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, ActionEnum action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, ActionEnum action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, string     action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, string     message, int dummy = 0 ) { Logging.LogAction(entity, message, dummy ); return entity; }
        public   AudioFileOutput LogAction(AudioFileOutput entity, string     action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   Sample          LogAction(Sample          entity, ActionEnum action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   Sample          LogAction(Sample          entity, ActionEnum action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        public   Sample          LogAction(Sample          entity, string     action                 ) { Logging.LogAction(entity, action         ); return entity; }
        public   Sample          LogAction(Sample          entity, string     message, int dummy = 0 ) { Logging.LogAction(entity, message, dummy ); return entity; }
        public   Sample          LogAction(Sample          entity, string     action,  string message) { Logging.LogAction(entity, action, message); return entity; }
        
        // LogAction (On Simple Types)
        
        public void LogAction(object entity,   ActionEnum action                             ) => Logging.LogAction(entity,     action               );
        public void LogAction(object entity,   ActionEnum action,              string message) => Logging.LogAction(entity,     action,       message);
        public void LogAction(object entity,   ActionEnum action, string name, int dummy = 0 ) => Logging.LogAction(entity,     action, name, dummy  );
        public void LogAction(object entity,   ActionEnum action, string name, string message) => Logging.LogAction(entity,     action, name, message);
        public void LogAction(object entity                                                  ) => Logging.LogAction(entity                           );
        public void LogAction(object entity,                                   string message) => Logging.LogAction(entity,                   message);
        public void LogAction(object entity,   string     action,              int dummy = 0 ) => Logging.LogAction(entity,     action,       dummy  );
        public void LogAction(object entity,   string     action,              string message) => Logging.LogAction(entity,     action,       message);
        public void LogAction(object entity,   string     action, string name, int dummy = 0 ) => Logging.LogAction(entity,     action, name, dummy  );
        public void LogAction(object entity,   string     action, string name, string message) => Logging.LogAction(entity,     action, name, message);
        public void LogAction<TEntity>(        ActionEnum action                             ) => Logging.LogAction<TEntity>(   action               );
        public void LogAction<TEntity>(        ActionEnum action,              string message) => Logging.LogAction<TEntity>(   action,       message);
        public void LogAction<TEntity>(        ActionEnum action, string name, int dummy = 0 ) => Logging.LogAction<TEntity>(   action, name, dummy  );
        public void LogAction<TEntity>(        ActionEnum action, string name, string message) => Logging.LogAction<TEntity>(   action, name, message);
        public void LogAction<TEntity>(                                                      ) => Logging.LogAction<TEntity>(                        );
        public void LogAction<TEntity>(                                        string message) => Logging.LogAction<TEntity>(                 message);
        public void LogAction<TEntity>(        string     action,              int dummy = 0 ) => Logging.LogAction<TEntity>(   action,       dummy  );
        public void LogAction<TEntity>(        string     action,              string message) => Logging.LogAction<TEntity>(   action,       message);
        public void LogAction<TEntity>(        string     action, string name, int dummy = 0 ) => Logging.LogAction<TEntity>(   action, name, dummy  );
        public void LogAction<TEntity>(        string     action, string name, string message) => Logging.LogAction<TEntity>(   action, name, message);
        public void LogAction(Type entityType, ActionEnum action                             ) => Logging.LogAction(entityType, action               );
        public void LogAction(Type entityType, ActionEnum action,              string message) => Logging.LogAction(entityType, action,       message);
        public void LogAction(Type entityType, ActionEnum action, string name, int dummy = 0 ) => Logging.LogAction(entityType, action, name, dummy  );
        public void LogAction(Type entityType, ActionEnum action, string name, string message) => Logging.LogAction(entityType, action, name, message);
        public void LogAction(Type entityType                                                ) => Logging.LogAction(entityType                       );
        public void LogAction(Type entityType,                                 string message) => Logging.LogAction(entityType,               message);
        public void LogAction(Type entityType, string     action,              int dummy = 0 ) => Logging.LogAction(entityType, action,       dummy  );
        public void LogAction(Type entityType, string     action,              string message) => Logging.LogAction(entityType, action,       message);
        public void LogAction(Type entityType, string     action, string name, int dummy = 0 ) => Logging.LogAction(entityType, action, name, dummy  );
        public void LogAction(Type entityType, string     action, string name, string message) => Logging.LogAction(entityType, action, name, message);
        public void LogAction(string typeName, ActionEnum action                             ) => Logging.LogAction(typeName,   action               );
        public void LogAction(string typeName, ActionEnum action,              string message) => Logging.LogAction(typeName,   action,       message);
        public void LogAction(string typeName, ActionEnum action, string name, int dummy = 0 ) => Logging.LogAction(typeName,   action, name, dummy  );
        public void LogAction(string typeName, ActionEnum action, string name, string message) => Logging.LogAction(typeName,   action, name, message);
        public void LogAction(string typeName                                                ) => Logging.LogAction(typeName                         );
        public void LogAction(string typeName,                                 string message) => Logging.LogAction(typeName,                 message);
        public void LogAction(string typeName, string     action,              int dummy = 0 ) => Logging.LogAction(typeName,   action,       dummy  );
        public void LogAction(string typeName, string     action,              string message) => Logging.LogAction(typeName,   action,       message);
        public void LogAction(string typeName, string     action, string name, int dummy = 0 ) => Logging.LogAction(typeName,   action, name, dummy  );
        public void LogAction(string typeName, string     action, string name, string message) => Logging.LogAction(typeName,   action, name, message);

        // Memory Action Logging (On Simple Types)

        public void   LogMemoryAction(int byteCount                                                ) => Logging.LogMemoryAction(byteCount                       );
        public void   LogMemoryAction(int byteCount,                                 string message) => Logging.LogMemoryAction(byteCount,               message);
        public void   LogMemoryAction(int byteCount, ActionEnum action                             ) => Logging.LogMemoryAction(byteCount, action               );
        public void   LogMemoryAction(int byteCount, ActionEnum action,              string message) => Logging.LogMemoryAction(byteCount, action,       message);
        public void   LogMemoryAction(int byteCount, ActionEnum action, string name, int dummy = 0 ) => Logging.LogMemoryAction(byteCount, action, name, dummy  );
        public void   LogMemoryAction(int byteCount, ActionEnum action, string name, string message) => Logging.LogMemoryAction(byteCount, action, name, message);
        public void   LogMemoryAction(int byteCount,                    string name, int dummy = 0 ) => Logging.LogMemoryAction(byteCount,         name, dummy  );
        public void   LogMemoryAction(int byteCount,                    string name, string message) => Logging.LogMemoryAction(byteCount,         name, message);
        public void   LogMemoryAction(int byteCount, string     action, string name, string message) => Logging.LogMemoryAction(byteCount, action, name, message);
        public byte[] LogMemoryAction(byte[] bytes                                                 ) => Logging.LogMemoryAction(bytes                           );
        public byte[] LogMemoryAction(byte[] bytes,                                  string message) => Logging.LogMemoryAction(bytes,                   message);
        public byte[] LogMemoryAction(byte[] bytes,  ActionEnum action                             ) => Logging.LogMemoryAction(bytes,     action               );
        public byte[] LogMemoryAction(byte[] bytes,  ActionEnum action,              string message) => Logging.LogMemoryAction(bytes,     action,       message);
        public byte[] LogMemoryAction(byte[] bytes,  ActionEnum action, string name, int dummy = 0 ) => Logging.LogMemoryAction(bytes,     action, name, dummy  );
        public byte[] LogMemoryAction(byte[] bytes,  ActionEnum action, string name, string message) => Logging.LogMemoryAction(bytes,     action, name, message);
        public byte[] LogMemoryAction(byte[] bytes,                     string name, int dummy = 0 ) => Logging.LogMemoryAction(bytes,             name, dummy  );
        public byte[] LogMemoryAction(byte[] bytes,                     string name, string message) => Logging.LogMemoryAction(bytes,             name, message);
        public byte[] LogMemoryAction(byte[] bytes,  string     action, string name, string message) => Logging.LogMemoryAction(bytes,     action, name, message);
        public byte[] LogAction      (byte[] bytes                                                 ) => Logging.LogAction      (bytes                           );
        public byte[] LogAction      (byte[] bytes,                                  string message) => Logging.LogAction      (bytes,                   message);
        public byte[] LogAction      (byte[] bytes,  ActionEnum action                             ) => Logging.LogAction      (bytes,     action               );
        public byte[] LogAction      (byte[] bytes,  ActionEnum action,              string message) => Logging.LogAction      (bytes,     action,       message);
        public byte[] LogAction      (byte[] bytes,  ActionEnum action, string name, int dummy = 0 ) => Logging.LogAction      (bytes,     action, name, dummy  );
        public byte[] LogAction      (byte[] bytes,  ActionEnum action, string name, string message) => Logging.LogAction      (bytes,     action, name, message);
        public byte[] LogAction      (byte[] bytes,                     string name, int dummy = 0 ) => Logging.LogAction      (bytes,             name, dummy  );
        public byte[] LogAction      (byte[] bytes,                     string name, string message) => Logging.LogAction      (bytes,             name, message);
        public byte[] LogAction      (byte[] bytes,  string     action, string name, string message) => Logging.LogAction      (bytes,     action, name, message);

        // Memory Action Logging (On Entities)
        
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes                                   ) => Logging.LogMemoryAction(entity, bytes                 );
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                    string message) => Logging.LogMemoryAction(entity, bytes,         message);
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action                ) => Logging.LogMemoryAction(entity, bytes, action         );
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action, string message) => Logging.LogMemoryAction(entity, bytes, action, message);
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, string     action, int dummy = 0 ) => Logging.LogMemoryAction(entity, bytes, action, dummy  );
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, string     action, string message) => Logging.LogMemoryAction(entity, bytes, action, message);

        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes                                                ) => Logging.LogMemoryAction(entity, bytes                       );
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                                 string message) => Logging.LogMemoryAction(entity, bytes,               message);
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, ActionEnum action                             ) => Logging.LogMemoryAction(entity, bytes, action               );
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message) => Logging.LogMemoryAction(entity, bytes, action,       message);
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => Logging.LogMemoryAction(entity, bytes, action, name, dummy  );
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message) => Logging.LogMemoryAction(entity, bytes, action, name, message);
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                    string name, int dummy = 0 ) => Logging.LogMemoryAction(entity, bytes,         name, dummy  );
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes,                    string name, string message) => Logging.LogMemoryAction(entity, bytes,         name, message);
        internal ConfigResolver  LogMemoryAction(ConfigResolver  entity, byte[] bytes, string     action, string name, string message) => Logging.LogMemoryAction(entity, bytes, action, name, message);
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes                                                ) => Logging.LogMemoryAction(entity, bytes                       );
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                                 string message) => Logging.LogMemoryAction(entity, bytes,               message);
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, ActionEnum action                             ) => Logging.LogMemoryAction(entity, bytes, action               );
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, ActionEnum action,              string message) => Logging.LogMemoryAction(entity, bytes, action,       message);
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => Logging.LogMemoryAction(entity, bytes, action, name, dummy  );
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, ActionEnum action, string name, string message) => Logging.LogMemoryAction(entity, bytes, action, name, message);
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                    string name, int dummy = 0 ) => Logging.LogMemoryAction(entity, bytes,         name, dummy  );
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes,                    string name, string message) => Logging.LogMemoryAction(entity, bytes,         name, message);
        internal ConfigSection   LogMemoryAction(ConfigSection   entity, byte[] bytes, string     action, string name, string message) => Logging.LogMemoryAction(entity, bytes, action, name, message);

        public   Tape            LogMemoryAction(Tape            entity                                   ) => Logging.LogMemoryAction(entity                 );
        public   Tape            LogMemoryAction(Tape            entity,                    string message) => Logging.LogMemoryAction(entity,         message);
        public   Tape            LogMemoryAction(Tape            entity, ActionEnum action                ) => Logging.LogMemoryAction(entity, action         );
        public   Tape            LogMemoryAction(Tape            entity, ActionEnum action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   Tape            LogMemoryAction(Tape            entity, string     action, int dummy = 0 ) => Logging.LogMemoryAction(entity, action, dummy  );
        public   Tape            LogMemoryAction(Tape            entity, string     action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   TapeConfig      LogMemoryAction(TapeConfig      entity                                   ) => Logging.LogMemoryAction(entity                 );
        public   TapeConfig      LogMemoryAction(TapeConfig      entity,                    string message) => Logging.LogMemoryAction(entity,         message);
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, ActionEnum action                ) => Logging.LogMemoryAction(entity, action         );
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, ActionEnum action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, string     action, int dummy = 0 ) => Logging.LogMemoryAction(entity, action, dummy  );
        public   TapeConfig      LogMemoryAction(TapeConfig      entity, string     action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   TapeActions     LogMemoryAction(TapeActions     entity                                   ) => Logging.LogMemoryAction(entity                 );
        public   TapeActions     LogMemoryAction(TapeActions     entity,                    string message) => Logging.LogMemoryAction(entity,         message);
        public   TapeActions     LogMemoryAction(TapeActions     entity, ActionEnum action                ) => Logging.LogMemoryAction(entity, action         );
        public   TapeActions     LogMemoryAction(TapeActions     entity, ActionEnum action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   TapeActions     LogMemoryAction(TapeActions     entity, string     action, int dummy = 0 ) => Logging.LogMemoryAction(entity, action, dummy  );
        public   TapeActions     LogMemoryAction(TapeActions     entity, string     action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   TapeAction      LogMemoryAction(TapeAction      entity                                   ) => Logging.LogMemoryAction(entity                 );
        public   TapeAction      LogMemoryAction(TapeAction      entity,                    string message) => Logging.LogMemoryAction(entity,         message);
        public   TapeAction      LogMemoryAction(TapeAction      entity, ActionEnum action                ) => Logging.LogMemoryAction(entity, action         );
        public   TapeAction      LogMemoryAction(TapeAction      entity, ActionEnum action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   TapeAction      LogMemoryAction(TapeAction      entity, string     action, int dummy = 0 ) => Logging.LogMemoryAction(entity, action, dummy  );
        public   TapeAction      LogMemoryAction(TapeAction      entity, string     action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   Buff            LogMemoryAction(Buff            entity                                   ) => Logging.LogMemoryAction(entity                 );
        public   Buff            LogMemoryAction(Buff            entity,                    string message) => Logging.LogMemoryAction(entity,         message);
        public   Buff            LogMemoryAction(Buff            entity, ActionEnum action                ) => Logging.LogMemoryAction(entity, action         );
        public   Buff            LogMemoryAction(Buff            entity, ActionEnum action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   Buff            LogMemoryAction(Buff            entity, string     action, int dummy = 0 ) => Logging.LogMemoryAction(entity, action, dummy  );
        public   Buff            LogMemoryAction(Buff            entity, string     action, string message) => Logging.LogMemoryAction(entity, action, message);
        
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes                                   ) => Logging.LogMemoryAction(entity, bytes                 );
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes,                    string message) => Logging.LogMemoryAction(entity, bytes,         message);
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, ActionEnum action                ) => Logging.LogMemoryAction(entity, bytes, action         );
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, ActionEnum action, string message) => Logging.LogMemoryAction(entity, bytes, action, message);
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, string     action, int dummy = 0 ) => Logging.LogMemoryAction(entity, bytes, action, dummy  );
        public   AudioFileOutput LogMemoryAction(AudioFileOutput entity, byte[] bytes, string     action, string message) => Logging.LogMemoryAction(entity, bytes, action, message);
        
        public   Sample          LogMemoryAction(Sample          entity                                   ) => Logging.LogMemoryAction(entity                 );
        public   Sample          LogMemoryAction(Sample          entity,                    string message) => Logging.LogMemoryAction(entity,         message);
        public   Sample          LogMemoryAction(Sample          entity, ActionEnum action                ) => Logging.LogMemoryAction(entity, action         );
        public   Sample          LogMemoryAction(Sample          entity, ActionEnum action, string message) => Logging.LogMemoryAction(entity, action, message);
        public   Sample          LogMemoryAction(Sample          entity, string     action, int dummy = 0 ) => Logging.LogMemoryAction(entity, action, dummy  );
        public   Sample          LogMemoryAction(Sample          entity, string     action, string message) => Logging.LogMemoryAction(entity, action, message);

        // LogFileAction
        
        public   string         LogFileAction(                       string filePath                                                          ) => Logging.LogFileAction(        filePath                                 );
        public   string         LogFileAction(                       string filePath,                                    string sourceFilePath) => Logging.LogFileAction(        filePath,                  sourceFilePath);
        public   string         LogFileAction(                       string filePath, ActionEnum action                                       ) => Logging.LogFileAction(        filePath, action                         );
        public   string         LogFileAction(                       string filePath, ActionEnum action,                 string sourceFilePath) => Logging.LogFileAction(        filePath, action,          sourceFilePath);
        public   string         LogFileAction(                       string filePath, ActionEnum action, string message, string sourceFilePath) => Logging.LogFileAction(        filePath, action, message, sourceFilePath);
        public   string         LogFileAction(                       string filePath, string     action,                 int dummy = 0        ) => Logging.LogFileAction(        filePath, action,          dummy         );
        public   string         LogFileAction(                       string filePath, string     action,                 string sourceFilePath) => Logging.LogFileAction(        filePath, action,          sourceFilePath);
        public   string         LogFileAction(                       string filePath, string     action, string message, string sourceFilePath) => Logging.LogFileAction(        filePath, action, message, sourceFilePath);
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath                                                          ) => Logging.LogFileAction(entity, filePath                                 );
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath,                                    string sourceFilePath) => Logging.LogFileAction(entity, filePath,                  sourceFilePath);
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, ActionEnum action                                       ) => Logging.LogFileAction(entity, filePath, action                         );
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, ActionEnum action,                 string sourceFilePath) => Logging.LogFileAction(entity, filePath, action,          sourceFilePath);
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, ActionEnum action, string message, string sourceFilePath) => Logging.LogFileAction(entity, filePath, action, message, sourceFilePath);
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, string     action,                 int dummy = 0        ) => Logging.LogFileAction(entity, filePath, action,          dummy         );
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, string     action,                 string sourceFilePath) => Logging.LogFileAction(entity, filePath, action,          sourceFilePath);
        public   FlowNode       LogFileAction(FlowNode       entity, string filePath, string     action, string message, string sourceFilePath) => Logging.LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath                                                          ) => Logging.LogFileAction(entity, filePath                                 );
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath,                                    string sourceFilePath) => Logging.LogFileAction(entity, filePath,                  sourceFilePath);
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, ActionEnum action                                       ) => Logging.LogFileAction(entity, filePath, action                         );
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, ActionEnum action,                 string sourceFilePath) => Logging.LogFileAction(entity, filePath, action,          sourceFilePath);
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, ActionEnum action, string message, string sourceFilePath) => Logging.LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, string     action,                 int dummy = 0        ) => Logging.LogFileAction(entity, filePath, action,          dummy         );
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, string     action,                 string sourceFilePath) => Logging.LogFileAction(entity, filePath, action,          sourceFilePath);
        internal ConfigResolver LogFileAction(ConfigResolver entity, string filePath, string     action, string message, string sourceFilePath) => Logging.LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath                                                          ) => Logging.LogFileAction(entity, filePath                                 );
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath,                                    string sourceFilePath) => Logging.LogFileAction(entity, filePath,                  sourceFilePath);
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, ActionEnum action                                       ) => Logging.LogFileAction(entity, filePath, action                         );
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, ActionEnum action,                 string sourceFilePath) => Logging.LogFileAction(entity, filePath, action,          sourceFilePath);
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, ActionEnum action, string message, string sourceFilePath) => Logging.LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, string     action,                 int dummy = 0        ) => Logging.LogFileAction(entity, filePath, action,          dummy         );
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, string     action,                 string sourceFilePath) => Logging.LogFileAction(entity, filePath, action,          sourceFilePath);
        internal ConfigSection  LogFileAction(ConfigSection  entity, string filePath, string     action, string message, string sourceFilePath) => Logging.LogFileAction(entity, filePath, action, message, sourceFilePath);
        
        public Tape            LogFileAction(Tape            entity                                   ) => Logging.LogFileAction(entity                 );
        public Tape            LogFileAction(Tape            entity, ActionEnum action                ) => Logging.LogFileAction(entity, action         );
        public Tape            LogFileAction(Tape            entity, ActionEnum action, string message) => Logging.LogFileAction(entity, action, message);
        public Tape            LogFileAction(Tape            entity,                    string message) => Logging.LogFileAction(entity,         message);
        public Tape            LogFileAction(Tape            entity, string     action, int dummy = 0 ) => Logging.LogFileAction(entity, action, dummy  );
        public Tape            LogFileAction(Tape            entity, string     action, string message) => Logging.LogFileAction(entity, action, message);
        public TapeConfig      LogFileAction(TapeConfig      entity                                   ) => Logging.LogFileAction(entity                 );
        public TapeConfig      LogFileAction(TapeConfig      entity, ActionEnum action                ) => Logging.LogFileAction(entity, action         );
        public TapeConfig      LogFileAction(TapeConfig      entity, ActionEnum action, string message) => Logging.LogFileAction(entity, action, message);
        public TapeConfig      LogFileAction(TapeConfig      entity,                    string message) => Logging.LogFileAction(entity,         message);
        public TapeConfig      LogFileAction(TapeConfig      entity, string     action, int dummy = 0 ) => Logging.LogFileAction(entity, action, dummy  );
        public TapeConfig      LogFileAction(TapeConfig      entity, string     action, string message) => Logging.LogFileAction(entity, action, message);
        public TapeActions     LogFileAction(TapeActions     entity                                   ) => Logging.LogFileAction(entity                 );
        public TapeActions     LogFileAction(TapeActions     entity, ActionEnum action                ) => Logging.LogFileAction(entity, action         );
        public TapeActions     LogFileAction(TapeActions     entity, ActionEnum action, string message) => Logging.LogFileAction(entity, action, message);
        public TapeActions     LogFileAction(TapeActions     entity,                    string message) => Logging.LogFileAction(entity,         message);
        public TapeActions     LogFileAction(TapeActions     entity, string     action, int dummy = 0 ) => Logging.LogFileAction(entity, action, dummy  );
        public TapeActions     LogFileAction(TapeActions     entity, string     action, string message) => Logging.LogFileAction(entity, action, message);
        public TapeAction      LogFileAction(TapeAction      entity                                   ) => Logging.LogFileAction(entity                 );
        public TapeAction      LogFileAction(TapeAction      entity, ActionEnum action                ) => Logging.LogFileAction(entity, action         );
        public TapeAction      LogFileAction(TapeAction      entity, ActionEnum action, string message) => Logging.LogFileAction(entity, action, message);
        public TapeAction      LogFileAction(TapeAction      entity,                    string message) => Logging.LogFileAction(entity,         message);
        public TapeAction      LogFileAction(TapeAction      entity, string     action, int dummy = 0 ) => Logging.LogFileAction(entity, action, dummy  );
        public TapeAction      LogFileAction(TapeAction      entity, string     action, string message) => Logging.LogFileAction(entity, action, message);
        public Buff            LogFileAction(Buff            entity                                   ) => Logging.LogFileAction(entity                 );
        public Buff            LogFileAction(Buff            entity, ActionEnum action                ) => Logging.LogFileAction(entity, action         );
        public Buff            LogFileAction(Buff            entity, ActionEnum action, string message) => Logging.LogFileAction(entity, action, message);
        public Buff            LogFileAction(Buff            entity,                    string message) => Logging.LogFileAction(entity,         message);
        public Buff            LogFileAction(Buff            entity, string     action, int dummy = 0 ) => Logging.LogFileAction(entity, action, dummy  );
        public Buff            LogFileAction(Buff            entity, string     action, string message) => Logging.LogFileAction(entity, action, message);
        public AudioFileOutput LogFileAction(AudioFileOutput entity                                   ) => Logging.LogFileAction(entity                 );
        public AudioFileOutput LogFileAction(AudioFileOutput entity, ActionEnum action                ) => Logging.LogFileAction(entity, action         );
        public AudioFileOutput LogFileAction(AudioFileOutput entity, ActionEnum action, string message) => Logging.LogFileAction(entity, action, message);
        public AudioFileOutput LogFileAction(AudioFileOutput entity,                    string message) => Logging.LogFileAction(entity,         message);
        public AudioFileOutput LogFileAction(AudioFileOutput entity, string     action, int dummy = 0 ) => Logging.LogFileAction(entity, action, dummy  );
        public AudioFileOutput LogFileAction(AudioFileOutput entity, string     action, string message) => Logging.LogFileAction(entity, action, message);
        public Sample          LogFileAction(Sample          entity                                   ) => Logging.LogFileAction(entity                 );
        public Sample          LogFileAction(Sample          entity, ActionEnum action                ) => Logging.LogFileAction(entity, action         );
        public Sample          LogFileAction(Sample          entity, ActionEnum action, string message) => Logging.LogFileAction(entity, action, message);
        public Sample          LogFileAction(Sample          entity,                    string message) => Logging.LogFileAction(entity,         message);
        public Sample          LogFileAction(Sample          entity, string     action, int dummy = 0 ) => Logging.LogFileAction(entity, action, dummy  );
        public Sample          LogFileAction(Sample          entity, string     action, string message) => Logging.LogFileAction(entity, action, message);
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{

    
    public static partial class LogExtensionWishes
    {
        // ActionMessage (On Entities)
        
        public   static string ActionMessage(this FlowNode        entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this FlowNode        entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this FlowNode        entity, string     action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this FlowNode        entity, string     message,             int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   message,        dummy);
        public   static string ActionMessage(this FlowNode        entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        internal static string ActionMessage(this ConfigResolver  entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        internal static string ActionMessage(this ConfigResolver  entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        internal static string ActionMessage(this ConfigResolver  entity, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action, name         );
        internal static string ActionMessage(this ConfigResolver  entity, ActionEnum action, string name, string message) => ResolveLogging(entity).ActionMessage(entity,   action, name, message);
        internal static string ActionMessage(this ConfigResolver  entity                                                ) => ResolveLogging(entity).ActionMessage(entity                         );
        internal static string ActionMessage(this ConfigResolver  entity,                                 string message) => ResolveLogging(entity).ActionMessage(entity,                 message);
        internal static string ActionMessage(this ConfigResolver  entity, string     action,              int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action,       dummy  );
        internal static string ActionMessage(this ConfigResolver  entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        internal static string ActionMessage(this ConfigResolver  entity, string     action, string name, int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action, name, dummy  );
        internal static string ActionMessage(this ConfigResolver  entity, string     action, string name, string message) => ResolveLogging(entity).ActionMessage(entity,   action, name, message);
        internal static string ActionMessage(this ConfigSection   entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        internal static string ActionMessage(this ConfigSection   entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        internal static string ActionMessage(this ConfigSection   entity, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action, name         );
        internal static string ActionMessage(this ConfigSection   entity, ActionEnum action, string name, string message) => ResolveLogging(entity).ActionMessage(entity,   action, name, message);
        internal static string ActionMessage(this ConfigSection   entity                                                ) => ResolveLogging(entity).ActionMessage(entity                         );
        internal static string ActionMessage(this ConfigSection   entity,                                 string message) => ResolveLogging(entity).ActionMessage(entity,                 message);
        internal static string ActionMessage(this ConfigSection   entity, string     action,              int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action,       dummy  );
        internal static string ActionMessage(this ConfigSection   entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        internal static string ActionMessage(this ConfigSection   entity, string     action, string name, int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action, name, dummy  );
        internal static string ActionMessage(this ConfigSection   entity, string     action, string name, string message) => ResolveLogging(entity).ActionMessage(entity,   action, name, message);
        public   static string ActionMessage(this Tape            entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this Tape            entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this Tape            entity, string     action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this Tape            entity, string     message,             int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   message,        dummy);
        public   static string ActionMessage(this Tape            entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this TapeActions     entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this TapeActions     entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this TapeActions     entity, string     action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this TapeActions     entity, string     message,             int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   message,        dummy);
        public   static string ActionMessage(this TapeActions     entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        /// <inheritdoc cref="_logtapeaction" />
        public   static string Message      (this TapeAction      action                ) => ResolveLogging(action).ActionMessage(action         );
        /// <inheritdoc cref="_logtapeaction" />
        public   static string Message      (this TapeAction      action, string message) => ResolveLogging(action).ActionMessage(action, message);
        /// <inheritdoc cref="_logtapeaction" />
        public   static string ActionMessage(this TapeAction      action                ) => ResolveLogging(action).ActionMessage(action         );
        /// <inheritdoc cref="_logtapeaction" />
        public   static string ActionMessage(this TapeAction      action, string message) => ResolveLogging(action).ActionMessage(action, message);
        public   static string ActionMessage(this Buff            entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this Buff            entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this Buff            entity, string     action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this Buff            entity, string     message,             int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   message,        dummy);
        public   static string ActionMessage(this Buff            entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this AudioFileOutput entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this AudioFileOutput entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this AudioFileOutput entity, string     action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this AudioFileOutput entity, string     message,             int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   message,        dummy);
        public   static string ActionMessage(this AudioFileOutput entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this Sample          entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this Sample          entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this Sample          entity, string     action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        public   static string ActionMessage(this Sample          entity, string     message,             int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   message,        dummy);
        public   static string ActionMessage(this Sample          entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);

        public   static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action                             ) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   action               );
        public   static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action,              string message) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, string     action                             ) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   action               );
        public   static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, string     message,             int dummy = 0 ) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   message,        dummy);
        public   static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, string     action,              string message) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this Sample          entity, SynthWishes synthWishes, ActionEnum action                             ) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   action               );
        public   static string ActionMessage(this Sample          entity, SynthWishes synthWishes, ActionEnum action,              string message) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   action,       message);
        public   static string ActionMessage(this Sample          entity, SynthWishes synthWishes, string     action                             ) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   action               );
        public   static string ActionMessage(this Sample          entity, SynthWishes synthWishes, string     message,             int dummy = 0 ) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   message,        dummy);
        public   static string ActionMessage(this Sample          entity, SynthWishes synthWishes, string     action,              string message) => ResolveLogging(entity, synthWishes).ActionMessage(entity,   action,       message);
        
        // ActionMessage (On Simple Types)
        
        public   static string ActionMessage(this object entity,        ActionEnum action                             ) => ResolveLogging(entity    ).ActionMessage(entity,     action               );
        public   static string ActionMessage(this object entity,        ActionEnum action,              string message) => ResolveLogging(entity    ).ActionMessage(entity,     action,       message);
        public   static string ActionMessage(this object entity,        ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity    ).ActionMessage(entity,     action, name, dummy  );
        public   static string ActionMessage(this object entity,        ActionEnum action, string name, string message) => ResolveLogging(entity    ).ActionMessage(entity,     action, name, message);
        public   static string ActionMessage(this object entity                                                       ) => ResolveLogging(entity    ).ActionMessage(entity                           );
        public   static string ActionMessage(this object entity,                                        string message) => ResolveLogging(entity    ).ActionMessage(entity,                   message);
        public   static string ActionMessage(this object entity,        string     action,              int dummy = 0 ) => ResolveLogging(entity    ).ActionMessage(entity,     action               );
        public   static string ActionMessage(this object entity,        string     action,              string message) => ResolveLogging(entity    ).ActionMessage(entity,     action,       message);
        public   static string ActionMessage(this object entity,        string     action, string name, int dummy = 0 ) => ResolveLogging(entity    ).ActionMessage(entity,     action, name         );
        public   static string ActionMessage(this object entity,        string     action, string name, string message) => ResolveLogging(entity    ).ActionMessage(entity,     action, name, message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, ActionEnum action                             ) => ResolveLogging(obj       ).ActionMessage(obj,        action               );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, ActionEnum action,              string message) => ResolveLogging(obj       ).ActionMessage(obj,        action,       message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(obj       ).ActionMessage(obj,        action, name, dummy  );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, ActionEnum action, string name, string message) => ResolveLogging(obj       ).ActionMessage(obj,        action, name, message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj                                                ) => ResolveLogging(obj       ).ActionMessage(obj                              );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj,                                 string message) => ResolveLogging(obj       ).ActionMessage(obj,                      message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, string     action,              int dummy = 0 ) => ResolveLogging(obj       ).ActionMessage(obj,        action               );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, string     action,              string message) => ResolveLogging(obj       ).ActionMessage(obj,        action,       message);
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, string     action, string name, int dummy = 0 ) => ResolveLogging(obj       ).ActionMessage(obj,        action, name, dummy  );
        /// <inheritdoc cref="actionmethodtentityobject />
        public   static string ActionMessage<TEntity>(this TEntity obj, string     action, string name, string message) => ResolveLogging(obj       ).ActionMessage(obj,        action, name, message);
        public   static string ActionMessage(this Type entityType,      ActionEnum action                             ) => ResolveLogging(entityType).ActionMessage(entityType, action               );
        public   static string ActionMessage(this Type entityType,      ActionEnum action,              string message) => ResolveLogging(entityType).ActionMessage(entityType, action,       message);
        public   static string ActionMessage(this Type entityType,      ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entityType).ActionMessage(entityType, action, name, dummy  );
        public   static string ActionMessage(this Type entityType,      ActionEnum action, string name, string message) => ResolveLogging(entityType).ActionMessage(entityType, action, name, message);
        public   static string ActionMessage(this Type entityType                                                     ) => ResolveLogging(entityType).ActionMessage(entityType                       );
        public   static string ActionMessage(this Type entityType,                                      string message) => ResolveLogging(entityType).ActionMessage(entityType,               message);
        public   static string ActionMessage(this Type entityType,      string     action,              int dummy = 0 ) => ResolveLogging(entityType).ActionMessage(entityType, action               );
        public   static string ActionMessage(this Type entityType,      string     action,              string message) => ResolveLogging(entityType).ActionMessage(entityType, action,       message);
        public   static string ActionMessage(this Type entityType,      string     action, string name, int dummy = 0 ) => ResolveLogging(entityType).ActionMessage(entityType, action, name, dummy  );
        public   static string ActionMessage(this Type entityType,      string     action, string name, string message) => ResolveLogging(entityType).ActionMessage(entityType, action, name, message);
        public   static string ActionMessage(this string typeName,      ActionEnum action                             ) => ResolveLogging(typeName  ).ActionMessage(typeName,   action               );
        public   static string ActionMessage(this string typeName,      ActionEnum action,              string message) => ResolveLogging(typeName  ).ActionMessage(typeName,   action,       message);
        public   static string ActionMessage(this string typeName,      ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(typeName  ).ActionMessage(typeName,   action, name, dummy  );
        public   static string ActionMessage(this string typeName,      ActionEnum action, string name, string message) => ResolveLogging(typeName  ).ActionMessage(typeName,   action, name, message);
        public   static string ActionMessage(this string typeName                                                     ) => ResolveLogging(typeName  ).ActionMessage(typeName                         );
        public   static string ActionMessage(this string typeName,                                      string message) => ResolveLogging(typeName  ).ActionMessage(typeName,                 message);
        public   static string ActionMessage(this string typeName,      string     action,              int dummy = 0 ) => ResolveLogging(typeName  ).ActionMessage(typeName,   action               );
        public   static string ActionMessage(this string typeName,      string     action,              string message) => ResolveLogging(typeName  ).ActionMessage(typeName,   action,       message);
        public   static string ActionMessage(this string typeName,      string     action, string name, int dummy = 0 ) => ResolveLogging(typeName  ).ActionMessage(typeName,   action, name, dummy  );
        public   static string ActionMessage(this string typeName,      string     action, string name, string message) => ResolveLogging(typeName  ).ActionMessage(typeName,   action, name, message);

        // Memory Message (On Simple Types)

        // (Always tagged [MEMORY] here, so no need for target types: object. entity Type or TEntity.)
                
        public static string MemoryActionMessage(this int byteCount                                                ) => ResolveLogging(byteCount).MemoryActionMessage(byteCount                       );
        public static string MemoryActionMessage(this int byteCount,                                 string message) => ResolveLogging(byteCount).MemoryActionMessage(byteCount,               message);
        public static string MemoryActionMessage(this int byteCount, ActionEnum action                             ) => ResolveLogging(byteCount).MemoryActionMessage(byteCount, action               );   
        public static string MemoryActionMessage(this int byteCount, ActionEnum action,              string message) => ResolveLogging(byteCount).MemoryActionMessage(byteCount, action,       message);
        public static string MemoryActionMessage(this int byteCount, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(byteCount).MemoryActionMessage(byteCount, action, name, dummy  );
        public static string MemoryActionMessage(this int byteCount, ActionEnum action, string name, string message) => ResolveLogging(byteCount).MemoryActionMessage(byteCount, action, name, message);
        public static string MemoryActionMessage(this int byteCount,                    string name, int dummy = 0 ) => ResolveLogging(byteCount).MemoryActionMessage(byteCount,         name, dummy  );
        public static string MemoryActionMessage(this int byteCount,                    string name, string message) => ResolveLogging(byteCount).MemoryActionMessage(byteCount,         name, message);
        public static string MemoryActionMessage(this int byteCount, string     action, string name, string message) => ResolveLogging(byteCount).MemoryActionMessage(byteCount, action, name, message);
        public static string MemoryActionMessage(this byte[] bytes                                                 ) => ResolveLogging(bytes    ).MemoryActionMessage(bytes                           );
        public static string MemoryActionMessage(this byte[] bytes,                                  string message) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,                   message);
        public static string MemoryActionMessage(this byte[] bytes,  ActionEnum action                             ) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,     action               );   
        public static string MemoryActionMessage(this byte[] bytes,  ActionEnum action,              string message) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,     action,       message);
        public static string MemoryActionMessage(this byte[] bytes,  ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,     action, name, dummy  );
        public static string MemoryActionMessage(this byte[] bytes,  ActionEnum action, string name, string message) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,     action, name, message);
        public static string MemoryActionMessage(this byte[] bytes,                     string name, int dummy = 0 ) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,             name, dummy  );
        public static string MemoryActionMessage(this byte[] bytes,                     string name, string message) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,             name, message);
        public static string MemoryActionMessage(this byte[] bytes,  string     action, string name, string message) => ResolveLogging(bytes    ).MemoryActionMessage(bytes,     action, name, message);
        public static string ActionMessage      (this byte[] bytes                                                 ) => ResolveLogging(bytes    ).      ActionMessage(bytes                           );
        public static string ActionMessage      (this byte[] bytes,                                  string message) => ResolveLogging(bytes    ).      ActionMessage(bytes,                   message);
        public static string ActionMessage      (this byte[] bytes,  ActionEnum action                             ) => ResolveLogging(bytes    ).      ActionMessage(bytes,     action               );   
        public static string ActionMessage      (this byte[] bytes,  ActionEnum action,              string message) => ResolveLogging(bytes    ).      ActionMessage(bytes,     action,       message);
        public static string ActionMessage      (this byte[] bytes,  ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(bytes    ).      ActionMessage(bytes,     action, name, dummy  );
        public static string ActionMessage      (this byte[] bytes,  ActionEnum action, string name, string message) => ResolveLogging(bytes    ).      ActionMessage(bytes,     action, name, message);
        public static string ActionMessage      (this byte[] bytes,                     string name, int dummy = 0 ) => ResolveLogging(bytes    ).      ActionMessage(bytes,             name, dummy  );
        public static string ActionMessage      (this byte[] bytes,                     string name, string message) => ResolveLogging(bytes    ).      ActionMessage(bytes,             name, message);
        public static string ActionMessage      (this byte[] bytes,  string     action, string name, string message) => ResolveLogging(bytes    ).      ActionMessage(bytes,     action, name, message);
        
        // Memory Action Message (On Entities)
        
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes                                                ) => ResolveLogging(entity).MemoryActionMessage(bytes                       );
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes,                                 string message) => ResolveLogging(entity).MemoryActionMessage(bytes,               message);
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes, ActionEnum action                             ) => ResolveLogging(entity).MemoryActionMessage(bytes, action               );   
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes, ActionEnum action,              string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action,       message);
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, dummy  );
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes, ActionEnum action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes,                    string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, dummy  );
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes,                    string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, message);
        public   static string MemoryActionMessage(this FlowNode        entity, byte[] bytes, string     action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes                                                ) => ResolveLogging(entity).MemoryActionMessage(bytes                       );
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes,                                 string message) => ResolveLogging(entity).MemoryActionMessage(bytes,               message);
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, ActionEnum action                             ) => ResolveLogging(entity).MemoryActionMessage(bytes, action               );   
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action,       message);
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, dummy  );
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes,                    string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, dummy  );
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes,                    string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, message);
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, string     action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes                                                ) => ResolveLogging(entity).MemoryActionMessage(bytes                       );
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes,                                 string message) => ResolveLogging(entity).MemoryActionMessage(bytes,               message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, ActionEnum action                             ) => ResolveLogging(entity).MemoryActionMessage(bytes, action               );   
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, ActionEnum action,              string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action,       message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, dummy  );
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, ActionEnum action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes,                    string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, dummy  );
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes,                    string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, string     action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);
        
        public   static string MemoryActionMessage(this Tape            entity                                   ) => ResolveLogging(entity).MemoryActionMessage(entity                 );
        public   static string MemoryActionMessage(this Tape            entity,                    string message) => ResolveLogging(entity).MemoryActionMessage(entity,         message);
        public   static string MemoryActionMessage(this Tape            entity, ActionEnum action                ) => ResolveLogging(entity).MemoryActionMessage(entity, action         );
        public   static string MemoryActionMessage(this Tape            entity, ActionEnum action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this Tape            entity, string     action, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(entity, action, dummy  );
        public   static string MemoryActionMessage(this Tape            entity, string     action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this TapeConfig      entity                                   ) => ResolveLogging(entity).MemoryActionMessage(entity                 );
        public   static string MemoryActionMessage(this TapeConfig      entity,                    string message) => ResolveLogging(entity).MemoryActionMessage(entity,         message);
        public   static string MemoryActionMessage(this TapeConfig      entity, ActionEnum action                ) => ResolveLogging(entity).MemoryActionMessage(entity, action         );
        public   static string MemoryActionMessage(this TapeConfig      entity, ActionEnum action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this TapeConfig      entity, string     action, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(entity, action, dummy  );
        public   static string MemoryActionMessage(this TapeConfig      entity, string     action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this TapeActions     entity                                   ) => ResolveLogging(entity).MemoryActionMessage(entity                 );
        public   static string MemoryActionMessage(this TapeActions     entity,                    string message) => ResolveLogging(entity).MemoryActionMessage(entity,         message);
        public   static string MemoryActionMessage(this TapeActions     entity, ActionEnum action                ) => ResolveLogging(entity).MemoryActionMessage(entity, action         );
        public   static string MemoryActionMessage(this TapeActions     entity, ActionEnum action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this TapeActions     entity, string     action, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(entity, action, dummy  );
        public   static string MemoryActionMessage(this TapeActions     entity, string     action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this TapeAction      entity                                   ) => ResolveLogging(entity).MemoryActionMessage(entity                 );
        public   static string MemoryActionMessage(this TapeAction      entity,                    string message) => ResolveLogging(entity).MemoryActionMessage(entity,         message);
        public   static string MemoryActionMessage(this TapeAction      entity, ActionEnum action                ) => ResolveLogging(entity).MemoryActionMessage(entity, action         );
        public   static string MemoryActionMessage(this TapeAction      entity, ActionEnum action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this TapeAction      entity, string     action, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(entity, action, dummy  );
        public   static string MemoryActionMessage(this TapeAction      entity, string     action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this Buff            entity                                   ) => ResolveLogging(entity).MemoryActionMessage(entity                 );
        public   static string MemoryActionMessage(this Buff            entity,                    string message) => ResolveLogging(entity).MemoryActionMessage(entity,         message);
        public   static string MemoryActionMessage(this Buff            entity, ActionEnum action                ) => ResolveLogging(entity).MemoryActionMessage(entity, action         );
        public   static string MemoryActionMessage(this Buff            entity, ActionEnum action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this Buff            entity, string     action, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(entity, action, dummy  );
        public   static string MemoryActionMessage(this Buff            entity, string     action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, byte[] bytes                                   ) => ResolveLogging(entity).MemoryActionMessage(entity, bytes                 );
        public   static string MemoryActionMessage(this AudioFileOutput entity, byte[] bytes,                    string message) => ResolveLogging(entity).MemoryActionMessage(entity, bytes,         message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, byte[] bytes, ActionEnum action                ) => ResolveLogging(entity).MemoryActionMessage(entity, bytes, action         );
        public   static string MemoryActionMessage(this AudioFileOutput entity, byte[] bytes, ActionEnum action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, bytes, action, message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, byte[] bytes, string     action, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(entity, bytes, action, dummy  );
        public   static string MemoryActionMessage(this AudioFileOutput entity, byte[] bytes, string     action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, bytes, action, message);
        public   static string MemoryActionMessage(this Sample          entity                                   ) => ResolveLogging(entity).MemoryActionMessage(entity                 );
        public   static string MemoryActionMessage(this Sample          entity,                    string message) => ResolveLogging(entity).MemoryActionMessage(entity,         message);
        public   static string MemoryActionMessage(this Sample          entity, ActionEnum action                ) => ResolveLogging(entity).MemoryActionMessage(entity, action         );
        public   static string MemoryActionMessage(this Sample          entity, ActionEnum action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this Sample          entity, string     action, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(entity, action, dummy  );
        public   static string MemoryActionMessage(this Sample          entity, string     action, string message) => ResolveLogging(entity).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes                                   ) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, bytes                 );
        public   static string MemoryActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes,                    string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, bytes,         message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, ActionEnum action                ) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, bytes, action         );
        public   static string MemoryActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, ActionEnum action, string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, bytes, action, message);
        public   static string MemoryActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, string     action, int dummy = 0 ) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, bytes, action, dummy  );
        public   static string MemoryActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, string     action, string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, bytes, action, message);
        public   static string MemoryActionMessage(this Sample          entity, SynthWishes synthWishes                                   ) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity                 );
        public   static string MemoryActionMessage(this Sample          entity, SynthWishes synthWishes,                    string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity,         message);
        public   static string MemoryActionMessage(this Sample          entity, SynthWishes synthWishes, ActionEnum action                ) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, action         );
        public   static string MemoryActionMessage(this Sample          entity, SynthWishes synthWishes, ActionEnum action, string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, action, message);
        public   static string MemoryActionMessage(this Sample          entity, SynthWishes synthWishes, string     action, int dummy = 0 ) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, action, dummy  );
        public   static string MemoryActionMessage(this Sample          entity, SynthWishes synthWishes, string     action, string message) => ResolveLogging(entity, synthWishes).MemoryActionMessage(entity, action, message);
        
        // File Message (On Simple Types)

        public static string FileActionMessage(this string filePath                                                          ) => ResolveLogging(filePath).FileActionMessage(filePath                                 );
        public static string FileActionMessage(this string filePath,                                    string sourceFilePath) => ResolveLogging(filePath).FileActionMessage(filePath,                  sourceFilePath);
        public static string FileActionMessage(this string filePath, ActionEnum action                                       ) => ResolveLogging(filePath).FileActionMessage(filePath, action                         );
        public static string FileActionMessage(this string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(filePath).FileActionMessage(filePath, action,          sourceFilePath);
        public static string FileActionMessage(this string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(filePath).FileActionMessage(filePath, action, message, sourceFilePath);
        public static string FileActionMessage(this string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(filePath).FileActionMessage(filePath, action,          dummy         );
        public static string FileActionMessage(this string filePath, string     action,                 string sourceFilePath) => ResolveLogging(filePath).FileActionMessage(filePath, action,          sourceFilePath);
        public static string FileActionMessage(this string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(filePath).FileActionMessage(filePath, action, message, sourceFilePath);       
                
        // File Action Messages (On Entities)

        public   static string FileActionMessage(this FlowNode        entity, string filePath                                                          ) => ResolveLogging(entity).FileActionMessage(entity, filePath                                 );
        public   static string FileActionMessage(this FlowNode        entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath,                  sourceFilePath);
        public   static string FileActionMessage(this FlowNode        entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action                         );
        public   static string FileActionMessage(this FlowNode        entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        public   static string FileActionMessage(this FlowNode        entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);
        public   static string FileActionMessage(this FlowNode        entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          dummy         );
        public   static string FileActionMessage(this FlowNode        entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        public   static string FileActionMessage(this FlowNode        entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath                                                          ) => ResolveLogging(entity).FileActionMessage(entity, filePath                                 );
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath,                  sourceFilePath);
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action                         );
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          dummy         );
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        internal static string FileActionMessage(this ConfigSection   entity, string filePath                                                          ) => ResolveLogging(entity).FileActionMessage(entity, filePath                                 );
        internal static string FileActionMessage(this ConfigSection   entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath,                  sourceFilePath);
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action                         );
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          dummy         );
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        public   static string FileActionMessage(this Tape            entity                                   ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this Tape            entity, ActionEnum action                ) => ResolveLogging(entity).FileActionMessage(entity, action         );
        public   static string FileActionMessage(this Tape            entity, ActionEnum action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Tape            entity,                    string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this Tape            entity, string     action, int dummy = 0 ) => ResolveLogging(entity).FileActionMessage(entity, action, dummy  );
        public   static string FileActionMessage(this Tape            entity, string     action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this TapeConfig      entity                                   ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this TapeConfig      entity, ActionEnum action                ) => ResolveLogging(entity).FileActionMessage(entity, action         );
        public   static string FileActionMessage(this TapeConfig      entity, ActionEnum action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this TapeConfig      entity,                    string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this TapeConfig      entity, string     action, int dummy = 0 ) => ResolveLogging(entity).FileActionMessage(entity, action, dummy  );
        public   static string FileActionMessage(this TapeConfig      entity, string     action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this TapeActions     entity                                   ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this TapeActions     entity, ActionEnum action                ) => ResolveLogging(entity).FileActionMessage(entity, action         );
        public   static string FileActionMessage(this TapeActions     entity, ActionEnum action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this TapeActions     entity,                    string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this TapeActions     entity, string     action, int dummy = 0 ) => ResolveLogging(entity).FileActionMessage(entity, action, dummy  );
        public   static string FileActionMessage(this TapeActions     entity, string     action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this TapeAction      entity                                   ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this TapeAction      entity, ActionEnum action                ) => ResolveLogging(entity).FileActionMessage(entity, action         );
        public   static string FileActionMessage(this TapeAction      entity, ActionEnum action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this TapeAction      entity,                    string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this TapeAction      entity, string     action, int dummy = 0 ) => ResolveLogging(entity).FileActionMessage(entity, action, dummy  );
        public   static string FileActionMessage(this TapeAction      entity, string     action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Buff            entity                                   ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this Buff            entity, ActionEnum action                ) => ResolveLogging(entity).FileActionMessage(entity, action         );
        public   static string FileActionMessage(this Buff            entity, ActionEnum action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Buff            entity,                    string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this Buff            entity, string     action, int dummy = 0 ) => ResolveLogging(entity).FileActionMessage(entity, action, dummy  );
        public   static string FileActionMessage(this Buff            entity, string     action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this AudioFileOutput entity                                   ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this AudioFileOutput entity, ActionEnum action                ) => ResolveLogging(entity).FileActionMessage(entity, action         );
        public   static string FileActionMessage(this AudioFileOutput entity, ActionEnum action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this AudioFileOutput entity,                    string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this AudioFileOutput entity, string     action, int dummy = 0 ) => ResolveLogging(entity).FileActionMessage(entity, action, dummy  );
        public   static string FileActionMessage(this AudioFileOutput entity, string     action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Sample          entity                                   ) => ResolveLogging(entity).FileActionMessage(entity                 );
        public   static string FileActionMessage(this Sample          entity, ActionEnum action                ) => ResolveLogging(entity).FileActionMessage(entity, action         );
        public   static string FileActionMessage(this Sample          entity, ActionEnum action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Sample          entity,                    string message) => ResolveLogging(entity).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this Sample          entity, string     action, int dummy = 0 ) => ResolveLogging(entity).FileActionMessage(entity, action, dummy  );
        public   static string FileActionMessage(this Sample          entity, string     action, string message) => ResolveLogging(entity).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this AudioFileOutput entity, SynthWishes synthWishes                                   ) => ResolveLogging(entity, synthWishes).FileActionMessage(entity                 );
        public   static string FileActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action                ) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action         );
        public   static string FileActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action, string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this AudioFileOutput entity, SynthWishes synthWishes,                    string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, string     action, int dummy = 0 ) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action, dummy  );
        public   static string FileActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, string     action, string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Sample          entity, SynthWishes synthWishes                                   ) => ResolveLogging(entity, synthWishes).FileActionMessage(entity                 );
        public   static string FileActionMessage(this Sample          entity, SynthWishes synthWishes, ActionEnum action                ) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action         );
        public   static string FileActionMessage(this Sample          entity, SynthWishes synthWishes, ActionEnum action, string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action, message);
        public   static string FileActionMessage(this Sample          entity, SynthWishes synthWishes,                    string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity,         message);
        public   static string FileActionMessage(this Sample          entity, SynthWishes synthWishes, string     action, int dummy = 0 ) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action, dummy  );
        public   static string FileActionMessage(this Sample          entity, SynthWishes synthWishes, string     action, string message) => ResolveLogging(entity, synthWishes).FileActionMessage(entity, action, message);
        
        // LogAction
        
        public   static FlowNode        LogAction(this FlowNode        entity, ActionEnum action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static FlowNode        LogAction(this FlowNode        entity, ActionEnum action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static FlowNode        LogAction(this FlowNode        entity, string     action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static FlowNode        LogAction(this FlowNode        entity, string     message, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, message, dummy ); return entity; }
        public   static FlowNode        LogAction(this FlowNode        entity, string     action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, ActionEnum action                             ) { ResolveLogging(entity).LogAction(entity, action               ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, ActionEnum action,              string message) { ResolveLogging(entity).LogAction(entity, action,       message); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, ActionEnum action, string name, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action, name, dummy  ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, ActionEnum action, string name, string message) { ResolveLogging(entity).LogAction(entity, action, name, message); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity                                                ) { ResolveLogging(entity).LogAction(entity                       ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity,                                 string message) { ResolveLogging(entity).LogAction(entity,               message); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, string     action,              int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action,       dummy  ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, string     action,              string message) { ResolveLogging(entity).LogAction(entity, action,       message); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, string     action, string name, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action, name, dummy  ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, string     action, string name, string message) { ResolveLogging(entity).LogAction(entity, action, name, message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, ActionEnum action                             ) { ResolveLogging(entity).LogAction(entity, action               ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, ActionEnum action,              string message) { ResolveLogging(entity).LogAction(entity, action,       message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, ActionEnum action, string name, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action, name, dummy  ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, ActionEnum action, string name, string message) { ResolveLogging(entity).LogAction(entity, action, name, message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity                                                ) { ResolveLogging(entity).LogAction(entity                       ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity,                                 string message) { ResolveLogging(entity).LogAction(entity,               message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, string     action,              int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action,       dummy  ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, string     action,              string message) { ResolveLogging(entity).LogAction(entity, action,       message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, string     action, string name, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action, name, dummy  ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, string     action, string name, string message) { ResolveLogging(entity).LogAction(entity, action, name, message); return entity; }
        
        public   static Tape            LogAction(this Tape            entity, ActionEnum action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static Tape            LogAction(this Tape            entity, ActionEnum action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static Tape            LogAction(this Tape            entity, string     action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static Tape            LogAction(this Tape            entity, string     message, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, message, dummy ); return entity; }
        public   static Tape            LogAction(this Tape            entity, string     action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static TapeConfig      LogAction(this TapeConfig      entity, ActionEnum action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static TapeConfig      LogAction(this TapeConfig      entity, ActionEnum action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static TapeConfig      LogAction(this TapeConfig      entity, string     action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static TapeConfig      LogAction(this TapeConfig      entity, string     message, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, message, dummy ); return entity; }
        public   static TapeConfig      LogAction(this TapeConfig      entity, string     action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static TapeActions     LogAction(this TapeActions     entity, ActionEnum action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static TapeActions     LogAction(this TapeActions     entity, ActionEnum action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static TapeActions     LogAction(this TapeActions     entity, string     action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static TapeActions     LogAction(this TapeActions     entity, string     message, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, message, dummy ); return entity; }
        public   static TapeActions     LogAction(this TapeActions     entity, string     action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        
        /// <inheritdoc cref="_logtapeaction" />
        public   static TapeAction      Log      (this TapeAction      action                ) { ResolveLogging(action).Log      (action         ); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   static TapeAction      Log      (this TapeAction      action, string message) { ResolveLogging(action).Log      (action, message); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   static TapeAction      LogAction(this TapeAction      action                ) { ResolveLogging(action).LogAction(action         ); return action; }
        /// <inheritdoc cref="_logtapeaction" />
        public   static TapeAction      LogAction(this TapeAction      action, string message) { ResolveLogging(action).LogAction(action, message); return action; }        
        
        public   static Buff            LogAction(this Buff            entity, ActionEnum action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static Buff            LogAction(this Buff            entity, ActionEnum action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static Buff            LogAction(this Buff            entity, string     action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static Buff            LogAction(this Buff            entity, string     message, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, message, dummy ); return entity; }
        public   static Buff            LogAction(this Buff            entity, string     action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, ActionEnum action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, ActionEnum action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, string     action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, string     message, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, message, dummy ); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, string     action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static Sample          LogAction(this Sample          entity, ActionEnum action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static Sample          LogAction(this Sample          entity, ActionEnum action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }
        public   static Sample          LogAction(this Sample          entity, string     action                 ) { ResolveLogging(entity).LogAction(entity, action         ); return entity; }
        public   static Sample          LogAction(this Sample          entity, string     message, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, message, dummy ); return entity; }
        public   static Sample          LogAction(this Sample          entity, string     action,  string message) { ResolveLogging(entity).LogAction(entity, action, message); return entity; }

        public   static AudioFileOutput LogAction(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action                 ) { ResolveLogging(entity, synthWishes).LogAction(entity, action         ); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action,  string message) { ResolveLogging(entity, synthWishes).LogAction(entity, action, message); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, SynthWishes synthWishes, string     action                 ) { ResolveLogging(entity, synthWishes).LogAction(entity, action         ); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, SynthWishes synthWishes, string     message, int dummy = 0 ) { ResolveLogging(entity, synthWishes).LogAction(entity, message, dummy ); return entity; }
        public   static AudioFileOutput LogAction(this AudioFileOutput entity, SynthWishes synthWishes, string     action,  string message) { ResolveLogging(entity, synthWishes).LogAction(entity, action, message); return entity; }
        public   static Sample          LogAction(this Sample          entity, SynthWishes synthWishes, ActionEnum action                 ) { ResolveLogging(entity, synthWishes).LogAction(entity, action         ); return entity; }
        public   static Sample          LogAction(this Sample          entity, SynthWishes synthWishes, ActionEnum action,  string message) { ResolveLogging(entity, synthWishes).LogAction(entity, action, message); return entity; }
        public   static Sample          LogAction(this Sample          entity, SynthWishes synthWishes, string     action                 ) { ResolveLogging(entity, synthWishes).LogAction(entity, action         ); return entity; }
        public   static Sample          LogAction(this Sample          entity, SynthWishes synthWishes, string     message, int dummy = 0 ) { ResolveLogging(entity, synthWishes).LogAction(entity, message, dummy ); return entity; }
        public   static Sample          LogAction(this Sample          entity, SynthWishes synthWishes, string     action,  string message) { ResolveLogging(entity, synthWishes).LogAction(entity, action, message); return entity; }
        
        // LogAction (On Simple Types)
        
        public static void LogAction(this object entity,   ActionEnum action                             ) => ResolveLogging(entity    ).LogAction(entity,     action               );
        public static void LogAction(this object entity,   ActionEnum action,              string message) => ResolveLogging(entity    ).LogAction(entity,     action,       message);
        public static void LogAction(this object entity,   ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity    ).LogAction(entity,     action, name, dummy  );
        public static void LogAction(this object entity,   ActionEnum action, string name, string message) => ResolveLogging(entity    ).LogAction(entity,     action, name, message);
        public static void LogAction(this object entity                                                  ) => ResolveLogging(entity    ).LogAction(entity                           );
        public static void LogAction(this object entity,                                   string message) => ResolveLogging(entity    ).LogAction(entity,                   message);
        public static void LogAction(this object entity,   string     action,              int dummy = 0 ) => ResolveLogging(entity    ).LogAction(entity,     action,       dummy  );
        public static void LogAction(this object entity,   string     action,              string message) => ResolveLogging(entity    ).LogAction(entity,     action,       message);
        public static void LogAction(this object entity,   string     action, string name, int dummy = 0 ) => ResolveLogging(entity    ).LogAction(entity,     action, name, dummy  );
        public static void LogAction(this object entity,   string     action, string name, string message) => ResolveLogging(entity    ).LogAction(entity,     action, name, message);
        public static void LogAction(this Type entityType, ActionEnum action                             ) => ResolveLogging(entityType).LogAction(entityType, action               );
        public static void LogAction(this Type entityType, ActionEnum action,              string message) => ResolveLogging(entityType).LogAction(entityType, action,       message);
        public static void LogAction(this Type entityType, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entityType).LogAction(entityType, action, name, dummy  );
        public static void LogAction(this Type entityType, ActionEnum action, string name, string message) => ResolveLogging(entityType).LogAction(entityType, action, name, message);
        public static void LogAction(this Type entityType                                                ) => ResolveLogging(entityType).LogAction(entityType                       );
        public static void LogAction(this Type entityType,                                 string message) => ResolveLogging(entityType).LogAction(entityType,               message);
        public static void LogAction(this Type entityType, string     action,              int dummy = 0 ) => ResolveLogging(entityType).LogAction(entityType, action,       dummy  );
        public static void LogAction(this Type entityType, string     action,              string message) => ResolveLogging(entityType).LogAction(entityType, action,       message);
        public static void LogAction(this Type entityType, string     action, string name, int dummy = 0 ) => ResolveLogging(entityType).LogAction(entityType, action, name, dummy  );
        public static void LogAction(this Type entityType, string     action, string name, string message) => ResolveLogging(entityType).LogAction(entityType, action, name, message);
        public static void LogAction(this string typeName, ActionEnum action                             ) => ResolveLogging(typeName  ).LogAction(typeName,   action               );
        public static void LogAction(this string typeName, ActionEnum action,              string message) => ResolveLogging(typeName  ).LogAction(typeName,   action,       message);
        public static void LogAction(this string typeName, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(typeName  ).LogAction(typeName,   action, name, dummy  );
        public static void LogAction(this string typeName, ActionEnum action, string name, string message) => ResolveLogging(typeName  ).LogAction(typeName,   action, name, message);
        public static void LogAction(this string typeName                                                ) => ResolveLogging(typeName  ).LogAction(typeName                         );
        public static void LogAction(this string typeName,                                 string message) => ResolveLogging(typeName  ).LogAction(typeName,                 message);
        public static void LogAction(this string typeName, string     action,              int dummy = 0 ) => ResolveLogging(typeName  ).LogAction(typeName,   action,       dummy  );
        public static void LogAction(this string typeName, string     action,              string message) => ResolveLogging(typeName  ).LogAction(typeName,   action,       message);
        public static void LogAction(this string typeName, string     action, string name, int dummy = 0 ) => ResolveLogging(typeName  ).LogAction(typeName,   action, name, dummy  );
        public static void LogAction(this string typeName, string     action, string name, string message) => ResolveLogging(typeName  ).LogAction(typeName,   action, name, message);

        // Memory Action Logging (On Simple Types)

        public static byte[] LogMemoryAction(this byte[] bytes                                                 ) => ResolveLogging(bytes).LogMemoryAction(bytes                           );
        public static byte[] LogMemoryAction(this byte[] bytes,                                  string message) => ResolveLogging(bytes).LogMemoryAction(bytes,                   message);
        public static byte[] LogMemoryAction(this byte[] bytes,  ActionEnum action                             ) => ResolveLogging(bytes).LogMemoryAction(bytes,     action               );
        public static byte[] LogMemoryAction(this byte[] bytes,  ActionEnum action,              string message) => ResolveLogging(bytes).LogMemoryAction(bytes,     action,       message);
        public static byte[] LogMemoryAction(this byte[] bytes,  ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(bytes).LogMemoryAction(bytes,     action, name, dummy  );
        public static byte[] LogMemoryAction(this byte[] bytes,  ActionEnum action, string name, string message) => ResolveLogging(bytes).LogMemoryAction(bytes,     action, name, message);
        public static byte[] LogMemoryAction(this byte[] bytes,                     string name, int dummy = 0 ) => ResolveLogging(bytes).LogMemoryAction(bytes,             name, dummy  );
        public static byte[] LogMemoryAction(this byte[] bytes,                     string name, string message) => ResolveLogging(bytes).LogMemoryAction(bytes,             name, message);
        public static byte[] LogMemoryAction(this byte[] bytes,  string     action, string name, string message) => ResolveLogging(bytes).LogMemoryAction(bytes,     action, name, message);
        public static byte[] LogAction      (this byte[] bytes                                                 ) => ResolveLogging(bytes).LogAction      (bytes                           );
        public static byte[] LogAction      (this byte[] bytes,                                  string message) => ResolveLogging(bytes).LogAction      (bytes,                   message);
        public static byte[] LogAction      (this byte[] bytes,  ActionEnum action                             ) => ResolveLogging(bytes).LogAction      (bytes,     action               );
        public static byte[] LogAction      (this byte[] bytes,  ActionEnum action,              string message) => ResolveLogging(bytes).LogAction      (bytes,     action,       message);
        public static byte[] LogAction      (this byte[] bytes,  ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(bytes).LogAction      (bytes,     action, name, dummy  );
        public static byte[] LogAction      (this byte[] bytes,  ActionEnum action, string name, string message) => ResolveLogging(bytes).LogAction      (bytes,     action, name, message);
        public static byte[] LogAction      (this byte[] bytes,                     string name, int dummy = 0 ) => ResolveLogging(bytes).LogAction      (bytes,             name, dummy  );
        public static byte[] LogAction      (this byte[] bytes,                     string name, string message) => ResolveLogging(bytes).LogAction      (bytes,             name, message);
        public static byte[] LogAction      (this byte[] bytes,  string     action, string name, string message) => ResolveLogging(bytes).LogAction      (bytes,     action, name, message);

        // Memory Action Logging (On Entities)
        
        public   static FlowNode        LogMemoryAction(this FlowNode        entity, byte[] bytes                                   ) => ResolveLogging(entity).LogMemoryAction(entity, bytes                 );
        public   static FlowNode        LogMemoryAction(this FlowNode        entity, byte[] bytes,                    string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         message);
        public   static FlowNode        LogMemoryAction(this FlowNode        entity, byte[] bytes, ActionEnum action                ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action         );
        public   static FlowNode        LogMemoryAction(this FlowNode        entity, byte[] bytes, ActionEnum action, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, message);
        public   static FlowNode        LogMemoryAction(this FlowNode        entity, byte[] bytes, string     action, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, dummy  );
        public   static FlowNode        LogMemoryAction(this FlowNode        entity, byte[] bytes, string     action, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, message);

        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes                                                ) => ResolveLogging(entity).LogMemoryAction(entity, bytes                       );
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes,                                 string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,               message);
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, ActionEnum action                             ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action               );
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action,       message);
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, dummy  );
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, message);
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes,                    string name, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         name, dummy  );
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes,                    string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         name, message);
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, string     action, string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes                                                ) => ResolveLogging(entity).LogMemoryAction(entity, bytes                       );
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes,                                 string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,               message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, ActionEnum action                             ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action               );
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, ActionEnum action,              string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action,       message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, dummy  );
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, ActionEnum action, string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes,                    string name, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         name, dummy  );
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes,                    string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         name, message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, string     action, string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, message);

        public   static Tape            LogMemoryAction(this Tape            entity                                   ) => ResolveLogging(entity).LogMemoryAction(entity                 );
        public   static Tape            LogMemoryAction(this Tape            entity,                    string message) => ResolveLogging(entity).LogMemoryAction(entity,         message);
        public   static Tape            LogMemoryAction(this Tape            entity, ActionEnum action                ) => ResolveLogging(entity).LogMemoryAction(entity, action         );
        public   static Tape            LogMemoryAction(this Tape            entity, ActionEnum action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static Tape            LogMemoryAction(this Tape            entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, action, dummy  );
        public   static Tape            LogMemoryAction(this Tape            entity, string     action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static TapeConfig      LogMemoryAction(this TapeConfig      entity                                   ) => ResolveLogging(entity).LogMemoryAction(entity                 );
        public   static TapeConfig      LogMemoryAction(this TapeConfig      entity,                    string message) => ResolveLogging(entity).LogMemoryAction(entity,         message);
        public   static TapeConfig      LogMemoryAction(this TapeConfig      entity, ActionEnum action                ) => ResolveLogging(entity).LogMemoryAction(entity, action         );
        public   static TapeConfig      LogMemoryAction(this TapeConfig      entity, ActionEnum action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static TapeConfig      LogMemoryAction(this TapeConfig      entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, action, dummy  );
        public   static TapeConfig      LogMemoryAction(this TapeConfig      entity, string     action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static TapeActions     LogMemoryAction(this TapeActions     entity                                   ) => ResolveLogging(entity).LogMemoryAction(entity                 );
        public   static TapeActions     LogMemoryAction(this TapeActions     entity,                    string message) => ResolveLogging(entity).LogMemoryAction(entity,         message);
        public   static TapeActions     LogMemoryAction(this TapeActions     entity, ActionEnum action                ) => ResolveLogging(entity).LogMemoryAction(entity, action         );
        public   static TapeActions     LogMemoryAction(this TapeActions     entity, ActionEnum action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static TapeActions     LogMemoryAction(this TapeActions     entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, action, dummy  );
        public   static TapeActions     LogMemoryAction(this TapeActions     entity, string     action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static TapeAction      LogMemoryAction(this TapeAction      entity                                   ) => ResolveLogging(entity).LogMemoryAction(entity                 );
        public   static TapeAction      LogMemoryAction(this TapeAction      entity,                    string message) => ResolveLogging(entity).LogMemoryAction(entity,         message);
        public   static TapeAction      LogMemoryAction(this TapeAction      entity, ActionEnum action                ) => ResolveLogging(entity).LogMemoryAction(entity, action         );
        public   static TapeAction      LogMemoryAction(this TapeAction      entity, ActionEnum action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static TapeAction      LogMemoryAction(this TapeAction      entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, action, dummy  );
        public   static TapeAction      LogMemoryAction(this TapeAction      entity, string     action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static Buff            LogMemoryAction(this Buff            entity                                   ) => ResolveLogging(entity).LogMemoryAction(entity                 );
        public   static Buff            LogMemoryAction(this Buff            entity,                    string message) => ResolveLogging(entity).LogMemoryAction(entity,         message);
        public   static Buff            LogMemoryAction(this Buff            entity, ActionEnum action                ) => ResolveLogging(entity).LogMemoryAction(entity, action         );
        public   static Buff            LogMemoryAction(this Buff            entity, ActionEnum action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static Buff            LogMemoryAction(this Buff            entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, action, dummy  );
        public   static Buff            LogMemoryAction(this Buff            entity, string     action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, byte[] bytes                                   ) => ResolveLogging(entity).LogMemoryAction(entity, bytes                 );
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, byte[] bytes,                    string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         message);
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, byte[] bytes, ActionEnum action                ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action         );
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, byte[] bytes, ActionEnum action, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, message);
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, byte[] bytes, string     action, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, dummy  );
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, byte[] bytes, string     action, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, message);
        
        public   static Sample          LogMemoryAction(this Sample          entity                                   ) => ResolveLogging(entity).LogMemoryAction(entity                 );
        public   static Sample          LogMemoryAction(this Sample          entity,                    string message) => ResolveLogging(entity).LogMemoryAction(entity,         message);
        public   static Sample          LogMemoryAction(this Sample          entity, ActionEnum action                ) => ResolveLogging(entity).LogMemoryAction(entity, action         );
        public   static Sample          LogMemoryAction(this Sample          entity, ActionEnum action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);
        public   static Sample          LogMemoryAction(this Sample          entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, action, dummy  );
        public   static Sample          LogMemoryAction(this Sample          entity, string     action, string message) => ResolveLogging(entity).LogMemoryAction(entity, action, message);

        
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes                                   ) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, bytes                 );
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes,                    string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, bytes,         message);
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, ActionEnum action                ) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, bytes, action         );
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, ActionEnum action, string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, bytes, action, message);
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, string     action, int dummy = 0 ) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, bytes, action, dummy  );
        public   static AudioFileOutput LogMemoryAction(this AudioFileOutput entity, SynthWishes synthWishes, byte[] bytes, string     action, string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, bytes, action, message);
        
        public   static Sample          LogMemoryAction(this Sample          entity, SynthWishes synthWishes                                   ) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity                 );
        public   static Sample          LogMemoryAction(this Sample          entity, SynthWishes synthWishes,                    string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity,         message);
        public   static Sample          LogMemoryAction(this Sample          entity, SynthWishes synthWishes, ActionEnum action                ) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, action         );
        public   static Sample          LogMemoryAction(this Sample          entity, SynthWishes synthWishes, ActionEnum action, string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, action, message);
        public   static Sample          LogMemoryAction(this Sample          entity, SynthWishes synthWishes, string     action, int dummy = 0 ) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, action, dummy  );
        public   static Sample          LogMemoryAction(this Sample          entity, SynthWishes synthWishes, string     action, string message) => ResolveLogging(entity, synthWishes).LogMemoryAction(entity, action, message);

        // LogFileAction
        
        public   static string         LogFileAction(this                        string filePath                                                          ) => ResolveLogging(filePath).LogFileAction(        filePath                                 );
        public   static string         LogFileAction(this                        string filePath,                                    string sourceFilePath) => ResolveLogging(filePath).LogFileAction(        filePath,                  sourceFilePath);
        public   static string         LogFileAction(this                        string filePath, ActionEnum action                                       ) => ResolveLogging(filePath).LogFileAction(        filePath, action                         );
        public   static string         LogFileAction(this                        string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(filePath).LogFileAction(        filePath, action,          sourceFilePath);
        public   static string         LogFileAction(this                        string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(filePath).LogFileAction(        filePath, action, message, sourceFilePath);
        public   static string         LogFileAction(this                        string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(filePath).LogFileAction(        filePath, action,          dummy         );
        public   static string         LogFileAction(this                        string filePath, string     action,                 string sourceFilePath) => ResolveLogging(filePath).LogFileAction(        filePath, action,          sourceFilePath);
        public   static string         LogFileAction(this                        string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(filePath).LogFileAction(        filePath, action, message, sourceFilePath);
        public   static FlowNode       LogFileAction(this FlowNode       entity, string filePath                                                          ) => ResolveLogging(entity  ).LogFileAction(entity, filePath                                 );
        public   static FlowNode       LogFileAction(this FlowNode       entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath,                  sourceFilePath);
        public   static FlowNode       LogFileAction(this FlowNode       entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action                         );
        public   static FlowNode       LogFileAction(this FlowNode       entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        public   static FlowNode       LogFileAction(this FlowNode       entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        public   static FlowNode       LogFileAction(this FlowNode       entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          dummy         );
        public   static FlowNode       LogFileAction(this FlowNode       entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        public   static FlowNode       LogFileAction(this FlowNode       entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath                                                          ) => ResolveLogging(entity  ).LogFileAction(entity, filePath                                 );
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath,                  sourceFilePath);
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action                         );
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          dummy         );
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath                                                          ) => ResolveLogging(entity  ).LogFileAction(entity, filePath                                 );
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath,                  sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action                         );
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          dummy         );
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        
        public static Tape            LogFileAction(this Tape            entity                                   ) => ResolveLogging(entity).LogFileAction(entity                 );
        public static Tape            LogFileAction(this Tape            entity, ActionEnum action                ) => ResolveLogging(entity).LogFileAction(entity, action         );
        public static Tape            LogFileAction(this Tape            entity, ActionEnum action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static Tape            LogFileAction(this Tape            entity,                    string message) => ResolveLogging(entity).LogFileAction(entity,         message);
        public static Tape            LogFileAction(this Tape            entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogFileAction(entity, action, dummy  );
        public static Tape            LogFileAction(this Tape            entity, string     action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static TapeConfig      LogFileAction(this TapeConfig      entity                                   ) => ResolveLogging(entity).LogFileAction(entity                 );
        public static TapeConfig      LogFileAction(this TapeConfig      entity, ActionEnum action                ) => ResolveLogging(entity).LogFileAction(entity, action         );
        public static TapeConfig      LogFileAction(this TapeConfig      entity, ActionEnum action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static TapeConfig      LogFileAction(this TapeConfig      entity,                    string message) => ResolveLogging(entity).LogFileAction(entity,         message);
        public static TapeConfig      LogFileAction(this TapeConfig      entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogFileAction(entity, action, dummy  );
        public static TapeConfig      LogFileAction(this TapeConfig      entity, string     action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static TapeActions     LogFileAction(this TapeActions     entity                                   ) => ResolveLogging(entity).LogFileAction(entity                 );
        public static TapeActions     LogFileAction(this TapeActions     entity, ActionEnum action                ) => ResolveLogging(entity).LogFileAction(entity, action         );
        public static TapeActions     LogFileAction(this TapeActions     entity, ActionEnum action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static TapeActions     LogFileAction(this TapeActions     entity,                    string message) => ResolveLogging(entity).LogFileAction(entity,         message);
        public static TapeActions     LogFileAction(this TapeActions     entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogFileAction(entity, action, dummy  );
        public static TapeActions     LogFileAction(this TapeActions     entity, string     action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static TapeAction      LogFileAction(this TapeAction      entity                                   ) => ResolveLogging(entity).LogFileAction(entity                 );
        public static TapeAction      LogFileAction(this TapeAction      entity, ActionEnum action                ) => ResolveLogging(entity).LogFileAction(entity, action         );
        public static TapeAction      LogFileAction(this TapeAction      entity, ActionEnum action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static TapeAction      LogFileAction(this TapeAction      entity,                    string message) => ResolveLogging(entity).LogFileAction(entity,         message);
        public static TapeAction      LogFileAction(this TapeAction      entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogFileAction(entity, action, dummy  );
        public static TapeAction      LogFileAction(this TapeAction      entity, string     action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static Buff            LogFileAction(this Buff            entity                                   ) => ResolveLogging(entity).LogFileAction(entity                 );
        public static Buff            LogFileAction(this Buff            entity, ActionEnum action                ) => ResolveLogging(entity).LogFileAction(entity, action         );
        public static Buff            LogFileAction(this Buff            entity, ActionEnum action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static Buff            LogFileAction(this Buff            entity,                    string message) => ResolveLogging(entity).LogFileAction(entity,         message);
        public static Buff            LogFileAction(this Buff            entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogFileAction(entity, action, dummy  );
        public static Buff            LogFileAction(this Buff            entity, string     action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity                                   ) => ResolveLogging(entity).LogFileAction(entity                 );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, ActionEnum action                ) => ResolveLogging(entity).LogFileAction(entity, action         );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, ActionEnum action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity,                    string message) => ResolveLogging(entity).LogFileAction(entity,         message);
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogFileAction(entity, action, dummy  );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, string     action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static Sample          LogFileAction(this Sample          entity                                   ) => ResolveLogging(entity).LogFileAction(entity                 );
        public static Sample          LogFileAction(this Sample          entity, ActionEnum action                ) => ResolveLogging(entity).LogFileAction(entity, action         );
        public static Sample          LogFileAction(this Sample          entity, ActionEnum action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);
        public static Sample          LogFileAction(this Sample          entity,                    string message) => ResolveLogging(entity).LogFileAction(entity,         message);
        public static Sample          LogFileAction(this Sample          entity, string     action, int dummy = 0 ) => ResolveLogging(entity).LogFileAction(entity, action, dummy  );
        public static Sample          LogFileAction(this Sample          entity, string     action, string message) => ResolveLogging(entity).LogFileAction(entity, action, message);

        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, SynthWishes synthWishes                                   ) => ResolveLogging(entity, synthWishes).LogFileAction(entity                 );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action                ) => ResolveLogging(entity, synthWishes).LogFileAction(entity, action         );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action, string message) => ResolveLogging(entity, synthWishes).LogFileAction(entity, action, message);
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, SynthWishes synthWishes,                    string message) => ResolveLogging(entity, synthWishes).LogFileAction(entity,         message);
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, SynthWishes synthWishes, string     action, int dummy = 0 ) => ResolveLogging(entity, synthWishes).LogFileAction(entity, action, dummy  );
        public static AudioFileOutput LogFileAction(this AudioFileOutput entity, SynthWishes synthWishes, string     action, string message) => ResolveLogging(entity, synthWishes).LogFileAction(entity, action, message);
        public static Sample          LogFileAction(this Sample          entity, SynthWishes synthWishes                                   ) => ResolveLogging(entity, synthWishes).LogFileAction(entity                 );
        public static Sample          LogFileAction(this Sample          entity, SynthWishes synthWishes, ActionEnum action                ) => ResolveLogging(entity, synthWishes).LogFileAction(entity, action         );
        public static Sample          LogFileAction(this Sample          entity, SynthWishes synthWishes, ActionEnum action, string message) => ResolveLogging(entity, synthWishes).LogFileAction(entity, action, message);
        public static Sample          LogFileAction(this Sample          entity, SynthWishes synthWishes,                    string message) => ResolveLogging(entity, synthWishes).LogFileAction(entity,         message);
        public static Sample          LogFileAction(this Sample          entity, SynthWishes synthWishes, string     action, int dummy = 0 ) => ResolveLogging(entity, synthWishes).LogFileAction(entity, action, dummy  );
        public static Sample          LogFileAction(this Sample          entity, SynthWishes synthWishes, string     action, string message) => ResolveLogging(entity, synthWishes).LogFileAction(entity, action, message);
    }
}