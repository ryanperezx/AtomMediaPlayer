using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AtomMediaPlayer
{
    class ViewModel
    {
        private ICommand _fullScreen;
        public ICommand fullScreen
        {
            get
            {
                return _fullScreen
                    ?? (_fullScreen = new ActionCommands(() =>
                    {
                        //MessageBox.Show("SomeCommand");
                    }));
            }
        }

    }
}
