using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using Microsoft.Practices.Unity;
using SecureBanana.Console.Services;

namespace SecureBanana.Console
{
	class Program
	{
		static int Main(string[] args)
		{
			Bootstrapper.Bootstrap(new UnityContainer());

			bool show_help = false;
			string inputFile = null, outputFile = null, password = null;
			string workingDirectory = System.IO.Directory.GetCurrentDirectory();
			bool? encrypt = null;

			OptionSet p = null;

			try
			{
				p = new OptionSet () {
		            { "?|help",  "Show this message and exit", 
		              v => show_help = v != null },
		            { "e|encrypt=", "The {FILE} to encrypt.", v => 
		            	{
							inputFile = v; 
							encrypt = true; 
		            	} 
		            },
		            { "d|decrypt=", "The {FILE} to decrypt.", v => 
		            	{
		            		inputFile = v; 
		            		encrypt = false; 
		            	} 
		            },
		            { "o|output=", "The {FILE} that will contain the encryped or decrypted output.", v => outputFile = v },
		            { "p|password=", "The {PASSWORD} used to perform the encryption/decryption.", v => password = v }
		        };
		    }
		    catch(System.Exception ex)
		    {
		    	System.Console.Error.WriteLine(ex.Message);
		    	return 1;
		    }

	        List<string> extra;
	        try {
	            extra = p.Parse (args);

	            if(show_help || extra.Count > 0)
		        {
		        	ShowHelp(p);
				}
				else if(encrypt.HasValue && encrypt.Value)
				{
					using(var encryptor = Bootstrapper.Container.Resolve<IEncryptor>())
					{
						encryptor.Encrypt(inputFile, outputFile, password);
					}
				}
				else if(encrypt.HasValue && !encrypt.Value)
				{
					using(var encryptor = Bootstrapper.Container.Resolve<IEncryptor>())
					{
						encryptor.Decrypt(inputFile, outputFile, password);
					}
				}
	        }
	        catch (OptionException) {
	        	ShowHelp(p);
	            return 1;
	        }
	        catch (System.Exception ex)
	        {
	        	OutputError(ex);	
	        	return 1;
	        }

			return 0;
		}

		static void OutputError(System.Exception ex)
		{
			System.Console.Error.WriteLine("Error:");
			System.Exception curr = ex;
			while(curr != null)
			{
				System.Console.Error.WriteLine(string.Format("\t{0}{1}\t\t{2}", curr.GetType(), System.Environment.NewLine, curr.Message));
				curr = curr.InnerException;
			}
		}

		static void ShowHelp(OptionSet options)
		{
			System.Console.WriteLine("Secure Banana");
			System.Console.WriteLine("Squadron 5 South");
			System.Console.WriteLine("----------------");
			System.Console.WriteLine("Usage: SecureBanana [Options]");
			options.WriteOptionDescriptions(System.Console.Out);
		}
	}
}
