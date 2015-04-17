﻿using System;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.ViewModels;
using Sample.Core.ViewModels;


namespace Sample.Core {
    
    public class App : MvxApplication {

        public App() {
            this.RegisterAppStart<HomeViewModel>();
        }


        public override void LoadPlugins(IMvxPluginManager pluginManager) {
            base.LoadPlugins(pluginManager);
            //pluginManager.EnsurePlatformAdaptionLoaded<Acr.MvvmCross.Plugins.BarCodeScanner.PluginLoader>();
            pluginManager.EnsurePlatformAdaptionLoaded<Acr.MvvmCross.Plugins.DeviceInfo.PluginLoader>();
            //pluginManager.EnsurePlatformAdaptionLoaded<Acr.MvvmCross.Plugins.Settings.PluginLoader>();
			pluginManager.EnsurePlatformAdaptionLoaded<Acr.MvvmCross.Plugins.UserDialogs.PluginLoader>();
            //pluginManager.EnsurePlatformAdaptionLoaded<Acr.MvvmCross.Plugins.FileSystem.PluginLoader>();
            //pluginManager.EnsurePlatformAdaptionLoaded<Acr.MvvmCross.Plugins.SignaturePad.PluginLoader>();
            //pluginManager.EnsurePlatformAdaptionLoaded<Cirrious.MvvmCross.Plugins.Color.PluginLoader>();
            //pluginManager.EnsurePlatformAdaptionLoaded<Cirrious.MvvmCross.Plugins.File.PluginLoader>();
            //pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Messenger.PluginLoader>();
        }
    }
}
