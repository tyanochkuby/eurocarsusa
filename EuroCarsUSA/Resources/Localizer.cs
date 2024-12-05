using EuroCarsUSA.Controllers;
using Microsoft.Extensions.Localization;

namespace EuroCarsUSA.Resources
{
    public class Localizer
    {
        private IStringLocalizer<HomeController> _localizer;

        public Localizer(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }

        public string Back => _localizer["Back"];
        public string CarType => _localizer["CarType"];
        public string Close => _localizer["Close"];
        public string Color => _localizer["Color"];
        public string EngineVolume => _localizer["EngineVolume"];
        public string Filters => _localizer["Filters"];
        public string Find => _localizer["Find"];
        public string FuelType => _localizer["FuelType"];
        public string Make => _localizer["Make"];
        public string Mileage => _localizer["Mileage"];
        public string Model => _localizer["Model"];
        public string Price => _localizer["Price"];
        public string SortBy => _localizer["SortBy"];
        public string Transmission => _localizer["Transmission"];
        public string Year => _localizer["Year"];
        public string SortOrder_NewFirst => _localizer["SortOrder_NewFirst"];
        public string SortOrder_ByYear => _localizer["SortOrder_ByYear"];
        public string SortOrder_ByYearDesc => _localizer["SortOrder_ByYearDesc"];
        public string SortOrder_ByMileage => _localizer["SortOrder_ByMileage"];
        public string SortOrder_ByPrice => _localizer["SortOrder_ByPrice"];
        public string SortOrder_ByPriceDesc => _localizer["SortOrder_ByPriceDesc"];
        public string Electric => _localizer["Electric"];
        public string ShowMore => _localizer["ShowMore"];
        public string ContactSeller => _localizer["ContactSeller"];
        public string MessageSent => _localizer["MessageSent"];
        public string PhoneNumber => _localizer["PhoneNumber"];
        public string SendMessage => _localizer["SendMessage"];
        public string CarColor_Beige => _localizer["CarColor_Beige"];
        public string CarColor_Black => _localizer["CarColor_Black"];
        public string CarColor_Blue => _localizer["CarColor_Blue"];
        public string CarColor_Brown => _localizer["CarColor_Brown"];
        public string CarColor_Gray => _localizer["CarColor_Gray"];
        public string CarColor_Green => _localizer["CarColor_Green"];
        public string CarColor_Orange => _localizer["CarColor_Orange"];
        public string CarColor_Pink => _localizer["CarColor_Pink"];
        public string CarColor_Purple => _localizer["CarColor_Purple"];
        public string CarColor_Red => _localizer["CarColor_Red"];
        public string CarColor_Silver => _localizer["CarColor_Silver"];
        public string CarColor_White => _localizer["CarColor_White"];
        public string CarColor_Yellow => _localizer["CarColor_Yellow"];
        public string CarFuelType_Benzine => _localizer["CarFuelType_Benzine"];
        public string CarFuelType_Diesel => _localizer["CarFuelType_Diesel"];
        public string CarFuelType_Electric => _localizer["CarFuelType_Electric"];
        public string CarTransmission_Automatic => _localizer["CarTransmission_Automatic"];
        public string CarTransmission_Manual => _localizer["CarTransmission_Manual"];
        public string CarTransmission_Robot => _localizer["CarTransmission_Robot"];
        public string LikesHeader => _localizer["LikesHeader"];
        public string Choose => _localizer["Choose"];
        public string Clear => _localizer["Clear"];
        public string Diesel => _localizer["Diesel"];
        public string From => _localizer["From"];
        public string Message => _localizer["Message"];
        public string Name => _localizer["Name"];
        public string To => _localizer["To"];
        public string Type => _localizer["Type"];
        public string Call => _localizer["Call"];
        public string Save => _localizer["Save"];
        public string Details => _localizer["Details"];
        public string Empty => _localizer["Empty"];
    }
}
