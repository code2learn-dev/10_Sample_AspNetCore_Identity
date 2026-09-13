using System.Text.RegularExpressions;

namespace Identity.WebAppRazorPage.Utilities
{
    public static class FileExtensions
    {
        static string[] allowedFormats = ["image/jpg", "image/jpeg", "image/png"];
        static int fileSize = 5 * (1024 * 1024);

        public static async Task<Dictionary<string, string>> UploadImgeAsync(
            this IFormFile? file,
            IWebHostEnvironment webHost,
            string outputDir,
            string fileTitle)
        {
            Dictionary<string, string> fileState = [];

            if (file is null || file.Length == 0)
            {
                fileState.Add("error", "فایلی برای تصویر دوره آموزشی انتخاب نشده است");
                fileState.Add("filename", "");
                return fileState;
            }

            if (!allowedFormats.Contains(file.ContentType))
            {
                fileState.Add("error", "فرمت فایل انتخابی نامعتبر است");
				fileState.Add("filename", "");
				return fileState;
			}

            string imageDirName = Path.Combine(webHost.WebRootPath, "assets/images", outputDir);
            if (!Directory.Exists(imageDirName))
                Directory.CreateDirectory(imageDirName);

            string cleanedFileName = EscapeInvalidCharsFromFileName(fileTitle);
            var fileName = $"{cleanedFileName}_{Guid.NewGuid()}_{Path.GetExtension(file.FileName)}";

            string filePathToUpload = Path.Combine(imageDirName, fileName);
            await using Stream target = new FileStream(
                                        filePathToUpload,
                                        FileMode.Create,
                                        FileAccess.Write,
                                        FileShare.None,
                                        bufferSize: fileSize,
                                        useAsync: true);
            await file.CopyToAsync(target);

            fileState.Add("filename", fileName);
            return fileState;
        }


        public static async Task<Dictionary<string, string>> EditImageAsync(
            this IFormFile? file,
            IWebHostEnvironment webHost,
            string dirName,
            string currentFileName,
            string newFileName)
        { 
            Dictionary<string, string> fileState = [];

			if (file is null || file.Length == 0)
			{ 
				fileState.Add("filename", currentFileName);
				return fileState;
			}

			if (!allowedFormats.Contains(file.ContentType))
			{
				fileState.Add("error", "فرمت فایل انتخابی نامعتبر است");
				fileState.Add("filename", "");
				return fileState;
			}

			string imageDir = Path.Combine(webHost.WebRootPath, "assets/images", dirName);
            if (!Directory.Exists(imageDir))
                Directory.CreateDirectory(imageDir);

            string currentFilePath = Path.Combine(imageDir, currentFileName);
            if (File.Exists(currentFilePath))
                File.Delete(currentFilePath);

            string cleanedFileName = EscapeInvalidCharsFromFileName(newFileName);
            string fileName = $"{cleanedFileName}_{Guid.NewGuid()}_{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(imageDir, fileName);

            await using Stream target = new FileStream(
                                                        filePath,
                                                        FileMode.Create,
                                                        FileAccess.Write,
                                                        FileShare.None,
                                                        bufferSize: fileSize,
                                                        useAsync: true);
            await file.CopyToAsync(target);
            fileState.Add("filename", fileName);
            return fileState;
        }

        public static void DeleteImage(
            this string? fileName, 
            IWebHostEnvironment webHost,
            string outputDir)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;

            string imageDir = Path.Combine(webHost.WebRootPath, "assets/images", outputDir);
            string filePath = Path.Combine(imageDir, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private static string EscapeInvalidCharsFromFileName(string? fileName) {
            if (string.IsNullOrWhiteSpace(fileName)) return string.Empty;

			// method 1: using built-in charactes
			char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (var ch in invalidChars)
            {
                fileName = fileName.Replace(ch.ToString(), "");
            }

			// method 2: using pattern matching
			string pattern = @"[^\u0600-\u06FFa-zA-Z0-9\s_-]";
			fileName = Regex.Replace(fileName, pattern, "");

			return fileName;
		}
    }
}
