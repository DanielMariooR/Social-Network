using System;
using System.Collections.Generic;

namespace Tubes2_Vertex
{
    public class Vertex
    {
        public string key;
        public List<string> neighbours;

        public Vertex(string key)
        {
            this.key = key;
            neighbours = new List<string>();
        }

        public void addNeighbours(string Nkey)
        {
            neighbours.Add(Nkey);
        }

        public List<string> getNeighbours()
        {
            return neighbours;
        }

        public void printVertex()
        {
            Console.Write("Key: ");
            Console.WriteLine(this.key);

            Console.Write("Neighbours: ");
            foreach (string vertex in neighbours)
            {
                Console.Write(vertex);
                Console.Write("-->");
            }
        }
    }
}