using System;
using System.Collections.Generic;

public class Graph {
    public List<Vertex> adj;
    public int size;

    public Graph(int size){
       adj = new List<Vertex> (); 
       this.size = size;
    }

    public void addVertex(Vertex V){
        adj.Add(V);
    }

    public void addEdge(string vertex1, string vertex2){
        if(!isVertex(vertex1)) {
            Vertex temp1 = new Vertex(vertex1);
            adj.Add(temp1);
        }

        if(!isVertex(vertex2)) {
            Vertex temp2 = new Vertex(vertex2);
            adj.Add(temp2);
        }

        foreach(Vertex V in adj){
            if(V.key == vertex1){
                V.addNeighbours(vertex2);
            } else if(V.key == vertex2){
                V.addNeighbours(vertex1);
            }
        }
    }

    public int getindex(string vertex1){
        int idx = -1;
        for(int i=0; i<adj.Count; i++){
            if(adj[i].key == vertex1){
                idx = i;
            }
        }
        return idx;
    }

    public Vertex getVertex(string vertex1){
        // Assume vertex1 is always going to be in graph
        
        // Placeholder to prevent compiler error, won't effect program execution
        Vertex Vnull = new Vertex("null"); 

        foreach(Vertex V in adj){
            if(V.key == vertex1){
                return V;
            }
        }

        return Vnull;
    }

    public bool isVertex(string vertex1){
        bool found = false;
        foreach(Vertex V in adj){
            if(V.key == vertex1){
                found = true;
                break;
            }
        }
        return found;
    }

    public void printGraph(){
        foreach(Vertex V in adj){
            Console.Write("Head : " +  V.key);
            Console.WriteLine();
            Console.Write("Tail : ");
            foreach(string s in V.neighbours){
                Console.Write(s + "-->");
            }
            Console.WriteLine();
        }
    }

}