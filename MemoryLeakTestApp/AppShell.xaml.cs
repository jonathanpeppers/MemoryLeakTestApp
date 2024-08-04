namespace MemoryLeakTestApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        Routing.RegisterRoute(nameof(OneLineCellPage.OneLineCellPage), typeof(OneLineCellPage.OneLineCellPage));
    }
}