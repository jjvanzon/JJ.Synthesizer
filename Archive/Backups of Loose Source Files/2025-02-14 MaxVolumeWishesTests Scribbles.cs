                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.WavHeader.With8Bit();
                    if (val.bits == 16) return x.Immutable.WavHeader.With16Bit();
                    if (val.bits == 32) return x.Immutable.WavHeader.With32Bit();
                    return default; // ncrunch: no coverage
                });

                AssertProp(() => val.bits.BitsToEnum());
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.SampleDataTypeEnum.With8Bit();
                    if (val.bits == 16) return x.Immutable.SampleDataTypeEnum.With16Bit();
                    if (val.bits == 32) return x.Immutable.SampleDataTypeEnum.With32Bit();
                    return default; // ncrunch: no coverage
                });

                AssertProp(() => val.bits.BitsToEntity(x.SynthBound.Context));
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.SampleDataType.With8Bit(x.SynthBound.Context);
                    if (val.bits == 16) return x.Immutable.SampleDataType.With16Bit(x.SynthBound.Context);
                    if (val.bits == 32) return x.Immutable.SampleDataType.With32Bit(x.SynthBound.Context);
                    return default; // ncrunch: no coverage
                });

                AssertProp(() => val.bits.BitsToType());
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.Type.With8Bit();
                    if (val.bits == 16) return x.Immutable.Type.With16Bit();
                    if (val.bits == 32) return x.Immutable.Type.With32Bit();
                    return default; // ncrunch: no coverage
                });


            // int (Bits)

            var bitsList = new List<int>();
            {
                void AssertProp(Func<int> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init.maxAmplitude);

                    int bits2 = setter();

                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init.maxAmplitude);
                    Assert_Immutable_Getters(bits2, val.maxAmplitude);

                    sampleDataTypeEnums.Add(bits2);
                }

                AssertProp(() => x.Immutable.Bits.SetBits(val.bits));
            }
