﻿using PresentFactory.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentFactory
{
    public partial class Form1 : Form
    {
        private List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;
        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();

            Factory = new BallFactory();
            
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _balls.Add(ball);
            mainPanel.Controls.Add(ball);
            ball.Left = -ball.Width;
        }

        private void conveyorTime_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var ball in _balls)
            {
                ball.MoveBall();
                if (ball.Left>maxPosition)
                {
                    maxPosition = ball.Left;
                }
            }

            if (maxPosition >= 1000)
            {
                var oldestBall = _balls[0];
                _balls.Remove(oldestBall);
                mainPanel.Controls.Remove(oldestBall);
            }
        }
    }
}