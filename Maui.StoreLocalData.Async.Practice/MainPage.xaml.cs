using Maui.StoreLocalData.Async.Practice.Models;

namespace Maui.StoreLocalData.Async.Practice
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnNewButtonClicked(object sender, EventArgs e)
        {
            statusMessage.Text = "";
            await App.PersonRepo.AddNewPerson(newPerson.Text);
            statusMessage.Text = App.PersonRepo.StatusMessage;
        }

        private async void OnGetButtonClicked(object sender, EventArgs e)
        {
            statusMessage.Text = "";
            List<Person> people = await App.PersonRepo.GetAllPeople();
            peopleList.ItemsSource = people;
            statusMessage.Text = App.PersonRepo.StatusMessage;
        }
    }
}