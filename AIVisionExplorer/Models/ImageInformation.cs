using AIVisionExplorer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace AIVisionExplorer
{
    public class ImageInformation : ObservableBase
    {        
        public ImageInformation(Canvas detectionCanvas = null)
        {
            DetectionCanvas = detectionCanvas;
            AnalyzeImageCommand = new RelayCommand(async () => { await AnalyzeImageAsync(); });
        }

        public async Task<bool> AnalyzeImageAsync()
        {
            bool successful = false;
            this.IsBusy = true;

            try
            {
                var analysis = await Helpers.ComputerVisionHelper.AnalyzeImageAsync(this.FileBytes);

                this.Description = analysis.caption.ToFirstCharUpper();
                this.Tags = new ObservableCollection<string>(analysis.tags);
                this.IsClipart = Convert.ToBoolean(analysis.details.imageType.clipArtType);
                this.IsLineDrawing = Convert.ToBoolean(analysis.details.imageType.lineDrawingType);
                this.DominantColors = analysis.details.color.dominantColors.ToList();
                this.AccentColor = new SolidColorBrush(($"#FF{analysis.details.color.accentColor}").GetColorFromHex());
                this.ForegroundColor = analysis.details.color.dominantColorForeground;
                this.BackgroundColor = analysis.details.color.dominantColorBackground;
                this.IsAdult = analysis.details.adult.isAdultContent;
                this.AdultScore = analysis.details.adult.adultScore;
                this.IsRacy = analysis.details.adult.isRacyContent;
                this.RacyScore = analysis.details.adult.racyScore;
                this.ImageFormat = analysis.details.metadata.format.ToUpper();
                this.ImageHeight = analysis.details.metadata.height;
                this.ImageWidth = analysis.details.metadata.width;

                successful = true;

            }
            catch (Exception ex)
            {
            }

            this.IsBusy = false;
            return successful;
        }

        public ICommand AnalyzeImageCommand { get; private set; }
       
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }
        
        private string _url;
        public string Url
        {
            get { return _url; }
            set { Set(ref _url, value); }
        }

        private ObservableCollection<string> _tags;
        public ObservableCollection<string> Tags
        {
            get { return _tags; }
            set { Set(ref _tags, value); }
        }

        private byte[] _fileBytes;
        public byte[] FileBytes
        {
            get { return _fileBytes; }
            set { Set(ref _fileBytes, value); }
        }

        private bool _isAdult;
        public bool IsAdult
        {
            get { return _isAdult; }
            set { Set(ref _isAdult, value); }
        }

        private double _adultScore;
        public double AdultScore
        {
            get { return _adultScore; }
            set { Set(ref _adultScore, value); }
        }

        private bool _isRacy;
        public bool IsRacy
        {
            get { return _isRacy; }
            set { Set(ref _isRacy, value); }
        }

        private double _racyScore;
        public double RacyScore
        {
            get { return _racyScore; }
            set { Set(ref _racyScore, value); }
        }

        private List<string> _dominantColors;
        public List<string> DominantColors
        {
            get { return _dominantColors; }
            set { Set(ref _dominantColors, value); }
        }

        private SolidColorBrush _accentColor;
        public SolidColorBrush AccentColor
        {
            get { return _accentColor; }
            set { Set(ref _accentColor, value); }
        }

        private string _foregroundColor;
        public string ForegroundColor
        {
            get { return _foregroundColor; }
            set { Set(ref _foregroundColor, value); }
        }

        private string _backgroundColor;
        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set { Set(ref _backgroundColor, value); }
        }

        private bool _isClipart;
        public bool IsClipart
        {
            get { return _isClipart; }
            set { Set(ref _isClipart, value); }
        }

        private bool _isLineDrawing;
        public bool IsLineDrawing
        {
            get { return _isLineDrawing; }
            set { Set(ref _isLineDrawing, value); }
        }

        private int _imageWidth;
        public int ImageWidth
        {
            get { return _imageWidth; }
            set { Set(ref _imageWidth, value); }
        }

        private int _imageHeight;
        public int ImageHeight
        {
            get { return _imageHeight; }
            set { Set(ref _imageHeight, value); }
        }

        private string _imageFormat;
        public string ImageFormat
        {
            get { return _imageFormat; }
            set { Set(ref _imageFormat, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        public Canvas DetectionCanvas { get; set; }
        
    }
}
