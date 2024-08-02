using MemoryLeakTestApp.Styles;

namespace MemoryLeakTestApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        // Resources.MergedDictionaries.Add(new MemoryLeakTestApp.Styles.LabelStyles());
    }
}