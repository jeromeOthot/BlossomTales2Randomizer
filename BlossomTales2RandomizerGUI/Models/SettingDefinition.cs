namespace BlossomTales2Randomizer.Models;

public sealed class SettingDefinition
{
    public required string Key { get; init; }
    public required string Label { get; init; }
    public required string Category { get; init; }
    public string Description { get; init; } = string.Empty;
    public bool DefaultValue { get; init; }
}
