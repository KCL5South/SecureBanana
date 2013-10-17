using NUnit.Framework;
using TechTalk.SpecFlow;
using System.Diagnostics;

namespace SecureBanana.Console.Features.StepDefinitions
{
	[Binding]
	public class Encrypt_StepDefinitions
	{
		private System.Diagnostics.ProcessStartInfo StartInfo = null;
		private System.Diagnostics.Process Executable = null;
		private string StandardError = null;
		private string StandardOutput = null;

		public FileSystem.TempDirectory TestDirectory { get; set; }

		public void SetupExecutable()
		{
			string exePath = null;

			#if DEBUG
			exePath = @"..\..\..\SecureBanana.Console\bin\Debug\SecureBanana.exe";
			#else
			exePath = @"..\..\..\SecureBanana.Console\bin\Release\SecureBanana.exe";
			#endif
			
			if (System.IO.File.Exists(exePath))
			{
				StartInfo = new ProcessStartInfo(exePath);
			}
			else
				throw new System.Exception("Unable to find the SecureBanana executable.");

			StartInfo.CreateNoWindow = true;
			StartInfo.UseShellExecute = false;
			StartInfo.RedirectStandardError = true;
			StartInfo.RedirectStandardOutput = true;
			StartInfo.WorkingDirectory = TestDirectory.Path;

			Executable = new Process();
			Executable.StartInfo = StartInfo;
		}

		[Before]
		public void BeforeRun()
		{
			TestDirectory = new FileSystem.TempDirectory();

			SetupExecutable();
		}

		[After]
		public void AfterRun()
		{
			TestDirectory.Dispose();
		}

		[When(@"I have a file called ""(.*)""")]
		public void CreateFile(string filePath)
		{
			System.IO.File.Create(TestDirectory.Append(filePath)).Dispose();	
		}

		[When(@"""(.*)"" has content of:")]
		public void PopulateFile(string filePath, string content)
		{
			if(!System.IO.File.Exists(TestDirectory.Append(filePath)))
				Assert.Fail("{0} was expected but was not found.", filePath);

			System.IO.File.WriteAllText(TestDirectory.Append(filePath), content);
		}

		[When(@"I pass the following arguments to SecureBanana:")]
		public void SetArguments(string arguments)
		{
			StartInfo.Arguments = arguments;
		}

		[When(@"SecureBanana is ran for encryption")]
		public void Run()
		{
			Executable.Start();
			StandardError = Executable.StandardError.ReadToEnd();
			StandardOutput = Executable.StandardOutput.ReadToEnd();
			Executable.WaitForExit(10000);

			Assert.AreEqual(0, Executable.ExitCode, "The exit code of SecureBanana indicates an error.{0}{1}", System.Environment.NewLine, StandardError);
			SetupExecutable();
		}

		[Then(@"the content of ""(.*)"" should not be:")]
		public void ContentNotEqual(string filepath, string content)
		{
			if(!System.IO.File.Exists(TestDirectory.Append(filepath)))
				Assert.Fail("{0} was expected but was not found.", filepath);

			Assert.AreNotEqual(content, System.IO.File.ReadAllText(TestDirectory.Append(filepath)));
		}

		[Then(@"the content of ""(.*)"" should be:")]
		public void ContentEqual(string filepath, string content)
		{
			if(!System.IO.File.Exists(TestDirectory.Append(filepath)))
				Assert.Fail("{0} was expected but was not found.", filepath);

			Assert.AreEqual(content, System.IO.File.ReadAllText(TestDirectory.Append(filepath)));
		}
	}
}