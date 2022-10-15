﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using mshtml;
using System.Text.RegularExpressions;

namespace CSharpTextEditor
{
    public partial class Form1 : Form
    {
        private bool bCompleted = false;
        private bool bOnce = false;

        private InputManager inputManager;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Focus();
            HtmlViewer.DocumentText = 
                "<html>" +
                "<head>" +
                "<style>" +
                ".page-body {" +
                    "position: relative;" +
                    "padding: 1cm;" + // make changeable
                    "margin: 30px;" +
                    "height: 300px;" + // as well
                    "overflow-y: auto;" +
                    "word-wrap: break-word;" +
                    "background-color: white;" +
                "}" + // best to isоlate the style string
                ".page-container {" +
                    "position: relative;" +
                "}" +
                ".editguard {" +
                    "-ms-user-select: none;" +
                "}" +
                "</style>" +
                "</head>" +
                "<body style=\"background-color: gray; -ms-user-select: none;\">" +
                    "<div class=\"editguard page-container\">" +
                            "<div class=\"page-body\">" +
                            "</div>"+
                    "</div>" +
                "</body>" +
                "</html>";

            bCompleted = true;
            inputManager = new InputManager(HtmlViewer.Document);
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void HtmlViewer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (bCompleted && !bOnce)
            {
                HtmlViewer.Document.Click += OnDocumentGlobalClick;
                bOnce = true;
            }
        }

        private void InsertPageBtn_Click(object sender, EventArgs e)
        {
            //pageContainer.InsertPageAfterActive();
        }

        private void HtmlViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            inputManager.OnKeyDown(sender, e);
        }

        private void OnDocumentGlobalClick(object sender, HtmlElementEventArgs e)
        {
            inputManager.OnDocumentGlobalClick(sender, e);
        }
    }
}
