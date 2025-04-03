using UnityEngine;

public class EvasionAgent : MonoBehaviour
{
    public Transform[] alvos; // Array de alvos
    public float velocidade = 3f;
    private Vector3 direcaoAtual;
    private float tempoAtual = 0f;
    private Camera cam;
    private int indiceAlvoAtual = 0; // Índice do alvo atual

    private Collider2D meuCollider; // Referência ao collider do agente
    private Collider2D colliderAlvo; // Referência ao collider do alvo atual

    void Start()
    {
        cam = Camera.main; // Obtém a câmera principal
        meuCollider = GetComponent<Collider2D>();
        colliderAlvo = alvos[indiceAlvoAtual].GetComponent<Collider2D>();
    }

    void Update()
    {
        if (alvos != null && alvos.Length > 0)
        {
            Transform alvoAtual = alvos[indiceAlvoAtual];
            if (alvoAtual != null)
            {
                // Calcula a direção para o alvo atual
                Vector3 direcaoParaAlvo = (alvoAtual.position - transform.position).normalized;
                Bounds boundsFuturos = meuCollider.bounds;
                boundsFuturos.center += direcaoParaAlvo * velocidade * Time.deltaTime;

                if (colliderAlvo != null && meuCollider != null && colliderAlvo.bounds.Intersects(boundsFuturos))
                {
                    // Calcula uma nova direção para evitar a colisão
                    direcaoAtual = -direcaoAtual;
                    indiceAlvoAtual = (indiceAlvoAtual + 1) % alvos.Length;
                    colliderAlvo = alvos[indiceAlvoAtual].GetComponent<Collider2D>();
                }
                else
                {
                    // Segue em direção ao alvo
                    direcaoAtual = direcaoParaAlvo;
                }
            }
        }

        // Move o agente na direção calculada
        transform.position += direcaoAtual * velocidade * Time.deltaTime;

        // Atualiza a rotação do agente para apontar na direção do movimento
        float angulo = Mathf.Atan2(direcaoAtual.y, direcaoAtual.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    void OnDrawGizmos()
    {
        if (alvos != null)
        {
            Gizmos.color = Color.red;
            foreach (Transform alvo in alvos)
            {
            Collider2D colliderAlvo = alvo.GetComponent<Collider2D>();
            if (colliderAlvo != null)
            {
                Gizmos.DrawWireCube(colliderAlvo.bounds.center, colliderAlvo.bounds.size);
            }
            }
        }
    }
}
