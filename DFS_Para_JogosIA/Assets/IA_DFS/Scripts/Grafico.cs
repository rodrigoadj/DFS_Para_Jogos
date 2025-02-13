using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grafico : MonoBehaviour
{
    public int altura, largura;
    public Vector2Int posicaoInicial;
    public List<Vector2Int> listaCaminho = new List<Vector2Int>();
    [SerializeField] GameObject caminho, parede, paredeDest, pontoInicial;
    [SerializeField] Transform cam;

    [SerializeField] int quantParedeDest;

    void Awake()
    {
        // quantParede = (altura - 3) * (largura - 3);
        GerarGrid();
    }

    // Update is called once per frame
    void GerarGrid()
    {
        for (int x = 0; x < altura; x++)
        {
            for (int y = 0; y < largura; y++)
            {
                if (x == 0 && y == 8)
                {
                    GameObject spawnInicial = Instantiate(pontoInicial, new Vector2(x * 1.1f, y * 1.1f), Quaternion.identity);
                    spawnInicial.name = $"Ponto inicial {x}, {y}";
                    posicaoInicial = new Vector2Int(x, y);
                }

                else if (x % 2 != 0 && y % 2 != 0)
                {
                    GameObject spawnParede = Instantiate(parede, new Vector2(x * 1.1f, y * 1.1f), Quaternion.identity);
                    spawnParede.name = $"Parede {x}, {y}";
                }

                else
                {
                    // GameObject spawnCaminho = Instantiate(SelecionarObjeto(), new Vector2(x, y), Quaternion.identity);
                    GameObject spawnCaminho = Instantiate(caminho, new Vector2(x * 1.1f, y * 1.1f), Quaternion.identity);
                    spawnCaminho.name = $"Caminho {x},{y}";
                    listaCaminho.Add(new Vector2Int(x, y));
                }
            }
        }

        cam.transform.position = new Vector3((float)largura / 2 + 3f, (float)altura / 2, -10f);
    }

    // GameObject SelecionarObjeto()
    // {
    //     GameObject[] objeto = { caminho, paredeDest };
    //     int random = Random.Range(0, objeto.Length);

    //     if (quantParedeDest > 0 && random == 1)
    //     {
    //         quantParedeDest--;
    //         return objeto[random];
    //     }
    //     return caminho;
    // }
}
