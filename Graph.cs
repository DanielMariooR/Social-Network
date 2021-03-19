using System;
using System.Collections.Generic;

public class Graph {
    List<Vertex> adj;

    public Graph(){
       adj = new List<Vertex> (); 
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

    static void Main(string[] args){
        Vertex A = new Vertex("A");
        A.addNeighbours("B");
        A.addNeighbours("C");
        Graph G = new Graph();
        G.addVertex(A);
        G.printGraph();
    }
}