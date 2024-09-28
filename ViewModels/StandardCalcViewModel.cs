using Calculator.Core;
using Calculator.WPF.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Calculator.WPF.ViewModels
{
    public class StandardCalcViewModel : ObservableObject
    {
        #region Private members

        private double _firstNumber;
        private double _secondNumber;
        private double _finalNumber;
        private string? _currentOperator;

        private readonly IDialogService _dialogService;

        private bool _isOperatorUsed = false;
        
        private Base10NumberGenerator _numberGenerator;

        private readonly History _history;

        private readonly Calculate _calculate;
        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public StandardCalcViewModel(History history, Calculate calculate, IDialogService dialogService)
        {
            _numberGenerator = new();
            _dialogService = dialogService;
            _history = history;
            _calculate = calculate;
        }

        #endregion

        #region Public Properties

        public bool IsDecimalUsed
        {
            get => _numberGenerator.IsDecimalUsed;
            set
            {
                _numberGenerator.IsDecimalUsed = value;
                OnPropertyChanged(nameof(IsDecimalUsed));
            }
        }

        public double FirstNumber
        {
            get => _firstNumber;
            set => SetProperty(ref _firstNumber, value);
        }
        public double SecondNumber
        {
            get => _secondNumber;
            set => SetProperty(ref _secondNumber, value);
        }
        public double FinalNumber
        {
            get => _finalNumber;
            set => SetProperty(ref _finalNumber, value);
        }
        public string? CurrentOperator
        {
            get => _currentOperator;
            set => SetProperty(ref _currentOperator, value);
        }

        #endregion

        #region RelayCommands
        public RelayCommand HistoryCommand => new(OpenHistory);
        public RelayCommand<string> NumberButtonCommand => new(num => CreateNumber(short.Parse(num)));
        public RelayCommand<string> OperatorPressedCommand => new(OperatorPressed);
        public RelayCommand EqualToCommand => new(EqualTo);
        public RelayCommand DecimalCommand => new(AddDecimal);
        public RelayCommand PositiveNegativeCommand => new(ToggleNegative);
        public RelayCommand CCommand => new(ClearAll);
        public RelayCommand CECommand => new(ClearRecentEntry);
        public RelayCommand BackspaceCommand => new(ClearLastEntry);

        #endregion

        #region Private methods

        private void OpenHistory()
        {
            _dialogService.Show("historyWindow");
        }

        /// <summary>
        /// Clears the last entered digit of the current number.
        /// </summary>
        private void ClearLastEntry()
        {
            _numberGenerator.ClearLastEnteredDigit();
            if (_isOperatorUsed)
                SecondNumber = _numberGenerator.GetNumber();
            else
                FirstNumber = _numberGenerator.GetNumber();

            //enable the decimal button if the decimal was the last used button
            if (_numberGenerator.IsDecimalTheLastEnteredDigit)
                IsDecimalUsed = false;
        }

        /// <summary>
        /// Adds a decimal to the number being entered
        /// </summary>
        private void AddDecimal()
        {
            _numberGenerator.AddDecimal();
            OnPropertyChanged(nameof(IsDecimalUsed));
        }

        /// <summary>
        /// Clears the number that was being entered.
        /// </summary>
        private void ClearRecentEntry()
        {
            if (_isOperatorUsed)
                SecondNumber = 0;
            else
                FirstNumber = 0;

            _numberGenerator = new();
            IsDecimalUsed = false;
        }

        /// <summary>
        /// Clears all the inputs and returns to calculator's default state
        /// </summary>
        private void ClearAll()
        {
            FirstNumber = 0;
            SecondNumber = 0;
            _isOperatorUsed = false;
            FinalNumber = 0;
            _numberGenerator = new();
            CurrentOperator = null;
            IsDecimalUsed = false;
        }

        /// <summary>
        /// Toggles the negative/positive on the number that is being entered.
        /// </summary>
        private void ToggleNegative()
        {
            _numberGenerator.ToggleNegative();

            if (_isOperatorUsed)
                SecondNumber = _numberGenerator.GetNumber();
            else
                FirstNumber = _numberGenerator.GetNumber();
        }

        /// <summary>
        /// Calculates the final value.
        /// </summary>
        private void EqualTo()
        {
            if (CurrentOperator == null)
                return;
            if (SecondNumber == 0)
                SecondNumber = FirstNumber;

            FinalNumber = _calculate.StandardCalcFindProduct(FirstNumber, CurrentOperator, SecondNumber);


            //updating the values for next calculation
            _isOperatorUsed = false;
            FirstNumber = 0;
            SecondNumber = 0;
            CurrentOperator = null;
            _numberGenerator = new();
        }

        /// <summary>
        /// Executes all the necessary calculations(previous pending calculations) and stores them as the first number.
        /// If the operator that is pressed only requires a single number, then executes on that value without touching the inputs before.
        /// </summary>
        /// <param name="ch">the operator that was pressed</param>
        /// <exception cref="Exception">Caused when an invalid operator is pressed</exception>
        private void OperatorPressed(string ch)
        {
            if (Calculate.StandardCalcCompatibleOperators.Contains(ch))
            {
                if(Calculate.StandardCalcSingleNumberOperators.Contains(ch))
                {
                    if (_isOperatorUsed)
                        SecondNumber = _calculate.StandardCalcFindProduct(SecondNumber, ch);
                    else if (FirstNumber == 0)
                        FinalNumber = _calculate.StandardCalcFindProduct(FinalNumber, ch);
                    else
                        FirstNumber = _calculate.StandardCalcFindProduct(FirstNumber, ch);
                }
                else
                {
                    if (_isOperatorUsed)
                        EqualTo();
                    if(FirstNumber == 0)
                    {
                        FirstNumber = FinalNumber;
                        SecondNumber = 0;
                    }
                    CurrentOperator = ch;
                    _isOperatorUsed = true;
                    _numberGenerator = new();
                    IsDecimalUsed = false;
                }
            }
            else
                throw new Exception("Invalid operator");
        }

        /// <summary>
        /// Inputs the entered digit to the <seealso cref="_numberGenerator"/>, creates the number and updates it's value
        /// </summary>
        /// <param name="input">the digit that was input</param>
        private void CreateNumber(short input)
        {
            if (input <= 0 && input > 9)
                return;

            _numberGenerator.AddNextDigit(input);

            if (_isOperatorUsed)
                SecondNumber = _numberGenerator.GetNumber();
            else
                FirstNumber = _numberGenerator.GetNumber();
        }

        #endregion
    }
}
