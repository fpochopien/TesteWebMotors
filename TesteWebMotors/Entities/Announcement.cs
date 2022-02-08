namespace TesteWebMotors.Entities
{
    public class Announcement
    {
        public Announcement()
        {
        }
        public int Id { get; set; }
        public int Ano { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Observacao { get; set; }
        public int Quilometragem { get; set; }
        public string Versao { get; set; }
    }
}
