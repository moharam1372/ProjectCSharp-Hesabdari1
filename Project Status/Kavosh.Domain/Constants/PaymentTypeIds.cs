namespace Kavosh.Domain.Constants
{
    public static class PaymentTypeIds
    {
        public static readonly Guid Cash = new("768b622b-1a81-4e97-a8ef-292ad56f8773");
        public static readonly Guid Check = new("db73144b-1c73-4b19-814f-4055bb3feace");
        public static readonly Guid CardToCard = new("bd6503dd-98eb-4183-b4c9-b7ad55e13b2c");
        public static readonly Guid Debtor = new("bc3c7e7d-170a-4a5d-a4b9-770231963030");
    }
}   