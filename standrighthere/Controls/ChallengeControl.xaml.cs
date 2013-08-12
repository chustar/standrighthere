using System;
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
    public partial class ChallengeControl : UserControl
    {
        public ChallengeControl()
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
