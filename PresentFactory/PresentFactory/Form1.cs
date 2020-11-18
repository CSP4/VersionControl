﻿using PresentFactory.Abstractions;
using PresentFactory.Entities;
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
        private List<Toy> _toys = new List<Toy>();
        private Toy _nextToy;

        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set 
            { 
                _factory = value;
                DisplayNext();
            }
        }

        private void DisplayNext()
        {
            if (_nextToy != null) mainPanel.Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        public Form1()
        {
            InitializeComponent();

            Factory = new BallFactory
            {
                BallColor = button3.BackColor
            };

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            mainPanel.Controls.Add(toy);
            toy.Left = -toy.Width;
        }

        private void conveyorTime_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var toy in _toys)
            {
                toy.MoveToy();
                if (toy.Left>maxPosition)
                {
                    maxPosition = toy.Left;
                }
            }

            if (maxPosition >= 1000)
            {
                var oldestToy = _toys[0];
                _toys.Remove(oldestToy);
                mainPanel.Controls.Remove(oldestToy);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory
            {
                BallColor = button3.BackColor
            };
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cd = new ColorDialog();

            cd.Color = button3.BackColor;
            if (cd.ShowDialog() != DialogResult.OK) return;
            button.BackColor = cd.Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Factory = new PresentFactoryCS
            {
                PresentMainColor = button5.BackColor,
                PresentLineColor = button6.BackColor
            };
        }

        private void BallMoveTimer_Tick(object sender, EventArgs e)
        {
            foreach (Ball balls in _toys)
            {
                balls.Top += 1;   
            }
        }
    }
}
