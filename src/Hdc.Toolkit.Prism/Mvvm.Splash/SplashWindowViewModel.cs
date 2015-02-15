#region File Info Header
/*________________________________________________________________________________________

  Copyright (C) 2011 Jason Zhang, eagleboost@msn.com

  * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
  * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
  * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

________________________________________________________________________________________*/
#endregion File Info Header

using Hdc.Mvvm;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Hdc.Mvvm.Splash
{

    using System;
    using System.ComponentModel;

    public class SplashWindowViewModel : INotifyPropertyChanged
    {
        #region Declarations
        private string _status;
        #endregion

        #region ctor
        public SplashWindowViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<SplashMessageUpdatedEvent>().Subscribe(e => UpdateMessage(e.Message));
        }
        #endregion

        #region Public Properties
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }
        #endregion

        #region Private Methods
        private void UpdateMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            //Status += string.Concat(Environment.NewLine, message, "...");
            Status = string.Concat(string.Format("{0}: ", DateTime.Now), message, Environment.NewLine) + Status;
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}