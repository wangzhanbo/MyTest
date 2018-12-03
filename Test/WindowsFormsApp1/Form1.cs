using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TikaOnDotNet.TextExtraction;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            Segment segment = new Segment() { Name = "00", StartNode = new Node() { Id = 0 }, EndNode = new Node() { Id = 1 } };
            Segment segment12 = new Segment() { Name = "12", StartNode = new Node() { Id = 1 }, EndNode = new Node() { Id = 2 } };
            Segment segment23 = new Segment() { Name = "23", StartNode = new Node() { Id = 2 }, EndNode = new Node() { Id = 3 } };
            Segment segment36 = new Segment() { Name = "36", StartNode = new Node() { Id = 3 }, EndNode = new Node() { Id = 6 } };
            Segment segment69 = new Segment() { Name = "69", StartNode = new Node() { Id = 6 }, EndNode = new Node() { Id = 9 } };


            Segment segment14 = new Segment() { Name = "14", StartNode = new Node() { Id = 1 }, EndNode = new Node() { Id = 4 } };
            Segment segment47 = new Segment() { Name = "47", StartNode = new Node() { Id = 4 }, EndNode = new Node() { Id = 7 } };
            Segment segment78 = new Segment() { Name = "78", StartNode = new Node() { Id = 7 }, EndNode = new Node() { Id = 8 } };
            Segment segment89 = new Segment() { Name = "89", StartNode = new Node() { Id = 8 }, EndNode = new Node() { Id = 9 } };

            List<Segment> list = new List<Segment>();
            list.Add(segment12);
            list.Add(segment23);
            list.Add(segment36);
            list.Add(segment69);

            list.Add(segment14);
            list.Add(segment47);
            //list.Add(segment78);
            list.Add(segment89);

            findNode(list, segment, new Node() { Id = 9 });
            //Console.ReadKey();
        }

        Segment findNode(List<Segment> list, Segment segment, Node node)
        {
            if (segment.EndNode.Id == node.Id)
            {
                Console.WriteLine(segment.Name);
                return segment;
            }

            else
            {
                var end = list.Where(p => p.StartNode.Id == segment.EndNode.Id);
                Segment _seg = null;
                foreach (var seg in end)
                {
                    _seg = findNode(list, seg, node);

                }
                if (_seg != null)
                    Console.WriteLine(segment.Name);
                return _seg;
            }
        }

    }



    class Node
    {
        public int Id { get; set; }

    }

    class Segment
    {
        public string Name { get; set; }
        public Node StartNode { get; set; }

        public Node EndNode { get; set; }


    }

}
