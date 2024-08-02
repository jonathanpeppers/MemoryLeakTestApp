using System.Windows.Input;
using MemoryLeakTestApp.Cells;

namespace MemoryLeakTestApp.OneLineCellPage;

public class OneLineCellViewModel : NotifyPropertyChangedViewModel
{
    private int _badgeCount;
    private OneLineCellType _cellType;

    private bool _hasSemanticEffectForTrailingImage;

    private bool _isBusy;

    private bool _isDividerVisible;

    private bool _isEnabled;

    private bool _isSelected;

    private bool _isSwitchEnabled;

    private bool _isSwitchToggled;

    private bool _isTagVisible;

    private ImageSource? _leadingImage;

    private ImageSource? _leadingInactiveImage;

    private string _tagText;

    private ICommand _tappedCommand;

    private string _text;

    private string _trailingImageAccessibilityText;

    private ImageSource? _trailingImageSource;

    private ImageSource? _trailingInactiveImageSource;

    /// <summary>
    ///     A ctor without defining a default command.
    /// </summary>
    /// <param name="text">String</param>
    /// <param name="cellType">Of type OneLineCellType</param>
    public OneLineCellViewModel(string text, OneLineCellType cellType)
    {
        Text = text;
        CellType = cellType;
    }

    public OneLineCellViewModel(string text,
                                OneLineCellType cellType,
                                Command? tappedCommand = default,
                                string tagText = "Tag text",
                                string tappedCommandParameterValue = "",
                                ImageSource? leadingImage = null,
                                ImageSource? leadingInactiveImage = null,
                                ImageSource? trailingImageSource = null,
                                ImageSource? trailingInactiveImageSource = null,
                                bool isEnabled = true,
                                bool isDividerVisible = true,
                                bool isTagVisible = false,
                                int badgeCount = 0,
                                bool isSwitchEnabled = true,
                                bool isSwitchToggled = false,
                                bool isBusy = false,
                                bool isSelected = true,
                                string trailingImageAccessibilityText = "Image accessibility text")
    {
        Text = text;
        TagText = tagText;
        LeadingImageSource = leadingImage;
        LeadingInactiveImage = leadingInactiveImage;
        TrailingImageSource = trailingImageSource;
        TrailingInactiveImageSource = trailingInactiveImageSource;
        IsEnabled = isEnabled;
        IsDividerVisible = isDividerVisible;
        IsTagVisible = isTagVisible;
        BadgeCount = badgeCount;
        IsSwitchEnabled = isSwitchEnabled;
        IsSwitchToggled = isSwitchToggled;
        IsBusy = isBusy;
        IsSelected = isSelected;
        TrailingImageAccessibilityText = trailingImageAccessibilityText;

        CellType = cellType;

        TappedCommandParameterValue = tappedCommandParameterValue;
        TappedCommand = tappedCommand ?? new Command<string>(OpenDialog);
    }

    public OneLineCellType CellType
    {
        get => _cellType;
        set => SetProperty(ref _cellType, value);
    }

    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    public string TagText
    {
        get => _tagText;
        set => SetProperty(ref _tagText, value);
    }

    public ImageSource? LeadingImageSource
    {
        get => _leadingImage;
        set => SetProperty(ref _leadingImage, value);
    }

    public ImageSource? LeadingInactiveImage
    {
        get => _leadingInactiveImage;
        set => SetProperty(ref _leadingInactiveImage, value);
    }

    public ImageSource? TrailingImageSource
    {
        get => _trailingImageSource;
        set => SetProperty(ref _trailingImageSource, value);
    }

    public ImageSource? TrailingInactiveImageSource
    {
        get => _trailingInactiveImageSource;
        set => SetProperty(ref _trailingInactiveImageSource, value);
    }

    public bool IsSwitchToggled
    {
        get => _isSwitchToggled;
        set => SetProperty(ref _isSwitchToggled, value);
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }

    public bool IsSwitchEnabled
    {
        get => _isSwitchEnabled;
        set => SetProperty(ref _isSwitchEnabled, value);
    }

    public ICommand TappedCommand
    {
        get => _tappedCommand;
        set => SetProperty(ref _tappedCommand, value);
    }

    public string TappedCommandParameterValue { get; set; }

    public bool IsDividerVisible
    {
        get => _isDividerVisible;
        set => SetProperty(ref _isDividerVisible, value);
    }

    public bool IsTagVisible
    {
        get => _isTagVisible;
        set => SetProperty(ref _isTagVisible, value);
    }

    public int BadgeCount
    {
        get => _badgeCount;
        set => SetProperty(ref _badgeCount, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public bool HasSemanticEffectForTrailingImage
    {
        get => _hasSemanticEffectForTrailingImage;
        set => SetProperty(ref _hasSemanticEffectForTrailingImage, value);
    }

    public string TrailingImageAccessibilityText
    {
        get => _trailingImageAccessibilityText;
        set => SetProperty(ref _trailingImageAccessibilityText, value);
    }

    private void OpenDialog(string parameterValue)
    {
        Application.Current.MainPage.DisplayAlert("Info", $"Tapped on cell with parameter value: '{parameterValue}'.", "OK");
    }
}