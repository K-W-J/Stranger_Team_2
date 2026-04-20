using System.Linq;
using _01_Work.HS.Core.Map;
using _01_Work.KWJ._01_Scripes.WorkingUnit;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace _01_Work.LCM._01.Scripts.BuildResources.Resource
{
    public abstract class Resource : MonoBehaviour, IPlaceable, IResource
    {
        [Header("ResourceSO")] [SerializeField]
        protected ResourceSO resourceSO;
        private Ground _ground;

        [Header("Settings")] public bool IsGrowEnd { get; set; }

        public int _hp;
        [SerializeField] private Transform selectBox;
        [SerializeField] private GameObject selectTool;
        private bool _isSelected;

        [SerializeField] private GameObject item;

        public UnityEvent OnHitResourceEvent;
        public UnityEvent OnDeadResourceEvent;

        private Material _mat;

        protected virtual void Awake()
        {
            _hp = resourceSO.resourceHp;
            _mat = selectBox.GetComponent<MeshRenderer>().material;
        }

        private void Start()
        {
            IsGrowEnd = false;
        }
        
        public void Setup(Ground ground)
        {
            _ground = ground;
        }

        public bool CanTakeResource()
        {
            if (_isSelected) return false;
            return IsGrowEnd && _hp > 0;
        }

        public void HitResource(int damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                if(BuildResourceManager.Instance != null)
                    DropResourceItem();
            }
            else if(_hp != resourceSO.resourceHp)
            {
                transform.DOShakePosition(0.5f, 0.05f);
                OnHitResourceEvent?.Invoke();
            }
        }

        public void DragResource()
        {
            if (_isSelected) return;
            
            _isSelected = true;
            selectTool.SetActive(_isSelected);
            
            _mat.DOFade(0f, 0.7f);
            selectBox.DOScaleY(35f, 0.8f).OnComplete(() =>
            {
                _mat.DOFade(1f, 0.1f);
                selectBox.DOScaleY(0.1f, 0.01f);
            });
        }

        private void DropResourceItem()
        {
            OnDeadResourceEvent?.Invoke();
            
            BuildResourceManager.Instance.takeResources.Enqueue(Instantiate(item,
                new Vector3(transform.position.x, 0.22f, transform.position.z), item.transform.rotation));
            WorkingUnitManager.Instance.SettingTakeResourceList();
            Destroy(gameObject);
            _ground.SetPlaceObject(null);
        }
    }
}