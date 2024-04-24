using Microsoft.Win32;
using System.Text.Json;
using Whisper.net.Ggml;

namespace SpeechSynth.Lib
{
    public  class Options
    {
        private const string appsettings = "appsettings.json";
        private const string solutionFolder = "SpeechSynth.Net";

        private static readonly JsonSerializerOptions jOpts = new () 
        { 
            WriteIndented = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        /// <summary>
        /// Options Values
        /// </summary>
        public SpeechSynthOption Value = new();


        public async Task Load(CancellationToken cancellationToken = default)
        {
            VerifyDirectory();

            if(File.Exists(FilePath(appsettings)))
            {
                var st = await File.ReadAllTextAsync(FilePath(appsettings), cancellationToken);
                Value = JsonSerializer.Deserialize<SpeechSynthOption>(st) ?? new SpeechSynthOption();
            }
        }

        public Task Save()
        {
            VerifyDirectory();

            var opts = JsonSerializer.Serialize(
                Value,
                jOpts);

            File.WriteAllText(FilePath(appsettings), opts);
            return Task.CompletedTask;
        }

        public static string FilePath(string fileName)
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                solutionFolder,
                fileName);
        }


        public bool IsLightTheme =>
            (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", 1) 
            == 1;
            

        #region private

        private static void VerifyDirectory()
        {
            var dir = new DirectoryInfo(FilePath(""));

            if(!dir.Exists)
            {
                dir.Create();
            }
        }

        public bool ClearData()
        {
            try
            {
                var directory = new DirectoryInfo(FilePath(""));
                var files = directory.GetFiles().ToList();

                files.RemoveAll(f => f.Name == appsettings);
                files.RemoveAll(f => f.Name == $"ggml-{Value.ModelSize}.bin");

                files.ForEach(f =>
                {
                    if (f.Exists)
                    {
                        f.Delete();
                    }
                });
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        #endregion
    }


    public class SpeechSynthOption
    {
        public GgmlType ModelSize { get; set; } = GgmlType.Base;

        public string Key { get; set; } = "F2";

        public string InputDevice { get; set; } = "";

        public bool WhilePressed { get; set; } = true;

        public bool StartMinimize { get; set; } = false;

        public bool StartWithWindows {  get; set; } = false;
    }
}
