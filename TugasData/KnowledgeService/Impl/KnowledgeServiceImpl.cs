using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TugasData.KnowledgeService.Model;
using TugasUtils.HttpUtils.Model;

namespace TugasData.KnowledgeService.Impl
{
    public class KnowledgeServiceImpl : IKnowledgeService
    {
        private readonly DriveService _driveService;
        private readonly string _folderId; // ID folder yang akan dibaca

        public KnowledgeServiceImpl(string applicationName, GoogleCredential credential, string folderId)
        {
            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
            _folderId = folderId;
        }

        public async Task<ApiResponse<ListFolderKnowledgeResponseService>> getFileKnowledgeByFolderId(CancellationToken ct = default)
        {
            // Query untuk mencari file-yang berada di folder tertentu (parents = folderId)
            var request = _driveService.Files.List();
            request.Q = $"'{_folderId}' in parents and trashed = false";
            request.Fields = "files(id, name, description, webViewLink, mimeType)"; // ambil field yang diperlukan
            request.PageSize = 100;

            var result = await request.ExecuteAsync(ct);
            var files = result.Files;

            var list = files?.Select(f => new ListKnowledgeResponseService
            {
                nama = f.Name ?? "",
                deskripsi = f.Description ?? "",
                link = f.WebViewLink ?? "",
                mataKuliah = "Sistem Basis Data" 
            }).ToList() ?? new List<ListKnowledgeResponseService>();

            var resultFolder = new ListFolderKnowledgeResponseService
            {
                knowledges = list
            };

            return ApiResponse<ListFolderKnowledgeResponseService>.Ok(resultFolder);
        }
    }
}
