﻿using System;
using System.Drawing;

/**
 * This class represents (the data for) a Door, at this moment only a position in the dungeon.
 * Changes to this class might be required based on your specific implementation of the algorithm.
 */
class Cross
{
    public readonly Point location;
    public bool topLeftCorner;
    public bool bottomLeftCorner;
    public bool topRightCorner;
    public bool bottomRightCorner;

    //Keeping tracks of the Rooms that this door connects to,
    //might make your life easier during some of the assignments
    public Room roomA = null;
    public Room roomB = null;

    //You can also keep track of additional information such as whether the door connects horizontally/vertically
    //Again, whether you need flags like this depends on how you implement the algorithm, maybe you need other flags
    public bool horizontal = false;

    public Cross(Point pLocation,bool pTopLeftCorner=false, bool pTopRightCorner=false,bool pBottomLeftCorner = false, bool pBottomRightCorner = false)
    {
        location = pLocation;
        topLeftCorner = pTopLeftCorner;
        bottomLeftCorner = pBottomLeftCorner;
        bottomRightCorner = pBottomRightCorner;
        topRightCorner = pBottomRightCorner;
    }

    public override string ToString()
    {
        return $"Cross: x: {location.X}, y: {location.Y}";
    }
}

