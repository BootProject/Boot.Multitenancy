![Logo](rec/multitenancy.png)
#Web.config configuration

This is the most trivial way of getting starting is to use the built in configuration.

### Init host
In Startup.cs or Applicatio_Start in Global application, add:

    //startup.cs
      Host.Init();

This will trigger Boot.Multitenancy to look for the configuration in web.config.


###Web.config.

Add these lines in web.config (root document). 

    <configSections>
      <section name="sessionFactoryConfiguration" type="Boot.Multitenancy.Configuration.SessionFactoryConfiguration, Boot.Multitenancy" />  
    </configSections>

    <sessionFactoryConfiguration configSource="Boot.SessionConfiguration.MySql.config" />

###External config file.

Create a file with a name that suit your database. For eg. Databases.MySql.config, in the root of application(web project) and add the following content.

    <sessionFactoryConfiguration persist="true" namespace="Boot">
      <databases>
        <clear/>
          <add name="devdata"
            theme = "Dev"
            autoPersist="true"
            dbType="MySql5"
            connectionstring = "Server=127.0.0.1;Port=3306;Database=devdata;Uid=boots;Pwd=boots;"
            domains="www.boot.com|boot.com"
            properties = "Theme,Dev" />
            <!--Add more databases here if needed.-->
    </sessionFactoryConfiguration>