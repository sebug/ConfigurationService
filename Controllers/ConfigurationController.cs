using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace ConfigurationService.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
	public IEnumerable<string> Get()
	{
	    return new string[0];
	}
    }
}
