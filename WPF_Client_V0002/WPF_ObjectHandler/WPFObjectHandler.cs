using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_CrossComm_Client
{
    internal class clientWPFObjectHandler
    {
        public Grid CreateGrid(int rowNum, int colNum, HorizontalAlignment horAll, VerticalAlignment verAll, bool showGridLines, string name)
        {
            Grid grid = new Grid();

            RowDefinition[] rowDef = new RowDefinition[rowNum];
            ColumnDefinition[] colDef = new ColumnDefinition[colNum];

            grid.Name = name;
            grid.HorizontalAlignment = horAll;
            grid.VerticalAlignment = verAll;
            grid.ShowGridLines = showGridLines;

            for (int i = 0; i < rowNum; i++)
            {
                rowDef[i] = new RowDefinition();
                rowDef[i].Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowDef[i]);
            }

            for (int i = 0; i < colNum; i++)
            {
                colDef[i] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(colDef[i]);
            }

            return grid;
        }

        public TextBox[] CreateTextBoxArray(int numOfTextBox, int fontSize, bool isReadOnly)
        {
            TextBox[] TextBoxes = new TextBox[numOfTextBox];
            for (int i = 0; i < numOfTextBox; i++)
            {
                TextBoxes[i] = new TextBox();
                TextBoxes[i].Text = "";
                TextBoxes[i].HorizontalAlignment = HorizontalAlignment.Stretch;
                TextBoxes[i].VerticalAlignment = VerticalAlignment.Stretch;
                TextBoxes[i].HorizontalContentAlignment = HorizontalAlignment.Center;
                TextBoxes[i].VerticalContentAlignment = VerticalAlignment.Center;
                //TextBoxes[i].Height = 27;
                //TextBoxes[i].Width= 300;
                //TextBoxes[i].Margin= 42,360,0,0;
                TextBoxes[i].TextWrapping = TextWrapping.Wrap;
                TextBoxes[i].FontStretch = FontStretches.UltraExpanded;
                TextBoxes[i].Height = Double.NaN;
                TextBoxes[i].Width = Double.NaN;
                TextBoxes[i].FontSize = fontSize;
                TextBoxes[i].IsReadOnly = isReadOnly;
                TextBoxes[i].Name = "WriteVariableTextBoxe_" + "i";
            }
            return TextBoxes;
        }

        public Label createLabel(string content, FontFamily fontFam, int fontSize, FontWeight fontWeight, HorizontalAlignment horAll, VerticalAlignment verAll)
        {
            Label label = new Label();

            label.Content               = content;
            label.FontFamily            = fontFam;
            label.FontSize              = fontSize;
            label.FontWeight            = fontWeight;
            label.HorizontalAlignment   = horAll;
            label.VerticalAlignment     = verAll;
            label.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            label.VerticalContentAlignment = VerticalAlignment.Stretch;

            return label;
        }

        /// <summary>
        /// copys the Strings from the String array to the TextBox array (part Of array 1--> variables,2-->values)
        /// first string in a string array is server command, last string is a length
        /// </summary>
        /// <param name="Ta"></param>
        /// <param name="Sa"></param>
        public void AssignReadValToTextBoxArray(TextBox[] Ta, String[] Sa, int partOfSarray, int numOfVar)
        {
            String[] a_firstPart = new String[numOfVar];
            String[] a_secondPart= new String[numOfVar];

            Array.ConstrainedCopy(Sa,1, a_firstPart,0, numOfVar);
            Array.ConstrainedCopy(Sa, numOfVar+1, a_secondPart,0, numOfVar);

            if (partOfSarray == 1)
            {
                for (int i = 1; i < a_firstPart.Length; i++)
                {
                    if (a_firstPart[i] == "empty")
                    {
                        Ta[i].Text = "";
                    }
                    else
                    {
                        Ta[i].Text = a_firstPart[i];
                    }
                }
            }

            if (partOfSarray == 2)
            {
                for (int i = 0; i < a_secondPart.Length; i++)
                {
                    if (a_secondPart[i] == "empty")
                    {
                        Ta[i].Text = "";
                    }
                    else
                    {
                        Ta[i].Text = a_secondPart[i];
                    }
                }
            }
        }

        /// <summary>
        /// Creates TabControl with tabs for communication parameters(communication with KUKA VM machine)
        /// LoadParameters from PLC are the format values for loadparampal.src
        /// IO ReadWrite is a tab used to send KUKA through state machine steps
        /// </summary>
        /// <returns></returns>
        public TabControl Create_TabsControl()
        {
            TabControl Tabs = new TabControl();

            return Tabs; 
        }

        public TabItem Create_Tab(String Name)
        {
            TabItem tab = new TabItem();
            tab.Header = Name;
          
            return tab;
        }
    }
}