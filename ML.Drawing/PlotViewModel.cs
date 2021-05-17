using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OxyPlot;
using Prism.Commands;
using Prism.Mvvm;

namespace ML.Drawing
{
    public class PlotViewModel : BindableBase
    {

        private ObservableCollection<DataPoint> lossPoints;
        public ObservableCollection<DataPoint> LossPoints
        {
            get { return lossPoints; }
            set
            {
                lossPoints = value;

                RaisePropertyChanged("LossPoints");
            }
        }

      
        private ObservableCollection<DataPoint> accuracyPoints;
        public ObservableCollection<DataPoint> AccuracyPoints
        {
            get { return accuracyPoints; }
            set
            {
                accuracyPoints = value;
                RaisePropertyChanged("AccuracyPoints");
            }
        }

        public DelegateCommand ClickTrainNetEvent { get; set; }

        public int step = 10;

        int lossIndex = 0;

        int accyractIndex = 0;

        public PlotViewModel()
        {
            LossPoints = new ObservableCollection<DataPoint>();

            AccuracyPoints = new ObservableCollection<DataPoint>();

            ClickTrainNetEvent = new DelegateCommand(ExecuteTrainNet);
        }

        private void ExecuteTrainNet()
        {
            Task.Factory.StartNew(() =>
            {
                NetTester.Inistance.TestTwoLayerNet();

                NetTester.Inistance.LossUpdated += OnLossUpdated;

                NetTester.Inistance.AccuracyUpdated += OnAccuracyUpdated;
            });
        }

        private void OnAccuracyUpdated(double value)
        {
            App.Current.Dispatcher.Invoke(() => AccuracyPoints.Add(new DataPoint(accyractIndex * 100, value)));

            accyractIndex++;
        }

        private void OnLossUpdated(double value)
        {
            if (lossIndex % 50 == 0) App.Current.Dispatcher.Invoke(() => LossPoints.Add(new DataPoint(lossIndex, value)));

            lossIndex++;
        }
    
    }
}
