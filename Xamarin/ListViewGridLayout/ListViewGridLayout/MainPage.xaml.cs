using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewGridLayout
{
	  public partial class MainPage : ContentPage
    {
        private double pageWidth;
        #region Constructor       
        public MainPage()
        {
            InitializeComponent();           
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            try
            {
                base.OnSizeAllocated(width, height);

                if (width > 0 && pageWidth != width)
                {
                    var size = Application.Current.MainPage.Width / listView.ItemSize;
                    var gridLayout = listView.LayoutManager as GridLayout;
                    gridLayout.SpanCount = (int)size;                  
                    listView.LayoutManager = gridLayout;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }    
}
                