Deployment:
You need to remove web david by adding the following to web.config file on the server:

	  <modules runAllManagedModulesForAllRequests="false">
	  	<remove name="WebDAVModule" />
	  </modules>

See https://stackoverflow.com/questions/9854602/asp-net-web-api-405-http-verb-used-to-access-this-page-is-not-allowed-how