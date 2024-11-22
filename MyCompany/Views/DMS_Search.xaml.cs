using MyCompany.Model;
using MyCompany.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyCompany.Views;

public partial class DMS_Search : ContentPage
{
    DmsService DmsService = new DmsService();
    public ObservableCollection<DMS> Docslist { get; } = new();
    public ObservableCollection<string> DeptList { get; set; } = new();
    public ObservableCollection<string> CatalogList { get; set; } = new();
    public DMS_Search()
    {
        try
        {
            InitializeComponent();
            //List<string> deplist_temp = new List<string>();
            var deplist_temp = Task.Run(() => DmsService.GetDepts()).Result;
            DeptList.Clear();
            foreach (var dept in deplist_temp)
            {
                DeptList.Add(dept);
            }
            deptname_in.ItemsSource = DeptList;

            var cataloglist_temp = Task.Run(() => DmsService.GetCatalogs()).Result;
            CatalogList.Clear();
            foreach (var catalog in cataloglist_temp)
            {
                CatalogList.Add(catalog);
            }
            doctype_in.ItemsSource = CatalogList;
        }
        //catch (Exception ex)
        catch 
        {
            Shell.Current.DisplayAlert("Error!", "Vui lòng sử dụng mạng nội bộ để truy cập!", "Ok");
        }


    }

    async void SearchAsync(object sender, EventArgs e)
    {
        searchgrid.IsVisible = false;
        resultgrid.IsVisible = true;
        if (IsBusy == true)
            return;
        try
        {
            IsBusy = true;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
            var deptname = "";
            var doctype = "";

            if (deptname_in.SelectedItem is not null)
            {
                deptname = deptname_in.SelectedItem.ToString();
            }
            if (doctype_in.SelectedItem is not null)
            {
                doctype = doctype_in.SelectedItem.ToString();
            }
            Docslist.Clear();
            var docs_list_temp = await DmsService.GetDocs(id_in.Text, docname_in.Text, uploadate_in.Text, searchkey_in.Text, deptname, doctype, company_in.Text, author_in.Text);

            var index = 1;
            foreach (var doc in docs_list_temp)
            {
                if (doc.Dms_name != "")
                {
                    doc.index = index;
                    Docslist.Add(doc);
                    index++;
                }
                
            }
            docscollectionview.ItemsSource = Docslist;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get Documents: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
        }
    }

    void BackSearchAsync(object sender, EventArgs e)
    {
        searchgrid.IsVisible = true;
        resultgrid.IsVisible = false;

    }
    async void GoToDetailsAsync(object sender, EventArgs e)
    {
        Frame frame = (Frame)sender;
        var imageSender = frame.BindingContext;
        var nvaparam = new Dictionary<string, object>
        {
            { nameof(DMS),imageSender}
        };
        await Shell.Current.GoToAsync(nameof(DMS_Detail), true, nvaparam);
    }


}