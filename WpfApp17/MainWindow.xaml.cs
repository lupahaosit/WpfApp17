using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event Action<string> BT_Click;
        Random rnd = new Random(); 
        Programm pg = new Programm();
        TryCatch tryCatch = new TryCatch();
        ObservableCollection<string> Whathappends = new ObservableCollection<string>() {("start")};
        List<Abonent> abonents1;
        List<Abonent> abonents2;
        List<Deposite> deposites;
        List<NonDeposite> nonDeposites;
        string[] directory = new string[] {"Deposite", "NonDeposite"};
        int y;
        int z;
        int choose_send;
       
        int choose_get;
        int start;
        int end;
        int choosen_derictory;

        string Directory_Changed = "смена директории";
        public MainWindow()
        {
            InitializeComponent();
           
            deposites = pg.Deposites();
            nonDeposites = pg.NonDeposites();
            LV_NonDeposite.Visibility = Visibility.Hidden;
            LV_Deposite.Visibility = Visibility.Hidden;
            Button_Get.Visibility = Visibility.Hidden;
            finally_add.Visibility = Visibility.Hidden;
            #region создание списков
            IEnumerable<Abonent> Abonents1 = deposites;
            abonents1 = Abonents1.ToList();
            abonents2 = new List<Abonent>();
            foreach (var item in abonents1)
            {
                abonents2.Add(new Abonent(item.index, item.Name, rnd.Next(1000, 1000000)));
            }
            #endregion
            #region соурсы
            LV_Deposite.ItemsSource = abonents1;
            LV_NonDeposite.ItemsSource = abonents2;
            CB_Directory.ItemsSource = directory;
            CB_Directory.SelectedIndex = 0;
            CB_WhatHappend.ItemsSource = Whathappends;
            
            #endregion
        }
        public void Print(string text)
        {
            MessageBox.Show(text, "", MessageBoxButton.OK);
        }

        public void Directory(string text)// methdod
        {
            if (CB_Directory.SelectedValue == directory[0])
            {
                y = 0;
                LV_Deposite.Visibility = Visibility.Visible;
                LV_NonDeposite.Visibility = Visibility.Hidden;
                TB_GetterName.Visibility = Visibility.Hidden;
                TB_SenderName.Visibility = Visibility.Hidden;
                TB_GetterNameNonDeposite.Visibility = Visibility.Visible;

                TB_SenderNameNonDeposite.Visibility = Visibility.Visible;
            }
            if (CB_Directory.SelectedValue == directory[1])
            {
                y = 1;
                LV_Deposite.Visibility = Visibility.Hidden;
                LV_NonDeposite.Visibility = Visibility.Visible;

                TB_GetterName.Visibility = Visibility.Visible;
                TB_SenderName.Visibility = Visibility.Visible;

                TB_GetterNameNonDeposite.Visibility = Visibility.Hidden;
                TB_SenderNameNonDeposite.Visibility = Visibility.Hidden;
            }
            var g = (Directory_Changed);
            Whathappends.Add(g);
            BT_Click?.Invoke(text);
        }
        private void CB_Directory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Directory("text");
            
        }

        public void Money(string text)
        {
            TB_Money.Text = null;
            
        }//Method
        private void TB_Money_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            Money("text");
            Whathappends.Add(("введена сумма"));
            
        }

        public void CloseClick(string text)
        {

            switch (y)
            {
                case 0:
                    if (!(tryCatch.Close(LV_Deposite.SelectedIndex)))
                    {
                        MessageBox.Show("выберите объект");
                        return;
                    }
                    if (abonents1[LV_Deposite.SelectedIndex].Status == false)
                    {
                        abonents1[LV_Deposite.SelectedIndex].Status = true;
                    }
                    else
                    {
                        abonents1[LV_Deposite.SelectedIndex].Status = false;
                    }

                    break;
                case 1:
                    if (!(tryCatch.Close(LV_NonDeposite.SelectedIndex)))
                    {
                        MessageBox.Show("выберите объект");
                        return;
                    }
                    if (abonents2[LV_NonDeposite.SelectedIndex].Status == false)
                    {
                        abonents2[LV_NonDeposite.SelectedIndex].Status = true;
                    }
                    else
                    {
                        abonents2[LV_NonDeposite.SelectedIndex].Status = false;
                    }
                    break;
                default:
                    break;
            }
            ICollectionView view = CollectionViewSource.GetDefaultView(abonents1);
            view.Refresh();
            ICollectionView view1 = CollectionViewSource.GetDefaultView(abonents2);
            view1.Refresh();
            BT_Click?.Invoke(text);
        }//method
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            BT_Click += Print;
            CloseClick(" выбраный счёт закрыт");
            BT_Click -= Print;
            Whathappends.Add(("счёт закрыт"));
        }

         public void AugClick()//method
        {
            if (MessageBox.Show("создать депозитный счёт?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Add_Name.Visibility = Visibility.Visible;
                Add_Money.Visibility = Visibility.Visible;
                Add.Visibility = Visibility.Hidden;
                finally_add.Visibility = Visibility.Visible;
                choosen_derictory = 0;
                return;
            }
            if (MessageBox.Show("создать недепозитный счёт?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Add_Name.Visibility = Visibility.Visible;
                Add_Money.Visibility = Visibility.Visible;
                Add.Visibility = Visibility.Hidden;
                finally_add.Visibility = Visibility.Visible;
                choosen_derictory = 1;
            }
            else
            {
                return;
            }
            
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AugClick();
            Whathappends.Add(("добавление клиента"));


        }

        public void Finally(string Text)//method
        {
            string text = Add_Name.Text;
            if (!(tryCatch.Sum(Add_Money.Text)))
            {
                MessageBox.Show("введите подходящее число");
                return;
            }
            
          
            int AddMoney = Convert.ToInt32(Add_Money.Text);
            if (choosen_derictory == 0)
            {
                abonents1.Add(new Abonent(abonents1.Count, text, AddMoney));
                Text = "создан депозитный счёт";
            }
            if (choosen_derictory == 1)
            {
                abonents2.Add(new Abonent(abonents1.Count, text, AddMoney));
                Text = "создан не депозитный счёт";
            }
            ICollectionView view = CollectionViewSource.GetDefaultView(abonents1);
            view.Refresh();
            ICollectionView view1 = CollectionViewSource.GetDefaultView(abonents2);
            view1.Refresh();
            Add_Name.Visibility = Visibility.Hidden;
            Add_Money.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
            finally_add.Visibility = Visibility.Hidden;
            BT_Click?.Invoke(Text);
        }
        private void finally_add_Click(object sender, RoutedEventArgs e)
        {
            BT_Click += Print;
           Finally("text");
            BT_Click -= Print;
            Whathappends.Add(("клиент добавлен"));
            CB_WhatHappend.Items.Refresh();
        }

        public void send(string text)//method
        {
            switch (y)
            {
                case 0:
                    if (!(tryCatch.Close(LV_Deposite.SelectedIndex)))
                    {
                        MessageBox.Show("выберите объект");
                        return;
                    }
                    if (abonents1[LV_Deposite.SelectedIndex].Status == false)
                    {

                        return;
                    }
                    else
                    {

                        z++;
                        choose_send = LV_Deposite.SelectedIndex;
                        start = 0;



                    }

                    break;
                case 1:
                    if (!(tryCatch.Close(LV_NonDeposite.SelectedIndex)))
                    {
                        MessageBox.Show("выберите объект");
                        return;
                    }
                    if (abonents2[LV_NonDeposite.SelectedIndex].Status == false)
                    {
                        return;
                    }
                    else
                    {

                        z++;
                        choose_send = LV_NonDeposite.SelectedIndex;
                        start = 1;


                    }
                    break;
                default:
                    break;
            }
            Send.Visibility = Visibility.Hidden;
            Button_Get.Visibility = Visibility.Visible;
            BT_Click?.Invoke(text);
        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            BT_Click += Print;
            send("введите сумму");
            BT_Click -= Print;
            Whathappends.Add(("выбор куда отправить деньги"));


        }

        public void Get_Click(string text)
        {
            if (!tryCatch.Sum(TB_Money.Text))
            {
                MessageBox.Show("Введте число");
                return;

            }
            else
            {
                if (z == 1 && y == 0)
                {
                    z = 0;
                    choose_get = LV_Deposite.SelectedIndex;
                    end = 0;
                }
                else
                {
                    z = 0;
                    choose_get = LV_NonDeposite.SelectedIndex;
                    end = 1;
                }
                
                if (start == 0 && end == 0)
                {
                    abonents1[choose_send].SendMoney(Convert.ToInt32(TB_Money.Text), abonents1[choose_get]);
                }
                if (start == 1 && end == 0)
                {
                    abonents2[choose_send].SendMoney(Convert.ToInt32(TB_Money.Text), abonents1[choose_get]);
                }
                if (start == 0 && end == 1)
                {
                    abonents1[choose_send].SendMoney(Convert.ToInt32(TB_Money.Text), abonents2[choose_get]);
                }
                if (start == 1 && end == 1)
                {
                    abonents2[choose_send].SendMoney(Convert.ToInt32(TB_Money.Text), abonents2[choose_get]);
                }
                ICollectionView view = CollectionViewSource.GetDefaultView(abonents1);
                view.Refresh();
                ICollectionView view1 = CollectionViewSource.GetDefaultView(abonents2);
                view1.Refresh();
            }
            Button_Get.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Visible;
            BT_Click?.Invoke(text);
        }//method
        private void Button_Get_Click(object sender, RoutedEventArgs e)
        {

            BT_Click += Print;
            Get_Click("деньги переведены");
            BT_Click -= Print;
            Whathappends.Add(("деньги были отправлены"));

        }

        
    }
}
