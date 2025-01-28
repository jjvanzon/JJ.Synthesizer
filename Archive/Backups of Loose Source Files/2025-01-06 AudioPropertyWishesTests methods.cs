// Bits Getters

    [TestMethod] public void Bits_Getters_Normal()
    {
        Bits_Getters_Normal(8);
        Bits_Getters_Normal(16);
        Bits_Getters_Normal(32);
    }
    void Bits_Getters_Normal(int bits)
    {
        var x = new TestEntities(bits);
        x.GlobalBound_Bits_Equal(DefaultBits);
        x.All_Bits_Equal(bits);
    }

    [TestMethod] public void Bits_Getters_8BitShorthand()
    {
        var x = new TestEntities(bits: 8);

        // Global-Bound
        IsFalse(() => x.ConfigSection.Is8Bit());

        // SynthWishes-Bound
        IsTrue(() => x.SynthWishes.Is8Bit());
        IsTrue(() => x.FlowNode.Is8Bit());
        IsTrue(() => x.ConfigWishes.Is8Bit());

        // Tape-Bound
        IsTrue(() => x.Tape.Is8Bit());
        IsTrue(() => x.TapeConfig.Is8Bit());
        IsTrue(() => x.TapeActions.Is8Bit());
        IsTrue(() => x.TapeAction.Is8Bit());
        
        // Buff-Bound
        IsTrue(() => x.Buff.Is8Bit());
        IsTrue(() => x.AudioFileOutput.Is8Bit());
        
        // Independent after Taping
        IsTrue(() => x.Sample.Is8Bit());
        IsTrue(() => x.WavHeader.Is8Bit());
        IsTrue(() => x.AudioInfoWish.Is8Bit());
        IsTrue(() => x.AudioFileInfo.Is8Bit());
        
        // Immutable
        IsTrue(() => x.SampleDataTypeEnum.Is8Bit());
        IsTrue(() => x.SampleDataType.Is8Bit());
        IsTrue(() => x.Type.Is8Bit());
    }

    [TestMethod] public void Bits_Getters_16BitShorthand()
    {
        var x = new TestEntities(bits: 16);
        
        // Global-Bound
        IsFalse(() => x.ConfigSection.Is16Bit());

        // SynthWishes-Bound
        IsTrue(() => x.SynthWishes.Is16Bit());
        IsTrue(() => x.FlowNode.Is16Bit());
        IsTrue(() => x.ConfigWishes.Is16Bit());

        // Tape-Bound
        IsTrue(() => x.Tape.Is16Bit());
        IsTrue(() => x.TapeConfig.Is16Bit());
        IsTrue(() => x.TapeActions.Is16Bit());
        IsTrue(() => x.TapeAction.Is16Bit());

        // Buff-Bound
        IsTrue(() => x.Buff.Is16Bit());
        IsTrue(() => x.AudioFileOutput.Is16Bit());

        // Independent after Taping
        IsTrue(() => x.Sample.Is16Bit());
        IsTrue(() => x.WavHeader.Is16Bit());
        IsTrue(() => x.AudioInfoWish.Is16Bit());
        IsTrue(() => x.AudioFileInfo.Is16Bit());

        // Immutable
        IsTrue(() => x.SampleDataTypeEnum.Is16Bit());
        IsTrue(() => x.SampleDataType.Is16Bit());
        IsTrue(() => x.Type.Is16Bit());
    }
            
    [TestMethod] public void Bits_Getters_32BitShorthand()
    {
        var x = new TestEntities(bits: 32);
        
        // Global-Bound
        IsTrue(() => x.ConfigSection.Is32Bit());

        // SynthWishes-Bound
        IsTrue(() => x.SynthWishes.Is32Bit());
        IsTrue(() => x.FlowNode.Is32Bit());
        IsTrue(() => x.ConfigWishes.Is32Bit());

        // Tape-Bound
        IsTrue(() => x.Tape.Is32Bit());
        IsTrue(() => x.TapeConfig.Is32Bit());
        IsTrue(() => x.TapeActions.Is32Bit());
        IsTrue(() => x.TapeAction.Is32Bit());

        // Buff-Bound
        IsTrue(() => x.Buff.Is32Bit());
        IsTrue(() => x.AudioFileOutput.Is32Bit());

        // Independent after Taping
        IsTrue(() => x.Sample.Is32Bit());
        IsTrue(() => x.WavHeader.Is32Bit());
        IsTrue(() => x.AudioInfoWish.Is32Bit());
        IsTrue(() => x.AudioFileInfo.Is32Bit());

        // Immutable
        IsTrue(() => x.SampleDataTypeEnum.Is32Bit());
        IsTrue(() => x.SampleDataType.Is32Bit());
        IsTrue(() => x.Type.Is32Bit());
    }

// Bits Setters

    [TestMethod]
    public void Bits_Setters_Normal_ShallowTest()
    {
        Bits_Setters_Normal_ShallowTest(32, 8);
        Bits_Setters_Normal_ShallowTest(32, 16);
        Bits_Setters_Normal_ShallowTest(16, 32);
    }

    void Bits_Setters_Normal_ShallowTest(int from, int to)
    {
        var x = new TestEntities(from);

        // SynthWishes-Bound
        AreEqual(x.SynthWishes, () => x.SynthWishes.Bits(to));
        AreEqual(x.FlowNode, () => x.FlowNode.Bits(to));
        AreEqual(x.ConfigWishes, () => x.ConfigWishes.Bits(to));

        // Tape-Bound
        AreEqual(x.Tape, () => x.Tape.Bits(to));
        AreEqual(x.TapeConfig, () => x.TapeConfig.Bits(to));
        AreEqual(x.TapeActions, () => x.TapeActions.Bits(to));
        AreEqual(x.TapeAction, () => x.TapeAction.Bits(to));

        // Buff-Bound
        AreEqual(x.Buff, () => x.Buff.Bits(to, x.Context));
        AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Bits(to, x.Context));

        // Independent after Taping
        AreEqual(x.Sample, () => x.Sample.Bits(to, x.Context));
        AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.Bits(to));
        AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.Bits(to));

        // Immutable
        NotEqual(x.WavHeader, () => x.WavHeader.Bits(to));
        NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.Bits(to));
        NotEqual(x.SampleDataType, () => x.SampleDataType.Bits(to, x.Context));
        NotEqual(x.Type, () => x.Type.Bits(to));
    }

    [TestMethod] public void Bits_Setters_8Bit_Shorthand()
    {
        { // Check Before Change
            
            var x = new TestEntities(bits: 32);
            
            // SynthWishes-Bound
            IsFalse(() => x.SynthWishes.Is8Bit());
            IsFalse(() => x.FlowNode.Is8Bit());
            IsFalse(() => x.ConfigWishes.Is8Bit());
            
            // Tape-Bound
            IsFalse(() => x.Tape.Is8Bit());
            IsFalse(() => x.TapeConfig.Is8Bit());
            IsFalse(() => x.TapeActions.Is8Bit());
            IsFalse(() => x.TapeAction.Is8Bit());
            
            // Buff-Bound
            IsFalse(() => x.Buff.Is8Bit());
            IsFalse(() => x.AudioFileOutput.Is8Bit());
            
            // Independent after Taping
            IsFalse(() => x.Sample.Is8Bit());
            IsFalse(() => x.AudioInfoWish.Is8Bit());
            IsFalse(() => x.AudioFileInfo.Is8Bit());
            
            // Immutable
            IsFalse(() => x.WavHeader.Is8Bit());
            IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
            IsFalse(() => x.SampleDataType.Is8Bit());
            IsFalse(() => x.Type.Is8Bit());
        }
        
        { // SynthWishes-Bound Change
            
            var x = new TestEntities(bits: 32);
            
            { // Change-Check
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is8Bit());
                IsFalse(() => x.FlowNode.Is8Bit());
                IsFalse(() => x.ConfigWishes.Is8Bit());
                
                // Change!
                x.ConfigWishes.With8Bit();
                
                // SynthWishes-Bound
                IsTrue(() => x.SynthWishes.Is8Bit());
                IsTrue(() => x.FlowNode.Is8Bit());
                IsTrue(() => x.ConfigWishes.Is8Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is8Bit());
                IsFalse(() => x.TapeConfig.Is8Bit());
                IsFalse(() => x.TapeActions.Is8Bit());
                IsFalse(() => x.TapeAction.Is8Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is8Bit());
                IsFalse(() => x.AudioFileOutput.Is8Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is8Bit());
                IsFalse(() => x.AudioInfoWish.Is8Bit());
                IsFalse(() => x.AudioFileInfo.Is8Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is8Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                IsFalse(() => x.SampleDataType.Is8Bit());
                IsFalse(() => x.Type.Is8Bit());
            }
            
            { // After-Record
                
                x.Record();
                
                // SynthWishes-Bound
                IsTrue(() => x.SynthWishes.Is8Bit());
                IsTrue(() => x.FlowNode.Is8Bit());
                IsTrue(() => x.ConfigWishes.Is8Bit());
                
                // Tape-Bound
                IsTrue(() => x.Tape.Is8Bit());
                IsTrue(() => x.TapeConfig.Is8Bit());
                IsTrue(() => x.TapeActions.Is8Bit());
                IsTrue(() => x.TapeAction.Is8Bit());
                
                // Buff-Bound
                IsTrue(() => x.Buff.Is8Bit());
                IsTrue(() => x.AudioFileOutput.Is8Bit());
                
                // Independent after Taping
                IsTrue(() => x.Sample.Is8Bit());
                IsTrue(() => x.AudioInfoWish.Is8Bit());
                IsTrue(() => x.AudioFileInfo.Is8Bit());
                
                // Immutable
                IsTrue(() => x.WavHeader.Is8Bit());
                IsTrue(() => x.SampleDataTypeEnum.Is8Bit());
                IsTrue(() => x.SampleDataType.Is8Bit());
                IsTrue(() => x.Type.Is8Bit());
            }
        }
        
        { // Tape-Bound Change
            
            var x = new TestEntities(bits: 32);
            
            { // Change-Check
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is8Bit());
                IsFalse(() => x.FlowNode.Is8Bit());
                IsFalse(() => x.ConfigWishes.Is8Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is8Bit());
                IsFalse(() => x.TapeConfig.Is8Bit());
                IsFalse(() => x.TapeActions.Is8Bit());
                IsFalse(() => x.TapeAction.Is8Bit());
                
                // Change!
                x.TapeAction.With8Bit();
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is8Bit());
                IsFalse(() => x.FlowNode.Is8Bit());
                IsFalse(() => x.ConfigWishes.Is8Bit());
                
                // Tape-Bound
                IsTrue(() => x.Tape.Is8Bit());
                IsTrue(() => x.TapeConfig.Is8Bit());
                IsTrue(() => x.TapeActions.Is8Bit());
                IsTrue(() => x.TapeAction.Is8Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is8Bit());
                IsFalse(() => x.AudioFileOutput.Is8Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is8Bit());
                IsFalse(() => x.AudioInfoWish.Is8Bit());
                IsFalse(() => x.AudioFileInfo.Is8Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is8Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                IsFalse(() => x.SampleDataType.Is8Bit());
                IsFalse(() => x.Type.Is8Bit());
            }
            
            { // After-Record
                
                x.Record();
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is8Bit());
                IsFalse(() => x.FlowNode.Is8Bit());
                IsFalse(() => x.ConfigWishes.Is8Bit());
                
                // By Design: Currently can't re-record over the same tape.
                // So you always get a new tape, overwriting the changed values upon record.
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is8Bit());
                IsFalse(() => x.TapeConfig.Is8Bit());
                IsFalse(() => x.TapeActions.Is8Bit());
                IsFalse(() => x.TapeAction.Is8Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is8Bit());
                IsFalse(() => x.AudioFileOutput.Is8Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is8Bit());
                IsFalse(() => x.AudioInfoWish.Is8Bit());
                IsFalse(() => x.AudioFileInfo.Is8Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is8Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                IsFalse(() => x.SampleDataType.Is8Bit());
                IsFalse(() => x.Type.Is8Bit());
            }
        }
        
        { // Buff-Bound Change
            
            var x = new TestEntities(bits: 32);
            
            { // Change-Check
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is8Bit());
                IsFalse(() => x.FlowNode.Is8Bit());
                IsFalse(() => x.ConfigWishes.Is8Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is8Bit());
                IsFalse(() => x.TapeConfig.Is8Bit());
                IsFalse(() => x.TapeActions.Is8Bit());
                IsFalse(() => x.TapeAction.Is8Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is8Bit());
                IsFalse(() => x.AudioFileOutput.Is8Bit());
                
                // Change!
                x.AudioFileOutput.With8Bit(x.Context);
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is8Bit());
                IsFalse(() => x.FlowNode.Is8Bit());
                IsFalse(() => x.ConfigWishes.Is8Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is8Bit());
                IsFalse(() => x.TapeConfig.Is8Bit());
                IsFalse(() => x.TapeActions.Is8Bit());
                IsFalse(() => x.TapeAction.Is8Bit());
                
                // Buff-Bound
                IsTrue(() => x.Buff.Is8Bit());
                IsTrue(() => x.AudioFileOutput.Is8Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is8Bit());
                IsFalse(() => x.AudioInfoWish.Is8Bit());
                IsFalse(() => x.AudioFileInfo.Is8Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is8Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                IsFalse(() => x.SampleDataType.Is8Bit());
                IsFalse(() => x.Type.Is8Bit());
            }
            
            { // After Record
                
                x.Record();
                
                // Overwritten with original SynthWishes settings.
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is8Bit());
                IsFalse(() => x.FlowNode.Is8Bit());
                IsFalse(() => x.ConfigWishes.Is8Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is8Bit());
                IsFalse(() => x.TapeConfig.Is8Bit());
                IsFalse(() => x.TapeActions.Is8Bit());
                IsFalse(() => x.TapeAction.Is8Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is8Bit());
                IsFalse(() => x.AudioFileOutput.Is8Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is8Bit());
                IsFalse(() => x.AudioInfoWish.Is8Bit());
                IsFalse(() => x.AudioFileInfo.Is8Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is8Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                IsFalse(() => x.SampleDataType.Is8Bit());
                IsFalse(() => x.Type.Is8Bit());
            }
        }
    }

    [TestMethod] public void Bits_Setters_16Bit_Shorthand()
    {
        { // Check Before Change
            
            var x = new TestEntities(bits: 32);
            
            // SynthWishes-Bound
            IsFalse(() => x.SynthWishes.Is16Bit());
            IsFalse(() => x.FlowNode.Is16Bit());
            IsFalse(() => x.ConfigWishes.Is16Bit());
            
            // Tape-Bound
            IsFalse(() => x.Tape.Is16Bit());
            IsFalse(() => x.TapeConfig.Is16Bit());
            IsFalse(() => x.TapeActions.Is16Bit());
            IsFalse(() => x.TapeAction.Is16Bit());
            
            // Buff-Bound
            IsFalse(() => x.Buff.Is16Bit());
            IsFalse(() => x.AudioFileOutput.Is16Bit());
            
            // Independent after Taping
            IsFalse(() => x.Sample.Is16Bit());
            IsFalse(() => x.AudioInfoWish.Is16Bit());
            IsFalse(() => x.AudioFileInfo.Is16Bit());
            
            // Immutable
            IsFalse(() => x.WavHeader.Is16Bit());
            IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
            IsFalse(() => x.SampleDataType.Is16Bit());
            IsFalse(() => x.Type.Is16Bit());
        }
        
        { // SynthWishes-Bound Change
            
            var x = new TestEntities(bits: 32);
            
            { // Change-Check
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is16Bit());
                IsFalse(() => x.FlowNode.Is16Bit());
                IsFalse(() => x.ConfigWishes.Is16Bit());
                
                // Change!
                x.ConfigWishes.With16Bit();
                
                // SynthWishes-Bound
                IsTrue(() => x.SynthWishes.Is16Bit());
                IsTrue(() => x.FlowNode.Is16Bit());
                IsTrue(() => x.ConfigWishes.Is16Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is16Bit());
                IsFalse(() => x.TapeConfig.Is16Bit());
                IsFalse(() => x.TapeActions.Is16Bit());
                IsFalse(() => x.TapeAction.Is16Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is16Bit());
                IsFalse(() => x.AudioFileOutput.Is16Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is16Bit());
                IsFalse(() => x.AudioInfoWish.Is16Bit());
                IsFalse(() => x.AudioFileInfo.Is16Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is16Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                IsFalse(() => x.SampleDataType.Is16Bit());
                IsFalse(() => x.Type.Is16Bit());
            }
            
            { // After-Record
                
                x.Record();
                
                // SynthWishes-Bound
                IsTrue(() => x.SynthWishes.Is16Bit());
                IsTrue(() => x.FlowNode.Is16Bit());
                IsTrue(() => x.ConfigWishes.Is16Bit());
                
                // Tape-Bound
                IsTrue(() => x.Tape.Is16Bit());
                IsTrue(() => x.TapeConfig.Is16Bit());
                IsTrue(() => x.TapeActions.Is16Bit());
                IsTrue(() => x.TapeAction.Is16Bit());
                
                // Buff-Bound
                IsTrue(() => x.Buff.Is16Bit());
                IsTrue(() => x.AudioFileOutput.Is16Bit());
                
                // Independent after Taping
                IsTrue(() => x.Sample.Is16Bit());
                IsTrue(() => x.AudioInfoWish.Is16Bit());
                IsTrue(() => x.AudioFileInfo.Is16Bit());
                
                // Immutable
                IsTrue(() => x.WavHeader.Is16Bit());
                IsTrue(() => x.SampleDataTypeEnum.Is16Bit());
                IsTrue(() => x.SampleDataType.Is16Bit());
                IsTrue(() => x.Type.Is16Bit());
            }
        }
        
        { // Tape-Bound Change
            
            var x = new TestEntities(bits: 32);
            
            { // Change-Check
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is16Bit());
                IsFalse(() => x.FlowNode.Is16Bit());
                IsFalse(() => x.ConfigWishes.Is16Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is16Bit());
                IsFalse(() => x.TapeConfig.Is16Bit());
                IsFalse(() => x.TapeActions.Is16Bit());
                IsFalse(() => x.TapeAction.Is16Bit());
                
                // Change!
                x.TapeAction.With16Bit();
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is16Bit());
                IsFalse(() => x.FlowNode.Is16Bit());
                IsFalse(() => x.ConfigWishes.Is16Bit());
                
                // Tape-Bound
                IsTrue(() => x.Tape.Is16Bit());
                IsTrue(() => x.TapeConfig.Is16Bit());
                IsTrue(() => x.TapeActions.Is16Bit());
                IsTrue(() => x.TapeAction.Is16Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is16Bit());
                IsFalse(() => x.AudioFileOutput.Is16Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is16Bit());
                IsFalse(() => x.AudioInfoWish.Is16Bit());
                IsFalse(() => x.AudioFileInfo.Is16Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is16Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                IsFalse(() => x.SampleDataType.Is16Bit());
                IsFalse(() => x.Type.Is16Bit());
            }
            
            { // After-Record
                
                x.Record();
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is16Bit());
                IsFalse(() => x.FlowNode.Is16Bit());
                IsFalse(() => x.ConfigWishes.Is16Bit());
                
                // By Design: Currently can't re-record over the same tape.
                // So you always get a new tape, overwriting the changed values upon record.
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is16Bit());
                IsFalse(() => x.TapeConfig.Is16Bit());
                IsFalse(() => x.TapeActions.Is16Bit());
                IsFalse(() => x.TapeAction.Is16Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is16Bit());
                IsFalse(() => x.AudioFileOutput.Is16Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is16Bit());
                IsFalse(() => x.AudioInfoWish.Is16Bit());
                IsFalse(() => x.AudioFileInfo.Is16Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is16Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                IsFalse(() => x.SampleDataType.Is16Bit());
                IsFalse(() => x.Type.Is16Bit());
            }
        }
        
        { // Buff-Bound Change
            
            var x = new TestEntities(bits: 32);
            
            { // Change-Check
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is16Bit());
                IsFalse(() => x.FlowNode.Is16Bit());
                IsFalse(() => x.ConfigWishes.Is16Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is16Bit());
                IsFalse(() => x.TapeConfig.Is16Bit());
                IsFalse(() => x.TapeActions.Is16Bit());
                IsFalse(() => x.TapeAction.Is16Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is16Bit());
                IsFalse(() => x.AudioFileOutput.Is16Bit());
                
                // Change!
                x.AudioFileOutput.With16Bit(x.Context);
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is16Bit());
                IsFalse(() => x.FlowNode.Is16Bit());
                IsFalse(() => x.ConfigWishes.Is16Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is16Bit());
                IsFalse(() => x.TapeConfig.Is16Bit());
                IsFalse(() => x.TapeActions.Is16Bit());
                IsFalse(() => x.TapeAction.Is16Bit());
                
                // Buff-Bound
                IsTrue(() => x.Buff.Is16Bit());
                IsTrue(() => x.AudioFileOutput.Is16Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is16Bit());
                IsFalse(() => x.AudioInfoWish.Is16Bit());
                IsFalse(() => x.AudioFileInfo.Is16Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is16Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                IsFalse(() => x.SampleDataType.Is16Bit());
                IsFalse(() => x.Type.Is16Bit());
            }
            
            { // After Record
                
                x.Record();
                
                // Overwritten with original SynthWishes settings.
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is16Bit());
                IsFalse(() => x.FlowNode.Is16Bit());
                IsFalse(() => x.ConfigWishes.Is16Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is16Bit());
                IsFalse(() => x.TapeConfig.Is16Bit());
                IsFalse(() => x.TapeActions.Is16Bit());
                IsFalse(() => x.TapeAction.Is16Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is16Bit());
                IsFalse(() => x.AudioFileOutput.Is16Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is16Bit());
                IsFalse(() => x.AudioInfoWish.Is16Bit());
                IsFalse(() => x.AudioFileInfo.Is16Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is16Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                IsFalse(() => x.SampleDataType.Is16Bit());
                IsFalse(() => x.Type.Is16Bit());
            }
        }
    }

    [TestMethod] public void Bits_Setters_32Bit_Shorthand()
    {
        { // Check Before Change
            
            var x = new TestEntities(bits: 16);
            
            // SynthWishes-Bound
            IsFalse(() => x.SynthWishes.Is32Bit());
            IsFalse(() => x.FlowNode.Is32Bit());
            IsFalse(() => x.ConfigWishes.Is32Bit());
            
            // Tape-Bound
            IsFalse(() => x.Tape.Is32Bit());
            IsFalse(() => x.TapeConfig.Is32Bit());
            IsFalse(() => x.TapeActions.Is32Bit());
            IsFalse(() => x.TapeAction.Is32Bit());
            
            // Buff-Bound
            IsFalse(() => x.Buff.Is32Bit());
            IsFalse(() => x.AudioFileOutput.Is32Bit());
            
            // Independent after Taping
            IsFalse(() => x.Sample.Is32Bit());
            IsFalse(() => x.AudioInfoWish.Is32Bit());
            IsFalse(() => x.AudioFileInfo.Is32Bit());
            
            // Immutable
            IsFalse(() => x.WavHeader.Is32Bit());
            IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
            IsFalse(() => x.SampleDataType.Is32Bit());
            IsFalse(() => x.Type.Is32Bit());
        }
        
        { // SynthWishes-Bound Change
            
            var x = new TestEntities(bits: 16);
            
            { // Change-Check
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is32Bit());
                IsFalse(() => x.FlowNode.Is32Bit());
                IsFalse(() => x.ConfigWishes.Is32Bit());
                
                // Change!
                x.ConfigWishes.With32Bit();
                
                // SynthWishes-Bound
                IsTrue(() => x.SynthWishes.Is32Bit());
                IsTrue(() => x.FlowNode.Is32Bit());
                IsTrue(() => x.ConfigWishes.Is32Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is32Bit());
                IsFalse(() => x.TapeConfig.Is32Bit());
                IsFalse(() => x.TapeActions.Is32Bit());
                IsFalse(() => x.TapeAction.Is32Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is32Bit());
                IsFalse(() => x.AudioFileOutput.Is32Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is32Bit());
                IsFalse(() => x.AudioInfoWish.Is32Bit());
                IsFalse(() => x.AudioFileInfo.Is32Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is32Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                IsFalse(() => x.SampleDataType.Is32Bit());
                IsFalse(() => x.Type.Is32Bit());
            }
            
            { // After-Record
                
                x.Record();
                
                // SynthWishes-Bound
                IsTrue(() => x.SynthWishes.Is32Bit());
                IsTrue(() => x.FlowNode.Is32Bit());
                IsTrue(() => x.ConfigWishes.Is32Bit());
                
                // Tape-Bound
                IsTrue(() => x.Tape.Is32Bit());
                IsTrue(() => x.TapeConfig.Is32Bit());
                IsTrue(() => x.TapeActions.Is32Bit());
                IsTrue(() => x.TapeAction.Is32Bit());
                
                // Buff-Bound
                IsTrue(() => x.Buff.Is32Bit());
                IsTrue(() => x.AudioFileOutput.Is32Bit());
                
                // Independent after Taping
                IsTrue(() => x.Sample.Is32Bit());
                IsTrue(() => x.AudioInfoWish.Is32Bit());
                IsTrue(() => x.AudioFileInfo.Is32Bit());
                
                // Immutable
                IsTrue(() => x.WavHeader.Is32Bit());
                IsTrue(() => x.SampleDataTypeEnum.Is32Bit());
                IsTrue(() => x.SampleDataType.Is32Bit());
                IsTrue(() => x.Type.Is32Bit());
            }
        }
        
        { // Tape-Bound Change
            
            var x = new TestEntities(bits: 16);
            
            { // Change-Check
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is32Bit());
                IsFalse(() => x.FlowNode.Is32Bit());
                IsFalse(() => x.ConfigWishes.Is32Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is32Bit());
                IsFalse(() => x.TapeConfig.Is32Bit());
                IsFalse(() => x.TapeActions.Is32Bit());
                IsFalse(() => x.TapeAction.Is32Bit());
                
                // Change!
                x.TapeAction.With32Bit();
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is32Bit());
                IsFalse(() => x.FlowNode.Is32Bit());
                IsFalse(() => x.ConfigWishes.Is32Bit());
                
                // Tape-Bound
                IsTrue(() => x.Tape.Is32Bit());
                IsTrue(() => x.TapeConfig.Is32Bit());
                IsTrue(() => x.TapeActions.Is32Bit());
                IsTrue(() => x.TapeAction.Is32Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is32Bit());
                IsFalse(() => x.AudioFileOutput.Is32Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is32Bit());
                IsFalse(() => x.AudioInfoWish.Is32Bit());
                IsFalse(() => x.AudioFileInfo.Is32Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is32Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                IsFalse(() => x.SampleDataType.Is32Bit());
                IsFalse(() => x.Type.Is32Bit());
            }
            
            { // After-Record
                
                x.Record();
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is32Bit());
                IsFalse(() => x.FlowNode.Is32Bit());
                IsFalse(() => x.ConfigWishes.Is32Bit());
                
                // By Design: Currently can't re-record over the same tape.
                // So you always get a new tape, overwriting the changed values upon record.
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is32Bit());
                IsFalse(() => x.TapeConfig.Is32Bit());
                IsFalse(() => x.TapeActions.Is32Bit());
                IsFalse(() => x.TapeAction.Is32Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is32Bit());
                IsFalse(() => x.AudioFileOutput.Is32Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is32Bit());
                IsFalse(() => x.AudioInfoWish.Is32Bit());
                IsFalse(() => x.AudioFileInfo.Is32Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is32Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                IsFalse(() => x.SampleDataType.Is32Bit());
                IsFalse(() => x.Type.Is32Bit());
            }
        }
        
        { // Buff-Bound Change
            
            var x = new TestEntities(bits: 16);
            
            { // Change-Check
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is32Bit());
                IsFalse(() => x.FlowNode.Is32Bit());
                IsFalse(() => x.ConfigWishes.Is32Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is32Bit());
                IsFalse(() => x.TapeConfig.Is32Bit());
                IsFalse(() => x.TapeActions.Is32Bit());
                IsFalse(() => x.TapeAction.Is32Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is32Bit());
                IsFalse(() => x.AudioFileOutput.Is32Bit());
                
                // Change!
                x.AudioFileOutput.With32Bit(x.Context);
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is32Bit());
                IsFalse(() => x.FlowNode.Is32Bit());
                IsFalse(() => x.ConfigWishes.Is32Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is32Bit());
                IsFalse(() => x.TapeConfig.Is32Bit());
                IsFalse(() => x.TapeActions.Is32Bit());
                IsFalse(() => x.TapeAction.Is32Bit());
                
                // Buff-Bound
                IsTrue(() => x.Buff.Is32Bit());
                IsTrue(() => x.AudioFileOutput.Is32Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is32Bit());
                IsFalse(() => x.AudioInfoWish.Is32Bit());
                IsFalse(() => x.AudioFileInfo.Is32Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is32Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                IsFalse(() => x.SampleDataType.Is32Bit());
                IsFalse(() => x.Type.Is32Bit());
            }
            
            { // After Record
                
                x.Record();
                
                // Overwritten with original SynthWishes settings.
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is32Bit());
                IsFalse(() => x.FlowNode.Is32Bit());
                IsFalse(() => x.ConfigWishes.Is32Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is32Bit());
                IsFalse(() => x.TapeConfig.Is32Bit());
                IsFalse(() => x.TapeActions.Is32Bit());
                IsFalse(() => x.TapeAction.Is32Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is32Bit());
                IsFalse(() => x.AudioFileOutput.Is32Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is32Bit());
                IsFalse(() => x.AudioInfoWish.Is32Bit());
                IsFalse(() => x.AudioFileInfo.Is32Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is32Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                IsFalse(() => x.SampleDataType.Is32Bit());
                IsFalse(() => x.Type.Is32Bit());
            }
        }
    }

// Bit Setters - Shorthand

    [TestMethod]
    public void Bits_Setters_8Bit_Shorthand_ShallowTest()
    {
        var x = new TestEntities(bits: 32);

        // SynthWishes-Bound
        AreEqual(x.SynthWishes, () => x.SynthWishes.With8Bit());
        AreEqual(x.FlowNode, () => x.FlowNode.With8Bit());
        AreEqual(x.ConfigWishes, () => x.ConfigWishes.With8Bit());

        // Tape-Bound
        AreEqual(x.Tape, () => x.Tape.With8Bit());
        AreEqual(x.TapeConfig, () => x.TapeConfig.With8Bit());
        AreEqual(x.TapeActions, () => x.TapeActions.With8Bit());
        AreEqual(x.TapeAction, () => x.TapeAction.With8Bit());

        // Buff-Bound
        AreEqual(x.Buff, () => x.Buff.With8Bit(x.Context));
        AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With8Bit(x.Context));

        // Independent after Taping
        AreEqual(x.Sample, () => x.Sample.With8Bit(x.Context));
        AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With8Bit());
        AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With8Bit());

        // Immutable
        NotEqual(x.WavHeader, () => x.WavHeader.With8Bit());
        NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.With8Bit());
        NotEqual(x.SampleDataType, () => x.SampleDataType.With8Bit(x.Context));
        NotEqual(x.Type, () => x.Type.With8Bit());
    }

    [TestMethod]
    public void Bits_Setters_16Bit_Shorthand_ShallowTest()
    {
        var x = new TestEntities(bits: 32);

        // SynthWishes-Bound
        AreEqual(x.SynthWishes, () => x.SynthWishes.With16Bit());
        AreEqual(x.FlowNode, () => x.FlowNode.With16Bit());
        AreEqual(x.ConfigWishes, () => x.ConfigWishes.With16Bit());

        // Tape-Bound
        AreEqual(x.Tape, () => x.Tape.With16Bit());
        AreEqual(x.TapeConfig, () => x.TapeConfig.With16Bit());
        AreEqual(x.TapeActions, () => x.TapeActions.With16Bit());
        AreEqual(x.TapeAction, () => x.TapeAction.With16Bit());

        // Buff-Bound
        AreEqual(x.Buff, () => x.Buff.With16Bit(x.Context));
        AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With16Bit(x.Context));

        // Independent after Taping
        AreEqual(x.Sample, () => x.Sample.With16Bit(x.Context));
        AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With16Bit());
        AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With16Bit());

        // Immutable
        NotEqual(x.WavHeader, () => x.WavHeader.With16Bit());
        NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.With16Bit());
        NotEqual(x.SampleDataType, () => x.SampleDataType.With16Bit(x.Context));
        NotEqual(x.Type, () => x.Type.With16Bit());
    }

    [TestMethod]
    public void Bits_Setters_32Bit_Shorthand_ShallowTest()
    {
        var x = new TestEntities(bits: 16);

        // SynthWishes-Bound
        AreEqual(x.SynthWishes, () => x.SynthWishes.With32Bit());
        AreEqual(x.FlowNode, () => x.FlowNode.With32Bit());
        AreEqual(x.ConfigWishes, () => x.ConfigWishes.With32Bit());

        // Tape-Bound
        AreEqual(x.Tape, () => x.Tape.With32Bit());
        AreEqual(x.TapeConfig, () => x.TapeConfig.With32Bit());
        AreEqual(x.TapeActions, () => x.TapeActions.With32Bit());
        AreEqual(x.TapeAction, () => x.TapeAction.With32Bit());

        // Buff-Bound
        AreEqual(x.Buff, () => x.Buff.With32Bit(x.Context));
        AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With32Bit(x.Context));

        // Independent after Taping
        AreEqual(x.Sample, () => x.Sample.With32Bit(x.Context));
        AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With32Bit());
        AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With32Bit());

        // Immutable
        NotEqual(x.WavHeader, () => x.WavHeader.With32Bit());
        NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.With32Bit());
        NotEqual(x.SampleDataType, () => x.SampleDataType.With32Bit(x.Context));
        NotEqual(x.Type, () => x.Type.With32Bit());
    }

// Immutables 8-Bit Shorthand Methods

    [TestMethod] public void Bits_Immutables_8Bit_Shorthand()
    {
        var init = 32;
        var value = 8;
        var x = new TestEntities(init);

        // Immutable                        
        x.WavHeader.Assert_Bits(init);
        var wavHeaderAfter = x.WavHeader.With8Bit();
        x.WavHeader.Assert_Bits(init);
        wavHeaderAfter.Assert_Bits(value);

        x.SampleDataTypeEnum.Assert_Bits(init);
        var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With8Bit();
        x.SampleDataTypeEnum.Assert_Bits(init);
        sampleDataTypeEnumAfter.Assert_Bits(value);

        x.SampleDataType.Assert_Bits(init);
        var sampleDataTypeAfter = x.SampleDataType.With8Bit(x.Context);
        x.SampleDataType.Assert_Bits(init);
        sampleDataTypeAfter.Assert_Bits(value);

        x.Type.Assert_Bits(init);
        var typeAfter = x.Type.With8Bit();
        x.Type.Assert_Bits(init);
        typeAfter.Assert_Bits(value);

        // After Record
        x.Record();
        
        // All is reset
        x.All_Bits_Equal(init);

        // Except for our variables
        wavHeaderAfter.Assert_Bits(value);
        sampleDataTypeEnumAfter.Assert_Bits(value);
        sampleDataTypeAfter.Assert_Bits(value);
        typeAfter.Assert_Bits(value);
    }
            
    [TestMethod] public void Bits_Immutables_16Bit_Shorthand()
    {
        var init = 32;
        var value = 16;
        var x = new TestEntities(init);

        // Immutable                        
        x.WavHeader.Assert_Bits(init);
        var wavHeaderAfter = x.WavHeader.With16Bit();
        x.WavHeader.Assert_Bits(init);
        wavHeaderAfter.Assert_Bits(value);
        
        x.SampleDataTypeEnum.Assert_Bits(init);
        var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With16Bit();
        x.SampleDataTypeEnum.Assert_Bits(init);
        sampleDataTypeEnumAfter.Assert_Bits(value);

        x.SampleDataType.Assert_Bits(init);
        var sampleDataTypeAfter = x.SampleDataType.With16Bit(x.Context);
        x.SampleDataType.Assert_Bits(init);
        sampleDataTypeAfter.Assert_Bits(value);

        x.Type.Assert_Bits(init);
        var typeAfter = x.Type.With16Bit();
        x.Type.Assert_Bits(init);
        typeAfter.Assert_Bits(value);

        // After Record
        x.Record();
        
        // All is reset
        x.All_Bits_Equal(init);

        // Except for our variables
        wavHeaderAfter.Assert_Bits(value);
        sampleDataTypeEnumAfter.Assert_Bits(value);
        sampleDataTypeAfter.Assert_Bits(value);
        typeAfter.Assert_Bits(value);
    }
            
    [TestMethod] public void Bits_Immutables_32Bit_Shorthand()
    {
        var init = 16;
        var value = 32;
        var x = new TestEntities(init);

        // Immutable                        
        x.WavHeader.Assert_Bits(init);
        var wavHeaderAfter = x.WavHeader.With32Bit();
        x.WavHeader.Assert_Bits(init);
        wavHeaderAfter.Assert_Bits(value);
        
        x.SampleDataTypeEnum.Assert_Bits(init);
        var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With32Bit();
        x.SampleDataTypeEnum.Assert_Bits(init);
        sampleDataTypeEnumAfter.Assert_Bits(value);

        x.SampleDataType.Assert_Bits(init);
        var sampleDataTypeAfter = x.SampleDataType.With32Bit(x.Context);
        x.SampleDataType.Assert_Bits(init);
        sampleDataTypeAfter.Assert_Bits(value);

        x.Type.Assert_Bits(init);
        var typeAfter = x.Type.With32Bit();
        x.Type.Assert_Bits(init);
        typeAfter.Assert_Bits(value);

        // After Record
        x.Record();
        
        // All is reset
        x.All_Bits_Equal(init);

        // Except for our variables
        wavHeaderAfter.Assert_Bits(value);
        sampleDataTypeEnumAfter.Assert_Bits(value);
        sampleDataTypeAfter.Assert_Bits(value);
        typeAfter.Assert_Bits(value);
    }
