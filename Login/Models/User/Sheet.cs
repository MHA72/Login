namespace Login.Models.User
{
    public class Sheet : BaseEntity
    {
        public Sheet()
        {
        }

        public string SheetId { get; set; }
        public string SheetName { get; set; }
    }
}