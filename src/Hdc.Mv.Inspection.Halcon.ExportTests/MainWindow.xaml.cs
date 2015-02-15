using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HalconDotNet;

namespace Hdc.Mv.Inspection.Halcon.ExportTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HDevEngine MyEngine = new HDevEngine();
        private HDevProcedureCall ProcCall;

        public MainWindow()
        {
            InitializeComponent();

//            new Exports().RakeEdgeLineWrapper();
        }
    }

    public class Exports
    {
//        private HDevEngine MyEngine = new HDevEngine();

        public void Run(HImage hImage)
        {
            try
            {
//                RakeEdgeLineWrapper();
            }
            catch (HDevEngineException Ex)
            {
                MessageBox.Show(Ex.Message, "HDevEngine Exception");
                return;
            }
        }



    }
}