namespace MemoryLeakTestApp;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        try
        {
            InitializeComponent();
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
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        
    }
}