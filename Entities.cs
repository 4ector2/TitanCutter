using System;
using System.Drawing;
using System.IO;

namespace TitanCutter
{
    public class Player
    {
        public PointF Position;
        public SizeF Size = new SizeF(80, 120);
        public bool IsMovingLeft = false;
        public bool IsMovingRight = false;
        public bool IsAttacking = false;
        public int AttackTimer = 0;

        public Image[] IdleFrames;
        public Image AttackFrame;

        private int idleFrameIndex = 0;
        private int idleFrameTick = 0;

        public Player(PointF start)
        {
            Position = start;
            LoadSprites();
        }

        private void LoadSprites()
        {
            string imgDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Images");
            try
            {
                string p0 = Path.Combine(imgDir, "levi_idle_0.png");
                string p1 = Path.Combine(imgDir, "levi_idle_1.png");
                string atk = Path.Combine(imgDir, "levi_attack.png");
                IdleFrames = new Image[2];
                if (File.Exists(p0)) IdleFrames[0] = Image.FromFile(p0);
                if (File.Exists(p1)) IdleFrames[1] = Image.FromFile(p1);
                if (File.Exists(atk)) AttackFrame = Image.FromFile(atk);
            }
            catch { }
        }

        public void Update(int screenWidth)
        {
            if (IsMovingLeft) Position.X -= 6f;
            if (IsMovingRight) Position.X += 6f;
            Position.X = Math.Max(0, Math.Min(screenWidth - Size.Width, Position.X));

            if (IsAttacking)
            {
                AttackTimer++;
                if (AttackTimer > 12) { IsAttacking = false; AttackTimer = 0; }
            }
            else
            {
                idleFrameTick++;
                if (idleFrameTick > 10) { idleFrameIndex = (idleFrameIndex + 1) % Math.Max(1, IdleFrames?.Length ?? 1); idleFrameTick = 0; }
            }
        }

        public Image CurrentFrame()
        {
            if (IsAttacking && AttackFrame != null) return AttackFrame;
            if (IdleFrames != null && IdleFrames.Length > 0) return IdleFrames[idleFrameIndex];
            return null;
        }

        public RectangleF Bounds => new RectangleF(Position, Size);

        public void Draw(Graphics g)
        {
            var img = CurrentFrame();
            if (img != null) g.DrawImage(img, Position.X, Position.Y, Size.Width, Size.Height);
            else g.FillRectangle(Brushes.Blue, Bounds);
        }

        public void StartAttack() { IsAttacking = true; AttackTimer = 0; }
    }

    public class FallingObject
    {
        public PointF Position;
        public SizeF Size;
        public float Speed;
        public Image Sprite;
        public bool IsTitan;
        public bool Alive = true;

        public FallingObject(Image sprite, PointF pos, SizeF size, float speed, bool isTitan)
        {
            Sprite = sprite; Position = pos; Size = size; Speed = speed; IsTitan = isTitan;
        }

        public void Update() { Position = new PointF(Position.X, Position.Y + Speed); }

        public RectangleF Bounds => new RectangleF(Position, Size);

        public void Draw(Graphics g)
        {
            if (Sprite != null) g.DrawImage(Sprite, Position.X, Position.Y, Size.Width, Size.Height);
            else g.FillRectangle(IsTitan ? Brushes.Red : Brushes.Brown, Bounds);
        }
    }
}
