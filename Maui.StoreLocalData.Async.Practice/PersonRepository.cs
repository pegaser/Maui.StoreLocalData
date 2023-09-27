using Maui.StoreLocalData.Async.Practice.Models;
using SQLite;

namespace Maui.StoreLocalData.Async.Practice
{
    public class PersonRepository
    {
        private SQLiteAsyncConnection conn;
        string _dbPath;

        public string StatusMessage { get; set; }
        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }
        private async Task Init()
        {
            if (conn != null)
                return;
            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<Person>();
        }
        public async Task AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                await Init();
                if (String.IsNullOrEmpty(name))
                {
                    throw new Exception("Valid Name Required");
                }
                result = await conn.InsertAsync(new Person { Name = name });
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
        }

        public async Task<List<Person>> GetAllPeople()
        {
            try
            {
                await Init();
                return await conn.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
