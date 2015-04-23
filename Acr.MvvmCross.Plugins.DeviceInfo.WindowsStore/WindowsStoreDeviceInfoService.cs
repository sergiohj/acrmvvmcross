using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.ApplicationModel;
using Windows.Graphics.Display;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.Xaml;

namespace Acr.MvvmCross.Plugins.DeviceInfo.WindowsStore
{
    public class WindowsStoreDeviceInfoService : IDeviceInfoService
    {
        private readonly Lazy<string> deviceId;
        public WindowsStoreDeviceInfoService()
        {
            this.deviceId = new Lazy<string>(() =>
            {
                HardwareToken myToken = HardwareIdentification.GetPackageSpecificToken(null);
                return myToken.Id.ToString();
            });
            switch (GetScaleFactor())
            {

                case 150:
                    this.ScreenWidth = 720;
                    this.ScreenHeight = 1280;
                    break;

                case 160:
                    this.ScreenWidth = 768;
                    this.ScreenHeight = 1280;
                    break;

                case 100:
                default:
                    this.ScreenWidth = 480;
                    this.ScreenHeight = 800;
                    break;
            }
        }
        private static int GetScaleFactor()
        {
            ResolutionScale resolutionScale = DisplayInformation.GetForCurrentView().ResolutionScale;
            double factor = (double)resolutionScale / 100.0;
            return Convert.ToInt32(factor);
        }
        public int ScreenHeight
        {
            get;
            private set;
        }

        public int ScreenWidth
        {
            get;
            private set;
        }

        public string AppVersion
        {
            get { return Package.Current.Id.Version.ToString(); }
        }

        public string DeviceId
        {
            get { return this.deviceId.Value; }
        }

        public string Manufacturer
        {
            get
            {
                Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
                return deviceInfo.SystemManufacturer;
            }
        }

        public string Model
        {
            get { throw new NotImplementedException(); }
        }

        public string OperatingSystem
        {
            get
            {
                Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
                return deviceInfo.OperatingSystem;
            }
        }

        public bool IsFrontCameraAvailable
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsRearCameraAvailable
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsSimulator
        {
            get { throw new NotImplementedException(); }
        }
    }
}
