namespace Life.Ecosystems
{
    using System;
    using System.Windows.Media.Media3D;
    using System.Windows.Threading;
    using Life.Generic;

    public class Behaviour
    {
        public bool Alive = true;
        public double JumpHeight = 5;
        /// <summary>
        /// Max Speed is 20
        /// </summary>
        public float Speed = 5;
        public bool LosesSpeedWhenHungry = true;

        DispatcherTimer NextJumpTimer;

        public void Walk(TranslateTransform3D position, Random rand)
        {
            if (Speed > 20) // Speed Cap
                Speed = 20;

            NextJumpTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 2000)
            };

            DispatcherTimer OffsetTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, rand.Next(0, 3000)),
            };
            OffsetTimer.Start();

            OffsetTimer.Tick += OffsetTimer_Tick;
            void OffsetTimer_Tick(object sender, EventArgs e)
            {
                NextJumpTimer.Start();
                OffsetTimer.Stop();
            }

            NextJumpTimer.Tick += Timer_Tick;
            void Timer_Tick(object sender, EventArgs e)
            {
                if (Alive)
                {
                    double x = rand.NextDouble();
                    double z = rand.NextDouble();
                    x = x * 2 - 1;
                    z = z * 2 - 1;
                    JumpTo(position, new Point3D(x * 100, 0, z * 100), JumpHeight);
                }
                else
                {
                    NextJumpTimer.Stop();
                }
            }
        }

        private void JumpTo(TranslateTransform3D from, Point3D to, double height)
        {
            int time = 0;

            DispatcherTimer JumpAnimation = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 20)
            };

            JumpAnimation.Tick += Timer_Tick;
            JumpAnimation.Start();

            void Timer_Tick(object sender, EventArgs e)
            {
                if (time++ <= 10)
                {
                    if (from.OffsetX < to.X)
                        from.OffsetX += 1;
                    else if (from.OffsetX > to.X)
                        from.OffsetX -= 1;

                    if (from.OffsetZ < to.Z)
                        from.OffsetZ += 1;
                    else if (from.OffsetZ > to.Z)
                        from.OffsetZ -= 1;

                    double jumpHeight = height / 5;
                    if (time <= 5)
                        from.OffsetY += jumpHeight;
                    else if (time > 5 && time <= 10)
                        from.OffsetY -= jumpHeight;
                }
                else
                {
                    JumpAnimation.Stop();
                }
            }
        }

        public void Hunger(Bunny bunny)
        {
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, bunny.StarvationTime, 0)
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            void Timer_Tick(object sender, EventArgs e)
            {
                if (bunny.Food >= 1)
                {
                    bunny.Food -= 1;
                    if (LosesSpeedWhenHungry && Speed > 2)
                    {
                        Speed -= Speed / 10;
                        NextJumpTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(2000 - (900 - Speed * 100)));
                    }
                }
                else
                {
                    timer.Stop();
                    Simulation.Remove(bunny);
                }
            }
        }
    }
}