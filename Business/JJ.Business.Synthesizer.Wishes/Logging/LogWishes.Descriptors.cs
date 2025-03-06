using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using static System.Environment;
using static System.IO.File;
using static System.String;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.FilledInHelper;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;

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
            
            string channel = tape.ChannelDescriptor()?.ToLower();
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
        
        public string Descriptors(IList<Tape> tapes)
        {
           if (!Has(tapes)) return default;
           string[] tapeDescriptors = tapes.Where(x => x != null).Select(Descriptor).ToArray();
           return Join(NewLine, tapeDescriptors);
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

        public string Descriptor(IList<FlowNode> signals)
        {
            if (signals == null) throw new ArgumentNullException(nameof(signals));
            return signals.Count == 0 ? "<Signal=null>" : Join(" | ", signals.Select(x => $"{x}"));
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

        public string ChannelDescriptor(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            return ChannelDescriptor(tape.Config);
        }

        public string ChannelDescriptor(TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new ArgumentNullException(nameof(tapeConfig));
            return ChannelDescriptor(tapeConfig.Channels, tapeConfig.Channel);
        }
        
        public string ChannelDescriptor(int? channelCount, int? channel = null)
        {
            if (!Has(channelCount) && channel == null)
                return default;
            
            if (Has(channelCount) && channel == null)
                return channelCount == 1 ? "Mono" : channelCount == 2 ? "Stereo" : $"{channelCount} Channels";
            
            if (!Has(channelCount) && channel != null)
                return channel == 0 ? "Left" : channel == 1 ? "Right" : $"Channel {channel}";
            
            if (Has(channelCount) && channel != null)
            {
                if (channelCount == 1)
                    return channel == 0 ? "Mono" : $"Mono | ⚠ Channel {channel}";
                
                if (channelCount == 2)
                    return channel == 0 ? "Left" : channel == 1 ? "Right" : $"Stereo | ⚠ Channel {channel}";
                
                return channel < channelCount
                    ? $"{channelCount} Channels | Channel {channel}"
                    : $"{channelCount} Channels | ⚠ Channel {channel}";
            }
            
            return default;
        }
        
        internal string TapesLeftMessage(IList<Tape> tapesLeft, int todoCount)
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
    }
}

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        internal string IDDescriptor     (Tape tape                             ) => Logging.IDDescriptor     (tape                 );
        internal string IDDescriptor     (IList<int> ids                        ) => Logging.IDDescriptor     (ids                  );
        public   string Descriptor       (Tape tape                             ) => Logging.Descriptor       (tape                 );
        public   string Descriptors      (IList<Tape> tapes                     ) => Logging.Descriptors      (tapes                );
        public   string Descriptor       (TapeActions actions                   ) => Logging.Descriptor       (actions              );
        public   string Descriptor       (IList<FlowNode> signals               ) => Logging.Descriptor       (signals              );
        public   string Descriptor       (AudioFileOutput audioFileOutput       ) => Logging.Descriptor       (audioFileOutput      );
        public   string ChannelDescriptor(Tape tape                             ) => Logging.ChannelDescriptor(tape                 );
        public   string ChannelDescriptor(TapeConfig tapeConfig                 ) => Logging.ChannelDescriptor(tapeConfig           );
        public   string ChannelDescriptor(int? channelCount, int? channel = null) => Logging.ChannelDescriptor(channelCount, channel);
        internal string TapesLeftMessage (IList<Tape> tapesLeft,  int todoCount ) => Logging.TapesLeftMessage (tapesLeft, todoCount );
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {
        internal static string IDDescriptor     (this Tape tape                             ) => ResolveLogging(tape           ).IDDescriptor     (tape                 );
        internal static string IDDescriptor     (this IList<int> ids                        ) => ResolveLogging(ids            ).IDDescriptor     (ids                  );
        public   static string Descriptor       (this Tape tape                             ) => ResolveLogging(tape           ).Descriptor       (tape                 );
        public   static string Descriptor       (this IList<Tape> tapes                     ) => ResolveLogging(tapes          ).Descriptors      (tapes                );
        public   static string Descriptor       (this TapeActions actions                   ) => ResolveLogging(actions        ).Descriptor       (actions              );
        public   static string Descriptor       (this IList<FlowNode> signals               ) => ResolveLogging(signals        ).Descriptor       (signals              );
        public   static string Descriptor       (this AudioFileOutput audioFileOutput       ) => ResolveLogging(audioFileOutput).Descriptor       (audioFileOutput      );
        public   static string ChannelDescriptor(this Tape tape                             ) => ResolveLogging(tape           ).ChannelDescriptor(tape                 );
        public   static string ChannelDescriptor(this TapeConfig tapeConfig                 ) => ResolveLogging(tapeConfig     ).ChannelDescriptor(tapeConfig           );
        public   static string ChannelDescriptor(this int? channelCount, int? channel = null) => ResolveLogging(channelCount   ).ChannelDescriptor(channelCount, channel);
        internal static string TapesLeftMessage (this IList<Tape> tapesLeft, int todoCount  ) => ResolveLogging(tapesLeft      ).TapesLeftMessage (tapesLeft, todoCount );
        
        // On different entities for logging context
        // (Outcommented the redundant)

        internal static string IDDescriptor     (this FlowNode        entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this FlowNode        entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        public   static string Descriptor       (this FlowNode        entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this FlowNode        entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        public   static string Descriptor       (this FlowNode        entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this FlowNode        entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this FlowNode        entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string ChannelDescriptor(this FlowNode        entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this FlowNode        entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this FlowNode        entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this FlowNode        entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
                                                                                                                              
        internal static string IDDescriptor     (this ConfigResolver  entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this ConfigResolver  entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string ChannelDescriptor(this ConfigResolver  entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        internal static string ChannelDescriptor(this ConfigResolver  entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        internal static string ChannelDescriptor(this ConfigResolver  entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this ConfigResolver  entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
                                                                                                                              
        internal static string IDDescriptor     (this ConfigSection   entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this ConfigSection   entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string ChannelDescriptor(this ConfigSection   entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        internal static string ChannelDescriptor(this ConfigSection   entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        internal static string ChannelDescriptor(this ConfigSection   entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this ConfigSection   entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
                                                                                                                              
        // TODO: Add overload with tuples
        
        //internal static string IDDescriptor   (this Tape            entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);                                                                                )
        internal static string IDDescriptor     (this Tape            entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        //public static string Descriptor       (this Tape            entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);                                                                                                                             )
        public   static string Descriptor       (this Tape            entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        public   static string Descriptor       (this Tape            entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this Tape            entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this Tape            entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        //public static string ChannelDescriptor(this Tape            entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);                                                                                                                             )
        public   static string ChannelDescriptor(this Tape            entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this Tape            entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this Tape            entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
                                                                                                                              
        internal static string IDDescriptor     (this TapeConfig      entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this TapeConfig      entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        public   static string Descriptor       (this TapeConfig      entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this TapeConfig      entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        public   static string Descriptor       (this TapeConfig      entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this TapeConfig      entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this TapeConfig      entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string ChannelDescriptor(this TapeConfig      entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        //public static string ChannelDescriptor(this TapeConfig      entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);                                                                                                                             )
        public   static string ChannelDescriptor(this TapeConfig      entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this TapeConfig      entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
                                                                                                                              
        internal static string IDDescriptor     (this TapeActions     entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this TapeActions     entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        public   static string Descriptor       (this TapeActions     entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this TapeActions     entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        //public static string Descriptor       (this TapeActions     entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);                                                                                                                             )
        public   static string Descriptor       (this TapeActions     entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this TapeActions     entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string ChannelDescriptor(this TapeActions     entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this TapeActions     entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this TapeActions     entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this TapeActions     entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
                                                                                                                              
        internal static string IDDescriptor     (this TapeAction      entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this TapeAction      entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        public   static string Descriptor       (this TapeAction      entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this TapeAction      entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        public   static string Descriptor       (this TapeAction      entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this TapeAction      entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this TapeAction      entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string ChannelDescriptor(this TapeAction      entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this TapeAction      entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this TapeAction      entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this TapeAction      entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
                                                                                                                              
        internal static string IDDescriptor     (this Buff            entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this Buff            entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        public   static string Descriptor       (this Buff            entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this Buff            entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        public   static string Descriptor       (this Buff            entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this Buff            entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this Buff            entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string ChannelDescriptor(this Buff            entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this Buff            entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this Buff            entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this Buff            entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
                                                                                                                              
        internal static string IDDescriptor     (this AudioFileOutput entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this AudioFileOutput entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        public   static string Descriptor       (this AudioFileOutput entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this AudioFileOutput entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        public   static string Descriptor       (this AudioFileOutput entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this AudioFileOutput entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        //public static string Descriptor       (this AudioFileOutput entityForLogContext, AudioFileOutput entityToDescribe              ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);                                                                                                                             )
        public   static string ChannelDescriptor(this AudioFileOutput entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this AudioFileOutput entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this AudioFileOutput entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this AudioFileOutput entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
                                                                                                                              
        internal static string IDDescriptor     (this Sample          entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this Sample          entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        public   static string Descriptor       (this Sample          entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this Sample          entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        public   static string Descriptor       (this Sample          entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this Sample          entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string Descriptor       (this Sample          entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        public   static string ChannelDescriptor(this Sample          entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this Sample          entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        public   static string ChannelDescriptor(this Sample          entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this Sample          entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);
    }
}
