using Godot;
using System;

public class Castle : Area2D
{

    [Export]
    public NodePath BattleUIPath { get; set; }

    private BattleUI battleUI => GetNode<BattleUI>(BattleUIPath);

    public override void _Ready()
    {
        Connect("body_entered", this, nameof(enemyOnCastle));
    }

    private void enemyOnCastle(Node body)
    {
        if (body is BaseEnemy enemy)
        {
            GD.Print("Enemy on castle");
            battleUI.Damage(enemy.DamageOnCastle);
            enemy.QueueFree();
        }
    }
}
