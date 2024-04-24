namespace SpeechSynth
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            textBox2 = new TextBox();
            LbAction = new Label();
            LbSource = new Label();
            CbInputDevice = new ComboBox();
            LbShortCut = new Label();
            CBKeyboardShortcut = new ComboBox();
            RbWhilePressed_True = new RadioButton();
            panel1 = new Panel();
            RbWhilePressed_False = new RadioButton();
            CBModel = new ComboBox();
            LbModel = new Label();
            Trayicon = new NotifyIcon(components);
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            BtnClear = new Button();
            CbStartWithWindows = new CheckBox();
            CbStartMinimize = new CheckBox();
            tabPage2 = new TabPage();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.BackColor = SystemColors.ControlLightLight;
            textBox2.Location = new Point(3, 2);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(297, 297);
            textBox2.TabIndex = 0;
            // 
            // LbAction
            // 
            LbAction.AutoSize = true;
            LbAction.Location = new Point(12, 11);
            LbAction.Name = "LbAction";
            LbAction.Size = new Size(0, 15);
            LbAction.TabIndex = 1;
            // 
            // LbSource
            // 
            LbSource.AutoSize = true;
            LbSource.Location = new Point(41, 84);
            LbSource.Name = "LbSource";
            LbSource.Size = new Size(79, 15);
            LbSource.TabIndex = 2;
            LbSource.Text = "Micro source:";
            // 
            // CbInputDevice
            // 
            CbInputDevice.FormattingEnabled = true;
            CbInputDevice.Location = new Point(126, 81);
            CbInputDevice.Name = "CbInputDevice";
            CbInputDevice.Size = new Size(177, 23);
            CbInputDevice.TabIndex = 3;
            CbInputDevice.SelectedIndexChanged += CBKOptions_SelectedIndexChanged;
            // 
            // LbShortCut
            // 
            LbShortCut.AutoSize = true;
            LbShortCut.Location = new Point(12, 114);
            LbShortCut.Name = "LbShortCut";
            LbShortCut.Size = new Size(93, 15);
            LbShortCut.TabIndex = 4;
            LbShortCut.Text = "Racourci clavier:";
            // 
            // CBKeyboardShortcut
            // 
            CBKeyboardShortcut.FormattingEnabled = true;
            CBKeyboardShortcut.Location = new Point(126, 110);
            CBKeyboardShortcut.Name = "CBKeyboardShortcut";
            CBKeyboardShortcut.Size = new Size(177, 23);
            CBKeyboardShortcut.TabIndex = 5;
            CBKeyboardShortcut.SelectedIndexChanged += CBKOptions_SelectedIndexChanged;
            // 
            // RbWhilePressed_True
            // 
            RbWhilePressed_True.AutoSize = true;
            RbWhilePressed_True.Location = new Point(6, 5);
            RbWhilePressed_True.Name = "RbWhilePressed_True";
            RbWhilePressed_True.Size = new Size(128, 19);
            RbWhilePressed_True.TabIndex = 6;
            RbWhilePressed_True.TabStop = true;
            RbWhilePressed_True.Text = "Maintenir la touche";
            RbWhilePressed_True.UseVisualStyleBackColor = true;
            RbWhilePressed_True.CheckedChanged += ButtonMode_CheckedChanged;
            // 
            // panel1
            // 
            panel1.BackgroundImageLayout = ImageLayout.None;
            panel1.Controls.Add(RbWhilePressed_False);
            panel1.Controls.Add(RbWhilePressed_True);
            panel1.Location = new Point(166, 11);
            panel1.Name = "panel1";
            panel1.Size = new Size(137, 57);
            panel1.TabIndex = 7;
            // 
            // RbWhilePressed_False
            // 
            RbWhilePressed_False.AutoSize = true;
            RbWhilePressed_False.Location = new Point(6, 35);
            RbWhilePressed_False.Name = "RbWhilePressed_False";
            RbWhilePressed_False.Size = new Size(124, 19);
            RbWhilePressed_False.TabIndex = 7;
            RbWhilePressed_False.TabStop = true;
            RbWhilePressed_False.Text = "Bascule par appuie";
            RbWhilePressed_False.UseVisualStyleBackColor = true;
            RbWhilePressed_False.CheckedChanged += ButtonMode_CheckedChanged;
            // 
            // CBModel
            // 
            CBModel.FormattingEnabled = true;
            CBModel.Location = new Point(126, 167);
            CBModel.Name = "CBModel";
            CBModel.Size = new Size(177, 23);
            CBModel.TabIndex = 8;
            CBModel.SelectedIndexChanged += CBKOptions_SelectedIndexChanged;
            // 
            // LbModel
            // 
            LbModel.AutoSize = true;
            LbModel.Location = new Point(12, 149);
            LbModel.Name = "LbModel";
            LbModel.Size = new Size(260, 15);
            LbModel.TabIndex = 9;
            LbModel.Text = "Taille du model (affecte la précision et la vitesse)";
            // 
            // Trayicon
            // 
            Trayicon.Text = "SpeechSynth.Net";
            Trayicon.Visible = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(8, 8);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(317, 327);
            tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(BtnClear);
            tabPage1.Controls.Add(CbStartWithWindows);
            tabPage1.Controls.Add(CbStartMinimize);
            tabPage1.Controls.Add(LbAction);
            tabPage1.Controls.Add(LbModel);
            tabPage1.Controls.Add(CBModel);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(CBKeyboardShortcut);
            tabPage1.Controls.Add(LbSource);
            tabPage1.Controls.Add(LbShortCut);
            tabPage1.Controls.Add(CbInputDevice);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(309, 299);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Configuration";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // BtnClear
            // 
            BtnClear.Location = new Point(126, 196);
            BtnClear.Name = "BtnClear";
            BtnClear.Size = new Size(177, 23);
            BtnClear.TabIndex = 14;
            BtnClear.Text = "Effacer les données inutiles";
            BtnClear.UseVisualStyleBackColor = true;
            BtnClear.Click += BtnClear_Click;
            // 
            // CbStartWithWindows
            // 
            CbStartWithWindows.AutoSize = true;
            CbStartWithWindows.Location = new Point(12, 274);
            CbStartWithWindows.Name = "CbStartWithWindows";
            CbStartWithWindows.Size = new Size(281, 19);
            CbStartWithWindows.TabIndex = 13;
            CbStartWithWindows.Text = "Démarrer automatique à l'ouverture de windows";
            CbStartWithWindows.UseVisualStyleBackColor = true;
            CbStartWithWindows.CheckedChanged += CbStartWithWindows_CheckedChanged;
            // 
            // CbStartMinimize
            // 
            CbStartMinimize.AutoSize = true;
            CbStartMinimize.Location = new Point(12, 239);
            CbStartMinimize.Name = "CbStartMinimize";
            CbStartMinimize.Size = new Size(236, 19);
            CbStartMinimize.TabIndex = 12;
            CbStartMinimize.Text = "Démarrage réduit dans la barre de tâche";
            CbStartMinimize.UseVisualStyleBackColor = true;
            CbStartMinimize.CheckedChanged += CbStartMinimize_CheckedChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(textBox2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(309, 299);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Logs";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(324, 340);
            Controls.Add(tabControl1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "SpeechToText Administration";
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBox2;
        private Label LbAction;
        private Label LbSource;
        private ComboBox CbInputDevice;
        private Label LbShortCut;
        private ComboBox CBKeyboardShortcut;
        private RadioButton RbWhilePressed_True;
        private Panel panel1;
        private RadioButton RbWhilePressed_False;
        private ComboBox CBModel;
        private Label LbModel;
        private NotifyIcon Trayicon;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private CheckBox CbStartWithWindows;
        private CheckBox CbStartMinimize;
        private Button BtnClear;
    }
}
