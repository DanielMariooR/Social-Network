using System;
using System.Collections.Generic;
using System.Text;
namespace Tubes2
{
    public class Mutual
    {
        public Graph graph;
        public Mutual(int size)
        {
            graph = new Graph(size);
        }
        public int findIdx (string removeNode, List<string> mutualX)
        {
            int idx = 0;
            int found = 0;
            while (idx < mutualX.Count && found == 0)
            {
                if (removeNode == mutualX[idx])
                {
                    found = 1;
                }
                else
                {
                    idx++;
                }
            }
            return idx;
        }

        public void printMutual(string mutualX, List<string> Mutual)
        {
            Console.Write("Nama Akun: " + mutualX);
            Console.WriteLine();
            Console.Write(Mutual.Count + " mutual friends: ");
            Console.WriteLine();
            for (int i = 0; i < Mutual.Count; i++)
            {
                Console.Write(Mutual[i]);
                Console.WriteLine();
            }
        }
        public bool isElmt(List<string> listVertex, string vertex)
        {
            int i = 0;
            bool found = false;
            while (i < listVertex.Count && found == false)
            {
                if (vertex == listVertex[i])
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }
            return found;
        }
        
        public void mutualSearch(string akun)
        {
            Vertex akun_ = graph.getVertex(akun);
            List<string> akunNeighbour = akun_.getNeighbours(); // BCD
            for (int l = 0; l < akunNeighbour.Count; l++)
            {
                Console.WriteLine("Neighbour A : " + akunNeighbour[l]);
            }
            List<List<string>> allNeighbour = new List<List<string>>();

            List<string> secondDegree = new List<string>(); // E F G
            for (int i = 0; i < graph.size; i++)
            {
                if (graph.adj[i] != akun_ && !isElmt(akunNeighbour, graph.adj[i].key))// != A atau BCD
                {
                    List<string> neighbour = graph.adj[i].getNeighbours();// Neig 
                    for (int l = 0; l < neighbour.Count; l++)
                    {
                        Console.WriteLine("Neighbour " + graph.adj[i].key + " : " + neighbour[l]);
                    }

                    List<string> newNeighbour = new List<string>();
                    for (int j = 0; j < neighbour.Count; j++) // E BFH
                    {
                        

                        if (isElmt(akunNeighbour, neighbour[j]))
                        {
                            string a = graph.adj[i].key; //E
                            if (!isElmt(secondDegree, a))
                            {
                                secondDegree.Add(a);
                            }
                            newNeighbour.Add(neighbour[j]);

                        }
                        
                    }
                    allNeighbour.Add(newNeighbour);
                }
            }
            //allNeighbour.RemoveAt(allNeighbour.Count - 1);
            foreach(List<string> a in allNeighbour)
            {
                foreach(string b in a)
                {
                    Console.WriteLine(b);
                }
                Console.WriteLine();
            }
            Console.Write(allNeighbour.Count);
            Console.Write(" "+secondDegree.Count);
            for (int i = 0; i < secondDegree.Count; i++)
            {
                Console.Write("Nama Akun: " + secondDegree[i]);
                Console.WriteLine();

                for (int j = 0; j < allNeighbour[i].Count; j++)
                {
                    Console.WriteLine(allNeighbour[i][j]);
                    continue;
                    
                }
            }
        }

    }
}
