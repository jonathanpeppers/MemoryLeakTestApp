namespace MemoryLeakTestApp.OneLineCellPage;

public partial class OneLineCellPage : ContentPage
{
    public OneLineCellPage()
    {
        InitializeComponent();
    }
    private async void OnCounterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}