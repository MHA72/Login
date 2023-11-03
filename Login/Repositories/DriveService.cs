using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Login.Context;
using Login.IRepositories;
using Login.Models.User;
using File = Google.Apis.Drive.v3.Data.File;

namespace Login.Repositories;

public class DriveService : IDriveService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly LoginContext _authContext;
    private readonly ISheetService _sheetService;
    private readonly IConfiguration _configuration;

    public DriveService(
        IServiceScopeFactory serviceScopeFactory,
        ISheetService sheetService,
        IConfiguration configuration, LoginContext authContext)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _sheetService = sheetService;
        _configuration = configuration;
        _authContext = authContext;
    }


    private static Google.Apis.Drive.v3.DriveService GetService()
    {
        var tokenResponse = new TokenResponse
        {
            AccessToken =
                "ya29.a0AfB_byAm4tS8IjGtpfgwSNQLo45vYbD3PE-WP3Hauxt1VkEycgbin10QA6yz5xiLjcVp9yytX5F84ICP-E3z925UXaXhIcNk4WqbS4B1WpHqmHRBgGd2LTh5KpqevU4yNdSnTBky3zOLZePBuLA1vhllag3UoX-QjnDuaCgYKAdgSAQ8SFQGOcNnC8FacrW1Lc3cNOYXBCJbGHA0171",
            RefreshToken =
                "1//04PSkYbGjB_e9CgYIARAAGAQSNwF-L9Ir5yOIhixkFzdyPR5Whx7FfFnofFYOiaZYxNj2diwgjswjc2LXFrSAhQG3yNgZUfQDZ2Y"
        };

        var applicationName = "DriveMHA";
        var username = "arabbagher4@gmail.com";

        var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = "465173885320-g5tbh1iifcb4m3fufcb9od7a7eg6ob9p.apps.googleusercontent.com",
                ClientSecret = "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",
            },
            Scopes = new[] { Google.Apis.Drive.v3.DriveService.Scope.Drive },
            DataStore = new FileDataStore(applicationName)
        });

        var credential = new UserCredential(apiCodeFlow, username, tokenResponse);

        var service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            }
        );
        return service;
    }

    public async Task<File> CreateFolder(string email, string fileName, CancellationToken cancellationToken)
    {
        var service = GetService();

        var fileMetaData = new
            File
            {
                Name = fileName,
                MimeType = "application/vnd.google-apps.folder"
            };

        var request = service.Files.Create(fileMetaData);
        request.Fields = "id";
        var file = await request.ExecuteAsync(cancellationToken);

        var userPermission = new Permission
        {
            Type = "user",
            Role = "writer",
            EmailAddress = email
        };
        await service.Permissions.Create(userPermission, file.Id).ExecuteAsync(cancellationToken);
        return file;
    }

    public async Task CreateSheet(Guid userId, string email, string userName, CancellationToken cancellationToken)
    {
        var service = GetService();
        var _parent = "12unHAQHkNnnxE60dI5qnyndGcW4osgEV"; //ID of folder if you want to create spreadsheet in specific folder
        var filename = userName;
        var fileMetadata = new File
        {
            Name = filename,
            MimeType = "application/vnd.google-apps.spreadsheet",
            //TeamDriveId = teamDriveID, //  IF you want to add to specific team drive
        };

        var request = service.Files.Create(fileMetadata);
        request.SupportsTeamDrives = true;
        fileMetadata.Parents = new List<string> { _parent }; // Parent folder id or TeamDriveID
        request.Fields = "id";
        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        var file = await request.ExecuteAsync(cancellationToken);

        var userPermission = new Permission
        {
            Type = "user",
            Role = "writer",
            EmailAddress = email
        };
        await service.Permissions.Create(userPermission, file.Id).ExecuteAsync(cancellationToken);

        var userPermissionAdmin = new Permission
        {
            Type = "user",
            Role = "writer",
            EmailAddress = "tmtm-765@tmtm-387307.iam.gserviceaccount.com"
        };
        await service.Permissions.Create(userPermissionAdmin, file.Id).ExecuteAsync(cancellationToken);

        await _sheetService.UpdateNameSheet(file.Id, "ECPA", cancellationToken);
        await _sheetService.CreateRow(file.Id, "A", "L", new List<string>()
        {
            "Project",
            "Total",
            "Category",
            "Tax",
            "Type",
            "Supplier",
            "Document Date",
            "Tag",
            "Currency",
            "Additional Note",
            "Title",
            "File"
        }, cancellationToken);

        var sheetService = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LoginContext>();
        var sheet = new FileSheet
        {
            UserId = userId,
            FileId = file.Id,
            SheetNames = new List<string>
            {
                "MHA"
            }
        };

        await sheetService.FileSheets.AddAsync(sheet, cancellationToken);
        await sheetService.SaveChangesAsync(cancellationToken);
    }
}