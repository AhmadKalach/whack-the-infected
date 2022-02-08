using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HammerSlam : MonoBehaviour
{
    public float rotateBackwardsValue;
    public float rotateBackwardsDuration;

    public float finalRotation;
    public float finalRotationDuration;

    public float rotateToInitialDuration;
    public LayerMask squashableLayer;

    public Transform pointOfHit;
    public float HitRadius;
    public ParticleSystem particleEffect;
    public List<AudioClip> slamClips;

    public static bool isSlamming;
    float startRotation;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation.eulerAngles.z;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSlamming)
        {
            Slam();
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Squashable>() != null) {
            Squashable _squashable = collision.gameObject.GetComponent<Squashable>();
            _squashable.Squash();
        }
    }
    */

    void Slam()
    {
        isSlamming = true;
        Sequence sequence = DOTween.Sequence();
        Tween RotateUp = transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z - rotateBackwardsValue), rotateBackwardsDuration);
        RotateUp.OnComplete(() => {
            Collider[] hitColliders = Physics.OverlapSphere(pointOfHit.position, HitRadius);

            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.GetComponent<Squashable>() != null)
                {
                    Squashable _squashable = collider.gameObject.GetComponent<Squashable>();
                    _squashable.Squash();
                }
            }
        });
        
        sequence.Append(RotateUp);
        Tween rotateDownwards = transform.DORotate(new Vector3(0, 0, finalRotation), finalRotationDuration).SetEase(Ease.OutCubic);
        sequence.Append(rotateDownwards);
        rotateDownwards.OnComplete(() => PlayEffects());

        sequence.Append(transform.DORotate(new Vector3(0, 0, startRotation), rotateToInitialDuration).SetEase(Ease.OutBack));

        sequence.OnComplete(() => isSlamming = false);
        sequence.Play();
    }

    void PlayEffects()
    {
        particleEffect.Play();
        if (slamClips.Count > 0)
        {
            audioSource.clip = slamClips[Random.Range(0, slamClips.Count)];
            audioSource.Play();
        }
    }
}
