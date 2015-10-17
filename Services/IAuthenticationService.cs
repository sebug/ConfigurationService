namespace ConfigurationService.Services
{
    public interface IAuthenticationService
    {
	bool IsAuthenticated(string applicationName, string key);
    }
}
