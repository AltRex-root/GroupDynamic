namespace GroupDynamic;

public partial class Form1
{
    private AppTheme currentTheme = AppTheme.System;

    private void ApplyTheme(AppTheme theme)
    {
        currentTheme = theme;
        appSettings.Theme = theme;
        ThemeColors colors = ThemeService.GetColors(theme);

        BackColor = colors.FormBackColor;
        ForeColor = colors.ForeColor;
        menuStrip1.BackColor = colors.PanelBackColor;
        menuStrip1.ForeColor = colors.ForeColor;
        topPanel.BackColor = colors.PanelBackColor;
        topPanel.ForeColor = colors.ForeColor;
        statusStrip1.BackColor = colors.PanelBackColor;
        statusStrip1.ForeColor = colors.ForeColor;
        searchTextBox.BackColor = colors.InputBackColor;
        searchTextBox.ForeColor = colors.ForeColor;
        searchLabel.ForeColor = colors.ForeColor;

        ApplyThemeToControl(topPanel);
        ApplyThemeToControl(menuStrip1);
        ApplyThemeToControl(statusStrip1);

        foreach (TabPage tabPage in documentTabControl.TabPages)
        {
            ApplyThemeToTab(tabPage);
        }

        lightThemeToolStripMenuItem.Checked = theme == AppTheme.Light;
        darkThemeToolStripMenuItem.Checked = theme == AppTheme.Dark;
        systemThemeToolStripMenuItem.Checked = theme == AppTheme.System;
    }

    private void ApplyThemeToTab(TabPage tabPage)
    {
        ThemeColors colors = ThemeService.GetColors(currentTheme);
        tabPage.BackColor = colors.EditorBackColor;
        tabPage.ForeColor = colors.ForeColor;

        foreach (Control control in tabPage.Controls)
        {
            ApplyThemeToControl(control);
        }
    }

    private void ApplyThemeToControl(Control control)
    {
        ThemeColors colors = ThemeService.GetColors(currentTheme);

        if (control is RichTextBox richTextBox)
        {
            richTextBox.BackColor = colors.EditorBackColor;
            richTextBox.ForeColor = colors.ForeColor;
            return;
        }

        if (control is TextBox textBox)
        {
            textBox.BackColor = colors.InputBackColor;
            textBox.ForeColor = colors.ForeColor;
        }
        else if (control is Button button)
        {
            button.BackColor = colors.ButtonBackColor;
            button.ForeColor = colors.ForeColor;
            button.FlatStyle = FlatStyle.Flat;
        }
        else
        {
            control.BackColor = colors.PanelBackColor;
            control.ForeColor = colors.ForeColor;
        }

        foreach (Control child in control.Controls)
        {
            ApplyThemeToControl(child);
        }
    }
}

public static class ThemeService
{
    public static ThemeColors GetColors(AppTheme theme)
    {
        return theme switch
        {
            AppTheme.Light => new ThemeColors(
                Color.White,
                Color.FromArgb(245, 245, 245),
                Color.White,
                Color.White,
                Color.FromArgb(235, 235, 235),
                Color.Black),
            AppTheme.Dark => new ThemeColors(
                Color.FromArgb(30, 31, 34),
                Color.FromArgb(43, 45, 48),
                Color.FromArgb(30, 31, 34),
                Color.FromArgb(60, 63, 65),
                Color.FromArgb(76, 80, 82),
                Color.Gainsboro),
            _ => new ThemeColors(
                SystemColors.Control,
                SystemColors.ControlLight,
                Color.White,
                Color.White,
                SystemColors.Control,
                SystemColors.ControlText)
        };
    }
}

public enum AppTheme
{
    Light,
    Dark,
    System
}

public readonly record struct ThemeColors(
    Color FormBackColor,
    Color PanelBackColor,
    Color EditorBackColor,
    Color InputBackColor,
    Color ButtonBackColor,
    Color ForeColor);
