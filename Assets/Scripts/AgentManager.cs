using TMPro;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public PlayerAgent agent;
    public TextMeshProUGUI infoText;

    public float timeScale = 1f;

    public float minEpsilon = 0.05f;
    public float decayRate = 0.999f;

    private int _episodeCount;
    private float _initialEpsilon;
    private int _bestScore;

    void Start()
    {
        Time.timeScale = timeScale;
        _episodeCount = 0;

        _initialEpsilon = agent.Epsilon;
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (infoText == null) return;
        if (agent.Score > _bestScore)
        {
            _bestScore = agent.Score;
        }

        infoText.text = $"BestScore: {_bestScore}\n" +
                        $"Score: {agent.Score}\n" +
                        $"Episodes: {_episodeCount}\n" +
                        $"Epsilon: {agent.Epsilon:F3}/n";
    }

    public void EndEpisode()
    {
        _episodeCount++;
        DecayEpsilon();
        ResetEpisode();
    }

    void DecayEpsilon()
    {
        float newEpsilon = _initialEpsilon * Mathf.Pow(decayRate, _episodeCount);
        agent.Epsilon = Mathf.Max(minEpsilon, newEpsilon);
    }

    void ResetEpisode()
    {
        agent.Reset();
        DestroyAllPipes();
    }

    void DestroyAllPipes()
    {
        PipeMovement[] pipes = FindObjectsByType<PipeMovement>(FindObjectsSortMode.None);
        foreach (PipeMovement pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
    }
}
