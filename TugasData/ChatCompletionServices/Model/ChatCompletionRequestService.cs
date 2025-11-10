using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugasData.ChatCompletionServices.Model
{
    public class ChatCompletionsRequestService
    {
        public List<ChatCompletionRequestService> ChatCompletionRequestService { get; set; }
    }

    public class ChatCompletionRequestService
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class ChatCompletionResponseService
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Content { get; set; }
    }

    public class OpenAIResponse
    {
        public Choice[] Choices { get; set; }
    }
}
