using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char WallSymbol = '■';

        public Wall(int leftX, int topY) 
            : base(leftX, topY)
        {
            this.InitializeBorders();
        }

        public bool IsPointOfWall(Point snakeHead)
            => snakeHead.LeftX == 0
                || snakeHead.TopY == 0
                || snakeHead.LeftX == LeftX
                || snakeHead.TopY == TopY;

        private void InitializeBorders()
        {
            SetHorizontalBorder(0);

            SetVerticalBorder(0);

            SetVerticalBorder(LeftX - 1);

            SetHorizontalBorder(TopY);
        }

        private void SetHorizontalBorder()
        {
            for (int i = 0; i < LeftX; i++)
            {
                Console.Write(WallSymbol);
            }
        }

        private void SetHorizontalBorder(int y)
        {
            Console.SetCursorPosition(0, y);

            for (int i = 0; i < LeftX; i++)
            {
                Console.Write(WallSymbol);
            }
        }

        private void SetVerticalBorder()
        {
            for (int i = 1; i < TopY; i++)
            {
                Console.SetCursorPosition(LeftX - 1, i);
                Console.Write(WallSymbol);
            }
        }

        private void SetVerticalBorder(int x)
        {
            for (int i = 1; i < TopY; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write(WallSymbol);
            }
        }
    }
}
