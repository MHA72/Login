namespace Login.Models.User
{
    public class FileSheet : BaseEntity
    {
        public FileSheet()
        {
        }

        public string FileId { get; set; }
        public User? User { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Sheet>? SheetNames { get; set; }
    }
}