using System;
using UnityEngine;

public class DoubleAgent : MonoBehaviour
{
    // Lista de pontos que formam o caminho
    public Transform[] pontosDoCaminho;
    
    // Configurações de movimento
    public float velocidade = 2f;
    public float distanciaMinima = 0.2f; // Distância para considerar que alcançou o ponto

    // Índice do ponto atual sendo seguido
    private int indicePontoAtual = 0;

    void Update()
    {
        // Se não há pontos definidos, não faz nada
        if (pontosDoCaminho.Length == 0) return;

        // Obtém a posição do mouse no mundo
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicaoMouse.z = 0; // Define z como 0 para 2D

        // Verifica se o mouse está na mesma linha que o transform está direcionado
        Vector3 direcaoAtual = (pontosDoCaminho[indicePontoAtual].position - transform.position).normalized;
        Vector3 direcaoMouse = (posicaoMouse - transform.position).normalized;
        bool mouseNoCaminho = Math.Abs(Vector3.Dot(direcaoAtual, direcaoMouse)) > 0.90f; // Verifica se as direções são quase paralelas

        if (mouseNoCaminho)
        {
            transform.position += direcaoMouse * velocidade * Time.deltaTime;

            // Rotaciona o objeto para ficar de frente para o movimento
            RotacionarNaDirecao(direcaoMouse);
        }
        else
        {
            // Obtém o ponto atual do caminho
            Transform pontoAtual = pontosDoCaminho[indicePontoAtual];

            // Calcula a direção para o ponto
            Vector3 direcao = (pontoAtual.position - transform.position).normalized;

            // Move o objeto na direção do ponto
            transform.position += direcao * velocidade * Time.deltaTime;

            // Rotaciona o objeto para ficar de frente para o movimento
            RotacionarNaDirecao(direcao);

            // Verifica se chegou perto o suficiente do ponto
            if (Vector3.Distance(transform.position, pontoAtual.position) < distanciaMinima)
            {
                indicePontoAtual++;

                // Se chegou no último ponto, volta para o primeiro (loop)
                if (indicePontoAtual >= pontosDoCaminho.Length)
                {
                    indicePontoAtual = 0;
                }
            }
        }
    }

    // Rotaciona o objeto para apontar na direção do movimento
    void RotacionarNaDirecao(Vector3 direcao)
    {
        if (direcao != Vector3.zero)
        {
            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angulo);
        }
    }
}
