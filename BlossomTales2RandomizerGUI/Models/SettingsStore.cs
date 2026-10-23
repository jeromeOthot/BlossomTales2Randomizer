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
            def => TryGetSavedValue(root, def, out var value) ? value : def.DefaultValue);
    }

    public static void Save(IReadOnlyList<SettingDefinition> catalog, Dictionary<string, bool> values)
    {
        var root = LoadRoot();

        foreach (var categoryDefs in catalog.GroupBy(d => d.CategoryKey))
        {
            if (root[categoryDefs.Key] is not JsonObject group)
            {
                group = new JsonObject();
                root[categoryDefs.Key] = group;
            }

            foreach (var fieldDefs in categoryDefs.GroupBy(d => d.FieldName))
            {
                group[fieldDefs.Key] = ToJsonValue(fieldDefs.ToList(), values);
            }
        }

        SaveRoot(root);
    }

    // Flag checkboxes of one field are written back as a single integer: the OR of the
    // checked flag values (e.g. TreasureChests | Shops = 9).
    private static JsonNode ToJsonValue(List<SettingDefinition> fieldDefs, Dictionary<string, bool> values)
    {
        if (fieldDefs[0].FlagsEnumType is null)
        {
            return JsonValue.Create(values[fieldDefs[0].Key]);
        }

        var combined = fieldDefs.Where(d => values[d.Key]).Aggregate(0L, (acc, d) => acc | d.FlagValue);

        return JsonValue.Create(combined);
    }

    public static string LoadSeedNumber() =>
        LoadRoot()[SeedNumberKey]?.GetValue<string>() ?? string.Empty;

    public static void SaveSeedNumber(string seedNumber)
    {
        var root = LoadRoot();
        root[SeedNumberKey] = seedNumber;
        SaveRoot(root);
    }

    private static bool TryGetSavedValue(JsonObject root, SettingDefinition def, out bool value)
    {
        value = false;

        if (root[def.CategoryKey] is not JsonObject group || group[def.FieldName] is not JsonValue node)
        {
            return false;
        }

        if (def.FlagsEnumType is not { } enumType)
        {
            return node.TryGetValue(out value);
        }

        // The saved integer is turned back into the enum so each flag can be tested on it.
        // A string such as "Shops, Traders" (an earlier format) is still understood.
        if (node.TryGetValue<long>(out var bits))
        {
            value = HasFlag(Enum.ToObject(enumType, bits), def.FlagValue);
            return true;
        }

        if (node.TryGetValue<string>(out var text) && Enum.TryParse(enumType, text, true, out var parsed))
        {
            value = HasFlag(parsed, def.FlagValue);
            return true;
        }

        return false;
    }

    private static bool HasFlag(object enumValue, long flagValue) =>
        (Convert.ToInt64(enumValue) & flagValue) == flagValue;
}
