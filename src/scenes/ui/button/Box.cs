using Godot;
using System;

public class Box : AnimatedSprite
{
    //[Export(PropertyHint.ResourceType, "String")]
    public String normalSpriteName;

    //[Export(PropertyHint.ResourceType, "String")]
    public String hoverSpriteName;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void mouseEnter() {
        int frame = Frame + 1;
        Animation = hoverSpriteName;
        Frame = frame;
    }

    public void mouseExit() {
        int frame = Frame + 1;
        Animation = normalSpriteName;
        Frame = frame;
    }

}
