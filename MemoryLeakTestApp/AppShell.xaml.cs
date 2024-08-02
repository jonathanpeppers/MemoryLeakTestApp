namespace MemoryLeakTestApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(OneLineCellPage.OneLineCellPage), typeof(OneLineCellPage.OneLineCellPage));
    }
}