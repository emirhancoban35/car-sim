public class Pedal
{
    public Pedals PedalName { get; private set; }
    public bool IsPressed { get; private set; }
    
    public Pedal(Pedals pedalName)
    {
        PedalName = pedalName;
        IsPressed = false;
    }
    
    public void Press() => IsPressed = true;
    public void Release() => IsPressed = false;
}