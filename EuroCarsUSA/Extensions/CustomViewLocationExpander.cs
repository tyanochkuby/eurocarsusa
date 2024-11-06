using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

public class CustomViewLocationExpander : IViewLocationExpander
{
    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        var customLocations = new[]
        {
            "/Views/Shared/Components/{0}.cshtml"
        };

        return customLocations.Concat(viewLocations);
    }
}
