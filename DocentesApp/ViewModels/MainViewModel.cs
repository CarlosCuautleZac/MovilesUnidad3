using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentesApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {


        public void PropertyChange(string property = "") 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property)); 
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
