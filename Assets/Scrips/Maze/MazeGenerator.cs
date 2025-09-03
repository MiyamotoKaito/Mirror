using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MazeGenerator : MonoBehaviour
{
    public int Width, Height;
    private System.Random random = new System.Random();
    private MazeCellModel[,] maze;

    public GameObject mazeCellPrefab;
    [SerializeField] private Transform root;
    private float cellScale = 4;

    private void Start()
    {
        GenerateMaze();
    }
    public void ClearMaze()
    {
        List<GameObject> tempList = new List<GameObject>();
        foreach (Transform child in root)
        {
            tempList.Add(child.gameObject);
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            Destroy(tempList[i]);
        }
    }

    public void GenerateMaze()
    {
        ClearMaze();
        maze = new MazeCellModel[Width, Height];
        for (int w = 0; w < Width; w++)
        {
            for (int h = 0; h < Height; h++)
            {
                maze[w, h] = new MazeCellModel();
            }
        }
        GenerateMaze(0, 0);

        for (int w = 0; w < Width; w++)
        {
            for (int h = 0; h < Height; h++)
            {
                float posX = w * cellScale;
                float posZ = h * cellScale;

                MazeCell cell =
                    Instantiate(mazeCellPrefab, new Vector3(posX, 0f, posZ), Quaternion.identity, root)
                    .GetComponent<MazeCell>();

                cell.transform.localScale *= cellScale;
                cell.name = $"{w} - {h}";
                cell.SetUp(maze[w, h]);
            }
        }
    }

    private void GenerateMaze(int x, int y)
    {
        MazeCellModel currrentCell = maze[x, y];
        currrentCell.visited = true;


        foreach (var direction in ShuffleDirections())
        {
            int newX = x + direction.Item1;
            int newZ = y + direction.Item2;
            if (newX >= 0 && newZ >= 0 && newX < Width && newZ < Height)
            {
                MazeCellModel neighbourCell = maze[newX, newZ];
                if (!neighbourCell.visited)
                {
                    neighbourCell.visited = true;
                    currrentCell.RemoveWall(direction.Item3);
                    neighbourCell.RemoveWall(direction.Item4);
                    GenerateMaze(newX, newZ);
                }
            }
        }
    }

    private List<(int, int, MazeCellModel.Wall, MazeCellModel.Wall)> ShuffleDirections()
    {
        List<(int, int, MazeCellModel.Wall, MazeCellModel.Wall)> directions = new List<(int, int, MazeCellModel.Wall, MazeCellModel.Wall)>
        {
            (0, 1, MazeCellModel.Wall.Top, MazeCellModel.Wall.Bottom),
            (0, -1, MazeCellModel.Wall.Bottom, MazeCellModel.Wall.Top),
            (-1, 0, MazeCellModel.Wall.Left, MazeCellModel.Wall.Right),
            (1, 0, MazeCellModel.Wall.Right, MazeCellModel.Wall.Left)
        };
        for (int i = 0; i < directions.Count; i++)
        {
            var temp = directions[i];
            int randomIntex = random.Next(i, directions.Count);
            directions[i] = directions[randomIntex];
            directions[randomIntex] = temp;
        }
        return directions;
    }
}