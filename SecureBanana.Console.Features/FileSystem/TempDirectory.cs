using System.Text;

namespace SecureBanana.Console.Features.FileSystem
{
	public class TempDirectory : System.IDisposable
	{
		public bool Disposed { get; private set; }
		public string Path { get; private set; }

		public TempDirectory()
		{
			Disposed = false;
			Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.Guid.NewGuid().ToString().Replace("-", ""));
			System.IO.Directory.CreateDirectory(Path);
		}
		~TempDirectory()
		{
			Dispose(false);
		}

		protected void Dispose(bool disposing)
		{
			if(!Disposed)
			{
				//Delete this directory and all subdirectories;
				System.IO.Directory.Delete(Path, true);
				Disposed = true;
			}
		}

		public string Append(string pathToAppend)
		{
			return System.IO.Path.Combine(Path, pathToAppend);
		}

		#region System.IDisposable Members

		public void Dispose()
		{
			System.GC.SuppressFinalize(this);
			Dispose(true);
		}

		#endregion
	}	
}