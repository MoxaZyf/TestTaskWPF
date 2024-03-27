using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Xceed.Wpf.Toolkit.Calculator;
using TestTaskWPF.Model;
using TestTaskWPF.Data;
using TestTaskWPF.Utils;

namespace TestTaskWPF.ViewModel
{
    public class CarVM: ViewModelBase
    {
        private readonly Database1Entities _db = new Database1Entities();

        public ObservableCollection<Car> CarLisz { get; set; }



        public Car car;
        private readonly Car _car;



        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }


        private string _nameCar;

        public string NameCar
        {
            get => _nameCar;
            set
            {
                _nameCar = value;
                RaisePropertyChanged(nameof(NameCar));
            }
        }


        private int _numCar;
        public int NumCar
        {
            get => _numCar;
            set
            {
                _numCar = value;
                RaisePropertyChanged(nameof(NumCar));
            }
        }

        private double _wTara;
        public double WTara
        {
            get => _wTara;
            set
            {
                _wTara = value;
                RaisePropertyChanged(nameof(WTara));
            }
        }
        public CarVM(Car car)
        {
            _car = car;
           _nameCar = car.Name;
            _numCar = car.NumCar;
            _wTara = car.WTara;



        }
        public CarVM()
        {

        }
      

    }
}

