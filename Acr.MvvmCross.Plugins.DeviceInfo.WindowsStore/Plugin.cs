using Cirrious.CrossCore.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;

namespace Acr.MvvmCross.Plugins.DeviceInfo.WindowsStore
{
    public class Plugin : IMvxPlugin
    {

        public void Load()
        {
            Mvx.RegisterSingleton<IDeviceInfoService>(new WindowsStoreDeviceInfoService());
        }
    }
}
