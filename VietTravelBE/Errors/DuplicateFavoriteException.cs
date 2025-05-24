namespace VietTravelBE.Errors
{
    public class DuplicateFavoriteException: Exception
    {
        public DuplicateFavoriteException(string message) : base(message) { }
    }
}
