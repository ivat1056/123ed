using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class Material
    {
        public string materialSupliers
        {
            get
            {
                string suppliers = "";
                List<MaterialSupplier> materialSupplier = Base.BD.MaterialSupplier.Where(x => x.MaterialID == ID).ToList();
                for(int i = 0; i < materialSupplier.Count; i++)
                {
                    if(i == materialSupplier.Count - 1)
                    {
                        suppliers += materialSupplier[i].Supplier.Title;
                    }
                    else
                    {
                        suppliers += materialSupplier[i].Supplier.Title + ",";
                    }
                }
                return suppliers;
            }
        }
        public SolidColorBrush ColorStr
        {
            get
            {
                if(CountinStock < MinCount)
                {
                    SolidColorBrush minCount = (SolidColorBrush)new BrushConverter().ConvertFromString("#f19292");
                    return minCount;
                }
                else if((CountinStock/MinCount)*100 > 300)
                {
                    SolidColorBrush maxCount = (SolidColorBrush)new BrushConverter().ConvertFromString("#ffba01");
                    return maxCount;
                }
                else
                {
                    return Brushes.White;
                }
            }
        }
    }
}
