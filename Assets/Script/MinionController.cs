using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    private bool _moving;
    private Vector3 _lastPosition;
    private Vector3 _position;
    private Camera _camera;

    private Transform _mainIsland;
    private IslandController _lastIslandController;
    private MinionDescriptor _minionDescriptor;
    

    public Transform MainIsland
    {
        get { return _mainIsland; }
        set { _mainIsland = value; }
    }

    private void Start()
    {
        _camera = Camera.main;
        if (!_mainIsland)
            _mainIsland = transform.parent;
        _minionDescriptor = GetComponent<MinionDescriptor>();
        StartCoroutine(Animator());
    }

    private void Update()
    {
        if (_moving)
        {
            RaycastHit[] hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    _position = hit.point;
                }
            }
            
            transform.position = Vector3.Lerp(transform.position, _position, 10f * Time.deltaTime);
        }
    }

    private IEnumerator Animator()
    {
        while (_minionDescriptor.Working)
        {
            bool active = Random.Range(0f, 1f) > 0.5f;
            
            if (active)
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                GetComponent<Animator>().SetBool("active", active);
                yield return new WaitForSeconds(5f);
            }
            else
            {
                transform.DOLookAt(Camera.main.transform.position, 1f);
                yield return new WaitForSeconds(2f);
            }

            GetComponent<Animator>().SetBool("active", false);
        }
    }

    public void OnMouseDown()
    {
        if (!transform.parent.CompareTag("VacationIsland") && !transform.parent.CompareTag("MergeIsland"))
        {
            GameManager.Instance.TurnVacationIslandOutline();
            GameManager.Instance.TurnMergeIslandOutline();
        }
        else
        {
            if (transform.parent.CompareTag("VacationIsland"))
            {
                GameManager.Instance.TurnMergeIslandOutline();
            }
            else
            {
                GameManager.Instance.TurnVacationIslandOutline();
            }
            
            _mainIsland.GetComponent<IslandController>().TurnOutline(true);
        }
        
        _lastPosition = transform.position;
        _moving = true;
        _minionDescriptor.WhenMoving();
        TurnLaser(true);
    }

    public void OnMouseUp()
    {
        _moving = false;
        TurnLaser(false);
        GameManager.Instance.TurnOffAllOutline();
        
        RaycastHit[] hits = Physics.RaycastAll(transform.position, -transform.up);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("VacationIsland"))
            {
                if (transform.parent.CompareTag("VacationIsland"))
                {
                    transform.position = new Vector3(transform.position.x, -0.148f, transform.position.z);
                    return;
                }

                var islandController = hit.collider.GetComponent<IslandController>();
                if (islandController.MinionsCount < islandController.maxMinions)
                {
                    islandController.AddMinion(gameObject);
                    islandController.MinionsCount++;
                    
                    var prevIsland = transform.parent.GetComponent<IslandController>();
                    if (transform.parent.CompareTag("MergeIsland"))
                    {
                        prevIsland.MinionsCount--;
                    }
                    prevIsland.RemoveMinion(gameObject);

                    transform.position = new Vector3(transform.position.x, -0.148f, transform.position.z);
                    transform.parent = hit.collider.transform;
                    
                    _minionDescriptor.WhenPlacedOnRestoration();
                    GameManager.Instance.TurnOffAllOutline();
                    
                    _moving = false;
                    return;
                }
            }
            
            if (hit.collider.CompareTag("MergeIsland"))
            {
                if (transform.parent.CompareTag("MergeIsland"))
                {
                    transform.position = new Vector3(transform.position.x, -0.148f, transform.position.z);
                    return;
                }
                
                var islandController = hit.collider.GetComponent<IslandController>();

                if (islandController.MinionsCount < islandController.maxMinions)
                {
                    if (islandController.MinionsCount > 0)
                    {
                        var merged = islandController.MergeBrothers(gameObject.GetComponent<MinionDescriptor>());

                        if (merged)
                        {
                            islandController.AddMinion(merged);
                        }
                        else
                        {
                            islandController.AddMinion(gameObject);
                        }
                        
                        islandController.MinionsCount++;
                    }
                    else
                    {
                        islandController.AddMinion(gameObject);
                        islandController.MinionsCount++;
                    }
                   
                    var prevIsland = transform.parent.GetComponent<IslandController>();
                    if (transform.parent.CompareTag("VacationIsland"))
                    {
                        prevIsland.MinionsCount--;
                    }
                    prevIsland.RemoveMinion(gameObject);
                    
                    transform.position = new Vector3(transform.position.x, -0.148f, transform.position.z);
                    transform.parent = hit.collider.transform;
                    
                    _minionDescriptor.WhenPlacedOnMerging();
                    GameManager.Instance.TurnOffAllOutline();
                    
                    _moving = false;
                    return;
                }
            }

            if (hit.collider.transform == _mainIsland)
            {
                var currentIsland = _mainIsland.GetComponent<IslandController>();
                currentIsland.AddMinion(gameObject);
                
                var prevIsland = transform.parent.GetComponent<IslandController>();
                prevIsland.RemoveMinion(gameObject);
                prevIsland.MinionsCount--;
                
                transform.position = new Vector3(transform.position.x, -0.148f, transform.position.z);
                transform.parent = hit.collider.transform;
                
                _minionDescriptor.WhenPlacedOnWorking();
                GameManager.Instance.TurnOffAllOutline();
                
                StartCoroutine(Animator());
                _moving = false;
                return;
            }
        }

        transform.position = _lastPosition;
    }

    private void TurnLaser(bool on)
    {
        var laser = GetComponent<Laser>();
        laser.gunPoint.gameObject.SetActive(on);
        laser.pause = !on;
    }
}
