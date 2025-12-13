using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;

namespace TitanCutter
{
    public class GameEngine
    {
        public static int GlobalDifficulty = 1; // 0 easy,1 normal,2 hard

        public int SpawnIntervalMs => (GlobalDifficulty == 0) ? 1100 : (GlobalDifficulty == 1) ? 800 : 550;

        private Player player;
        private List<FallingObject> objects = new List<FallingObject>();
        private Random rng = new Random();
        private int screenWidth = 800;
        private int screenHeight = 600;

        private SoundPlayer sliceSound;
        private SoundPlayer hitSound;
        private SoundPlayer gameoverSound;

        public bool Paused = false;
        public bool GameOver = false;
        public int KilledTitans = 0;
        public int Score = 0;
        private DateTime startTime = DateTime.Now;
        public int ElapsedSeconds => (int)(DateTime.Now - startTime).TotalSeconds;

        private bool sfxEnabledLocal = true;

        public GameEngine(Player player, bool sfxEnabled)
        {
            this.player = player;
            this.sfxEnabledLocal = sfxEnabled;
            LoadSounds();
            startTime = DateTime.Now;
        }

        private void LoadSounds()
        {
            string snd = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Sounds");
            string s1 = Path.Combine(snd, "slice.wav");
            string s2 = Path.Combine(snd, "hit.wav");
            string s3 = Path.Combine(snd, "gameover.wav");
            if (File.Exists(s1)) sliceSound = new SoundPlayer(s1);
            if (File.Exists(s2)) hitSound = new SoundPlayer(s2);
            if (File.Exists(s3)) gameoverSound = new SoundPlayer(s3);
        }

        public void Update()
        {
            if (Paused || GameOver) return;

            player.Update(screenWidth);
            // update objects
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                objects[i].Update();
                if (objects[i].Position.Y > screenHeight + 50) objects.RemoveAt(i);
            }

            // check collisions
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                var o = objects[i];
                if (o.Bounds.IntersectsWith(player.Bounds))
                {
                    if (o.IsTitan)
                    {
                        if (player.IsAttacking)
                        {
                            // titan killed
                            PlaySlice();
                            objects.RemoveAt(i);
                            KilledTitans++;
                            Score += 100;
                        }
                        else
                        {
                            // player hit by titan => game over
                            PlayHit();
                            EndGame();
                            return;
                        }
                    }
                    else
                    {
                        // debris always hurts
                        PlayHit();
                        EndGame();
                        return;
                    }
                }
            }
        }

        public void SpawnOne()
        {
            if (Paused || GameOver) return;

            string imgDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Images");
            bool spawnTitan = rng.NextDouble() < 0.7;
            int x = rng.Next(20, screenWidth - 80);
            if (spawnTitan)
            {
                // choose titan sprite
                Image sprite = null;
                string t0 = Path.Combine(imgDir, "titan_0.png");
                string t1 = Path.Combine(imgDir, "titan_1.png");
                if (File.Exists(t0) && rng.Next(2) == 0) sprite = Image.FromFile(t0);
                else if (File.Exists(t1)) sprite = Image.FromFile(t1);

                var size = new SizeF(60 + rng.Next(0, 30), 80 + rng.Next(0, 40));
                float speed = 2.0f + (float)rng.NextDouble() * (GlobalDifficulty + 1.5f);
                objects.Add(new FallingObject(sprite, new PointF(x, -size.Height), size, speed, true));
            }
            else
            {
                Image sprite = null;
                string d0 = Path.Combine(imgDir, "debris_0.png");
                if (File.Exists(d0)) sprite = Image.FromFile(d0);
                var size = new SizeF(30 + rng.Next(0, 50), 20 + rng.Next(0, 30));
                float speed = 3.0f + (float)rng.NextDouble() * (GlobalDifficulty + 2.0f);
                objects.Add(new FallingObject(sprite, new PointF(x, -size.Height), size, speed, false));
            }
        }

        public void Render(Graphics g)
        {
            // draw falling objects
            foreach (var o in objects) o.Draw(g);
            // draw player
            player.Draw(g);
        }

        private void PlaySlice()
        {
            if (!sfxEnabledLocal) return;
            try { sliceSound?.Play(); } catch { }
        }

        private void PlayHit()
        {
            if (!sfxEnabledLocal) return;
            try { hitSound?.Play(); } catch { }
        }

        private void EndGame()
        {
            GameOver = true;
            try { gameoverSound?.Play(); } catch { }
        }

        public void PlayerAttack()
        {
            if (Paused || GameOver) return;
            if (!player.IsAttacking)
            {
                player.StartAttack();
            }
        }
    }
}
