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
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                this.IsBusy = true;

                var fileProperties = await file.Properties.GetImagePropertiesAsync();

                byte[] imageBytes = await file.AsByteArrayAsync();

                var image = new ImageInformation()
                {
                    DisplayName = file.DisplayName,
                    Description = "(no description)",
                    Tags = new ObservableCollection<string>(),
                    FileBytes = imageBytes,
                    Url = file.Path,
                    ImageHeight = (int)fileProperties.Height,
                    ImageWidth = (int)fileProperties.Width,
                };

                image.Url = await Helpers.StorageHelper.SaveToTemporaryFileAsync("VisionServices", file.Name, imageBytes);

                this.CurrentImage = image;
                this.IsBusy = false;
            }

            return file != null;
        }

        private void CourseSelectionChanged(object args)
        {
            this.CurrentCourseType = args.AsCourseType();
        }
    }
}
