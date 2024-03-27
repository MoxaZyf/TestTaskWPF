using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestTaskWPF.Data;
using TestTaskWPF.Model;
using TestTaskWPF.Utils;
using static Xceed.Wpf.Toolkit.Calculator;


namespace TestTaskWPF.ViewModel
{
  public  class WeightListVM: ViewModelBase
    {
        private readonly Database1Entities _db = new Database1Entities();
        public ObservableCollection<WeightList> List { get; set; }

        public WeightList order;
        private readonly WeightList _order;


        public WeightList list;
        private readonly WeightList _list;
        private int _numCar;
        public int NumCar
        {
            get { return _numCar; }
            set
            {
                _numCar = value;
                RaisePropertyChanged("NumCar");
            }
        }

        private double _wNetto;
        public double WNetto
        {
            get { return _wNetto; }
            set
            {
                _wNetto = value;
                RaisePropertyChanged("WNetto");
            }
        }
        private double _wBrutto;
        public double WBrutto
        {
            get { return _wBrutto; }
            set
            {
                _wBrutto = value;
                RaisePropertyChanged("WBrutto");
            }
        }
        private double _wTara;
        public double WTara
        {
            get { return _wTara; }
            set
            {
                _wTara = value;
                RaisePropertyChanged("WTara");
            }
        }

        private DateTime? _dTTara;
        public DateTime? DTTara
        {
            get { return _dTTara; }
            set
            {
                _dTTara = value;
                RaisePropertyChanged("DTTara");
            }
        }
        private DateTime? _dTBrutto;
        public DateTime? DTBrutto
        {
            get { return _dTBrutto; }
            set
            {
                _dTBrutto = value;
                RaisePropertyChanged("DTBrutto");
            }
        }
        private ObservableCollection<WeightList> _weightList;
        private WeightList model;
        private ObservableCollection<Car> _Car;
        public ObservableCollection<Car> Cars
        {
            get => _Car;
            set
            {
                _Car = value;
                RaisePropertyChanged(nameof(Cars));
               
            }
        }

        private Car _SelectedCar;
        public Car SelectedCar
        {
            get { return _SelectedCar; }
            set
            {
                if (_SelectedCar == value)
                    return;
                _SelectedCar = value;
                RaisePropertyChanged("SelectedCar");
                WTara= SelectedCar.WTara;
                //MessageBox.Show(SelectedOperator.Id.ToString());
                MessageBox.Show(SelectedCar.WTara.ToString());
            }
        }
        private int _carStr;
        public int CarStr
        {
            get => _carStr;
            set
            {
                _carStr = value;
                RaisePropertyChanged(nameof(CarStr));
            }
        }

        private Car _Car_Id;
        public Car Car_Id
        {
            get => _Car_Id;
            set
            {
                _Car_Id = value;
                RaisePropertyChanged(nameof(Car_Id));
            }
        }


        private int _Car_Id_Int;
        public int Car_IdINT
        {
            get => _Car_Id_Int;
            set
            {
                _Car_Id_Int = value;
                RaisePropertyChanged(nameof(Car_IdINT));
            }
        }
        public WeightListVM(WeightList order)
        {
            _list = order;
           
            _numCar = (int)order.NumCar;
            _wBrutto = (double)order.WBrutto;
            _wTara = (double)order.WTara;
            _wNetto = (double)order.WNetto;
            _dTBrutto = (DateTime)order.DTBrutto;
            _dTTara = (DateTime)order.DTTara;


            var o = _db.Car.Where(x => x.Id == order.Car_Id).Select(x => x.NumCar);
            foreach (var carstr in o)
            {
                _carStr = carstr;
            }
             Car_IdINT = order.Car.Id;
        }

        public WeightListVM()
        {
            Cars = new ObservableCollection<Car>(_db.Car.OrderBy(x => x.NumCar).ToList());
        }

       
        public ObservableCollection<WeightList> WeightList
        {
            get
            {
                return _weightList;
            }
            set
            {
                _weightList = value;
                RaisePropertyChanged("WeightList");
            }
        }
      
    }
}
