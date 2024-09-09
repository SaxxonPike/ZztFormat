namespace ZztFormat;

[Flags]
public enum FlagOptions
{
    None = 0,
    
    /// <summary>
    /// If specified, case will be preserved when setting flags. This does not
    /// automatically imply <see cref="CaseSensitive"/>.
    /// </summary>
    PreserveCase = 1 << 0,
    
    /// <summary>
    /// If specified, case-sensitive comparisons will be done when checking
    /// for existing flag names.
    /// By default, this comparison is case-insensitive. This does not
    /// automatically imply <see cref="PreserveCase"/>.
    /// </summary>
    CaseSensitive = 1 << 1,
    
    /// <summary>
    /// If specified, duplicate flags are permitted in the collection.
    /// </summary>
    AllowDuplicates = 1 << 2
}