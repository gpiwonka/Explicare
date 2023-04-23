using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicare.OpenAIClient
{
    public class OpenAIModel
    {
        public string model => "text-davinci-003";
        public string prompt { get; set; }
        public int temperature => 0;
        public int max_tokens => 256;
        public double top_p => 1.0;
        public double frequency_penalty => 0.0;
        public double presence_penalty => 0.0;
        public List<string> stop => new List<string> { "\"\"\"" };

    }

    public class Choice
    {
        public string text { get; set; }
        public int index { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }

    public class OpenAIResponse
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public List<Choice> choices { get; set; }
        public Usage usage { get; set; }
    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
}
