namespace Oulanka.Domain.Enums
{
    public enum ActionType
    {
        NewTicket = 1,
        NewPost = 2,
        Assignment = 3,
        ChangePriority = 4,
        ResolveTicket = 5,
        CloseByOperator = 6,
        UpdateTicket = 7,
        ChangeStatus = 8,
        CloseByUser = 9,
        Diagnostic = 10,
        Solution = 11,
        Note = 12,
        FirstAttention = 13,
        SendCopyTo = 14,
        Close = 99,
    }
}