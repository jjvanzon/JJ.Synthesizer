
        class Case
        {

            /// <summary>
            /// Constructor that initializes main property.
            /// </summary>
            public Case(int from, int to) { FromFrameCount = from; ToFrameCount = to; }
                
            /// <summary>
            /// Constructor that initializes dependencies to practical defaults.
            /// </summary>
            public Case(
                int    samplingRate   = 44100,
                double audioLength    = 1.6,
                int    courtesyFrames = 3,
                int    channels       = 2)
            {
                SamplingRate   = samplingRate;
                AudioLength    = audioLength;
                CourtesyFrames = courtesyFrames;
                Channels       = channels;
            }
            
            // FrameCount:
            
            // The main property being tested,
            // adjusted directly or via dependencies.
            
            public int? FromFrameCountNully     { get; set; }
            public int  FromFrameCountCoalesced { get; set; }
            public int? ToFrameCountNully       { get; set; }
            public int  ToFrameCountCoalesced   { get; set; }

            public int FrameCount
            {
                get => FromFrameCount == ToFrameCount ? ToFrameCount : default;
                set => FromFrameCount  = ToFrameCount = value;
            }
            
            public int FromFrameCount
            {
                get => FromFrameCountNully == FromFrameCountCoalesced ? FromFrameCountCoalesced : default;
                set => FromFrameCountNully  = FromFrameCountCoalesced = value;
            }
                        
            public int ToFrameCount
            {
                get => ToFrameCountNully == ToFrameCountCoalesced ? ToFrameCountCoalesced : default;
                set => ToFrameCountNully  = ToFrameCountCoalesced = value;
            }
                        
            // SamplingRate:

            // Scales FrameCount
            
            public int? FromSamplingRateNully     { get; set; }
            public int  FromSamplingRateCoalesced { get; set; }
            public int? ToSamplingRateNully       { get; set; }
            public int  ToSamplingRateCoalesced   { get; set; }

            public int SamplingRate
            {
                get => FromSamplingRate == ToSamplingRate ? ToSamplingRate : default;
                set => FromSamplingRate  = ToSamplingRate = value;
            }

            public int FromSamplingRate
            {
                get => FromSamplingRateNully == FromSamplingRateCoalesced ? FromSamplingRateCoalesced : default;
                set => FromSamplingRateNully  = FromSamplingRateCoalesced = value;
            }
                        
            public int ToSamplingRate
            {
                get => ToSamplingRateNully == ToSamplingRateCoalesced ? ToSamplingRateCoalesced : default;
                set => ToSamplingRateNully  = ToSamplingRateCoalesced = value;
            }
                        
            // AudioLength:
            
            // Scales FrameCount + FrameCount setters adjust AudioLength.
            
            public double? FromAudioLengthNully     { get; set; }
            public double  FromAudioLengthCoalesced { get; set; }
            public double? ToAudioLengthNully       { get; set; }
            public double  ToAudioLengthCoalesced   { get; set; }

            public double AudioLength
            {
                get => FromAudioLength == ToAudioLength ? ToAudioLength : default;
                set => FromAudioLength  = ToAudioLength = value;
            }

            public double FromAudioLength
            {
                get => FromAudioLengthNully == FromAudioLengthCoalesced ? FromAudioLengthCoalesced : default;
                set => FromAudioLengthNully  = FromAudioLengthCoalesced = value;
            }
                        
            public double ToAudioLength
            {
                get => ToAudioLengthNully == ToAudioLengthCoalesced ? ToAudioLengthCoalesced : default;
                set => ToAudioLengthNully  = ToAudioLengthCoalesced = value;
            }
                        
            // CourtesyFrames:
            
            // AudioLength does not incorporate CourtesyFrames, but
            // FrameCount does.
            
            public int? FromCourtesyFramesNully     { get; set; }
            public int  FromCourtesyFramesCoalesced { get; set; }
            public int? ToCourtesyFramesNully       { get; set; }
            public int  ToCourtesyFramesCoalesced   { get; set; }

            public int CourtesyFrames
            {
                get => FromCourtesyFrames == ToCourtesyFrames ? ToCourtesyFrames : default;
                set => FromCourtesyFrames  = ToCourtesyFrames = value;
            }

            public int FromCourtesyFrames
            {
                get => FromCourtesyFramesNully == FromCourtesyFramesCoalesced ? FromCourtesyFramesCoalesced : default;
                set => FromCourtesyFramesNully  = FromCourtesyFramesCoalesced = value;
            }
            
            public int ToCourtesyFrames
            {
                get => ToCourtesyFramesNully == ToCourtesyFramesCoalesced ? ToCourtesyFramesCoalesced : default;
                set => ToCourtesyFramesNully  = ToCourtesyFramesCoalesced = value;
            }
            
            // Channels:
            
            // AudioLength vs FrameCount should be invariant under Channels,
            // but was accidentally involved in the formulas.
            
            public int? FromChannelsNully     { get; set; }
            public int  FromChannelsCoalesced { get; set; }
            public int? ToChannelsNully       { get; set; }
            public int  ToChannelsCoalesced   { get; set; }

            public int Channels
            {
                get => FromChannels == ToChannels ? ToChannels : default;
                set => FromChannels  = ToChannels = value;
            }

            public int FromChannels
            {
                get => FromChannelsNully == FromChannelsCoalesced ? FromChannelsCoalesced : default;
                set => FromChannelsNully  = FromChannelsCoalesced = value;
            }

            public int ToChannels
            {
                get => ToChannelsNully == ToChannelsCoalesced ? ToChannelsCoalesced : default;
                set => ToChannelsNully  = ToChannelsCoalesced = value;
            }
        }
