namespace BlossomTales2Randomizer.Models;

public sealed class SettingDefinition
{
    public required string Key { get; init; }
    public required string Label { get; init; }
    public required string Category { get; init; }
    public required string CategoryKey { get; init; }
    public required string FieldName { get; init; }
    public Type? FlagsEnumType { get; init; }
    public long FlagValue { get; init; }
    public NullableBoolChoicesAttribute? Choices { get; init; }
    public string Description { get; init; } = string.Empty;
    public bool? DefaultValue { get; init; }

    public bool IsChoice => Choices is not null;
}
