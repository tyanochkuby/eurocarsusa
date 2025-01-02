namespace EuroCarsUSA.Views.Shared.Components;

public interface IEnumDropdown
{
    Enum Value { get; set; }
    string Style { get; set; }
    string Name { get; set; }
    string CssClass { get; set; }
}

public class EnumDropdown<TEnum> : IEnumDropdown where TEnum : Enum
{
    required public TEnum Value { get; set; }
    public string Style { get; set; } = "";
    public string Name { get; set; } = "";
    public string CssClass { get; set; } = "";
    Enum IEnumDropdown.Value
    {
        get => Value;
        set => Value = (TEnum)value;
    }
}