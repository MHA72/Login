using File = Google.Apis.Drive.v3.Data.File;

namespace Login.IRepositories;

public interface IDriveService

{
    Task<File> CreateFolder(string email, string fileName, CancellationToken cancellationToken);
    Task CreateSheet(Guid userId, string email, string userName, CancellationToken cancellationToken);
}