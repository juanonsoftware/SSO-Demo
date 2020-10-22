Step 1: Create a web mvc project targeting .net framework 462

Step 2: Install the package

Install-Package Security.Users.ExternManaged

Step 3: Add configuration changes

* Add to <appSettings>

    <add key="AppPassword" value="PwdFor@SsoDemoApp$" />
    <add key="AppName" value="Sso Demo - Local" />
    <add key="AppLogOffUrl" value="http://id.appnam.com/Auth/LogOff?appId=9" />
    <add key="AppManageAccountUrl" value="http://id.appnam.com" />
	
* Add to <system.web>

	<authentication mode="Forms">
      <forms loginUrl="http://id.appnam.com/Auth/AuthOn?appId=9" slidingExpiration="true" timeout="60" domain="" defaultUrl="~/" />
    </authentication>

    <httpModules>
      <add name="ExternManagedAuthentication" type="Security.Users.ExternManaged.AuthenticationModule, Security.Users.ExternManaged" />
    </httpModules>
	
* Add to <system.webServer>

	<validation validateIntegratedModeConfiguration="false" />
    <modules>
      <add name="ExternManagedAuthentication" type="Security.Users.ExternManaged.AuthenticationModule, Security.Users.ExternManaged" />
    </modules>
	
* Add to the Home controller

        [Authorize]
        public ActionResult Auth()
        {
            return Redirect("~/");
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session.Clear();

            var logOffUrl = ConfigurationManager.AppSettings["AppLogOffUrl"];

            return Redirect(logOffUrl);
        }
		
* Add serialization strategy on Application_Start()

SerializationContext.Current.Initialize(SerializationType.DataContractJson);		
		
* Add a login/logout buttons on Index.cshtml

        @if (User.Identity.IsAuthenticated)
        {
            <a class="btn btn-info btn-lg" href="@Url.Action("Auth")">Login</a>
        }
        else
        {
            <a class="btn btn-info btn-lg" href="@Url.Action("LogOff")">Logout => @User.Identity.Name</a>
        }

		
Step 4: Run the application and test on your side with account below:

test@gmail.com / Abc@123456
