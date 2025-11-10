using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TugasData.KnowledgeService.Model;
using TugasUtils.HttpUtils.Model;

namespace TugasData.KnowledgeService
{
    public interface IKnowledgeService
    {
        public Task<ApiResponse<ListFolderKnowledgeResponseService>> getFileKnowledgeByFolderId(CancellationToken ct);
    }
}
