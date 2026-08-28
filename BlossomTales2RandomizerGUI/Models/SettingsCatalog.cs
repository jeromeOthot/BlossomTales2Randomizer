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
        nameof(RamdomizerSettings.OtherSetting),
    };

    public static readonly List<SettingDefinition> Definitions = BuildFromRandomizerSettings();

    private static List<SettingDefinition> BuildFromRandomizerSettings()
    {
        var definitions = new List<SettingDefinition>();

        var categoryTypes = typeof(RamdomizerSettings).GetNestedTypes(BindingFlags.Public)
            .OrderBy(t =>
            {
                var index = Array.IndexOf(CategoryDisplayOrder, t.Name);
                return index < 0 ? int.MaxValue : index;
            });

        foreach (var categoryType in categoryTypes)
        {
            var categoryKey = TrimSettingSuffix(categoryType.Name);
            var categoryLabel = Humanize(categoryKey);

            var fields = categoryType.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(bool));

            foreach (var field in fields)
            {
                definitions.Add(new SettingDefinition
                {
                    Key = $"{categoryKey}.{field.Name}",
                    Label = Humanize(field.Name),
                    Category = categoryLabel,
                    DefaultValue = (bool)field.GetValue(null)!,
                });
            }
        }

        return definitions;
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
