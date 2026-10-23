using System.Reflection;
using System.Text;

namespace BlossomTales2Randomizer.Models;

public static class SettingsCatalog
{
    // Controls the top-to-bottom order of the panels in the UI. Nested types not listed
    // here are appended after these, in whatever order reflection returns them.
    private static readonly string[] CategoryDisplayOrder =
    {
        nameof(RamdomizerSettings.ItemLocalisationSetting),
    };

    public static readonly List<SettingDefinition> Definitions = BuildFromRandomizerSettings();

    private static List<SettingDefinition> BuildFromRandomizerSettings()
    {
        var definitions = new List<SettingDefinition>();

        var categoryTypes = typeof(RamdomizerSettings).GetNestedTypes(BindingFlags.Public)
            .Where(t => t.IsClass)
            .OrderBy(t =>
            {
                var index = Array.IndexOf(CategoryDisplayOrder, t.Name);
                return index < 0 ? int.MaxValue : index;
            });

        foreach (var categoryType in categoryTypes)
        {
            var categoryKey = TrimSettingSuffix(categoryType.Name);
            var categoryLabel = Humanize(categoryKey);

            foreach (var field in categoryType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (field.FieldType == typeof(bool))
                {
                    definitions.Add(new SettingDefinition
                    {
                        Key = $"{categoryKey}.{field.Name}",
                        Label = Humanize(field.Name),
                        Category = categoryLabel,
                        CategoryKey = categoryKey,
                        FieldName = field.Name,
                        DefaultValue = (bool)field.GetValue(null)!,
                    });
                }
                else if (field.FieldType.IsEnum && field.FieldType.IsDefined(typeof(FlagsAttribute), false))
                {
                    AddFlagDefinitions(definitions, categoryKey, categoryLabel, field);
                }
            }
        }

        return definitions;
    }

    // A [Flags] enum field becomes one checkbox per single-bit value. None and combined
    // values (e.g. AllExceptKeyPieces) get no checkbox of their own.
    private static void AddFlagDefinitions(
        List<SettingDefinition> definitions, string categoryKey, string categoryLabel, FieldInfo field)
    {
        var enumType = field.FieldType;
        var current = Convert.ToInt64(field.GetValue(null));

        foreach (var value in Enum.GetValues(enumType))
        {
            var flagValue = Convert.ToInt64(value);
            if (flagValue <= 0 || (flagValue & (flagValue - 1)) != 0)
            {
                continue;
            }

            var flagName = Enum.GetName(enumType, value)!;

            definitions.Add(new SettingDefinition
            {
                Key = $"{categoryKey}.{field.Name}.{flagName}",
                Label = Humanize(flagName),
                Category = categoryLabel,
                CategoryKey = categoryKey,
                FieldName = field.Name,
                FlagsEnumType = enumType,
                FlagValue = flagValue,
                Description = $"Included in {field.Name}.",
                DefaultValue = (current & flagValue) == flagValue,
            });
        }
    }

    private static string TrimSettingSuffix(string name)
    {
        if (name.EndsWith("Settings", StringComparison.Ordinal))
        {
            return name[..^"Settings".Length];
        }

        if (name.EndsWith("Setting", StringComparison.Ordinal))
        {
            return name[..^"Setting".Length];
        }

        return name;
    }

    private static string Humanize(string identifier)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < identifier.Length; i++)
        {
            var c = identifier[i];
            if (i > 0 && char.IsUpper(c))
            {
                var prev = identifier[i - 1];
                var next = i + 1 < identifier.Length ? identifier[i + 1] : '\0';
                if (char.IsLower(prev) || (char.IsUpper(prev) && char.IsLower(next)))
                {
                    sb.Append(' ');
                }
            }

            sb.Append(c);
        }

        return sb.ToString();
    }
}
