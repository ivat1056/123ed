using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ListMaterial.xaml
    /// </summary>
    public partial class ListMaterial : Page
    {
        public ListMaterial()
        {
            InitializeComponent();
            lvMaterials.ItemsSource = Base.BD.Material.ToList();
            List<MaterialType> materialTypes = Base.BD.MaterialType.ToList();
            cbFilter.Items.Add("Все типы");
            foreach(MaterialType materialType in materialTypes)
            {
                cbFilter.Items.Add(materialType.Title);
            }
            cbFilter.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;
            CountRecords.Text = Base.BD.Material.ToList().Count + " из " + Base.BD.Material.ToList().Count;
        }
        private void Filter()
        {
            List<Material> materials = Base.BD.Material.ToList();
            if (tbSearch.Text != "")
            {
                materials = materials.Where(x => x.Title.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
                List<Material> materials1 = Base.BD.Material.Where(x => x.Description.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
                foreach (Material material in materials1)
                {
                    materials.Add(material);
                }
                materials = materials.Distinct().ToList();
            }
            if(cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex != 0)
            {
                materials = materials.Where(x => x.MaterialType.Title == cbFilter.SelectedValue).ToList();
            }
            switch(cbSort.SelectedIndex)
            {
                case 1:
                    materials = materials.OrderBy(x => x.Title).ToList();
                    break;
                case 2:
                    materials = materials.OrderByDescending(x => x.Title).ToList();
                    break;
                case 3:
                    materials = materials.OrderBy(x => x.CountinStock).ToList();
                    break;
                case 4:
                    materials = materials.OrderByDescending(x => x.CountinStock).ToList();
                    break;
                case 5:
                    materials = materials.OrderBy(x => x.Cost).ToList();
                    break;
                case 6:
                    materials = materials.OrderByDescending(x => x.Cost).ToList();
                    break;
            }
            lvMaterials.ItemsSource = materials;
            if(materials.Count == 0)
            {
                MessageBox.Show("Результат не найден");
            }
            CountRecords.Text = materials.Count + " из " + Base.BD.Material.ToList().Count;
        }

        private void tbSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }


        private void lvMaterials_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lvMaterials.SelectedItems.Count > 1)
            {
                changeMinCost.Visibility = Visibility.Visible;
            }
            else
            {
                changeMinCost.Visibility = Visibility.Hidden;
            }
        }

        private void changeMinCost_Click(object sender, RoutedEventArgs e)
        {
            List<Material> materials = new List<Material>();
            foreach(Material material in lvMaterials.SelectedItems)
            {
                materials.Add(material);
            }
            ChangeMinCount changeMinCount = new ChangeMinCount(materials);
            changeMinCount.ShowDialog();
            lvMaterials.ItemsSource = Base.BD.Material.ToList();
            changeMinCost.Visibility = Visibility.Hidden;
        }
    }
}
