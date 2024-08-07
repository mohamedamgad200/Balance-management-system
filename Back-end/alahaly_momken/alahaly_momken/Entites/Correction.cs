namespace alahaly_momken.Entites
{
    public class Correction
    {
        public  string type { get; set; }
        public int amount { get; set; }
        public int Bankid { get; set; }
        public float blanaceafter { get; set; } = 0;
        public DateTime Date { get; set; }
        public float balancebefore { get; set; } = 0;
        public int id { get; set; }
    }
}
