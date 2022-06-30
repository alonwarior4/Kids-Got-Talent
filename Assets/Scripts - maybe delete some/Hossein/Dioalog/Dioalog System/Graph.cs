using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    public InitGraph InitGraph ;
    
    public List<Node> ListNode = new List<Node>();
    public List<Edge> ListEdge = new List<Edge>();

    public int NumberOfNodes;

    
	// Use this for initialization
	void Start () {

	    MakeGraph();
        ShowG();
	    
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeGraph()
    {
        NumberOfNodes = InitGraph.Info.Length;

        for (int i = 0; i <InitGraph.Info.Length; i++)
        {

            Node NodeS = new Node();

            for (int j = 0; j <InitGraph.Info[i].childs.Length; j++)
            {
                Node NodeD = new Node();
                Edge edge = new Edge();

                string[] ed = InitGraph.Info[i].childs[j].str.Split('-');

                NodeS.NameNode = ed[0];
                NodeS.text = InitGraph.Info[i].txt;

                NodeD.NameNode = ed[1];

                edge.SourceNode = NodeS.NameNode;
                edge.DistinationNode = NodeD.NameNode;


                ListNode.Add(NodeS);
                ListEdge.Add(edge);
            }

        }

    }


    public void ShowG()
    {
        for (int l = 0; l <ListNode.Count; l++)
        {
            print(ListNode[l].NameNode + " tag = node");
        }


        for (int l = 0; l < ListEdge.Count; l++)
        {
            print(ListEdge[l].SourceNode + " -->"+ ListEdge[l].DistinationNode);
        }

    }


}









