using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cricketers
{
	public partial class BlockWithBorder : UserControl
	{
        public string DisplayText { get; set; }
		public BlockWithBorder()
		{
			// Required to initialize variables
			InitializeComponent();
           
		}
	}
}