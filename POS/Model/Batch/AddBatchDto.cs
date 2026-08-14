namespace POS.Model;

public record AddBatchRequestDto
(
    string BatchNumber,
    decimal Quantity,
    decimal UnitPrice,
    decimal SalePrice,
    decimal MRP
);

public record AddBatchResponseDto
(
    int PurchaseDraftId
);