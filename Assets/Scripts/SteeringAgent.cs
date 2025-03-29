using UnityEngine;

public class AgenteDirecionamento : MonoBehaviour
{
    // Velocidade máxima que o triângulo pode ter
    public float velocidadeMaxima = 5f;

    // Força máxima que pode ser aplicada para mudar a direção
    public float forcaMaxima = 10f;

    // Velocidade atual do triângulo
    private Vector2 velocidadeAtual;

    void Update()
    {
        // Pega a posição do mouse no mundo do jogo
        Vector2 posicaoAlvo = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calcula a direção desejada para o movimento
        Vector2 velocidadeDesejada = (posicaoAlvo - (Vector2)transform.position).normalized * velocidadeMaxima;

        // Calcula a força de direção necessária
        Vector2 forcaDirecao = velocidadeDesejada - velocidadeAtual;

        // Limita a força de direção para não ser muito forte
        forcaDirecao = Vector2.ClampMagnitude(forcaDirecao, forcaMaxima);

        // Atualiza a velocidade atual aplicando a força
        velocidadeAtual = Vector2.ClampMagnitude(velocidadeAtual + forcaDirecao * Time.deltaTime, velocidadeMaxima);

        // Move o triângulo de acordo com a velocidade
        transform.position += (Vector3)velocidadeAtual * Time.deltaTime;

        // Calcula o ângulo de rotação para a direção do movimento
        float angulo = Mathf.Atan2(velocidadeAtual.y, velocidadeAtual.x) * Mathf.Rad2Deg;

        // Aplica a rotação ao triângulo
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
    }
}