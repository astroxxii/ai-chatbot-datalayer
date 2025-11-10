using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TugasData.ChatCompletionServices.Model;

namespace TugasData.ChatCompletionServices.Impl
{
    public class ChatCompletionServiceImpl : IChatCompletionServices
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public ChatCompletionServiceImpl(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<string> getChatCompletionAsync(ChatCompletionsRequestService d, CancellationToken ct)
        {
            var apiKey = _config["OpenAI:ApiKey"];
            var apiUrl = _config["OpenAI:ApiUrl"];  // Mengambil URL dari konfigurasi
            var model = _config["OpenAI:Model"];

            var requestBody = new
            {
                model = model,
                messages = d.ChatCompletionRequestService.Select(m => new
                {
                    role = m.role,
                    content = m.content
                }).ToList()
            };

            // Serialisasi JSON
            var json = JsonSerializer.Serialize(requestBody);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Set header authorization
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // Kirim request ke OpenAI menggunakan URL dari konfigurasi
            using var response = await _http.PostAsync($"{apiUrl}", content, ct);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Gagal memanggil OpenAI API: {response.StatusCode}\n{errorBody}");
            }

            // Ambil hasil string dari response
            var result = await response.Content.ReadAsStringAsync(ct);

            // Parse JSON langsung untuk mengambil content
            using (JsonDocument doc = JsonDocument.Parse(result))
            {
                // Ambil value dari content
                var contentResponse = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return contentResponse ?? "No content found"; // Mengembalikan content atau pesan default jika tidak ada
            }
        }
    }
}
