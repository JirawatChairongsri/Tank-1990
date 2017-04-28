﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanks
{
    public class GameModel
    {
        public bool gameOver = false;
        public Tank player = new PlayerTank();
        public List<Bullet> bullets = new List<Bullet>();
        public List<Tank> bot;

        public GameModel()
        { }


        public void Move(Tank tank, Direction direction) // перемещение танка пользователя
        {
            int dX = tank.shift[direction].Key; // смещение по x
            int dY = tank.shift[direction].Value; // смещение по y

            if (tank.direction == direction)
            {
                tank.point = new Point((int)(tank.point.X + dX), (int)(tank.point.Y + dY));
            }

            else
            {
                RotateImage(tank, direction); // поворачиваем картинку в нужном направлении
                tank.direction = direction;
            }
        }

        public void Shoot(Tank tank)
        {
            if (!tank.isShooting)
            {
                bullets.Add(new Bullet(tank));
                tank.isShooting = true;
            }
        }

        public void MoveBullet()
        {
            List<int> index = new List<int>();
            int i = 0;
            foreach(var bullet in bullets)
            {
                int dX = bullet.shift[bullet.direction].Key; // смещение по x
                int dY = bullet.shift[bullet.direction].Value; // смещение по y
                bullet.middle = new Point(bullet.middle.X + dX, bullet.middle.Y + dY);
                bullet.point = new Point(bullet.point.X + dX, bullet.point.Y + dY);

                if (bullet.middle.X <= 100 || bullet.middle.X >= 1000 ||
                    bullet.middle.Y <= 100 || bullet.middle.Y >= 1000)
                {
                    bullet.tank.isShooting = false;
                    index.Add(i);
                }
                i++;
            }

            foreach (var j in index)
            {
                bullets.RemoveAt(j);
            }
        }

        private static void RotateImage(Tank tank, Direction newDirection) // поворачиваем картинку на нужный угол
        {
            int angle = tank.windRose[newDirection] - tank.windRose[tank.direction];
            angle = angle < 0 ? angle + 360 : angle;

            switch (angle)
            {
                case 270:
                    tank.img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case 180:
                    tank.img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 90:
                    tank.img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
            }
        }
    }
}
