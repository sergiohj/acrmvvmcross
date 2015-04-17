using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;


namespace Acr.MvvmCross.Plugins.UserDialogs.WindowsStore
{

    public class WinStoreUserDialogService : AbstractUserDialogService
    {
        //// TODO: dispatching

        //public override void ActionSheet(ActionSheetOptions options)
        //{
        //    var input = new InputDialog
        //    {
        //        ButtonsPanelOrientation = Orientation.Vertical
        //    };

        //    var buttons = options.Options
        //        .Select(x => x.Text)
        //        .ToArray();

        //    input
        //        .ShowAsync(options.Title, null, buttons)
        //        .ContinueWith(x =>
        //            options
        //                .Options
        //                .Single(y => y.Text == x.Result)
        //                .Action()
        //        );
        //}


        //public override void Alert(string message, string title, string okText, Action onOk)
        //{
        //    var input = new InputDialog();

        //    input
        //        .ShowAsync(title, message, okText)
        //        .ContinueWith(x =>
        //        {
        //            if (onOk != null)
        //                onOk();
        //        });
        //}


        //public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText)
        //{
        //    var input = new InputDialog
        //    {
        //        AcceptButton = okText,
        //        CancelButton = cancelText
        //    };
        //    input
        //        .ShowAsync(title, message)
        //        .ContinueWith(x =>
        //        {
        //            // TODO: how to get button click for this scenario?
        //        });
        //}


        //public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint)
        //{
        //    var input = new InputDialog
        //    {
        //        AcceptButton = okText,
        //        CancelButton = cancelText,
        //        InputText = hint
        //    };
        //    input
        //        .ShowAsync(title, message)
        //        .ContinueWith(x =>
        //        {
        //            // TODO: how to get button click for this scenario?
        //        });
        //}


        //public override void Toast(string message, int timeoutSeconds, Action onClick)
        //{
        //    //http://msdn.microsoft.com/en-us/library/windows/apps/hh465391.aspx
        //    //  TODO: Windows.UI.Notifications.

        //    //var toast = new ToastPrompt {
        //    //    Message = message,
        //    //    MillisecondsUntilHidden = timeoutSeconds * 1000
        //    //};
        //    //if (onClick != null) {
        //    //    toast.Tap += (sender, args) => onClick();
        //    //}
        //    //toast.Show();
        //}


        //protected override WinStoreProgressDialog CreateProgressDialogInstance()
        //{
        //    return new WinStoreProgressDialog();
        //}

        //public override void Alert(AlertConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void ActionSheet(ActionSheetConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Confirm(ConfirmConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Login(LoginConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Prompt(PromptConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override IProgressDialog CreateDialogInstance()
        //{
        //    throw new NotImplementedException();
        //}
        private IAsyncOperation<IUICommand> uiCommand;
        public async override void Alert(AlertConfig config)
        {
            if (uiCommand != null)
            {
                uiCommand.Cancel();
            }
            CoreDispatcher dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    var dialog = new MessageDialog(config.Message, config.Title);
                    //OK Button
                    UICommand okBtn = new UICommand(config.OkText);
                    if (config.OnOk != null)
                        okBtn.Invoked = command => config.OnOk();
                    dialog.Commands.Add(okBtn);
                        uiCommand = dialog.ShowAsync();                    
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            });
        }

        public override void ActionSheet(ActionSheetConfig config)
        {
            throw new NotImplementedException();
        }

        public async override void Confirm(ConfirmConfig config)
        {
            if (uiCommand != null)
            {
                uiCommand.Cancel();
            }
            CoreDispatcher dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    MessageDialog dialog = new MessageDialog(config.Message, config.Title);
                    UICommand okBtn = new UICommand(config.OkText);
                    UICommand cancelBtn = new UICommand(config.CancelText);
                    okBtn.Invoked = command => config.OnConfirm(true);
                    cancelBtn.Invoked = command => config.OnConfirm(false);
                    dialog.Commands.Add(okBtn);
                    dialog.Commands.Add(cancelBtn);
                        uiCommand = dialog.ShowAsync();                    
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            });
        }

        public override void Login(LoginConfig config)
        {
            throw new NotImplementedException();
        }

        public override void Prompt(PromptConfig config)
        {
            throw new NotImplementedException();
        }

        public override void Toast(string message, int timeoutSeconds = 3, Action onClick = null)
        {
            throw new NotImplementedException();
        }
        #region · Protected ·
        protected override IProgressDialog CreateDialogInstance()
        {
            return new WindowsStoreProgressDialog();
        }
        #endregion
        #region · Private ·
        #endregion
    }
}
