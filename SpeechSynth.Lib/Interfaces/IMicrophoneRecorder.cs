
namespace SpeechSynth.Lib.Interfaces
{
    public interface IMicrophoneRecorder : IDisposable
    {
        public Task StartListener(int inuptIndex);

        public Task<Stream> StopListener();

        public Task<string> StopListenerAndGetFilepath();

        public IEnumerable<string> DevicesList();
    }
}
