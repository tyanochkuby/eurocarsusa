namespace EuroCarsUSA.Data.Enum
{
    public enum SortOrder
    {

        NewFirst,
        ByYear,
        ByYearDesc,
        ByMileage,
        ByMileageDesc,
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
                    return "Newest First";
                case SortOrder.ByYear:
                    return "By Year (Ascending)";
                case SortOrder.ByYearDesc:
                    return "By Year (Descending)";
                case SortOrder.ByMileage:
                    return "By Mileage (Ascending)";
                case SortOrder.ByMileageDesc:
                    return "By Mileage (Descending)";
                case SortOrder.ByPrice:
                    return "By Price (Ascending)";
                case SortOrder.ByPriceDesc:
                    return "By Price (Descending)";
                default:
                    return sortOrder.ToString();
            }
        }
    }
}
