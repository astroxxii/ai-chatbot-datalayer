using System.Threading;
using System.Threading.Tasks;
using TugasData.KnowledgeService.Model;
using TugasUtils.HttpUtils.Model;

namespace TugasData.KnowledgeService
{
    /// <summary>
    /// Service contract for retrieving knowledge documents from Google Drive.
    /// </summary>
    public interface IKnowledgeService
    {
        /// <summary>
        /// Retrieves list of knowledge documents from the configured Google Drive folder.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>ApiResponse containing the list of knowledge items.</returns>
        Task<ApiResponse<ListFolderKnowledgeResponseService>> GetFileKnowledgeByFolderIdAsync(CancellationToken ct = default);

        /// <summary>
        /// Backward-compatible alias for GetFileKnowledgeByFolderIdAsync.
        /// </summary>
        Task<ApiResponse<ListFolderKnowledgeResponseService>> getFileKnowledgeByFolderId(CancellationToken ct = default);
    }
}
