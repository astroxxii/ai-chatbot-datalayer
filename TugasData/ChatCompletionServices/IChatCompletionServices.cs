using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TugasData.ChatCompletionServices.Model;

namespace TugasData.ChatCompletionServices
{
    public interface IChatCompletionServices
    {
        public Task<string> getChatCompletionAsync(ChatCompletionsRequestService d, CancellationToken ct);
    }
}
