#Create Tenant collection from code

> It's pretty simple to create a Tenant and adds it to Boot.Multitenancy.
> Create the ITenantConfiguration, and add it to an ITenant.
> Add each ITenant to the collection.


    using Boot.Multitenancy;
    using Boot.Multitenancy.Factory;

    public static class BootConfig
    {
        public static void Init()
        {
           var conf1 = new TenantConfiguration {
                Key = "BootTestData",
                DbType = DbType.MySql5,
                HostValues = new List<string> { "localhost", "www.boot.se" },
                Properties = new Dictionary<string, object> { {"Theme", "Boot"} },
                Connectionstring = "Server=127.0.0.1;Port=3306;Database=BootTestData;Uid=boots;Pwd=boots;"
            };
           
            var conf2 = new TenantConfiguration {
                Key = "MyOtherDatabase",
                DbType = DbType.MySql5,
                HostValues = new List<string> { "localhost", "www.boot.se" },
                Properties = new Dictionary<string, object> { { "Theme", "Boot" } },
                Connectionstring = "Server=127.0.0.1;Port=3306;Database=BootTestData;Uid=boots;Pwd=boots;"
            };

                var tenants = new TenantCollection { 
                	new Tenant(conf1), 
                	new Tenant(conf2)
            };

            Host.Init(tenants);
        }
    }