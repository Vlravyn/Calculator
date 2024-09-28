using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator.WPF.Dialogs
{
    public class DialogService : IDialogService
    {
        public void Show(string name)
        {
            var k = App.GetKeyedService<IDialogWindow>(name) as Window;
            k.Show();
        }
    }
}
