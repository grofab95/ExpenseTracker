using Colorful;
using System;
using System.Drawing;
using System.IO;
using Console = Colorful.Console;

namespace ExpenseTracker.Common
{
    public enum LogLevel
    {
        INFO,
        ERROR
    }

    public class Logger
    {
        private static readonly string _folderPath = $"{Directory.GetCurrentDirectory()}\\logs";

        public static void Log(string source, LogLevel logLevel = LogLevel.INFO)
        {
            var actualDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            var message = $"{actualDate} | {logLevel} -> {source}";

            WriteToConsole(message, logLevel);
            WriteToFile(message);
        }

        public static void Log<T>(string source, LogLevel logLevel = LogLevel.INFO)
        {
            var actualDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            var message = $"{actualDate} | {logLevel} ({typeof(T).Name}) ->  {source}";

            var coloredConsole = !(typeof(T).Name.Contains("Component", StringComparison.InvariantCultureIgnoreCase));

            WriteToConsole(message, logLevel, coloredConsole);
            WriteToFile(message);
        }

        public static void Log(Exception exception, bool fullException = false)
        {
            var actualDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            var message = string.Empty;

            if (fullException)
            {
                message = $"{actualDate} | {LogLevel.ERROR} -> {Environment.NewLine}          {exception.ToString()}";
            }
            else
            {
                message = $"{actualDate} | {LogLevel.ERROR} -> {exception.Message}";
            }


            WriteToConsole(message, LogLevel.ERROR);
            WriteToFile(message);
        }

        public static void Log<T>(Exception exception, bool fullException = false)
        {
            var actualDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            var message = string.Empty;

            if (fullException)
            {
                message = $"{actualDate} | {LogLevel.ERROR} ({typeof(T).Name}) -> {Environment.NewLine}          {exception.ToString()}";
            }
            else
            {
                message = $"{actualDate} | {LogLevel.ERROR} ({typeof(T).Name}) -> {exception.Message}";
            }

            var coloredConsole = !(typeof(T).Name.Contains("Component", StringComparison.InvariantCultureIgnoreCase));

            WriteToConsole(message, LogLevel.ERROR, coloredConsole);
            WriteToFile(message);
        }

        private static void WriteToConsole(string message, LogLevel logLevel, bool coloredConsole = true)
        {
            var styleSheet = new StyleSheet(Color.White);

            var color = Color.White;
            
            switch (logLevel)
            {
                case LogLevel.INFO:
                    color = Color.Green;
                    break;

                case LogLevel.ERROR:
                    color = Color.Red;
                    break;

                default:
                    break;
            }

            var datePart = GetDatePart(message);
            var classPart = GetMessagePart(message, '(', ')');

            if (!string.IsNullOrEmpty(classPart))
            {
                styleSheet.AddStyle(classPart, Color.SkyBlue);
            }

            styleSheet.AddStyle(logLevel.ToString(), color);
            styleSheet.AddStyle(datePart, Color.Yellow);
            styleSheet.AddStyle("->", Color.Yellow);

            if (coloredConsole)
            {
                Console.WriteLineStyled(message, styleSheet);
            }
            else
            {
                System.Console.WriteLine(message);
            }
        }

        private static void WriteToFile(string message)
        {
            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);
           
            var fileName = GetFileName();
            var filePath = $"{_folderPath}\\{fileName}";

            using (StreamWriter stream = File.AppendText(filePath))
            {
                stream.WriteLine(message);
            }
        }

        private static string GetFileName()
        {
            return $"log_{DateTime.Now.ToString("ddMMyyyy")}.txt";
        }

        private static string GetDatePart(string message)
        {
            var block = string.Empty;

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == '|')
                {
                    block = message.Substring(0, i - 1);
                    return block;
                }
            }

            return block;
        }

        private static string GetMessagePart(string message, char firstChar, char lastChar)
        {
            var start = false;
            var block = string.Empty;

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == firstChar)
                {
                    i++;
                    start = true;
                }

                if (message[i] == lastChar)
                    return block;

                if (start)
                    block += message[i];
            }

            return block;
        }
    }
}
