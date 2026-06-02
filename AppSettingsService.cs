using System.Text.Json;

namespace GroupDynamic;

public sealed class AppSettings
{
    public AppTheme Theme { get; set; } = AppTheme.System;

    public string? LastDirectory { get; set; }

    public int WindowLeft { get; set; }

    public int WindowTop { get; set; }

    public int WindowWidth { get; set; }

    public int WindowHeight { get; set; }

    public bool IsMaximized { get; set; }
}

public static class AppSettingsService
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    public static AppSettings Load()
    {
        string settingsPath = GetSettingsPath();

        if (!File.Exists(settingsPath))
        {
            return new AppSettings();
        }

        try
        {
            string json = File.ReadAllText(settingsPath);
            return JsonSerializer.Deserialize<AppSettings>(json, SerializerOptions) ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }

    public static void Save(AppSettings settings)
    {
        string settingsPath = GetSettingsPath();
        Directory.CreateDirectory(Path.GetDirectoryName(settingsPath)!);

        string json = JsonSerializer.Serialize(settings, SerializerOptions);
        File.WriteAllText(settingsPath, json);
    }

    private static string GetSettingsPath()
    {
        return Path.Combine(Application.UserAppDataPath, "settings.json");
    }
}
