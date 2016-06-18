using System;
using System.IO;

namespace LSL.LockFile
{
	/// <summary>
	/// LockFileFactory.
	/// </summary>
	public class LockFileFactory : ILockFileFactory
	{	
		public ILockFile Create(string lockFilePath)
		{
			return new LockFile(lockFilePath)
				.Create();
		}
		
		internal class LockFile : ILockFile
		{
			internal LockFile(string lockFilePath)
			{
				LockFilePath = lockFilePath;
			}

			public string LockFilePath { get; private set; }

			public bool WasCreated { get; private set; }
                                    
			internal ILockFile Create()
			{
				try {
					File.Open(LockFilePath, FileMode.CreateNew).Dispose();
					WasCreated = true;
				}
				// disable once EmptyGeneralCatchClause
				catch {}
				
				return this;
			}
			
			public void Dispose()
			{				
			}

			public void DisposeManaged()
			{
				if (WasCreated) {
					File.Delete(LockFilePath);				
				}				
			}			
		}
	}
}
