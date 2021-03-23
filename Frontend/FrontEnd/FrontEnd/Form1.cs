using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Tubes2_Mutual;
using Tubes2_Social;

namespace FrontEnd
{
    public partial class Form1 : Form
    {
        Microsoft.Msagl.Drawing.Graph graphLayout;
        public List<string> nodes;
        public List<Pair> edges;
        public string vertex1;
        public string vertex2;
        public bool isDFS;
        public bool isBFS;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                this.vertex1 = comboBox2.GetItemText(comboBox2.SelectedItem);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                this.vertex2 = comboBox3.GetItemText(comboBox3.SelectedItem);
            }
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            int size = 0;
            int count = 0;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    textBox1.Text = filePath;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {

                        comboBox2.Items.Clear();
                        comboBox3.Items.Clear();
                        comboBox2.ResetText();
                        comboBox3.ResetText();
                        this.isBFS = false;
                        this.isDFS = false;

                        string ln;
                        edges = new List<Pair>();
                        nodes = new List<string>();

                        while ((ln = reader.ReadLine()) != null)
                        {
                            if (count == 0)
                            {
                                size = int.Parse(ln);
                            }
                            else
                            {
                                string[] temp = ln.Split(' ');
                                Pair tempPair = new Pair(temp[0], temp[1]);
                                edges.Add(tempPair);

                                if (!nodes.Contains(temp[0])) nodes.Add(temp[0]);
                                if (!nodes.Contains(temp[1])) nodes.Add(temp[1]);

                            }
                            count++;
                        }
                    }
                }
            }

            if (nodes.Count != 0)
            {
                foreach (string s in nodes)
                {
                    comboBox2.Items.Add(s);
                    comboBox3.Items.Add(s);
                }
            }

            graphLayout = new Microsoft.Msagl.Drawing.Graph();

            foreach (Pair P in edges)
            {
                graphLayout.AddEdge(P.vertex1, P.vertex2).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }

            gViewer1.Graph = graphLayout;

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void DFS_CheckedChanged(object sender, EventArgs e)
        {
            BFS.Checked = !DFS.Checked;
        }

        private void BFS_CheckedChanged(object sender, EventArgs e)
        {
            DFS.Checked = !BFS.Checked;
        }

        private void explore_Click(object sender, EventArgs e)
        {
            Mutual graphMutual = new Mutual(8);
            foreach(Pair P in edges)
            {
                graphMutual.graph.addEdge(P.vertex1, P.vertex2);
            }

            Social socialNetwork = new Social(nodes.Count);
            foreach(Pair P in edges)
            {
                socialNetwork.network.addEdge(P.vertex1, P.vertex2);
            }
        
            if (DFS.Checked)
            {
                List<string> stack = new List<string>();
                bool found = false;

                socialNetwork.exploreFriends(ref stack, this.vertex1, this.vertex2, ref found);
                if (found)
                {
                    ClearEdgeColor();
                    for(int i=0; i<stack.Count-1; i++)
                    {
                        string v1 = stack[i];
                        string v2 = stack[i + 1];
                        colorEdge(v1, v2);
                    }

                    gViewer1.Graph = graphLayout;
                    string answers = "";
                    answers += "Nama Akun: " + this.vertex1 + " " + this.vertex2 + "\n";
                    answers += (stack.Count - 2) + "-degree connection\n";
                    for(int i=0; i<stack.Count; i++)
                    {
                        answers += stack[i];
                        if(i != stack.Count - 1)
                        {
                            answers += "-->";
                        }
                    }
                    MessageBox.Show(answers);
                } else
                {
                    MessageBox.Show("Nama akun: " + this.vertex1 + " "+ this.vertex2+ "\n" + "Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.");
                }
            } else if (BFS.Checked)
            {
                List<string> path = new List<string>();
                bool found = false;
                MessageBox.Show(socialNetwork.pathBFS(ref path, this.vertex1,this.vertex2, ref found));
                if (found)
                {
                    ClearEdgeColor();
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        string v1 = path[i];
                        string v2 = path[i + 1];
                        colorEdge(v1, v2);
                    }

                    gViewer1.Graph = graphLayout;
                } else
                {

                }
            }

            displayFriendRec.Text = graphMutual.mutualSearch(this.vertex1);
        }

        private void colorEdge(string v1, string v2)
        {
            int idx = -1;
            
            Microsoft.Msagl.Drawing.Edge[] edges = graphLayout.Edges.ToArray();
            for(int i=0; i<edges.Length; i++)
            {
                if ((edges[i].Source == v1 && edges[i].Target == v2) || (edges[i].Source == v2 && edges[i].Target == v1))
                {
                    idx = i;
                }
            }

            if(idx != -1)
            {
                graphLayout.Edges.ElementAt(idx).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
            }
        }

        private void ClearEdgeColor()
        {
            Microsoft.Msagl.Drawing.Edge[] edges = graphLayout.Edges.ToArray();
            for(int i=0; i<edges.Length; i++)
            {
                graphLayout.Edges.ElementAt(i).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
            }
        }
    }
}
