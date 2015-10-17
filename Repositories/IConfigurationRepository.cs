using System;
using System.Collections.Generic;

namespace ConfigurationService.Repositories
{
    public interface IConfigurationRepository
    {
	IEnumerable<string> GetApplicationCodes();

	IDictionary<string, string> GetByApplicationCode(string applicationCode);
    }
}
