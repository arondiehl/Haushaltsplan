using System;
using GLib;
using Gtk;

namespace Haushaltsplan
{
    public class PaymentsWindow
    {
        public PaymentsWindow()
        {
            Window window = new Window("Haushaltsplan");
            window.SetSizeRequest(500, 200);
            window.DeleteEvent += DeleteEvent;
            window.BorderWidth = 20;

            Table table = new Table(2, 2, true);
            window.Add(table);


            ListStore expensesStore = new ListStore(
                                        typeof(string),
                                        typeof(Frequency),
                                        typeof(DateTime),
                                        typeof(DateTime)
                                    );
            TreeView expensesView = new TreeView(expensesStore);
            expensesView.AppendColumn("Beschreibung", new CellRendererText(), "text", 0);
            ListStore frequencyStore = new ListStore(typeof(Frequency), typeof(string));
            frequencyStore.AppendValues(Frequency.Once, "einmalig");
            frequencyStore.AppendValues(Frequency.Daily, "täglich");
            CellRendererCombo frequencyCell = new CellRendererCombo
            {
                Model = frequencyStore,
                Editable = true,
                HasEntry = false,
                TextColumn = 1

            };
            frequencyCell.Edited += HandleEditedHandler;
            frequencyCell.AddNotification("Text", FrequencyTextChanged);
            TreeViewColumn frequencyColumn = new TreeViewColumn
            {
                Title = "Frequenz"
            };
            frequencyColumn.PackStart(frequencyCell, false);
            frequencyColumn.AddAttribute(frequencyCell, "text", 1);
            frequencyColumn.SetCellDataFunc(frequencyCell, HandleTreeCellDataFunc);
            expensesView.AppendColumn(frequencyColumn);

            expensesStore.AppendValues("Zahlung 1", Frequency.Once, DateTime.Parse("2018-12-24"));
            

            table.Attach(expensesView, 0, 1, 0, 1);

            expensesView.Show();

            /* Create second button */

            Button button2 = new Button("button 2");

            /* When the button is clicked, we call the "callback" function
             * with a pointer to "button 2" as its argument */

            button2.Clicked += Callback;

            /* Insert button 2 into the upper right quadrant of the table */
            table.Attach(button2, 1, 2, 0, 1);

            button2.Show();

            /* Create "Quit" button */
            Button quitbutton = new Button("Quit");

            /* When the button is clicked, we call the "delete_event" function
             * and the program exits */
            quitbutton.Clicked += ExitEvent;

            /* Insert the quit button into the both
             * lower quadrants of the table */
            table.Attach(quitbutton, 0, 2, 1, 2);

            quitbutton.Show();

            table.Show();
            window.ShowAll();
        }

        private void FrequencyTextChanged(object o, NotifyArgs args)
        {

        }

        void HandleEditedHandler(object o, EditedArgs args)
        {
            CellRendererCombo cell = (CellRendererCombo)o;
            cell.Text = args.NewText;
        }


        void HandleTreeCellDataFunc(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter)
        {

        }


        private static void Callback(object obj, EventArgs args)
        {
            Button mybutton = (Button)obj;
            Console.WriteLine("Hello again - {0} was pressed", (string)mybutton.Label);
            // Have to figure out, how to recieve button name
        }

        /* another event */
        private static void DeleteEvent(object obj, DeleteEventArgs args)
        {
            Application.Quit();
        }

        static void ExitEvent(object obj, EventArgs args)
        {
            Application.Quit();
        }
    }
}
