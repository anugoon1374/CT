using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.Proxy;
using System;

namespace Beyond.Ct
{
    /// <summary>
    /// Service container for inject dependency and use/locate them during run time.
    /// </summary>
    /// <remarks>
    /// Best practice to use this is inject service at the application initialization and resolve them at constructor.
    /// Try to avoid using service locater, because it will make code harder to maintain.
    /// </remarks>
    public class ServiceCollection
    {
        #region service container

        /// <summary>
        /// The service container.
        /// </summary>
        private static IWindsorContainer _serviceContainer = new WindsorContainer(new DefaultKernel(
                                                                           new InlineDependenciesPropagatingDependencyResolver(),
                                                                           new DefaultProxyFactory()),
                                                                           new DefaultComponentInstaller());

        /// <summary>
        /// The lock for service container.
        /// </summary>
        private static readonly object LockService = new object();

        /// <summary>
        /// Gets or sets the service container.
        /// </summary>
        /// <value>The service container.</value>
        public static IWindsorContainer ServiceContainer
        {
            get { return _serviceContainer; }
            set
            {
                lock (LockService)
                {
                    _serviceContainer = value;
                }
            }
        }

        #endregion service container

        #region service registration methods

        /// <summary>
        /// Injects a dependency with transient lifestyle;
        /// A new instance is created every time.
        /// </summary>
        /// <typeparam name="TServiceInterface">The type of the service interface.</typeparam>
        /// <typeparam name="TServiceImplementation">The type of the service implementation.</typeparam>
        /// <remarks>
        /// Transient lifestyle is a good choice when you want to be in control of instance's lifetime of the instances.
        /// When you need new instance, with new state every time.
        /// Also transient components don't need to be thread safe, unless you explicitly use them in multi-threaded situations.
        /// In most applications you'll find that a large percentage of your components will end up as transient.
        /// </remarks>
        public static void AddTransient<TServiceInterface, TServiceImplementation>()
            where TServiceInterface : class
            where TServiceImplementation : class
        {
            ServiceContainer.Register(Component.For(typeof(TServiceInterface)).ImplementedBy(typeof(TServiceImplementation)).LifeStyle.Transient);
        }

        /// <summary>
        /// Injects a dependency from a DLL file with transient lifestyle;
        /// A new instance is created every time.
        /// </summary>
        /// <remarks>
        /// Transient lifestyle is a good choice when you want to be in control of instance's lifetime of the instances.
        /// When you need new instance, with new state every time.
        /// Also transient components don't need to be thread safe, unless you explicitly use them in multi-threaded situations.
        /// In most applications you'll find that a large percentage of your components will end up as transient.
        /// <para>Use WithServiceAllInterfaces() instead of WithServiceFirstInterface() because it can resolve derived class correctly.</para>
        /// </remarks>
        public static void AddTransient(string assemblyName)
        {
            if (assemblyName == null) throw new ArgumentNullException(nameof(assemblyName));
            ServiceContainer.Register(Classes.FromAssemblyNamed(assemblyName).Pick().WithServiceAllInterfaces().LifestyleTransient());
        }

        /// <summary>
        /// Injects a dependency with singleton lifestyle;
        /// An only single instance is created during application life time.
        /// </summary>
        /// <typeparam name="TServiceInterface">The type of the service interface.</typeparam>
        /// <typeparam name="TServiceImplementation">The type of the service implementation.</typeparam>
        /// <remarks>
        /// Characteristics of singleton make it a good choice in several scenarios.
        /// State-less components are one good candidate.
        /// Components that have state that is valid throughout the lifetime of the application that all the other components may need to access.
        /// If your application is multi threaded, remember to make sure state changes in your singleton are thread safe.
        /// You may want to consider making your component a singleton especially if the objects have big state, in which case producing multiple instances may unnecessarily raise memory consumption of your application.
        /// Usually, with some practice, the decision is a quick and easy one.
        /// </remarks>
        public static void AddSingleton<TServiceInterface, TServiceImplementation>()
            where TServiceInterface : class
            where TServiceImplementation : class
        {
            ServiceContainer.Register(Component.For(typeof(TServiceInterface)).ImplementedBy(typeof(TServiceImplementation)).LifeStyle.Singleton);
        }

        /// <summary>
        /// Injects a dependency from a DLL file with singleton lifestyle;
        /// An only single instance is created during application life time.
        /// </summary>
        /// <remarks>
        /// Characteristics of singleton make it a good choice in several scenarios.
        /// State-less components are one good candidate.
        /// Components that have state that is valid throughout the lifetime of the application that all the other components may need to access.
        /// If your application is multi threaded, remember to make sure state changes in your singleton are thread safe.
        /// You may want to consider making your component a singleton especially if the objects have big state, in which case producing multiple instances may unnecessarily raise memory consumption of your application.
        /// Usually, with some practice, the decision is a quick and easy one.
        /// </remarks>
        public static void AddSingleton(string assemblyName)
        {
            if (assemblyName == null) throw new ArgumentNullException(nameof(assemblyName));
            ServiceContainer.Register(Classes.FromAssemblyNamed(assemblyName).Pick().WithServiceAllInterfaces().LifestyleSingleton());
        }

        /// <summary>
        /// Injects a dependency with per-web-request lifestyle.
        /// A new instance is created and will be shared in scope of a web request.
        /// </summary>
        /// <typeparam name="TServiceInterface">The type of the service interface.</typeparam>
        /// <typeparam name="TServiceImplementation">The type of the service implementation.</typeparam>
        public static void AddPerWebRequest<TServiceInterface, TServiceImplementation>()
            where TServiceInterface : class
            where TServiceImplementation : class
        {
            ServiceContainer.Register(Component.For(typeof(TServiceInterface)).ImplementedBy(typeof(TServiceImplementation)).LifeStyle.PerWebRequest);
        }

        /// <summary>
        /// Injects a dependency from a DLL file with per-web-request lifestyle.
        /// A new instance is created and will be shared in scope of a web request.
        /// </summary>
        public static void AddPerWebRequest(string assemblyName)
        {
            if (assemblyName == null) throw new ArgumentNullException(nameof(assemblyName));
            ServiceContainer.Register(Classes.FromAssemblyNamed(assemblyName).Pick().WithServiceAllInterfaces().LifestylePerWebRequest());
        }

        #endregion service registration methods

        #region service requisition methods

        /// <summary>
        /// Resolves a specified dependency.
        /// </summary>
        /// <typeparam name="T">Interface of dependency.</typeparam>
        /// <returns>Requested dependency.</returns>
        public static T Use<T>()
        {
            return _serviceContainer.Resolve<T>();
        }

        /// <summary>
        /// Resolves a specified dependency.
        /// </summary>
        /// <returns>Requested dependency.</returns>
        public static object Use(Type type)
        {
            return _serviceContainer.Resolve(type);
        }

        #endregion service requisition methods
    }

    /// <summary>
    /// Inline dependencies propagating dependency resolver.
    /// Make service injector and locater work with WebAPI.
    /// </summary>
    /// <seealso href="http://stackoverflow.com/questions/12977743/asp-web-api-ioc-resolve-httprequestmessage">ASP Web Api - IoC - Resolve HttpRequestMessage</seealso>
    public class InlineDependenciesPropagatingDependencyResolver : DefaultDependencyResolver
    {
        /// <summary>
        /// This method rebuild the context for the parameter type. Naive implementation.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <returns>CreationContext.</returns>
        protected override CreationContext RebuildContextForParameter(
            CreationContext current,
            Type parameterType)
        {
            return parameterType.ContainsGenericParameters ? current : new CreationContext(parameterType, current, true);
        }
    }
}