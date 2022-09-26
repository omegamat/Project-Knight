using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoControle : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] float hpMax;                       //vida maxima do inimigo
    private float hpAatual;
    [SerializeField] float velocidade;                  //velocidade base do inimigo
    [SerializeField] float multVelPer;                    //multiplicador para a velocidade ao perseguir o jogador
    [SerializeField] float dano;                        //dano calsado ao jogador
    [SerializeField] bool neutro;                       //true == so ataca se for atacado; false == sempre ataca o jogador
    [SerializeField] bool patrulheiro;                  //true == patrula entre os postos de patrulha; false == ficara parado em um ponto
    [SerializeField] Image barraHp;                     // barra de hp do inimigo
    [SerializeField] float distanciaVisao;              // distancia do player para seguir
    [SerializeField] float multiplicatVisao;              // apos ver o player, distancia da visao multiplica por x
    float novoMultVel;
    float novaDistanciaVisao;      
    
    [Header("Animacoes")]
    [SerializeField] private Animator AnimControle;     // AnimatorCotroller do inimigo
    [SerializeField] float tempoDeAtaque;               // tempo entre cada ataque
    [SerializeField] bool OlhaDireita;                  //  

    [Header("Patrulha")]
    [SerializeField] List<Transform> pontosPatrulha;    // Ira patrulhar entre os pontos
    [SerializeField] Transform pontoAtual;              // ponto que esta indo
    [SerializeField] float tempoIdle;                   // tempo que ficara parado em cada ponto
    [SerializeField] int contagemPatrulha;
    [SerializeField] bool irPatrulhar = true;

    [SerializeField] float distanciaAtacar;             // distancia minima para atacar
    private Transform Jogador;
    private new Rigidbody2D rigidbody;
    float direcao;
    bool SeguirPlayer;
    bool atacando;



    // Start is called before the first frame update
    void Start()
    {
        hpAatual = hpMax;
        Jogador = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        AnimControle = GetComponent<Animator>();
        novaDistanciaVisao = distanciaVisao;
        barraHp.fillAmount = Mathf.InverseLerp(0, hpMax, hpAatual);
        pontoAtual.position = pontosPatrulha[0].position;
        novaDistanciaVisao = distanciaVisao;
        novoMultVel = velocidade;

        if(patrulheiro == true)
        {
            irPatrulhar = true;
        }
        else
        {
            irPatrulhar = false;
        }




    }

    // Update is called once per frame
    void Update()
    {

        AnimControle.SetFloat("VelAndando", rigidbody.velocity.magnitude);
        float distanciaJogador = Jogador.position.x - transform.position.x;
        direcao = pontoAtual.position.x - transform.position.x;
        distanciaJogador = Mathf.Abs(distanciaJogador);



        if (SeguirPlayer ==true)
        {
            pontoAtual.position = Jogador.position;
        }

        if(direcao < 0 && OlhaDireita)
        {
            Flip();            
        }
        if(direcao > 0 && !OlhaDireita)
        {
            Flip();
        }


        if (distanciaJogador < novaDistanciaVisao && SeguirPlayer == false && neutro == false)
        {
            SeguirPlayer = true;
            irPatrulhar = false;
            novaDistanciaVisao = distanciaVisao * multiplicatVisao;            
            StopCoroutine("Patrulhar");
        }
        
        if(distanciaJogador >= novaDistanciaVisao && SeguirPlayer == true)
        {
            novaDistanciaVisao = distanciaVisao;
            irPatrulhar = true;
            SeguirPlayer = false;
            pontoAtual.position = pontosPatrulha[contagemPatrulha].position;
        }


        if (distanciaJogador <= distanciaAtacar && atacando == false && SeguirPlayer == true)
        {            
            atacando = true;
            novoMultVel = 0;
            StartCoroutine("Atacar");            
        }
        
        
        if (distanciaJogador > distanciaAtacar && SeguirPlayer == true)
        {
            novoMultVel = velocidade * multVelPer;
        }

        if (SeguirPlayer == false && irPatrulhar == true)
        {
            novoMultVel = velocidade;
        }

        if (irPatrulhar == true && Mathf.Abs(direcao) <= 0.1f)
        {
            irPatrulhar = false;
            rigidbody.velocity = Vector2.zero;
            novoMultVel = 0;
            StartCoroutine("Patrulhar");
        }
        
    }

    public void Flip()
    {
        OlhaDireita = !OlhaDireita;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(direcao * velocidade , rigidbody.velocity.y).normalized;
        rigidbody.velocity = velocity * new Vector2(novoMultVel , 1);
    }


    IEnumerator Patrulhar()
    {
        yield return new WaitForSeconds(tempoIdle);
        contagemPatrulha += 1;
        if (contagemPatrulha > pontosPatrulha.Count - 1)
        {
            contagemPatrulha = 0;
        }
        pontoAtual.position = pontosPatrulha[contagemPatrulha].position;
        irPatrulhar = true;
    }

    IEnumerator Atacar()
    {
        AnimControle.SetTrigger("Atacando");
        yield return new WaitForSeconds(tempoDeAtaque);
        atacando = false;
    }

    // Receber dano no inimigo
    private void TakeDamage(float dano)
    {
        rigidbody.velocity = Vector2.zero;
        if (neutro == true)
        {
            neutro = false;
        }
        AnimControle.SetTrigger("hit");

        hpAatual -= dano;
        barraHp.fillAmount = Mathf.InverseLerp(0, hpMax, hpAatual);
        if (hpAatual <= 0)
        {
            Destroy(gameObject);
        }
    }


    // Ataque do inimigo
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("TakeDamage", dano);
        }
        else
        {
            return;
        }
    }
}
