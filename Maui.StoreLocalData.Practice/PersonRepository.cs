using Maui.StoreLocalData.Practice.Models;
using SQLite;

namespace Maui.StoreLocalData.Practice
{
    public class PersonRepository
    {
        private SQLiteConnection conn;
        string _dbPath;

        public string StatusMessage { get; set; }
        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }
        private void Init()
        {
            if (conn != null)
                return;
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Person>();
        }
        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();  
                if(String.IsNullOrEmpty(name))
                {
                    throw new Exception("Valid Name Required");
                }
                result = conn.Insert(new Person { Name = name });
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
        }

        public List<Person> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Person>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
