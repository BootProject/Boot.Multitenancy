#Multitenancy

###Package Boot.MultiTenancy 
Location: http://www.bitbucket.org/rickardmagnusson/boot.multitenancy

###About
This package is an extension of nHibernate's ISessionFactory and 
makes it incredible easy to use multiple session within the same 
application.

###Setup
The setup is really easy, but you can of course customize it in 
any way you like. The SessionFactoryContainer just take a "Key"
and a "ISessionFactory" as argument, typically an Fluent or 
nHibenate config.

###TestProject
For simplicity all extra code is in Environment folder, except Startup 
and HomeController.

###Note:
If you get any problem with different versions of nHibernate, uninstall all 
and add the from NuGet again.