namespace alahaly_momken.Entites
{
    public class Depoist
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "pending";
        public DateTime Date { get; set; }
        public string BankAccountNumber { get; set; }
        public string ImagePath { get; set; } // New property for image path
        public String BankName { get; set; }
        public String Transicationtype { get; set; } = "cashin";
        public String Type { get; set; } = "credit";
        public int Companyid { get; set; }
        public int Bankid { get; set; }
        public float blanaceafter { get; set; } = 0;

        public float balancebefore { get; set; } = 0; 
        public string Note { get; set; } = "null";
    }
}
