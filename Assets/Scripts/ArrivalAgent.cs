using UnityEngine;

public class ArrivalAgent : MonoBehaviour
{
    public float velocidade = 3f;
    public float rotacaoVelocidade = 200f;
    public Light luzTriangulo; // Referência à luz do triângulo

    private Vector3 destino;
    private bool temDestino = false; // Indica se já temos um destino válido

    void Start()
    {
        if (luzTriangulo != null)
        {
            luzTriangulo.enabled = false; // Luz desligada no início
        }

        destino = transform.position; // Começa parado no lugar
    }

    void Update()
    {
        // Obtém a posição do mouse na cena
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicaoMouse.z = 0;

        // Define um novo destino ao clicar com o mouse
        if (Input.GetMouseButton(0)) // Clique esquerdo do mouse
        {
            destino = posicaoMouse;
            temDestino = true;

            // Acende a luz quando está indo para o destino
            if (luzTriangulo != null)
            {
                luzTriangulo.enabled = true;
            }
        }

        if (temDestino)
        {
            // Move em direção ao destino
            Vector3 direcao = (destino - transform.position).normalized;
            transform.position += direcao * velocidade * Time.deltaTime;

            // Rotaciona para a direção do movimento
            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angulo);

            // Se estiver muito próximo do destino, para de se mover
            if (Vector3.Distance(transform.position, destino) < 0.1f)
            {
                temDestino = false;
                if (luzTriangulo != null)
                {
                    luzTriangulo.enabled = false; // Apaga a luz
                }
            }
        }
        else
        {
            // Se não tem um destino, rotaciona no lugar
            transform.Rotate(0, 0, rotacaoVelocidade * Time.deltaTime);
        }
    }
}
