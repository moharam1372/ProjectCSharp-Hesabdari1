namespace Kavosh.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string CodeMelli { get; set; }
        public string Address { get; set; }

        public ICollection<DefinitiveAccount> DefinitiveAccounts { get; set; } = new List<DefinitiveAccount>();
        public ICollection<FactorHeader> FactorHeaders { get; set; } = new List<FactorHeader>();

    }
}       