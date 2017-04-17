using System.Collections.Generic;
using System.Windows;

namespace InstructorRating
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class NewDonatorWindow : Window
    {      

        public NewDonatorWindow(List<Charity> charities) {
            InitializeComponent();

            comboBoxCharities.ItemsSource = charities;
        }

        Donator _newDonator;

        public Donator NewDonator {
            get {
                return _newDonator;
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e) {
            int sum;
            if (string.IsNullOrWhiteSpace(textBoxFio.Text)) {
                MessageBox.Show("Необходимо ввести фамилию");
                textBoxFio.Focus();
                return;
            }
            if (!int.TryParse(textBoxSum.Text, out sum)) {
                MessageBox.Show("Некорректное значение суммы");
                textBoxSum.Focus();
                return;
            }
            if (sum < 0) {
                MessageBox.Show("Сумма должна быть больше 0");
                textBoxSum.Focus();
                return;
            }

            if (comboBoxCharities.SelectedItem == null) {
                MessageBox.Show("Необходимо выбрать благотворительную акцию");
                comboBoxCharities.Focus();
                return;
            }
            
            _newDonator = new Donator(textBoxFio.Text,
                sum);
            _newDonator.Charity = comboBoxCharities.SelectedItem as Charity;
            // Close current window
            DialogResult = true;
        }
    }
}
