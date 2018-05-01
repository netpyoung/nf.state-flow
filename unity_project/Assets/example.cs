using UnityEngine;
using UnityEngine.UI;
using HelloFsm.AutoGen;
using NF.StateMachine.Interface;

public class example : MonoBehaviour {

    HelloFsmRunner runner;

    Text txt_state;
    Text txt_current;
    Button btn_generate;
    Image img_generate;
    Image img_display;

    void Start ()
    {
        this.txt_current = transform.Find(nameof(txt_current)).GetComponent<Text>();
        this.txt_state = transform.Find(nameof(txt_state)).GetComponent<Text>();
        this.btn_generate = transform.Find(nameof(btn_generate)).GetComponent<Button>();
        this.img_generate = transform.Find(nameof(img_generate)).GetComponent<Image>();
        this.img_display = transform.Find(nameof(img_display)).GetComponent<Image>();

        this.btn_generate.onClick.AddListener(OnBtnGenerate);
        HelloFsmRunner runner = this.runner = new HelloFsmRunner(this.img_generate, this.img_display);
        runner.OnStateChanged += OnStateChanged;
        runner.Init();
	}

    private void OnBtnGenerate()
    {
        var state = this.runner.CurrentState as GenerateColor;
        if (state == null)
        {
            return;
        }
        state.Gen();
    }

    private void OnStateChanged(IState arg1, IState arg2)
    {
        this.txt_state.text = $"{arg1} -> {arg2}";
    }

    void Update ()
    {
        this.txt_current.text = $"{runner.Current}";
        runner.Tick(Time.deltaTime);
    }
}

