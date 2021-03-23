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

namespace FrontEnd
{
    public partial class Form1 : Form
    {
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

            MessageBox.Show(edges[0].vertex1);
            if (nodes.Count != 0)
            {
                foreach (string s in nodes)
                {
                    comboBox2.Items.Add(s);
                    comboBox3.Items.Add(s);
                }
            }

            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graphLayout = new Microsoft.Msagl.Drawing.Graph();

            foreach (Pair P in edges)
            {
                graphLayout.AddEdge(P.vertex1, P.vertex2);
            }

            viewer.Graph = graphLayout;
            this.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(viewer);
            this.ResumeLayout();

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}