using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    /// <summary> Non-exhaustive list of log actions. </summary>
    public enum LogActions
    {
        Start,
        Stop,
        Create,
        Update,
        Delete,
        Write 
    }
}
