using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConfigurationService.Repositories;
using Microsoft.Framework.Logging;
using ConfigurationService.Services;

namespace ConfigurationService.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
	private readonly IConfigurationRepository _configurationRepository;
	private readonly ILogger<ConfigurationController> _logger;
	private readonly IAuthenticationService _authenticationService;
	
	public ConfigurationController(IConfigurationRepository configurationRepository,
				       ILogger<ConfigurationController> logger,
				       IAuthenticationService authenticationService)
	{
	    this._configurationRepository = configurationRepository;
	    this._logger = logger;
	    this._authenticationService = authenticationService;
	}

	[HttpGet]
	public IEnumerable<string> Get()
	{
	    Console.WriteLine("Getting");
	    return this._configurationRepository.GetApplicationCodes();
	}

	private void LogApplicationAccess(string applicationCode, string key)
	{
	    this._logger.Log(LogLevel.Information, 1, "Getting application with code " + applicationCode + " and key " + key, null, null);
	}

	private void LogCredentialError(string applicationCode, string key)
	{
	    this._logger.Log(LogLevel.Warning, 2, "Authentication failed for application code " + applicationCode + " and key " + key, null, null);
	}

	[HttpGet("{applicationCode}")]
	public IDictionary<string, string> Get(string applicationCode, string key)
	{
	    this.LogApplicationAccess(applicationCode, key);
	    if (!this._authenticationService.IsAuthenticated(applicationCode, key)) {
		this.LogCredentialError(applicationCode, key);
		return new Dictionary<string, string>();
	    }
	    return this._configurationRepository.GetByApplicationCode(applicationCode);
	}
    }
}
