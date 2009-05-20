using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Nathandelane.Writing.WordTool
{
    public partial class MainForm : Form
    {
        #region Fields

        private OpenFileDialog _openFileDialog;
        private IList<string> _wordCollection;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            _openFileDialog = new OpenFileDialog();
            _wordCollection = new List<string>();
        }

        private void OpenWordFile(object sender, EventArgs e)
        {
            DialogResult result = _openFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                _wordFileTextBox.Text = _openFileDialog.FileName;
                _wordCollection.Clear();

                using (StreamReader reader = new StreamReader(_wordFileTextBox.Text))
                {
                    string line = String.Empty;
                    while (!String.IsNullOrEmpty(line = reader.ReadLine()))
                    {
                        _wordCollection.Add(line);
                    }
                }

                MessageBox.Show(this, String.Format("{0} words were read in from word file.", _wordCollection.Count));
            }
        }

        private void RunQuery(object sender, EventArgs e)
        {
            string queryText = String.Empty;

            if (!String.IsNullOrEmpty(_queryTextBox.Text) && _wordCollection.Count > 0)
            {
                Cursor = Cursors.WaitCursor;

                if (!String.IsNullOrEmpty(_queryTextBox.SelectedText))
                {
                    queryText = _queryTextBox.SelectedText;
                }
                else
                {
                    queryText = _queryTextBox.Text;
                }

                try
                {
                    Regex regex = new Regex(queryText);

                    var results = from word in _wordCollection
                                  where regex.IsMatch(word)
                                  select word;

                    _resultsListBox.Items.Clear();
                    _resultsListBox.Items.AddRange(results.ToArray<string>());
                    _resultsLabel.Text = String.Format("Results: {0} words found!", results.Count<string>());
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "There was a problem with your query.");
                }
            }
            else if (_wordCollection.Count == 0)
            {
                MessageBox.Show(this, "You must load a word file before you may run a query.");
            }

            Cursor = Cursors.Default;
        }
    }
}
