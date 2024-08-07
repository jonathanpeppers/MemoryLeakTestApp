namespace MemoryLeakTestApp.Views;

public partial class Divider
{
    public Divider()
    {
        if (Application.Current.Resources["DividerDefault"] is Color dividerDefault)
        {
            BackgroundColor = dividerDefault;
            Stroke = dividerDefault;
        }

        try
        {
            InitializeComponent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}