using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Acr.MvvmCross.Plugins.UserDialogs.WPF
{
    public class WPFUserDialogService : AbstractUserDialogService
    {
        public override void Alert(AlertConfig config)
        {
            MessageBoxButton okBtn = MessageBoxButton.OK;              
            MessageBox.Show(config.Message, config.Title, okBtn);
        }

        public override void ActionSheet(ActionSheetConfig config)
        {
            throw new NotImplementedException();
        }

        public override void Confirm(ConfirmConfig config)
        {
            try
            {
                MessageBoxButton optionsBtn = MessageBoxButton.OKCancel;
                MessageBoxResult result = MessageBox.Show(config.Message, config.Title, optionsBtn);
                if (result.ToString()=="OK")
                    config.OnConfirm(true);
                else if (result.ToString()=="Cancel")
                    config.OnConfirm(false);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
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

        protected override IProgressDialog CreateDialogInstance()
        {
            return new WPFProgressDialog();
        }
    }
}
