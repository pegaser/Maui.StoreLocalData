using SQLite;

namespace Maui.StoreLocalData.SqlLiteExample
{
    public partial class MainPage : ContentPage
    {
        static string filename = Path.Combine("");
        static SQLiteConnection conn;
        public MainPage()
        {
            InitializeComponent();
            conn = new SQLiteConnection(filename);
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            conn.CreateTable<User>();
        }
        public int AddNewUser(User user)
        {
            int result = conn.Insert(user);
            return result;
        }
        public List<User> GetAllUsers()
        {
            List<User> users = conn.Table<User>().ToList();
            return users;
        }
        public User GetByUsername(string username)
        {
            var user = from u in conn.Table<User>()
                       where u.Username == username
                       select u;
            return user.FirstOrDefault();
        }
        public int UpdateUser(User user)
        {
            int result = 0;
            result = conn.Update(user);
            return result;
        }
        public int DeleteUser(int userID)
        {
            int result = 0;
            result = conn.Delete<User>(userID);
            return result;
        }

    }
    [Table("user")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(250), Unique]
        public string Username { get; set; }
    }

}