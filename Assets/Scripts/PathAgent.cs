using UnityEngine;

public class SeguimentoDeCaminho : MonoBehaviour
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
