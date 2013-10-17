namespace SecureBanana.Console.Services
{
	public interface IEncryptor : System.IDisposable
	{
		void Encrypt(System.IO.Stream input, System.IO.Stream output, string password);
		void Encrypt(string inputFile, string outputFile, string password);
		void Decrypt(System.IO.Stream input, System.IO.Stream output, string password);
		void Decrypt(string inputFile, string outputFile, string password);
	}
}