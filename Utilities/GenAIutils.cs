using Google.GenAI;

namespace DeviceManagement.Utilities;

public class GenAIutils
{
    public static async Task<string> GenDescription(string inputInfo)
    {
        var client = new Client();
        var response = await client.Models.GenerateContentAsync(
          model: "gemma-3-27b-it",
          contents: "Generate an informative and concise description of a device, to help employees choose one to use. The information about the device is as follows:" +
            inputInfo + "Return the description as a single paragraph, formatted in plaintext."
        );
        var stringResponse = response?.Candidates?[0].Content?.Parts?[0].Text;
        return stringResponse ?? "";
    }

}