using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acr.MvvmCross.Plugins.UserDialogs.WPF
{
    public class Plugin : IMvxPlugin
    {

        public void Load()
        {
            Mvx.RegisterSingleton<IUserDialogService>(new WPFUserDialogService());
        }
    }
}
