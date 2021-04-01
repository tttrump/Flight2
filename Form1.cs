using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
     
        double y = 0;
        double t = 0;
        double x = 0;

        double bodyStartPosition;
        double bodyStartSpeed;
        double bodyStartAngle;
        double vx;
        double vy;

        double bodyWeight;
        double bodySquare;
        double k;

        Body body;

        const double dt = 0.01;
        const double g = 9.81;
        const double coefLobSopr = 0.15;
        const double plotnostAtmosphere = 1.29;
        private void btStart_Click(object sender, EventArgs e)
        {

            bodyStartPosition = (double)edHight.Value;
            bodyStartSpeed = (double)edSpeed.Value;
            bodyStartAngle = (double)edAngle.Value;

            bodySquare = (double)edSquare.Value;
            bodyWeight = (double)edWeight.Value;

            x = 0;
            y = bodyStartPosition;
            t = 0;

            vx = bodyStartSpeed * Math.Cos(bodyStartAngle * Math.PI / 180);
            vy = bodyStartSpeed * Math.Sin(bodyStartAngle * Math.PI / 180);

            body = new Body(
                (double)edWeight.Value,
                (double)edSquare.Value,
                0,
                (double)edHight.Value,
                bodyStartSpeed * Math.Cos(bodyStartAngle * Math.PI / 180),
                bodyStartSpeed * Math.Sin(bodyStartAngle * Math.PI / 180)
            );

            k = 0.5 * coefLobSopr * body.square * plotnostAtmosphere / body.weight;

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x , y);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            vx = vx - k*vx*Math.Sqrt(vx * vx + vy * vy)*dt;
            vy= vy -(g + k * vy * Math.Sqrt(vx * vx + vy * vy)) * dt;

            x = x + vx * dt;
            y = y + vy * dt;

            chart1.Series[0].Points.AddXY(x, y);
           
            if(y<=0) timer1.Stop();
        }   
    }

    class Body {
        public double weight;
        public double square;
        public double positionX;
        public double positionY;
        public double speedX;
        public double speedY;
        public Body(double weight, double square, double positionX, double positionY, double speedX, double speedY) {
            this.weight = weight;
            this.square = square;
            this.positionX = positionX;
            this.positionY = positionY;
            this.speedX = speedX;
            this.speedY = speedY;
        }
    }
}
