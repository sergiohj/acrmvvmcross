using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acr.MvvmCross.Plugins.UserDialogs.WPF
{
    public class WPFProgressDialog : IProgressDialog
    {
        //private readonly ProgressPopUp progress = new ProgressPopUp();
        private string text;
        public string Title
        {
            get { return this.text; }
            set
            {
                if (this.text == value)
                    return;

                this.text = value;
                this.Refresh();
            }
        }

        private void Refresh()
        {
                //this.progress.LoadingText = this.text;
                //if (this.IsDeterministic)
                //{
                //    this.progress.PercentComplete = this.percentComplete;
                //    this.progress.CompletionText = this.percentComplete + "%";
                //}
        }

        private int percentComplete;
        public int PercentComplete
        {
            get { return this.percentComplete; }
            set
            {
                if (this.percentComplete == value)
                    return;

                if (value > 100)
                {
                    this.percentComplete = 100;
                }
                else if (value < 0)
                {
                    this.percentComplete = 0;
                }
                else
                {
                    this.percentComplete = value;
                }
                this.percentComplete = value;
                this.Refresh();
            }
        }

        public bool IsDeterministic { get; set;
            //get { return !this.progress.IsIndeterminate; }
            //set { this.progress.IsIndeterminate = !value; }
        }


        public bool IsShowing { get; private set; }


        public void SetCancel(Action onCancel, string cancelText)
        {
            //this.progress.SetCancel(onCancel, cancelText);
        }


        public void Show()
        {
            if (this.IsShowing)
                return;

            this.IsShowing = true;
        }

        public void Hide()
        {
            if (!this.IsShowing)
                return;

            this.IsShowing = false;
        }

        public void Dispose()
        {
            this.Hide();
        }
    }
}
