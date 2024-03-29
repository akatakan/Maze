﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    //Camel Case stilini kullanarak kodladım
    public class Maze
    {
        private const char SEPARATOR = ';';
        private const char WALL = '0';

        private readonly int left;
        private readonly int top;
        private readonly bool[,] fields;

        private int Width
        {
            get { return this.fields.GetLength(1); }
        }

        private int Height
        {
            get { return this.fields.GetLength(0); }
        }

        public int Left
        {
            get { return this.left; }
        }

        public int Top
        {
            get { return this.top; }
        }

        private Maze(int left, int top, int width, int height)
        {
            this.left = left;
            this.top = top;
            this.fields = new bool[height, width];
        }

        private void SetWall(int row, int column)
        {
            this.fields[row, column] = true;
        }

        private void SetWallForExit(int row, int column)
        {
            this.fields[row, column] = true;
        }
        //labirent yol ve duvar oluşumu
        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int row = 0; row < this.Height; row++)
            {
                Console.SetCursorPosition(this.left, this.top + row);

                for (int column = 0; column < this.Width; column++)
                {
                    Console.Write(this.IsWall(row, column) ? WALL : '1');
                }
            }
        }
        //duvar kontrolü
        public bool IsWall(int row, int column)
        {
            bool valid = this.isValid(row, column);
            return !valid || (valid && this.fields[row, column]);
        }

        public bool IsValidCheck(int row, int column)
        {
            return row >= 0 && row < this.fields.GetLength(0) && column >= 0 && column < this.fields.GetLength(1);
        }
        //satır sütün uygunluğu
        private bool isValid(int row, int column)
        {
            return this.IsValidRow(row) && this.IsValidColumn(column);
        }
        //satır uygunluğu
        private bool IsValidRow(int row)
        {
            return row >= 0 && row < this.Height;
        }

        //sütun uygunluğu
        private bool IsValidColumn(int column)
        {
            return column >= 0 && column < this.Width;
        }

        public Player createPlayer(Random random, ConsoleColor background)
        {
            int row = 0;
            int column = 0;
            do
            {
                Console.WriteLine("3 baslangic noktasindan birini seciniz= ");
                int n = Convert.ToInt32(Console.ReadLine());
                if(n==1)
                {
                    row = 9;
                    column = 2;
                }
                if(n==2)
                {
                    row = 9;
                    column = 5;
                }
                if(n==3)
                {
                    row = 9;
                    column = 8;
                }
                
            } while (IsWall(row, column) );
            return new Player(background, row, column);
        }

        public static Maze Load(int left, int top, String fileName)
        {
            StreamReader reader = new StreamReader(File.Open(fileName, FileMode.Open));
            Maze maze = createMaze(left, top, reader.ReadLine());
            String line = "";

            int row = 0;
            while ((line = reader.ReadLine()) != null)
            {
                for (int column = 0; column < line.Length; column++)
                {
                    if (line[column] == WALL)
                    {
                        maze.SetWall(row, column);
                    }
                }
                row++;
            }
            reader.Close();
            return maze;
        }

        private static Maze createMaze(int left, int top, String coordinateLine)
        {
            String[] coords = coordinateLine.Split(SEPARATOR);
            return new Maze(left, top, Int32.Parse(coords[0]), Int32.Parse(coords[1]));
        }
        public Bomba createBomba(Random random, ConsoleColor background)
        {
            int row = 0;
            int column = 0;
                do
                {
                    row = random.Next(this.Height);
                    column = random.Next(this.Width);
                } while (IsWall(row, column));           
            return new Bomba(background, row, column);
        }
        public Bomba2 createBomba2(Random random, ConsoleColor background)
        {
            int row = 0;
            int column = 0;
            do
            {
                row = random.Next(this.Height);
                column = random.Next(this.Width);
            } while (IsWall(row, column ));
            return new Bomba2(background, row, column);
        }           
    }
}
