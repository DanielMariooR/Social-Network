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
            List<List<string>> allNeighbour = new List<List<string>>();

            List<string> secondDegree = new List<string>(); // E F G
            for (int i = 0; i < graph.size; i++)
            {
                if (graph.adj[i] != akun_ && !isElmt(akunNeighbour, graph.adj[i].key))// != A atau BCD
                {
                    List<string> neighbour = graph.adj[i].getNeighbours();// Neighbour selain A
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
            //Sorting berdasarkan mutual friend terbanyak
            List<string> allNeighbourTemp = new List<string>();
            string secondDegreeTemp;
            for (int y = 0; y < allNeighbour.Count - 2; y++)
            {
                for (int x = 0; x < allNeighbour.Count - 2; x++)
                {
                    if (allNeighbour[x].Count <= allNeighbour[x + 1].Count)
                    {
                        allNeighbourTemp = allNeighbour[x + 1];
                        allNeighbour[x + 1] = allNeighbour[x];
                        allNeighbour[x] = allNeighbourTemp;

                        secondDegreeTemp = secondDegree[x + 1];
                        secondDegree[x + 1] = secondDegree[x];
                        secondDegree[x] = secondDegreeTemp;
                    }
                }
            }
            Console.WriteLine("Daftar rekomendasi teman untuk akun " + akun);
            for (int i = 0; i < secondDegree.Count; i++)
            {
                Console.Write("Nama Akun: " + secondDegree[i]);
                Console.WriteLine();
                if (allNeighbour[i].Count == 1)
                {
                    Console.WriteLine("1 mutual friend:");
                }
                else
                {
                    Console.WriteLine(allNeighbour[i].Count + " mutual friends:");
                }
                for (int j = 0; j < allNeighbour[i].Count; j++)
                {
                    Console.WriteLine(allNeighbour[i][j]);
                    continue;
                }
            }
        }
    }
}