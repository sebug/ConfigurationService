using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Framework.Logging;
using Newtonsoft.Json;

namespace ConfigurationService.Repositories
{
    /// <summary>
    ///   A configuration repository based on files.
    /// </summary>
    public class FileConfigurationRepository : IConfigurationRepository
    {
	private readonly string _baseDirectory;
	private readonly ILogger<FileConfigurationRepository> _logger;
	public FileConfigurationRepository(string baseDirectory,
					   ILogger<FileConfigurationRepository> logger)
	{
	    this._baseDirectory = baseDirectory;
	    this._logger = logger;
	}

	private void EnsureBaseDirectory()
	{
	    if (!Directory.Exists(this._baseDirectory))
	    {
		Directory.CreateDirectory(this._baseDirectory);
	    }
	}
	
	public IEnumerable<string> GetApplicationCodes()
	{
	    this.EnsureBaseDirectory();
	    return Directory.EnumerateFiles(this._baseDirectory, "*.json")
		.Select(Path.GetFileNameWithoutExtension);
	}

	private string GetConfigurationFileContent(string applicationCode)
	{
	    this.EnsureBaseDirectory();
	    string configFileName = Path.Combine(this._baseDirectory,
						 applicationCode +
						 ".json");
	    if (!File.Exists(configFileName))
	    {
		this._logger.Log(LogLevel.Warning, 5, "Config file not found: " + configFileName, null, null);
		return "{}";
	    }
	    return File.ReadAllText(configFileName);
	}

	public IDictionary<string, string> GetByApplicationCode(string applicationCode)
	{
	    string content = this.GetConfigurationFileContent(applicationCode);
	    return JsonConvert.DeserializeObject<IDictionary<string, string>>(content);
	}
    }
}
