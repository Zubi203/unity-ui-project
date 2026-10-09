using UnityEngine;
using UnityEngine.EventSystems;

public class SliderSFXPlayer : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    [SerializeField] private AudioClip SFXSliderSound;
    [SerializeField] private AudioSource source;
    private bool isDragging = false;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            PlaySound();
        }
    }

    void PlaySound()
    {
        if (SFXSliderSound == null || source == null) {return;}

        source.PlayOneShot(SFXSliderSound);
    }
    
}
