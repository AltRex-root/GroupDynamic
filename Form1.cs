using System.Text;

namespace GroupDynamic;

public partial class Form1 : Form
{
    private readonly UTF8Encoding utf8WithoutBom = new(encoderShouldEmitUTF8Identifier: false);
    private string lastSearchText = string.Empty;
    private bool lastMatchCase;
    private bool lastWholeWord;
    private bool lastUseRegex;
    private int untitledDocumentCount;
    private AppSettings appSettings = new();

    public Form1()
    {
        InitializeComponent();
        appSettings = AppSettingsService.Load();
        ConfigureForm();
        RestoreWindowSettings();
        CreateEmptyTab();
    }

    private void ConfigureForm()
    {
        saveFileDialog.Filter = "Text files|*.txt|C# files|*.cs|JSON files|*.json|XML files|*.xml|Markdown files|*.md|Log files|*.log|CSV files|*.csv|RTF files|*.rtf|All files|*.*";
        openFileDialog.Filter = "Supported files|*.txt;*.cs;*.json;*.xml;*.md;*.log;*.csv;*.ini;*.config;*.rtf;*.pdf|Text files|*.txt;*.cs;*.json;*.xml;*.md;*.log;*.csv;*.ini;*.config|PDF files|*.pdf|RTF files|*.rtf|All files|*.*";

        if (!string.IsNullOrWhiteSpace(appSettings.LastDirectory) && Directory.Exists(appSettings.LastDirectory))
        {
            openFileDialog.InitialDirectory = appSettings.LastDirectory;
            saveFileDialog.InitialDirectory = appSettings.LastDirectory;
        }

        Text = "StudentPad";
        ApplyTheme(appSettings.Theme);
        UpdateStatus("Приложение готово к работе.");
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (!ConfirmCloseAllDocuments())
        {
            e.Cancel = true;
            return;
        }

        SaveWindowSettings();
        AppSettingsService.Save(appSettings);
        base.OnFormClosing(e);
    }

    private void newToolStripMenuItem_Click(object sender, EventArgs e)
    {
        CreateEmptyTab();
        UpdateStatus("Создан новый файл.");
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenDocument();
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveDocument(false);
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveDocument(true);
    }

    private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
    {
        CloseActiveTab();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void openButton_Click(object sender, EventArgs e)
    {
        OpenDocument();
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
        SaveDocument(false);
    }

    private void findNextButton_Click(object sender, EventArgs e)
    {
        FindAndSelect(true);
    }

    private void findFromStartButton_Click(object sender, EventArgs e)
    {
        FindAndSelect(false);
    }

    private void clearSelectionButton_Click(object sender, EventArgs e)
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor is null)
        {
            return;
        }

        editor.SelectionLength = 0;
        editor.Focus();
        UpdateStatus("Выделение очищено.");
    }

    private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            FindAndSelect(true);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void documentTabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateWindowTitle();
        UpdateEditorInfo();
    }

    private void lightThemeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ApplyTheme(AppTheme.Light);
    }

    private void darkThemeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ApplyTheme(AppTheme.Dark);
    }

    private void systemThemeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ApplyTheme(AppTheme.System);
    }

    private void OpenDocument()
    {
        if (openFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        string selectedPath = openFileDialog.FileName;
        string extension = Path.GetExtension(selectedPath).ToLowerInvariant();

        try
        {
            LoadedDocument document = extension == ".rtf"
                ? new LoadedDocument(string.Empty, false)
                : DocumentLoader.Load(selectedPath);

            TabPage tab = CreateDocumentTab(Path.GetFileName(selectedPath));
            DocumentTabState tabState = GetTabState(tab);
            tabState.IsLoading = true;

            try
            {
                if (extension == ".rtf")
                {
                    tabState.Editor.LoadFile(selectedPath, RichTextBoxStreamType.RichText);
                    tabState.IsPdfDocument = false;
                }
                else
                {
                    tabState.Editor.Text = document.Text;
                    tabState.IsPdfDocument = document.IsPdf;
                }
            }
            finally
            {
                tabState.IsLoading = false;
            }

            tabState.FilePath = selectedPath;
            tabState.DisplayName = Path.GetFileName(selectedPath);
            tabState.Editor.SelectionStart = 0;
            tabState.Editor.SelectionLength = 0;
            SetTabDirty(tab, false);

            documentTabControl.TabPages.Add(tab);
            documentTabControl.SelectedTab = tab;
            RememberDirectory(selectedPath);

            UpdateWindowTitle();
            UpdateStatus($"Файл открыт: {selectedPath}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть файл.\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            UpdateStatus("Ошибка открытия файла.");
        }
    }

    private bool SaveDocument(bool saveAs)
    {
        TabPage? tab = GetActiveDocumentTab();
        return tab is not null && SaveDocument(tab, saveAs);
    }

    private bool SaveDocument(TabPage tab, bool saveAs)
    {
        DocumentTabState tabState = GetTabState(tab);
        string? targetPath = tabState.FilePath;

        if (saveAs || string.IsNullOrWhiteSpace(targetPath) || tabState.IsPdfDocument)
        {
            if (tabState.IsPdfDocument)
            {
                saveFileDialog.FileName = "converted-from-pdf.txt";
            }
            else if (!string.IsNullOrWhiteSpace(tabState.FilePath))
            {
                saveFileDialog.FileName = Path.GetFileName(tabState.FilePath);
            }
            else
            {
                saveFileDialog.FileName = GetDefaultSaveFileName(tabState);
            }

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            targetPath = saveFileDialog.FileName;
        }

        try
        {
            string extension = Path.GetExtension(targetPath).ToLowerInvariant();

            if (extension == ".rtf")
            {
                tabState.Editor.SaveFile(targetPath, RichTextBoxStreamType.RichText);
            }
            else
            {
                File.WriteAllText(targetPath, tabState.Editor.Text, utf8WithoutBom);
            }

            tabState.FilePath = targetPath;
            tabState.IsPdfDocument = false;
            tabState.DisplayName = Path.GetFileName(targetPath);
            SetTabDirty(tab, false);
            RememberDirectory(targetPath);
            UpdateWindowTitle();
            UpdateStatus($"Файл сохранён: {targetPath}");
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось сохранить файл.\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            UpdateStatus("Ошибка сохранения файла.");
            return false;
        }
    }

    private void FindAndSelect(bool searchFromCurrentPosition)
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor is null)
        {
            return;
        }

        string searchText = searchTextBox.Text;
        SearchOptions options = new(
            caseSensitiveCheckBox.Checked,
            wholeWordCheckBox.Checked,
            regexCheckBox.Checked);

        if (string.IsNullOrWhiteSpace(searchText))
        {
            MessageBox.Show("Введите текст для поиска.", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        string sourceText = editor.Text;
        if (string.IsNullOrEmpty(sourceText))
        {
            UpdateStatus("В документе нет текста для поиска.");
            return;
        }

        int startIndex = 0;
        bool optionsChanged = searchText != lastSearchText
            || options.MatchCase != lastMatchCase
            || options.WholeWord != lastWholeWord
            || options.UseRegex != lastUseRegex;

        if (searchFromCurrentPosition && !optionsChanged)
        {
            startIndex = editor.SelectionStart + editor.SelectionLength;
        }

        SearchResult result = SearchService.Find(sourceText, searchText, startIndex, options);

        if (!result.Found && result.ErrorMessage is null && startIndex > 0)
        {
            result = SearchService.Find(sourceText, searchText, 0, options);
        }

        lastSearchText = searchText;
        lastMatchCase = options.MatchCase;
        lastWholeWord = options.WholeWord;
        lastUseRegex = options.UseRegex;

        if (result.ErrorMessage is not null)
        {
            MessageBox.Show($"Ошибка в регулярном выражении.\n{result.ErrorMessage}", "Regex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            UpdateStatus("Ошибка в регулярном выражении.");
            return;
        }

        if (!result.Found)
        {
            UpdateStatus("Ничего не найдено.");
            MessageBox.Show("Совпадение не найдено.", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        editor.Focus();
        editor.SelectionStart = result.Start;
        editor.SelectionLength = result.Length;
        editor.ScrollToCaret();
        UpdateStatus($"Найдено совпадение на позиции {result.Start + 1}.");
    }

    private void CreateEmptyTab()
    {
        string title = untitledDocumentCount == 0
            ? "Новый файл"
            : $"Новый файл {untitledDocumentCount + 1}";

        untitledDocumentCount++;

        TabPage tab = CreateDocumentTab(title);
        documentTabControl.TabPages.Add(tab);
        documentTabControl.SelectedTab = tab;
        UpdateWindowTitle();
        UpdateEditorInfo();
    }

    private TabPage CreateDocumentTab(string title)
    {
        TabPage tab = new();

        RichTextBox editor = new()
        {
            Dock = DockStyle.Fill,
            Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point),
            BorderStyle = BorderStyle.None
        };

        editor.TextChanged += Editor_TextChanged;

        DocumentTabState state = new(editor)
        {
            DisplayName = title
        };

        tab.Tag = state;
        tab.Controls.Add(editor);
        UpdateTabTitle(tab);

        ApplyThemeToControl(editor);
        ApplyThemeToTab(tab);
        return tab;
    }

    private void CloseActiveTab()
    {
        TabPage? tab = GetActiveDocumentTab();
        if (tab is null || !ConfirmSaveTab(tab))
        {
            return;
        }

        documentTabControl.TabPages.Remove(tab);
        tab.Dispose();

        if (documentTabControl.TabPages.Count == 0)
        {
            CreateEmptyTab();
        }

        UpdateWindowTitle();
        UpdateEditorInfo();
        UpdateStatus("Вкладка закрыта.");
    }

    private bool ConfirmCloseAllDocuments()
    {
        foreach (TabPage tab in documentTabControl.TabPages.Cast<TabPage>().ToList())
        {
            documentTabControl.SelectedTab = tab;

            if (!ConfirmSaveTab(tab))
            {
                return false;
            }
        }

        return true;
    }

    private bool ConfirmSaveTab(TabPage tab)
    {
        DocumentTabState tabState = GetTabState(tab);
        if (!tabState.IsDirty)
        {
            return true;
        }

        DialogResult result = MessageBox.Show(
            $"Сохранить изменения в документе «{tabState.DisplayName}»?",
            "Несохранённые изменения",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Warning);

        return result switch
        {
            DialogResult.Yes => SaveDocument(tab, false),
            DialogResult.No => true,
            _ => false
        };
    }

    private void Editor_TextChanged(object? sender, EventArgs e)
    {
        if (sender is RichTextBox editor)
        {
            TabPage? tab = FindTabByEditor(editor);
            if (tab is not null)
            {
                DocumentTabState tabState = GetTabState(tab);

                if (!tabState.IsLoading)
                {
                    SetTabDirty(tab, true);
                }
            }
        }

        UpdateEditorInfo();
    }

    private TabPage? GetActiveDocumentTab()
    {
        return documentTabControl.SelectedTab;
    }

    private RichTextBox? GetActiveEditor()
    {
        TabPage? tab = GetActiveDocumentTab();
        return tab is null ? null : GetTabState(tab).Editor;
    }

    private TabPage? FindTabByEditor(RichTextBox editor)
    {
        foreach (TabPage tab in documentTabControl.TabPages)
        {
            if (GetTabState(tab).Editor == editor)
            {
                return tab;
            }
        }

        return null;
    }

    private void SetTabDirty(TabPage tab, bool isDirty)
    {
        DocumentTabState tabState = GetTabState(tab);
        tabState.IsDirty = isDirty;
        UpdateTabTitle(tab);
        UpdateWindowTitle();
    }

    private void UpdateTabTitle(TabPage tab)
    {
        DocumentTabState tabState = GetTabState(tab);
        tab.Text = tabState.IsDirty ? $"{tabState.DisplayName}*" : tabState.DisplayName;
    }

    private void UpdateWindowTitle()
    {
        TabPage? tab = GetActiveDocumentTab();
        string fileName = tab?.Text ?? "StudentPad";
        Text = $"StudentPad - {fileName}";
    }

    private void UpdateEditorInfo()
    {
        RichTextBox? editor = GetActiveEditor();

        if (editor is null)
        {
            editorInfoLabel.Text = string.Empty;
            return;
        }

        int lines = editor.Lines.Length;
        int symbols = editor.TextLength;
        editorInfoLabel.Text = $"Строк: {lines} | Символов: {symbols} | Вкладок: {documentTabControl.TabPages.Count}";
    }

    private void UpdateStatus(string message)
    {
        statusLabel.Text = message;
        UpdateEditorInfo();
    }

    private string GetDefaultSaveFileName(DocumentTabState tabState)
    {
        string fileName = tabState.DisplayName;
        return Path.HasExtension(fileName) ? fileName : $"{fileName}.txt";
    }

    private void RememberDirectory(string path)
    {
        string? directory = Path.GetDirectoryName(path);
        if (string.IsNullOrWhiteSpace(directory))
        {
            return;
        }

        appSettings.LastDirectory = directory;
        openFileDialog.InitialDirectory = directory;
        saveFileDialog.InitialDirectory = directory;
    }

    private void RestoreWindowSettings()
    {
        if (appSettings.WindowWidth >= MinimumSize.Width && appSettings.WindowHeight >= MinimumSize.Height)
        {
            Size = new Size(appSettings.WindowWidth, appSettings.WindowHeight);
        }

        if (appSettings.WindowWidth > 0 && appSettings.WindowHeight > 0)
        {
            Point savedLocation = new(appSettings.WindowLeft, appSettings.WindowTop);
            bool visibleOnScreen = Screen.AllScreens.Any(screen => screen.WorkingArea.Contains(savedLocation));

            if (visibleOnScreen)
            {
                StartPosition = FormStartPosition.Manual;
                Location = savedLocation;
            }
        }

        if (appSettings.IsMaximized)
        {
            WindowState = FormWindowState.Maximized;
        }
    }

    private void SaveWindowSettings()
    {
        Rectangle bounds = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;

        appSettings.Theme = currentTheme;
        appSettings.WindowLeft = bounds.Left;
        appSettings.WindowTop = bounds.Top;
        appSettings.WindowWidth = bounds.Width;
        appSettings.WindowHeight = bounds.Height;
        appSettings.IsMaximized = WindowState == FormWindowState.Maximized;
    }

    private DocumentTabState GetTabState(TabPage tab)
    {
        return (DocumentTabState)tab.Tag!;
    }
}

public class DocumentTabState
{
    public DocumentTabState(RichTextBox editor)
    {
        Editor = editor;
    }

    public RichTextBox Editor { get; }

    public string DisplayName { get; set; } = "Новый файл";

    public string? FilePath { get; set; }

    public bool IsPdfDocument { get; set; }

    public bool IsDirty { get; set; }

    public bool IsLoading { get; set; }
}
