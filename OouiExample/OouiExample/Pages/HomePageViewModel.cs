namespace OouiExample.Pages
{
    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Forms.ViewModels;

    public class HomePageViewModel : ViewModelBase
    {
        public NotificationValue<int> Counter { get; } = new NotificationValue<int>();

        public DelegateCommand IncrementCommand { get; }

        public HomePageViewModel()
        {
            IncrementCommand = MakeDelegateCommand(() => Counter.Value++);
        }
    }
}
