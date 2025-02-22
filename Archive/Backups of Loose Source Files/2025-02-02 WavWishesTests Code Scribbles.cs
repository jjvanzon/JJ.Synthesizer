            AssertWrite(bin => AreEqual(x.BuffBound.Buff           , () => x.BuffBound.Buff             .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.BuffBound.Buff           , () => x.BuffBound.Buff             .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.BuffBound.Buff           , () => x.BuffBound.Buff             .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.BuffBound.Buff           , () => x.BuffBound.Buff             .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput  .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput  .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput  .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput  .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test); // By Design: explicit FrameCount() call omitted; would fall between two internal WriteWavHeader calls.
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.BuffBound.Buff                           )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.BuffBound.Buff                           )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.BuffBound.Buff                           )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.BuffBound.Buff                           )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.BuffBound.AudioFileOutput                )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.BuffBound.AudioFileOutput                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.BuffBound.AudioFileOutput                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.BuffBound.AudioFileOutput                )), ForBinaryWriter, test); // By Design: explicit FrameCount() call omitted; would fall between two internal WriteWavHeader calls.
            AssertWrite(bin => AreEqual(x.BuffBound.Buff,            () => WriteWavHeader(x.BuffBound.Buff,              bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.BuffBound.Buff,            () => WriteWavHeader(x.BuffBound.Buff,              bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.BuffBound.Buff,            () => WriteWavHeader(x.BuffBound.Buff,              bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.BuffBound.Buff,            () => WriteWavHeader(x.BuffBound.Buff,              bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => WriteWavHeader(x.BuffBound.AudioFileOutput,   bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => WriteWavHeader(x.BuffBound.AudioFileOutput,   bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => WriteWavHeader(x.BuffBound.AudioFileOutput,   bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => WriteWavHeader(x.BuffBound.AudioFileOutput,   bin.BinaryWriter             )), ForBinaryWriter, test); // By Design: explicit FrameCount() call omitted; would fall between two internal WriteWavHeader calls.
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.BuffBound.Buff                          )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.BuffBound.Buff                          )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.BuffBound.Buff                          )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.BuffBound.Buff                          )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.BuffBound.AudioFileOutput               )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.BuffBound.AudioFileOutput               )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.BuffBound.AudioFileOutput               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.BuffBound.AudioFileOutput               )), ForBinaryWriter, test); // By Design: explicit FrameCount() call omitted; would fall between two internal WriteWavHeader calls.
            AssertWrite(bin => AreEqual(x.BuffBound.Buff,            () => WavWishes.WriteWavHeader(x.BuffBound.Buff,              bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.BuffBound.Buff,            () => WavWishes.WriteWavHeader(x.BuffBound.Buff,              bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.BuffBound.Buff,            () => WavWishes.WriteWavHeader(x.BuffBound.Buff,              bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.BuffBound.Buff,            () => WavWishes.WriteWavHeader(x.BuffBound.Buff,              bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => WavWishes.WriteWavHeader(x.BuffBound.AudioFileOutput,   bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => WavWishes.WriteWavHeader(x.BuffBound.AudioFileOutput,   bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => WavWishes.WriteWavHeader(x.BuffBound.AudioFileOutput,   bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.BuffBound.AudioFileOutput, () => WavWishes.WriteWavHeader(x.BuffBound.AudioFileOutput,   bin.BinaryWriter)), ForBinaryWriter, test); // By Design: explicit FrameCount() call omitted; would fall between two internal WriteWavHeader calls.
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.BuffBound.Buff             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.BuffBound.Buff             )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.BuffBound.Buff             )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.BuffBound.Buff             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.BuffBound.AudioFileOutput  )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.BuffBound.AudioFileOutput  )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.BuffBound.AudioFileOutput  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.BuffBound.AudioFileOutput  )), ForBinaryWriter, test); // By Design: explicit FrameCount() call omitted; would fall between two internal WriteWavHeader calls.
