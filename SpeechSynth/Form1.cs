using Microsoft.Win32;
using SpeechSynth.Lib;
using SpeechSynth.Lib.Interfaces;
using System.Diagnostics;
using Whisper.net.Ggml;

namespace SpeechSynth
{
    public partial class Form1 : Form
    {
        private readonly IMicrophoneRecorder _micRecoder;
        private readonly ISpeechToText _speechService;
        private readonly KeyboardHook _keyboardHook;
        private readonly KeyboardSender _sender;
        private readonly Options _options;
        private bool isRecoding = false;
        private CancellationTokenSource _cancellationToken = new();
        private bool _trayClose = false;

        private Keys optionKey
        {
            get => Enum.Parse<Keys>(_options.Value.Key);
            set { _options.Value.Key = value.ToString(); }
        }

        public Form1(
            Options options,
            IMicrophoneRecorder recoder,
            ISpeechToText speechToText,
            KeyboardHook keyboardHook,
            KeyboardSender sender)
        {
            _options = options;
            _micRecoder = recoder;
            _speechService = speechToText;
            _keyboardHook = keyboardHook;
            _sender = sender;
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(
                Screen.PrimaryScreen.Bounds.Width - (this.Width + 10),
                Screen.PrimaryScreen.Bounds.Height - (this.Height + 50));

            InitView();

            _keyboardHook.KeyUp += _keyboardHook_KeyUp;
            _keyboardHook.KeyDown += _keyboardHook_KeyDown;

            Trayicon.Icon = new Icon(_options.IsLightTheme
                ? "./Images/Logo-Dark.ico"
                : "./Images/Logo-Light.ico");
            this.Icon = Trayicon.Icon;

            Trayicon.ContextMenuStrip = new ContextMenuStrip();
            Trayicon.ContextMenuStrip.Items.AddRange(new[]
            {
                new ToolStripMenuItem(
                    "Ouvrir", null, new EventHandler(OpenConfig), "Config"),
                new ToolStripMenuItem(
                    "Site web officiel", null, new EventHandler(BrowserSite), "Browse"),
                new ToolStripMenuItem(
                    "Stopper l'application", null, new EventHandler(CloseApp), "Close")
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //auto Minize on stratup calling close event handler
            if (_options.Value.StartMinimize)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }

            VerifyModel();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_trayClose)
            {
                _cancellationToken.Cancel();
                _keyboardHook.Dispose();
                _speechService.Dispose();
                _micRecoder.Dispose();
            }
            else
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _options.Save().Wait();
        }

        #region privates view functions

        private void InitView()
        {
            CbInputDevice.Items.AddRange(_micRecoder.DevicesList().ToArray());
            CBKeyboardShortcut.Items.AddRange(Enum.GetNames<Keys>());

            var idx = CbInputDevice.Items.IndexOf(_options.Value.InputDevice);
            CbInputDevice.SelectedIndex = idx > -1 ? idx : 0;

            idx = CBKeyboardShortcut.Items.IndexOf(_options.Value.Key.ToString());
            CBKeyboardShortcut.SelectedIndex = idx > -1 ? idx : 0;

            if (_options.Value.WhilePressed)
            {
                RbWhilePressed_True.Checked = true;
            }
            else
            {
                RbWhilePressed_False.Checked = true;
            }

            CBModel.Items.AddRange(Enum.GetNames<GgmlType>());
            idx = CBModel.Items.IndexOf(_options.Value.ModelSize.ToString());
            CBModel.SelectedIndex = idx > -1 ? idx : 0;

            CbStartMinimize.Checked = _options.Value.StartMinimize;
            CbStartWithWindows.Checked = _options.Value.StartWithWindows;
        }

        public void VerifyModel()
        {
            if(! _speechService.VerifyModel().Result)
            {
                Task.Run(async () =>
                {
                    this.BeginInvoke(() =>
                    {
                        textBox2.Text += Environment.NewLine + "downloading recogition model";
                        this.LbAction.Text = "downloading model ...";
                    });
                    await _speechService.DownLoadModel();
                    return _options.ClearData();
                })
                .ContinueWith(r =>
                {
                    this.BeginInvoke(() =>
                    {
                        textBox2.Text += Environment.NewLine + "download complet";
                        this.LbAction.Text = "";
                    });
                });
            }

           
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            if(_options.ClearData())
            {
                MessageBox.Show(
                    "Le dossier à été nettoyer", 
                    "Nettoyage du dossier d'application", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Une erreur est survenue durrant le nettoyage: addresse du dossier : %localappdata%\\SpeetchSynth.Net",
                    "Nettoyage du dossier d'application",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Config Events

        private void ButtonMode_CheckedChanged(object sender, EventArgs e)
        {
            _options.Value.WhilePressed = RbWhilePressed_True.Checked;
        }

        private void CBKOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbInputDevice.SelectedItem != null)
            {
                _options.Value.InputDevice = CbInputDevice.SelectedItem?.ToString() ?? "";
            }

            if (CBKeyboardShortcut.SelectedItem != null)
            {
                optionKey = Enum.Parse<Keys>(CBKeyboardShortcut.SelectedItem?.ToString() ?? "");
            }

            if (CBModel.SelectedItem != null &&
                CBModel.SelectedItem?.ToString() != _options.Value.ModelSize.ToString())
            {
                _options.Value.ModelSize = Enum.Parse<GgmlType>(CBModel.SelectedItem.ToString() ?? "Base");
                VerifyModel();
            }
        }

        private void CbStartMinimize_CheckedChanged(object sender, EventArgs e)
        {
            if (CbStartMinimize.Checked != _options.Value.StartMinimize)
            {
                _options.Value.StartMinimize = CbStartMinimize.Checked;
            }
        }

        private void CbStartWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (CbStartWithWindows.Checked != _options.Value.StartWithWindows)
            {
                _options.Value.StartWithWindows = CbStartWithWindows.Checked;

                var subKey = Registry
                    .CurrentUser
                    .OpenSubKey("Software")?
                    .OpenSubKey("Microsoft")?
                    .OpenSubKey("Windows")?
                    .OpenSubKey("CurrentVersion")?
                    .OpenSubKey("Run", true);
                if (_options.Value.StartWithWindows)
                {
                    if (subKey.GetValue("SpeechSynth") == null)
                        subKey.SetValue(
                        "SpeechSynth",
                        $"\"{Application.ExecutablePath}\"",
                        RegistryValueKind.String);
                }
                else
                {
                    if (subKey.GetValue("SpeechSynth") != null)
                        subKey.DeleteValue(
                        "SpeechSynth");
                }
            }
        }

        #endregion

        #region keys Event

        private void _keyboardHook_KeyUp(Keys key, bool Shift, bool Ctrl, bool Alt)
        {
            if (key == optionKey && isRecoding && _options.Value.WhilePressed)
            {
                LbAction.BeginInvoke(() =>
                {
                    LbAction.Text = "";
                });
                isRecoding = false;
                _ = StopRecodingAsync();
            }
        }
        private void _keyboardHook_KeyDown(Keys key, bool Shift, bool Ctrl, bool Alt)
        {
            if (key == optionKey)
            {
                if (!isRecoding)
                {
                    LbAction.BeginInvoke(() =>
                    {
                        LbAction.Text = "Listenning ...";
                    });
                    isRecoding = true;
                    _ = StartRecodingAsync();
                }
                else if (isRecoding && !_options.Value.WhilePressed)
                {
                    LbAction.BeginInvoke(() =>
                    {
                        LbAction.Text = "";
                    });
                    isRecoding = false;
                    _ = StopRecodingAsync();
                }
            }
        }

        #endregion

        #region Privates parse Function

        private Task StartRecodingAsync()
        {
            _micRecoder.StartListener(CbInputDevice.SelectedIndex);
            _ = _sender.AnimateListening();
            return Task.CompletedTask;
        }

        private async Task StopRecodingAsync()
        {
            var audioFile = await _micRecoder.StopListenerAndGetFilepath();
            LbAction.BeginInvoke(() =>
            {
                LbAction.Text = "Parsing ...";
            });

            textBox2.Invoke(() =>
            {
                textBox2.Text += Environment.NewLine + "- transcription: ";
            });
            var asyncs = _speechService.ConvertAsync(audioFile, _cancellationToken.Token);
            _sender.Endlistening();
            await foreach (var st in asyncs)
            {
                await _sender.Send(st);
                textBox2.Invoke(() =>
                {
                    textBox2.Text += st;
                });
            }

            File.Delete(audioFile);

            LbAction.BeginInvoke(() =>
            {
                LbAction.Text = "";
            });
        }

        #endregion

        #region Tray Event 

        private void BrowserSite(object? sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Coopaguard/SpeechSynth.Net/tree/master",
                UseShellExecute = true
            });
        }

        private void OpenConfig(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.Show();
            }

            // Activate the form.
            this.Activate();
            this.Focus();
        }

        private void CloseApp(object? sender, EventArgs e)
        {
            _trayClose = true;
            this.Close();
        }

        #endregion

    }
}
