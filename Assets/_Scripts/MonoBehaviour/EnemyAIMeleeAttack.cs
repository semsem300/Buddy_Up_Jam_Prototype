using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIMeleeAttack : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    public UnityEvent<GameObject> OnPlayerDetectd;

    [Range(.1f, 10)]
    public float radius;
    [Header("Gizmo paramter")]
    public Color gizmoColor = Color.green;
    public bool showGizmo = true;
    public bool PlayerDetcted { get; set; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var colider = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
        PlayerDetcted = colider != null;
        if (PlayerDetcted)
            OnPlayerDetectd?.Invoke(colider.gameObject);
    }
    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
