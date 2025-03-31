using UnityEngine;

public class EvasionAgent : MonoBehaviour
{
    public Transform alvo; 
    public float velocidade = 3f;
    public float distanciaEvasao = 3f; 
    public float forcaEvasao = 2f; 
    public float tempoMudancaDirecao = 2f; 
    public float distanciaDeteccao = 1.5f;
    public LayerMask camadaObstaculos; 

    private Vector3 direcaoAtual;
    private float tempoAtual = 0f;
    private Camera cam;

    void Start()
    {
        direcaoAtual = Random.insideUnitCircle.normalized;
        cam = Camera.main; // Obtém a câmera principal
    }

    void Update()
    {
        if (alvo != null)
        {
            float distancia = Vector3.Distance(transform.position, alvo.position);
            if (distancia < distanciaEvasao)
            {
                Vector3 direcaoEvasao = (transform.position - alvo.position).normalized * forcaEvasao;
                direcaoAtual = (direcaoAtual + direcaoEvasao).normalized;
            }
        }

        if (Physics2D.Raycast(transform.position, direcaoAtual, distanciaDeteccao, camadaObstaculos))
        {
            direcaoAtual = Quaternion.Euler(0, 0, Random.Range(90f, 180f)) * direcaoAtual;
        }

        tempoAtual += Time.deltaTime;
        if (tempoAtual >= tempoMudancaDirecao)
        {
            direcaoAtual = Random.insideUnitCircle.normalized;
            tempoAtual = 0f;
        }

        transform.position += direcaoAtual * velocidade * Time.deltaTime;

        ConfinarNaTela(); // Garante que o triângulo não saia da tela

        float angulo = Mathf.Atan2(direcaoAtual.y, direcaoAtual.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    void ConfinarNaTela()
    {
        Vector3 posicaoTela = transform.position;
        Vector3 limites = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        posicaoTela.x = Mathf.Clamp(posicaoTela.x, -limites.x, limites.x);
        posicaoTela.y = Mathf.Clamp(posicaoTela.y, -limites.y, limites.y);

        transform.position = posicaoTela;
    }
}

