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
        public string GetValue(string key) => _localizer[key];
        public string ToLocalizedString(Enum enumValue) => _localizer[$"{enumValue.GetType().Name}_{enumValue}"];


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
        public string SortOrderType_NewFirst => _localizer[nameof(SortOrderType_NewFirst)];
        public string SortOrderType_ByYear => _localizer[nameof(SortOrderType_ByYear)];
        public string SortOrderType_ByYearDesc => _localizer[nameof(SortOrderType_ByYearDesc)];
        public string SortOrderType_ByMileage => _localizer[nameof(SortOrderType_ByMileage)];
        public string SortOrderType_ByPrice => _localizer[nameof(SortOrderType_ByPrice)];
        public string SortOrderType_ByPriceDesc => _localizer[nameof(SortOrderType_ByPriceDesc)];
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
        public string Gearbox => _localizer[nameof(Gearbox)];
        public string BodyType => _localizer[nameof(BodyType)];
        public string EditImages => _localizer[nameof(EditImages)];
        public string AddImage => _localizer[nameof(AddImage)];
        public string UploadImage => _localizer[nameof(UploadImage)];
        public string Add => _localizer[nameof(Add)];
        public string EnterImageURL => _localizer[nameof(EnterImageURL)];
        public string LastAddedAuto => _localizer[nameof(LastAddedAuto)];
        public string WeRecommend => _localizer[nameof(WeRecommend)];
        public string SeeAll => _localizer[nameof(SeeAll)];
        public string AboutUs => _localizer[nameof(AboutUs)];
        public string AboutUsText => _localizer[nameof(AboutUsText)];
        public string PhoneContact => _localizer[nameof(PhoneContact)];
        public string PhoneContactText => _localizer[nameof(PhoneContactText)];
        public string FormContact => _localizer[nameof(FormContact)];
        public string FormContactText => _localizer[nameof(FormContactText)];
        public string IndividualOrder => _localizer[nameof(IndividualOrder)];
        public string Likes => _localizer[nameof(Likes)];
        public string Views => _localizer[nameof(Views)];
        public string Statistics => _localizer[nameof(Statistics)];
        public string CustomOrderText => _localizer[nameof(CustomOrderText)];
        public string CustomOrder => _localizer[nameof(CustomOrder)];
        public string TrackYourOrder => _localizer[nameof(TrackYourOrder)];
        public string TrackOrder => _localizer[nameof(TrackOrder)];
        public string CreateYourOrder => _localizer[nameof(CreateYourOrder)];
        public string AddForm => _localizer[nameof(AddForm)];
        public string SubmitForm => _localizer[nameof(SubmitForm)];
        public string Form => _localizer[nameof(Form)];
        public string MaxPrice => _localizer[nameof(MaxPrice)];
        public string MaxMileage => _localizer[nameof(MaxMileage)];
        public string MinYear => _localizer[nameof(MinYear)];
        public string MaxYear => _localizer[nameof(MaxYear)];
        public string Description => _localizer[nameof(Description)];
        public string ClientOrders => _localizer[nameof(ClientOrders)];
        public string NotProvided => _localizer[nameof(NotProvided)];
        public string DateAdded => _localizer[nameof(DateAdded)];
        public string OrderStatus_Sent => _localizer[nameof(OrderStatus_Sent)];
        public string OrderStatus_Opened => _localizer[nameof(OrderStatus_Opened)];
        public string OrderStatus_Closed => _localizer[nameof(OrderStatus_Closed)];
        public string ContactData => _localizer[nameof(ContactData)];
        public string Catalog => _localizer[nameof(Catalog)];
        public string MainPage => _localizer[nameof(MainPage)];
        public string CatalogEdition => _localizer[nameof(CatalogEdition)];
        public string Privacy => _localizer[nameof(Privacy)];
        public string ProvideContactData => _localizer[nameof(ProvideContactData)];
        public string ShippingCars => _localizer[nameof(ShippingCars)];
        public string ThanksText => _localizer[nameof(ThanksText)];
        public string YourTrackNumberIs => _localizer[nameof(YourTrackNumberIs)];
        public string Copy => _localizer[nameof(Copy)];
        public string GoTrackOrder => _localizer[nameof(GoTrackOrder)];
        public string NoOrderWithSuchId => _localizer[nameof(NoOrderWithSuchId)];
        public string IdIsNotValid => _localizer[nameof(IdIsNotValid)];
        public string DetailPageEmailSubject => _localizer[nameof(DetailPageEmailSubject)];
        public string DetailPageEmailBody => _localizer[nameof(DetailPageEmailBody)];
        public string AdminDetailPageEmailSubject => _localizer[nameof(AdminDetailPageEmailSubject)];
        public string AdminDetailPageEmailBody => _localizer[nameof(AdminDetailPageEmailBody)];
        public string EmptyEmailField => _localizer[nameof(EmptyEmailField)];
        public string SoldCars => _localizer[nameof(SoldCars)];
        public string NoCarsFound => _localizer[nameof(NoCarsFound)];

        /// <summary>
        /// Drag and drop or click here
        /// </summary>
        public string ImageEditor1 => _localizer[nameof(ImageEditor1)];

        /// <summary>
        /// to upload image
        /// </summary>
        public string ImageEditor2 => _localizer[nameof(ImageEditor2)];

        /// <summary>
        /// Upload any images from desktop
        /// </summary>
        public string ImageEditor3 => _localizer[nameof(ImageEditor3)];
        public string PrivacyPolicy => _localizer[nameof(PrivacyPolicy)];
        public string Introduction => _localizer[nameof(Introduction)];
        public string IntroductionText => _localizer[nameof(IntroductionText)];
        public string WhatDataWeCollect => _localizer[nameof(WhatDataWeCollect)];
        public string WhatDataWeCollectText => _localizer[nameof(WhatDataWeCollectText)];
        public string WhenUsingForms => _localizer[nameof(WhenUsingForms)];
        public string EmailAddress => _localizer[nameof(EmailAddress)];
        public string WhenUsingWebsiteFunctionality => _localizer[nameof(WhenUsingWebsiteFunctionality)];
        public string CookiesText => _localizer[nameof(CookiesText)];
        public string AutomaticallyCollectedData => _localizer[nameof(AutomaticallyCollectedData)];
        public string IPAddress => _localizer[nameof(IPAddress)];
        public string DeviceAndBrowserType => _localizer[nameof(DeviceAndBrowserType)];
        public string ReCaptchaData => _localizer[nameof(ReCaptchaData)];
        public string HowWeUseData => _localizer[nameof(HowWeUseData)];
        public string HowWeUseDataText => _localizer[nameof(HowWeUseDataText)];
        public string ProcessingRequests => _localizer[nameof(ProcessingRequests)];
        public string CommunicatingWithUsers => _localizer[nameof(CommunicatingWithUsers)];
        public string EnsuringWebsiteSecurity => _localizer[nameof(EnsuringWebsiteSecurity)];
        public string PersonalizingUserExperience => _localizer[nameof(PersonalizingUserExperience)];
        public string Cookies => _localizer[nameof(Cookies)];
        public string CookiesUsage => _localizer[nameof(CookiesUsage)];
        public string SavingFavoriteVehicles => _localizer[nameof(SavingFavoriteVehicles)];
        public string AnalyzingWebsitePerformance => _localizer[nameof(AnalyzingWebsitePerformance)];
        public string ManageCookies => _localizer[nameof(ManageCookies)];
        public string HowWeProtectData => _localizer[nameof(HowWeProtectData)];
        public string HowWeProtectDataText => _localizer[nameof(HowWeProtectDataText)];
        public string DataEncryption => _localizer[nameof(DataEncryption)];
        public string HTTPSProtocol => _localizer[nameof(HTTPSProtocol)];
        public string ReCaptchaProtection => _localizer[nameof(ReCaptchaProtection)];
        public string SharingDataWithThirdParties => _localizer[nameof(SharingDataWithThirdParties)];
        public string SharingDataWithThirdPartiesText => _localizer[nameof(SharingDataWithThirdPartiesText)];
        public string WhenRequiredByLaw => _localizer[nameof(WhenRequiredByLaw)];
        public string WhenNecessaryToTransferData => _localizer[nameof(WhenNecessaryToTransferData)];
        public string YourRights => _localizer[nameof(YourRights)];
        public string YourRightsText => _localizer[nameof(YourRightsText)];
        public string RequestAccessToData => _localizer[nameof(RequestAccessToData)];
        public string CorrectOrDeleteData => _localizer[nameof(CorrectOrDeleteData)];
        public string RestrictProcessing => _localizer[nameof(RestrictProcessing)];
        public string ObjectToProcessing => _localizer[nameof(ObjectToProcessing)];
        public string TransferData => _localizer[nameof(TransferData)];
        public string ExerciseRights => _localizer[nameof(ExerciseRights)];
        public string DataRetention => _localizer[nameof(DataRetention)];
        public string DataRetentionText => _localizer[nameof(DataRetentionText)];
        public string ContactInformation => _localizer[nameof(ContactInformation)];
        public string ContactInformationText => _localizer[nameof(ContactInformationText)];
        public string ChangesToPrivacyPolicy => _localizer[nameof(ChangesToPrivacyPolicy)];
        public string ChangesToPrivacyPolicyText => _localizer[nameof(ChangesToPrivacyPolicyText)];
        public string LastUpdated => _localizer[nameof(LastUpdated)];
    }
}
