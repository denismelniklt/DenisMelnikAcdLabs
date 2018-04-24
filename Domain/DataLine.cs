using System.Collections.Generic;

namespace Domain
{
    public class DataLine : Base
    {
        public IList<Fragment> Fragments = new List<Fragment>();
    }
}