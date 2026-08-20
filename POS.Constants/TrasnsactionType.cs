namespace POS.Domain
{
    public enum TransactionType
    {
        OpeningStock,
        Purchase,
        Sale,
        PurchaseReturn,
        SalesReturn,
        Adjustment,
        Damage,
        Expired
    }
}
