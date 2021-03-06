﻿using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Calculator.Ast;
using Calculator.Win.Properties;

namespace Calculator.Win {
    public partial class CalculatorForm : Form {
        public CalculatorForm() {
            InitializeComponent();
        }

        private void Update(TreeNode tn, string key, Node value) {
            key = key.ToLower();
            tn.Text = !string.IsNullOrEmpty(key) ?  $"{key} = {value}" : value.ToString();
            foreach (var e in value) {
                var ctn = tn.Nodes.Add("");
                Update(ctn, e.Key, e.Value);
            }
        }

        private void SetExpText(string src) {
            tree.BeginUpdate();
            try {
                tree.Nodes.Clear();
                var root = tree.Nodes.Add("root");
                Exp ret = Calc.Parse(src);
                Update(root, "", ret);
                root.Expand();
                txtLog.ForeColor = SystemColors.ControlText;
                txtLog.Text = ret.Value().ToString(CultureInfo.InvariantCulture);
            } catch (Exception exception) {
                txtLog.ForeColor = Color.Red;
                txtLog.Text = exception.Message;
                tree.Nodes.Clear();
                tree.Nodes.Add("Error");
            }
            tree.EndUpdate();
        }

        private void txtSrc_TextChanged(object sender, EventArgs e) {
            SetExpText(txtSrc.Text);
        }

        private void CalculatorForm_Load(object sender, EventArgs e) {
            txtSrc.Focus();
            txtSrc.SelectAll();
            SetExpText(txtSrc.Text);
        }

        private void CalculatorForm_FormClosed(object sender, FormClosedEventArgs e) {
            Settings.Default.Save();
        }
    }
}