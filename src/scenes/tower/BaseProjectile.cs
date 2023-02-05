using Godot;
using System;

public abstract class BaseProjectile : Node2D
{
    public Node2D Target { get; set; }
    [Signal]
    protected delegate void Hit();
    [Export]
    public BaseTower Tower { get; set; }
    [Export]
    private Texture texture;
    [Export]
    private NodePath visibilityPath;

    private Vector2 targetVector = new Vector2();

    private Lazy<VisibilityNotifier2D> lazyVisibility;
    protected VisibilityNotifier2D visibility => lazyVisibility.Value;
    
    public BaseProjectile() {
        lazyVisibility = new Lazy<VisibilityNotifier2D>(() => GetNode<VisibilityNotifier2D>(visibilityPath));
    }

    public override void _Ready(){
        Sprite sprite = new Sprite();
        sprite.Texture = texture;
        AddChild(sprite);
        Update();
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 currentPosition = GlobalPosition;
        if (IsInstanceValid(Target)) {
            targetVector = Target.Position - currentPosition;
        }
        Vector2 nextVelocity = targetVector.Normalized() * Tower.ProjectileSpeed * delta;
        GlobalPosition += nextVelocity;
        if (!visibility.IsOnScreen())
        {
            QueueFree();
        }
    }


    public virtual void _on_Area2D_body_entered(Node2D body)
    {
        Connect(nameof(Hit), body, "onHit");
        EmitSignal(nameof(Hit), Tower.Heal, Tower.Effect, Tower.EffectDuration);
        QueueFree();
    }



}
