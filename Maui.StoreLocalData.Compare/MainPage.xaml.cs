using System.Text.Json;

namespace Maui.StoreLocalData.Compare;

public partial class MainPage : ContentPage
{
    bool _wasClicked = false;
    List<Customer> customers = null;
    string _fileName = Path.Combine(FileSystem.AppDataDirectory,"CustomerFile.dat");
    string _docFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		if (Preferences.ContainsKey("WasClicked"))
		{
            var wasClicked = Preferences.Get("WasClicked", false);
			if (wasClicked)
			{
                await DisplayAlert("Aviso", "Ya había sido clickeado", "Ok");
                Preferences.Remove("WasClicked");
                await DisplayAlert("Aviso", "Se borró el clickeo", "Ok");

                var rawData = File.ReadAllText(_fileName);
                customers = JsonSerializer.Deserialize<List<Customer>>(rawData);
                await DisplayAlert("Aviso", $"Ya hay {customers.Count()} Customers guardados.", "Ok");
                File.Delete(_fileName);
                await DisplayAlert("Aviso", $"Se eliminó archivo {_fileName}.", "Ok");
            }
        }
		else
		{
			_wasClicked = true;
            Preferences.Set("WasClicked", _wasClicked);
            await DisplayAlert("Aviso", "Se guardó preferencia", "Ok");

            customers = new List<Customer>() {
                new Customer() {
                    Id = 1,
                    Name = "Sergio",
                    LastName = "Pérez"
                },
                new Customer() {
                    Id = 2,
                    Name = "Alejandro",
                    LastName = "Pérez"
                }
            };
            var serializedData = JsonSerializer.Serialize(customers);
            File.WriteAllText(_fileName, serializedData);
            await DisplayAlert("Aviso", $"Se guardaron {customers.Count()} Customers en {_fileName}.", "Ok");
        }
            
	}
    private class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}

