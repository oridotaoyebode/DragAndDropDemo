using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Digiterra.DragDrop.Demo
{
    public class MainViewModel: BaseViewModel
    {
        private ObservableCollection<string> _images;
        public ObservableCollection<string> Images
        {
            get => _images;
            set => this.SetProperty(ref _images, value);
        }

        public MainViewModel()
        {
            Images = new ObservableCollection<string>()
            {
               IonicIconsFont.Earth,
               IonicIconsFont.IosAlarmOutline,
               IonicIconsFont.IosBoltOutline,
               IonicIconsFont.IosBriefcaseOutline
            };
        }
    }
}
