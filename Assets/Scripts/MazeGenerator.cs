using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    public GameObject[] floors;
    public GameObject[] walls;

    public int rowNum;
    public int colNum;
    // Start is called before the first frame update
    void Start()
    {
        rowNum = 2 * rowNum - 1;
        colNum= 2 * colNum - 1;

        int[][] maze;
        while(true)
        {
            maze = GenerateMaze();
            if (FindPath(maze, 0, 0))
            {
                break;
            }
        }
        Build(maze);
        /*for (int i = 0; i < rowNum; i++)
        {
            string temp = " ";
            for (int j = 0; j < colNum; j++)
            {
                temp = temp + " " + maze[i][j];
            }
            Debug.Log(temp);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool FindPath(int[][] maze, int row, int col)
    {
        if (row == rowNum-1 && col == colNum-1)
        {
            maze[row][col] = 2;
            return true;
        }
        maze[row][col] = 2;
        if (col + 1 < maze[0].Length && 0 == maze[row][col + 1])
            if (FindPath(maze, row, col + 1))
                return true;
        if (row + 1 < maze.Length && 0 == maze[row + 1][col])
            if (FindPath(maze, row + 1, col))
                return true;
        if (col - 1 >= 0 && 0 == maze[row][col - 1])
            if (FindPath(maze, row, col - 1))
                return true;
        if (row - 1 >= 0 && 0 == maze[row - 1][col])
            if (FindPath(maze, row - 1, col))
                return true;
        maze[row][col] = 0;
        return false;
    }

    int[][] GenerateMaze()
    {
        int[][] maze = new int[rowNum][];
        for (int i = 0; i < colNum; i++)
        {
            maze[i] = new int[colNum];
        }
        for(int row = 0; row < rowNum; row++)
        {
            int[] temp = new int[colNum];
            for(int col = 0; col < colNum; col++)
            {
                if(row%2 == 0)
                {
                    if(col % 2 == 0)
                    {
                        temp[col] = 0;
                    }
                    else
                    {
                        temp[col] = Random.Range(0, 2);
                    }
                }
                else
                {
                    if (col % 2 == 0)
                    {
                        temp[col] = Random.Range(0, 2);
                    }
                    else
                    {
                        temp[col] = 1;
                    }
                }
            }
            maze[row] = temp;
        }
        return maze;
    }

    void Build(int[][] maze)
    {
        int floorCount = 0;
        int wallCount = 0;
        for (int row = 0; row < rowNum; row++)
        {
            for (int col = 0; col < colNum; col++)
            {
                if(row % 2 == 0)
                {
                    if(col % 2 == 0)
                    {
                        floors[floorCount].SetActive(true);
                        if (maze[row][col] == 2)
                        {
                            floors[floorCount].GetComponent<Renderer>().material.color = Color.green;
                        }
                        floorCount++;
                    }
                    else
                    {
                        if (maze[row][col] == 1)
                        {
                            walls[wallCount++].SetActive(true);
                        }
                        else
                        {
                            walls[wallCount++].SetActive(false);
                        }
                    }
                }
                else
                {
                    if (col % 2 == 0)
                    {
                        if (maze[row][col] == 1)
                        {
                            walls[wallCount++].SetActive(true);
                        }
                        else
                        {
                            walls[wallCount++].SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
