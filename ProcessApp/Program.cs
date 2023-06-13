using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProcessApp
{
    class Program
    {
        static string GetFileExtensionFromUrl(string url)
        {
            string fileExtension = Path.GetExtension(url);

            if (!string.IsNullOrEmpty(fileExtension))
            {
                // Remove the dot (.) from the file extension
                fileExtension = fileExtension.TrimStart('.');
            }

            return fileExtension;
        }

        static string GetDownloadsFolderPath()
        {
            string downloadsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            downloadsFolderPath = Path.Combine(downloadsFolderPath, "Downloads");

            return downloadsFolderPath;
        }

        public static async Task Main(string[] args)
        {
            // STYLING
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            // GETTING DOWNLOADS FOLDER PATH
            string downloadsFolderPath = GetDownloadsFolderPath().Replace("\\", "\\\\");

            // TAKING URL FROM USER
            Console.WriteLine("WELCOME TO M-DOWNLOADER");
            Console.Write("Enter the url : ");
            string url = Console.ReadLine();
            
            // GETTING THE EXTENSION OF THE FILE FROM USER PROVIDED URL BY USING GetFileExtensionFromUrl() utility function
            string fileExtension = GetFileExtensionFromUrl(url);

            // GENERATING A GUID FOR RANDOM NAME FOR DOWNLOADED FILE
            Guid guid = Guid.NewGuid();

            // PATH WHERE DOWNLOADED FILES WILL BE SAVED
            string savePath = $"{downloadsFolderPath}\\{guid.ToString()}.{fileExtension}";

            // DOWNLOADING FILE
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] data = await client.GetByteArrayAsync(url);
                    await File.WriteAllBytesAsync(savePath, data);

                    Console.WriteLine("Download successful");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occured " + ex.Message);
                }
            }
        }
    }
}