using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVideoPlayer.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Model.VideoModel _videoModel;
        public Model.VideoModel VideoModel
        {
            get
            {
                return _videoModel;
            }
            set
            {
                _videoModel = value;
                RaisePropertyChanged(nameof(VideoModel));
            }
        }
    }
}
