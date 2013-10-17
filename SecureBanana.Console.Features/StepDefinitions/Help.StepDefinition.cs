using TechTalk.SpecFlow;
using System;
using System.IO;
using System.Diagnostics;
using NUnit.Framework;

namespace SecureBanana.Console.Features.StepDefinitions
{
	[Binding]
	public class Help_StepDefinitions
	{
		private System.Diagnostics.ProcessStartInfo StartInfo = null;
		private System.Diagnostics.Process Executable = null;
		private string StandardError = null;
		private string StandardOutput = null;

		[Before]
		public void BeforeSteps()
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

			Executable = new Process();
			Executable.StartInfo = StartInfo;
		}

		[When(@"the following arguments are sent to SecureBanana:")]
        public void When_HelpIsPassedAsAnArgumentToSecureBanana(string arguments)
        {
			StartInfo.Arguments = arguments;
        }

		[When(@"SecureBanana is ran")]
		public void Run()
		{
			Executable.Start();
			StandardError = Executable.StandardError.ReadToEnd();
			StandardOutput = Executable.StandardOutput.ReadToEnd();
			Executable.WaitForExit(10000);
		}

        [Then(@"stdout should start with:")]
        public void StdOutShouldStartWith(string multilineText)
        {
            Assert.IsTrue(StandardOutput.StartsWith(multilineText));
        }
	}
}