using System.Globalization;

namespace MemoryLeakTestApp.Views;

public partial class Badge
{
    #region Bindable Properties

    public static readonly BindableProperty FillColorProperty = TypedBindableProperty<Badge>.Create<Brush>(
        nameof(FillColor),
        defaultBindingMode: BindingMode.OneWay,
        defaultValue: new SolidColorBrush((Color)Application.Current.Resources["Default"]),
        onPropertyChanged: (badge, _, newValue) =>
        {
            badge.Ellipse.Fill = newValue ?? new SolidColorBrush((Color)Application.Current.Resources["Default"]);
        });

    public static readonly BindableProperty SizeProperty = TypedBindableProperty<Badge>.Create<double>(nameof(Size),
        defaultBindingMode: BindingMode.OneWay,
        onPropertyChanged: (badge, _, newValue) =>
        {
            if (!newValue.HasValue)
            {
                return;
            }

            badge.Ellipse.HeightRequest = newValue.Value;
            badge.Ellipse.WidthRequest = newValue.Value;
        });

    public static readonly BindableProperty CounterProperty = TypedBindableProperty<Badge>.Create<int>(nameof(Counter),
        defaultBindingMode: BindingMode.TwoWay,
        onPropertyChanged: (badge, _, newValue) =>
        {
            if (newValue.HasValue)
            {
                badge.CounterLabel.Text = GetCounterText(newValue.Value);
            }
        });

    #endregion

    #region Properties

    public Brush? FillColor
    {
        get => (Brush)GetValue(FillColorProperty);
        set => SetValue(FillColorProperty, value);
    }

    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public int Counter
    {
        get => (int)GetValue(CounterProperty);
        set => SetValue(CounterProperty, value);
    }

    #endregion

    #region Constructor

    public Badge()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        CounterLabel.Text = GetCounterText(Counter);
    }

    #endregion

    #region Methods

    private static string GetCounterText(int counter)
    {
        return counter > 99
            ? "99+"
            : counter.ToString(CultureInfo.InvariantCulture);
    }

    #endregion
}