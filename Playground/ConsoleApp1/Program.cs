using System;
using OpenAI;
using OpenAI.Responses;
using System.ClientModel;
using System.Text;
using System.Text.Json;

#pragma warning disable OPENAI001

const string deploymentName = "gpt-5.4-mini";

const string endpoint =
    "https://helloworldfoundaryresource.services.ai.azure.com/openai/v1";

const string apiKey =
    "test";

ResponsesClient client = new(
    credential: new ApiKeyCredential(apiKey),
    options: new ResponsesClientOptions
    {
        Endpoint = new Uri(endpoint)
    });
Console.WriteLine("Provide you input to search for information");
string searchParameter=Console.ReadLine();

CreateResponseOptions options = new()
{
    Model = deploymentName,
    InputItems =
    {
        ResponseItem.CreateUserMessageItem(searchParameter)
    }
};

ResponseResult response = client.CreateResponse(options);

Console.WriteLine($"[ASSISTANT]: {response.GetOutputText()}");



// Azure Translator configuration
const string endpointTranslation = "https://api.cognitive.microsofttranslator.com/";
const string apiVersion = "2025-10-01-preview";

const string subscriptionKey = "test";
const string region = "westeurope";

var translationRequest = new
{
    inputs = new[]
    {
        new
        {
            text = "Doctor is available next Monday. Do you want to schedule an appointment?",
            language = "en",
            targets = new[]
            {
                new
                {
                    language = "es"
                }
            }
        }
    }
};

var url =
    $"{endpointTranslation}/translate?api-version={apiVersion}";

using var clientTranslation = new HttpClient();

clientTranslation.DefaultRequestHeaders.Add(
    "Ocp-Apim-Subscription-Key",
    subscriptionKey);

clientTranslation.DefaultRequestHeaders.Add(
    "Ocp-Apim-Subscription-Region",
    region);

var jsonRequest = JsonSerializer.Serialize(translationRequest);

using var content = new StringContent(
    jsonRequest,
    Encoding.UTF8,
    "application/json");

Console.WriteLine("Request:");
Console.WriteLine(jsonRequest);

var responseTranslation =
    await clientTranslation.PostAsync(url, content);

var responseJson =
    await responseTranslation.Content.ReadAsStringAsync();

Console.WriteLine("\nStatus:");
Console.WriteLine(responseTranslation.StatusCode);

Console.WriteLine("\nResponse:");
Console.WriteLine(responseJson);