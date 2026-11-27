namespace BlossomTales2Randomizer.Models;

// Marks a bool? setting to be shown as a combobox with one entry per state.
[AttributeUsage(AttributeTargets.Field)]
public sealed class NullableBoolChoicesAttribute(string whenNull, string whenFalse, string whenTrue) : Attribute
{
    public string WhenNull { get; } = whenNull;
    public string WhenFalse { get; } = whenFalse;
    public string WhenTrue { get; } = whenTrue;
}
