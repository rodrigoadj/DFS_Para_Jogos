using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DFS : MonoBehaviour
{
    public Grafico scptGrafico; // Referência ao gerador do grid
    public Vector2Int posicaoFinal = new Vector2Int(); // Posição de destino

    private bool[,] casaVisitada; // Matriz para marcar células visitadas
    private Stack<Vector2Int> pilhaCasas = new Stack<Vector2Int>(); // Pilha para o DFS
    private List<Vector2Int> caminho = new List<Vector2Int>(); // Caminho encontrado

    void Start()
    {
        // Inicializa a matriz de visitados com base no layout do grid
        int largura = scptGrafico.largura; // Largura do grid
        int altura = scptGrafico.altura;   // Altura do grid
        casaVisitada = new bool[largura, altura];

        // Executa o DFS a partir do ponto inicial
        DFSAlgorithm(scptGrafico.posicaoInicial);
    }

    void DFSAlgorithm(Vector2Int startPosition)
    {
        // Adiciona o ponto inicial à pilha
        pilhaCasas.Push(startPosition);
        SetarPosicaoFinal();

        while (pilhaCasas.Count > 0)
        {
            // Remove o nó atual da pilha
            Vector2Int posicaoAtual = pilhaCasas.Pop();

            // Verifica se a célula atual é o destino
            //if (posicaoAtual == posicaoFinal)
            //{
            //    Debug.Log("Caminho encontrado!");
            //    caminho.Add(posicaoAtual); // Adiciona a célula ao caminho
            //    MostrarCaminho();
            //    return;
            //}

            // Marca a célula atual como visitada
            casaVisitada[posicaoAtual.x, posicaoAtual.y] = true;

            // Adiciona a célula atual ao caminho
            caminho.Add(posicaoAtual);

            // Define as direções possíveis (cima, baixo, esquerda, direita)
            Vector2Int[] direcoesMovimento = new Vector2Int[]
            {
                new Vector2Int(0, 1),  // Cima
                new Vector2Int(1, 0),  // Direita
                new Vector2Int(0, -1), // Baixo
                new Vector2Int(-1, 0)  // Esquerda
            };

            // Explora as células vizinhas
            foreach (var direcao in direcoesMovimento)
            {
                Vector2Int proxPosicao = posicaoAtual + direcao;

                // Verifica se a próxima posição é válida e não foi visitada
                if (VerificarPosicao(proxPosicao) && !casaVisitada[proxPosicao.x, proxPosicao.y])
                {
                    if (proxPosicao == posicaoFinal)
                    {
                        Debug.Log("Caminho encontrado!");
                        caminho.Add(proxPosicao);
                        //MostrarCaminho();
                        StartCoroutine(MostrarCaminho());
                        return;
                    }
                    pilhaCasas.Push(proxPosicao); // Adiciona à pilha
                }
            }
        }

        Debug.Log("Caminho não encontrado.");
    }

    // Verifica se a posição é válida e faz parte do caminho
    bool VerificarPosicao(Vector2Int position)
    {
        // Verifica se está dentro dos limites do grid
        if (position.x < 0 || position.x >= scptGrafico.largura || position.y < 0 || position.y >= scptGrafico.altura)
            return false;

        // Verifica se a célula faz parte do caminho
        foreach (var posicaoCaminho in scptGrafico.listaCaminho)
        {
            if (position == posicaoCaminho)
            {
                return true;
            }
        }
        return false;
    }


    // void MostrarCaminho()
    // {
    //     foreach (var posicao in caminho)
    //     {
    //         // Encontra a célula no grid e muda sua cor
    //         GameObject caminhoEncontrado = GameObject.Find($"Caminho {posicao.x},{posicao.y}");

    //         if (caminhoEncontrado != null && caminhoEncontrado.name != GameObject.Find($"Caminho {posicaoFinal.x},{posicaoFinal.y}").name)
    //         {
    //             // caminhoEncontrado.GetComponent<SpriteRenderer>().material.color = Color.green;
    //             Color corCaminho = caminhoEncontrado.GetComponent<SpriteRenderer>().material.color;
    //             corCaminho.g = 255;
    //             caminhoEncontrado.GetComponent<SpriteRenderer>().material.color = corCaminho;
    //             Debug.Log(caminhoEncontrado.name);
    //         }
    //     }
    // }

    // Destaca o caminho encontrado
    IEnumerator MostrarCaminho()
    {
        foreach (var posicao in caminho)
        {
            yield return new WaitForSeconds(1.5f);
            // Encontra a célula no grid e muda sua cor
            GameObject caminhoEncontrado = GameObject.Find($"Caminho {posicao.x},{posicao.y}");

            if (caminhoEncontrado != null && caminhoEncontrado.name != GameObject.Find($"Caminho {posicaoFinal.x},{posicaoFinal.y}").name)
            {
                // caminhoEncontrado.GetComponent<SpriteRenderer>().material.color = Color.green;
                Color corCaminho = caminhoEncontrado.GetComponent<SpriteRenderer>().material.color;
                corCaminho.g = 255;
                caminhoEncontrado.GetComponent<SpriteRenderer>().material.color = corCaminho;
                Debug.Log(caminhoEncontrado.name);
            }
        }
    }

    void SetarPosicaoFinal()
    {
        //int random = Random.Range(0, scptGrafico.listaCaminho.Count);
        //posicaoFinal = scptGrafico.listaCaminho[random];
        //Debug.Log(posicaoFinal);
        posicaoFinal = new Vector2Int(5, 4);

        GameObject pontoFinal = GameObject.Find($"Caminho {posicaoFinal.x},{posicaoFinal.y}");
        if (pontoFinal != null)
        {
            Color cor = pontoFinal.GetComponent<SpriteRenderer>().material.color;
            cor.r = 255f;
            pontoFinal.GetComponent<Renderer>().material.color = cor;
        }
    }
}