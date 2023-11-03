using Core.DTOs.General;
using Core.Prodocers;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Core.Utility
{
    public static class Applications
    {
        public static bool IsValidNC(this string NC)
        {

            char[] chArray = NC.ToCharArray();
            int[] numArray = new int[chArray.Length];
            for (int i = 0; i < chArray.Length; i++)
            {
                numArray[i] = (int)char.GetNumericValue(chArray[i]);
            }
            int num2 = numArray[9];
            string[] strArray = { "0000000000", "1111111111", "22222222222", "33333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (strArray.Contains(NC))
            {
                return false;
            }
            else
            {
                int num3 = ((((((((numArray[0] * 10) + (numArray[1] * 9)) + (numArray[2] * 8)) + (numArray[3] * 7)) + (numArray[4] * 6)) + (numArray[5] * 5)) + (numArray[6] * 4)) + (numArray[7] * 3)) + (numArray[8] * 2);
                int num4 = num3 - ((num3 / 11) * 11);
                if ((((num4 == 0) && (num2 == num4)) || ((num4 == 1) && (num2 == 1))) || ((num4 > 1) && (num2 == Math.Abs((int)(num4 - 11)))))
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }
        public static bool IsValidEmail(this string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }       
        public static async Task<FileValidation> UploadedImageValidation(this IFormFile uploadImage, int validLength, string[] validExtensions)
        {
            string Messages = string.Empty;
            decimal vlen = decimal.Divide(validLength, 100);
            decimal Vlenght = vlen * 1024 * 1024;
            bool Valid = true;
            if (uploadImage == null)
            {
                Messages = "تصویری انتخاب نشده است !";
                Valid = false;
                return new FileValidation { IsValid = Valid, Message = Messages };
            }
            string Ex = Path.GetExtension(uploadImage.FileName);
            if (string.IsNullOrEmpty(Ex))
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "پسوند فایل نامشخص است !" };
            }
            if (!validExtensions.Any(a => a == Ex))
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "پسوند فایل نامعتبر است !";
                }
                else
                {
                    Messages += Environment.NewLine + "پسوند فایل نامعتبر است !";
                }
            }
            if (uploadImage.Length > Vlenght)
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }
                else
                {
                    Messages += Environment.NewLine + "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }

            }
            using (var memoryStream = new MemoryStream())
            {
                await uploadImage.CopyToAsync(memoryStream);


            }

            return new FileValidation { IsValid = Valid, Message = Messages };


        }
        public static async Task<FileValidation> UploadedImageValidation(this IFormFile uploadImage, int validLength,int validWIdth,int validHight, string[] validExtensions)
        {
            string Messages = string.Empty;
            decimal vlen = decimal.Divide(validLength, 100);
            decimal Vlenght = vlen * 1024 * 1024;
            bool Valid = true;
            if (uploadImage == null)
            {
                Messages = "تصویری انتخاب نشده است !";
                Valid = false;
                return new FileValidation { IsValid = Valid, Message = Messages };
            }
            string Ex = Path.GetExtension(uploadImage.FileName);
            if (string.IsNullOrEmpty(Ex))
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "پسوند فایل نامشخص است !" };
            }
            if (!validExtensions.Any(a => a == Ex))
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "پسوند فایل نامعتبر است !";
                }
                else
                {
                    Messages += Environment.NewLine + "پسوند فایل نامعتبر است !";
                }
            }
            if (uploadImage.Length > Vlenght)
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }
                else
                {
                    Messages += Environment.NewLine + "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }

            }

            
            Image image = Image.FromStream(uploadImage.OpenReadStream());
            
            if (image.Width > validWIdth)
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "عرض تصویر نامعتبر است !" };
            }
            if (image.Height > validHight)
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "ارتفاع تصویر نامعتبر است !" };
            }
            using (var memoryStream = new MemoryStream())
            {
                await uploadImage.CopyToAsync(memoryStream);


            }

            return new FileValidation { IsValid = Valid, Message = Messages };


        }
        public static async Task<FileValidation> UploadedImageValidationBaseOnMaxDim(this IFormFile uploadImage, int validLength,int maxWidth, int maxHeight, string[] validExtensions)
        {
            string Messages = string.Empty;
            decimal vlen = decimal.Divide(validLength, 100);
            decimal Vlenght = vlen * 1024 * 1024;
            bool Valid = true;
            if (uploadImage == null)
            {
                Messages = "تصویری انتخاب نشده است !";
                Valid = false;
                return new FileValidation { IsValid = Valid, Message = Messages };
            }
            string Ex = Path.GetExtension(uploadImage.FileName);
            if (string.IsNullOrEmpty(Ex))
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "پسوند فایل نامشخص است !" };
            }
            if (!validExtensions.Any(a => a == Ex))
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "پسوند فایل نامعتبر است !";
                }
                else
                {
                    Messages += Environment.NewLine + "پسوند فایل نامعتبر است !";
                }
            }
            if (uploadImage.Length > Vlenght)
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }
                else
                {
                    Messages += Environment.NewLine + "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }

            }


            Image image = Image.FromStream(uploadImage!.OpenReadStream());

            if (image.Width > maxWidth)
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "عرض تصویر بزرگتر از حد مجاز است !" };
            }
            if (image.Height > maxHeight)
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "ارتفاع تصویر بزرگتر از حد مجاز است !" };
            }
            using (var memoryStream = new MemoryStream())
            {
                await uploadImage.CopyToAsync(memoryStream);


            }

            return new FileValidation { IsValid = Valid, Message = Messages };


        }
        public static string SaveUploadedImage(this IFormFile uploadImage, string root, bool SetFileName)
        {
            try
            {
                string ImageName = uploadImage.FileName;
                if (SetFileName)
                {
                    ImageName = uploadImage.FileName;
                }
                else
                {
                    ImageName = Path.Combine(Generators.GenerateUniqueCode(), Path.GetExtension(uploadImage.FileName));
                }
                string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), root, ImageName);
                using (var stream = new FileStream(ImagePath, FileMode.Create))
                {
                    uploadImage.CopyTo(stream);
                }
                return ImageName;
            }
            catch (Exception)
            {

                return null;
            }
        }
        
        public static string SaveUploadedFile(this IFormFile uploadFile,string FName, string root, bool SetFileName, bool SpotSuggestionFileName)
        {
            try
            {
                string FileName = uploadFile.FileName;
                string ex = Path.GetExtension(uploadFile.FileName);
                if (SetFileName)
                {
                    FileName = uploadFile.FileName;
                }
                else
                {
                    if (SpotSuggestionFileName)
                    {
                        FileName = FName + ex;
                    }
                    else
                    {
                        string gncode = Generators.GenerateUniqueCode();                        
                        FileName = gncode + ex;
                    }
                   
                }

                string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), root, FileName);
                using (FileStream stream = new(ImagePath, FileMode.Create))
                {
                    uploadFile.CopyTo(stream);
                }
                return FileName;
            }
            catch
            {                
                return null;
            }
        }
        public static bool IsImage(this string source)
        {
            string[] extensions = { ".png", ".jpg", "jpeg", ".gif", ".png", ".bmp", ".tif", ".tiff", ".ico", ".webp", ".avif" };
            string source_ex = Path.GetExtension(source);
            return extensions.Any(a => a == source_ex.ToLower(new CultureInfo("en-us", false)));
        }
        public static bool IsVideo(this string source)
        {
            string[] extensions = { ".mp4", ".webm", ".mpeg", ".mkv", ".flv", ".mov", ".wmv", ".avi", ".mkv" };
            string source_ex = Path.GetExtension(source);
            return extensions.Any(a => a == source_ex.ToLower(new CultureInfo("en-us", false)));
        }
        public static bool IsPdf(this string source)
        {
            string[] extensions = { ".pdf" };
            string source_ex = Path.GetExtension(source);
            return extensions.Any(a => a == source_ex.ToLower(new CultureInfo("en-us", false)));
        }
        public static string GetLetterOfText(this string Text, int count)
        {
            int txtL = Text.Length;
            if (count > txtL)
            {
                return Text;
            }
            return Text[..count];
        }
        public static string GetYearType(this int Year)
        {
            if (Year >= 1900)
            {
                return "miladi";
            }
            return "shamsi";
        }
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static Guid ToGuid(long value)
        {
            byte[] guidData = new byte[16];
            Array.Copy(BitConverter.GetBytes(value), guidData, 8);
            return new Guid(guidData);
        }
        public static long ToLong(Guid guid)
        {
            if (BitConverter.ToInt64(guid.ToByteArray(), 8) != 0)
                throw new OverflowException("Value was either too large or too small for an Int64.");
            return BitConverter.ToInt64(guid.ToByteArray(), 0);
        }


        private static readonly HashSet<char> DefaultNonWordCharacters
          = new()
          { ',', '.', ':', ';' };

        /// <summary>
        /// Returns a substring from the start of <paramref name="value"/> no 
        /// longer than <paramref name="length"/>.
        /// Returning only whole words is favored over returning a string that 
        /// is exactly <paramref name="length"/> long. 
        /// </summary>
        /// <param name="value">The original string from which the substring 
        /// will be returned.</param>
        /// <param name="length">The maximum length of the substring.</param>
        /// <param name="nonWordCharacters">Characters that, while not whitespace, 
        /// are not considered part of words and therefor can be removed from a 
        /// word in the end of the returned value. 
        /// Defaults to ",", ".", ":" and ";" if null.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="length"/> is negative
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="value"/> is null
        /// </exception>
        public static string CropWholeWords(this string value,int length,HashSet<char>? nonWordCharacters = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (length < 0)
            {
                throw new ArgumentException("Negative values not allowed.", nameof(length));
            }

            nonWordCharacters ??= DefaultNonWordCharacters;

            if (length >= value.Length)
            {
                return value;
            }
            int end = length;

            for (int i = end; i > 0; i--)
            {
                if (value[i].IsWhitespace())
                {
                    break;
                }

                if (nonWordCharacters.Contains(value[i])
                    && (value.Length == i + 1 || value[i + 1] == ' '))
                {
                    //Removing a character that isn't whitespace but not part 
                    //of the word either (ie ".") given that the character is 
                    //followed by whitespace or the end of the string makes it
                    //possible to include the word, so we do that.
                    break;
                }
                end--;
            }

            if (end == 0)
            {
                //If the first word is longer than the length we favor 
                //returning it as cropped over returning nothing at all.
                end = length;
            }

            return value[..end];
        }

        private static bool IsWhitespace(this char character)
        {
            return character == ' ' || character == 'n' || character == 't';
        }

        public static class ConvertArabicNumberToEnglish
        {
            public static string ToEnglishNumber(string input)
            {
                string EnglishNumbers = "";
                for (int i = 0; i < input.Length; i++)
                {
                    if (char.IsDigit(input[i]))
                    {
                        EnglishNumbers += char.GetNumericValue(input, i);
                    }
                    else
                    {
                        EnglishNumbers += input[i].ToString();
                    }
                }
                return EnglishNumbers;
            }
        }    
        
        public static string GetMounthShamsiName(this int ShamsiMounth)
        {
            string mName = string.Empty;
            switch (ShamsiMounth)
            {
                case 1:
                    {
                        mName = "فروردین";
                        break;
                    }
                case 2:
                    {
                        mName = "اردیبهشت";
                        break;
                    }
                case 3:
                    {
                        mName = "خرداد";
                        break;
                    }
                case 4:
                    {
                        mName = "تیر";
                        break;
                    }
                case 5:
                    {
                        mName = "مرداد";
                        break;
                    }
                case 6:
                    {
                        mName = "شهریور";
                        break;
                    }
                case 7:
                    {
                        mName = "مهر";
                        break;
                    }
                case 8:
                    {
                        mName = "آبان";
                        break;
                    }
                case 9:
                    {
                        mName = "آذر";
                        break;
                    }
                case 10:
                    {
                        mName = "دی";
                        break;
                    }
                case 11:
                    {
                        mName = "بهمن";
                        break;
                    }
                case 12:
                    {
                        mName = "اسفند";
                        break;
                    }
                default:
                    break;
            }
            return mName;
        }
    }
}
