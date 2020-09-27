﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            labelFullName.Text = Names.FullName;
            buttonAdd.Text = Names.Add;

            listBoxNames.DataSource = users;
            listBoxNames.ValueMember = "ID";
            listBoxNames.DisplayMember = "FullName";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = textBoxFullName.Text,
            };
            users.Add(u);
        }
    }
}
