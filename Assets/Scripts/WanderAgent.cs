using UnityEngine;

public class ComportamentoVagar : MonoBehaviour
{
    // Configurações de velocidade
    public float velocidadeMinima = 1f;
    public float velocidadeMaxima = 3f;
    
    // Tempo entre mudanças de direção
    public float tempoMinMudanca = 1f;
    public float tempoMaxMudanca = 3f;
    
    // Tempo máximo fora da tela antes de voltar
    public float tempoForaDaTela = 3f;

    // Variáveis de estado
    private Vector3 velocidadeAtual;
    private float contadorMudanca;
    private float contadorForaTela;
    private bool estaRetornando;

    void Start()
    {
        EscolherNovaDirecao();
    }

    void Update()
    {
        // Atualiza contadores
        contadorMudanca -= Time.deltaTime;
        
        // Se acabou o tempo e não está retornando, escolhe nova direção
        if (contadorMudanca <= 0 && !estaRetornando)
        {
            EscolherNovaDirecao();
        }

        // Move o objeto
        transform.position += velocidadeAtual * Time.deltaTime;
        
        // Rotaciona na direção do movimento
        RotacionarNaDirecao();
        
        // Verifica se saiu da tela
        VerificarSeSaiuDaTela();
    }

    // Escolhe uma direção aleatória para o objeto se mover
    void EscolherNovaDirecao()
    {
        // Direções possíveis: cima, baixo, esquerda, direita
        Vector3[] direcoes = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        
        // Seleciona direção e velocidade aleatórias
        velocidadeAtual = direcoes[Random.Range(0, direcoes.Length)] * Random.Range(velocidadeMinima, velocidadeMaxima);
        
        // Define novo tempo para próxima mudança
        contadorMudanca = Random.Range(tempoMinMudanca, tempoMaxMudanca);
    }

    // Verifica se o objeto saiu da área visível da câmera
    void VerificarSeSaiuDaTela()
    {
        Camera cam = Camera.main;
        Vector3 posicaoTela = cam.WorldToViewportPoint(transform.position);

        // Se está fora dos limites da tela (0-1 no viewport)
        if (posicaoTela.x < 0 || posicaoTela.x > 1 || posicaoTela.y < 0 || posicaoTela.y > 1)
        {
            contadorForaTela += Time.deltaTime;

            // Se ficou muito tempo fora, começa a voltar
            if (contadorForaTela >= tempoForaDaTela)
            {
                ComecarRetornarParaTela();
            }
        }
        else
        {
            // Resetar contador se voltou para a tela
            contadorForaTela = 0;
            estaRetornando = false;
        }
    }

    // Inicia o comportamento de retorno para a tela
    void ComecarRetornarParaTela()
    {
        estaRetornando = true;
        
        Camera cam = Camera.main;
        Vector3 posicaoAlvo = Vector3.zero;

        // Calcula dimensões da área visível
        float alturaCamera = 2f * cam.orthographicSize;
        float larguraCamera = alturaCamera * cam.aspect;

        // Determina para qual ponto central retornar baseado em onde saiu
        if (transform.position.x < -larguraCamera / 2) { posicaoAlvo = new Vector3(0, transform.position.y, 0); }
        else if (transform.position.x > larguraCamera / 2) { posicaoAlvo = new Vector3(0, transform.position.y, 0); }
        else if (transform.position.y < -alturaCamera / 2) { posicaoAlvo = new Vector3(transform.position.x, 0, 0); }
        else if (transform.position.y > alturaCamera / 2) { posicaoAlvo = new Vector3(transform.position.x, 0, 0); }

        // Define velocidade na direção do ponto central
        velocidadeAtual = (posicaoAlvo - transform.position).normalized * Random.Range(velocidadeMinima, velocidadeMaxima);
    }

    // Rotaciona o objeto para ficar de frente para a direção do movimento
    void RotacionarNaDirecao()
    {
        if (velocidadeAtual != Vector3.zero)
        {
            float angulo = Mathf.Atan2(velocidadeAtual.y, velocidadeAtual.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angulo);
        }
    }
}