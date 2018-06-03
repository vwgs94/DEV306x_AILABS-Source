using AIVisionExplorer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace AIVisionExplorer.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        public MainViewModel()
        {
            BrowseForImageCommand = new RelayCommand(async () => { await BrowseForImageAsync(); });    
            CourseChangedCommand = new RelayCommand<SelectionChangedEventArgs>((args) => { CourseSelectionChanged(args); });
        }
        
        public ICommand BrowseForImageCommand { get; private set; }
        public ICommand CourseChangedCommand { get; private set; }        

        private ImageInformation _currentImage;
        public ImageInformation CurrentImage
        {
            get { return _currentImage; }
            set { Set(ref _currentImage, value); }
        }

        private CourseType _currentCourseType;
        public CourseType CurrentCourseType
        {
            get { return _currentCourseType; }
            set { Set(ref _currentCourseType, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        public async Task<bool> BrowseForImageAsync()
        {
            return false;
        }

        private void CourseSelectionChanged(object args)
        {
            this.CurrentCourseType = args.AsCourseType();
        }
    }
}
