using System;
using System.Linq;
using System.Windows.Forms;

namespace Larp.winforms
{
    // Prevents the same screen from opening twice.
    // If the target form is already open underneath the current one,
    // the current form just closes and the one below becomes active again.
    // Otherwise the target opens on top of the current form.
    public static class FormNavigator
    {
        public static void GoTo<T>(Form current, Func<T> create) where T : Form
        {
            var existing = Application.OpenForms
                .OfType<T>()
                .FirstOrDefault(f => !f.IsDisposed && !ReferenceEquals(f, current));

            if (existing != null)
            {
                current.Close();
                return;
            }

            using var form = create();
            form.ShowDialog(current);
        }
    }
}