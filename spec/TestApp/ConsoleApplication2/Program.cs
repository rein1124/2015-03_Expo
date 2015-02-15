using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var idf = new IDF()
                      {
                          Version = "Version 1, Revision 3",
                          ReleaseDateTime = DateTime.Now,
                          Comment = "This is a valid IDF sample file."
                      };
            idf.RootNode = new Node()
                       {
                           Name = "RootNode",
                       };
            idf.RootNode.AppendParameters();
            for (int i = 0; i < 2; i++)
            {
                var L1Child = new Node()
                              {
                                  Name = "Node_" + i,
                              };
                idf.RootNode.Nodes.Add(L1Child);
                L1Child.AppendParameters();

                for (int j = 0; j < 2; j++)
                {
                    var L2Child = new Node()
                    {
                        Name = "Node_"+ i +"-" + j,
                    };
                    L1Child.Nodes.Add(L2Child);
                    L2Child.AppendParameters();
                }
            }

            idf.SaveToFile("sample.xml");
        }

    }

    public static class Ex
    {
        public static void AppendParameters(this Node node)
        {
            for (int i = 0; i < 2; i++)
            {
                var para = new Parameter
                           {
                               Name = "Parameter_" + i + " of " + node.Name,
                               DataType = DataType.Int16,
                               DefaultValue = 10.ToString(),
                               Maximum = 1000.ToString(),
                               Minimum = (-1000).ToString()
                           };
                node.Parameters.Add(para);
            }
        }
    }
}
