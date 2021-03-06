﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.CrossCore;

#if __ANDROID__
using Java.Nio;
using Android.Runtime;
using Android.Graphics;
#elif __IOS__
using UIKit;
#elif WINDOWS_PHONE
using System.Windows.Media.Imaging;
#endif

using ZXing;
using ZXing.Common;
using ZXing.Mobile;


namespace Acr.MvvmCross.Plugins.BarCodeScanner {

    public class BarCodeService : IBarCodeService {

		public BarCodeService() {
            var def = ZXing.Mobile.MobileBarcodeScanningOptions.Default;

			BarCodeReadConfiguration.Default = new BarCodeReadConfiguration {
				AutoRotate = def.AutoRotate,
				CharacterSet = def.CharacterSet,
				DelayBetweenAnalyzingFrames = def.DelayBetweenAnalyzingFrames,
				InitialDelayBeforeAnalyzingFrames = def.InitialDelayBeforeAnalyzingFrames,
				PureBarcode = def.PureBarcode,
				TryHarder = def.TryHarder,
				TryInverted = def.TryInverted,
				UseFrontCameraIfAvailable = def.UseFrontCameraIfAvailable
            };
        }


		public virtual Stream Create(BarCodeCreateConfiguration cfg) {
            var writer = new BarcodeWriter {
				Format = (BarcodeFormat)Enum.Parse(typeof(BarcodeFormat), cfg.Format.ToString()),
                Encoder = new MultiFormatWriter(),
                Options = new EncodingOptions {
					Height = cfg.Height,
					Margin = cfg.Margin,
					Width = cfg.Height,
					PureBarcode = cfg.PureBarcode
                }
            };
#if __IOS__
			return (cfg.ImageType == ImageType.Png)
				? writer.Write(cfg.BarCode).AsPNG().AsStream()
				: writer.Write(cfg.BarCode).AsJPEG().AsStream();
#elif __ANDROID__
			MemoryStream stream = null;

			var cf = cfg.ImageType == ImageType.Png
				? Bitmap.CompressFormat.Png
				: Bitmap.CompressFormat.Jpeg;

			using (var bitmap = writer.Write(cfg.BarCode)) {
//				bitmap.Compress(cf, 0, ms); doesn't work
				var buffer = ByteBuffer.Allocate(bitmap.RowBytes * bitmap.Height);
				bitmap.CopyPixelsToBuffer(buffer);
				buffer.Rewind();
//				var bytes = buffer.ToArray<byte>(); doesn't work
				var classHandle = JNIEnv.FindClass("java/nio/ByteBuffer");
				var methodId = JNIEnv.GetMethodID(classHandle, "array", "()[B");
				var resultHandle = JNIEnv.CallObjectMethod(buffer.Handle, methodId);
				var bytes = JNIEnv.GetArray<byte>(resultHandle);
				JNIEnv.DeleteLocalRef(resultHandle);

				stream = new MemoryStream(bytes);
			}

			stream.Position = 0;
            return stream;
#elif WINDOWS_PHONE
            return new MemoryStream(writer.Write(cfg.BarCode).ToByteArray());
#endif
        }


		public async Task<BarCodeResult> Read(BarCodeReadConfiguration config, CancellationToken cancelToken) {
			config = config ?? BarCodeReadConfiguration.Default;
#if __IOS__
            var scanner = new MobileBarcodeScanner { UseCustomOverlay = false };
#elif __ANDROID__
            var topActivity = Mvx.Resolve<Cirrious.CrossCore.Droid.Platform.IMvxAndroidCurrentTopActivity>().Activity;
            var scanner = new MobileBarcodeScanner(topActivity) { UseCustomOverlay = false };
#elif WINDOWS_PHONE
            var scanner = new MobileBarcodeScanner(System.Windows.Deployment.Current.Dispatcher) { UseCustomOverlay = false };
#endif
			cancelToken.Register(scanner.Cancel);

            var result = await scanner.Scan(this.GetXingConfig(config));
            return (result == null || String.IsNullOrWhiteSpace(result.Text)
                ? BarCodeResult.Fail
                : new BarCodeResult(result.Text, FromXingFormat(result.BarcodeFormat))
            );
        }


        private static BarCodeFormat FromXingFormat(ZXing.BarcodeFormat format) {
            return (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), format.ToString());
        }


		private MobileBarcodeScanningOptions GetXingConfig(BarCodeReadConfiguration cfg) {
            var opts = new MobileBarcodeScanningOptions {
                AutoRotate = cfg.AutoRotate,
                CharacterSet = cfg.CharacterSet,
                DelayBetweenAnalyzingFrames = cfg.DelayBetweenAnalyzingFrames,
                InitialDelayBeforeAnalyzingFrames = cfg.InitialDelayBeforeAnalyzingFrames,
                PureBarcode = cfg.PureBarcode,
                TryHarder = cfg.TryHarder,
                TryInverted = cfg.TryInverted,
                UseFrontCameraIfAvailable = cfg.UseFrontCameraIfAvailable
            };

            if (cfg.Formats != null && cfg.Formats.Count > 0) {
                opts.PossibleFormats = cfg.Formats
                    .Select(x => (BarcodeFormat)(int)x)
                    .ToList();
            }
            return opts;
        }
    }
}
