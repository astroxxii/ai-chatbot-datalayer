using System;
using System.Threading;
using System.Threading.Tasks;
using TugasData.ChatCompletionServices.Model;

namespace TugasData.ChatCompletionServices
{
    /// <summary>
    /// Service contract for interacting with OpenAI or OpenAI-compatible Chat Completion APIs.
    /// </summary>
    public interface IChatCompletionServices
    {
        /// <summary>
        /// Sends a chat completion request to the configured LLM API asynchronously.
        /// </summary>
        /// <param name="request">Request containing conversation messages.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The generated message content response from the model.</returns>
        Task<string> GetChatCompletionAsync(ChatCompletionsRequestService request, CancellationToken ct = default);

        /// <summary>
        /// Backward-compatible alias for GetChatCompletionAsync.
        /// </summary>
        Task<string> getChatCompletionAsync(ChatCompletionsRequestService d, CancellationToken ct);
    }
}
