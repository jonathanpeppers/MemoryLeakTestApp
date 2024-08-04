namespace MemoryLeakTestApp.Badges;

public partial class Tag
{
    #region Bindable Properties

    public static readonly BindableProperty TagTextProperty =
        TypedBindableProperty<Tag>.Create(nameof(TagText),
            defaultValue: string.Empty,
            onPropertyChanged: (tag, _, newValue) => tag.TagTextLabel.Text = newValue ?? string.Empty);
    #endregion

    #region Properties

    public string TagText
    {
        get => (string)GetValue(TagTextProperty);
        set => SetValue(TagTextProperty, value);
    }

    #endregion

    #region Constructor

    public Tag()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    #endregion
}