namespace MagicVillaAPI.Utils
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtcDate(this DateOnly date)
            => DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
    }
}
