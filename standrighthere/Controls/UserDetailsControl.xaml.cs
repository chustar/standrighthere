﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace standrighthere.Controls
{
    public partial class UserDetailsControl : UserControl
    {
        public UserDetailsControl()
        {
            InitializeComponent();
            NewString = "solved";
        }

        public string NewString
        {
            get;
            set;
        }
    }
}