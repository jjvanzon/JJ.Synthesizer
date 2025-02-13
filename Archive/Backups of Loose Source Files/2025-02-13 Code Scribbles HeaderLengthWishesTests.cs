
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithAudioFormat(val.audioFormat)));
            
            AssertProp(x => { switch (val.audioFormat) {
                case Raw: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .AsRaw()); break;
                case Wav: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .AsWav()); break;
                default : AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .WithAudioFormat(val.audioFormat)); break; } });
                                                                
            AssertProp(x => { switch (val.audioFormat) {        
                case Raw: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .AsRaw()); break;
                case Wav: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .AsWav()); break;
                default : AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .AudioFormat(val.audioFormat)); break; } });
            
            AssertProp(x => { switch (val.audioFormat) {
                case Raw: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsRaw()); break;
                case Wav: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsWav()); break;
                default : AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AudioFormat(val.audioFormat)); break; } });

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.AudioFormat(val.audioFormat)));
            AssertProp(x =>                                         x.TapeBound.TapeConfig.AudioFormat = val.audioFormat);
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.AudioFormat(val.audioFormat)));
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.AsWav()); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.AsWav()); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsWav()); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.AsWav()); });

            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.AsRaw(x.SynthBound.Context));
                if (val.audioFormat == Wav) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.AsWav(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsRaw(x.SynthBound.Context));
                if (val.audioFormat == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsWav(x.SynthBound.Context)); });

                
                AssertProp(() => {
                    if (val.audioFormat == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsRaw(x.SynthBound.Context));
                    if (val.audioFormat == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsWav(x.SynthBound.Context)); });

                AssertProp(() => val.audioFormat.AudioFormat());
                AssertProp(() => val.audioFormat == Raw ? x.Immutable.AudioFormat.AsRaw() : x.Immutable.AudioFormat.AsWav());


                AssertProp(() => val.headerLength.AudioFormat              (x.SynthBound.Context));
                AssertProp(() => val.headerLength.ToAudioFormat            (x.SynthBound.Context));
                AssertProp(() => val.headerLength.HeaderLengthToAudioFormat(x.SynthBound.Context));
                AssertProp(() => AudioFormat                           (val.headerLength, x.SynthBound.Context));
                AssertProp(() => ToAudioFormat                         (val.headerLength, x.SynthBound.Context));
                AssertProp(() => HeaderLengthToAudioFormat             (val.headerLength, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.AudioFormat              (val.headerLength, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.ToAudioFormat            (val.headerLength, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.HeaderLengthToAudioFormat(val.headerLength, x.SynthBound.Context));

                AssertProp(() => x.Immutable.AudioFormatEntity.AudioFormat(val.audioFormat, x.SynthBound.Context));
                AssertProp(() => val.audioFormat.ToEntity(x.SynthBound.Context));
                AssertProp(() => val.audioFormat == Raw ? x.Immutable.AudioFormatEntity.AsRaw(x.SynthBound.Context) : x.Immutable.AudioFormatEntity.AsWav(x.SynthBound.Context));

            
            AreEqual(headerLength == 0, () => x.SynthBound.SynthWishes.IsRaw());
            AreEqual(headerLength == 0, () => x.SynthBound.SynthWishes.IsRaw);
            AreEqual(headerLength == 0, () => x.SynthBound.FlowNode.IsRaw());
            AreEqual(headerLength == 0, () => x.SynthBound.FlowNode.IsRaw);
            AreEqual(headerLength == 0, () => x.SynthBound.ConfigResolver.IsRaw());
            AreEqual(headerLength == 0, () => x.SynthBound.ConfigResolver.IsRaw);
            
            AreEqual(headerLength == 44, () => x.SynthBound.SynthWishes.IsWav());
            AreEqual(headerLength == 44, () => x.SynthBound.SynthWishes.IsWav);
            AreEqual(headerLength == 44, () => x.SynthBound.FlowNode.IsWav());
            AreEqual(headerLength == 44, () => x.SynthBound.FlowNode.IsWav);
            AreEqual(headerLength == 44, () => x.SynthBound.ConfigResolver.IsWav());
            AreEqual(headerLength == 44, () => x.SynthBound.ConfigResolver.IsWav);

            
            AreEqual(headerLength == 0, () => x.TapeBound.Tape.IsRaw());
            AreEqual(headerLength == 0, () => x.TapeBound.TapeConfig.IsRaw());
            AreEqual(headerLength == 0, () => x.TapeBound.TapeActions.IsRaw());
            AreEqual(headerLength == 0, () => x.TapeBound.TapeAction.IsRaw());
        
            AreEqual(headerLength == 44, () => x.TapeBound.Tape.IsWav());
            AreEqual(headerLength == 44, () => x.TapeBound.TapeConfig.IsWav());
            AreEqual(headerLength == 44, () => x.TapeBound.TapeActions.IsWav());
            AreEqual(headerLength == 44, () => x.TapeBound.TapeAction.IsWav());

            
            AreEqual(headerLength == 0, () => x.BuffBound.Buff.IsRaw());
            AreEqual(headerLength == 0, () => x.BuffBound.AudioFileOutput.IsRaw());
            
            AreEqual(headerLength == 44, () => x.BuffBound.Buff.IsWav());
            AreEqual(headerLength == 44, () => x.BuffBound.AudioFileOutput.IsWav());

            AreEqual(headerLength == 0, () => audioFileFormatEnum.IsRaw());
            AreEqual(headerLength == 44, () => audioFileFormatEnum.IsWav());

            AreEqual(headerLength == 0,  () => audioFormatEntity.IsRaw());
            AreEqual(headerLength == 44, () => audioFormatEntity.IsWav());
