using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Login.Context;
using Login.IRepositories;
using Login.Models.User;
using Microsoft.EntityFrameworkCore;
using SpreadSheet.Services;

namespace Login.Repositories;

public class SheetService : ISheetService
{
    private readonly LoginContext _authContext;
    private readonly IUserRepository _userRepository;
    private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private static readonly string ApplicationName = "DriveMHA";
    private const string Sheet = "MHA";
    private static SheetsService? _sheets;


    public SheetService(LoginContext authContext, IUserRepository userRepository)
    {
        _authContext = authContext;
        _userRepository = userRepository;
    }

    private static async Task Authorize()
    {
        GoogleCredential credential;
        await using (var stream = new FileStream("sheet.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
        }

        _sheets = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName
        });
    }

    public async Task<IList<IList<object>>> ReadEntries(CancellationToken cancellationToken = default)
    {
        await Authorize();

        var spreadsheet = await _authContext.FileSheets.FirstAsync(fileSheet => fileSheet.UserId == _userRepository.GetAuthenticatedUser().Id,
            cancellationToken);

        var range = $"{Sheet}";
        var request = _sheets!.Spreadsheets.Values.Get(spreadsheet.FileId, range);

        var response = await request.ExecuteAsync(cancellationToken);
        var values = response.Values;

        return values;
    }

    public async Task<IList<IList<object>>> CreateRow(string spreadsheetId, string startRange, string endRange, List<string> request,
        CancellationToken cancellationToken = default)
    {
        await Authorize();

        var range = $"MHA!{startRange}:{endRange}";
        var valueRange = new ValueRange();
        var objectList = new List<object>();

        foreach (var t in request)
        {
            objectList.Add(t);
        }

        valueRange.Values = new List<IList<object>> { objectList };

        var appendRequest = _sheets!.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
        appendRequest.ValueInputOption =
            SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
        await appendRequest.ExecuteAsync(cancellationToken);

        var newRequest = _sheets.Spreadsheets.Values.Get(spreadsheetId, range);

        var response = await newRequest.ExecuteAsync(cancellationToken);
        var values = response.Values;

        return values;
    }

    public async Task<IList<IList<object>>> UpdateCell(string placeCell, string valueCell,
        CancellationToken cancellationToken = default)
    {
        await Authorize();

        var spreadsheet = await _authContext.FileSheets.FirstAsync(fileSheet => fileSheet.UserId == _userRepository.GetAuthenticatedUser().Id,
            cancellationToken);

        var range = $"{Sheet}!{placeCell}";
        var valueRange = new ValueRange();
        var objectList = new List<object> { valueCell };

        valueRange.Values = new List<IList<object>> { objectList };

        var updateRequest = _sheets!.Spreadsheets.Values.Update(valueRange, spreadsheet.FileId, range);
        updateRequest.ValueInputOption =
            SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        await updateRequest.ExecuteAsync(cancellationToken);

        var newRange = $"{Sheet}";
        var request = _sheets.Spreadsheets.Values.Get(spreadsheet.FileId, newRange);

        var response = await request.ExecuteAsync(cancellationToken);
        var values = response.Values;

        return values;
    }

    public async Task DeleteRow(string startColumn, string endColumn, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var spreadsheet = await _authContext.FileSheets.FirstAsync(fileSheet => fileSheet.UserId == _userRepository.GetAuthenticatedUser().Id,
                cancellationToken);

        var range = $"{Sheet}!{startColumn}:{endColumn}";
        var requestBody = new ClearValuesRequest();

        var deleteRequest = _sheets!.Spreadsheets.Values.Clear(requestBody, spreadsheet.FileId, range);

        await deleteRequest.ExecuteAsync(cancellationToken);
    }

    public async Task AddSheet(string sheetName, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var spreadsheet = await FindFileSheetByUserId(_userRepository.GetAuthenticatedUser().Id, cancellationToken);
        // Add new Sheet
        var addSheetRequest = new AddSheetRequest
        {
            Properties = new SheetProperties
            {
                Title = sheetName,
            }
        };


        var batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest
        {
            Requests = new List<Request>()
        };
        batchUpdateSpreadsheetRequest.Requests.Add(new Request
        {
            AddSheet = addSheetRequest
        });

        // Create request
        var batchUpdateRequest =
            _sheets!.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheet.FileId);

        // Execute request
        await batchUpdateRequest.ExecuteAsync(cancellationToken);
    }

    public async Task UpdateNameSheet(string spreadsheetId, string sheetName, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var addSheetRequest2 = new UpdateSheetPropertiesRequest
        {
            Properties = new SheetProperties
            {
                Title = sheetName,
            },
            Fields = "Title"
        };
        var batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest
        {
            Requests = new List<Request>()
        };
        batchUpdateSpreadsheetRequest.Requests.Add(new Request
        {
            UpdateSheetProperties = addSheetRequest2
        });

        // Create request
        var batchUpdateRequest =
            _sheets!.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheetId);

        // Execute request
        await batchUpdateRequest.ExecuteAsync(cancellationToken);
    }

    public async Task CreateSheet(CancellationToken cancellationToken = default)
    {
        GoogleCredential credential;
        await using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
        {
            credential = (await GoogleCredential.FromStreamAsync(stream, cancellationToken)).CreateScoped(Scopes);
        }

        var sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Google-SheetsSample/0.1",
        });

        var requestBody = new Spreadsheet();

        var request = sheetsService.Spreadsheets.Create(requestBody);

        await request.ExecuteAsync(cancellationToken);
    }
    
    public Task<FileSheet> FindFileSheetByUserId(Guid id, CancellationToken cancellationToken)
    {
        return _authContext.FileSheets.Where(fileSheet => fileSheet.UserId == id).FirstAsync(cancellationToken);
    }
}