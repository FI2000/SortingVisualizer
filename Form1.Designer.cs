using System.ComponentModel;
using System.Windows.Forms;

namespace SortingVisualizer
{
    partial class Form1
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            Button startButton = new Button
            {
                Text = "Start",
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(75, 25)
            };
            startButton.Click += new System.EventHandler(this.StartButton_Click);
            Controls.Add(startButton);
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }
    }
}
