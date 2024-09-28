using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.WPF.Dialogs
{
    public interface IDialogService
    {
        /// <summary>
        /// Shows the dialog
        /// </summary>
        /// <param name="name">the name of the registered dialog</param>
        void Show(string name);
    }
}
