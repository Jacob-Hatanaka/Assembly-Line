using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Control
{
	public Dictionary<string, Item> items = new Dictionary<string, Item>();
    private GridContainer grid;
    public bool addItem(string itemName, int itemAmount)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName].addAmount(itemAmount);
        }
        else
        {
            items[itemName] = new Item(itemName, itemAmount);
            grid.AddChild(items[itemName]);
        }
        return true;
    }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        grid = new GridContainer();
        grid.Position = new Vector2(960, 0);
        grid.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        AddChild(grid);
        foreach (var item in items)
        {
            grid.AddChild(item.Value);
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}

public partial class Item : HBoxContainer
{
    public string itemName;
    public int itemAmount;
    private Label nameLabel;
    private Label amountLabel;

    public Item(string itemName, int itemAmount)
    {
        this.itemName = itemName;
        this.itemAmount = itemAmount;
    }

    public override void _Ready()
    {
        nameLabel = new Label();
        nameLabel.Text = itemName;
        AddChild(nameLabel);

        amountLabel = new Label();
        amountLabel.Text = itemAmount.ToString();
        AddChild(amountLabel);
    }
    public void addAmount(int amt) { 
        itemAmount += amt;
        amountLabel.Text = itemAmount.ToString();
    }
}