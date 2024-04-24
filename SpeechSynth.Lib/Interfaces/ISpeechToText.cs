

namespace SpeechSynth.Lib.Interfaces
{
    public interface ISpeechToText : IDisposable
    {
        public IAsyncEnumerable<string> ConvertAsync(
            Stream audioStream,
            CancellationToken cancellationToken);

        public IAsyncEnumerable<string> ConvertAsync(
            string audioPath,
            CancellationToken cancellationToken);

        public Task<bool> VerifyModel();

        public Task DownLoadModel();
    }
}
