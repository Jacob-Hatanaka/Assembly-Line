using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
        grid.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        grid.Columns = 10;
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

public partial class Item : Control
{
    public string itemName;
    public int itemAmount;
    private Label amountLabel;

    public Item(string itemName, int itemAmount)
    {
        this.itemName = itemName;
        this.itemAmount = itemAmount;
    }

    public override void _Ready()
    {
        AddChild((Node2D)GD.Load<PackedScene>("res://item.tscn").Instantiate());
        Label nameLabel = GetChild(0).GetChild<Label>(2);
        nameLabel.Text = itemName;
        Sprite2D sprite = GetChild(0).GetChild<Sprite2D>(5);
        sprite.Texture = (ResourceLoader.Exists($"res://assets/art/{itemName}.png")) ? GD.Load<Texture2D>($"res://assets/art/{itemName}.png") : sprite.Texture;

        amountLabel = GetChild(0).GetChild<Label>(3);
        amountLabel.Text = itemAmount.ToString();
        CustomMinimumSize = new Vector2(200, 200);
    }
    public void addAmount(int amt)
    {
        itemAmount += amt;
        amountLabel.Text = itemAmount.ToString();
    }
}
