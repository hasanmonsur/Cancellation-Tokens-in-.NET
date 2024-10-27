// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

using var cts = new CancellationTokenSource();
var httpClient = new HttpClient();
Console.WriteLine("Are your want to Cancel task...");
Console.ReadLine();

// Cancel the task after 5 seconds
cts.CancelAfter(TimeSpan.FromSeconds(5));

Console.WriteLine("Starting long-running task...");

try
{
    var response = await httpClient.GetAsync("https://localhost:5001/LongRunningTask", cts.Token);
    if (response.IsSuccessStatusCode)
    {
        string result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
    }
    else
    {
        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("The operation was canceled.");
}
catch (HttpRequestException e)
{
    Console.WriteLine($"Request error: {e.Message}");
}