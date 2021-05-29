namespace JJ.Business.Synthesizer.Helpers
{
    internal static class WavHeaderConstants
    {
        public const int WAV_HEADER_LENGTH = 44;
        public const int BIG_ENDIAN_ASCII_CHARACTERS_RIFF = 0x46464952;
        public const int BIG_ENDIAN_ASCII_CHARACTERS_WAVE = 0x45564157;
        public const int BIG_ENDIAN_ASCII_CHARACTERS_FMT_SPACE_LOWERCASE = 0x20746D66;
        public const int BIG_ENDIAN_ASCII_CHARACTERS_DATA_LOWERCASE = 0x61746164;
        public const short AUDIO_FORMAT_INDICATOR_PCM = 1;
        public const int SUB_CHUNK_1_SIZE = 16;
    }
}
