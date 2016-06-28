using Core.Resources;

namespace Core.Diagnostics.Resources
{
    internal class Strings : StringResource<Strings>
    {
        public static string NonEmptyCollection => GetString(nameof(NonEmptyCollection));

        public static string NonEmptyString => GetString(nameof(NonEmptyString));

        public static string NonNullItems => GetString(nameof(NonNullItems));

        public static string Positive => GetString(nameof(Positive));

        public static string NonPositive => GetString(nameof(NonPositive));

        public static string Negative => GetString(nameof(Negative));

        public static string NonNegative => GetString(nameof(NonNegative));
    }
}
