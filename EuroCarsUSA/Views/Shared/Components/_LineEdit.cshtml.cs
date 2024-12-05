﻿namespace EuroCarsUSA.Views.Shared.Components
{
    public class _LineEdit
    {
        public string Classes { get; set; } = string.Empty;
        public string Attributes { get; set; } = string.Empty;
        public object? Leading { get; set; }
        public object? Trailing { get; set; }
        public string Placeholder { get; set; } = string.Empty;
        public bool ShowOverline { get; set; } = false;
        public bool ShowOverlineInside { get; set; } = false;
        public bool HasOutsidOverlineSizeEffect { get; set; } = false;
        public string Overline { get; set; } = string.Empty;
        public bool ShowSupportive { get; set; } = false;
        public bool HasOutsideSupportiveSizeEffect { get; set; } = false;
        public string Supportive { get; set; } = string.Empty;
        public bool ShowAdditional { get; set; } = false;
        public bool ShowAdditionalBackground { get; set; } = false;
        public string Additional { get; set; } = string.Empty;
    }
}
