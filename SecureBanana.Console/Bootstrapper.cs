using Microsoft.Practices.Unity;
using SecureBanana.Console.Abstractions;
using SecureBanana.Console.AbstractionImplementations;
using SecureBanana.Console.Services;
using SecureBanana.Console.ServiceImplementations;

namespace SecureBanana.Console
{
	internal class Bootstrapper
	{
		internal static IUnityContainer Container { get; private set; }

		public static void Bootstrap(IUnityContainer container)
		{
			if(container == null)
				throw new System.ArgumentNullException("container");
			Container = container;

			container.RegisterType<IAESAlgorithm, AESAlgorithmImplementation>(new PerResolveLifetimeManager());
			container.RegisterType<IHashAlgorithm, HashAlgorithmImplementation>(new PerResolveLifetimeManager());
			container.RegisterType<IEncryptor, EncryptorImplementation>(new PerResolveLifetimeManager());
			container.RegisterType<IFile, FileImplementation>();
		}
	}
}