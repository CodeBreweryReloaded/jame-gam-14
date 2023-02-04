using Godot;
using System;

public class Cursor : Node2D
{
    private Sprite sprite;

    public override void _Ready()
    {
        sprite = GetNode<Sprite>("SelectedSprite");
    }

    public override void _Process(float delta)
    {
        Position = new Vector2(
            (float)Math.Round(GetViewport().GetMousePosition().x, 0),
            (float)Math.Round(GetViewport().GetMousePosition().y)
        );
    }

    public void setSprite(Texture texture) {
        GD.Print(texture);
        sprite.Texture = texture;
    }
}
