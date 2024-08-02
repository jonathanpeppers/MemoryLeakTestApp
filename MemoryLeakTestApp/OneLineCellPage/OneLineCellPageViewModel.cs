using System.Reflection;
using System.Windows.Input;
using MemoryLeakTestApp.Cells;

namespace MemoryLeakTestApp.OneLineCellPage;

public class OneLineCellPageViewModel : NotifyPropertyChangedViewModel
{
    public OneLineCellViewModel Cell { get; }

    private bool _isCommandAttached;
    private int _selectedCellTypeIndex = 2;
    private int _selectedLeadingImageIndex;
    private int _selectedLeadingInactiveImageIndex;
    private int _selectedTrailingImageIndex;
    private int _selectedTrailingInactiveImageIndex;
    private OneLineCellType _cellType = OneLineCellType.Icon;
    private ICommand? _command;

    public string[] CellTypes => Enum.GetNames(typeof(OneLineCellType));

    public ICommand IncreaseBadgeCountCommand => new Command(ExecuteIncreaseBadgeCountCommand);
    public ICommand DecreaseBadgeCountCommand => new Command(ExecuteDecreaseBadgeCountCommand);
    

    public bool IsCommandAttached
    {
        get => _isCommandAttached;
        set
        {
            SetProperty(ref _isCommandAttached, value);
            Command = _isCommandAttached ? new Command(ExecuteCommand) : null;
        }
    }

    public OneLineCellType CellType
    {
        get => _cellType;
        set
        {
            if (SetProperty(ref _cellType, value))
            {
                Cell.CellType = _cellType;
            }
        }
    }

    public int SelectedCellTypeIndex
    {
        get => _selectedCellTypeIndex;
        set
        {
            if (SetProperty(ref _selectedCellTypeIndex, value))
            {
                CellType = (OneLineCellType)value;
            }
        }
    }

    public int SelectedLeadingImageIndex
    {
        get => _selectedLeadingImageIndex;
        set
        {
            if (SetProperty(ref _selectedLeadingImageIndex, value))
            {
            }
        }
    }

    public int SelectedLeadingInactiveImageIndex
    {
        get => _selectedLeadingInactiveImageIndex;
        set
        {
            if (SetProperty(ref _selectedLeadingInactiveImageIndex, value))
            {
            }
        }
    }

    public int SelectedTrailingImageIndex
    {
        get => _selectedTrailingImageIndex;
        set
        {
            if (SetProperty(ref _selectedTrailingImageIndex, value))
            {
                Cell.IsSelected = Cell.TrailingImageSource != null;
            }
        }
    }

    public int SelectedTrailingInactiveImageIndex
    {
        get => _selectedTrailingInactiveImageIndex;
        set
        {
            if (SetProperty(ref _selectedTrailingInactiveImageIndex, value))
            {
                Cell.IsSelected = Cell.TrailingImageSource != null;
            }
        }
    }


    static async Task NavigateBack()
    {
        await Shell.Current.GoToAsync("..");
    }
    
    public ICommand? Command
    {
        get => _command;
        set => SetProperty(ref _command, value);
    }
    public OneLineCellPageViewModel()
    {
        Cell = new OneLineCellViewModel("One Line Cell",
                                        CellType,
                                        tagText: "Tag",
                                        isEnabled: true,
                                        tappedCommandParameterValue: "One line cell",
                                        isBusy: false);
    }



    public void ExecuteIncreaseBadgeCountCommand() => Cell.BadgeCount++;

    public void ExecuteDecreaseBadgeCountCommand()
    {
        if (Cell.BadgeCount == 0)
        {
            return;
        }

        Cell.BadgeCount--;
    }

    public void ExecuteCommand()
    {
        if (_cellType == OneLineCellType.Switch)
        {
            Application.Current.MainPage.DisplayAlert("Info", "Switch was tapped.", "OK");
        }
        else
        {
            Application.Current.MainPage.DisplayAlert("Info", "Tapped on cell.", "OK");
        }
    }
}