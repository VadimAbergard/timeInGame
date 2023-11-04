using System;
using System.Collections.Generic;
using System.Text;

namespace Bubla
{
    public class Game
    {
        private static void Main(string[] args)
        {

            const int seconds = 30;

            long time = 0;

            string[] timeText = ReadFile();

            if(timeText != null) time = int.Parse(timeText[2]) + int.Parse(timeText[1]) * 60 + int.Parse(timeText[0]) * 3600;

            while (true)
            {
                Thread.Sleep(seconds * 1000);
                time += seconds;
                WriteFile(time);
                Console.WriteLine(time);
            }
        }

        private static string FinalTime(long time)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;

            for(; 0 < time ; time -= 1)
            {
                second++;
                if(second == 60)
                {
                    minute++;
                    second = 0;
                }
                if(minute == 60)
                {
                    hour++;
                    minute = 0;
                }
            }

            return $"{hour}.{minute}.{second}";
        }

        private static void WriteFile(long time)
        {
            if (File.Exists("timeInGame.txt")) File.Delete("timeInGame.txt");

            FileStream fileScript = new FileStream("timeInGame.txt", FileMode.CreateNew);
            string textTime = FinalTime(time);
            fileScript.Write(Encoding.UTF8.GetBytes(textTime), 0, textTime.Length);
            fileScript.Close();
        }

        private static string[] ReadFile()
        {
            if (!File.Exists("timeInGame.txt")) return null;
            string text = File.ReadAllText("timeInGame.txt");
            return text.Split('.');
        }
    }
}