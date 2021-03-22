using System;
using System.Collections.Generic;

public class Social{
    public Graph network;

    public Social(int size){
        network = new Graph(size);
    }

    public void exploreFriends(ref List<string> stack, string vertex1, string vertex2, ref bool found){
        bool[] isVisited = new bool[network.size];

        for(int i=0; i<network.size; i++){
            isVisited[i] = false;
        }

        found = false;
        exploreDFS(ref stack, vertex1, vertex2, isVisited, ref found);
    }
    

    public void exploreDFS(ref List<string> stack, string vertex1, string vertex2, bool[] isVisited, ref bool found){

        stack.Add(vertex1);

        if(vertex1 == vertex2){
            found = true;
        }

        int i = network.getindex(vertex1);
        isVisited[i] = true;

        Vertex V = network.getVertex(vertex1);
        
        List<string> neighbours = V.getNeighbours();
        neighbours.Sort();
        
        if(neighbours.Count > 0 && !(found)){
            foreach(string neighbour in neighbours) {
                int j = network.getindex(neighbour);
                if(!isVisited[j] && !(found)){
                    exploreDFS(ref stack, neighbour, vertex2, isVisited, ref found);
                    if(found) break;
                }
                if(found) break;
            }
        }

        if(!found) stack.RemoveAt(stack.Count - 1);
    }
    
    public bool BFS(string vertex1, string vertex2, bool[] isVisited, string []pred)
    {
        bool found = false;
        Queue<string> qbfs = new Queue<string>();
        qbfs.Enqueue(vertex1);
        int i = network.getindex(vertex1);
        isVisited[i] = true;
        for(int j=0; j < network.size; j++)
        {
            pred[j] = "-";
        }
        while (qbfs.Count > 0 && !found)
        {
            vertex1 = qbfs.First();
            Vertex V = network.getVertex(vertex1);
            qbfs.Dequeue();
            if (vertex1 == vertex2)
            {
                found = true;
            }
            List<string> neighbours = V.getNeighbours();
            neighbours.Sort();
            if (!found)
            {
                foreach (string neighbour in neighbours)
                {
                    int k = network.getindex(neighbour);
                    if (!isVisited[k])
                    {
                        qbfs.Enqueue(neighbour);
                        pred[k] = vertex1;
                        isVisited[k] = true;
                     }
                }
            }
        }
        if (found)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void pathBFS(string vertex1, string vertex2)
    {
        bool[] isVisited = new bool[network.size];
        for (int i = 0; i < network.size; i++)
        {
            isVisited[i] = false;
        }
        string[] pred = new string[network.size];
        if (BFS(vertex1, vertex2, isVisited, pred) == false)
        {
            Console.WriteLine("make a connection");
        }
        else
        {
            List<string> path = new List<string>();
            int idxpred = network.getindex(vertex2);
            path.Add(vertex2);
            while(pred[idxpred] != "-")
            {
            path.Add(pred[idxpred]);
            string currV = pred[idxpred];
            idxpred = network.getindex(currV);
            }
            path.Reverse();
            foreach(string s in path)
            {
                Console.WriteLine(s);
            }  
        }
    }

    static void Main(string[] args){
        Social S = new Social(8);
        S.network.addEdge("A", "B");
        S.network.addEdge("A", "C");
        S.network.addEdge("A", "D");
        S.network.addEdge("E", "B");
        S.network.addEdge("C", "B");
        S.network.addEdge("F", "B");
        S.network.addEdge("F", "C");
        S.network.addEdge("C", "G");
        S.network.addEdge("E", "F");
        S.network.addEdge("E", "H");
        S.network.addEdge("F", "H");
        S.network.addEdge("D", "G");
        S.network.addEdge("D", "F");


        List<string> stack = new List<string> ();
        bool found = false;

        S.exploreFriends(ref stack, "A","H", ref found);

        foreach(string s in stack){
            Console.WriteLine(s);
        }
    } 

}
