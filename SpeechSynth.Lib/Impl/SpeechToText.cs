using SpeechSynth.Lib.Interfaces;
using System.Runtime.CompilerServices;
using Whisper.net;
using Whisper.net.Ggml;

namespace SpeechSynth.Lib.Impl
{
    public class SpeechToText : ISpeechToText
    {
        private readonly Options _options;
        private WhisperProcessor? processor;
        private string _modelName => $"ggml-{_options.Value.ModelSize}.bin";

        public SpeechToText(Options opts)
        {
            _options = opts;
            _options.Load().Wait();
        }

        public void Dispose()
        {
            processor?.Dispose();
        }

        public Task<bool> VerifyModel() => 
            Task.FromResult(File.Exists(Options.FilePath(_modelName)));

        public async Task DownLoadModel()
        {
            if (!File.Exists(Options.FilePath(_modelName)))
            {
                using var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(GgmlType.Base);
                using var fileWriter = File.OpenWrite(Options.FilePath(_modelName));
                await modelStream.CopyToAsync(fileWriter);
            }
        }

        public async IAsyncEnumerable<string> ConvertAsync(
            Stream audioStream, 
            [EnumeratorCancellation]CancellationToken cancellationToken)
        {
            if(processor == null)
            {
                using var whisperFactory = WhisperFactory.FromPath(Options.FilePath(_modelName));
                processor = whisperFactory.CreateBuilder()
                .WithLanguage("auto")
                .Build();
            }

            await foreach (var result in processor.ProcessAsync(audioStream, cancellationToken))
            {
                yield return result.Text;
            }
        }

        public async IAsyncEnumerable<string> ConvertAsync(string audioPath, [EnumeratorCancellation]CancellationToken cancellationToken)
        {
            using var whisperFactory = WhisperFactory.FromPath(Options.FilePath(_modelName));

            // This section creates the processor object which is used to process the audio file, it uses language `auto` to detect the language of the audio file.
            using var processor = whisperFactory.CreateBuilder()
                .WithLanguage(_options.Value.ModelSize.ToString().Contains("En") ? "en" : "fr")
                .Build();

            using var fileStream = File.OpenRead(audioPath);

            // This section processes the audio file and prints the results (start time, end time and text) to the console.
            await foreach (var result in processor.ProcessAsync(fileStream))
            {
               yield return result.Text;
            }
        }

    }
}
