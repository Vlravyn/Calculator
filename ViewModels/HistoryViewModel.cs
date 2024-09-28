using Calculator.Core;
using Calculator.Core.OldCalculations;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.WPF.ViewModels
{
    public class HistoryViewModel : ObservableObject
    {
        public ObservableCollection<IOldCalculation> OldCalculations { get; set; }

        /// <summary>
        /// Default constructor for design time purposes.
        /// </summary>
        public HistoryViewModel()
        {
            
        }

        /// <summary>
        /// The constructor that the IoC will use.
        /// </summary>
        /// <param name="history"></param>
        public HistoryViewModel(History history)
        {
            history.NewHistoryadded += NewCalculationAdded;
            OldCalculations = new(history.CalculationHistory);
        }

        private void NewCalculationAdded(object? sender, EventArgs e)
        {
            var newValue = (e as AddingNewEventArgs)?.NewObject as IOldCalculation;

            if (newValue != null)
                OldCalculations.Insert(0, newValue);
        }
    }
}
