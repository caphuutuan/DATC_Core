using DATC_Core.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace DATC_Core.Helper
{
    public static class Utilities
    {
        public static int PAGE_SIZE = 20;
        public static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
            {
                Directory.CreateDirectory(path);
            }
        }
        public static string ToTitleCase(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join(" ", words);
            }
            return string.Join(" ", result);
        }
        public static bool IsInteger(string str)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
            }
            catch
            {

            }
            return true;
        }
        public static string GetRandomKey(int length = 5)
        {
            string pattern = @"123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }

        public static string SEOUrl(this string url)
        {
            url = url.ToLower().Trim();
            url = Regex.Replace(url, @"[áàảãạăắằẳẵặâấầẩẫậÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ]", "a");
            url = Regex.Replace(url, @"[éèẻẽẹêếềểễệÉÈẺẼẸÊẾỀỂỄỆ]", "e");
            url = Regex.Replace(url, @"[óòỏõọôốồổỗộơớờởỡợÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ]", "o");
            url = Regex.Replace(url, @"[úùủũụưứừửữựÚÙỦŨỤƯỨỪỬỮỰ]", "u");
            url = Regex.Replace(url, @"[ýỳỷỹỵÝỲỶỸỴ]", "y");
            url = Regex.Replace(url, @"[íìỉĩịÍÌỈĨỊ]", "i");
            url = Regex.Replace(url, @"[đĐ]", "d");
            url = Regex.Replace(url, @"[\s]", "-");
            url = Regex.Replace(url, @"[,]", "");
            url = Regex.Replace(url, @"[[]]", "");
            url = Regex.Replace(url, @"[^a-zA-Z0-9-\s]", "");
            url = Regex.Replace(url, @"[(-)+]", "");

            while (true)
            {
                if (url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;
        }
        public static async Task<string> UploadFile(IFormFile file, string sDirectory, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory, newname);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower())) /// Khác các file định nghĩa
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch
            {
                return null;
            }
        }
        public static void DeleteFile(string sDirectory, string fileName)
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory, fileName);

                // Kiểm tra xem tệp tin có tồn tại không trước khi xoá
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi trong quá trình xoá tệp tin
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
        }
    }
}
