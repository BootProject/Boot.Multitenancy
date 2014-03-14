![Logo](rec/multitenancy.png)

# HostContainer
## The magic of SessionFactoryHostContainer.

SessionFactoryHostContainer creats and delivers Nhibernate ISessionFactories. When you call SessionFactoryHostContainer.CurrentFactory, the system already knows your ISession and domain. In this way you can also provide Properties, like themes or other resources you need in your application.