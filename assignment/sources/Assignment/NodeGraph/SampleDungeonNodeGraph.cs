using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

/**
 * An example of a dungeon nodegraph implementation.
 * 
 * This implementation places only three nodes and only works with the SampleDungeon.
 * Your implementation has to do better :).
 * 
 * It is recommended to subclass this class instead of NodeGraph so that you already 
 * have access to the helper methods such as getRoomCenter etc.
 * 
 * TODO:
 * - Create a subclass of this class, and override the generate method, see the generate method below for an example.
 */
 class SampleDungeonNodeGraph : NodeGraph
{
	protected Dungeon _dungeon;


    public SampleDungeonNodeGraph(Dungeon pDungeon) : base((int)(pDungeon.size.Width * pDungeon.scale), (int)(pDungeon.size.Height * pDungeon.scale), (int)pDungeon.scale/3)
	{
		Debug.Assert(pDungeon != null, "Please pass in a dungeon.");

		_dungeon = pDungeon;
	}

	protected override void generate ()
	{
        //Generate nodes, in this sample node graph we just add to nodes manually
        //of course in a REAL nodegraph (read:yours), node placement should 
        //be based on the rooms in the dungeon

        //We assume (bad programming practice 1-o-1) there are two rooms in the given dungeon.
        //The getRoomCenter is a convenience method to calculate the screen space center of a room
        //nodes.Add(new Node(getRoomCenter(_dungeon.rooms[0])));
        //nodes.Add(new Node(getRoomCenter(_dungeon.rooms[1])));
        //The getDoorCenter is a convenience method to calculate the screen space center of a door
        //	nodes.Add(new Node(getDoorCenter(_dungeon.doors[0])));


        /* for (int i = 0; i < _dungeon.rooms.Count; i++)
         {
             nodes.Add(new Node(getRoomCenter(_dungeon.rooms[i])));
         }*/
        foreach(Room room in _dungeon.rooms) 
        {
			Node nodeToAdd = new Node(getRoomCenter(room), true, room);
            nodes.Add(nodeToAdd);
           // Console.WriteLine(room +	$" nodeX: {nodeToAdd.location.X/_dungeon.scale}, nodeY: {nodeToAdd.location.Y/_dungeon.scale}");
        }
	   foreach(Door door in _dungeon.doors)
		{
			Node nodeToAdd = new Node(getDoorCenter(door),false,door);
			nodes.Add(nodeToAdd);
		}
		AddConnections();
        //create a connection between the two rooms and the door...
        //AddConnection(nodes[0], nodes[2]);
		//AddConnection(nodes[1], nodes[2]);

	}

	/**
	 * A helper method for your convenience so you don't have to meddle with coordinate transformations.
	 * @return the location of the center of the given room you can use for your nodes in this class
	 */
	protected Point getRoomCenter(Room pRoom)
	{
		float centerX = ((pRoom.area.Left + pRoom.area.Right) / 2.0f) * _dungeon.scale;
		float centerY = ((pRoom.area.Top + pRoom.area.Bottom) / 2.0f) * _dungeon.scale;

        return new Point((int)centerX, (int)centerY);
	}

	/**
	 * A helper method for your convenience so you don't have to meddle with coordinate transformations.
	 * @return the location of the center of the given door you can use for your nodes in this class
	 */
	protected Point getDoorCenter(Door pDoor)
	{
		return getPointCenter(pDoor.location);
	}

	/**
	 * A helper method for your convenience so you don't have to meddle with coordinate transformations.
	 * @return the location of the center of the given point you can use for your nodes in this class
	 */
	protected Point getPointCenter(Point pLocation)
	{
		float centerX = (pLocation.X + 0.5f) * _dungeon.scale;
		float centerY = (pLocation.Y + 0.5f) * _dungeon.scale;
		return new Point((int)centerX, (int)centerY);
	}

	void AddConnections()
	{
		for(int i =0; i< nodes.Count; i++)
		{
            for (int j=1; j< nodes.Count; j++)
			{
				if (nodes[i].trueIfRoom && !nodes[j].trueIfRoom)
				{
					//left node 
                    if (nodes[j].door.location.X == nodes[i].room.area.X && nodes[j].door.location.Y > nodes[i].room.area.Y && nodes[j].door.location.Y< nodes[i].room.area.Y + nodes[i].room.area.Height /*|| nodes[j].location.X == nodes[i].room.area.X + nodes[i].room.area.Width*/)
                    {
						AddConnection(nodes[i], nodes[j]);
					}
					//righgt node
                    if (nodes[j].door.location.X == nodes[i].room.area.X + nodes[i].room.area.Width-1 && nodes[j].door.location.Y > nodes[i].room.area.Y && nodes[j].door.location.Y < nodes[i].room.area.Y + nodes[i].room.area.Height /*|| nodes[j].location.X == nodes[i].room.area.X + nodes[i].room.area.Width*/)
                    {
                        AddConnection(nodes[i], nodes[j]);
                    }
					//top node
                    if (nodes[j].door.location.Y == nodes[i].room.area.Y && nodes[j].door.location.X > nodes[i].room.area.X && nodes[j].door.location.X < nodes[i].room.area.X + nodes[i].room.area.Width /*|| nodes[j].location.X == nodes[i].room.area.X + nodes[i].room.area.Width*/)
                    {
                        AddConnection(nodes[i], nodes[j]);
                    }
					//bottom node
                    if (nodes[j].door.location.Y == nodes[i].room.area.Y + nodes[i].room.area.Height - 1&& nodes[j].door.location.X > nodes[i].room.area.X && nodes[j].door.location.X < nodes[i].room.area.X + nodes[i].room.area.Width /*|| nodes[j].location.X == nodes[i].room.area.X + nodes[i].room.area.Width*/)
                    {
                        AddConnection(nodes[i], nodes[j]);
                    }
                }
			}
		}
	}

}
