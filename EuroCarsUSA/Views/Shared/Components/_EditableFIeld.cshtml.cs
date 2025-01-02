namespace EuroCarsUSA.Views.Shared.Components;

public class EditableField
{
    public object Value { get; set; }
    public bool IsVisible { get; set; } = true;
    public string Width { get; set; } = "100%";
    public Enum SelectedValue { get; set; }
    public string CssClass { get; set; } = "";
    public string Style { get; set; } = "";
    public string Name { get; set; }
}
