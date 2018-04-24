using ViewModels;

namespace Infrastructure
{
    public static class DataLineViewModelExtensions
    {
        public const string FragmentSeparator = "\t";

        public static string GetString(this DataLineViewModel dataLineViewModel)
        {
            var result = string.Empty;

            foreach(var fragment in dataLineViewModel.Fragments)
            {
                result = string.Concat(result, fragment.Value, FragmentSeparator);
            }

            return result;
        }
    }
}