using MyCompany.Model;
using MyCompany.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace MyCompany.Views;

[QueryProperty(nameof(DMS), "Dms")]
public partial class DMS_Detail : IQueryAttributable, INotifyPropertyChanged
{
    public DMS Dms { get; private set; }
    private ObservableCollection<DMS> Docslist { get; } = new();
    DmsService DmsService = new DmsService();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Dms = query["DMS"] as DMS;
        var authority = await DmsService.GetDocsAuthority(MainPage.username, Dms.Dms_docid);

        if (authority == true)
        {
            //Docslist.Add(Dms);
            //docscollectionview.ItemsSource = Docslist;
            doc_Dms_docid.Text = doc_Dms_docid.Text + Dms.Dms_docid;
            doc_catolog.Text = doc_catolog.Text + Dms.catolog;
            doc_name.Text = doc_name.Text + Dms.Dms_name;
            doc_author.Text = doc_author.Text + Dms.author;
            doc_company.Text = doc_company.Text + Dms.company;
            doc_depart.Text = doc_depart.Text + Dms.depart;
            List<DMS> dms_details;
            dms_details = await DmsService.GetDocs(Dms.Dms_docid, "", "", "", "", "", "", "");
            var index = 1;
            foreach (var doc in dms_details)
            {
                ServerUrl serverUrl = new ServerUrl();
                doc.index = index;
                Docslist.Add(doc);
                index++;
            }
            docscollectionview.ItemsSource = Docslist;

        }
        else
        {
            docscollectionview.ItemsSource = null;
            await Shell.Current.DisplayAlert("Info!", $"Bạn không có quyền truy cập tài liệu", "Ok");
            await Shell.Current.GoToAsync("..", true);
        }
        OnPropertyChanged("DMS");

    }
    public DMS_Detail()
    {
        InitializeComponent();
        BindingContext = this;
    }
    async void openfile(object sender, EventArgs e)
    {
        bool temp = false;
        Frame frame = (Frame)sender;
        var dms_temp = (DMS)frame.BindingContext;

        try
        {
            IsBusy = true;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
            temp = await DmsService.GetDoc(dms_temp);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable open file: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
        }
        if (temp == false)
        {
            await Shell.Current.DisplayAlert("Info!", $"Thử lại file khác.", "Ok");
        }
        IsBusy = false;
        ActivityIndicatorin.IsVisible = IsBusy;
        ActivityIndicatorin.IsRunning = IsBusy;

    }


}

