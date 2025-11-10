using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugasData.KnowledgeService.Model
{
    public class ListKnowledgeResponseService
    {
        public string nama { get; set; }
        public string deskripsi { get; set; }
        public string link { get; set; }
        public string mataKuliah { get; set; }
    }

    public class ListFolderKnowledgeResponseService
    {
        public List<ListKnowledgeResponseService> knowledges { get; set; }
    }

    public class GoogleDriveSettings
    {
        public string ApplicationName { get; set; } = string.Empty;
        public string ServiceAccountCredentialPath { get; set; } = string.Empty;
        public string FolderId { get; set; } = string.Empty;
    }
}
