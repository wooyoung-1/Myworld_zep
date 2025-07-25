using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// 박스 콜라이더를 만들어 플레이어가 들어오면 인식시키고 해당 하위 자식스프라이트아웃라인을 켜는식으로 만듬
// https://cholol.tistory.com/438 아웃라인 쉐이더 코드는 이걸 참고함

public class ObjectEvent : MonoBehaviour
{
    public Animator ui;
    public Animator ui2;
    public Animator ui3;

    private Coroutine fadeOutCoroutine;

    bool isPlayerInRange = false;
    bool isPlayerInCinema = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        SpriteOutline outline = GetComponentInChildren<SpriteOutline>();

        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

            outline.enabled = true;
            Debug.Log("들어옴");

            ui.SetBool("On", true);
            ui.SetBool("Fade", true);

            HandleFadeOut();

        }
    }

    // 페이드 인 아웃 효과를 구현하기 위해 코루틴을 사용

    private void OnTriggerExit2D(Collider2D other)
    {
        SpriteOutline outline = GetComponentInChildren<SpriteOutline>();
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            outline.enabled = false;
            Debug.Log("나감");

            ui.SetBool("On", false);
            ui.SetBool("Fade", true);

            HandleFadeOut();
        }
    }

    private void Update()
    {
        if (!isPlayerInCinema && isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!GameManager.Instance.IsInDialogue)
            {

                GameManager.Instance.EnterDialogue();

                ui.SetBool("On", false);
                ui.SetBool("Fade", true);
                HandleFadeOut();

                StartCoroutine(FadeInCinema());

                Debug.Log("상호작용/대화");
            }
            else
            {
                GameManager.Instance.ExitDialogue();

                ui.SetBool("On", true);
                ui.SetBool("Fade", true);
                HandleFadeOut();

                StartCoroutine(FadeOutCinema());

                Debug.Log("대화 종료");
            }
            
        }
    }

    // 현재 코루틴중 다시 작동되는경우-> 들어갔다 나갔다 반복 시 코루틴이 꼬이는 증상을 방지하기위해 코루틴에 값이 있으면 새로 생성되게함 (스타트 코루틴)
    // 코드가 난잡해져서 마무리후 코드를 리팩토링 해바야할것 같다 cs나누기 등


    private void HandleFadeOut()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }

        fadeOutCoroutine = StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        ui.SetBool("Fade", true);

        yield return new WaitForSeconds(0.5f);

        ui.SetBool("Fade", false);
    }

    private IEnumerator FadeInCinema()
    {
        Text _Text = GameObject.Find("dialogText").GetComponentInChildren<Text>();
        ObjectData myData = GetComponent<ObjectData>();

        ui2.SetInteger("IsCinema", 1);
        _Text.text = myData.dialogue;
        isPlayerInCinema = true;

        yield return new WaitForSeconds(0.5f);
        ui3.SetInteger("IsDialog", 1);
        yield return new WaitForSeconds(0.5f);
        isPlayerInCinema = false;
    }

    private IEnumerator FadeOutCinema()
    {
        ui2.SetInteger("IsCinema", 2);
        ui3.SetInteger("IsDialog", 2);
        isPlayerInCinema = true;

        yield return new WaitForSeconds(1f);

        ui2.SetInteger("IsCinema", 3);
        ui3.SetInteger("IsDialog", 3);
        isPlayerInCinema = false;
    }



}


