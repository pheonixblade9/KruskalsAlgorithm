using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4Kruskals
{
    public class Node
    {
        public string name;
        public int treeGroup;
        public List<Edge> edges = new List<Edge>();

        public Node() { }

        public Node(string pName)
        {
            name = pName;
            treeGroup = -1;
        }

        public override string ToString()
        { return this.name; }

    }
}