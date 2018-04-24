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
        ITrackConverter trackConverter;
        ISorter sorter;
        ITrackController controller;
        ITrackSpeed ts;
        ITrackCompassCourse tcc;
        ISeperationEventChecker checker;
        ISeperationEventHandler warningCreator;
        ISeperationEventLogger logger;



        public MainWindow()
        {
            // TrackConverter that takes a TransponderReceiver and starts it
            trackConverter = new TrackConverter(TransponderReceiverFactory.CreateTransponderDataReceiver());
            // A sorter that uses the information from the TrackConverter
            sorter = new Sorter(trackConverter);
            // The logic-classes using and handling data
            ts = new TrackSpeed();
            tcc = new TrackCompassCourse();
            checker = new CheckForSeparationEvent();
            warningCreator = new CreateWarning(checker);
            logger = new LogSeparationEvent(checker);

            // Controller that delegates out the data
            controller = new Controller(sorter,ts,tcc,checker,warningCreator,logger);

            InitializeComponent();
            DataContext = new Controller(sorter, ts, tcc, checker, warningCreator, logger);
            Loaded += new RoutedEventHandler(MainWindow_Loaded);

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        public void UpdateCurrentTracks(List<TrackObject> trackObjects)
        {
           // lbxflyikonflik
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
