using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace ListViewGridLayout
{
    public class ListViewBehavior   : Behavior<MainPage>
    {
        private ListViewGridLayoutViewModel viewModel;        
        private GridLayout gridLayout;
        private SfListView listView;

        protected override void OnAttachedTo(BindableObject bindable)
        {          
            base.OnAttachedTo(bindable);
            viewModel = new ListViewGridLayoutViewModel();
            var mainPage = bindable as MainPage;
            listView = mainPage.FindByName<SfListView>("listView");
            var headereGrid = mainPage.FindByName<Grid>("headerGrid");
            headereGrid.BindingContext = viewModel;
            listView.ItemsSource = viewModel.Gallerynfo;           
            listView.BindingContext = viewModel;         
            listView.SelectionChanged += ListView_SelectionChanged;
            gridLayout = new GridLayout();

            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                gridLayout.SpanCount = Device.Idiom == TargetIdiom.Phone ? 2 : 4;
            else if (Device.RuntimePlatform == Device.UWP)
            {
                Debug.WriteLine(Device.RuntimePlatform);
                gridLayout.SpanCount = Device.Idiom == TargetIdiom.Desktop || Device.Idiom == TargetIdiom.Tablet ? 4 : 2;
                listView.ItemSize = Device.Idiom == TargetIdiom.Desktop || Device.Idiom == TargetIdiom.Tablet ? 230 : 140;
            }

            listView.LayoutManager = gridLayout;
            listView.DataSource.GroupDescriptors.Add(new GroupDescriptor() { PropertyName = "CreatedDate" });
        }
        private void ListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            for (int i = 0; i < e.AddedItems.Count; i++)
            {
                var item = e.AddedItems[i];
                (item as ListViewGalleryInfo).IsSelected = true;
            }
            for (int i = 0; i < e.RemovedItems.Count; i++)
            {
                var item = e.RemovedItems[i];
                (item as ListViewGalleryInfo).IsSelected = false;
            }
            viewModel.RefreshSelection();
        }

    }
}
