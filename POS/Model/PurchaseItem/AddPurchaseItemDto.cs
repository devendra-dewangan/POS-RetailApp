namespace POS.Model;


public record AddPurchaseItemRequestDto
(
    int ProductId,
    AddBatchRequestDto Batch
);