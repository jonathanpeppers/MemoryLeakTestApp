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

    #region Fields

    private bool _wasToggleStateSetProgramatically;
    private bool _wasToggleEventSet;

    #endregion

    #region Bindable Properties

    public static readonly BindableProperty LeadingImageSourceProperty =
        TypedBindableProperty<OneLineCell>.Create<ImageSource>(nameof(LeadingImageSource),
                                                               onPropertyChanged: (cell, _, newValue) =>
                                                               {
                                                                   if (cell.IsEnabled)
                                                                   {
                                                                       cell.LeadingImageView.Source = newValue;
                                                                       cell.LeadingImageView.IsVisible = newValue != null && cell.CellType != OneLineCellType.None && !cell.IsBusy;
                                                                       cell.LeadingImageFrame.IsVisible = cell.LeadingImageView.IsVisible;
                                                                   }
                                                               });

    public static readonly BindableProperty LeadingInactiveImageSourceProperty =
        TypedBindableProperty<OneLineCell>.Create<ImageSource>(nameof(LeadingInactiveImageSource),
                                                               onPropertyChanged: (cell, _, newValue) =>
                                                               {
                                                                   if (!cell.IsEnabled)
                                                                   {
                                                                       cell.LeadingImageView.Source = newValue;
                                                                       cell.LeadingImageView.IsVisible = newValue != null && cell.CellType != OneLineCellType.None && !cell.IsBusy;
                                                                       cell.LeadingImageFrame.IsVisible = cell.LeadingImageView.IsVisible;
                                                                   }
                                                               });

    public static readonly BindableProperty CellTypeProperty =
        TypedBindableProperty<OneLineCell>.Create<OneLineCellType>(nameof(CellType),
                                                                   defaultValue: OneLineCellType.None,
                                                                   onPropertyChanged: (cell, _, newValue) =>
                                                                   {
                                                                       cell.Switch.IsVisible = newValue == OneLineCellType.Switch && !cell.IsBusy;

                                                                       SetImageViewsVisibility(cell, newValue, cell.IsBusy);

                                                                       cell.UpdateBadgeSpacing();
                                                                   });

    public static readonly BindableProperty TrailingImageSourceProperty =
        TypedBindableProperty<OneLineCell>.Create<ImageSource>(nameof(TrailingImageSource),
                                                               onPropertyChanged: (cell, _, newValue) =>
                                                               {
                                                                   if (cell.IsEnabled)
                                                                   {
                                                                       cell.TrailingImageView.Source = newValue;
                                                                       cell.TrailingImageView.IsVisible = newValue != null && cell.CellType != OneLineCellType.Switch && !cell.IsBusy;

                                                                       cell.UpdateBadgeSpacing();
                                                                   }
                                                               });

    public static readonly BindableProperty TrailingInactiveImageSourceProperty =
        TypedBindableProperty<OneLineCell>.Create<ImageSource>(nameof(TrailingInactiveImageSource),
                                                               onPropertyChanged: (cell, _, newValue) =>
                                                               {
                                                                   if (!cell.IsEnabled)
                                                                   {
                                                                       cell.TrailingImageView.Source = newValue;
                                                                       cell.TrailingImageView.IsVisible = newValue != null && cell.CellType != OneLineCellType.Switch && !cell.IsBusy;

                                                                       cell.UpdateBadgeSpacing();
                                                                   }
                                                               });

    public static readonly BindableProperty TextProperty =
        TypedBindableProperty<OneLineCell>.Create(nameof(Text),
                                                  defaultValue: string.Empty,
                                                  onPropertyChanged: (cell, _, newValue) =>
                                                  {
                                                      cell.TextLabel.Text = newValue ?? string.Empty;
                                                  });

    public static readonly BindableProperty TagTextProperty =
        TypedBindableProperty<OneLineCell>.Create(nameof(TagText),
                                                  defaultValue: string.Empty,
                                                  onPropertyChanged: (cell, _, newValue) =>
                                                  {
                                                      cell.Tag.TagText = newValue ?? string.Empty;
                                                  });

    public static readonly BindableProperty IsSwitchToggledProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsSwitchToggled),
                                                        defaultValue: false,
                                                        onPropertyChanged: (cell, _, newValue) =>
                                                                           {
                                                                               if (cell.Switch.IsToggled == (newValue ?? false))
                                                                               {
                                                                                   return;
                                                                               }

                                                                               // Trigger the "state set programatically" flag only after the toggle event was
                                                                               // subscribed, check constructor for more info on why the subscription for the
                                                                               // toggle is delayed when the cell is created
                                                                               cell._wasToggleStateSetProgramatically = cell._wasToggleEventSet;
                                                                               cell.Switch.IsToggled = newValue ?? false;
                                                                           });

    public static readonly BindableProperty IsSwitchEnabledProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsSwitchEnabled),
                                                        defaultValue: true,
                                                        onPropertyChanged: (cell, _, newValue) =>
                                                            cell.Switch.IsEnabled = newValue ?? false);

    public static readonly BindableProperty IsDividerVisibleProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsDividerVisible),
                                                        defaultValue: true,
                                                        onPropertyChanged: (cell, _, newValue) =>
                                                            cell.Divider.IsVisible = newValue ?? false);

    public static readonly BindableProperty IsTagVisibleProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsTagVisible),
                                                        defaultValue: false,
                                                        onPropertyChanged: (cell, _, newValue) =>
                                                        {
                                                            cell.Tag.IsVisible = (newValue ?? false) && !cell.IsBusy;
                                                        });

    public static readonly BindableProperty BadgeCountProperty =
        TypedBindableProperty<OneLineCell>.Create<int>(nameof(BadgeCount),
                                                       defaultValue: -1,
                                                       onPropertyChanged: (cell, _, newValue) =>
                                                       {
                                                           if (newValue.HasValue)
                                                           {
                                                               cell.Badge.Counter = newValue.Value;
                                                               cell.Badge.IsVisible = newValue > 0 && !cell.IsBusy;

                                                               cell.UpdateBadgeSpacing();
                                                           }
                                                       });

    public static readonly BindableProperty IsBusyProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsBusy),
                                                        defaultValue: false,
                                                        onPropertyChanged: (cell, _, newValue) =>
                                                        {

                                                            SetImageViewsVisibility(cell, cell.CellType, newValue ?? false);
                                                            SetTrailingControlsVisibility(cell, newValue ?? false);

                                                            cell.TextLabel.IsEnabled = cell.IsEnabled;

                                                            cell.UpdateBadgeSpacing();
                                                        });

    public static readonly BindableProperty TappedCommandProperty =
        TypedBindableProperty<OneLineCell>.Create<ICommand>(nameof(TappedCommand),
                                                            onPropertyChanged: (cell, _, _) =>
                                                            {
                                                            });

    public static readonly BindableProperty TappedCommandParameterProperty =
        TypedBindableProperty<OneLineCell>.Create<object>(nameof(TappedCommandParameter),
                                                          defaultValue: true);

    public static readonly BindableProperty IsSelectedProperty =
        TypedBindableProperty<OneLineCell>.Create<bool>(nameof(IsSelected),
                                                        defaultValue: false,
                                                        onPropertyChanged: (cell, _, _) =>
                                                        {
                                                        });

    public static readonly BindableProperty TagBackgroundColorProperty =
        TypedBindableProperty<OneLineCell>.Create(nameof(Tag),
                                                  defaultValue: (Color)Application.Current.Resources["DividerDefault"],
                                                  onPropertyChanged: (cell, _, newValue) => cell.Tag.BackgroundColor = newValue ?? (Color)Application.Current.Resources["DividerDefault"]);

    #endregion

    #region Properties

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

    #endregion

    #region Constructor

    public OneLineCell()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        TextLabel.Text = Text;
        Tag.TagText = TagText;

        LeadingImageView.IsVisible = LeadingImageView?.Source != null && CellType != OneLineCellType.None && !IsBusy;
        LeadingImageFrame.IsVisible = (LeadingImageView?.IsVisible ?? false) || IsBusy;

        // Has to be initialized on true because of a property change issue on android (MAPP-63588)
        TrailingImageView.IsVisible = CellType != OneLineCellType.Switch && !IsBusy;

        Switch.IsEnabled = IsEnabled ? IsSwitchEnabled : IsEnabled;
        Switch.IsVisible = CellType == OneLineCellType.Switch && !IsBusy;
        Switch.IsToggled = IsSwitchToggled;

        Divider.IsVisible = IsDividerVisible;
        Tag.IsVisible = IsTagVisible && !IsBusy;
        Badge.IsVisible = BadgeCount > 0 && !IsBusy;

        UpdateBadgeSpacing();
    }

    #endregion

    #region Event Methods

    /// <summary>
    /// Tap command should not be executed on cell tap if cell is of type Switch.
    /// Special case: If a switch cell is in the accessible tree, we have to toggle the switch via the tap gesture
    /// </summary>
    private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
    {
        if (CellType == OneLineCellType.Switch && IsEnabled )
        {
            Switch.IsToggled = !Switch.IsToggled;
            Switch_OnToggled(this, new ToggledEventArgs(Switch.IsToggled));

            return;
        }

        if (CellType != OneLineCellType.Switch &&
            IsEnabled &&
            TappedCommand?.CanExecute(TappedCommandParameter) == true)
        {
            TappedCommand.Execute(TappedCommandParameter);
        }
    }

    /// <summary>
    /// Switch is executing TappedCommand.
    /// </summary>
    private void Switch_OnToggled(object? sender, ToggledEventArgs e)
    {
        if (_wasToggleStateSetProgramatically)
        {
            _wasToggleStateSetProgramatically = false;

            return;
        }

        // Workaround to fix the switch for modules using the Event Handler instead of TappedCommand.
        // We should move this line to the if below when every module will use TappedCommand
        IsSwitchToggled = Switch.IsToggled;

        if (IsEnabled && TappedCommand?.CanExecute(IsSwitchToggled) == true)
        {
            TappedCommand.Execute(Switch.IsToggled);
        }
    }

    #endregion

    #region Property Change Methods

    private void UpdateBadgeSpacing() =>
        Badge.Margin = new Thickness(left: 0,
                                     top: 0,
                                     right: TrailingImageView.IsVisible || Switch.IsVisible ? 6 : 16,
                                     bottom: 0);

    private static void SetImageViewsVisibility(OneLineCell cell, OneLineCellType? cellType, bool isBusy)
    {
        cell.LeadingImageView.IsVisible = cell.LeadingImageView?.Source != null && cellType != OneLineCellType.None && !isBusy;
        cell.LeadingImageFrame.IsVisible = (cell.LeadingImageView?.IsVisible ?? false) || isBusy;
        cell.TrailingImageView.IsVisible = cell.TrailingImageView?.Source != null && cellType != OneLineCellType.Switch && !isBusy;
    }

    private static void SetTrailingControlsVisibility(OneLineCell cell, bool isBusy)
    {
        cell.Tag.IsVisible = cell.IsTagVisible && !isBusy;
        cell.Badge.IsVisible = cell.BadgeCount > 0 && !isBusy;

        cell.Switch.IsVisible = cell.CellType == OneLineCellType.Switch && !isBusy;
    }


    #endregion

    #region VisualElement Override Methods

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName is nameof(IsEnabled) && !IsBusy)
        {
            TextLabel.IsEnabled = IsEnabled;
            Switch.IsEnabled = IsEnabled;
        }
    }

    protected override void ChangeVisualState()
    {
        base.ChangeVisualState();

        Switch.IsVisible = CellType == OneLineCellType.Switch && !IsBusy;

        LeadingImageView.Source = IsEnabled ? LeadingImageSource : LeadingInactiveImageSource;
        LeadingImageView.IsVisible = LeadingImageView?.Source != null && CellType != OneLineCellType.None && !IsBusy;

        LeadingImageFrame.IsVisible = (LeadingImageView?.IsVisible ?? false) || IsBusy;

        TrailingImageView.Source = IsEnabled ? TrailingImageSource : TrailingInactiveImageSource;
        TrailingImageView.IsVisible = TrailingImageView?.Source != null && CellType != OneLineCellType.Switch && !IsBusy;

        UpdateBadgeSpacing();
    }

    #endregion
}