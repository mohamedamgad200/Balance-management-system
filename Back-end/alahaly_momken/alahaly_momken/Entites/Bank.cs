namespace alahaly_momken.Entites
{
    public class Bank
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public float balance { get; set; } = 0;
        public String Role { get; set; } = "Bank";
        public List<Depoist> user_depoists { get; set; } = new List<Depoist>();
        public List<Depit> bank_depits { get; set; } = new List<Depit>();
        public List<Correction> corrections { get; set; } = new List<Correction>();

    }
}
