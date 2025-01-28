        
        private static string GetPaddingDescriptor(double? leadingSilence, double? trailingSilence)
        {
            string descriptor = "";
            
            if (leadingSilence != trailingSilence)
            {
                if (leadingSilence != null && leadingSilence != 0)
                {
                    descriptor += $"Leading Silence {PrettyDuration(leadingSilence)}";
                }
                
                if (trailingSilence != null && trailingSilence != 0)
                {
                    descriptor += $"Trailing Silence {PrettyDuration(trailingSilence)}";
                }
            }
            else
            {
                if (leadingSilence != null && leadingSilence != 0)
                {
                    descriptor += $"Padding {PrettyDuration(leadingSilence)}";
                }
            }
            
            return descriptor;
        }


Backup copy:

            if (FilledIn(channelCount) && channel == null) 
            {
                if (channelCount == 1) elements.Add("Mono");
                else if (channelCount == 2) elements.Add("Stereo");
                else elements.Add($"{channelCount} Channels");
            }
            else if (!FilledIn(channelCount) && channel != null)
            {
                if (channel == 0) elements.Add("Left");
                else if (channel == 1) elements.Add("Right");
                else elements.Add($"Channel {channel}");
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            else if (FilledIn(channelCount) && channel != null) 
            {
                if (channelCount == 1 && channel == 0) elements.Add("Mono");
                else if (channelCount == 1) elements.Add($"Mono | ⚠️ Channel {channel}");
                else if (channelCount == 2 && channel == 0) elements.Add("Left");
                else if (channelCount == 2 && channel == 1) elements.Add("Right");
                else if (channelCount == 2) elements.Add($"Stereo | ⚠️ Channel {channel}");
                else elements.Add($"{channelCount} Channels | Channel {channel}");
            }

Backup copy (2):

            if (FilledIn(channelCount) && channel == null) 
            {
                elements.Add(channelCount == 1 ? "Mono" : channelCount == 2 ? "Stereo" : $"{channelCount} Channels");
            }
            else if (!FilledIn(channelCount) && channel != null)
            {
                elements.Add(channel == 0 ? "Left" : channel == 1 ? "Right" : $"Channel {channel}");
            }
            else if (FilledIn(channelCount) && channel != null) 
            {
                if (channelCount == 1)
                {
                    elements.Add(channel == 0 ? "Mono" : $"Mono | ⚠️ Channel {channel}");
                }
                else if (channelCount == 2)
                {
                    if (channel == 0) elements.Add("Left");
                    else if (channel == 1) elements.Add("Right");
                    else elements.Add($"Stereo | ⚠️ Channel {channel}");
                }
                else 
                {
                    elements.Add(channel < channelCount 
                        ? $"{channelCount} Channels | Channel {channel}"
                        : $"{channelCount} Channels | ⚠️ Channel {channel}");
                }
            }

Backup copy 3:

        private static string GetChannelDescriptor(int? channelCount, int? channel)
        {
            if (!FilledIn(channelCount) && channel == null) return default;
            
            if (FilledIn(channelCount) && channel == null) 
            {
                return channelCount == 1 ? "Mono" : channelCount == 2 ? "Stereo" : $"{channelCount} Channels";
            }
            
            if (!FilledIn(channelCount) && channel != null)
            {
                return channel == 0 ? "Left" : channel == 1 ? "Right" : $"Channel {channel}";
            }
            
            if (FilledIn(channelCount) && channel != null)
            {
                if (channelCount == 1)
                {
                    return channel == 0 ? "Mono" : $"Mono | ⚠️ Channel {channel}";
                }
                
                if (channelCount == 2)
                {
                    if (channel == 0) return "Left";
                    if (channel == 1) return "Right";
                    return $"Stereo | ⚠️ Channel {channel}";
                }
                
                return channel < channelCount
                    ? $"{channelCount} Channels | Channel {channel}"
                    : $"{channelCount} Channels | ⚠️ Channel {channel}";
            }
            
            return default;
        }