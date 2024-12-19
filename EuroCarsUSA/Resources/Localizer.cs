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

        public string Back => _localizer[nameof(Back)];
        public string CarType => _localizer[nameof(CarType)];
        public string Close => _localizer[nameof(Close)];
        public string Color => _localizer[nameof(Color)];
        public string EngineVolume => _localizer[nameof(EngineVolume)];
        public string Filters => _localizer[nameof(Filters)];
        public string Find => _localizer[nameof(Find)];
        public string FuelType => _localizer[nameof(FuelType)];
        public string Make => _localizer[nameof(Make)];
        public string Mileage => _localizer[nameof(Mileage)];
        public string Model => _localizer[nameof(Model)];
        public string Price => _localizer[nameof(Price)];
        public string SortBy => _localizer[nameof(SortBy)];
        public string Transmission => _localizer[nameof(Transmission)];
        public string Year => _localizer[nameof(Year)];
        public string SortOrder_NewFirst => _localizer[nameof(SortOrder_NewFirst)];
        public string SortOrder_ByYear => _localizer[nameof(SortOrder_ByYear)];
        public string SortOrder_ByYearDesc => _localizer[nameof(SortOrder_ByYearDesc)];
        public string SortOrder_ByMileage => _localizer[nameof(SortOrder_ByMileage)];
        public string SortOrder_ByPrice => _localizer[nameof(SortOrder_ByPrice)];
        public string SortOrder_ByPriceDesc => _localizer[nameof(SortOrder_ByPriceDesc)];
        public string Electric => _localizer[nameof(Electric)];
        public string ShowMore => _localizer[nameof(ShowMore)];
        public string ContactSeller => _localizer[nameof(ContactSeller)];
        public string MessageSent => _localizer[nameof(MessageSent)];
        public string PhoneNumber => _localizer[nameof(PhoneNumber)];
        public string SendMessage => _localizer[nameof(SendMessage)];
        public string CarColor_Beige => _localizer[nameof(CarColor_Beige)];
        public string CarColor_Black => _localizer[nameof(CarColor_Black)];
        public string CarColor_Blue => _localizer[nameof(CarColor_Blue)];
        public string CarColor_Brown => _localizer[nameof(CarColor_Brown)];
        public string CarColor_Gray => _localizer[nameof(CarColor_Gray)];
        public string CarColor_Green => _localizer[nameof(CarColor_Green)];
        public string CarColor_Orange => _localizer[nameof(CarColor_Orange)];
        public string CarColor_Pink => _localizer[nameof(CarColor_Pink)];
        public string CarColor_Purple => _localizer[nameof(CarColor_Purple)];
        public string CarColor_Red => _localizer[nameof(CarColor_Red)];
        public string CarColor_Silver => _localizer[nameof(CarColor_Silver)];
        public string CarColor_White => _localizer[nameof(CarColor_White)];
        public string CarColor_Yellow => _localizer[nameof(CarColor_Yellow)];
        public string CarFuelType_Benzine => _localizer[nameof(CarFuelType_Benzine)];
        public string CarFuelType_Diesel => _localizer[nameof(CarFuelType_Diesel)];
        public string CarFuelType_Electric => _localizer[nameof(CarFuelType_Electric)];
        public string CarTransmission_Automatic => _localizer[nameof(CarTransmission_Automatic)];
        public string CarTransmission_Manual => _localizer[nameof(CarTransmission_Manual)];
        public string CarTransmission_Robot => _localizer[nameof(CarTransmission_Robot)];
        public string LikesHeader => _localizer[nameof(LikesHeader)];
        public string Choose => _localizer[nameof(Choose)];
        public string Clear => _localizer[nameof(Clear)];
        public string Diesel => _localizer[nameof(Diesel)];
        public string From => _localizer[nameof(From)];
        public string Message => _localizer[nameof(Message)];
        public string Name => _localizer[nameof(Name)];
        public string To => _localizer[nameof(To)];
        public string Type => _localizer[nameof(Type)];
        public string Call => _localizer[nameof(Call)];
        public string Save => _localizer[nameof(Save)];
        public string Details => _localizer[nameof(Details)];
        public string Empty => _localizer[nameof(Empty)];
        public string CustomOrderEmailSubject => _localizer[nameof(CustomOrderEmailSubject)];
        public string CustomOrderEmailBody => _localizer[nameof(CustomOrderEmailBody)];
        public string AdminCustomOrderEmailSubject => _localizer[nameof(AdminCustomOrderEmailSubject)];
        public string AdminCustomOrderEmailBody => _localizer[nameof(AdminCustomOrderEmailBody)];
        public string Delete => _localizer[nameof(Delete)];
        public string Status => _localizer[nameof(Status)];
        
        public string CarStatus_Available => _localizer[nameof(CarStatus_Available)];
        public string CarStatus_Shipping => _localizer[nameof(CarStatus_Shipping)];
        public string CarStatus_Recommended => _localizer[nameof(CarStatus_Recommended)];
    }
}
