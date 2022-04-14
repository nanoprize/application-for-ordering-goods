using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ФорвестWhere
{
    public partial class Клиенты_магазина
    {
        public override string ToString()
        {
            //return base.ToString();
            return ФИО + номер + адрес+фото;
        }

    }
    public partial class Товары_магазина
    {
        public override string ToString()
        {
            //return base.ToString();
            return название + цена + наличие + фото;
        }

    }
    public partial class Ведение_заказов
    {
        public override string ToString()
        {
            //return base.ToString();
            return название_доставки + стоимость_доставки + сумма_с_доставкой;
        }

    }
}

