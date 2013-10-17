using NUnit.Framework;
using Microsoft.Practices.Unity;
using Moq;
using SecureBanana.Console.Abstractions;
using SecureBanana.Console.Services;
using System.Linq;

namespace SecureBanana.Console
{
	[TestFixture]
	public class BootstrapperTests
	{
		[Test]
		public void Bootstrap_ContainerInitialized()
		{
			IUnityContainer container = Mock.Of<IUnityContainer>();
			Bootstrapper.Bootstrap(container);
			Assert.AreEqual(container, Bootstrapper.Container);
		}

		[Test]
		public void Bootstrap_NullContainer()
		{
			try
			{
				Bootstrapper.Bootstrap(null);
				Assert.Fail("An ArgumentNullException was expected.");
			}
			catch (System.ArgumentNullException) { }
		}

		[Test]
		[TestCase(typeof(IAESAlgorithm))]
		[TestCase(typeof(IHashAlgorithm))]
		[TestCase(typeof(IEncryptor))]
		[TestCase(typeof(IFile))]
		public void IsRegistered(System.Type targetType)
		{
			IUnityContainer container = new UnityContainer();
			Bootstrapper.Bootstrap(container);
			Assert.IsTrue(container.IsRegistered(targetType));
		}

		[Test]
		[TestCase(typeof(IAESAlgorithm))]
		[TestCase(typeof(IHashAlgorithm))]
		[TestCase(typeof(IEncryptor))]
		public void IsRegisteredAsPerResolve(System.Type targetType)
		{
			IUnityContainer container = new UnityContainer();
			Bootstrapper.Bootstrap(container);

			var registration = container.Registrations.FirstOrDefault(a => a.RegisteredType == targetType);
			if (registration == null)
				Assert.Inconclusive("Unable to determine if the target type ({0}) is being registered with a per resolve lifetime manager.", targetType);

			Assert.IsInstanceOf<PerResolveLifetimeManager>(registration.LifetimeManager, @"
We need to make sure that we're creating a new instance of this abstraction every time we resolve 
for a new one.  So we check to make sure that we're registering the type with a PerResolveLifetimeManager.
");
		}
	}
}