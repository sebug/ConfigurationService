using System;
using System.Linq;
using Microsoft.AspNet.DataProtection;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using System.Security.Cryptography;

namespace ConfigurationService.Services
{
    public class AuthenticationService : IAuthenticationService, IKeyGeneratorService
    {
	private readonly IDataProtector _protector;
	private readonly ILogger<AuthenticationService> _logger;
	
	public AuthenticationService(IDataProtectionProvider protectionProvider,
				     ILogger<AuthenticationService> logger)
	{
	    this._protector = protectionProvider.CreateProtector("ConfigurationService.AuthenticationService");
	    this._logger = logger;
	}
	
	public bool IsAuthenticated(string applicationCode, string key)
	{
	    string protectedSample = 
		_protector.Protect(String.Format("{0}-{1:yyyy-MM-dd}",
						 applicationCode,
						 DateTime.UtcNow));

	    this._logger.Log(LogLevel.Information, 4, "Protected string sample for " + applicationCode + " : " + protectedSample, null, null);

	    string unprotected = null;

	    try
	    {
		unprotected = this._protector.Unprotect(key);
	    }
	    catch (CryptographicException ex)
	    {
		this._logger.Log(LogLevel.Error, 5, null, ex, null);
		return false;
	    }

	    this._logger.Log(LogLevel.Information, 4, "Unprotected key: " + unprotected, null, null);

	    if (unprotected.StartsWith(unprotected)) {
		return true;
	    }
	    
	    return false;
	}

	public string GetKeyForApplicationCode(string applicationCode)
	{
	    return this._protector.Protect(String.Format("{0}-{1:yyyy-MM-dd}",
							 DateTime.UtcNow));
	}
    }
}