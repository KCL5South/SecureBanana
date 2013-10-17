using NUnit.Framework;
using System.Text;

namespace SecureBanana.Console.Features.FileSystem
{
	[TestFixture]
	public class TempDirectoryTests
	{
		[Test, Category("Unit")]
		public void Constructor_DisposedSetToFalse()
		{
			TempDirectory target = new TempDirectory();
			Assert.IsFalse(target.Disposed, "The Disposed property should be false after construction.");
		}
		[Test, Category("Unit")]
		public void Constructor_PathSet()
		{
			TempDirectory target = new TempDirectory();
			Assert.IsNotNull(target.Path, "The Path property should not be null after construction.");
		}
		[Test, Category("Unit")]
		public void Constructor_PathBeginsWithTempPath()
		{
			TempDirectory target = new TempDirectory();
			Assert.IsTrue(target.Path.StartsWith(System.IO.Path.GetTempPath()), "The Path property should point to a location within the user's temp directory.");
		}
		[Test, Category("Unit")]
		public void Constructor_DirectoryExists()
		{
			TempDirectory target = new TempDirectory();
			Assert.IsTrue(System.IO.Directory.Exists(target.Path), "The directory designated by 'Path' should exist after construction.");
		}
		[Test, Category("Unit")]
		public void Dispose_DisposedSetToTrue()
		{
			TempDirectory target = new TempDirectory();
			target.Dispose();

			Assert.IsTrue(target.Disposed, "The Disposed property should be set to true after disposal.");
		}
		[Test, Category("Unit")]
		public void Dispose_DirectoryDoesNotExist()
		{
			TempDirectory target = new TempDirectory();
			target.Dispose();

			Assert.IsFalse(System.IO.Directory.Exists(target.Path), "The directory designated by 'Path' should not exist after disposal: {0}", target.Path);
		}
		[Test, Category("Unit")]
		public void Finalize_DirectoryDoesNotExist()
		{
			TempDirectory target = new TempDirectory();
			string path = target.Path;

			target = null;

			System.GC.Collect();
			System.GC.WaitForPendingFinalizers();

			Assert.IsFalse(System.IO.Directory.Exists(path), "The directory designated by 'Path' should not exist after disposal: {0}", path);
		}
		[Test, Category("Unit")]
		public void Append_ReturnsStringBuilderWithCorrectPath()
		{
			TempDirectory target = new TempDirectory();

			Assert.AreEqual(System.IO.Path.Combine(target.Path, ""), target.Append(""), "The value of the StringBuilder result is not correct.");
		}
	}
}