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
using TransponderReceiver;
using ATM;
using ATM.Logic.Handlers;
using ATM.Logic.Controllers;
using ATM.Logic.Interfaces;

namespace FlightApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // private List<TrackObject> tracks;
        public MainWindow()
        {
            TrackConverter trackConverter = new TrackConverter(TransponderReceiverFactory.CreateTransponderDataReceiver());
            Sorter sorter = new Sorter(trackConverter);
            Controller controller = new Controller(sorter);
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
     //       TrackConverter_TrackObjectsReady(??);
        }

        private static void TrackConverter_TrackObjectsReady(object sender, TrackObjectEventArgs e)
        {
       //     lbxflyikonflikt.Items.Clear();
            foreach (var track in e.TrackObjects)
            {

        //        lbxflyikonflikt.Items.Add(track);
            }
        }
    }
}
