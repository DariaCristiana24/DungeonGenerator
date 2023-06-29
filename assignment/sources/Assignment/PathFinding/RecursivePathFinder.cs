using GXPEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;

/**
 * An example of a PathFinder implementation which completes the process by rolling a die 
 * and just returning the straight-as-the-crow-flies path if you roll a 6 ;). 
 */
class RecursivePathFinder : PathFinder
{
    NodeGraph nodeGraph;
    List<Node> path = new List<Node>();
    List<Node> visited = new List<Node>();
    List<List<Node>> possiblePaths = new List<List<Node>>();
    int listIndex = 0;
    public RecursivePathFinder(NodeGraph pGraph) : base(pGraph) 
    {
        nodeGraph = pGraph;
    }

    protected override List<Node> generate(Node pFrom, Node pTo)
    {
        
        //at this point you know the FROM and TO node and you have to write an 
        //algorithm which finds the path between them
        Console.WriteLine("For now I'll just roll a die for a random path!!");

        int dieRoll = Utils.Random(1, 7);
        Console.WriteLine("I rolled a ..." + dieRoll);

         path.Add(pFrom);
        //possiblePaths.Add(new List<Node>());
       // possiblePaths[0].Add(pFrom);
        visited.Add(pFrom);
 
        findNextNode(pFrom, pTo);
        List<Node> lastPath = path;
        for (int i = 0; i< possiblePaths.Count; i++)
        {
            Console.WriteLine("pos paths:  " +possiblePaths.Count);
            if (lastPath.Count < possiblePaths[i].Count)
            {
                lastPath = possiblePaths[i];
            }
        }
         

 
        if (dieRoll == 6)
        {
            Console.WriteLine("Yes 'random' path found!!");
            //return new List<Node>() { pFrom, pTo };
            return path;
        }
        else
        {
            Console.WriteLine("Too bad, no path found !!");
            return null;
        }
        
    }

    Node nextNode;


    private void findNextNode(Node pFrom, Node pTo)
    {

        if(pFrom != pTo)
        {
            for (int i = 0; i < nodeGraph.connections.Count; i++)
            {
                if (pFrom == nodeGraph.connections[i].NodeA )
                {
                    if (checkTakenNodes(nodeGraph.connections[i].NodeB))
                    {
                        nextNode = nodeGraph.connections[i].NodeB;

                    }


                }
                if (pFrom == nodeGraph.connections[i].NodeB)
                {
                    if (checkTakenNodes(nodeGraph.connections[i].NodeA))
                    {
                        nextNode = nodeGraph.connections[i].NodeA; 
                    }


                }

            }
            if (nextNode == path[path.Count-1])
            {
                path.Remove(nextNode);
                nextNode = path[path.Count-1];
                visited.Add(nextNode);
            }
            else
            {
                path.Add(nextNode);
                visited.Add(nextNode);
            }


            //possiblePaths[listIndex].Add(nextNode);
            

            if (nextNode != pTo )
            {
                //Console.WriteLine("nextnode " + nextNode);
                findNextNode(nextNode, pTo);
            }
            /*else //one path found
            {
                Console.WriteLine("one path done");
                //create new path
                possiblePaths.Add(path);
                Node last2Vis = path[path.Count - 1];
                for (int i = possiblePaths[listIndex].Count; i >= 0; i--)
                {
                    path.Remove(path[path.Count - 1]);
                    findNextNode(path[path.Count - 2], pTo);
                    //if find nde succ rem vis
                    if (i + 2 < possiblePaths[listIndex].Count)
                    {
                        visited.Remove(last2Vis);
                    }
                }
                listIndex++;

            }/**/

        }
       
        
    }

    private bool checkTakenNodes(Node nodeToCheck)
    {
        for (int j = 0; j < visited.Count; j++)
        {
            if(nodeToCheck == visited[j])
            {
                return false;
            }
        }
        return true;
    }



    
}