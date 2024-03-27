using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using LiveCharts.Wpf;
using LiveCharts;
using TestTaskWPF.Utils;
using TestTaskWPF.Data;
using GrapeCity.DataVisualization.TypeScript;
using System.Data;


namespace TestTaskWPF.ViewModel
{
    public class ManagerDBVM : ViewModelBase
    {

        public RelayCommand SaveEvent { get; set; }

        private Car _selectedCar;
        public Car SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                if (_selectedCar == value)
                    return;
                _selectedCar = value;
                RaisePropertyChanged("SelectedCar");
            }
        }
        private ObservableCollection<Car> _numCar;
        public ObservableCollection<Car> Cars
        {
            get { return _numCar; }
            set
            {
                if (_numCar == value)
                    return;
                _numCar = value;
                RaisePropertyChanged("Cars");
            }
        }

        public CollectionViewSource Collection { get; private set; }
        private Database1Entities _db;
        private WeightListVM _selectedItem;

        public WeightListVM SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
                BuildChart();
          
            }
        }

        private readonly WeightListVM order = new WeightListVM();
        public WeightListVM Order
        {
            get { return order; }

        }

        private WeightListVM __order;
        public WeightListVM _order

        {
            get { return __order; }
            set
            {
                __order = value;
                RaisePropertyChanged(nameof(_order));

            }

        }
        private ObservableCollection<WeightListVM> collectionView;
        public ObservableCollection<WeightListVM> CollectionViewVM
        {
            get { return collectionView; }
            set
            {
                collectionView = value;
                RaisePropertyChanged("CollectionViewVM");
                // BuildChart();
            }
        }
        List<double> allValuesTara = new List<double>();
        List<double> allValuesBrutto = new List<double>();
        public double[] ValuesTara = new double[0];
        List<DateTime> dateTimesTara = new List<DateTime>();
        List<DateTime> dateTimesBrutto = new List<DateTime>();


        public List<DataCharts> datas;


        public ManagerDBVM()
        {
            Database1Entities db;
            db = new Database1Entities();
            SaveEvent = new RelayCommand(Save, CanSave);
            RaisePropertyChanged(nameof(_order));
            var ViewModel = db.WeightList.ToList().Select(model => new WeightListVM(model));
            CollectionViewVM = new ObservableCollection<WeightListVM>(ViewModel);
            
            BuildChart();
        
        }

        private SeriesCollection seriesCollection = new SeriesCollection();
        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set { seriesCollection = value; RaisePropertyChanged("SeriesCollection"); }
        }

    
        public void BuildChart()
        {
            if (SelectedItem != null)
            {
                MessageBox.Show("upd chart");

                datas = new List<DataCharts>();

                datas.Add(new DataCharts() { valuesBrutto = SelectedItem.WBrutto, valuesTara = SelectedItem.WTara, dateBrutto = SelectedItem.DTBrutto, dateTara = SelectedItem.DTTara });
                SeriesCollection.Clear();


                foreach (var model in datas)
                {
                    allValuesTara.Add((double)(model.valuesTara));
                    allValuesBrutto.Add((double)(model.valuesBrutto));
                };
                ChartValues<double> zpBrutto = new ChartValues<double>();
                ChartValues<double> zpTapa = new ChartValues<double>(); 
                List<string> date = new List<string>(); 
                foreach (var item in datas) 
                {
                    zpTapa.Add(item.valuesTara);
                    zpBrutto.Add(item.valuesBrutto);
                    date.Add(item.dateTara.ToString());
                }

                for (var i = 0; i < allValuesTara.Count; i++)
                {
                    SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Тара",
                       Values = zpTapa
                    },
                    new ColumnSeries
                    {
                        Title = "Брутто",
                        Values = zpBrutto
                    }
                };
                };

                foreach (var item in datas)
                {
                    dateTimesBrutto.Add((DateTime)item.dateBrutto);
                    dateTimesTara.Add((DateTime)item.dateTara);

                }
                for (var i = 0; i < allValuesTara.Count; i++)
                {
                    Labels = new[] { dateTimesTara.toString(), dateTimesTara.toString() };
                }


                Formatter = value => value.ToString("n");
            }
            else
            {
                datas = new List<DataCharts>();
                foreach (var data in CollectionViewVM)
                {
                    datas.Add(new DataCharts() { valuesBrutto = data.WBrutto, valuesTara = data.WTara, dateBrutto = data.DTBrutto, dateTara = data.DTTara });
                }
                _order = new WeightListVM { };

                foreach (var model in datas)
                {
                    allValuesTara.Add((double)(model.valuesTara));
                    allValuesBrutto.Add((double)(model.valuesBrutto));
                };
                ChartValues<double> zpBrutto = new ChartValues<double>();
                ChartValues<double> zpTapa = new ChartValues<double>(); 
                List<string> date = new List<string>(); 
                foreach (var item in datas)
                {
                    zpTapa.Add(item.valuesTara);
                    zpBrutto.Add(item.valuesBrutto);
                    date.Add(item.dateTara.ToString());
                }

                for (var i = 0; i < allValuesTara.Count; i++)
                {
                    SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Тара",
                       Values = zpTapa
                    },
                    new ColumnSeries
                    {
                        Title = "Брутто",
                        Values = zpBrutto
                    }
                };
                };

                foreach (var item in datas)
                {
                    dateTimesBrutto.Add((DateTime)item.dateBrutto);
                    dateTimesTara.Add((DateTime)item.dateTara);

                }
                for (var i = 0; i < allValuesTara.Count; i++)
                {
                    Labels = new[] { dateTimesTara.toString(), dateTimesTara.toString() };
                }

                Formatter = value => value.ToString("n");

            }
        }

        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        public void Save(object parameter)
        {
            AsyncDataLoad();
        }

        public async Task AsyncDataLoad()
        {
            Database1Entities _db;
            _db = new Database1Entities();
            DateTime DateNow = DateTime.Now;

            try
            {

                CollectionViewVM.Add(_order);
                _db.WeightList.Add(new WeightList
                {

                    NumCar = _order.SelectedCar.NumCar,
                    WNetto = _order.WNetto,
                    WBrutto = _order.WBrutto,
                    WTara = _order.SelectedCar.WTara,
                    DTTara = (_order.DTTara == null ? DateNow : _order.DTTara),
                    DTBrutto = (_order.DTBrutto == null ? DateNow : _order.DTTara),
                    Car_Id = _order.SelectedCar.Id,
                }); ;
                _db.SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            await Task.Run(() => AddItemsToList());
        }

        public void AddItemsToList()
        {
            _db = new Database1Entities();
            _db.WeightList.Load();
            var ViewModel = _db.WeightList.ToList().Select(model => new WeightListVM(model));
            CollectionViewVM = new ObservableCollection<WeightListVM>(ViewModel);
        }

        public bool CanSave(object parameter)
        {
            if (Errors == 0)
                return true;
            else
                return false;
        }



        public class DataCharts
        {
            public double valuesTara { get; set; }
            public double valuesBrutto { get; set; }
            public DateTime? dateTara { get; set; }
            public DateTime? dateBrutto { get; set; }
        }
        public int Errors { get; private set; }


        #region INotifyPropertyChanged
        protected void NoticeMe(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }
}

