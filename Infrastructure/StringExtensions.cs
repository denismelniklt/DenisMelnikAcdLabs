using Domain.Dto;

namespace Infrastructure
{
    public static class StringExtensions
    {
        public const string FragmentSeparator = "\t";
        public const string NumberObjectType = "0";
        public const string WordObjectType = "1";

        public static DataLineDto GetDataLineDto(this string line)
        {
            var fragments = line.Split(FragmentSeparator);
            var lineDto = new DataLineDto();

            foreach(var fragment in fragments)
            {
                var fragmentDto = new FragmentDto();

                if (decimal.TryParse(fragment, out decimal decimalFragment))
                {
                    fragmentDto.Type = NumberObjectType;
                }
                else
                {
                    fragmentDto.Type = WordObjectType;
                }

                fragmentDto.Value = fragment;

                lineDto.Fragments.Add(fragmentDto);
            }

            return lineDto;
        }
    }
}