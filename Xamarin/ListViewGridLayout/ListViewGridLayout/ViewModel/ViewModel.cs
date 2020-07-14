using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewGridLayout
{
    public class ListViewGridLayoutViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<ListViewGalleryInfo> galleryinfo;
        private Command deleteImageTapped;
        private ImageSource delete;
        private string headerInfo;
        private string titleInfo;
        private bool isVisible;

        #endregion

        #region Constructor

        public ListViewGridLayoutViewModel()
        {
            titleInfo = "Photos";
            headerInfo = "";
            delete = ImageSource.FromResource("ListViewGridLayout.Images.Delete1.png");
            GenerateSource();
            deleteImageTapped = new Command(DeleteImageTapped);
            SelectedItems = new ObservableCollection<object>();
        }
       
        #endregion

        #region Properties

        public ObservableCollection<ListViewGalleryInfo> Gallerynfo
        {
            get { return galleryinfo; }
            set { this.galleryinfo = value; }
        }

        public ObservableCollection<object> SelectedItems { get; set; }


        public ImageSource Delete
        {
            get
            {
                return delete;
            }
            set
            {
                if (delete != value)
                {
                    delete = value;
                    OnPropertyChanged("Delete");
                }
            }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    OnPropertyChanged("IsVisible");
                }
            }
        }

        public string HeaderInfo
        {
            get { return headerInfo; }
            set
            {
                if (headerInfo != value)
                {
                    headerInfo = value;
                    OnPropertyChanged("HeaderInfo");
                }
            }
        }

        public string TitleInfo
        {
            get { return titleInfo; }
            set
            {
                if (titleInfo != value)
                {
                    titleInfo = value;
                    OnPropertyChanged("TitleInfo");
                }
            }
        }

        public Command DeleteCommand
        {
            get { return this.deleteImageTapped; }
            set { this.deleteImageTapped = value; }
        }

        #endregion

        #region ItemSource

        public void GenerateSource()
        {
            ListViewGalleryInfoRepository bookRepository = new ListViewGalleryInfoRepository();
            galleryinfo = bookRepository.GetGalleryInfo();
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        internal void RefreshSelection()
        {
            if (SelectedItems.Count > 0)
            {
                TitleInfo = "";
                HeaderInfo = SelectedItems.Count == 1 ? SelectedItems.Count + " Photo Selected" : SelectedItems.Count + " Photos Selected";
                IsVisible = true;
            }
            else
            {
                TitleInfo = "Photos";
                HeaderInfo = "";
                IsVisible = false;
            }
        }
        private void DeleteImageTapped()
        {
            var galleryInfo = SelectedItems.ToList();

            foreach (ListViewGalleryInfo item in galleryInfo)
            {
                if (Gallerynfo.Contains(item))
                    Gallerynfo.Remove(item);
            }
            RefreshSelection();
        }
    }
}
