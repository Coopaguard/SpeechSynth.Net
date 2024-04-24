using SpeechSynth.Lib.Interfaces;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.Compression;
using NAudio.Wave.SampleProviders;
using System.IO;
using System.Reflection.PortableExecutable;
using NAudio.CoreAudioApi;
using System.IO.Pipes;
using System.Net.NetworkInformation;

namespace SpeechSynth.Lib.Impl
{
    public class NAudioMicrophoneRecorderWaveIn : IMicrophoneRecorder
    {
        private WaveFileWriter? RecordedAudioWriter;
        private WaveInEvent? WaveInEvent;

        private Guid fileId;
        private string _filePath => Options.FilePath(fileId + ".wav");

        public NAudioMicrophoneRecorderWaveIn()
        {
        }

        public void Dispose()
        {
            RecordedAudioWriter?.Dispose();
            WaveInEvent?.Dispose();
        }

        private void CaptureInstance_RecordingStopped(object? sender, StoppedEventArgs e)
        {
            this.RecordedAudioWriter?.Dispose();
            this.RecordedAudioWriter = null;
            WaveInEvent?.Dispose();
        }

        private void CaptureInstance_DataAvailable(object? sender, WaveInEventArgs e)
        {
            this.RecordedAudioWriter?.Write(e.Buffer, 0, e.BytesRecorded);
        }

        public IEnumerable<string> DevicesList() =>
            new MMDeviceEnumerator()
            .EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)
            .Select(d => d.FriendlyName);

        public Task StartListener(int inuptIndex)
        {
            fileId = Guid.NewGuid();

            WaveInEvent = new WaveInEvent()
            {
                DeviceNumber = inuptIndex,
                WaveFormat = new WaveFormat(16000, 16, 1)
            };

            // Redefine the audio writer instance with the given configuration
            this.RecordedAudioWriter = new WaveFileWriter(_filePath, WaveInEvent.WaveFormat);

            // When the capturer receives audio, start writing the buffer into the mentioned file
            this.WaveInEvent.DataAvailable += CaptureInstance_DataAvailable; ;

            // When the Capturer Stops
            this.WaveInEvent.RecordingStopped += CaptureInstance_RecordingStopped;

            //Start the recorder
            this.WaveInEvent.StartRecording();

            return Task.CompletedTask;
        }

        public Task<Stream> StopListener()
        {
            // Stop recording !
            var st = new MemoryStream();
            this.WaveInEvent?.StopRecording();
            this.RecordedAudioWriter?.Flush();
            this.RecordedAudioWriter?.Seek(0, SeekOrigin.Begin);
            this.RecordedAudioWriter?.CopyTo(st);
            this.RecordedAudioWriter?.Close();

            //var fs = File.OpenRead(this._filePath);


            //using var reader = new WaveFileReader(fs);
            //var resampler = new WdlResamplingSampleProvider(reader.ToSampleProvider(), 16000);
            //WaveFileWriter.WriteWavFileToStream(st, resampler.ToWaveProvider16());

            // This section sets the wavStream to the beginning of the stream. (This is required because the wavStream was written to in the previous section)
            st.Seek(0, SeekOrigin.Begin);

            //fs.Close();
            File.Delete(this._filePath);

            return Task.FromResult(st as Stream);
        }

        public Task<string> StopListenerAndGetFilepath()
        {
            // Stop recording !
            this.WaveInEvent?.StopRecording();
            this.RecordedAudioWriter?.Flush();
            this.RecordedAudioWriter?.Close();
            this.WaveInEvent?.Dispose();
            return Task.FromResult(_filePath);
        }
    }
}
