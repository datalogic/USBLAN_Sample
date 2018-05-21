using datalogic_ce_sync;
using System;
using System.Collections.Generic;
using System.IO;

namespace USBLAN_Sample
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("~~~ Datalogic USB/LAN API Test App ~~~");
			Console.WriteLine("1) Start Process");
			Console.WriteLine("2) Find Files");
			Console.WriteLine("3) Find Directories");
			Console.WriteLine("4) Create Directory");
			Console.WriteLine("5) Push File To Device");
			Console.WriteLine("6) Pull File From Device");
			Console.WriteLine("7) Set File Date and Time");
			Console.WriteLine("8) Delete File");
			Console.WriteLine("Enter) Exit");
			Console.WriteLine("\nChoose test...");

			string command;
			while ((command = Console.ReadLine()) != "")
			{
				try
				{
					Console.WriteLine("Starting test {0}...", command);
					switch (command)
					{
						case "1": TestStartProcess(); break;
						case "2": TestFindFiles(); break;
						case "3": TestFindDirectories(); break;
						case "4": TestCreateDirectory(); break;
						case "5": TestPushFileToDevice(); break;
						case "6": TestPullFileFromDevice(); break;
						case "7": TestSetFileDateTime(); break;
						case "8": TestDeleteFile(); break;
							
						default: Console.WriteLine("Invalid input."); break;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
				Console.WriteLine("Done");
			}
		}

		private static void TestStartProcess()
		{
			bool result = USBLAN.StartProcess("/Windows/ctlpnl.exe", "/Windows/Audio.cpl");
			Console.WriteLine("response: " + result);
		}
		private static void TestFindFiles()
		{
			IEnumerable<SimpleFileInfo> files = USBLAN.FindFiles("/Windows/", "DeviceImage.pn*");
			if (files != null)
			{
				int count = 0;
				Console.WriteLine("Files: ");
				foreach (var file in files)
				{
					Console.WriteLine(++count + ") " + file.Name + " : " + file.Length);
					Console.WriteLine("\t" + file.FullName + " : " + file.Extension);
				}
			}
			else
				Console.WriteLine("returned null");
		}
		private static void TestFindDirectories()
		{
			IEnumerable<SimpleFileInfo> dirs = USBLAN.FindDirectories("/Windows/", "*ro*");
			if (dirs != null)
			{
				Console.WriteLine("Dirs: ");
				int count = 0;
				foreach (var dir in dirs)
					Console.WriteLine(++count + ") " + dir.Name);
			}
			else
				Console.WriteLine("returned null");
		}
		private static void TestCreateDirectory()
		{
			bool result = USBLAN.CreateDirectory("/My Documents/testdir");
			Console.WriteLine("response: " + result);
		}
		private static void TestPushFileToDevice()
		{
			bool result = USBLAN.PushFileToDevice("c:/100MB.bin", "/Temp/100MB.bin", false);
			Console.WriteLine("response: " + result);
		}
		private static void TestPullFileFromDevice()
		{
			bool result = USBLAN.PullFileFromDevice("/Temp/big.jpg", "C:/Users/mchew/Documents/IT_71796/pull.jpg", false);
			Console.WriteLine("response: " + result);
		}
		private static void TestSetFileDateTime()
		{
			DateTime dt = new DateTime(1995, 10, 25, 3, 57, 0, 050);
			Console.WriteLine(dt.ToFileTimeUtc());
			bool result = USBLAN.SetFileDateTime("Temp/push.txt", dt);
			Console.WriteLine("response: " + result);
		}
		private static void TestDeleteFile()
		{
			bool result = USBLAN.DeleteFile("/Temp/push.txt");
			Console.WriteLine("response: " + result);
		}

		private static bool CmpInfo(FileInfo fi, SimpleFileInfo sfi)
		{
			return fi.FullName == sfi.FullName
				&& fi.Name == sfi.Name
				&& fi.Length == sfi.Length
				&& fi.LastWriteTime == sfi.LastWriteTime
				&& fi.Extension == sfi.Extension
				&& fi.DirectoryName == sfi.DirectoryName
				;
		}
	}
}
