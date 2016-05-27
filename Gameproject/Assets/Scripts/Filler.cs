﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Board
{
    public class Filler
    {
        private int[,] cells;
        private int width;
        private int height;
        private List<List<Celldata>> groups;

        private int latestGroupID = 10;

        public Filler(int[,] cells)
        {
            width = cells.GetLength(0);
            height = cells.GetLength(1);
            this.cells = (int[,])cells.Clone();

            groups = new List<List<Celldata>>();
        }

        public struct Celldata
        {
            public int x;
            public int y;
            public int group;
            public Celldata(int x, int y, int group)
            {
                this.x = x;
                this.y = y;
                this.group = group;
            }
        }

        public List<List<Celldata>> GetGroups()
        {
            return groups;
        }

        public void Fill(int startX, int startY)
        {
            fill(startX, startY);
        }

        private void fill(int cellX, int cellY, int groupID = 1, List<Celldata> group = null)
        {

            if (cellY < 0 || cellX < 0 || cellY >= height || cellX >= width)
                return;

            cells[cellX, cellY] = groupID;

            if (group != null)
                group.Add(new Celldata(cellX, cellY, groupID));


            int bottom = getCell(cellX, cellY - 1);
            if (bottom == 0)
            {
                fillCell(cellX, cellY - 1, groupID, group);
            }

            int top = getCell(cellX, cellY + 1);
            if (top == 0)
            {
                fillCell(cellX, cellY + 1, groupID, group);
            }

            int right = getCell(cellX + 1, cellY);
            if (right == 0)
            {
                fillCell(cellX + 1, cellY, groupID, group);
            }

            int left = getCell(cellX - 1, cellY);
            if (left == 0)
            {
                fillCell(cellX - 1, cellY, groupID, group);
            }
        }

        private void fillCell(int x, int y, int groupID, List<Celldata> group)
        {
            List<Celldata> cellGroup = group;
            if (group == null)
            {
                cellGroup = new List<Celldata>();
                groups.Add(cellGroup);
            }

            int id = groupID;
            if (groupID == 1)
            {
                latestGroupID++;
                id = latestGroupID;
            }

            fill(x, y, id, cellGroup);
        }

        private int getCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
                return cells[x, y];
            return -1;
        }
    }
}