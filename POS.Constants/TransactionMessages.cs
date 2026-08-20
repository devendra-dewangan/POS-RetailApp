namespace POS.Constants
{
    public static class TransactionMessages
    {
        public static string CartNotFound(TransactionType type)
            => $"{type} Cart Not Found.";

        public static string ItemNotFound(TransactionType type)
            => $"{type} Item Not Found.";

        public static string CartContainsNoItems(TransactionType type)
            => $"{type} Cart Contains No Items.";

        public static string InvalidCart(TransactionType type)
            => $"Invalid {type} Cart.";

        public static string BatchNotFound(TransactionType type, int productId, int batchId)
            => $"{type} Batch Not Found. ProductId: {productId}, BatchId: {batchId}.";

        public static string BatchQuantityInsufficient(TransactionType type, int productId, int batchId)
            => $"{type} Batch Quantity Insufficient. ProductId: {productId}, BatchId: {batchId}.";

        public static string SupplierNotFound(int supplierId)
            => $"Supplier Not Found. SupplierId: {supplierId}.";

        public static string ProoductNotFound(TransactionType purchase, int productId)
            => $"{purchase} Product Not Found. ProductId: {productId}.";

        public static string BatchInformationMissing(int productId)
            => $"Batch information is missing for product: {productId}.";
    }
}
