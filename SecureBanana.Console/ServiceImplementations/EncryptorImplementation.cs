using SecureBanana.Console.Abstractions;
using System.Security.Cryptography;
using System.Linq;
using System.IO;

namespace SecureBanana.Console.ServiceImplementations
{
	internal class EncryptorImplementation : SecureBanana.Console.Services.IEncryptor
	{
		public IAESAlgorithm Algorithm { get; private set; }
		public IHashAlgorithm HashAlgorithm { get; private set; }
		public IFile FileSystem { get; private set; }

		public EncryptorImplementation(IAESAlgorithm algorithm, IHashAlgorithm hashAlgorithm, IFile fileSystem)
		{
			if(algorithm == null)
				throw new System.ArgumentNullException("algorithm");
			if(hashAlgorithm == null)
				throw new System.ArgumentNullException("hashAlgorithm");
			if(fileSystem == null)
				throw new System.ArgumentNullException("fileSystem");

			Algorithm = algorithm;
			HashAlgorithm = hashAlgorithm;
			FileSystem = fileSystem;
		}	

		#region IEncryptor Members

		public void Encrypt(System.IO.Stream input, System.IO.Stream output, string password)
		{
			if(input == null)
				throw new System.ArgumentNullException("input");
			if(output == null)
				throw new System.ArgumentNullException("output");
			if(string.IsNullOrEmpty(password))
				throw new System.ArgumentException("Must not be null or empty.", "password");

			byte[] hashedPassword = HashAlgorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

			// Create a decrytor to perform the stream transform.
			ICryptoTransform encryptor = Algorithm.CreateEncryptor(hashedPassword.Take(32).ToArray(), hashedPassword.Skip(48).ToArray());
			
			using (MemoryStream msEncrypt = new MemoryStream())
			{	
				using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				{
					input.CopyTo(csEncrypt);
					csEncrypt.FlushFinalBlock();
					msEncrypt.Position = 0;
					msEncrypt.CopyTo(output);
				}
			}
		}

		public void Encrypt(string inputFile, string outputFile, string password)
		{
			if(string.IsNullOrWhiteSpace(inputFile))
				throw new System.ArgumentException("Must not be null, empty, or only whitespace.", "inputFile");
			if(string.IsNullOrWhiteSpace(outputFile))
				throw new System.ArgumentException("Must not be null, empty, or only whitespace.", "outputFile");
			if(string.IsNullOrWhiteSpace(password))
				throw new System.ArgumentException("Must not be null, empty, or only whitespace.", "password");

			if(!FileSystem.Exists(inputFile))
				throw new System.IO.FileNotFoundException(inputFile);
			if(FileSystem.Exists(outputFile))
				throw new System.Exception(string.Format("The output file ({0}) already exists.  You must delete the file first.", outputFile));

			using(var inputFileStream = FileSystem.OpenRead(inputFile))
			{
				using(var outputFileStream = FileSystem.OpenWrite(outputFile))
				{
					Encrypt(inputFileStream, outputFileStream, password);
					outputFileStream.Flush();
				}
			}
		}

		public void Decrypt(System.IO.Stream input, System.IO.Stream output, string password)
		{
			if(input == null)
				throw new System.ArgumentNullException("input");
			if(output == null)
				throw new System.ArgumentNullException("output");
			if(string.IsNullOrEmpty(password))
				throw new System.ArgumentException("Must not be null or empty.", "password");

			byte[] hashedPassword = HashAlgorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

			// Create a decrytor to perform the stream transform.
			ICryptoTransform encryptor = Algorithm.CreateDecryptor(hashedPassword.Take(32).ToArray(), hashedPassword.Skip(48).ToArray());
			
			using (MemoryStream msEncrypt = new MemoryStream())
			{	
				using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				{
					input.CopyTo(csEncrypt);
					csEncrypt.FlushFinalBlock();
					msEncrypt.Position = 0;
					msEncrypt.CopyTo(output);
				}
			}
		}

		public void Decrypt(string inputFile, string outputFile, string password)
		{
			if(string.IsNullOrWhiteSpace(inputFile))
				throw new System.ArgumentException("Must not be null, empty, or only whitespace.", "inputFile");
			if(string.IsNullOrWhiteSpace(outputFile))
				throw new System.ArgumentException("Must not be null, empty, or only whitespace.", "outputFile");
			if(string.IsNullOrWhiteSpace(password))
				throw new System.ArgumentException("Must not be null, empty, or only whitespace.", "password");

			if(!FileSystem.Exists(inputFile))
				throw new System.IO.FileNotFoundException(inputFile);
			if(FileSystem.Exists(outputFile))
				throw new System.Exception(string.Format("The output file ({0}) already exists.  You must delete the file first.", outputFile));

			using(var inputFileStream = FileSystem.OpenRead(inputFile))
			{
				using(var outputFileStream = FileSystem.OpenWrite(outputFile))
				{
					Decrypt(inputFileStream, outputFileStream, password);
					outputFileStream.Flush();
				}
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			
		}

		#endregion
	}
}