namespace LibRoton;

public partial class Actor
{
    public bool IsBound =>
        Length < 0;

    public void Bind(int index)
    {
        Length = -index;
        Script = [];
    }

    public void Unbind()
    {
        if (Length < 0)
            return;

        Length = 0;
        Script = [];
    }
}