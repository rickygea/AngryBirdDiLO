using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    public GameObject folderbird;
    public List<Bird> Birds = new List<Bird>();
    public GameObject folderenemy;
    public List<Enemy> Enemies = new List<Enemy>();

    private bool _isGameEnded = false;

    private Bird _shotBird;
    public BoxCollider2D TapCollider;
    public int stageselanjutnya;
    void Start()
    {
        for (int i = 0; i < folderbird.transform.childCount; i++)
        {
            Birds.Add( folderbird.transform.GetChild(i).GetComponent<Bird>());
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdDestroyed += checkbird;
            Birds[i].OnBirdShot += AssignTrail;
        }
        for (int i = 0; i < folderenemy.transform.childCount; i++)
        {
            Enemies.Add(folderenemy.transform.GetChild(i).GetComponent<Enemy>());
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }
    public void checkbird() {
        if (Birds.Count == 0)
        {
            _isGameEnded = true;
            StartCoroutine(nextstage(SceneManager.GetActiveScene().buildIndex));
        }
    }
    public void ChangeBird()
    {
        if (TapCollider != null)
        {
            TapCollider.enabled = false;
        }
        if (_isGameEnded)
        {
            return;
        }
        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {       SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }
    }
    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            Debug.Log("gameover");
            _isGameEnded = true;
            StartCoroutine(nextstage(stageselanjutnya));
        }
    }
    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }
    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
    IEnumerator nextstage(int input) {
        yield return new WaitForSeconds(2f);
       
            SceneManager.LoadScene(input);
     
    }
}
