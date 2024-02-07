using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public abstract class Food : Point
    {
        private readonly char foodSymbol;
        private readonly Random random;
        private readonly Wall wall;

        protected Food(Wall wall, char foodSymbol, int points) 
            : base(0, 0)
        {
            this.wall = wall;
            this.foodSymbol = foodSymbol;
            this.FoodPoints = points;

            this.random = new Random();
        }

        public int FoodPoints { get; private set; }

        public bool IsFoodPoint(Point snakeHead)
            => LeftX == snakeHead.LeftX && TopY == snakeHead.TopY;
        

        public void SetRandomPosition(Queue<Point> snake)
        {
            this.LeftX = this.random.Next(2, wall.LeftX - 2);
            this.TopY = this.random.Next(2, wall.TopY -2);

            bool isPartOfSnake = snake
                .Any(x => x.TopY == this.TopY && x.LeftX == this.LeftX);

            while (isPartOfSnake)
            {
                this.LeftX = this.random.Next(2, wall.LeftX - 2);
                this.TopY = this.random.Next(2, wall.TopY - 2);

                isPartOfSnake = snake
                    .Any(x => x.TopY == this.TopY && x.LeftX == this.LeftX);
            }

            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(LeftX, TopY);
            Console.Write(foodSymbol);

            this.Draw(this.foodSymbol);

            Console.BackgroundColor = ConsoleColor.White;
        }
    }
}
