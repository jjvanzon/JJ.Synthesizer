

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithBits(value * 8)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithBits(value * 8)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithBits(value * 8)));
            
            AssertProp(x => { switch (value) {
                case  1: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .With8Bit (     )); break;
                case  2: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .With16Bit(     )); break;
                case  4: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .With32Bit(     )); break; 
                default: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .WithBits (value)); break; } });
            
            AssertProp(x => { switch (value) {
                case  1: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .With8Bit (     )); break;
                case  2: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .With16Bit(     )); break;
                case  4: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .With32Bit(     )); break; 
                default: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .Bits     (value)); break;} });
            
            AssertProp(x => { switch (value) {
                case  1: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With8Bit (     )); break;
                case  2: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With16Bit(     )); break;
                case  4: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With32Bit(     )); break; 
                default: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With32Bit(     )); break; } });

            AssertProp(x =>                                   x.TapeBound.TapeConfig.Bits = value * 8);

            
            AssertProp(x => {
                if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With8Bit());
                if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With16Bit());
                if (value == 4) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With32Bit()); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With8Bit());
                if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With16Bit());
                if (value == 4) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With32Bit()); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With8Bit());
                if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With16Bit());
                if (value == 4) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With32Bit()); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With8Bit());
                if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With16Bit());
                if (value == 4) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With32Bit()); });

            
            AssertProp(x => {
                if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With8Bit(x.SynthBound.Context));
                if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With16Bit(x.SynthBound.Context));
                if (value == 4) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With32Bit(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With8Bit(x.SynthBound.Context));
                if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With16Bit(x.SynthBound.Context));
                if (value == 4) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With32Bit(x.SynthBound.Context)); });
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With8Bit(x.SynthBound.Context));
                    if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With16Bit(x.SynthBound.Context));
                    if (value == 4) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With32Bit(x.SynthBound.Context)); });
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Bits = value * 8);
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With8Bit());
                    if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With16Bit());
                    if (value == 4) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With32Bit()); });

                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With8Bit());
                    if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With16Bit());
                    if (value == 4) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With32Bit()); });

                
                AssertProp(() => 
                {
                    if (value == 1) return x.Immutable.WavHeader.With8Bit();
                    if (value == 2) return x.Immutable.WavHeader.With16Bit();
                    if (value == 4) return x.Immutable.WavHeader.With32Bit();
                    return default; // ncrunch: no coverage
                });

                AssertProp(() => (value * 8).BitsToEnum());
                
                AssertProp(() => 
                {
                    if (value == 1) return x.Immutable.SampleDataTypeEnum.With8Bit();
                    if (value == 2) return x.Immutable.SampleDataTypeEnum.With16Bit();
                    if (value == 4) return x.Immutable.SampleDataTypeEnum.With32Bit();
                    return default; // ncrunch: no coverage
                });
                AssertProp(() => (value * 8).BitsToEntity(x.SynthBound.Context));
                
                AssertProp(() => 
                {
                    if (value == 1) return x.Immutable.SampleDataType.With8Bit(x.SynthBound.Context);
                    if (value == 2) return x.Immutable.SampleDataType.With16Bit(x.SynthBound.Context);
                    if (value == 4) return x.Immutable.SampleDataType.With32Bit(x.SynthBound.Context);
                    return default; // ncrunch: no coverage
                });

                AssertProp(() => (value * 8).BitsToType());
                
                AssertProp(() => 
                {
                    if (value == 1) return x.Immutable.Type.With8Bit();
                    if (value == 2) return x.Immutable.Type.With16Bit();
                    if (value == 4) return x.Immutable.Type.With32Bit();
                    return default; // ncrunch: no coverage
                });

            
            AreEqual(sizeOfBitDepth, x.SynthBound.SynthWishes .Bits()  / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.SynthWishes .GetBits / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.FlowNode    .Bits()  / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.FlowNode    .GetBits / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.ConfigResolver.Bits()  / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.ConfigResolver.GetBits / 8);
            
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.SynthWishes.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.SynthWishes.Is8Bit);
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.FlowNode.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.FlowNode.Is8Bit);
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.ConfigResolver.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.ConfigResolver.Is8Bit);
            
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.SynthWishes.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.SynthWishes.Is16Bit);
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.FlowNode.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.FlowNode.Is16Bit);
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.ConfigResolver.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.ConfigResolver.Is16Bit);
            
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.SynthWishes.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.SynthWishes.Is32Bit);
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.FlowNode.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.FlowNode.Is32Bit);
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.ConfigResolver.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.ConfigResolver.Is32Bit);

            
            AreEqual(sizeOfBitDepth, x.TapeBound.Tape       .Bits() / 8);
            AreEqual(sizeOfBitDepth, x.TapeBound.TapeConfig .Bits() / 8);
            AreEqual(sizeOfBitDepth, x.TapeBound.TapeConfig .Bits   / 8);
            AreEqual(sizeOfBitDepth, x.TapeBound.TapeActions.Bits() / 8);
            AreEqual(sizeOfBitDepth, x.TapeBound.TapeAction .Bits() / 8);
            
            AreEqual(sizeOfBitDepth == 1, () => x.TapeBound.Tape.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.TapeBound.TapeConfig.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.TapeBound.TapeActions.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.TapeBound.TapeAction.Is8Bit());
        
            AreEqual(sizeOfBitDepth == 2, () => x.TapeBound.Tape.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.TapeBound.TapeConfig.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.TapeBound.TapeActions.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.TapeBound.TapeAction.Is16Bit());
        
            AreEqual(sizeOfBitDepth == 4, () => x.TapeBound.Tape.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.TapeBound.TapeConfig.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.TapeBound.TapeActions.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.TapeBound.TapeAction.Is32Bit());

            AreEqual(sizeOfBitDepth * 8,  () => sample.Bits());
            AreEqual(sizeOfBitDepth == 1, () => sample.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => sample.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => sample.Is32Bit());

            AreEqual(sizeOfBitDepth * 8,  () => audioInfoWish.Bits);
            AreEqual(sizeOfBitDepth * 8,  () => audioInfoWish.Bits());
            AreEqual(sizeOfBitDepth == 1, () => audioInfoWish.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => audioInfoWish.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => audioInfoWish.Is32Bit());

            AreEqual(sizeOfBitDepth * 8,  () => audioFileInfo.Bits());
            AreEqual(sizeOfBitDepth == 1, () => audioFileInfo.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => audioFileInfo.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => audioFileInfo.Is32Bit());

            AreEqual(sizeOfBitDepth * 8,  () => wavHeader.BitsPerValue);
            AreEqual(sizeOfBitDepth * 8,  () => wavHeader.Bits());
            AreEqual(sizeOfBitDepth == 1, () => wavHeader.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => wavHeader.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => wavHeader.Is32Bit());

            AreEqual(sizeOfBitDepth * 8,  () => sampleDataTypeEnum.Bits());
            AreEqual(sizeOfBitDepth * 8,  () => sampleDataTypeEnum.EnumToBits());
            AreEqual(sizeOfBitDepth == 1, () => sampleDataTypeEnum.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => sampleDataTypeEnum.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => sampleDataTypeEnum.Is32Bit());

            AreEqual(sizeOfBitDepth * 8,  () => sampleDataType.Bits());
            AreEqual(sizeOfBitDepth * 8,  () => sampleDataType.EntityToBits());
            AreEqual(sizeOfBitDepth == 1, () => sampleDataType.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => sampleDataType.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => sampleDataType.Is32Bit());

            AreEqual(sizeOfBitDepth * 8,  () => type.Bits());
            AreEqual(sizeOfBitDepth * 8,  () => type.TypeToBits());
            AreEqual(sizeOfBitDepth == 1, () => type.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => type.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => type.Is32Bit());

            
            AreEqual(sizeOfBitDepth * 8, () => x.BuffBound.Buff.Bits());
            AreEqual(sizeOfBitDepth * 8, () => x.BuffBound.AudioFileOutput.Bits());
            
            AreEqual(sizeOfBitDepth == 1, () => x.BuffBound.Buff.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.BuffBound.AudioFileOutput.Is8Bit());
            
            AreEqual(sizeOfBitDepth == 2, () => x.BuffBound.Buff.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.BuffBound.AudioFileOutput.Is16Bit());
            
            AreEqual(sizeOfBitDepth == 4, () => x.BuffBound.Buff.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.BuffBound.AudioFileOutput.Is32Bit());
