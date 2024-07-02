namespace Logger
{
    public class KeyHandler
    {
        public string GetKeyName(Keys key)
        {
            InputLanguage currentLanguage = InputLanguage.CurrentInputLanguage;

            if (currentLanguage.Culture.Equals(new System.Globalization.CultureInfo("en-US")))
            {
                return GetKeyNameEnglish(key);
            }
            else if (currentLanguage.Culture.Equals(new System.Globalization.CultureInfo("ru-RU")))
            {
                return GetKeyNameRussian(key);
            }
            else
            {
                return GetCommonKeyName(key);
            }
        }

        private static string GetKeyNameEnglish(Keys key)
        {
            switch (key)
            {
                case Keys.A: return "A";
                case Keys.B: return "B";
                case Keys.C: return "C";
                case Keys.D: return "D";
                case Keys.E: return "E";
                case Keys.F: return "F";
                case Keys.G: return "G";
                case Keys.H: return "H";
                case Keys.I: return "I";
                case Keys.J: return "J";
                case Keys.K: return "K";
                case Keys.L: return "L";
                case Keys.M: return "M";
                case Keys.N: return "N";
                case Keys.O: return "O";
                case Keys.P: return "P";
                case Keys.Q: return "Q";
                case Keys.R: return "R";
                case Keys.S: return "S";
                case Keys.T: return "T";
                case Keys.U: return "U";
                case Keys.V: return "V";
                case Keys.W: return "W";
                case Keys.X: return "X";
                case Keys.Y: return "Y";
                case Keys.Z: return "Z";
                default: return GetCommonKeyName(key);
            }
        }

        private static string GetKeyNameRussian(Keys key)
        {
            switch (key)
            {
                case Keys.Oem3: return "Ё";
                case Keys.Q: return "Й";
                case Keys.W: return "Ц";
                case Keys.E: return "У";
                case Keys.R: return "К";
                case Keys.T: return "Е";
                case Keys.Y: return "Н";
                case Keys.U: return "Г";
                case Keys.I: return "Ш";
                case Keys.O: return "Щ";
                case Keys.P: return "З";
                case Keys.OemOpenBrackets: return "Х";
                case Keys.OemCloseBrackets: return "Ъ";
                case Keys.A: return "Ф";
                case Keys.S: return "Ы";
                case Keys.D: return "В";
                case Keys.F: return "А";
                case Keys.G: return "П";
                case Keys.H: return "Р";
                case Keys.J: return "О";
                case Keys.K: return "Л";
                case Keys.L: return "Д";
                case Keys.Oem1: return "Ж";
                case Keys.Oem7: return "Э";
                case Keys.Z: return "Я";
                case Keys.X: return "Ч";
                case Keys.C: return "С";
                case Keys.V: return "М";
                case Keys.B: return "И";
                case Keys.N: return "Т";
                case Keys.M: return "Ь";
                case Keys.Oemcomma: return "Б";
                case Keys.OemPeriod: return "Ю";
                default: return GetCommonKeyName(key);
            }
        }

        private static string GetCommonKeyName(Keys key)
        {
            switch (key)
            {
                case Keys.D0: return "0";
                case Keys.D1: return "1";
                case Keys.D2: return "2";
                case Keys.D3: return "3";
                case Keys.D4: return "4";
                case Keys.D5: return "5";
                case Keys.D6: return "6";
                case Keys.D7: return "7";
                case Keys.D8: return "8";
                case Keys.D9: return "9";
                case Keys.NumPad0: return "0";
                case Keys.NumPad1: return "1";
                case Keys.NumPad2: return "2";
                case Keys.NumPad3: return "3";
                case Keys.NumPad4: return "4";
                case Keys.NumPad5: return "5";
                case Keys.NumPad6: return "6";
                case Keys.NumPad7: return "7";
                case Keys.NumPad8: return "8";
                case Keys.NumPad9: return "9";
                case Keys.Space: return " ";
                case Keys.Enter: return "\n";
                case Keys.Tab: return "\t";
                case Keys.Oemtilde: return "~";
                case Keys.OemMinus: return "-";
                case Keys.Oemplus: return "+";
                case Keys.OemOpenBrackets: return "[";
                case Keys.OemCloseBrackets: return "]";
                case Keys.Oemcomma: return ",";
                case Keys.OemPeriod: return ".";
                case Keys.OemQuestion: return "?";
                case Keys.OemQuotes: return "\"";
                case Keys.OemBackslash: return "\\";
                case Keys.OemPipe: return "|";
                case Keys.OemSemicolon: return ";";
                case Keys.Delete: return " Delete";
                case Keys.Insert: return " Insert";
                case Keys.Home: return " Home";
                case Keys.End: return " End";
                case Keys.PageUp: return " PageUp";
                case Keys.PageDown: return " PageDown";
                case Keys.Up: return " UpArrow";
                case Keys.Down: return " DownArrow";
                case Keys.Left: return " LeftArrow";
                case Keys.Right: return " RightArrow";
                case Keys.Escape: return " Escape";
                case Keys.Shift: return " Shift";
                case Keys.LShiftKey: return " LShift";
                case Keys.RShiftKey: return " RShift";
                case Keys.Control: return " Control";
                case Keys.LControlKey: return " LControl";
                case Keys.RControlKey: return " RControl";
                case Keys.Alt: return " Alt";
                default: return key.ToString();
            }
        }
    }
}