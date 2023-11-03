namespace Login.Models.User
{
    public class FileSheet : BaseEntity
    {
        public FileSheet()
        {
        }

        public Guid FileSheetId { get; set; }
        public string FileId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public List<string> SheetNames { get; set; }
    }
}