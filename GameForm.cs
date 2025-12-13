using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace TitanCutter
{
    public partial class GameForm : Form
    {
        private Timer gameTimer;
        private Timer spawnTimer;
        private Timer attackTimer;

        private Player player;
        private GameEngine engine;
        private Image background;
        private SoundPlayer music;
        private bool musicEnabled = true;
        private bool sfxEnabled = true;

        private bool spaceHeld = false;
        private bool showControls = true;

        public GameForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ClientSize = new Size(800, 600);
            LoadAssetsAndSettings();

            player = new Player(new PointF(this.ClientSize.Width / 2 - 40, this.ClientSize.Height - 140));
            engine = new GameEngine(player, sfxEnabled);

            gameTimer = new Timer { Interval = 20 };
            gameTimer.Tick += GameLoop;

            spawnTimer = new Timer { Interval = engine.SpawnIntervalMs };
            spawnTimer.Tick += SpawnLoop;

            attackTimer = new Timer { Interval = 150 };
            attackTimer.Tick += AttackLoop;

            // игра НЕ запускается, пока идет стартовый экран
            showControls = true;

            this.KeyDown += GameForm_KeyDown;
            this.KeyUp += GameForm_KeyUp;
            this.FormClosing += GameForm_FormClosing;
        }

        // ----------------- НАЧАЛО ИГРЫ ПО КЛАВИШЕ -----------------

        private void StartGameAfterControls()
        {
            if (!showControls) return;

            showControls = false;

            gameTimer.Start();
            spawnTimer.Start();

            if (musicEnabled) TryPlayMusicLoop();
        }

        // ----------------- АТАКА ПРИ УДЕРЖАНИИ -----------------

        private void AttackLoop(object sender, EventArgs e)
        {
            if (spaceHeld)
                engine.PlayerAttack();
        }

        // ----------------- ЗАГРУЗКА -----------------

        private void LoadAssetsAndSettings()
        {
            string imgDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Images");
            string sndDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Sounds");

            if (File.Exists(Path.Combine(imgDir, "background.png")))
                background = Image.FromFile(Path.Combine(imgDir, "background.png"));

            var musicPath = Path.Combine(sndDir, "music_loop.wav");
            if (File.Exists(musicPath)) music = new SoundPlayer(musicPath);

            string settingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Data", "settings.txt");
            if (File.Exists(settingsFile))
            {
                try
                {
                    var lines = File.ReadAllLines(settingsFile);
                    foreach (var l in lines)
                    {
                        var parts = l.Split('=');
                        if (parts.Length != 2) continue;

                        var key = parts[0].Trim();
                        var val = parts[1].Trim();

                        if (key == "Music") musicEnabled = val == "1";
                        if (key == "SFX") sfxEnabled = val == "1";

                        if (key == "Difficulty" &&
                            int.TryParse(val, out int d))
                        {
                            GameEngine.GlobalDifficulty = Math.Max(0, Math.Min(2, d));
                        }
                    }
                }
                catch { }
            }
        }

        private void TryPlayMusicLoop()
        {
            try { music?.PlayLooping(); } catch { }
        }

        private void StopMusic()
        {
            try { music?.Stop(); } catch { }
        }

        // ----------------- GAME LOOP -----------------

        private void SpawnLoop(object sender, EventArgs e)
        {
            if (!engine.Paused)
                engine.SpawnOne();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!engine.Paused)
            {
                engine.Update();
                if (spaceHeld)
                    engine.PlayerAttack();
                Invalidate();
            }
            else
            {
                Invalidate();
            }

            if (engine.GameOver)
            {
                gameTimer.Stop();
                spawnTimer.Stop();
                attackTimer.Stop();

                StopMusic();

                System.Media.SystemSounds.Hand.Play();

                using (var nameForm = new NameEntryForm())
                {
                    if (nameForm.ShowDialog(this) == DialogResult.OK)
                    {
                        var nm = string.IsNullOrWhiteSpace(nameForm.PlayerName)
                            ? "Player"
                            : nameForm.PlayerName;

                        HighScoresManager.SaveEntry(nm, engine.Score);
                    }
                }

                this.Close();
            }
        }

        // ----------------- РЕНДЕР -----------------

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            // стартовый экран
            if (showControls)
            {
                g.Clear(Color.Black);

                string txt =
                    "УПРАВЛЕНИЕ:\n\n" +
                    "← / A — идти влево\n" +
                    "→ / D — идти вправо\n" +
                    "SPACE — атака\n" +
                    "ESC — пауза\n\n" +
                    "Нажмите ЛЮБУЮ клавишу для начала игры";

                var f = new Font("Segoe UI", 20, FontStyle.Bold);
                var size = g.MeasureString(txt, f);

                g.DrawString(txt, f, Brushes.White,
                    (Width - size.Width) / 2,
                    (Height - size.Height) / 2);

                return;
            }

            // обычный рендер игры
            if (background != null)
                g.DrawImage(background, 0, 0, Width, Height);
            else
                g.Clear(Color.DarkSlateGray);

            engine.Render(g);

            var font = new Font("Segoe UI", 12, FontStyle.Bold);
            g.DrawString($"Time: {engine.ElapsedSeconds}s", font, Brushes.White, 10, 10);
            g.DrawString($"Killed: {engine.KilledTitans}", font, Brushes.White, 10, 32);
            g.DrawString($"Score: {engine.Score}", font, Brushes.White, 10, 54);

            if (engine.Paused)
            {
                var f = new Font("Segoe UI", 36, FontStyle.Bold);
                var sz = g.MeasureString("PAUSED", f);
                g.DrawString("PAUSED", f, Brushes.Yellow,
                    (Width - sz.Width) / 2,
                    (Height - sz.Height) / 2);
            }
        }

        // ----------------- INPUT -----------------

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (showControls)
            {
                StartGameAfterControls();
                return;
            }

            if (e.KeyCode == Keys.Escape)
            {
                engine.Paused = !engine.Paused;
                if (engine.Paused) StopMusic();
                else if (musicEnabled) TryPlayMusicLoop();
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                player.IsMovingLeft = true;

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                player.IsMovingRight = true;

            if (e.KeyCode == Keys.Space)
            {
                if (!spaceHeld)
                {
                    spaceHeld = true;
                    engine.PlayerAttack();
                    attackTimer.Start();
                }
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                player.IsMovingLeft = false;

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                player.IsMovingRight = false;

            if (e.KeyCode == Keys.Space)
            {
                spaceHeld = false;
                attackTimer.Stop();
            }
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopMusic();
        }
    }
}
