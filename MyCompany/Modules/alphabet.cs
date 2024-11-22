static class alphabet
{
    // Lưu ý chỉ cần sửa IP chỗ này thành tên SQL Server
    public class Alphabet_list
    {
        public string[] Alphabet_array = new[] { "a", "ă", "â", "b", "c", "d", "đ", "e", "ê", "g", "h", "i", "k", "l", "m", "n", "o", "ô", "ơ", "p", "q", "r", "s", "t", "u", "ư", "v", "x", "y", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    }

    public class SurroundingClass
    {
        private static readonly string[] VietNamChar = new string[] { "aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ" };
        private static readonly string[,] window_special_Char = new string[,] { { @"\", "@@@1" }, { "/", "@@@2" }, { ":", "@@@3" }, { "*", "@@@4" }, { "?", "@@@5" }, { "\"", "@@@6" }, { "<", "@@@7" }, { ">", "@@@8" }, { "|", "@@@9" } };
        private static readonly string[,] url_special_Char = new string[,] { { @"\", "@@@1" }, { "/", "@@@2" }, { ":", "@@@3" }, { "*", "@@@4" }, { "?", "@@@5" }, { "\"", "@@@6" }, { "<", "@@@7" }, { ">", "@@@8" }, { "|", "@@@9" }, { ".", "@@10" }, { ",", "@@11" }, { "~", "@@12" } };

        public static string LocDau(string str)
        {
            for (int i = 1; i <= VietNamChar.Length - 1; i++)
            {
                for (int j = 0; j <= VietNamChar[i].Length - 1; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }

            return str;
        }

        public static string encode_window_special_char(string str)
        {
            for (var i = 0; i <= window_special_Char.GetUpperBound(0); i++)
            {
                if (str.Contains(window_special_Char[i, 0]) == true)
                    str = str.Replace(window_special_Char[i, 0], window_special_Char[i, 1]);
            }
            return str;
        }

        public static string decode_window_special_char(string str)
        {
            for (var i = 0; i <= window_special_Char.GetUpperBound(0); i++)
            {
                if (str.Contains(window_special_Char[i, 1]) == true)
                    str = str.Replace(window_special_Char[i, 1], window_special_Char[i, 0]);
            }
            return str;
        }

        public string returnString_fromBoolen(Boolean invalue)
        {
            if (invalue == true)
                return "1";
            else if (invalue == false)
                return "0";
            else
                return "";
        }
    }
    public static string DecodeBase64(string input)
    {
        if (input != "" & input != null)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }
        return "";
    }
    public static string EncodeBase64(string input)
    {
        if (input != "" & input != null)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input));
        }
        return "";
    }
}
