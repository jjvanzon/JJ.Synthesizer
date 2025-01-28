ActionRunnes:

    internal class ChannelActionRunner : ActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action)
        {
            bool active = action.Tape.Config.Channel != null && action.IsForChannel;
            if (active && active != action.Active)
            {
                throw new Exception("action.Active did not return true when expected.");
            }
            return active;
        }
    }
    
    internal class MonoActionRunner : ActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action)
        {
            bool active = action.Tape.Config.IsMono && !action.IsForChannel;
            if (active && active != action.Active)
            {
                throw new Exception("action.Active did not return true when expected.");
            }
            return active;
        }
    }
    
    internal class StereoActionRunner : ActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action)
        {
            bool active = action.Tape.Config.IsStereo && !action.IsForChannel;
            if (active && active != action.Active)
            {
                throw new Exception("action.Active did not return true when expected.");
            }
            return active;
        }
    }



TapeAction:

    /// <inheritdoc cref="docs._tapeactionactive" />"/>
    public bool Active
    {
        get
        {
            if (!On) return false;
            if (Done) return false;
            if (IsIntercept && Callback == null) 
            {
                throw new Exception("Intercept action is missing its callback.");
            }
            
            if (Tape.Config.IsStereo) 
            {
                return IsForChannel == IsChannel;
            }
            
            return true;
            if (Tape.Config.IsMono && !IsChannel)
            {
                throw new Exception("Mono tape is known to always have channel 0. Does it not?");
            }
            
            if (Tape.Config.IsMono)
            {
                if (IsForChannel) return IsChannel;
                return true;
            }
            
            // TODO: Exception?
            throw new Exception("Tape should be either IsStereo or IsMono, but it has neither.");
            return false;
            return (IsChannel && IsForChannel) || // For Channel Tapes
                    (Tape.Config.IsMono && !IsForChannel) || // For Mono
                    (Tape.Config.IsStereo && !IsForChannel); // For Stereo.
        }
    }

    if (Type == ActionEnum.DiskCache)
    {
        return true;
        // Only return DiskCache as active if a save action isn't already active as well?
        // Too fragile.
        //return !Tape.Actions.Save.Active && !Tape.Actions.SaveChannels.Active;
        //return (!Tape.Actions.Save.Active || Tape.Actions.Save.Done) &&
        //       (!Tape.Actions.SaveChannels.Active || Tape.Actions.SaveChannels.Done);
    }
