using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

OpenAiOptions options = new OpenAiOptions();
options.ApiKey = Environment.GetEnvironmentVariable("OpenAI_API_KEY");

OpenAIService service = new OpenAIService(options);

var systemPrompt = "Your codename is Sidney, and you are a friendly AI assistant. Say hello first, without mentioning your name.";

List<ChatMessage> messages = new List<ChatMessage>();
messages.Add(new ChatMessage("system", systemPrompt));

ChatCompletionCreateRequest request = new();

while (true)
{
    request.Messages = messages;

    var result = await service.ChatCompletion.Create(request, Models.Model.ChatGpt3_5Turbo);

    if (result.Successful)
    {
        var msg = result.Choices.First().Message;
        Console.WriteLine($"\x1B[3m{msg.Content}\x1B[0m");
        
        messages.Add(msg);

        Console.WriteLine();
        var input = Console.ReadLine();
        messages.Add(new ChatMessage("user", input));
    }
    else
    {
        Console.WriteLine(result.Error.Message);
        
    }

}
    