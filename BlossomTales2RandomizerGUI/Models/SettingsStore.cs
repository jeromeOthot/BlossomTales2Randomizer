using System.Text.Json;
using System.Text.Json.Nodes;

namespace BlossomTales2Randomizer.Models;

public static class SettingsStore
{
    private const string SeedNumberKey = "SeedNumber";

    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        WriteIndented = true
    };

    private static readonly JsonDocumentOptions ReadOptions = new()
    {
        AllowTrailingCommas = true
    };

    public static string GetSettingsPath() =>
        Path.Combine(Directory.GetParent(GetProjectRoot())!.FullName, "settings.json");

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

    private static JsonObject LoadRoot()
    {
        var path = GetSettingsPath();
        if (!File.Exists(path))
        {
            return new JsonObject();
        }

        try
        {
            return JsonNode.Parse(File.ReadAllText(path), documentOptions: ReadOptions) as JsonObject
                   ?? new JsonObject();
        }
        catch (JsonException)
        {
            return new JsonObject();
        }
    }

    private static void SaveRoot(JsonObject root) =>
        File.WriteAllText(GetSettingsPath(), root.ToJsonString(WriteOptions));

    public static Dictionary<string, bool> Load(IReadOnlyList<SettingDefinition> catalog)
    {
        var root = LoadRoot();

        return catalog.ToDictionary(
            def => def.Key,
            def => TryGetSavedValue(root, def.Key, out var value) ? value : def.DefaultValue);
    }

    public static void Save(Dictionary<string, bool> values)
    {
        var root = LoadRoot();

        foreach (var (key, value) in values)
        {
            var (category, field) = SplitKey(key);

            if (root[category] is not JsonObject group)
            {
                group = new JsonObject();
                root[category] = group;
            }

            group[field] = value;
        }

        SaveRoot(root);
    }

    public static string LoadSeedNumber() =>
        LoadRoot()[SeedNumberKey]?.GetValue<string>() ?? string.Empty;

    public static void SaveSeedNumber(string seedNumber)
    {
        var root = LoadRoot();
        root[SeedNumberKey] = seedNumber;
        SaveRoot(root);
    }

    private static bool TryGetSavedValue(JsonObject root, string key, out bool value)
    {
        value = false;

        var (category, field) = SplitKey(key);

        return root[category] is JsonObject group
            && group[field] is JsonValue node
            && node.TryGetValue(out value);
    }

    private static (string Category, string Field) SplitKey(string key)
    {
        var separatorIndex = key.IndexOf('.');
        return separatorIndex >= 0
            ? (key[..separatorIndex], key[(separatorIndex + 1)..])
            : (key, key);
    }
}
