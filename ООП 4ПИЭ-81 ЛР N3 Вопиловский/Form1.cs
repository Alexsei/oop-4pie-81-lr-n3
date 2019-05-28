using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ООП_4ПИЭ_81_ЛР_N3_Вопиловский
{
    public partial class Form1 : Form
    {
        Shop sh = new Shop(); // магазин 
        Goods Item1;  // вспомогательный объект класса Goods
        Goods Item2;  // вспомогательный объект класса Goods
        public Form1()
        {
            InitializeComponent();
            initDefault();    // значение по умолчанию
            ListViev();       // отобразить игрушки в магазине
            ListViewBasket(); // отобразить игрушки в корзине
            ListViewOrder();  // отобразить игрушки в истории заказов
        }

        private void initDefault()
        {
            sh.addStockItem("Кукла ЛОЛ", "meChina", 1, 599);
            sh.addStockItem("Кукла Беби Борн", "Born", 6, 1599);
            sh.addStockItem("Супер машинка", "AvtoRun", 2, 1990);
            sh.addStockItem("Водяной пистолет", "АкваПлюс", 5, 330);
            sh.addStockItem("Конструктор", "ООО Умник", 8, 300);
            sh.addStockItem("Говоломка", "ОАО Секрет", 7, 520);
            sh.addStockItem("Краски", "Триколор", 6, 1990);
            sh.addStockItem("Пластелин", "Залип", 5, 1990);
        }
        private void ListViev() // вывод данных на экран
        {
            listStoreView.Items.Clear(); // очистить списока магазина
            for (int i = 0; i < sh.StockLength; i++)
            {
                sh.getStockItem(i, out int id, out string name, out string producer, out int category, out int price);
                ListViewItem newAvto = new ListViewItem(Convert.ToString(id));
                newAvto.SubItems.Add(name);
                newAvto.SubItems.Add(producer);
                newAvto.SubItems.Add(Convert.ToString(category) + "+");
                newAvto.SubItems.Add(Convert.ToString(price) + ".00");
                listStoreView.Items.Add(newAvto);
            }
            label3.Text = ("Сумма: " + Convert.ToString(sh.getStockSumma) + ".00 рублей"); // сумма цен всех игрушек            
            label7.Text = ("от 1 до 7 лет: "+Convert.ToString(sh.getStockCatItens(1, 7)));// количество игрушек в категории от 1 до 7 лет
        }
        private void ListViewBasket() // вывод данных на экран listBasketView
        {
        listBasketView.Items.Clear(); // очистить списока магазина
            for (int i = 0; i<sh.BasketLength; i++)
            {
                sh.getBasketItem(i, out int id, out string name, out string producer, out int category, out int price);
        ListViewItem newAvto = new ListViewItem(Convert.ToString(i));
        newAvto.SubItems.Add(name);
                newAvto.SubItems.Add(producer);
                newAvto.SubItems.Add(Convert.ToString(category) + "+");
                newAvto.SubItems.Add(Convert.ToString(price) + ".00");
                listBasketView.Items.Add(newAvto);
            }
            label4.Text = ("Сумма: " + Convert.ToString(sh.getBasketSumma) + ".00 рублей");
        }
        private void ListViewOrder() // вывод данных на экран listOrderView
        {
            listOrderView.Items.Clear(); // очистить списока магазина
            for (int i = 0; i < sh.OrderLength; i++)
            {
                sh.getOrderItem(i, out int id, out string name, out string producer, out int category, out int price);
                ListViewItem newAvto = new ListViewItem(Convert.ToString(i));
                newAvto.SubItems.Add(name);
                newAvto.SubItems.Add(producer);
                newAvto.SubItems.Add(Convert.ToString(category) + "+");
                newAvto.SubItems.Add(Convert.ToString(price) + ".00");
                listOrderView.Items.Add(newAvto);
            }
            label5.Text = ("Сумма: " + Convert.ToString(sh.getOrderSumma) + ".00 рублей");
        }

        private void Button1_Click(object sender, EventArgs e)
        {  // добавить игрушку в корзину
            sh.getStockItem(Convert.ToInt32(listStoreView.FocusedItem.Text), 
                out int id,  out string name, out string producer, out int category, out int price);
            sh.addBasketItem(name, producer, category, price);
            ListViewBasket();
        }

        private void Button8_Click(object sender, EventArgs e)
        {  // фильтрация списка по категории
            List<Goods> Filter = sh.getStockFilter(Convert.ToInt32( comboBox1.Text));
            listStoreView.Items.Clear(); // очистить списока магазина
            int summa = 0;  // сумма за товарры
            for (int i = 0; i < Filter.Count; i++)
            {
                Filter[i].Get(out int id, out string name, out string producer, out int category, out int price);
                ListViewItem newAvto = new ListViewItem(Convert.ToString(id));
                newAvto.SubItems.Add(name);
                newAvto.SubItems.Add(producer);
                newAvto.SubItems.Add(Convert.ToString(category) + "+");
                newAvto.SubItems.Add(Convert.ToString(price) + ".00");
                listStoreView.Items.Add(newAvto);
                summa = summa + price;
            }
            label3.Text = ("Сумма: "+Convert.ToString( summa)+".00 рублей");
        }

        private void Button9_Click(object sender, EventArgs e)
        { // сбросить список игрушек в магазине
            ListViev();
        }

        private void Button2_Click(object sender, EventArgs e)
        {   // выбрать перваю игрушку
            sh.getStockItem(Convert.ToInt32(listStoreView.FocusedItem.Text), out int id, out string name, out string producer, out int category, out int price);
            Item1 = new Goods(id, name, producer, category, price);
            label1.Text = (name + " " + producer);
        }

        private void Button3_Click(object sender, EventArgs e)
        {   // выбрать вторую игрушку
            sh.getStockItem(Convert.ToInt32(listStoreView.FocusedItem.Text), out int id, out string name, out string producer, out int category, out int price);
            Item2 = new Goods(id, name, producer, category, price);
            label2.Text = (name + " " + producer);
        }

        private void Button4_Click(object sender, EventArgs e)
        {// создаем гибридную игрушку по условию задания
            if ((label1.Text != "-") && (label2.Text != "-")) // если игрушка выбрана
            {
                Goods newItem = Item1.Hybrid(Item1, Item2);
                sh.addStock(newItem); // добавить в магазин гибридную игрушку
                ListViev();  // обновим список на экране
            }
        }
        private void Button5_Click(object sender, EventArgs e)
        {   // заказать игрушки в корзине
            for (int i = 0; i < sh.BasketLength; i++)
            {  // добавление игрушек из корзины в историю заказов
                sh.getBasketItem(i, out int id, out string name, out string producer, out int category, out int price);
                sh.addOrderItem(name, producer, category, price);
            }
            sh.basketClear(); // очистить корзину
            ListViewBasket(); // обновить вывод корзины на экран 
            ListViewOrder();  // обновить вывод истории заказов на экран 
        }
        private void Button6_Click(object sender, EventArgs e)
        { // очистить список в корзине
            sh.basketClear(); // очистить
            ListViewBasket(); // обновить вывод на экран
        }

        private void Button7_Click(object sender, EventArgs e)
        {  // очистить список в истории заказов
            sh.orderClear();// очистить
            ListViewOrder();// обновить вывод на экран
        }
    }
}
