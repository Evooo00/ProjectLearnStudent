const meter = document.getElementById("progress--circle");
const meterProgress = document.getElementById("meter--progress");
const ranger = document.getElementById("meter--ranger");
const insideText = document.getElementById("progress--text");

ranger.addEventListener("input", e => {
    const rangeValue = e.target.value;

    meterProgress.innerText = `${rangeValue}%`;
    insideText.textContent = `${rangeValue}%`;
    meter.style.strokeDashoffset = 100 - rangeValue;

    if (rangeValue === "0") {
        meter.style.stroke = "none";
    } else {
        meter.style.stroke = "#28411B";
    }
});

ranger.addEventListener("wheel", e => {
    e.preventDefault();

    const isWheelPositive = () => e.deltaY > 0;

    if (isWheelPositive()) {
        let rangerValue = +ranger.value;
        ranger.value = rangerValue += 1;
    } else {
        ranger.value -= 1;
    }

    // Trigger the above (:6) event listener manually  
    ranger.dispatchEvent(new Event("input"));
});

document.addEventListener("DOMContentLoaded", () => {
    ranger.dispatchEvent(new Event("input"));
});


/*
using System;
using System.Windows.Forms;
using System.Drawing;

public class MyForm : Form
{
    private ProgressBar meter;
    private Label meterProgress;
    private TrackBar ranger;
    private Label insideText;

    public MyForm()
    {
        meter = new ProgressBar();
        meterProgress = new Label();
        ranger = new TrackBar();
        insideText = new Label();

        // Add your controls to the form
        this.Controls.Add(meter);
        this.Controls.Add(meterProgress);
        this.Controls.Add(ranger);
        this.Controls.Add(insideText);

        ranger.Scroll += new EventHandler(Ranger_Scroll);
        this.Load += new EventHandler(MyForm_Load);
    }

    private void Ranger_Scroll(object sender, EventArgs e)
    {
        int rangeValue = ranger.Value;

        meterProgress.Text = $"{rangeValue}%";
        insideText.Text = $"{rangeValue}%";
        meter.Value = 100 - rangeValue;

        if (rangeValue == 0) {
            meter.ForeColor = Color.Transparent;
        }
        else {
            meter.ForeColor = Color.FromArgb(40, 65, 27);
        }
    }

    private void MyForm_Load(object sender, EventArgs e)
    {
        Ranger_Scroll(null, null);
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MyForm());
    }
}
