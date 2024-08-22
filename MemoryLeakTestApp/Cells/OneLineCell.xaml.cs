using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MemoryLeakTestApp.Cells;

public enum OneLineCellType
{
    None = 0,
    Switch = 1,
    Icon = 2
}

public partial class OneLineCell
{

    public static readonly BindableProperty LeadingImageSourceProperty =
        TypedBindableProperty<OneLineCell>.Create<ImageSource>(nameof(LeadingImageSource));

    public static readonly BindableProperty LeadingInactiveImageSourceProperty =
        TypedBindableProperty<OneLineCell>.Create<ImageSource>(nameof(LeadingInactiveImageSource));

    public static readonly BindableProperty CellTypeProperty =
        TypedBindableProperty<OneLineCell>.Create<OneLineCellType>(nameof(CellType),
                                                                   defaultValue: OneLineCellType.None);

    public static readonly BindableProperty TrailingImageSourceProperty =
        TypedBindableProperty<OneLineCell>.Create<ImageSource>(nameof(TrailingImageSource));

    public static readonly BindableProperty TrailingInactiveImageSourceProperty =
        TypedBindableProperty<OneLineCell>.Create<ImageSource>(nameof(TrailingInactiveImageSource));

    public static readonly BindableProperty TextProperty =
        TypedBindableProperty<OneLineCell>.Create(nameof(Text),
                                                  defaultValue: string.Empty);

    public static readonly BindableProperty TagTextProperty =
        TypedBindableProperty<OneLineCell>.Create(nameof(TagText),
                                                  defaultValue: string.Empty);

    public static readonly BindableProperty IsSwitchToggledProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsSwitchToggled),
                                                        defaultValue: false);

    public static readonly BindableProperty IsSwitchEnabledProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsSwitchEnabled),
                                                        defaultValue: true);

    public static readonly BindableProperty IsDividerVisibleProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsDividerVisible),
                                                        defaultValue: true);

    public static readonly BindableProperty IsTagVisibleProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsTagVisible),
                                                        defaultValue: false);

    public static readonly BindableProperty BadgeCountProperty =
        TypedBindableProperty<OneLineCell>.Create<int>(nameof(BadgeCount),
                                                       defaultValue: -1);

    public static readonly BindableProperty IsBusyProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsBusy),
                                                        defaultValue: false);

    public static readonly BindableProperty TappedCommandProperty =
        TypedBindableProperty<OneLineCell>.Create<ICommand>(nameof(TappedCommand));

    public static readonly BindableProperty TappedCommandParameterProperty =
        TypedBindableProperty<OneLineCell>.Create<object>(nameof(TappedCommandParameter),
                                                          defaultValue: true);

    public static readonly BindableProperty IsSelectedProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsSelected),
                                                        defaultValue: false);

    public static readonly BindableProperty TagBackgroundColorProperty =
        TypedBindableProperty<OneLineCell>.Create(nameof(TagBackgroundColor),
                                                  defaultValue: (Color)Application.Current.Resources["DividerDefault"]);

    public ImageSource? LeadingImageSource
    {
        get => (ImageSource?)GetValue(LeadingImageSourceProperty);
        set => SetValue(LeadingImageSourceProperty, value);
    }

    public ImageSource? LeadingInactiveImageSource
    {
        get => (ImageSource?)GetValue(LeadingInactiveImageSourceProperty);
        set => SetValue(LeadingInactiveImageSourceProperty, value);
    }

    public OneLineCellType CellType
    {
        get => (OneLineCellType)GetValue(CellTypeProperty);
        set => SetValue(CellTypeProperty, value);
    }

    public ImageSource? TrailingImageSource
    {
        get => (ImageSource?)GetValue(TrailingImageSourceProperty);
        set => SetValue(TrailingImageSourceProperty, value);
    }

    public ImageSource? TrailingInactiveImageSource
    {
        get => (ImageSource?)GetValue(TrailingInactiveImageSourceProperty);
        set => SetValue(TrailingInactiveImageSourceProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string TagText
    {
        get => (string)GetValue(TagTextProperty);
        set => SetValue(TagTextProperty, value);
    }

    public bool IsSwitchToggled
    {
        get => (bool)GetValue(IsSwitchToggledProperty);
        set => SetValue(IsSwitchToggledProperty, value);
    }

    public bool IsSwitchEnabled
    {
        get => (bool)GetValue(IsSwitchEnabledProperty);
        set => SetValue(IsSwitchEnabledProperty, value);
    }

    public bool IsDividerVisible
    {
        get => (bool)GetValue(IsDividerVisibleProperty);
        set => SetValue(IsDividerVisibleProperty, value);
    }

    public bool IsTagVisible
    {
        get => (bool)GetValue(IsTagVisibleProperty);
        set => SetValue(IsTagVisibleProperty, value);
    }

    public int BadgeCount
    {
        get => (int)GetValue(BadgeCountProperty);
        set => SetValue(BadgeCountProperty, value);
    }

    public ICommand? TappedCommand
    {
        get => (ICommand?)GetValue(TappedCommandProperty);
        set => SetValue(TappedCommandProperty, value);
    }

    public object? TappedCommandParameter
    {
        get => GetValue(TappedCommandParameterProperty);
        set => SetValue(TappedCommandParameterProperty, value);
    }

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public Color TagBackgroundColor
    {
        get => (Color)GetValue(TagBackgroundColorProperty);
        set => SetValue(TagBackgroundColorProperty, value);
    }
}