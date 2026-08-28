using System.Text.Json;

namespace BlossomTales2Randomizer.Models;

public static class SettingsStore
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        WriteIndented = true
    };

    private static readonly JsonSerializerOptions ReadOptions = new()
    {
        AllowTrailingCommas = true
    };

    public static string GetSettingsPath() =>
        Path.Combine(GetProjectRoot(), "settings.json");

    // dotnet run/build outputs land in bin/<Config>/<TFM>, several levels under the
    // project folder. Walk back up to the folder containing the .csproj so settings.json
    // lives alongside the source instead of inside build output.
    private static string GetProjectRoot()
    {
        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
        {
            if (dir.GetFiles("*.csproj").Length > 0)
            {
                return dir.FullName;
            }
        }

        return AppContext.BaseDirectory;
    }

    public static Dictionary<string, bool> Load(IReadOnlyList<SettingDefinition> catalog)
    {
        Dictionary<string, Dictionary<string, bool>>? saved = null;

        var path = GetSettingsPath();
        if (File.Exists(path))
        {
            try
            {
                saved = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, bool>>>(
                    File.ReadAllText(path), ReadOptions);
            }
            catch (JsonException)
            {
                saved = null;
            }
        }

        return catalog.ToDictionary(
            def => def.Key,
            def => TryGetSavedValue(saved, def.Key, out var value) ? value : def.DefaultValue);
    }

    public static void Save(Dictionary<string, bool> values)
    {
        var nested = new Dictionary<string, Dictionary<string, bool>>();

        foreach (var (key, value) in values)
        {
            var (category, field) = SplitKey(key);

            if (!nested.TryGetValue(category, out var group))
            {
                group = new Dictionary<string, bool>();
                nested[category] = group;
            }

            group[field] = value;
        }

        var json = JsonSerializer.Serialize(nested, WriteOptions);
        File.WriteAllText(GetSettingsPath(), json);
    }

    private static bool TryGetSavedValue(Dictionary<string, Dictionary<string, bool>>? saved, string key, out bool value)
    {
        value = false;

        if (saved is null)
        {
            return false;
        }

        var (category, field) = SplitKey(key);

        return saved.TryGetValue(category, out var group) && group.TryGetValue(field, out value);
    }

    private static (string Category, string Field) SplitKey(string key)
    {
        var separatorIndex = key.IndexOf('.');
        return separatorIndex >= 0
            ? (key[..separatorIndex], key[(separatorIndex + 1)..])
            : (key, key);
    }
}
