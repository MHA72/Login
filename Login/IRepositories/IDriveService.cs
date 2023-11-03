using File = Google.Apis.Drive.v3.Data.File;

namespace SpreadSheet.Services;

public interface IDriveService

{
    Task<File> CreateFolder(string email, string fileName, CancellationToken cancellationToken);
    Task CreateSheet(Guid userId, string email, string name, CancellationToken cancellationToken);
}