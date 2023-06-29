using GXPEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

/**
 * An example of a dungeon implementation.  
 * This implementation places two rooms manually but your implementation has to do it procedurally.
 */
class SampleDungeon : Dungeon
{
   // int roomNo = 1;
    bool debugging = false;
    public List<Cross> crosses = new List<Cross>();

    public SampleDungeon(Size pSize) : base(pSize) {}



	/**
	 * This method overrides the super class generate method to implement a two-room dungeon with a single door.
	 * The good news is, it's big enough to house an Ogre and his ugly children, the bad news your implementation
	 * should generate the dungeon procedurally, respecting the pMinimumRoomSize.
	 * 
	 * Hints/tips: 
	 * - start by generating random rooms in your own Dungeon class and placing random doors.
	 * - playing/experiment freely is the key to all success
	 * - this problem can be solved both iteratively or recursively
	 */
	protected override void generate(int pMinimumRoomSize)
	{



        //left room from 0 to half of screen + 1 (so that the walls overlap with the right room)
        //(TODO: experiment with removing the +1 below to see what happens with the walls)
        //rooms.Add(new Room(new Rectangle(0, 0, size.Width/2+1, size.Height)));
        //rooms[1].x = 0;
        //right room from half of screen to the end
        //rooms.Add(new Room(new Rectangle(size.Width/2, 0, size.Width/2, size.Height)));
        //and a door in the middle wall with a random y position
        //TODO:experiment with changing the location and the Pens.White below
        //	doors.Add(new Door(new Point(size.Width / 2, size.Height / 2 + Utils.Random(-5, 5))));
        rooms.Add(new Room(new Rectangle(0, 0, size.Width, size.Height)));

        divideRooms(pMinimumRoomSize);
       // AddCrosses();
        //check crosses again
        //addDoors();
        // addDoorsBetter();
        addDoorsEvenBetter();
		if (debugging)
		{
			for (int i = 0; i < crosses.Count; i++)
			{
				Console.WriteLine("crosses: " + crosses[i]);
			}
		}

    }


	//method to divide rooms
	//for each room
	//if room.size>min and random => divide rooms in half
	//check whi is longer height or width
	//stop randomnly or when limit reached
	void divideRooms(int pMinimumRoomSize)
	{
		

		for(int i=0; i<=rooms.Count; i++)
		{
			if (i < rooms.Count)
			{

                if (rooms[i] != null)
				{
                    //if ((rooms[i].area.Width * rooms[i].area.Height < pMinimumRoomSize * 10 || Utils.Random(0, 4) == 1)&&roomNo>minimRooms && rooms[i].area.Width>7 && rooms[i].area.Height>7)//&& (i!=roomNo-1))
                    //if ((rooms[i].area.Width * rooms[i].area.Height < pMinimumRoomSize * 10 || Utils.Random(0, 4) == 1) && roomNo > minimRooms && rooms[i].area.Width * rooms[i].area.Height < maxRoomSize)
                    if ((rooms[i].area.Width  < pMinimumRoomSize*2 && rooms[i].area.Height < pMinimumRoomSize*2 ))
                    {
                        //room is not divided
						if (debugging)
						{
							Console.WriteLine("Division stopped: " + i);
						}
							
                    }
					else
					{

                        if (rooms[i].area.Width < rooms[i].area.Height) //divide horizontally
						{

                            divideHorizontally(i, pMinimumRoomSize);
                            

                        }
						else //divide vertically
						{
                            divideVertically(i, pMinimumRoomSize);
							

						}
                       // roomNo++;

						i--;
                        
                    }
				}
                
            }

        }


    }

    void divideHorizontally(int i, int pMinimumRoomSize)
    {
        int divSpot=0;

        if (pMinimumRoomSize < rooms[i].area.Height - pMinimumRoomSize + 1)
        {
             divSpot = Utils.Random(pMinimumRoomSize, rooms[i].area.Height - pMinimumRoomSize);

        }
        
        if (rooms[i].area.Height % 2 == 0)
         {
            rooms.Add(new Room(new Rectangle(rooms[i].area.X, rooms[i].area.Y + divSpot, rooms[i].area.Width, rooms[i].area.Height- divSpot)));
             rooms[i] = new Room(new Rectangle(rooms[i].area.X, rooms[i].area.Y, rooms[i].area.Width,  divSpot+1));
            

         }
         else
        {
            rooms.Add(new Room(new Rectangle(rooms[i].area.X, rooms[i].area.Y + divSpot, rooms[i].area.Width, rooms[i].area.Height - divSpot )));
            rooms[i] = new Room(new Rectangle(rooms[i].area.X, rooms[i].area.Y, rooms[i].area.Width,  divSpot + 1));


        }

    }

    void divideVertically(int i, int pMinimumRoomSize)
    {
        int divSpot = 0;
        if (pMinimumRoomSize < rooms[i].area.Width - pMinimumRoomSize + 1)
        {
            divSpot = Utils.Random(pMinimumRoomSize, rooms[i].area.Width - pMinimumRoomSize);
        }
        if (rooms[i].area.Width % 2 == 0)
        {
            rooms.Add(new Room(new Rectangle(rooms[i].area.X + divSpot , rooms[i].area.Y, rooms[i].area.Width - divSpot , rooms[i].area.Height)));
            rooms[i] = new Room(new Rectangle(rooms[i].area.X, rooms[i].area.Y, divSpot+1, rooms[i].area.Height));
        }
        else
        {
            rooms.Add(new Room(new Rectangle(rooms[i].area.X + divSpot , rooms[i].area.Y, rooms[i].area.Width -divSpot, rooms[i].area.Height)));
            rooms[i] = new Room(new Rectangle(rooms[i].area.X, rooms[i].area.Y, divSpot +1, rooms[i].area.Height));
        }
    }

    void AddCrosses()
	{
        for (int i = 0; i < rooms.Count; i++)
        {   int x = rooms[i].area.X;
            int y = rooms[i].area.Y;
            int width = rooms[i].area.Width;
            int height = rooms[i].area.Height;

            checkCross(new Cross(new Point(x, y),true));
            checkCross(new Cross(new Point(x + width - 1, y + height - 1),false,false,false,true));
            checkCross(new Cross( new Point(x + width - 1, y),false,true));
            checkCross(new Cross(new Point(x, y + height - 1),false,false,true));
        }

        /*
        doors.Add(new Door(new Point((int)x, (int)y)));
        doors.Add(new Door(new Point(x + width-1, y)));
        doors.Add(new Door(new Point(x, y+ height-1)));
        doors.Add(new Door(new Point(x+ width-1, y + height-1)));
        /**/
    }

    void checkCross(Cross cross)
    {
        bool check = false;
        for(int i=0; i< crosses.Count; i++)
        {
            if (crosses[i].location == cross.location)
            {
                check = true;
                if (crosses[i].topRightCorner == true) cross.topRightCorner = true;
                if (crosses[i].topLeftCorner == true) cross.topLeftCorner = true;
                if (crosses[i].bottomLeftCorner == true) cross.bottomLeftCorner = true;
                if (crosses[i].bottomRightCorner == true) cross.bottomRightCorner = true;
               // break;
            }
        }


        if (!check)
        {
            crosses.Add(cross);
        }
    }

    void crossSort(bool yTrue)
    {
        for (int i =0;i<crosses.Count; i++)
        {

            for (int j= i +1 ; j < crosses.Count; j++)
            {
                int xj = crosses[j].location.X;
                int yj = crosses[j].location.Y;
                int xi = crosses[i].location.X;
                int yi = crosses[i].location.Y;
                if (yTrue)
                {
                    Cross holder = crosses[i];
                    if (yi > yj)
                    {
                        //switch
                        
                        crosses[i] = crosses[j];
                        crosses[j] = holder;
                        i--;
                        j--;
                        break;
                    }
                    else if (yi == yj)
                    {
                        if (xi > xj)
                        {
                            //switch
                            crosses[i] = crosses[j];
                            crosses[j] = holder;
                            i--;
                            j--;
                            break;
                        }
                    }
                }
                else
                {
                    Cross holder = crosses[i];
                    if (xi > xj)
                    {
                        //switch
                        crosses[i] = crosses[j];
                        crosses[j] = holder;
                        i--;
                        j--;
                        break;
                    }
                    else if (xi == xj)
                    {
                        if (yi > yj)
                        {
                            //switch
                            crosses[i] = crosses[j];
                            crosses[j] = holder;
                            i--;
                            j--;
                            break;
                        }
                    }
                }


                
            }
        }

        for(int i = 0; i < crosses.Count; i++)
        {
            //Console.WriteLine(crosses[i]); 
           //doors.Add(new Door(crosses[i]));
        }
    }


    void addDoorsBetter()
    {
        crossSort(true);
        for (int i = 0; i < crosses.Count-1; i++)
        {
            if (crosses[i].location.Y == crosses[i + 1].location.Y && crosses[i].location.Y!=0 && crosses[i].location.Y!= size.Height-1) {
                if (crosses[i].location.X + 2 < crosses[i + 1].location.X - 2)
                {
                   // if ((crosses[i+1].bottomRightCorner || crosses[i + 1].topRightCorner) && (crosses[i].bottomLeftCorner || crosses[i].topLeftCorner))
                    //{
                        Door doorToAdd = new Door(new Point(Utils.Random(crosses[i].location.X + 2, crosses[i + 1].location.X - 2), crosses[i].location.Y));
                        doors.Add(doorToAdd);
                   //}
                }
            }
        }
        crossSort(false);
        for (int i = 0; i < crosses.Count - 1; i++)
        {
            if (crosses[i].location.X == crosses[i + 1].location.X && crosses[i].location.X != 0 && crosses[i].location.X != size.Width - 1)
            {
                if (crosses[i].location.Y + 2 < crosses[i + 1].location.Y - 2)
                {
                   // if ((crosses[i].topLeftCorner || crosses[i].topRightCorner) && (crosses[i+1].bottomLeftCorner || crosses[i + 1].bottomRightCorner))
                   // {
                        doors.Add(new Door(new Point(crosses[i].location.X, Utils.Random(crosses[i].location.Y + 2, crosses[i + 1].location.Y - 2))));
                 //   }
                }
            }
        }

        for(int i = 0;i<doors.Count;i++) //check if door is on a wall
        {

            /*
            bool doorPositionGood = false;
            for(int j=0; j< rooms.Count; j++)
            {
                if ((doors[i].location.X > rooms[j].area.X && doors[i].location.X < rooms[j].area.X + rooms[j].area.Width) || 
                    (doors[i].location.Y > rooms[j].area.Y && doors[i].location.Y < rooms[j].area.Y + rooms[j].area.Height))
                {
                    doorPositionGood = true;
                }

            }
            if (!doorPositionGood)
            {
                doors.Remove(doors[i]);

            }
            */
        }
    }
    

    void addDoorsEvenBetter()
    {
        //private List<int,int> connections = new List<int,int>();
         List<Tuple<int, int>> wallsDone = new List<Tuple<int, int>>();
     //   Dictionary<int, int> wallsDone = new Dictionary<int, int>();
        //check intersect for width and height -> add door in correct pos
        for (int i=0;i< rooms.Count; i++)
        {
            for(int j =i+1; j < rooms.Count; j++)
            {
               // Console.WriteLine(rooms[j].area);
                if (rooms[i].area.IntersectsWith(rooms[j].area))
                {
                    Rectangle intersection = rooms[i].area;
                    intersection.Intersect(rooms[j].area);
                    Door doorToAdd =null;
                 
                    if (intersection.Width == 1)
                    {
                        if (intersection.Y + 1 < intersection.Bottom - 1)
                        {
                            doorToAdd= new Door(new Point(intersection.X, Utils.Random(intersection.Y + 1, intersection.Bottom - 1)));
                            doors.Add(doorToAdd);
                        }
                    }
                    if(intersection.Height == 1)
                    {
                        if (intersection.X + 1 < intersection.Right - 1)
                        {
                            doorToAdd = new Door(new Point(Utils.Random(intersection.X + 1, intersection.Right - 1), intersection.Y));
                            doors.Add(doorToAdd);
                        }
                    }
                    
                    /* if(doorToAdd != null)
                     {
                         //check  -> add door
                         //if (!(wallsDone.ContainsKey(i) && wallsDone.ContainsKey(j)))
                         for (int k = 0; k < wallsDone.Count; k++) {
                             if ((wallsDone[k].Item1 ==i && wallsDone[k].Item2 ==j)||
                                 (wallsDone[k].Item1 ==j && wallsDone[k].Item2 ==i))
                             {
                                 //break;

                             }
                             else
                             {
                                 doors.Add(doorToAdd);
                                 // wallsDone.Add(i, j);
                                 wallsDone.Add(new Tuple<int, int>(i, j));
                             }
                         }*/

                    // }

                }
            }
        }
    }

}

