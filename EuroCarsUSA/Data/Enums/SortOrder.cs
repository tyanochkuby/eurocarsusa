namespace EuroCarsUSA.Data.Enum
{
    public enum SortOrder
    {

        NewFirst,
        ByYear,
        ByYearDesc,
        ByMileage,
        ByPrice,
        ByPriceDesc,
    }

    public static class SortOrderExtensions
    {
        public static string ToCustomString(this SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.NewFirst:
                    return "Freshly added";
                case SortOrder.ByYear:
                    return "Oldest first";
                case SortOrder.ByYearDesc:
                    return "Newest first";
                case SortOrder.ByMileage:
                    return "Lowest mileage";
                case SortOrder.ByPrice:
                    return "Lowest price";
                case SortOrder.ByPriceDesc:
                    return "Highest price";
                default:
                    return sortOrder.ToString();
            }
        }
    }
}
