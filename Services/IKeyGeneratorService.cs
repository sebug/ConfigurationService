namespace ConfigurationService.Services
{
    public interface IKeyGeneratorService
    {
	string GetKeyForApplicationCode(string applicationCode);
    }
}
