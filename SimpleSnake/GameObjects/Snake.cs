using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char SnakeSymbol = '\u25CF';

        private readonly Queue<Point> snake;
        private readonly Food[] food;
        private readonly Wall wall;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;

        public Snake(Wall wall)
        {
            snake = new Queue<Point>();
            this.food = new Food[3];

            this.wall = wall;

            foodIndex = GetRandomPosition;

            this.GetFood();
            this.CreateSnake();

            food[foodIndex].SetRandomPosition(snake);
        }

        public int GetRandomPosition 
            => new Random().Next(0, food.Length);

        public bool IsMoving(Point direction)
        {
            Point snakeHead = this.snake.Last();

            //hitting snake
            //hitting wall
            //hitting food
            //hitting next posiotion

            GetNextDirection(direction, snakeHead);

            bool isPartOfSnake = this.snake
                .Any(x => x.LeftX == this.nextLeftX && x.TopY == nextTopY);

            if (isPartOfSnake)
            {
                //die
                return false;
            }

            var newHead = new Point(nextLeftX, nextTopY);

            bool isWall = this.wall.IsPointOfWall(
                new Point(this.nextLeftX, nextTopY));

            if (isWall)
            {
                //die
                return false;
            }

            this.snake.Enqueue(newHead);
            newHead.Draw(SnakeSymbol);

            if (this.food[foodIndex].IsFoodPoint(newHead))
            {
                //eat
                this.Eat(direction, newHead);
            }

            Point tail = this.snake.Dequeue();
            tail.Draw(' ');

            return true;
        }

        private void Eat(Point direction, Point newHead)
        {
            var foodPoints = this.food[foodIndex].FoodPoints;

            for (int i = 0; i < foodPoints; i++)
            {
                snake.Enqueue(new Point(nextLeftX, nextTopY));
                GetNextDirection(direction, newHead);
            }

            foodIndex = GetRandomPosition;
            food[foodIndex].SetRandomPosition(snake);
        }

        private void GetNextDirection(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }

        private void GetFood()
        {
            this.food[0] = new FoodAsterisk(this.wall);
            this.food[1] = new FoodDollar(this.wall);
            this.food[2] = new FoodHash(this.wall);
        }

        private void CreateSnake()
        {
            for (int leftX = 3; leftX <= 9; leftX++)
            {
                this.snake.Enqueue(new Point(leftX, 3));
            }
        }
    }
}
