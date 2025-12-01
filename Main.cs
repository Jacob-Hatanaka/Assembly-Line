using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().AutoAcceptQuit = false;
		var file = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);
		if (file != null && !file.EofReached())
		{
			Dictionary saveData = (Dictionary)file.GetVar();
			file.Close();
			var items = (Dictionary)saveData["items"];
			var inventory = GetNode<Inventory>("/root/Main/TabContainer/Inventory");
			foreach (var key in items.Keys)
			{
				inventory.addItem((string)key, (int)items[key]);
			}
		}
    }

	void _notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			// do save stuff here
			Dictionary saveData = new Dictionary();
			var items = GetNode<Inventory>("/root/Main/TabContainer/Inventory").items;
            Dictionary Ditems = new Dictionary();
			foreach (var item in items)
            {
                Ditems[item.Key] = item.Value.itemAmount;
            }
			saveData["items"] = Ditems;
			// save to file
			var file = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);
			file.StoreVar(saveData);
			file.Close();
			GD.Print("Saving game...");
			GetTree().Quit();
		}    
	}
}
