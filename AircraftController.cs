using UnityEngine;

public class AircraftController : MonoBehaviour
{
    public float ForwardSpeed = 10f; // 飛機向前速度
    public float ControllSpeed = 5f; // 上下左右速度

    public AudioSource audioSource; // 音效來源
    public AudioClip flySound; // 飛行
    public AudioClip crashSound; // 撞擊
    public AudioClip passSound; // 通過

    private bool isCrash = false; // 有沒有撞到
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 遊戲開始就撥放飛行音效
        if(audioSource != null && flySound != null) 
        {
            audioSource.clip = flySound;// 飛行放入音檔
            audioSource.loop = true; // 循環播放
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrash) return; // 撞到就不能再控制飛機

        transform.Translate(Vector3.forward * ForwardSpeed * Time.deltaTime); // 飛機向前移動

        float LF = Input.GetAxis("Horizontal"); // 左右移動
        float UD = Input.GetAxis("Vertical"); // 上下移動

        transform.Translate(new Vector3(LF, UD, 0) * ControllSpeed * Time.deltaTime); // 飛機上下左右移動
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isCrash) return;

        isCrash = true; // 撞到了

        // 停止飛行聲，撥撞擊聲
        if(audioSource != null && crashSound != null) 
        {
            audioSource.Stop();
            audioSource.PlayOneShot(crashSound);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isCrash) return;

        // 撥通過聲
        if(audioSource != null && passSound != null) 
        {
            audioSource.PlayOneShot(passSound);
        }
    }
}
