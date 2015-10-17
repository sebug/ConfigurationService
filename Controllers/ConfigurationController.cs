using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConfigurationService.Repositories;

namespace ConfigurationService.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
	private readonly IConfigurationRepository _configurationRepository;
	
	public ConfigurationController(IConfigurationRepository configurationRepository)
	{
	    this._configurationRepository = configurationRepository;
	}
	
	public IEnumerable<string> Get()
	{
	    return this._configurationRepository.GetApplicationCodes();
	}
    }
}
