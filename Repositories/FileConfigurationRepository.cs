using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ConfigurationService.Repositories
{
    /// <summary>
    ///   A configuration repository based on files.
    /// </summary>
    public class FileConfigurationRepository : IConfigurationRepository
    {
	private readonly string _baseDirectory;
	public FileConfigurationRepository(string baseDirectory)
	{
	    this._baseDirectory = baseDirectory;
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
    }
}
