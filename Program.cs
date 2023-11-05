using System;
using System.Collections.Generic;
using System.Text;

namespace Bubla
{
    public class Game
    {
        private static void Main(string[] args)
        {

            int seconds = 30;

            long time = 0;

            string text = ReadFile();
            if (text != null)
            {
                string[] timeText = text.Split('|')[1].Split('.');
                seconds = int.Parse(text.Split('|')[0]);
                time = int.Parse(timeText[2]) + int.Parse(timeText[1]) * 60 + int.Parse(timeText[0]) * 3600;
            }

            while (true)
            {
                Thread.Sleep(seconds * 1000);
                time += seconds;
                WriteFile(time, seconds);
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

        private static void WriteFile(long time,int seconds)
        {
            if (File.Exists("timeInGame.txt")) File.Delete("timeInGame.txt");

            FileStream fileScript = new FileStream("timeInGame.txt", FileMode.CreateNew);
            string textTime = $"{seconds}|{FinalTime(time)}";
            fileScript.Write(Encoding.UTF8.GetBytes(textTime), 0, textTime.Length);
            fileScript.Close();
        }

        private static string ReadFile()
        {
            if (!File.Exists("timeInGame.txt")) return null;
            string text = File.ReadAllText("timeInGame.txt");
            return text;
        }
    }
}
