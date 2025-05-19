//using MVVM_Einheitenumrechner.ViewModel.UnitConverter.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Channels;
//using System.Threading.Tasks;
//using System.Windows;

//namespace MVVM_Einheitenumrechner.ViewModel
//{
//    public class MainViewModel : BaseViewModel
//    {
//        public ObservableCollection<string> UnitCategories { get; set; }
//        public ObservableCollection<string> SourceUnits { get; set; }
//        public ObservableCollection<string> TargetUnits { get; set; }

//        private string _selectedCategory;
//        public string SelectedCategory
//        {
//            get => _selectedCategory;
//            set
//            {
//                _selectedCategory = value;
//                OnPropertyChanged();
//                LoadUnitsForCategory();
//                TryConvert();
//            }
//        }

//        private string _selectedSourceUnit;
//        public string SelectedSourceUnit
//        {
//            get => _selectedSourceUnit;
//            set
//            {
//                _selectedSourceUnit = value;
//                OnPropertyChanged();
//                TryConvert();
//            }
//        }

//        private string _selectedTargetUnit;
//        public string SelectedTargetUnit
//        {
//            get => _selectedTargetUnit;
//            set
//            {
//                _selectedTargetUnit = value;
//                OnPropertyChanged();
//                TryConvert();
//            }
//        }

//        private string _inputValue;
//        public string InputValue
//        {
//            get => _inputValue;
//            set
//            {
//                _inputValue = value;
//                OnPropertyChanged();
//                TryConvert();
//            }
//        }

//        private string _result;
//        public string Result
//        {
//            get => _result;
//            set => SetProperty(ref _result, value);
//        }

//        private string _errorMessage;
//        public string ErrorMessage
//        {
//            get => _errorMessage;
//            set => SetProperty(ref _errorMessage, value);
//        }

//        public MainViewModel()
//        {
//            UnitCategories = new ObservableCollection<string> { "Länge" };
//            SourceUnits = new ObservableCollection<string>();
//            TargetUnits = new ObservableCollection<string>();
//        }

//        private void LoadUnitsForCategory()
//        {
//            SourceUnits.Clear();
//            TargetUnits.Clear();

//            if (SelectedCategory == "Länge")
//            {
//                SourceUnits.Add("Meter");
//                SourceUnits.Add("Zentimeter");
//                SourceUnits.Add("Kilometer");

//                TargetUnits.Add("Meter");
//                TargetUnits.Add("Zentimeter");
//                TargetUnits.Add("Kilometer");
//            }
//        }

//        private void TryConvert()
//        {
//            ErrorMessage = "";
//            Result = "";

//            if (string.IsNullOrWhiteSpace(InputValue) ||
//                string.IsNullOrWhiteSpace(SelectedSourceUnit) ||
//                string.IsNullOrWhiteSpace(SelectedTargetUnit))
//                return;

//            if (!double.TryParse(InputValue, out double value))
//            {
//                ErrorMessage = "Bitte eine gültige Zahl eingeben.";
//                return;
//            }

//            double convertedValue = DummyConvert(value, SelectedSourceUnit, SelectedTargetUnit);
//            Result = $"{value} {SelectedSourceUnit} = {convertedValue} {SelectedTargetUnit}";
//        }

//        private double DummyConvert(double value, string from, string to)
//        {
//            // Nur Länge: einfache Beispiele
//            if (from == to) return value;

//            if (from == "Meter" && to == "Zentimeter") return value * 100;
//            if (from == "Zentimeter" && to == "Meter") return value / 100;
//            if (from == "Meter" && to == "Kilometer") return value / 1000;
//            if (from == "Kilometer" && to == "Meter") return value * 1000;
//            if (from == "Kilometer" && to == "Zentimeter") return value * 100000;
//            if (from == "Zentimeter" && to == "Kilometer") return value / 100000;

//            return value;
//        }
//    }

//}
