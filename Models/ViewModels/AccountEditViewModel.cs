namespace AccountKeep.Models.ViewModels
{
    // ViewModel for editing account details
    public class AccountEditViewModel
    {
        public int AccountId { get; set; }// Needed to identify which account to edit
        public int PersonId { get; set; }// Links account to owner

        public string AccountNumber { get; set; } = string.Empty;// Editable account number
        public string AccountType { get; set; } = string.Empty;// Editable type
        public string Status { get; set; } = "Open";// Editable status; default "Open

        public decimal Balance { get; set; }// Display only, not meant to be edited
    }
}
