        private const int DefaultHertz = DefaultSamplingRate;

            // FrameCount 0 is not nully. It means 0 seconds. Sort of, but you can't test it:
            
            // You need 3 courtesy frames to make AudioLength 0.
            // FrameCount 0 would make AudioLength -3 frames, resulting in an exception.
            //new Case ( from: (0,480+3), to: 480+3 ),
            //new Case ( from: 480+3, to: (0,480+3) ),

            // FrameCount 3 (courtesy frames) = AudioLength 0 sec.
            // But here the exception is thrown: "Duration is not above 0."
            //new Case ( from: 480+3, to: 3 ) { sec = { To = 0 } },
            //new Case ( from: 3, to: 480+3 ) { sec = { From = 0 } },
            
            // Attempt to stay just above 0. Nope, exception:
            // "Attempt to initialize FrameCount to 4 is inconsistent with FrameCount 3
            // based on initial values for AudioLength (default 1), SamplingRate (4800) and CourtesyFrames (3)."
            //new Case ( from: 4, to: 480+3 ) { sec = { From = 0 } },
            //new Case ( from: 480+3, to: 4 ) { sec = { To = 0 } },

            var zeroFramesCase = new Case
            {
                SamplingRate   = test.SamplingRate,
                Bits           = test.Bits,
                Channels       = test.Channels,
                CourtesyFrames = test.CourtesyFrames,
                FrameCount     = 0
            };

            void AssertSetter(Action setter, TestEntityEnum entity)
            {
                using (var changedEntities = CreateModifiedEntities(test, withDisk: entity == ForDestFilePath))
                {
                    binaries = changedEntities.BuffBound;
                    AssertInvariant(changedEntities, test);
                    
                    setter();
                    
                    if (entity == ForDestFilePath) AssertEntity(binaries.DestFilePath, test);
                    if (entity == ForDestBytes)    AssertEntity(binaries.DestBytes,    test);
                    if (entity == ForDestStream)   AssertEntity(binaries.DestStream,   test);
                    if (entity == ForBinaryWriter) AssertEntity(binaries.BinaryWriter, test);
                }
            }

