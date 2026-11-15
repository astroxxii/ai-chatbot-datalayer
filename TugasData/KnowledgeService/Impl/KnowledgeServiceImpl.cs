using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TugasData.KnowledgeService.Model;
using TugasUtils.HttpUtils.Model;

namespace TugasData.KnowledgeService.Impl
{
    /// <summary>
    /// Implementation of IKnowledgeService that queries files from Google Drive.
    /// </summary>
    public class KnowledgeServiceImpl : IKnowledgeService
    {
        private readonly DriveService _driveService;
        private readonly string _folderId;
        private readonly string _courseName;

        public KnowledgeServiceImpl(string applicationName, GoogleCredential credential, string folderId, string courseName = "Sistem Basis Data")
        {
            if (string.IsNullOrWhiteSpace(applicationName)) throw new ArgumentNullException(nameof(applicationName));
            if (credential == null) throw new ArgumentNullException(nameof(credential));
            if (string.IsNullOrWhiteSpace(folderId)) throw new ArgumentNullException(nameof(folderId));

            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
            _folderId = folderId;
            _courseName = string.IsNullOrWhiteSpace(courseName) ? "Sistem Basis Data" : courseName;
        }

        /// <summary>
        /// Queries files inside the configured Google Drive folder and maps them into knowledge items.
        /// </summary>
        public async Task<ApiResponse<ListFolderKnowledgeResponseService>> GetFileKnowledgeByFolderIdAsync(CancellationToken ct = default)
        {
            try
            {
                var request = _driveService.Files.List();
                request.Q = $"'{_folderId}' in parents and trashed = false";
                request.Fields = "files(id, name, description, webViewLink, mimeType)";
                request.PageSize = 100;

                var result = await request.ExecuteAsync(ct);
                var files = result.Files;

                var list = files?.Select(f => new ListKnowledgeResponseService
                {
                    nama = f.Name ?? string.Empty,
                    deskripsi = f.Description ?? string.Empty,
                    link = f.WebViewLink ?? string.Empty,
                    mataKuliah = _courseName
                }).ToList() ?? new List<ListKnowledgeResponseService>();

                var resultFolder = new ListFolderKnowledgeResponseService
                {
                    knowledges = list
                };

                return ApiResponse<ListFolderKnowledgeResponseService>.Ok(resultFolder);
            }
            catch (Exception ex)
            {
                return ApiResponse<ListFolderKnowledgeResponseService>.Fail(
                    errorDetail: ex.Message,
                    message: $"Gagal mengambil data dari Google Drive: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Backward-compatible alias for GetFileKnowledgeByFolderIdAsync.
        /// </summary>
        public Task<ApiResponse<ListFolderKnowledgeResponseService>> getFileKnowledgeByFolderId(CancellationToken ct = default)
        {
            return GetFileKnowledgeByFolderIdAsync(ct);
        }
    }
}
