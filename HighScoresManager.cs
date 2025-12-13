using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TitanCutter
{
    public static class HighScoresManager
    {
        private static readonly string folder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Data");

        private static readonly string file =
            Path.Combine(folder, "highscores.txt");

        // ------------------------
        // ЗАГРУЗКА РЕКОРДОВ
        // ------------------------
        public static List<ScoreEntry> LoadEntries()
        {
            var list = new List<ScoreEntry>();

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (!File.Exists(file))
                return list;

            foreach (var line in File.ReadAllLines(file))
            {
                var parts = line.Split('|');
                if (parts.Length != 3) continue;

                if (!int.TryParse(parts[1], out int score)) continue;
                if (!DateTime.TryParse(parts[2], out DateTime date))
                    date = DateTime.Now;

                list.Add(new ScoreEntry
                {
                    Name = parts[0],
                    Score = score,
                    Date = date
                });
            }

            // сортируем и оставляем топ-10
            return list
                .OrderByDescending(x => x.Score)
                .Take(10)
                .ToList();
        }

        // ------------------------
        // СОХРАНЕНИЕ НОВОГО РЕКОРДА
        // ------------------------
        public static void SaveEntry(string name, int score)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            List<ScoreEntry> entries = LoadEntries();

            entries.Add(new ScoreEntry
            {
                Name = name,
                Score = score,
                Date = DateTime.Now
            });

            // оставляем топ-10
            entries = entries
                .OrderByDescending(x => x.Score)
                .Take(10)
                .ToList();

            // записываем
            var lines = entries.Select(e =>
                $"{e.Name}|{e.Score}|{e.Date:g}");

            File.WriteAllLines(file, lines);
        }
    }
}
