
namespace MemoryLeakTestApp;

public partial class App : Application
{
    public App()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        MainPage = new AppShell();
    }
}