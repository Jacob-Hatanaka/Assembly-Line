using Godot;
using System;

public partial class Gather : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GridContainer grid = new GridContainer();
        grid.Position = new Vector2(960, 0);
        grid.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        AddChild(grid);
        var woodButton = new GatherResource("Wood", 1);
        grid.AddChild(woodButton);
        var stoneButton = new GatherResource("Stone", 1);
        grid.AddChild(stoneButton);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

public partial class GatherResource : Button
{
    private static bool disabled = false;
    private string resourceName;
    private int resourceAmount;
    public GatherResource(string resourceName, int resourceAmount)
    {
        this.resourceAmount = resourceAmount;
        this.resourceName = resourceName;
    }

    private Inventory inventory;

    public override void _Ready()
    {
        inventory = GetNode<Inventory>("/root/Main/TabContainer/Inventory");
        this.Text = $"Gather {resourceAmount} {resourceName}";
    }

    public override void _Pressed()
    {
        if (disabled) return;
        // add 1 sec time to collect
        disabled = true;
        Timer timer = new Timer();
        timer.WaitTime = 1.0;
        timer.OneShot = true;
        timer.Timeout += () => {
            inventory.addItem(resourceName, resourceAmount);
            disabled = false;
        };
        AddChild(timer);
        timer.Start();
    }
}