using System;
using System.Collections.Generic;

public class Vertex{
    public string key;
    public LinkedList<string> neighbours;

    public Vertex(string key){
        this.key = key;
        neighbours = new LinkedList<string>();
    }

    public void addNeighbours(string Nkey){
        neighbours.AddLast(Nkey);
    }

    public void printVertex(){
        Console.Write("Key: ");
        Console.WriteLine(this.key);

        Console.Write("Neighbours: ");
        foreach(string vertex in neighbours){
            Console.Write(vertex);
            Console.Write("-->");
        }
    }
}