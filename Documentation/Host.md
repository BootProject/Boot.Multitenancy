![Logo](rec/multitenancy.png)
#Host

The Host is responsible for creating Tenants. Host is implemented as a Singleton, because there can be only one Host that contain Tenants.

##There are several ways to create a Tenant.

###Web.config
>The regular way is to use Host.Init(). This require a minimum effort in web.config. [Read more](webconfiguration)


###Built in Tenant
>The second way is to create a dictionary of Tenants and persist these to Init(Dictionay<Key, ITenant>).

###Create your own Tenant
>The third way is to write your own Tenant and provide them as a Dictionay<Key, YourClassNameOfYourTenant>.

web.config setup.


