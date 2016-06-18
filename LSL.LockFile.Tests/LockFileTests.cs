using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using LSL.LockFile;

namespace LSL.LockFile.Tests
{
	[TestFixture]
	public class LockFileTests
	{
		string LockFilePath { get; set; }
                     
		public LockFileTests()
		{
			LockFilePath = string.Format("test-{0}.lock", Guid.NewGuid().ToString("N"));
		}
		
		private ILockFile BuildSut()
		{
			return new LockFileFactory().Create(LockFilePath);
		}
		
		[Test]
		public void LockFileTests_WhenALockFileDoesNotExistItShouldCreateOne()
		{
			
			using (var sut = BuildSut())
			{
				File.Exists(sut.LockFilePath).Should().BeTrue();
				sut.WasCreated.Should().BeTrue();
			}
			
			Assert.IsFalse(File.Exists(LockFilePath));
		}
		
		[Test]
		public void LockFileTests_WhenALockFileAlreadyExistsItShouldNotCreateANewOne()
		{
			File.WriteAllBytes(LockFilePath, new byte[0]);
			
			try {
				using (var sut = BuildSut())
				{
					sut.WasCreated.Should().BeFalse();
				}
				
				File.Exists(LockFilePath).Should().BeTrue();
			}
			finally {
				File.Delete(LockFilePath);
			}
		}
		
		[Test]
		public void LockFileTests_WhenALockFileDoesNotExistButSomeoneElseDeletesOursThenItShouldNotThrowAnException()
		{
			new Action(() => {
				using (var sut = BuildSut())
				{
					File.Delete(LockFilePath);
				}
			}).ShouldNotThrow();
		}
	}
}
