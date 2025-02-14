using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grafico : MonoBehaviour
{
    bool ativarCoordenadas = true;
    public int altura, largura;
    public Vector2Int posicaoInicial;
    public List<Vector2Int> listaCaminho = new List<Vector2Int>();
    public List<GameObject> listaCelulas = new List<GameObject>();
    [SerializeField] GameObject caminho, parede, paredeDest, pontoInicial;
    [SerializeField] Transform cam;

    [SerializeField] int quantParedeDest;

    void Awake()
    {
        GerarGrid();
    }

    void Start()
    {
        AtivarCoordenadas();
    }

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
                    MostrarCoordenadas(spawnInicial, x, y);
                    GetCelulas(spawnInicial.GetComponentInChildren<TMP_Text>().gameObject);
                }

                else if (x % 2 != 0 && y % 2 != 0)
                {
                    GameObject spawnParede = Instantiate(parede, new Vector2(x * 1.1f, y * 1.1f), Quaternion.identity);
                    spawnParede.name = $"Parede {x}, {y}";
                    MostrarCoordenadas(spawnParede, x, y);
                    GetCelulas(spawnParede.GetComponentInChildren<TMP_Text>().gameObject);
                }

                else
                {
                    GameObject spawnCaminho = Instantiate(caminho, new Vector2(x * 1.1f, y * 1.1f), Quaternion.identity);
                    spawnCaminho.name = $"Caminho {x},{y}";
                    MostrarCoordenadas(spawnCaminho, x, y);
                    GetCelulas(spawnCaminho.GetComponentInChildren<TMP_Text>().gameObject);
                    listaCaminho.Add(new Vector2Int(x, y));
                }
            }
        }

        cam.transform.position = new Vector3((float)largura / 2 + 3f, (float)altura / 2, -10f);
    }

    void MostrarCoordenadas(GameObject _objeto, int x = 0, int y = 0)
    {
        if (ativarCoordenadas)
            _objeto.GetComponentInChildren<TMP_Text>().text = $"{x}.{y}";
        else
            _objeto.GetComponentInChildren<TMP_Text>().text = " ";
    }

    void GetCelulas(GameObject _celulas)
    {
        listaCelulas.Add(_celulas);
    }

    void MostrarCoordenadas()
    {
        foreach (GameObject celulas in listaCelulas)
        {
            if (ativarCoordenadas)
                celulas.SetActive(true);
            else
                celulas.SetActive(false);
        }
    }

    public void AtivarCoordenadas()
    {
        ativarCoordenadas = !ativarCoordenadas;
        MostrarCoordenadas();
    }
}
