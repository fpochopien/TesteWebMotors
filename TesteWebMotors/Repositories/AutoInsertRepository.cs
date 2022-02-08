using Newtonsoft.Json;

using TesteWebMotors.Entities;
using TesteWebMotors.Entities.APIWebMotors;

namespace TesteWebMotors.Repositories
{
    public class AutoInsertRepository
    {
        public async void GetVehicles()
        {
            var listVehicles = new List<Vehicle>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://desafioonline.webmotors.com.br/api/OnlineChallenge/");
                var requestTask = client.GetAsync("Vehicles?Page=1");
                var response = await requestTask;
                if (response.IsSuccessStatusCode)
                {
                    listVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(await response.Content.ReadAsStringAsync());
                }
            }
            InsertVehicles(listVehicles);
        }

        private async void InsertVehicles(List<Vehicle> vehicles)
        {
            foreach(var vehicle in vehicles)
            {
                var announcement = new Announcement()
                {
                    Ano = vehicle.YearModel,
                    Marca = vehicle.Make,
                    Modelo = vehicle.Model,
                    Observacao = $"Cor {vehicle.Color}",
                    Quilometragem = vehicle.KM,
                    Versao = vehicle.Version
                };
                var announcementRet = new AnnouncementRepository().Create(announcement);
            }
        }
    }
}
