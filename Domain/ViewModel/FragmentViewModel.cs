namespace ViewModels
{
    public class FragmentViewModel : BaseViewModel
    {
        public FragmentTypeViewModel Type { get; set; } = new FragmentTypeViewModel();
        public string Value { get; set; }
    }
}