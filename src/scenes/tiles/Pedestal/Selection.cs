using Godot;
using System;

public class Selection : AnimatedSprite
{
    [Export]
    private string availableName;

    [Export]
    private string reservedName;

    public override void _Ready()
    {
        Animation = availableName;
    }


    public void toggleAvailability() {
        Animation = Animation == availableName ? reservedName : availableName;
    }

}
