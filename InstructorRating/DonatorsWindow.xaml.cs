using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace InstructorRating
{
    /// <summary>
    /// Interaction logic for LecturersWindow.xaml
    /// </summary>
    public partial class DonatorsWindow : Window
    {
        const string FileName = "donators.txt";
        List<Donator> _donators = new List<Donator>();
        List<Charity> _charities = new List<Charity>();
        public DonatorsWindow() {
            InitializeComponent();

            // Загружаем данные из файла
            LoadData();
        }

        private void RefreshListBox() {
            listBoxDonators.ItemsSource = null;
            listBoxDonators.ItemsSource = _donators;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e) {
            var window = new NewDonatorWindow(_charities);
            if (window.ShowDialog().Value) {

                _donators.Add(window.NewDonator);

                SaveData();
                RefreshListBox();
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e) {
            if (listBoxDonators.SelectedIndex != -1) {
                _donators.RemoveAt(listBoxDonators.SelectedIndex);
                SaveData();
                RefreshListBox();
            }
        }

        private void listBoxDonators_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // If selected index = -1, we set IsEnabled to false
            buttonRemove.IsEnabled = listBoxDonators.SelectedIndex != -1;
        }

        private void SaveData() {
            using (var sw = new StreamWriter(FileName)) {
                foreach (var lect in _donators) {
                    sw.WriteLine($"{lect.Name}:{lect.Sum}:{lect.Charity.Name}:{lect.Charity.Address}");
                }
            }
        }

        private void LoadData() {
            try {
                _donators = new List<Donator>();
                _charities = new List<Charity>();

                using (var sr = new StreamReader(FileName)) {
                    while (!sr.EndOfStream) {
                        var line = sr.ReadLine();
                        var parts = line.Split(':');
                        if (parts.Length == 4) {

                            int i = 0;
                            while (i < _charities.Count && _charities[i].Name != parts[2])
                                i++;
                            Charity f;
                            if (i < _charities.Count)
                                f = _charities[i];  // Use existing sphere
                            else {
                                // Create a new sphere and add it to the list
                                f = new Charity(parts[2], parts[3]);
                                _charities.Add(f);
                            }

                            var donator = new Donator(parts[0], int.Parse(parts[1]));
                            donator.Charity = f;
                            _donators.Add(donator);
                        }
                    }

                    
                }
            }
            catch (FileNotFoundException) {
                // Файла с данными нет, благотворительные акции по умолчанию, чтобы можно было
                // выбирать его при добавлении пожертвований
                _charities.Add(new Charity("Озеленение города", "Фонд 'Леса России'"));
                _charities.Add(new Charity("Помощь вымирающим животным", "Красная книга России"));
                _charities.Add(new Charity("Лечение тяжелых заболеваний", "Фонд 'Подари жизнь'"));
                _charities.Add(new Charity("Помощь детским домам", "Фонд 'Подари жизнь'"));
            }
            catch {
                MessageBox.Show("Ошибка чтения из файла");
            }
            RefreshListBox();
        }
    }
}
