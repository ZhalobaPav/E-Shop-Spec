namespace Infrastructure.Logging
{
    public interface ICustomLogger<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}