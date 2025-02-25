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

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        internal string IDDescriptor(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            return IDDescriptor(tape.IDs);
        }
        
        internal string IDDescriptor(IList<int> ids)
        {
            if (!Has(ids))
            {
                return "no ID";
            }
            
            string idDescriptor = Join("|", ids);
            return idDescriptor;
        }
        
        public string Descriptor(TapeActions actions)
        {
            if (actions == null) throw new ArgumentNullException(nameof(actions));
            
            var elements = new List<string>();
            
            if (actions.Play.Done) elements.Add("played");
            else if (actions.Play.On) elements.Add("play");
            
            if (actions.PlayChannels.Done) elements.Add("played-ch");
            else if (actions.PlayChannels.On) elements.Add("play-ch");
            
            if (actions.Save.Done) elements.Add("saved");
            else if (actions.Save.On) elements.Add("save");
            
            if (actions.SaveChannels.Done) elements.Add("saved-ch");
            else if (actions.SaveChannels.On) elements.Add("save-ch");
            
            if (actions.BeforeRecord.Done) elements.Add("before-done");
            else if (actions.BeforeRecord.On) elements.Add("before");
            if (actions.BeforeRecord.Callback != null) elements.Add("cbk");
            
            if (actions.AfterRecord.Done) elements.Add("after-done");
            else if (actions.AfterRecord.On) elements.Add("after");
            if (actions.AfterRecord.Callback != null) elements.Add("cbk");
            
            if (actions.BeforeRecordChannel.Done) elements.Add("before-ch-done");
            else if (actions.BeforeRecordChannel.On) elements.Add("before-ch");
            if (actions.BeforeRecordChannel.Callback != null) elements.Add("cbk");
            
            if (actions.AfterRecordChannel.Done) elements.Add("after-ch-done");
            else if (actions.AfterRecordChannel.On) elements.Add("after-ch");
            if (actions.AfterRecordChannel.Callback != null) elements.Add("cbk");

            return Join(",", elements);
        }
        
        public string Descriptor(Tape tape)
        {
            if (tape == null) return "<Tape=null>";

            string prefix = "";
            if (tape.Config.IsStereo && tape.Config.Channel == null) prefix = "(Stereo) ";
            else if (tape.NestingLevel > 0) prefix = $"(Level {tape.NestingLevel}) ";
            
            string nameDescriptor = tape.GetName();
            if (!Has(nameDescriptor)) nameDescriptor = "<Untitled>";
            
            var flags = new List<string>();
            
            string actions = Descriptor(tape.Actions);
            flags.Add(actions);
            
            if (tape.IsTape) flags.Add("tape");
            
            string channel = ChannelDescriptor(tape)?.ToLower();
            flags.Add(channel);
            
            if (Has(tape.Duration)) flags.Add($"{tape.Duration:0.###}s");
            
            if (tape.IsPadded)
            {
                if (tape.LeadingSilence == tape.TrailingSilence)
                {
                    flags.Add($"pad{tape.LeadingSilence:#.##}");
                }
                else
                {
                    flags.Add($"pad{tape.LeadingSilence:#.##},{tape.TrailingSilence:#.##}");
                }
            }
            
            if (Has(tape.UnderlyingAudioFileOutput)) flags.Add("out");
            if (Has(tape.Bytes)) flags.Add("mem");
            // TODO: Combine with saved flag, report inconsistencies if present.
            if (Exists(tape.FilePathResolved)) flags.Add("file");
            if (Has(tape.Sample)) flags.Add("smp");

            if (tape.SynthWishes != null)
            {
                if (Has(tape.Config.SamplingRate) && 
                    tape.Config.SamplingRate != tape.SynthWishes.GetSamplingRate)
                {
                    flags.Add($"{tape.Config.SamplingRate}hz");
                }

                if (Has(tape.Config.Bits) && 
                    tape.Config.Bits != tape.SynthWishes.GetBits)
                {
                    flags.Add($"{tape.Config.Bits}bit");
                }

                if (Has(tape.Config.AudioFormat) && 
                    tape.Config.AudioFormat != tape.SynthWishes.GetAudioFormat)
                {
                    flags.Add($"{tape.Config.AudioFormat}".ToLower());
                }

                if (Has(tape.Config.Interpolation) &&
                    tape.Config.Interpolation != tape.SynthWishes.GetInterpolation)
                {
                    flags.Add($"{tape.Config.Interpolation}".ToLower());
                }
            }

            flags = flags.Where(FilledIn).ToList();
            
            string flagDescriptor = default;
            if (flags.Count > 0)
            {
                flagDescriptor = " {" + Join(",", flags) + "}";
            }
            
            string idDescriptor = $" ({IDDescriptor(tape)})";
            
            return prefix + nameDescriptor + flagDescriptor + idDescriptor;
        }
        
        private string Descriptors(IList<Tape> tapes)
        {
           if (!Has(tapes)) return default;
           string[] tapeDescriptors = tapes.Where(x => x != null).Select(Descriptor).ToArray();
           return Join(NewLine, tapeDescriptors);
        }
        
        internal string TapesLeftMessage(int todoCount, Tape[] tapesLeft)
        {
            string prefix = default;
            if (todoCount != 0)
            {
                prefix = $"{todoCount} {nameof(Tape)}(s) Left: ";
            }
            
            if (Has(tapesLeft))
            {
                return prefix + NewLine + Descriptors(tapesLeft);
            }
            else
            {
                return prefix + "<none>";
            }
        }

        public string Descriptor(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            
            string name = Has(audioFileOutput.Name) ? audioFileOutput.Name : "";
            string configLog = ConfigLog(audioFileOutput);
            string filePath = Exists(audioFileOutput.FilePath) ? audioFileOutput.FilePath : "";
            
            string joined = Join(" | ", new[] { name, configLog, filePath }.Where(FilledIn));
            return joined;
        }

        public string Descriptor(IList<FlowNode> signals)
        {
            if (signals == null) throw new ArgumentNullException(nameof(signals));
            return signals.Count == 0 ? "<Signal=null>" : Join(" | ", signals.Select(x => $"{x}"));
        }
    }

    public static partial class LogExtensions
    {
        public static string Descriptor(this Tape tape)                       => Static.Descriptor(tape);
        public static string Descriptor(this TapeActions actions)             => Static.Descriptor(actions);
        public static string Descriptor(this AudioFileOutput audioFileOutput) => Static.Descriptor(audioFileOutput);
        public static string Descriptor(this IList<FlowNode> signals)         => Static.Descriptor(signals);
    }
}
