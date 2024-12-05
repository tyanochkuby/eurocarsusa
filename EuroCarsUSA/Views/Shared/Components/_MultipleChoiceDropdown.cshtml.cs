using EuroCarsUSA.Views.Home.Components.ViewModels;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace EuroCarsUSA.Views.Shared.Components
{
    public class _MultipleChoiceDropdown
    {
        public string DropdownId { get; set; }
        public string Placeholder { get; set; }
        public string Overline { get; set; }
        public string Name { get; set; }
        public string InputId { get; set; }
        public IEnumerable<string> SelectedValues { get; set; }
        public IEnumerable<FilterOptionViewModel> Options { get; set; }
        public IStringLocalizer Translator { get; set; }
    }
}
