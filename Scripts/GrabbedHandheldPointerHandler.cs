using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autohand;
using UnityEngine;

public class GrabbedHandheldPointerHandler : MonoBehaviour
{
    [SerializeField]
    private HandheldRemotePointer _remotePointer;
    [SerializeField]
    private AutoHandPlayer _player;
    [Header("serialized Hand for debugging")]
    [SerializeField]
    private Hand _grabbedHand;
    [SerializeField]
    private Grabbable _pointerGrabbable;
    [SerializeField]
    private Transform _leftHandRemoteSpawnPoint, _rightHandRemoteSpawnPoint;
    private GrabLock _grabLock;
    public bool _shouldAbsolutelyLockToHand = false;
    private Coroutine _RemoteContinuousGrabber = null;
    private bool _isGrabbingFirstTime = true;
    public static GrabbedHandheldPointerHandler instance;
    private void Awake() {
        instance = this;
    }
    private void Start()
    {
        _player.handLeft.OnGrabbed += OnGrabbed;
        _player.handRight.OnGrabbed += OnGrabbed;
        _pointerGrabbable.onRelease.AddListener(OnRemoteReleased);
        _pointerGrabbable.onGrab.AddListener(OnPointerGrabbed);
    }
    public void OnRemoteReleased(Hand hand, Grabbable grabbable)
    {
        //Destroy(_pointerGrabbable.GetComponent<GrabLock>());
       /* if (_shouldAbsolutelyLockToHand)
        {
            if (_RemoteContinuousGrabber == null)
            {
                _RemoteContinuousGrabber = StartCoroutine(IContinuouslyGrabRemote());
            }
        }*/
    }
    public void SetPointedIntroducerProperty(PointedObjectIntroducerProperty pointedObjectIntroducerProperty){
        _remotePointer.SetPointedObjectIntroducer(pointedObjectIntroducerProperty);
    }
    private void OnPointerGrabbed(Hand hand, Grabbable grabbable)
    {
        if (_isGrabbingFirstTime)
        {
            _shouldAbsolutelyLockToHand = true;
            _RemoteContinuousGrabber = StartCoroutine(IContinuouslyGrabRemote());
            _grabLock = _pointerGrabbable.gameObject.AddComponent<GrabLock>();
            _isGrabbingFirstTime = false;
        }
        //_grabLock = _pointerGrabbable.gameObject.AddComponent<GrabLock>();
    }
    private void OnGrabbed(Hand hand, Grabbable grabbable)
    {
        if (grabbable == _pointerGrabbable)
        {
            _grabbedHand = hand;
            _player.handLeft.OnGrabbed -= OnGrabbed;
            _player.handRight.OnGrabbed -= OnGrabbed;
        }
    }
    public void FreeGrabbedHand()
    {
        _grabbedHand.Release();
        Destroy(_grabLock);
        _pointerGrabbable.gameObject.SetActive(false);
        _shouldAbsolutelyLockToHand = false;
        _RemoteContinuousGrabber = null;
        
    }
    public async void AddControllerToGrabbedHand()
    {
        if(_grabbedHand == _player.handLeft)
        {
            _pointerGrabbable.transform.position = _leftHandRemoteSpawnPoint.position;
        }
        else
        {
            _pointerGrabbable.transform.position = _rightHandRemoteSpawnPoint.position;

        }
        _pointerGrabbable.gameObject.SetActive(true);
        //_grabbedHand.CreateGrabConnection(_pointerGrabbable);
        await Task.Delay(50);
        _grabbedHand.Grab();
        _shouldAbsolutelyLockToHand = true;
        if (_grabLock == null)
        {
            _grabLock = _pointerGrabbable.gameObject.AddComponent<GrabLock>();
        }
        if (_RemoteContinuousGrabber == null)
            _RemoteContinuousGrabber = StartCoroutine(IContinuouslyGrabRemote());
       //_grabbedHand.ForceGrab(_pointerGrabbable);
    }
    private IEnumerator IContinuouslyGrabRemote()
    {
        while (_shouldAbsolutelyLockToHand)
        {
            if (!_pointerGrabbable.IsHeld())
            {
                AddControllerToGrabbedHand();
            }
            yield return null;
        }
        _RemoteContinuousGrabber = null;
        yield return null;
    }
}
