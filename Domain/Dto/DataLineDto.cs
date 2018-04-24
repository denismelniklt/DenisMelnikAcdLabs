using System.Collections.Generic;

namespace Domain.Dto
{
    public class DataLineDto : Base
    {
        public IList<FragmentDto> Fragments = new List<FragmentDto>();
        public long Weight { get; set; }
    }
}