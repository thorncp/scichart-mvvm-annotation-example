using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AnnotationsMvvm.Data;
using Abt.Controls.SciChart;

namespace AnnotationsMvvm
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<IChartSeriesViewModel> _chartSeries;
        private IEnumerable<LabelViewModel> _chartLabels;

        // I'll leave it to you to wire up PropertyChanged to your base viewmodel type
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            var ds0 = new XyDataSeries<double, double>();
            var someData = new RandomWalkGenerator().GetRandomWalkSeries(200); // RandomWalkGenerator is found in the examples source code

            ds0.Append(someData.XData, someData.YData);
            
            // With SeriesSource API you do not need a DataSet. This is created automatically to match
            // the type of DataSeries<Tx,Ty>. All DataSeries must be the same type however

            // Create a list of ChartSeriesViewModel which unify dataseries and render series
            // This way you can add/remove your series and change the render series type easily
            // without worrying about DataSeriesIndex as per SciChart v1.3
            _chartSeries = new ObservableCollection<IChartSeriesViewModel>();
            _chartSeries.Add(new ChartSeriesViewModel(ds0, new FastLineRenderableSeries()));

            // Now create the labels
            _chartLabels = new[]
                               {
                                   new LabelViewModel(5, -2.5, "Label0", "Label0 Tooltip!"), 
                                   new LabelViewModel(20, -2, "Label1", "Label1 Tooltip!"), 
                                   new LabelViewModel(35, 3, "Label2", "Label2 Tooltip!"), 
                                   new LabelViewModel(50, 1.5, "Label3", "Label3 Tooltip!"), 
                                   new LabelViewModel(65, -0.5, "Label4", "Label4 Tooltip!"), 
                               };
        }

        public ObservableCollection<IChartSeriesViewModel> ChartSeries { get { return _chartSeries; } }

        public IEnumerable<LabelViewModel> ChartLabels { get { return _chartLabels; } }
    }
}
