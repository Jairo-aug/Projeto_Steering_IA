using UnityEngine;
using TMPro;

public class GerenciadorJogo : MonoBehaviour
{
    public GameObject[] triangulos; // Lista de triângulos
    public string[] nomesDosAgentes; // Lista de nomes dos agentes
    public TextMeshProUGUI textoAgente; // Referência ao texto na tela

    private int indiceAtual = 0;

    void Start()
    {
        AtualizarAgente();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Pressiona Enter
        {
            TrocarParaProximoTriangulo();
        }
    }

    void TrocarParaProximoTriangulo()
    {
        triangulos[indiceAtual].SetActive(false);
        indiceAtual = (indiceAtual + 1) % triangulos.Length;
        AtualizarAgente();
    }

    void AtualizarAgente()
    {
        for (int i = 0; i < triangulos.Length; i++)
        {
            triangulos[i].SetActive(i == indiceAtual);
        }

        // Atualiza o texto na tela
        textoAgente.text = nomesDosAgentes[indiceAtual];
    }
}

