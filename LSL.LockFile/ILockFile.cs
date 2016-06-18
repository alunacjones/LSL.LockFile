using System;

namespace LSL.LockFile
{
	/// <summary>
	/// Description of ILockFile.
	/// </summary>
	public interface ILockFile : IDisposable
	{
		bool WasCreated { get; }
		string LockFilePath { get; }
	}
}
