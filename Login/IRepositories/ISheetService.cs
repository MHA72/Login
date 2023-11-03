using Login.Models.User;

namespace Login.IRepositories;

public interface ISheetService

{
    Task<IList<IList<object>>> ReadEntries(CancellationToken cancellationToken);

    Task<IList<IList<object>>> CreateRow(string spreadsheetId, string startRange, string endRange, List<string> request,
        CancellationToken cancellationToken);

    Task<IList<IList<object>>> UpdateCell(string placeCell, string valueCell, CancellationToken cancellationToken);
    Task DeleteRow(string startColumn, string endColumn, CancellationToken cancellationToken);
    Task AddSheet(string sheetName, CancellationToken cancellationToken);
    Task CreateSheet(CancellationToken cancellationToken);
    Task UpdateNameSheet(string spreadsheetId, string sheetName, CancellationToken cancellationToken = default);
    Task<FileSheet> FindFileSheetByUserId(Guid id, CancellationToken cancellationToken);
}