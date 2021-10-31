using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhH60Store.Models {
    public class FormattingService {

        public string PhoneFormat(string pNum) {
            if (pNum != null)
                return "(" + pNum.Substring(0, 3) + ")-" + pNum.Substring(3, 3) + "-" + pNum.Substring(6, 4);
            else
                return pNum;
        }

        public string DateFormat(DateTime date) {
            return date.ToString("yyyy/MM/dd");
        }

        public string CurrencyFormat(Double? amount) {
            return $"{amount:C}";
        }

        public string DisplayProvince(string pv) {
            if (pv == "QC") {
                return "Quebec";
            } else if (pv == "ON") {
                return "Ontario";
            } else if (pv == "NB") {
                return "New Brunswick";
            } else if (pv == "MB") {
                return "Manitoba";
            } else {
                return pv;
            }
        }

        public string DisplayCreditCard(string cc) {
            return string.Format("{0:#### #### #### ####}", Convert.ToInt64(cc));
        }

    }
}
