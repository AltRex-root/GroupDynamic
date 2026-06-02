namespace GroupDynamic;

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
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        newToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem = new ToolStripMenuItem();
        saveToolStripMenuItem = new ToolStripMenuItem();
        saveAsToolStripMenuItem = new ToolStripMenuItem();
        closeTabToolStripMenuItem = new ToolStripMenuItem();
        exitToolStripMenuItem = new ToolStripMenuItem();
        settingsToolStripMenuItem = new ToolStripMenuItem();
        themeToolStripMenuItem = new ToolStripMenuItem();
        lightThemeToolStripMenuItem = new ToolStripMenuItem();
        darkThemeToolStripMenuItem = new ToolStripMenuItem();
        systemThemeToolStripMenuItem = new ToolStripMenuItem();
        topPanel = new Panel();
        clearSelectionButton = new Button();
        findFromStartButton = new Button();
        saveButton = new Button();
        openButton = new Button();
        regexCheckBox = new CheckBox();
        wholeWordCheckBox = new CheckBox();
        caseSensitiveCheckBox = new CheckBox();
        findNextButton = new Button();
        searchTextBox = new TextBox();
        searchLabel = new Label();
        documentTabControl = new TabControl();
        statusStrip1 = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        editorInfoLabel = new ToolStripStatusLabel();
        openFileDialog = new OpenFileDialog();
        saveFileDialog = new SaveFileDialog();
        menuStrip1.SuspendLayout();
        topPanel.SuspendLayout();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(1184, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, closeTabToolStripMenuItem, exitToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(48, 20);
        fileToolStripMenuItem.Text = "Файл";
        // 
        // newToolStripMenuItem
        // 
        newToolStripMenuItem.Name = "newToolStripMenuItem";
        newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
        newToolStripMenuItem.Size = new Size(198, 22);
        newToolStripMenuItem.Text = "Новый";
        newToolStripMenuItem.Click += newToolStripMenuItem_Click;
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
        openToolStripMenuItem.Size = new Size(198, 22);
        openToolStripMenuItem.Text = "Открыть";
        openToolStripMenuItem.Click += openToolStripMenuItem_Click;
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
        saveToolStripMenuItem.Size = new Size(198, 22);
        saveToolStripMenuItem.Text = "Сохранить";
        saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
        saveAsToolStripMenuItem.Size = new Size(198, 22);
        saveAsToolStripMenuItem.Text = "Сохранить как";
        saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
        // 
        // closeTabToolStripMenuItem
        // 
        closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
        closeTabToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;
        closeTabToolStripMenuItem.Size = new Size(198, 22);
        closeTabToolStripMenuItem.Text = "Закрыть вкладку";
        closeTabToolStripMenuItem.Click += closeTabToolStripMenuItem_Click;
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new Size(198, 22);
        exitToolStripMenuItem.Text = "Выход";
        exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
        // 
        // settingsToolStripMenuItem
        // 
        settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { themeToolStripMenuItem });
        settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        settingsToolStripMenuItem.Size = new Size(79, 20);
        settingsToolStripMenuItem.Text = "Настройки";
        // 
        // themeToolStripMenuItem
        // 
        themeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lightThemeToolStripMenuItem, darkThemeToolStripMenuItem, systemThemeToolStripMenuItem });
        themeToolStripMenuItem.Name = "themeToolStripMenuItem";
        themeToolStripMenuItem.Size = new Size(180, 22);
        themeToolStripMenuItem.Text = "Тема";
        // 
        // lightThemeToolStripMenuItem
        // 
        lightThemeToolStripMenuItem.Name = "lightThemeToolStripMenuItem";
        lightThemeToolStripMenuItem.Size = new Size(139, 22);
        lightThemeToolStripMenuItem.Text = "Светлая";
        lightThemeToolStripMenuItem.Click += lightThemeToolStripMenuItem_Click;
        // 
        // darkThemeToolStripMenuItem
        // 
        darkThemeToolStripMenuItem.Name = "darkThemeToolStripMenuItem";
        darkThemeToolStripMenuItem.Size = new Size(139, 22);
        darkThemeToolStripMenuItem.Text = "Тёмная";
        darkThemeToolStripMenuItem.Click += darkThemeToolStripMenuItem_Click;
        // 
        // systemThemeToolStripMenuItem
        // 
        systemThemeToolStripMenuItem.Name = "systemThemeToolStripMenuItem";
        systemThemeToolStripMenuItem.Size = new Size(139, 22);
        systemThemeToolStripMenuItem.Text = "Системная";
        systemThemeToolStripMenuItem.Click += systemThemeToolStripMenuItem_Click;
        // 
        // topPanel
        // 
        topPanel.Controls.Add(clearSelectionButton);
        topPanel.Controls.Add(findFromStartButton);
        topPanel.Controls.Add(saveButton);
        topPanel.Controls.Add(openButton);
        topPanel.Controls.Add(regexCheckBox);
        topPanel.Controls.Add(wholeWordCheckBox);
        topPanel.Controls.Add(caseSensitiveCheckBox);
        topPanel.Controls.Add(findNextButton);
        topPanel.Controls.Add(searchTextBox);
        topPanel.Controls.Add(searchLabel);
        topPanel.Dock = DockStyle.Top;
        topPanel.Location = new Point(0, 24);
        topPanel.Name = "topPanel";
        topPanel.Size = new Size(1184, 70);
        topPanel.TabIndex = 1;
        // 
        // clearSelectionButton
        // 
        clearSelectionButton.Location = new Point(1034, 21);
        clearSelectionButton.Name = "clearSelectionButton";
        clearSelectionButton.Size = new Size(126, 28);
        clearSelectionButton.TabIndex = 9;
        clearSelectionButton.Text = "Снять выделение";
        clearSelectionButton.UseVisualStyleBackColor = true;
        clearSelectionButton.Click += clearSelectionButton_Click;
        // 
        // findFromStartButton
        // 
        findFromStartButton.Location = new Point(919, 21);
        findFromStartButton.Name = "findFromStartButton";
        findFromStartButton.Size = new Size(100, 28);
        findFromStartButton.TabIndex = 8;
        findFromStartButton.Text = "Искать сначала";
        findFromStartButton.UseVisualStyleBackColor = true;
        findFromStartButton.Click += findFromStartButton_Click;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(117, 21);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(92, 28);
        saveButton.TabIndex = 7;
        saveButton.Text = "Сохранить";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
        // 
        // openButton
        // 
        openButton.Location = new Point(16, 21);
        openButton.Name = "openButton";
        openButton.Size = new Size(92, 28);
        openButton.TabIndex = 6;
        openButton.Text = "Открыть";
        openButton.UseVisualStyleBackColor = true;
        openButton.Click += openButton_Click;
        // 
        // regexCheckBox
        // 
        regexCheckBox.AutoSize = true;
        regexCheckBox.Location = new Point(769, 26);
        regexCheckBox.Name = "regexCheckBox";
        regexCheckBox.Size = new Size(61, 19);
        regexCheckBox.TabIndex = 5;
        regexCheckBox.Text = "Regex";
        regexCheckBox.UseVisualStyleBackColor = true;
        // 
        // wholeWordCheckBox
        // 
        wholeWordCheckBox.AutoSize = true;
        wholeWordCheckBox.Location = new Point(653, 26);
        wholeWordCheckBox.Name = "wholeWordCheckBox";
        wholeWordCheckBox.Size = new Size(99, 19);
        wholeWordCheckBox.TabIndex = 4;
        wholeWordCheckBox.Text = "Целое слово";
        wholeWordCheckBox.UseVisualStyleBackColor = true;
        // 
        // caseSensitiveCheckBox
        // 
        caseSensitiveCheckBox.AutoSize = true;
        caseSensitiveCheckBox.Location = new Point(511, 26);
        caseSensitiveCheckBox.Name = "caseSensitiveCheckBox";
        caseSensitiveCheckBox.Size = new Size(126, 19);
        caseSensitiveCheckBox.TabIndex = 3;
        caseSensitiveCheckBox.Text = "Учитывать регистр";
        caseSensitiveCheckBox.UseVisualStyleBackColor = true;
        // 
        // findNextButton
        // 
        findNextButton.Location = new Point(836, 21);
        findNextButton.Name = "findNextButton";
        findNextButton.Size = new Size(73, 28);
        findNextButton.TabIndex = 2;
        findNextButton.Text = "Дальше";
        findNextButton.UseVisualStyleBackColor = true;
        findNextButton.Click += findNextButton_Click;
        // 
        // searchTextBox
        // 
        searchTextBox.Location = new Point(278, 23);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.Size = new Size(214, 23);
        searchTextBox.TabIndex = 1;
        searchTextBox.KeyDown += searchTextBox_KeyDown;
        // 
        // searchLabel
        // 
        searchLabel.AutoSize = true;
        searchLabel.Location = new Point(225, 27);
        searchLabel.Name = "searchLabel";
        searchLabel.Size = new Size(47, 15);
        searchLabel.TabIndex = 0;
        searchLabel.Text = "Поиск:";
        // 
        // documentTabControl
        // 
        documentTabControl.Dock = DockStyle.Fill;
        documentTabControl.Location = new Point(0, 94);
        documentTabControl.Name = "documentTabControl";
        documentTabControl.SelectedIndex = 0;
        documentTabControl.Size = new Size(1184, 527);
        documentTabControl.TabIndex = 2;
        documentTabControl.SelectedIndexChanged += documentTabControl_SelectedIndexChanged;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel, editorInfoLabel });
        statusStrip1.Location = new Point(0, 621);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(1184, 22);
        statusStrip1.TabIndex = 3;
        statusStrip1.Text = "statusStrip1";
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(39, 17);
        statusLabel.Text = "Статус";
        // 
        // editorInfoLabel
        // 
        editorInfoLabel.Name = "editorInfoLabel";
        editorInfoLabel.Size = new Size(0, 17);
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1184, 643);
        Controls.Add(documentTabControl);
        Controls.Add(statusStrip1);
        Controls.Add(topPanel);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        MinimumSize = new Size(1000, 500);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "StudentPad";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        topPanel.ResumeLayout(false);
        topPanel.PerformLayout();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem newToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
    private ToolStripMenuItem closeTabToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripMenuItem themeToolStripMenuItem;
    private ToolStripMenuItem lightThemeToolStripMenuItem;
    private ToolStripMenuItem darkThemeToolStripMenuItem;
    private ToolStripMenuItem systemThemeToolStripMenuItem;
    private Panel topPanel;
    private Label searchLabel;
    private TextBox searchTextBox;
    private Button findNextButton;
    private CheckBox caseSensitiveCheckBox;
    private CheckBox wholeWordCheckBox;
    private CheckBox regexCheckBox;
    private TabControl documentTabControl;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel statusLabel;
    private ToolStripStatusLabel editorInfoLabel;
    private OpenFileDialog openFileDialog;
    private SaveFileDialog saveFileDialog;
    private Button openButton;
    private Button saveButton;
    private Button findFromStartButton;
    private Button clearSelectionButton;
}
