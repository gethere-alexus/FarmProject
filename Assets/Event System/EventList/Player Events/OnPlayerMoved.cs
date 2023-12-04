using System;
public class OnPlayerMoved : EventArgs
{
    public float PlayerPositionX, PlayerPositionY;
    public float HorizontalInput;

    public OnPlayerMoved(float x, float y, float horizontalInput)
    {
        PlayerPositionX = x;
        PlayerPositionY = y;

        HorizontalInput = horizontalInput;
    }
}
