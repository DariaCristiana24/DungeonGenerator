using GXPEngine;
using System;
using System.Collections.Generic;

/**
 * Very simple example of a nodegraphagent that walks directly to the node you clicked on,
 * ignoring walls, connections etc.
 */
class SampleNodeGraphAgent : NodeGraphAgent
{
	//Current target to move towards
	private Node _target = null;
	private Node location;
	private NodeGraph nodeGraph;
	private List<Node> queue = new List<Node>();
	private	 Node futureLocation;

    public SampleNodeGraphAgent(NodeGraph pNodeGraph) : base(pNodeGraph)
	{
		nodeGraph = pNodeGraph;
		SetOrigin(width / 2, height / 2);

		//position ourselves on a random node
		if (pNodeGraph.nodes.Count > 0)
		{
			location = pNodeGraph.nodes[Utils.Random(0, pNodeGraph.nodes.Count)];
             futureLocation = location;
            jumpToNode(location);
		}

		//listen to nodeclicks
		pNodeGraph.OnNodeLeftClicked += onNodeClickHandler;
	}

	
	protected virtual void onNodeClickHandler(Node pNode)
	{
		for(int i = 0; i< nodeGraph.connections.Count; i++)
		{
            if ((nodeGraph.connections[i].NodeA == futureLocation && nodeGraph.connections[i].NodeB == pNode)
                || (nodeGraph.connections[i].NodeA == pNode && nodeGraph.connections[i].NodeB == futureLocation))
			{
				queue.Add(pNode);

				futureLocation = pNode;
               // _target = pNode;
				/*if(location != queue[iQueue])
				{
					iQueue++;
				}*/
				
			}
		}
		
	}
	int iQueue = 0;	
	private void goThroughQueue()
	{
		if (iQueue < queue.Count) 
		{
			_target = queue[iQueue];
            if (moveTowardsNode(_target))
            {
				iQueue++;
                location = _target;
                _target = null;
            }
        }
	}

	protected override void Update()
	{
		//no target? Don't walk
		//if (_target == null) return;

		//Move towards the target node, if we reached it, clear the target
		goThroughQueue();
	}
}
