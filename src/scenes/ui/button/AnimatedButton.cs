using Godot;
using System;

public class AnimatedButton : Control
{
    private SpriteFrames sprite;
    private string normalAnimationName;
    private string hoverAnimationName;

    [Export(PropertyHint.ResourceType, "SpriteFrames")]
    private SpriteFrames Sprite {
        get => sprite;
        set {sprite = value;}
    }

    [Export(PropertyHint.ResourceType, "String")]
    private string NormalAnimationName {
        get => normalAnimationName;
        set {normalAnimationName = value;}
    }

    [Export(PropertyHint.ResourceType, "String")]
    private string HoverAnimationName {
        get => hoverAnimationName;
        set {hoverAnimationName = value;}
    }

    [Signal]
    delegate void ButtonPressed();

    public override void _Ready()
    {
        recalculate();
    }

    public virtual void onButtonPressed() {
        EmitSignal(nameof(ButtonPressed));
    }

    public void recalculate() {
        Button button = GetNode<Button>("Button");
        Box sprite = GetNode<Box>("Box");
        sprite.Frames = this.sprite;
        sprite.normalSpriteName = this.normalAnimationName;
        sprite.hoverSpriteName = this.hoverAnimationName;
        sprite.Animation = this.normalAnimationName;
        button.RectSize = this.sprite.GetFrame(this.normalAnimationName, 0).GetSize();
    }
}
