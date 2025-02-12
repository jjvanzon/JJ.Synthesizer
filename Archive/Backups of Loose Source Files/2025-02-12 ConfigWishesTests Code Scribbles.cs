2025-02-12 ConfigWishesTests Code Scribbles

FileExtensionWishesTests:

            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.SynthBound.SynthWishes  , () => x.SynthBound.SynthWishes.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.SynthBound.SynthWishes  , () => x.SynthBound.SynthWishes.AsWav()); 
                if (!Has(val.audioFormat) ) AreEqual(x.SynthBound.SynthWishes  ,       x.SynthBound.SynthWishes.AudioFormat(Undefined)); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw      ) AreEqual(x.SynthBound.FlowNode     , () => x.SynthBound.FlowNode   .AsRaw());
                if (val.audioFormat == Wav      ) AreEqual(x.SynthBound.FlowNode     , () => x.SynthBound.FlowNode   .AsWav());
                if (val.audioFormat == Undefined) AreEqual(x.SynthBound.FlowNode     ,       x.SynthBound.FlowNode   .AudioFormat(Undefined)); 
                if (val.audioFormat == 0        ) AreEqual(x.SynthBound.FlowNode     ,       x.SynthBound.FlowNode   .AudioFormat(0));
                if (val.audioFormat == null     ) AreEqual(x.SynthBound.FlowNode     ,       x.SynthBound.FlowNode   .AudioFormat(null));});
            
            AssertProp(x => {
                if (val.audioFormat == Raw      ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsRaw());
                if (val.audioFormat == Wav      ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsWav());
                if (val.audioFormat == Undefined) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat(Undefined));
                if (val.audioFormat == 0        ) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat(0));
                if (val.audioFormat == null     ) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat(null)); });


            
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

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .AudioFormat(val.audioFormat)));

            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.AsRaw(x.SynthBound.Context));
                if (val.audioFormat == Wav) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.AsWav(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsRaw(x.SynthBound.Context));
                if (val.audioFormat == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsWav(x.SynthBound.Context)); });

                
            AssertProp(() => {
                if (val.audioFormat == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsRaw(x.SynthBound.Context));
                if (val.audioFormat == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsWav(x.SynthBound.Context)); });

                AssertProp(() => val.audioFormat == Raw ? x.Immutable.AudioFormat.AsRaw() : x.Immutable.AudioFormat.AsWav());

                AssertProp(() => val.audioFormat.ToEntity(x.SynthBound.Context));
                AssertProp(() => val.audioFormat == Raw ? x.Immutable.AudioFormatEntity.AsRaw(x.SynthBound.Context) : x.Immutable.AudioFormatEntity.AsWav(x.SynthBound.Context));

            
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.SynthWishes   .IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.FlowNode      .IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.ConfigResolver.IsRaw());
            
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.SynthWishes   .IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.FlowNode      .IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.ConfigResolver.IsWav());
