using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace Maui.StoreLocalData.AsyncExample
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbPath);
        ObservableCollection<User> userList;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var conn = new SQLiteAsyncConnection(dbPath);
            await conn.CreateTableAsync<User>();
            

            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        public async Task AddAllUSersAsync()
        {
            List<User> users = await conn.Table<User>().ToListAsync();
            foreach (User user in users)
            {
                userList.Add(user);
            }
        }
        private class User
        {
        }
    }
}