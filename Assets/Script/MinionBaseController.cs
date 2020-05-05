using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MinionBaseController : MonoBehaviour, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private GameObject minionSample;
    [SerializeField] private IslandController islandController;

    [Header("MINION SETTINGS")]
    [SerializeField] private float maxHP = 20f;
    [SerializeField] private float cost = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private MinionDescriptor.Type type;
    
    private GameObject _spawnedMinion;
    private Camera _camera;
    private bool _pressed;
    private Vector3 _position;

    public Sprite img_1, img_2;

    public TMP_Text priceText;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        priceText.text = "" + (int)cost;
        
        if (_spawnedMinion)
        {
            RaycastHit[] hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    _position = hit.point;
                }
            }
            
            _spawnedMinion.transform.position = Vector3.Lerp(_spawnedMinion.transform.position, _position, 10f * Time.deltaTime);
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_spawnedMinion || !_pressed)
            return;
        
        var position = Input.mousePosition;
        position.z = position.z - _camera.transform.position.z;
        position.y = 3f;
        _spawnedMinion = Instantiate(minionSample, _camera.ScreenToWorldPoint(position), Quaternion.identity);
        _spawnedMinion.GetComponent<EnemyHealthController>().StartHealth = (int) maxHP;
        _spawnedMinion.GetComponent<MinionController>().enabled = false;
        _spawnedMinion.GetComponent<MinionDescriptor>().SetParameters(type, maxHP, power);
        
        GameManager.Instance.TurnOffAllOutline();
        islandController.TurnOutline(true);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
        GameManager.Instance.PopupMenuOpened = false;
        
        if (!_spawnedMinion)
            return;
        
        RaycastHit[] hits = Physics.RaycastAll(_spawnedMinion.transform.position, -_spawnedMinion.transform.up);

        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<IslandController>() == islandController)
            {
                if (islandController.MinionsCount < islandController.maxMinions)
                {
                    if (GameManager.Instance.BuyMinion(cost))
                    {
                        islandController.AddMinion(_spawnedMinion);
                        islandController.MinionsCount++;
                        _spawnedMinion.transform.position = new Vector3(_spawnedMinion.transform.position.x, -0.148f, _spawnedMinion.transform.position.z);
                        _spawnedMinion.transform.parent = islandController.gameObject.transform;
                        _spawnedMinion.GetComponent<MinionDescriptor>().WhenPlacedOnWorking();
                        _spawnedMinion.GetComponent<MinionController>().enabled = true;
                        
                        var laser = _spawnedMinion.GetComponent<Laser>();
                        laser.gunPoint.gameObject.SetActive(false);
                        laser.pause = true;
                        
                        _spawnedMinion = null;
                             
                        UpdateCost();
                            
                        GameManager.Instance.TurnOffAllOutline();
                        _pressed = false;
                        return;
                    }
                    else
                    {
                        Debug.Log("НЕДОСТАТОЧнО СФЕР ДЛЯ ПОКУПКИ");
                        //TODO: НЕДОСТАТОЧнО СФЕР ДЛЯ ПОКУПКИ
                    }
                }
                else
                {
                    Debug.Log("НЕДОСТАТОЧнО МЕСТА НА ОСТРОВЕ");
                    //TODO: НЕДОСТАТОЧнО МЕСТА НА ОСТРОВЕ
                }
            }
        }
                
        GameManager.Instance.TurnOffAllOutline();
        _pressed = false;
        Destroy(_spawnedMinion);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        GameManager.Instance.PopupMenuOpened = true;
    }

    private void UpdateCost()
    {
        cost *= 1.3f;
    }

    public void MouseEnter()
    {
        GameManager.Instance.PopupMenuOpened = true;
        gameObject.GetComponent<Image>().sprite = img_1;
    }

    public void MouseExit()
    {
        if (!_pressed)
            GameManager.Instance.PopupMenuOpened = false;
        gameObject.GetComponent<Image>().sprite = img_2;
    }
}