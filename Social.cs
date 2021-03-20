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