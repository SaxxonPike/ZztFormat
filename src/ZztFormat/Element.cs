using JetBrains.Annotations;

namespace ZztFormat;

/// <summary>
/// Describes intrinsic properties and behaviors of a game element.
/// </summary>
[PublicAPI]
public partial class Element
{
    /// <summary>
    /// Index# of the element.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Known mapping for the element. This will be
    /// <see cref="ElementType.Unknown"/> for elements that are undefined.
    /// </summary>
    public ElementType Type { get; init; }

    /// <summary>
    /// If no special draw function is specified, this is the character
    /// that will be shown on screen for this element.
    /// </summary>
    public int Character { get; set; }

    /// <summary>
    /// Determines the default color for the element. Some values have
    /// special meaning. A value less than 0xF0 will force the element to have
    /// the specific color value when plotted in the editor or via a script.
    /// Values 0xF0 to 0xFD will have a default color in the editor, but scripts
    /// can specify color explicitly. A value of 0xFE indicates a background
    /// dominant default color where foreground is set to white (so 0x04 would
    /// translate to 0x4F.) A value of 0xFF indicates that there is no default
    /// color, and that it can be specified with the editor color picker.
    /// </summary>
    public int Color { get; set; }

    /// <summary>
    /// If true, this element can be destroyed by projectiles such as bullets,
    /// stars and explosions from bombs.
    /// </summary>
    public bool IsDestructible { get; set; }

    /// <summary>
    /// If true, this element can be pushed by pushers, sliders or scripted
    /// objects that move into its occupied tile.
    /// </summary>
    public bool IsPushable { get; set; }

    /// <summary>
    /// If true, this element is always drawn, regardless whether it is
    /// within the torchlight radius on a dark board.
    /// </summary>
    public bool IsAlwaysVisible { get; set; }

    /// <summary>
    /// If true, the editor will allow placement of actors on top. This includes
    /// floors and fakes, but also includes water for the purpose of placing
    /// sharks.
    /// </summary>
    public bool IsEditorFloor { get; set; }

    /// <summary>
    /// If true, this element functions as a floor upon which actors can move.
    /// </summary>
    public bool IsFloor { get; set; }

    /// <summary>
    /// Indicates the default cycle setting for an actor. See
    /// <see cref="Actor.Cycle"/> for more information on how this value works.
    /// </summary>
    public int Cycle { get; set; }

    /// <summary>
    /// Number of the menu under which this element is listed in the editor.
    /// A negative value indicates this element is not listed in editor menus.
    /// </summary>
    public int Menu { get; set; }

    /// <summary>
    /// Keyboard key that will need to be typed in order to select this element
    /// in the editor.
    /// </summary>
    public char MenuKey { get; set; }

    /// <summary>
    /// In-game name of the element. Not all elements will have a name, but this
    /// is required in order to be eligible for use in scripts.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// When rendered in the editor menu, this text will be shown as a category
    /// header.
    /// </summary>
    public string EditorCategoryText { get; set; } = string.Empty;

    /// <summary>
    /// Text that is displayed when editing actor parameters.
    /// </summary>
    public string EditorP1Text { get; set; } = string.Empty;

    /// <summary>
    /// Text that is displayed when editing actor parameters.
    /// </summary>
    public string EditorP2Text { get; set; } = string.Empty;

    /// <summary>
    /// Text that is displayed when editing actor parameters.
    /// </summary>
    public string EditorP3Text { get; set; } = string.Empty;

    /// <summary>
    /// If an actor with this element is being edited, text here will cause
    /// a board selection window to show.
    /// </summary>
    public string EditorBoardText { get; set; } = string.Empty;
    
    /// <summary>
    /// Text that is displayed when editing actor parameters. The available
    /// options are limited to the four cardinal directions.
    /// </summary>
    public string EditorStepText { get; set; } = string.Empty;
    
    /// <summary>
    /// If an actor with this element is being edited, text here will cause
    /// a script editor to show after parameters have been edited.
    /// </summary>
    public string EditorCodeText { get; set; } = string.Empty;
    
    /// <summary>
    /// Number of points added to the player's score when collected as an item.
    /// </summary>
    public int Score { get; set; }
}