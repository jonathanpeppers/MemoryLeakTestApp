namespace MemoryLeakTestApp;

/// <summary>
/// Static helper class for creating <see cref="BindableProperty" /> in a type safe way.
/// </summary>
/// <typeparam name="TBindable">Type of the bindable object</typeparam>
public static class TypedBindableProperty<TBindable> where TBindable : BindableObject
{
    /// <summary>
    /// Property changed handler delegate for class typed properties.
    /// </summary>
    /// <param name="bindable">The bindable</param>
    /// <param name="oldValue">Old value</param>
    /// <param name="newValue">New value</param>
    /// <typeparam name="TProperty">Property type</typeparam>
    public delegate void ClassPropertyChangedDelegate<in TProperty>(TBindable bindable, TProperty? oldValue, TProperty? newValue)
        where TProperty : class;

    /// <summary>
    /// Property changed handler delegate for struct typed properties.
    /// </summary>
    /// <param name="bindable">The bindable</param>
    /// <param name="oldValue">Old value</param>
    /// <param name="newValue">New value</param>
    /// <typeparam name="TProperty">Property type</typeparam>
    public delegate void StructPropertyChangedDelegate<TProperty>(TBindable bindable, TProperty? oldValue, TProperty? newValue)
        where TProperty : struct;

    /// <summary>
    /// Create a <see cref="BindableProperty" /> for class typed properties.
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="defaultValue">default property value</param>
    /// <param name="defaultBindingMode">Default binding mode</param>
    /// <param name="onPropertyChanged">Property change handler</param>
    /// <typeparam name="TProperty">Property type</typeparam>
    /// <returns>A BindableProperty</returns>
    public static BindableProperty Create<TProperty>(string name,
                                                     TProperty? defaultValue = default,
                                                     BindingMode defaultBindingMode = BindingMode.OneWay,
                                                     ClassPropertyChangedDelegate<TProperty>? onPropertyChanged = default)
        where TProperty : class
    {
        return BindableProperty.Create(name,
                                       typeof(TProperty),
                                       typeof(TBindable),
                                       defaultValue,
                                       defaultBindingMode,
                                       propertyChanged: (bindable, oldValue, newValue) =>
                                       {
                                           if (onPropertyChanged == default || bindable is not TBindable typedBindable)
                                           {
                                               return;
                                           }

                                           onPropertyChanged(typedBindable, oldValue as TProperty, newValue as TProperty);
                                       });
    }

    /// <summary>
    /// Create a <see cref="BindableProperty" /> for struct typed properties.
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="defaultValue">default property value</param>
    /// <param name="defaultBindingMode">Default binding mode</param>
    /// <param name="onPropertyChanged">Property change handler</param>
    /// <typeparam name="TProperty">Property type</typeparam>
    /// <returns>A BindableProperty</returns>
    public static BindableProperty Create<TProperty>(string name,
                                                     TProperty? defaultValue = default,
                                                     BindingMode defaultBindingMode = BindingMode.OneWay,
                                                     StructPropertyChangedDelegate<TProperty>? onPropertyChanged = default)
        where TProperty : struct
    {
        return BindableProperty.Create(name,
                                       typeof(TProperty),
                                       typeof(TBindable),
                                       defaultValue,
                                       defaultBindingMode,
                                       propertyChanged: (bindable, oldValue, newValue) =>
                                       {
                                           if (onPropertyChanged == default || bindable is not TBindable typedBindable)
                                           {
                                               return;
                                           }

                                           onPropertyChanged(typedBindable, oldValue as TProperty?, newValue as TProperty?);
                                       });
    }
}