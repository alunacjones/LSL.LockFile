using System;

namespace LSL.LockFile
{
	/// <summary>
	/// Description of ILockFileFactory.
	/// </summary>
	public interface ILockFileFactory
	{		
		ILockFile Create(string lockFilePath);
	}
}
