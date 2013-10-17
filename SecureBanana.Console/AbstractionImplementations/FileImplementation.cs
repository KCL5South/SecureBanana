using System.IO;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.AccessControl;
using System.Text;

namespace SecureBanana.Console.AbstractionImplementations
{
	internal class FileImplementation : SecureBanana.Console.Abstractions.IFile
	{
		public void AppendAllLines(string path, IEnumerable<string> contents)
		{
			File.AppendAllLines(path, contents);
		}
		public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			File.AppendAllLines(path, contents, encoding);	
		}
		public void AppendAllText(string path, string contents)
		{
			File.AppendAllText(path, contents);	
		}
		public void AppendAllText(string path, string contents, Encoding encoding)
		{
			File.AppendText(path);	
		}
		public StreamWriter AppendText(string path)
		{
			return File.AppendText(path);	
		}
		public void Copy(string sourceFileName, string destFileName)
		{
			File.Copy(sourceFileName, destFileName);	
		}
		public void Copy(string sourceFileName, string destFileName, bool overwrite)
		{
			File.Copy(sourceFileName, destFileName, overwrite);	
		}
		public Stream Create(string path)
		{
			return File.Create(path);	
		}
		public Stream Create(string path, int bufferSize)
		{
			return File.Create(path, bufferSize);	
		}
		public Stream Create(string path, int bufferSize, FileOptions options)
		{
			return File.Create(path, bufferSize, options);	
		}
		public Stream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
		{
			return File.Create(path, bufferSize, options, fileSecurity);	
		}
		public StreamWriter CreateText(string path)
		{
			return File.CreateText(path);	
		}
		public void Decrypt(string path)
		{
			File.Decrypt(path);	
		}
		public void Delete(string path)
		{
			File.Delete(path);	
		}
		public void Encrypt(string path)
		{
			File.Encrypt(path);	
		}
		public bool Exists(string path)
		{
			return File.Exists(path);	
		}
		public FileSecurity GetAccessControl(string path)
		{
			return File.GetAccessControl(path);	
		}
		public FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
		{
			return File.GetAccessControl(path, includeSections);		
		}
		public FileAttributes GetAttributes(string path)
		{
			return File.GetAttributes(path);	
		}
		public DateTime GetCreationTime(string path)
		{
			return File.GetCreationTime(path);	
		}
		public DateTime GetCreationTimeUtc(string path)
		{
			return File.GetCreationTimeUtc(path);	
		}
		public DateTime GetLastAccessTime(string path)
		{
			return File.GetLastAccessTime(path);	
		}
		public DateTime GetLastAccessTimeUtc(string path)
		{
			return File.GetLastAccessTimeUtc(path);	
		}
		public DateTime GetLastWriteTime(string path)
		{
			return File.GetLastWriteTime(path);	
		}
		public DateTime GetLastWriteTimeUtc(string path)
		{
			return File.GetLastWriteTimeUtc(path);	
		}
		public void Move(string sourceFileName, string destFileName)
		{
			File.Move(sourceFileName, destFileName);	
		}
		public Stream Open(string path, FileMode mode)
		{
			return File.Open(path, mode);	
		}
		public Stream Open(string path, FileMode mode, FileAccess access)
		{
			return File.Open(path, mode, access);	
		}
		public Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return File.Open(path, mode, access, share);	
		}
		public Stream OpenRead(string path)
		{
			return File.OpenRead(path);	
		}
		public StreamReader OpenText(string path)
		{
			return File.OpenText(path);	
		}
		public Stream OpenWrite(string path)
		{
			return File.OpenWrite(path);	
		}
		public byte[] ReadAllBytes(string path)
		{
			return File.ReadAllBytes(path);	
		}
		public string[] ReadAllLines(string path)
		{
			return File.ReadAllLines(path);	
		}
		public string[] ReadAllLines(string path, Encoding encoding)
		{
			return File.ReadAllLines(path, encoding);	
		}
		public string ReadAllText(string path)
		{
			return File.ReadAllText(path);	
		}
		public string ReadAllText(string path, Encoding encoding)
		{
			return File.ReadAllText(path, encoding);	
		}
		public IEnumerable<string> ReadLines(string path)
		{
			return File.ReadLines(path);	
		}
		public IEnumerable<string> ReadLines(string path, Encoding encoding)
		{
			return File.ReadLines(path, encoding);	
		}
		public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
		{
			File.Replace(sourceFileName, destinationFileName, destinationBackupFileName);	
		}
		public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);	
		}
		public void SetAccessControl(string path, FileSecurity fileSecurity)
		{
			File.SetAccessControl(path, fileSecurity);	
		}
		public void SetAttributes(string path, FileAttributes fileAttributes)
		{
			File.SetAttributes(path, fileAttributes);	
		}
		public void SetCreationTime(string path, DateTime creationTime)
		{
			File.SetCreationTime(path, creationTime);	
		}
		public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			File.SetCreationTimeUtc(path, creationTimeUtc);	
		}
		public void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			File.SetLastAccessTime(path, lastAccessTime);	
		}
		public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			File.SetLastAccessTimeUtc(path, lastAccessTimeUtc);	
		}
		public void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			File.SetLastWriteTime(path, lastWriteTime);	
		}
		public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);	
		}
		public void WriteAllBytes(string path, byte[] bytes)
		{
			File.WriteAllBytes(path, bytes);	
		}
		public void WriteAllLines(string path, IEnumerable<string> contents)
		{
			File.WriteAllLines(path, contents);
		}
		public void WriteAllLines(string path, string[] contents)
		{
			File.WriteAllLines(path, contents);	
		}
		public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			File.WriteAllLines(path, contents, encoding);	
		}
		public void WriteAllLines(string path, string[] contents, Encoding encoding)
		{
			File.WriteAllLines(path, contents, encoding);	
		}
		public void WriteAllText(string path, string contents)
		{
			File.WriteAllText(path, contents);	
		}
		public void WriteAllText(string path, string contents, Encoding encoding)
		{
			File.WriteAllText(path, contents, encoding);	
		}	
	}
}