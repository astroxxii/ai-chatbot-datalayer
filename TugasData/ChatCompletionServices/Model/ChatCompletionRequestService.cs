using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TugasData.ChatCompletionServices.Model
{
    /// <summary>
    /// Wrapper for chat completion request containing messages history.
    /// </summary>
    public class ChatCompletionsRequestService
    {
        [JsonPropertyName("messages")]
        public List<ChatCompletionRequestService> ChatCompletionRequestService { get; set; } = new();

        /// <summary>
        /// Convenience alias following C# conventions.
        /// </summary>
        [JsonIgnore]
        public List<ChatCompletionRequestService> Messages
        {
            get => ChatCompletionRequestService;
            set => ChatCompletionRequestService = value;
        }

        public ChatCompletionsRequestService() { }

        public ChatCompletionsRequestService(List<ChatCompletionRequestService> messages)
        {
            ChatCompletionRequestService = messages;
        }
    }

    /// <summary>
    /// Individual chat message item (role and content).
    /// </summary>
    public class ChatCompletionRequestService
    {
        [JsonPropertyName("role")]
        public string role { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string content { get; set; } = string.Empty;

        /// <summary>
        /// Convenience alias following C# conventions.
        /// </summary>
        [JsonIgnore]
        public string Role
        {
            get => role;
            set => role = value;
        }

        /// <summary>
        /// Convenience alias following C# conventions.
        /// </summary>
        [JsonIgnore]
        public string Content
        {
            get => content;
            set => content = value;
        }

        public ChatCompletionRequestService() { }

        public ChatCompletionRequestService(string role, string content)
        {
            this.role = role;
            this.content = content;
        }
    }
}
