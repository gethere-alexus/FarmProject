using System;

public class OnMovementActionPerformed : EventArgs
{
    public float HorizontalInput, VerticalInput;

    public OnMovementActionPerformed(float horizontalInput, float verticalInput)
    {
        HorizontalInput = horizontalInput;
        VerticalInput = verticalInput;
    }
}
