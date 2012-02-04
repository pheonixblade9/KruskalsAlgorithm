using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Homework4Kruskals
{
    class Homework4Kruskals
    {
        public static int treeGroupCounter = 0;
        static void Main(string[] args)
        {
            List<Node> moretCities = new List<Node>();        //holds cities for moret/shapiro problem
            List<Edge> moretSpanningTree = new List<Edge>();  //holds edges  for moret/shapiro problem
            List<Edge> tempMoretTree;
            List<Edge> finalMoretTree = new List<Edge>();
            AddMoretCities(moretCities);                      //add cities for the Moret/Shapiro problem
            AddMoretNeighbors(moretSpanningTree, moretCities);//add neighbors for the Moret/Shapiro problem
            SortSpanningTree(moretSpanningTree);              //sorts by minimum cost
            AddEdgesToNodes(moretSpanningTree);               //links added nodes and edges
            tempMoretTree = new List<Edge>(moretSpanningTree);//the tree is sorted at this point
            BuildKruskalsGraph(moretCities, tempMoretTree, finalMoretTree);
            PrintTree(finalMoretTree, "moretProblem.txt");

            List<Node> bookCities = new List<Node>();
            List<Edge> bookTree = new List<Edge>();
            List<Edge> tempBookTree;
            List<Edge> finalBookTree = new List<Edge>();
            Add6_23Cities(bookCities);
            Add6_23Neighbors(bookTree, bookCities);
            SortSpanningTree(bookTree);
            AddEdgesToNodes(bookTree);
            tempBookTree = new List<Edge>(bookTree);
            BuildKruskalsGraph(bookCities, tempBookTree, finalBookTree);
            PrintTree(finalBookTree, "6_23Problem.txt");
        }

        private static void PrintTree(List<Edge> treeToPrint, string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            foreach (Edge e in treeToPrint)
            {
                sw.WriteLine(e.ToString());
            }
            sw.Close();
        }

        private static void BuildKruskalsGraph(List<Node> moretCities, List<Edge> tempKruskalsTree,
            List<Edge> finalKruskalsTree)
        {
            while ((finalKruskalsTree.Count < moretCities.Count - 1) &&
                tempKruskalsTree.Count != 0)
            {
                Edge tempEdge = tempKruskalsTree[0];
                tempKruskalsTree.RemoveAt(0);
                //if there is a cycle, discard the edge 
                if (!((tempEdge.edgeNodes[0].treeGroup == tempEdge.edgeNodes[1].treeGroup) &&
                    (tempEdge.edgeNodes[0].treeGroup != -1)))
                //if not a cycle and is not unassigned; in same group
                {
                    treeGroupCounter++;

                    int tempGroup0 = tempEdge.edgeNodes[0].treeGroup;
                    int tempGroup1 = tempEdge.edgeNodes[1].treeGroup;
                    tempEdge.edgeNodes[0].treeGroup = treeGroupCounter;
                    tempEdge.edgeNodes[1].treeGroup = treeGroupCounter;
                    //combines two nodes from edge into one group

                    foreach (Node n in moretCities)
                    {
                        if ((n.treeGroup == tempGroup0 || n.treeGroup == tempGroup1) && (n.treeGroup != -1))
                        //if either value of treeGroup matches the old values and they are assigned;
                        {
                            n.treeGroup = treeGroupCounter; //add node to new group
                        }
                    }
                    finalKruskalsTree.Add(tempEdge);
                }
            }
        }

        private static void SortSpanningTree(List<Edge> spanningTree)
        {
            spanningTree.Sort(delegate(Edge e1, Edge e2) { return e1.cost.CompareTo(e2.cost); });
        }

        private static void AddEdgesToNodes(List<Edge> spanningTree)
        {
            foreach (Edge e in spanningTree)
            {
                e.edgeNodes[0].edges.Add(e);
                e.edgeNodes[1].edges.Add(e);
            }
        }

        private static void AddMoretCities(List<Node> cities)
        {
            cities.Add(new Node("Baltimore"));    //0
            cities.Add(new Node("Buffalo"));      //1
            cities.Add(new Node("Cincinatti"));   //2
            cities.Add(new Node("Cleveland"));    //3
            cities.Add(new Node("Detroit"));      //4
            cities.Add(new Node("New York"));     //5
            cities.Add(new Node("Philadelphia")); //6
            cities.Add(new Node("Pittsburgh"));   //7
            cities.Add(new Node("Washington"));   //8
        }

        private static void AddMoretNeighbors(List<Edge> edges, List<Node> cities)
        {
            edges.Add(new Edge(345, new Node[] { cities[1], cities[0] }));
            edges.Add(new Edge(186, new Node[] { cities[3], cities[1] }));
            edges.Add(new Edge(244, new Node[] { cities[3], cities[2] }));
            edges.Add(new Edge(252, new Node[] { cities[4], cities[1] }));
            edges.Add(new Edge(265, new Node[] { cities[4], cities[2] }));
            edges.Add(new Edge(167, new Node[] { cities[4], cities[4] }));
            edges.Add(new Edge(445, new Node[] { cities[5], cities[1] }));
            edges.Add(new Edge(507, new Node[] { cities[5], cities[3] }));
            edges.Add(new Edge(97, new Node[] { cities[6], cities[0] }));
            edges.Add(new Edge(365, new Node[] { cities[6], cities[1] }));
            edges.Add(new Edge(92, new Node[] { cities[6], cities[5] }));
            edges.Add(new Edge(230, new Node[] { cities[7], cities[0] }));
            edges.Add(new Edge(217, new Node[] { cities[7], cities[1] }));
            edges.Add(new Edge(284, new Node[] { cities[7], cities[2] }));
            edges.Add(new Edge(125, new Node[] { cities[7], cities[3] }));
            edges.Add(new Edge(386, new Node[] { cities[7], cities[5] }));
            edges.Add(new Edge(305, new Node[] { cities[7], cities[6] }));
            edges.Add(new Edge(39, new Node[] { cities[8], cities[0] }));
            edges.Add(new Edge(492, new Node[] { cities[8], cities[2] }));
            edges.Add(new Edge(231, new Node[] { cities[8], cities[7] }));
        }

        private static void Add6_23Cities(List<Node> cities)
        {
            cities.Add(new Node("0"));
            cities.Add(new Node("1"));
            cities.Add(new Node("2"));
            cities.Add(new Node("3"));
            cities.Add(new Node("4"));
            cities.Add(new Node("5"));
            cities.Add(new Node("6"));
        }

        private static void Add6_23Neighbors(List<Edge> edges, List<Node> cities)
        {
            edges.Add(new Edge(28, new Node[] { cities[1], cities[0] }));
            edges.Add(new Edge(16, new Node[] { cities[2], cities[1] }));
            edges.Add(new Edge(12, new Node[] { cities[3], cities[4] }));
            edges.Add(new Edge(22, new Node[] { cities[4], cities[5] }));
            edges.Add(new Edge(10, new Node[] { cities[5], cities[0] }));
            edges.Add(new Edge(25, new Node[] { cities[5], cities[4] }));
            edges.Add(new Edge(14, new Node[] { cities[6], cities[1] }));
            edges.Add(new Edge(18, new Node[] { cities[6], cities[3] }));
            edges.Add(new Edge(24, new Node[] { cities[6], cities[4] }));

        }
    }
}
