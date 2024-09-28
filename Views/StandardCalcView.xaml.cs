using Calculator.WPF.ViewModels;
using System.Windows;

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
    }
}
