using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TugasData.KnowledgeService.Model
{
    /// <summary>
    /// Represents a knowledge item retrieved from Google Drive (e.g., lecture material, document).
    /// </summary>
    public class ListKnowledgeResponseService
    {
        [JsonPropertyName("nama")]
        public string nama { get; set; } = string.Empty;

        [JsonPropertyName("deskripsi")]
        public string deskripsi { get; set; } = string.Empty;

        [JsonPropertyName("link")]
        public string link { get; set; } = string.Empty;

        [JsonPropertyName("mataKuliah")]
        public string mataKuliah { get; set; } = string.Empty;

        // Convenience aliases following C# conventions
        [JsonIgnore]
        public string Name
        {
            get => nama;
            set => nama = value;
        }

        [JsonIgnore]
        public string Description
        {
            get => deskripsi;
            set => deskripsi = value;
        }

        [JsonIgnore]
        public string Url
        {
            get => link;
            set => link = value;
        }

        [JsonIgnore]
        public string Course
        {
            get => mataKuliah;
            set => mataKuliah = value;
        }
    }

    /// <summary>
    /// Container for list of knowledge items.
    /// </summary>
    public class ListFolderKnowledgeResponseService
    {
        [JsonPropertyName("knowledges")]
        public List<ListKnowledgeResponseService> knowledges { get; set; } = new();

        [JsonIgnore]
        public List<ListKnowledgeResponseService> Knowledges
        {
            get => knowledges;
            set => knowledges = value;
        }
    }

    /// <summary>
    /// Configuration options for Google Drive integration.
    /// </summary>
    public class GoogleDriveSettings
    {
        /// <summary>
        /// Application name sent in Google API requests.
        /// </summary>
        public string ApplicationName { get; set; } = "TugasApp";

        /// <summary>
        /// File path to the Google Service Account JSON credentials.
        /// </summary>
        public string ServiceAccountCredentialPath { get; set; } = string.Empty;

        /// <summary>
        /// Target Google Drive folder ID to read knowledge files from.
        /// </summary>
        public string FolderId { get; set; } = string.Empty;

        /// <summary>
        /// Default course name / category tagged to retrieved files.
        /// </summary>
        public string DefaultCourseName { get; set; } = "Sistem Basis Data";
    }
}
