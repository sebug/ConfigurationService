using System;
using System.IO;
using Microsoft.Framework.Logging;

namespace ConfigurationService.Services
{
    public class FileAuthenticationService : IAuthenticationService
    {
	private readonly string _baseDirectory;
	private string KeysDirectory
	{
	    get { return Path.Combine(this._baseDirectory, "keys"); }
	}
	private readonly ILogger<FileAuthenticationService> _logger;
	
	public FileAuthenticationService(string baseDirectory,
					 ILogger<FileAuthenticationService> logger)
	{
	    this._baseDirectory = baseDirectory;
	    this._logger = logger;
	}

	private void EnsureDirectory(string d)
	{
	    if (!Directory.Exists(d))
	    {
		Directory.CreateDirectory(d);
	    }
	}

	private void EnsureBaseDirectory()
	{
	    this.EnsureDirectory(this._baseDirectory);
	}

	private void EnsureKeysDirectory()
	{
	    this.EnsureBaseDirectory();
	    this.EnsureDirectory(this.KeysDirectory);
	}

	private void EnsureApplicationKey(string applicationCode)
	{
	    this.EnsureKeysDirectory();
	    string applicationKeyFile =
		Path.Combine(this.KeysDirectory, applicationCode + ".json");
	    if (!File.Exists(applicationKeyFile))
	    {
		this._logger.Log(LogLevel.Information, 3, "Key file does not exist for " + applicationCode + ", creating", null, null);
	    }
	}
	
	public bool IsAuthenticated(string applicationCode, string key)
	{
	    if (applicationCode == null)
	    {
		throw new ArgumentNullException("applicationCode");
	    }
	    this.EnsureApplicationKey(applicationCode);
	    return false;
	}
    }
}