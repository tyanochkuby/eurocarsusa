namespace EuroCarsUSA.Services.Interfaces
{
    public interface ICookieService
    {
        public List<Guid> GetUserLikedCars();
        public List<Guid> GetUserViewedCars();
        public void UpdateUserLikedCars(List<Guid> carsIds);
        public void UpdateUserViewedCars(List<Guid> carsIds);
    }
}
