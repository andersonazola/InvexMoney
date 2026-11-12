namespace Invex.Services.Interfaces;
public interface IAIService
{
    Task<string> GetAiResponse (string prompt);
}