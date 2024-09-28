using Calculator.Core.OldCalculations;
using Calculator.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator.WPF.Views
{
    /// <summary>
    /// Interaction logic for StandardCalcView.xaml
    /// </summary>
    public partial class StandardCalcView : Window
    {
        public StandardCalcView(StandardCalcViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Button? btn;
            switch(e.Key)
            {
                case Key.D0:
                case Key.NumPad0 :
                    btn =  Number0Button;
                    break;
                case Key.D1:
                case Key.NumPad1 :
                    btn = Number1Button;
                    break;
                case Key.D2:
                case Key.NumPad2 : 
                    btn = Number2Button;
                    break;
                case Key.D3:
                case Key.NumPad3 : 
                    btn = Number3Button;
                    break;
                case Key.D4:
                case Key.NumPad4 : 
                    btn = Number4Button;
                    break;
                case Key.D5:
                case Key.NumPad5 :
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        btn = PercentageButton;
                    else
                        btn = Number5Button;
                    break;
                case Key.D6:
                case Key.NumPad6 :
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        btn = ExponentButton;
                    else
                        btn = Number6Button;
                    break;
                case Key.D7 :
                case Key.NumPad7 : 
                    btn = Number7Button;
                    break;
                case Key.D8 :
                case Key.NumPad8 :
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        btn = MultiplyButton;
                    else
                        btn = Number8Button;
                    break;
                case Key.D9:
                case Key.NumPad9:
                    btn = Number9Button;
                    break;
                case Key.OemPlus:
                case Key.Add:
                    btn = AdditionButton;
                    break;
                case Key.OemMinus:
                case Key.Subtract:
                    btn = SubtractionButton;
                    break;
                case Key.Multiply:
                case Key.Oem8:
                    btn = MultiplyButton;
                    break;
                case Key.Divide:
                case Key.OemQuestion:
                    btn = DivideButton;
                    break;
                case Key.Decimal:
                    btn = DecimalButton;
                    break;
                case Key.Back:
                    btn = BackspaceButton;
                    break;
                case Key.Enter:
                    btn = EqualToButton;
                    break;
                default:
                    btn = null;
                    break;
            };

            btn?.Command.Execute(btn.CommandParameter);
        }
    }
}
