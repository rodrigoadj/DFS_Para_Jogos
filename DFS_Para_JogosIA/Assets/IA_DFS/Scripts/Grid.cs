using UnityEngine;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    public float cellSize = 1.0f; // Tamanho de cada célula
    public GameObject cellPrefab; // Prefab para células vazias
    public GameObject pathPrefab; // Prefab para o caminho
    public GameObject startPrefab; // Prefab para o ponto inicial

    // Layout do grid ('.' = célula vazia, 'O' = caminho, 'S' = ponto inicial)
    public string[] gridLayout = new string[]
    {
        ".....O..",
        ".OOOO...",
        "....OO.O",
        ".OOOO...",
        ".O..O.OO",
        ".O..OOO.",
        ".O......",
        ".OOOOOO."
    };

    // Lista de posições do caminho
    public List<Vector2Int> pathPositions = new List<Vector2Int>();

    // Posição inicial do personagem
    public Vector2Int startPosition;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int y = 0; y < gridLayout.Length; y++)
        {
            for (int x = 0; x < gridLayout[y].Length; x++)
            {
                // Calcula a posição no mundo (física)
                Vector3 worldPosition = new Vector3(x * cellSize, (gridLayout.Length - y - 1) * cellSize, 0);

                // Verifica o tipo de célula
                if (gridLayout[y][x] == 'O') // Caminho
                {
                    // Instancia o caminho como filho do objeto pai
                    GameObject path = Instantiate(pathPrefab, worldPosition, Quaternion.identity, transform);
                    path.name = $"Path_{x}_{y}";
                    pathPositions.Add(new Vector2Int(x, y)); // Adiciona à lista de caminhos
                }
                else if (gridLayout[y][x] == 'S') // Ponto inicial
                {
                    // Instancia o ponto inicial como filho do objeto pai
                    GameObject start = Instantiate(startPrefab, worldPosition, Quaternion.identity, transform);
                    start.name = $"Start_{x}_{y}";
                    startPosition = new Vector2Int(x, y); // Define a posição inicial
                }
                else if (gridLayout[y][x] == '.') // Célula vazia
                {
                    // Instancia a célula vazia como filho do objeto pai
                    GameObject cell = Instantiate(cellPrefab, worldPosition, Quaternion.identity, transform);
                    cell.name = $"Cell_{x}_{y}";
                }
            }
        }


    }
}