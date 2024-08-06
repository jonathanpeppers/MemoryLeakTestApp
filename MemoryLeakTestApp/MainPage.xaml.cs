namespace MemoryLeakTestApp;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        try
        {
            InitializeComponent();
            GC.Collect();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(OneLineCellPage));
            GC.Collect();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        
    }
}