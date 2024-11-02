
    public class SaveAudioResultData
    {
        public SaveAudioResultData(
            AudioFileOutput audioFileOutput,
            TimeSpan calculationTimeSpan,
            TimeSpan totalCalculationTimeSpan, 
            byte[] bytes = null)
        {
            AudioFileOutput = audioFileOutput ?? throw new ArgumentNullException(nameof(audioFileOutput));
            Bytes = bytes;
            Diagnostics = new SaveAudioDiagnostics(this, calculationTimeSpan, totalCalculationTimeSpan);
        }

        public AudioFileOutput AudioFileOutput { get; }

        /// <summary>
        /// Can be null. Only supplied when writeToMemory is true.
        /// </summary>
        public byte[] Bytes { get; }

        public TimeSpan CalculationTimeSpan { get; }

        public SaveAudioDiagnostics Diagnostics { get; }
    }

    public class SaveAudioDiagnostics
    {
        internal SaveAudioDiagnostics(
            SaveAudioResultData saveAudioResultData,
            TimeSpan calculationTimeSpan, 
            TimeSpan totalCalculationTimeSpan)
        {
            if (saveAudioResultData == null) throw new NullException(() => saveAudioResultData);
            CalculationTimeSpan = calculationTimeSpan;
            TotalCalculationTimeSpan = totalCalculationTimeSpan;
        }

        /// <summary>
        /// The time it took for the sound to be calculated,
        /// regardless of waiting for dependent parallels to finish.
        /// </summary>
        public TimeSpan CalculationTimeSpan { get; }
        
        /// <summary>
        /// In case of parallel processing, counts up all the time it took
        /// For all the parallels to finish executing.
        /// </summary>
        public TimeSpan TotalCalculationTimeSpan { get; }
    }
