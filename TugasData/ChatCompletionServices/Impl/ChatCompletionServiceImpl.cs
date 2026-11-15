using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TugasData.ChatCompletionServices.Model;

namespace TugasData.ChatCompletionServices.Impl
{
    /// <summary>
    /// Implementation of IChatCompletionServices communicating with OpenAI-compatible API.
    /// </summary>
    public class ChatCompletionServiceImpl : IChatCompletionServices
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public ChatCompletionServiceImpl(HttpClient http, IConfiguration config)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Sends messages to OpenAI API and extracts the text response.
        /// </summary>
        public async Task<string> GetChatCompletionAsync(ChatCompletionsRequestService request, CancellationToken ct = default)
        {
            if (request == null || request.ChatCompletionRequestService == null || request.ChatCompletionRequestService.Count == 0)
            {
                throw new ArgumentException("Chat request or messages cannot be empty.", nameof(request));
            }

            var apiKey = _config["OpenAI:ApiKey"];
            var apiUrl = _config["OpenAI:ApiUrl"] ?? "https://api.openai.com/v1/chat/completions";
            var model = _config["OpenAI:Model"] ?? "gpt-4o-mini";

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException("OpenAI API key is missing. Please set 'OpenAI:ApiKey' in configuration.");
            }

            var requestBody = new
            {
                model = model,
                messages = request.ChatCompletionRequestService.Select(m => new
                {
                    role = m.role,
                    content = m.content
                }).ToList()
            };

            var json = JsonSerializer.Serialize(requestBody);
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Set per-request Authorization header for thread safety
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            using var response = await _http.SendAsync(httpRequest, ct);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Gagal memanggil OpenAI API: {response.StatusCode}\n{errorBody}");
            }

            var result = await response.Content.ReadAsStringAsync(ct);

            using var doc = JsonDocument.Parse(result);
            if (doc.RootElement.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
            {
                var firstChoice = choices[0];
                if (firstChoice.TryGetProperty("message", out var message) &&
                    message.TryGetProperty("content", out var contentElement))
                {
                    return contentElement.GetString() ?? "No content found";
                }
            }

            return "No content found";
        }

        /// <summary>
        /// Backward-compatible alias for GetChatCompletionAsync.
        /// </summary>
        public Task<string> getChatCompletionAsync(ChatCompletionsRequestService d, CancellationToken ct)
        {
            return GetChatCompletionAsync(d, ct);
        }
    }
}
