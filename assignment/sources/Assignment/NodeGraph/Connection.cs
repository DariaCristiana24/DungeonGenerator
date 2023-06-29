using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

class Connection
{
    public readonly Node NodeA;
    public readonly Node NodeB;
    public readonly string id;
    private static int lastID = 0;

    public Connection(Node pNodeA, Node pNodeB)
    {
        NodeA = pNodeA;
        NodeB = pNodeB;
        id = "" + lastID++;
        System.Console.WriteLine(id);
    }
    public override string ToString()
    {
        return $"Connection: nodeA: {NodeA.location}, nodeB: {NodeB.location}";
    }

}

