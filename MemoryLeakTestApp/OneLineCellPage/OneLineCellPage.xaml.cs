namespace MemoryLeakTestApp.OneLineCellPage;

public partial class OneLineCellPage : ContentPage
{
    public OneLineCellPage()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    private async void OnCounterClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}