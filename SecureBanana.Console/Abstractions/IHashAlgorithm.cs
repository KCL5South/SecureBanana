namespace SecureBanana.Console.Abstractions
{
	public interface IHashAlgorithm : System.IDisposable
	{
		bool CanReuseTransform { get; }
		bool CanTransformMultipleBlocks { get; }
		byte[] Hash { get; }
		int HashSize { get; }
		int InputBlockSize { get; }
		int OutputBlockSize { get; }

		void Clear();
		byte[] ComputeHash(byte[] buffer, int offset, int count);
		byte[] ComputeHash(byte[] buffer);
		byte[] ComputeHash(System.IO.Stream inputStream);
		void Initialize();
		int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);
		byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
	}
}