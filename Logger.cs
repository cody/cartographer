using System;
using System.Windows.Forms;

namespace cartographer
{
    public static class Logger
    {
        private static TextBox textBox;
        private static int errorCounter;

        public static void init(TextBox tb)
        {
            textBox = tb;
        }

        public static void error(string s)
        {
            errorCounter++;
            log("Error: " + s);
        }

        public static void log(string s)
        {
            textBox.AppendText(s + Environment.NewLine);
        }

        public static void clear()
        {
            textBox.Clear();
            textBox.Update();
            errorCounter = 0;
        }

        public static void reportErrors()
        {
            log("Number of errors: " + errorCounter);
        }
    }
}
